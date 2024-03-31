// Decompiled with JetBrains decompiler
// Type: SettlementFinances.CaravanHelper
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

using HarmonyLib;
using MCM.Abstractions.Base.Global;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace SettlementFinances
{
    [HarmonyPatch(typeof(DefaultClanFinanceModel), "AddIncomeFromParties")]
    public class CaravanHelper
    {
        [HarmonyPostfix]
        public static void Postfix(Clan clan, ref ExplainedNumber goldChange, bool applyWithdrawals = false)
        {
            float num = 0.0f;
            foreach (Hero hero in clan.Heroes)
            {
                if (hero.PartyBelongedTo == null)
                {
                    continue;
                }

                if (hero.PartyBelongedTo == MobileParty.MainParty)
                {
                    continue;
                }

                if (hero.PartyBelongedTo.LeaderHero != hero)
                {
                    continue;
                }

                MobileParty party = hero.PartyBelongedTo;
                if (party.IsActive && party.LeaderHero != clan.Leader && party.IsCaravan)
                {
                    num += (float)party.LeaderHero.GetSkillValue(DefaultSkills.Trade) * GlobalSettings<Configuration>.Instance.CaravanTariffPerTrade;
                    num += (float)party.LeaderHero.GetSkillValue(DefaultSkills.Steward) * GlobalSettings<Configuration>.Instance.CaravanTariffPerSteward;
                }
            }

            if (Hero.MainHero.GetPerkValue(DefaultPerks.Trade.ContentTrades))
            {
                num *= (float)(1.0 + (double)DefaultPerks.Trade.ContentTrades.PrimaryBonus / 100.0);
            }

            goldChange.Add((float)(int)num, new TextObject("Caravan Tariffs"));
        }
    }
}
