using MCM.Abstractions.Base.Global;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace SettlementFinances
{
    internal class VillageData
    {
        public static Dictionary<string, VillageData> All = new Dictionary<string, VillageData>();
        [SaveableField(0)]
        public string Id;
        [SaveableField(1)]
        public int BasePrice;
        [SaveableField(2)]
        public int Number;
        [SaveableField(3)]
        public int BaseProfit;
        [SaveableField(4)]
        public int Owned;
        [SaveableField(5)]
        public string Type;
        [SaveableField(6)]
        public bool KeepProducts = false;
        [SaveableField(7)]
        public int GoldBuffer = 0;
        public static readonly Dictionary<string, VillageData.ProductionType> ProductionTypes = new Dictionary<string, VillageData.ProductionType>()
    {
      {
        "olive_trees",
        new VillageData.ProductionType()
        {
          Property = "Olive Orchards",
          ItemId = "olives",
          Value = 3000
        }
      },
      {
        "wheat_farm",
        new VillageData.ProductionType()
        {
          Property = "Wheat Fields",
          ItemId = "grain",
          Value = 3000
        }
      },
      {
        "date_farm",
        new VillageData.ProductionType()
        {
          Property = "Date Plantations",
          ItemId = "date_fruit",
          Value = 3000
        }
      },
      {
        "flax_plant",
        new VillageData.ProductionType()
        {
          Property = "Flax Plantations",
          ItemId = "flax",
          Value = 3000
        }
      },
      {
        "silk_plant",
        new VillageData.ProductionType()
        {
          Property = "Cotton Plantations",
          ItemId = "cotton",
          Value = 3000
        }
      },
      {
        "salt_mine",
        new VillageData.ProductionType()
        {
          Property = "Salt Galleries",
          ItemId = "salt",
          Value = 3000
        }
      },
      {
        "clay_mine",
        new VillageData.ProductionType()
        {
          Property = "Clay Pits",
          ItemId = "clay",
          Value = 4000
        }
      },
      {
        "iron_mine",
        new VillageData.ProductionType()
        {
          Property = "Iron Galleries",
          ItemId = "iron",
          Value = 8000
        }
      },
      {
        "silver_mine",
        new VillageData.ProductionType()
        {
          Property = "Silver Galleries",
          ItemId = "silver",
          Value = 12000
        }
      },
      {
        "vineyard",
        new VillageData.ProductionType()
        {
          Property = "Vineyards",
          ItemId = "grapes",
          Value = 3000
        }
      },
      {
        "fisherman",
        new VillageData.ProductionType()
        {
          Property = "Fishing Boats",
          ItemId = "fish",
          Value = 2500
        }
      },
      {
        "lumberjack",
        new VillageData.ProductionType()
        {
          Property = "Forest Acres",
          ItemId = "hardwood",
          Value = 4000
        }
      },
      {
        "empire_horse_ranch",
        new VillageData.ProductionType()
        {
          Property = "Imperial Stables",
          Value = 6000
        }
      },
      {
        "imperial_horse_ranch",
        new VillageData.ProductionType()
        {
          Property = "Imperial Stables",
          Value = 6000
        }
      },
      {
        "midlands_palfrey",
        new VillageData.ProductionType()
        {
          Property = "Midlands Palfrey Stables",
          Value = 6000
        }
      },
      {
        "battanian_horse_ranch",
        new VillageData.ProductionType()
        {
          Property = "Battanian Stables",
          Value = 6000
        }
      },
      {
        "steppe_horse_ranch",
        new VillageData.ProductionType()
        {
          Property = "Steppe Stables",
          Value = 6000
        }
      },
      {
        "desert_horse_ranch",
        new VillageData.ProductionType()
        {
          Property = "Desert Stables",
          Value = 6000
        }
      },
      {
        "sheep_farm",
        new VillageData.ProductionType()
        {
          Property = "Sheep Farms",
          Value = 5000
        }
      },
      {
        "cattle_farm",
        new VillageData.ProductionType()
        {
          Property = "Cow Farms",
          Value = 5000
        }
      },
      {
        "swine_farm",
        new VillageData.ProductionType()
        {
          Property = "Hog Farms",
          ItemId = "fur",
          Value = 5000
        }
      },
      {
        "trapper",
        new VillageData.ProductionType()
        {
          Property = "Trapper Fields",
          ItemId = "fur",
          Value = 5000
        }
      }
    };

        public static int MAX_VILLAGES => GlobalSettings<Configuration>.Instance.VillageMax + (int)((double)Hero.MainHero.Clan.Tier * (double)GlobalSettings<Configuration>.Instance.VillageMaxPerTier);

        public VillageData(string id)
        {
            this.Id = id;
            VillageData.ProductionType productionType;
            try
            {
                productionType = VillageData.ProductionTypes[((MBObjectBase)this.Settlement.Village.VillageType).GetName().ToString()];
            }
            catch
            {
                productionType = VillageData.ProductionTypes["midlands_palfrey"];
            }
            this.BasePrice = productionType.Price;
            this.Number = productionType.Number;
            this.BaseProfit = productionType.Profit;
            this.Owned = 0;
            this.Type = productionType.Property;
            this.KeepProducts = true;
        }

        public void DailyTick()
        {
            this.GoldBuffer += (int)this.TotalProfit;
            if (this.KeepProducts)
            {
                return;
            }

            this.Settlement.ItemRoster.Add(this.Products);
            this.ConsumeGoldBuffer();
        }

        public static void TotalDailyTick()
        {
            foreach (KeyValuePair<string, VillageData> keyValuePair in VillageData.All)
            {
                keyValuePair.Value.DailyTick();
            }
        }

        public float ActualProfit => (float)((double)this.BaseProfit * (double)GlobalSettings<Configuration>.Instance.VillageIncomeMultiplier * (1.0 + (double)this.Settlement.Village.Hearth * (double)GlobalSettings<Configuration>.Instance.VillagePercentagePerHearth));

        public float ActualPrice => (float)((double)this.BasePrice * (double)GlobalSettings<Configuration>.Instance.VillagePriceMultiplier * (1.0 + (double)this.Settlement.Village.Hearth * (double)GlobalSettings<Configuration>.Instance.VillagePercentagePerHearth));

        public float SellPrice => this.ActualPrice * GlobalSettings<Configuration>.Instance.VillageSellMultiplier;

        public float TotalProfit => !this.Settlement.IsRaided ? (float)this.Owned * this.ActualProfit : 0.0f;

        public float HearthGain => (double)this.Settlement.Village.Hearth < (double)GlobalSettings<Configuration>.Instance.VillageHearthCap ? (float)(int)((double)this.Owned / (double)(this.Owned + this.Number) * (double)GlobalSettings<Configuration>.Instance.VillageHearthGain) : 0.0f;

        public Settlement Settlement => Settlement.Find(this.Id);

        public int ProductNumber => this.GoldBuffer / this.ProductPrice;

        public ItemRosterElement Products => new ItemRosterElement(this.Settlement.Village.VillageType.PrimaryProduction, this.ProductNumber);

        public int ProductPrice => this.Settlement.IsVillage ? ((SettlementComponent)this.Settlement.Village).GetItemPrice(this.Settlement.Village.VillageType.PrimaryProduction, (MobileParty)null, false) : int.MaxValue;

        public void ConsumeGoldBuffer()
        {
            this.GoldBuffer -= this.ProductPrice * this.Products.Amount;
        }

        public void TakeProducts()
        {
            try
            {
                Hero.MainHero.PartyBelongedTo.ItemRoster.Add(this.Products);
                this.ConsumeGoldBuffer();
                InformationManager.DisplayMessage(new InformationMessage(new TextObject("Took {PRODUCT_STRING}").ToString()));
            }
            catch
            {
                InformationManager.DisplayMessage(new InformationMessage(new TextObject("Some unknown error occured. Reopen the menu please!").ToString()));
            }
        }

        public static float GetPlayerProfit(bool applyWithdrawals)
        {
            int playerProfit = 0;
            foreach (KeyValuePair<string, VillageData> keyValuePair in VillageData.All)
            {
                if (!keyValuePair.Value.KeepProducts)
                {
                    playerProfit += (int)keyValuePair.Value.TotalProfit;
                    if (applyWithdrawals)
                    {
                        keyValuePair.Value.ConsumeGoldBuffer();
                    }
                }
            }
            return (float)playerProfit;
        }

        public static VillageData Of(Settlement village)
        {
            return VillageData.Find(((MBObjectBase)village).StringId);
        }

        public static VillageData Find(string id)
        {
            try
            {
                return VillageData.All[id];
            }
            catch
            {
            }
            return (VillageData)null;
        }

        public static VillageData Current
        {
            get
            {
                if (!VillageData.All.ContainsKey(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId))
                {
                    VillageData.All.Add(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId, new VillageData(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId));
                }

                return VillageData.Find(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId);
            }
        }

        public void BuyLand()
        {
            Hero.MainHero.Gold -= (int)this.ActualPrice;
            ++this.Owned;
            --this.Number;
        }

        public void SellLand()
        {
            Hero.MainHero.Gold += (int)this.SellPrice;
            --this.Owned;
            ++this.Number;
        }

        public static float ProfitFrom(string type)
        {
            float num = 0.0f;
            foreach (KeyValuePair<string, VillageData> keyValuePair in VillageData.All)
            {
                if (keyValuePair.Value.Type == type)
                {
                    num += keyValuePair.Value.TotalProfit;
                }
            }
            return num;
        }

        public bool HasInvestments()
        {
            return this.Owned > 0;
        }

        public static List<VillageData> GetInvestedVillages()
        {
            List<VillageData> investedVillages = new List<VillageData>();
            foreach (KeyValuePair<string, VillageData> keyValuePair in VillageData.All)
            {
                if (keyValuePair.Value.HasInvestments())
                {
                    investedVillages.Add(keyValuePair.Value);
                }
            }
            return investedVillages;
        }

        public class ProductionType
        {
            public static readonly int MIN_FACTOR = 16000;
            public static readonly int MAX_FACTOR = 25000;

            public string Property
            {
                get; set;
            }

            public string ItemId
            {
                get; set;
            }

            public int Number => MBRandom.RandomInt(Math.Max(1, VillageData.ProductionType.MIN_FACTOR / this.Value), Math.Max(1, VillageData.ProductionType.MAX_FACTOR / this.Value));

            public int Price => MBRandom.RandomInt((int)((double)this.Value * 0.95), (int)((double)this.Value * 1.3));

            public int Profit => MBRandom.RandomInt((int)((double)this.Value * 0.03), (int)((double)this.Value * 0.04));

            public int Value
            {
                get; set;
            }
        }
    }
}
