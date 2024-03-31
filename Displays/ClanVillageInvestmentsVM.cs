using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;

namespace SettlementFinances.Displays
{
    internal class ClanVillageInvestmentsVM : ClanFinanceIncomeItemBaseVM
    {
        public ClanVillageInvestmentsVM(
          Action<ClanFinanceIncomeItemBaseVM> onSelection,
          Action onRefresh)
          : base(onSelection, onRefresh)
        {
            this.Name = "Village Investments";
            this.Income = (int)VillageData.GetPlayerProfit(false);
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
            foreach (KeyValuePair<string, VillageData> keyValuePair in VillageData.All)
            {
                KeyValuePair<string, VillageData> entry = keyValuePair;
                if ((double)entry.Value.TotalProfit != 0.0 || entry.Value.Settlement.IsRaided)
                {
                    string str1 = entry.Value.Settlement.Name.ToString();
                    int num;
                    string str2;
                    if (!entry.Value.Settlement.IsRaided)
                    {
                        num = (int)entry.Value.TotalProfit;
                        str2 = num.ToString() + " denars";
                    }
                    else
                    {
                        str2 = "Raided";
                    }

                    string str3 = str2;
                    if (entry.Value.KeepProducts)
                    {
                        string[] strArray = entry.Value.Settlement.Village.VillageType.PrimaryProduction.Name.ToString().Split(' ');
                        num = entry.Value.ProductNumber;
                        str3 = num.ToString() + "x " + strArray[strArray.Length - 1];
                    }
                    BasicTooltipViewModel tooltipViewModel = new BasicTooltipViewModel((Func<string>)(() => entry.Value.Type + ": " + entry.Value.Number.ToString()));
                    this.ItemProperties.Add(new SelectableItemPropertyVM(str1, str3, false, tooltipViewModel));
                }
            }
        }
    }
}
