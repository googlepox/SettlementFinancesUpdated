using MCM.Abstractions.Base.Global;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;

namespace SettlementFinances
{
    internal class SettlementSupport
    {
        public static void DailyTick()
        {
            foreach (Settlement settlement in (List<Settlement>)Settlement.All)
            {
                if (settlement.IsTown)
                {
                    Hero leader = settlement.OwnerClan.Leader;
                    if (settlement.Town.Gold <GlobalSettings<Configuration>.Instance.TownSupportThreshold && leader.Gold > GlobalSettings<Configuration>.Instance.OwnerSupportThreshold)
                    {
                        settlement.Town.ChangeGold(GlobalSettings<Configuration>.Instance.TownSupportThreshold);
                        leader.ChangeHeroGold(-GlobalSettings<Configuration>.Instance.TownSupportThreshold);
                    }
                }
            }
        }
    }
}
