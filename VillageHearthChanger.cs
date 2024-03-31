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
