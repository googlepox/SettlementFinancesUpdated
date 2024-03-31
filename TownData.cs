using MCM.Abstractions.Base.Global;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace SettlementFinances
{
    internal class TownData
    {
        public static TownData Last;
        public static Dictionary<string, TownData> All = new Dictionary<string, TownData>();
        [SaveableField(1)]
        public string id;
        [SaveableField(2)]
        public bool WorkerExport = false;
        [SaveableField(3)]
        public bool MilitiaExport = false;
        [SaveableField(4)]
        public bool FoodExport = false;
        [SaveableField(5)]
        public bool ExtraRent = false;

        public static void TotalDailyTick()
        {
            foreach (KeyValuePair<string, TownData> keyValuePair in TownData.All)
            {
                keyValuePair.Value.ApplyWithdrawals();
            }
        }

        public void ApplyWithdrawals()
        {
            if (this.ExtraRent && (double)this.Settlement.Town.Loyalty != 100.0)
            {
                this.Settlement.Town.Loyalty = Math.Max(0.0f, this.Settlement.Town.Loyalty - this.Settlement.Town.LoyaltyChange);
            }

            if (this.WorkerExport)
            {
                this.Settlement.Town.Prosperity = Math.Max(0.0f, this.Settlement.Town.Prosperity - this.Settlement.Town.ProsperityChange);
            }

            if (this.MilitiaExport)
            {
                this.Settlement.Militia = Math.Max(0.0f, this.Settlement.Militia - this.Settlement.Town.MilitiaChange);
            }

            if (!this.FoodExport || (double)((Fief)this.Settlement.Town).FoodStocks == (double)this.Settlement.Town.FoodStocksUpperLimit())
            {
                return;
            } ((Fief)this.Settlement.Town).FoodStocks = Math.Max(0.0f, ((Fief)this.Settlement.Town).FoodStocks - this.Settlement.Town.FoodChange);
        }

        public static TownData Current => TownData.Find(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId);

        public static float PlayerProfit(bool applyWithdrawals)
        {
            float num = 0.0f;
            foreach (KeyValuePair<string, TownData> keyValuePair in TownData.All)
            {
                num += keyValuePair.Value.Profit();
            }

            return (float)(int)num;
        }

        public float ProsperityProfit()
        {
            if (this.Settlement.IsTown && this.WorkerExport)
            {
                float prosperityChange = this.Settlement.Town.ProsperityChange;
                if ((double)prosperityChange > 0.0)
                {
                    return prosperityChange * (float)GlobalSettings<Configuration>.Instance.TownProsperityProfit;
                }
            }
            return 0.0f;
        }

        public float MilitiaProfit()
        {
            if (this.Settlement.IsTown && this.MilitiaExport)
            {
                float militiaChange = this.Settlement.Town.MilitiaChange;
                if ((double)militiaChange > 0.0)
                {
                    return militiaChange * (float)GlobalSettings<Configuration>.Instance.TownMilitiaProfit;
                }
            }
            return 0.0f;
        }

        public float FoodProfit()
        {
            if (this.Settlement.IsTown && this.FoodExport)
            {
                float foodChange = this.Settlement.Town.FoodChange;
                if ((double)foodChange > 0.0)
                {
                    return foodChange * (float)GlobalSettings<Configuration>.Instance.TownFoodProfit;
                }
            }
            return 0.0f;
        }

        public float RentProfit()
        {
            if (this.Settlement.IsTown && this.ExtraRent)
            {
                float num = (float)(1.0 * (double)((Fief)this.Settlement.Town).Settlement.Town.Prosperity * ((double)this.Settlement.Town.Loyalty / 100.0));
                if ((double)num > 0.0)
                {
                    return num * GlobalSettings<Configuration>.Instance.TownRentProfit;
                }
            }
            return 0.0f;
        }

        public TownData(string id)
        {
            this.id = id;
            TownData.Last = this;
        }

        public TownData(string id, float prosperity, float militia, float food, float rent)
        {
            this.id = id;
            TownData.Last = this;
        }

        public static TownData Of(Town town)
        {
            return TownData.Find(((MBObjectBase)((SettlementComponent)town).Settlement).StringId);
        }

        public static TownData Find(string stringId)
        {
            try
            {
                return TownData.All[stringId];
            }
            catch
            {
                return (TownData)null;
            }
        }

        public Settlement Settlement => Settlement.Find(this.id);

        public float Profit()
        {
            return this.Settlement.OwnerClan != Hero.MainHero.Clan ? 0.0f : 0.0f + this.ProsperityProfit() + this.MilitiaProfit() + this.FoodProfit() + this.RentProfit();
        }
    }
}
