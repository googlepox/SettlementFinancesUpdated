// Decompiled with JetBrains decompiler
// Type: SettlementFinances.Displays.ClanBankVM
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;

namespace SettlementFinances.Displays
{
    internal class ClanBankVM : ClanFinanceIncomeItemBaseVM
    {
        public ClanBankVM(Action<ClanFinanceIncomeItemBaseVM> onSelection, Action onRefresh)
          : base(onSelection, onRefresh)
        {
            this.Name = "Bank of " + BankData.CurrentBank.Settlement.Name?.ToString();
            this.Income = (int)((double)BankData.CurrentBank.Interest * (double)BankData.CurrentBank.Gold / 100.0);
            this.IncomeValueText = this.DetermineIncomeText(this.Income);
            this.Visual = new ImageIdentifierVM(CharacterCode.CreateFrom((BasicCharacterObject)Hero.MainHero.CharacterObject));
            this.IncomeTypeAsEnum = IncomeTypes.CommonArea;
            this.PopulateActionList();
            this.PopulateStatsList();
        }

        protected override void PopulateActionList()
        {
        }

        protected override void PopulateStatsList()
        {
            this.ItemProperties.Add(new SelectableItemPropertyVM("Gold", ((int)BankData.CurrentBank.Gold).ToString(), false, (BasicTooltipViewModel)null));
            this.ItemProperties.Add(new SelectableItemPropertyVM("Interest", BankData.CurrentBank.Interest.ToString() + "%", false, (BasicTooltipViewModel)null));
            this.ItemProperties.Add(new SelectableItemPropertyVM("Next Gain", ((int)((double)BankData.CurrentBank.Interest * (double)BankData.CurrentBank.Gold / 100.0)).ToString(), false, (BasicTooltipViewModel)null));
            if ((double)BankData.CurrentBank.Loan > 0.0)
            {
                this.ItemProperties.Add(new SelectableItemPropertyVM("Loan", BankData.CurrentBank.Loan.ToString(), false, (BasicTooltipViewModel)null));
                this.ItemProperties.Add(new SelectableItemPropertyVM("Days left", BankData.CurrentBank.LoanDays.ToString(), false, (BasicTooltipViewModel)null));
            }
            else
            {
                this.ItemProperties.Add(new SelectableItemPropertyVM("Loan", "None", false, (BasicTooltipViewModel)null));
            }
        }
    }
}
