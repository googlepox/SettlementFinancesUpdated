// Decompiled with JetBrains decompiler
// Type: SettlementFinances.SettlementSupport
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

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
