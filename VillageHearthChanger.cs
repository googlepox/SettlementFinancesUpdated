// Decompiled with JetBrains decompiler
// Type: SettlementFinances.VillageHearthChanger
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Localization;

namespace SettlementFinances
{
    [HarmonyPatch(typeof(DefaultSettlementProsperityModel), "CalculateHearthChange")]
    public class VillageHearthChanger
    {
        [HarmonyPostfix]
        public static void Postfix(Village village, ref ExplainedNumber __result)
        {
            VillageData villageData = VillageData.Of(((SettlementComponent)village).Settlement);
            if (villageData == null || village.VillageState != 0)
            {
                return;
            }

            try
            {
                float hearthGain = villageData.HearthGain;
                __result.Add(hearthGain, new TextObject("Generous Investors"));
            }
            catch
            {
            }
        }
    }
}
