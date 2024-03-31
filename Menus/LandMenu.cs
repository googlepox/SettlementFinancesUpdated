// Decompiled with JetBrains decompiler
// Type: SettlementFinances.Menus.LandMenu
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SettlementFinances.Menus
{
    internal class LandMenu : BaseMenu
    {
        public static string MENU_ID = "land_menu";
        public static readonly string OPTION_ID = "land_menu_option";

        public LandMenu(CampaignGameStarter gameStarter)
          : base(gameStarter)
        {
        }

        protected override void RegisterMenu()
        {
            this.GameStarter.AddGameMenu(MENU_ID, "This village is prosperous due to its {LAND_NAME}. Villagers here are working all day long in them to make a living.\n - You own {LAND_OWNED} {LAND_NAME} out of {LAND_NUMBER} here.\n - One property produces {LAND_PROFIT}{GOLD_ICON} worth of products a day.\n - Total production is estimated at {LAND_TOTAL_PROFIT}{GOLD_ICON} a day.", (OnInitDelegate)null, GameOverlays.MenuOverlayType.SettlementWithCharacters);
            this.GameStarter.AddGameMenuOption("village", LandMenu.OPTION_ID, "Investments", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Trade;
                if (VillageData.GetInvestedVillages().Count >= VillageData.MAX_VILLAGES && !VillageData.Current.HasInvestments())
                {
                    x.IsEnabled = false;
                    x.Tooltip = new TextObject("You have already invested in " + VillageData.MAX_VILLAGES.ToString() + ".");
                }
                return true;
            }), args =>
            {
                Hero.MainHero.CurrentSettlement.Village.VillageType.GetName().ToString();
                Settlement currentSettlement = Hero.MainHero.CurrentSettlement;
                if (!VillageData.All.ContainsKey(currentSettlement.StringId))
                {
                    VillageData.All.Add(currentSettlement.StringId, new VillageData(currentSettlement.StringId));
                }

                this.UpdateText();
                GameMenu.SwitchToMenu(MENU_ID);
            }, false, 4, false);
            this.GameStarter.AddGameMenuOption(MENU_ID, "village_land_buy", "Buy Property ({LAND_PRICE}{GOLD_ICON})", x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Bribe;
                if (VillageData.Current.BasePrice > Hero.MainHero.Gold)
                {
                    x.IsEnabled = false;
                    x.Tooltip = new TextObject("You don't have enough gold.");
                }
                else if (VillageData.Current.Number == 0)
                {
                    x.IsEnabled = false;
                    x.Tooltip = new TextObject("There is no more property to buy.");
                }
                else
                {
                    x.IsEnabled = true;
                    x.Tooltip = new TextObject("");
                }
                return true;
            }, x =>
            {
                VillageData.Current.BuyLand();
                this.UpdateText();
                GameMenu.SwitchToMenu(MENU_ID);
            }, false, -1, false);
            this.GameStarter.AddGameMenuOption(MENU_ID, "village_land_sell", "Sell Property ({LAND_SELL_PRICE}{GOLD_ICON})", x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Bribe;
                if (VillageData.Current.Owned == 0)
                {
                    x.IsEnabled = false;
                    x.Tooltip = new TextObject("You don't have any property to sell.");
                }
                else
                {
                    x.IsEnabled = true;
                    x.Tooltip = new TextObject("");
                }
                return true;
            }, (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                VillageData.Current.SellLand();
                this.UpdateText();
                GameMenu.SwitchToMenu(MENU_ID);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(MENU_ID, "village_land_option", "Keep products?", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Manage;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => InformationManager.ShowInquiry(new InquiryData(new TextObject("Keep Products?").ToString(), new TextObject("Should peasants keep what they produce for you instead of selling them for profit?").ToString(), true, true, "Yes", "No", (Action)(() =>
            {
                VillageData.Current.KeepProducts = true;
                this.UpdateText();
                GameMenu.SwitchToMenu(MENU_ID);
            }), (Action)(() =>
            {
                VillageData.Current.KeepProducts = false;
                this.UpdateText();
                GameMenu.SwitchToMenu(MENU_ID);
            }), ""), false)), false, -1, false);
            this.GameStarter.AddGameMenuOption(MENU_ID, "village_land_storage", "Take Products ({PRODUCT_STRING})", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Continue;
                if (VillageData.Current.ProductNumber == 0)
                {
                    x.Tooltip = new TextObject("The villagers don't have anything for you right now.");
                    x.IsEnabled = false;
                }
                return VillageData.Current.KeepProducts;
            }), (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                VillageData.Current.TakeProducts();
                this.UpdateText();
                GameMenu.SwitchToMenu(MENU_ID);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(MENU_ID, "village_land_leave", "Leave", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Leave;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => GameMenu.SwitchToMenu("village")), false, -1, false);
        }

        public void UpdateText()
        {
            VillageData villageData = VillageData.Current;
            MBTextManager.SetTextVariable("LAND_PROFIT", ((int)villageData.ActualProfit).ToString(), false);
            MBTextManager.SetTextVariable("LAND_NUMBER", (villageData.Number + villageData.Owned).ToString(), false);
            MBTextManager.SetTextVariable("LAND_PRICE", ((int)villageData.ActualPrice).ToString(), false);
            MBTextManager.SetTextVariable("LAND_SELL_PRICE", ((int)villageData.SellPrice).ToString(), false);
            MBTextManager.SetTextVariable("LAND_OWNED", villageData.Owned.ToString(), false);
            MBTextManager.SetTextVariable("LAND_NAME", villageData.Type.ToString(), false);
            MBTextManager.SetTextVariable("LAND_TOTAL_PROFIT", ((int)villageData.TotalProfit).ToString(), false);
            string text = "PRODUCT_STRING";
            string str = villageData.ProductNumber.ToString();
            string str2 = " x ";
            TextObject name = VillageData.Current.Settlement.Village.VillageType.PrimaryProduction.Name;
            MBTextManager.SetTextVariable(text, str + str2 + ((name != null) ? name.ToString() : null), false);
        }
    }
}
