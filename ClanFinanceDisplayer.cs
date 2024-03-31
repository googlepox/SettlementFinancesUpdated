using HarmonyLib;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SettlementFinances
{
    [HarmonyPatch(typeof(DefaultClanFinanceModel), "CalculateClanIncomeInternal")]
    public class ClanFinanceDisplayer
    {
        [HarmonyPostfix]
        public static void Postfix(Clan clan, ref ExplainedNumber goldChange, bool applyWithdrawals = false)
        {
            try
            {
                if ((double)TownData.PlayerProfit(false) != 0.0)
                {
                    goldChange.Add(TownData.PlayerProfit(applyWithdrawals), new TextObject("Town Finances"));
                }
            }
            catch
            {
            }
            try
            {
                string str1 = "Sold village goods (";
                foreach (KeyValuePair<string, VillageData> keyValuePair in VillageData.All)
                {
                    if (!keyValuePair.Value.Settlement.IsRaided && (double)keyValuePair.Value.TotalProfit != 0.0 && !keyValuePair.Value.KeepProducts)
                    {
                        str1 = str1 + keyValuePair.Value.Settlement.Name?.ToString() + ", ";
                    }
                }
                float playerProfit = VillageData.GetPlayerProfit(applyWithdrawals);
                if ((double)playerProfit != 0.0)
                {
                    string str2 = str1.Substring(0, str1.Length - 2) + ")";
                    goldChange.Add(playerProfit, new TextObject(str2));
                }
                string str3 = "Kept village goods (";
                float num = 0.0f;
                foreach (KeyValuePair<string, VillageData> keyValuePair in VillageData.All)
                {
                    if (!keyValuePair.Value.Settlement.IsRaided && (double)keyValuePair.Value.TotalProfit != 0.0 && keyValuePair.Value.KeepProducts)
                    {
                        str3 = str3 + keyValuePair.Value.Settlement.Name?.ToString() + ", ";
                        num += 10f;
                    }
                }
                if ((double)num == 0.0)
                {
                    return;
                }

                string str4 = str3.Substring(0, str3.Length - 2) + ")";
                goldChange.Add(num, new TextObject(str4));
            }
            catch (Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage(ex.Message));
            }
        }
    }
}
