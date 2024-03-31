// Decompiled with JetBrains decompiler
// Type: SettlementFinances.BankData
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

using MCM.Abstractions.Base.Global;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace SettlementFinances
{
    internal class BankData
    {
        public static readonly string MERCHANTS = "Guild of Merchants";
        public static readonly string ARTISANS = "Guild of Artisans";
        public static readonly string GANG_LEADERS = "Gang Leaders";
        public static readonly string HEADMEN = "Village Headmen";
        public static readonly string LANDOWNERS = "Village Landowners";
        public static BankData CurrentBank;
        [SaveableField(0)]
        public readonly string id;
        [SaveableField(1)]
        public float Gold = 0.0f;
        [SaveableField(2)]
        public float Loan = 0.0f;
        [SaveableField(3)]
        public float LoanDays = -1f;
        [SaveableField(4)]
        public string investment = BankData.MERCHANTS;

        public BankData(string id)
        {
            BankData.CurrentBank = this;
            this.id = id;
        }

        public Settlement Settlement => Settlement.Find(this.id);

        public static float NewAccountPrice(Settlement settlement)
        {
            return (float)GlobalSettings<Configuration>.Instance.BankAccountPricePerRenown + Hero.MainHero.Clan.Renown * (float)GlobalSettings<Configuration>.Instance.BankAccountPricePerRenown;
        }

        public static float InterestFor(Settlement settlement)
        {
            return settlement.Town.Prosperity / 10000f * GlobalSettings<Configuration>.Instance.BankInterestRate;
        }

        public float Interest => BankData.InterestFor(this.Settlement);

        public float Profit => this.Interest * this.Gold;

        public static bool HaveBank => BankData.CurrentBank != null;

        public static bool IsMainBank()
        {
            return BankData.CurrentBank != null && BankData.CurrentBank.Settlement == Hero.MainHero.CurrentSettlement;
        }

        public float MaxLoan => Math.Min((float)GlobalSettings<Configuration>.Instance.BankLoanCap, Hero.MainHero.Clan.Renown * (float)GlobalSettings<Configuration>.Instance.BankMaxLoanPerRenown);

        public void SetUpLoan(float amount)
        {
            Hero.MainHero.Gold += (int)amount;
            this.Loan = amount * (1f + GlobalSettings<Configuration>.Instance.BankLoanInterest);
            this.LoanDays = (float)GlobalSettings<Configuration>.Instance.BankLoanDays;
        }

        public void PayLoan()
        {
            Hero.MainHero.Gold -= (int)this.Loan;
            this.Loan = 0.0f;
            this.LoanDays = -1f;
        }

        public static void DailyTick()
        {
            if (!BankData.HaveBank)
            {
                return;
            }

            if ((double)BankData.CurrentBank.Loan > 0.0)
            {
                if ((double)BankData.CurrentBank.LoanDays > 0.0)
                {
                    --BankData.CurrentBank.LoanDays;
                }
                else if ((double)BankData.CurrentBank.LoanDays == 0.0)
                {
                    BankData.CurrentBank.Loan *= 1f + GlobalSettings<Configuration>.Instance.BankLoanInterest;
                    BankData.CurrentBank.LoanDays = -1f;
                }
                else
                {
                    float num1 = Math.Min(BankData.CurrentBank.Loan, (float)Hero.MainHero.Gold);
                    BankData.CurrentBank.Loan -= num1;
                    Hero.MainHero.Gold -= (int)num1;
                    float num2 = Math.Min(BankData.CurrentBank.Loan, BankData.CurrentBank.Gold);
                    BankData.CurrentBank.Loan -= num2;
                    BankData.CurrentBank.Gold -= (float)(int)num2;
                    InformationManager.ShowInquiry(new InquiryData(new TextObject("Forceful loan payment").ToString(), new TextObject("You bank has taken " + num1.ToString() + " denars from you and " + num2.ToString() + " denars from your account.").ToString(), true, true, "Ok", "God damn it!", (Action)(() => InformationManager.HideInquiry()), (Action)(() => InformationManager.HideInquiry()), ""), false);
                }
            }
            else
            {
                BankData.CurrentBank.Gold *= (float)(1.0 + (double)BankData.InterestFor(BankData.CurrentBank.Settlement) / 100.0);
                bool flag = (double)(BankData.CurrentBank.Gold / (float)GlobalSettings<Configuration>.Instance.NotableInfluenceProbability) > new Random().NextDouble();
                foreach (Hero notable in (List<Hero>)BankData.CurrentBank.Settlement.Notables)
                {
                    if (flag && (notable.IsMerchant && BankData.CurrentBank.investment == BankData.MERCHANTS || notable.IsArtisan && BankData.CurrentBank.investment == BankData.ARTISANS || notable.IsGangLeader && BankData.CurrentBank.investment == BankData.GANG_LEADERS) && notable.Power < GlobalSettings<Configuration>.Instance.NotableInfluenceCap)
                    {
                        notable.AddPower(GlobalSettings<Configuration>.Instance.NotableInfluenceAmount);
                    }
                }
                foreach (SettlementComponent boundVillage in (List<Village>)BankData.CurrentBank.Settlement.BoundVillages)
                {
                    foreach (Hero notable in (List<Hero>)boundVillage.Settlement.Notables)
                    {
                        if (flag)
                        {
                            if (notable.IsHeadman && BankData.CurrentBank.investment == BankData.HEADMEN)
                            {
                                if (notable.Power < GlobalSettings<Configuration>.Instance.NotableInfluenceCap)
                                {
                                    notable.AddPower(GlobalSettings<Configuration>.Instance.NotableInfluenceAmount);
                                }
                            }
                            else if (notable.Power < GlobalSettings<Configuration>.Instance.NotableInfluenceCap)
                            {
                                notable.AddPower(GlobalSettings<Configuration>.Instance.NotableInfluenceAmount);
                            }
                        }
                    }
                }
            }
        }
    }
}
