// Decompiled with JetBrains decompiler
// Type: SettlementFinances.Menus.FinanceMenu
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
using TaleWorlds.ObjectSystem;

namespace SettlementFinances.Menus
{
    internal class FinanceMenu : BaseMenu
    {
        public static readonly string MENU_ID = "finance_menu";
        public static readonly string OPTION_ID = "finance_menu_button";

        public FinanceMenu(CampaignGameStarter gameStarter)
          : base(gameStarter)
        {
        }

        protected override void RegisterMenu()
        {
            MBTextManager.SetTextVariable("FINANCE_TOWN_PROFIT", 0, false);
            MBTextManager.SetTextVariable("FINANCE_MILITIA_PROFIT", 0, false);
            MBTextManager.SetTextVariable("FINANCE_PROSPERITY_PROFIT", 0, false);
            MBTextManager.SetTextVariable("FINANCE_FOOD_PROFIT", 0, false);
            MBTextManager.SetTextVariable("FINANCE_RENT_PROFIT", 0, false);
            MBTextManager.SetTextVariable("FINANCE_BANK_PROFIT", 0, false);
            this.GameStarter.AddGameMenu(FinanceMenu.MENU_ID, "Choose how to manage finances.\nYou are currently making {FINANCE_TOWN_PROFIT} denars a day from this town's settings.\n + Worker Trading Profits: {FINANCE_PROSPERITY_PROFIT}\n + Militia Trading Profits: {FINANCE_MILITIA_PROFIT}\n + Food Export Profits: {FINANCE_FOOD_PROFIT}\n + Rent Profits: {FINANCE_RENT_PROFIT}", (OnInitDelegate)null, GameOverlays.MenuOverlayType.SettlementWithCharacters);
            this.GameStarter.AddGameMenuOption(MainMenu.MENU_ID, FinanceMenu.OPTION_ID, "Manage Finances", new GameMenuOption.OnConditionDelegate(BaseMenu.IsPlayerOwner), (GameMenuOption.OnConsequenceDelegate)(args =>
            {
                Settlement currentSettlement = Hero.MainHero.CurrentSettlement;
                if (!TownData.All.ContainsKey(((MBObjectBase)currentSettlement).StringId))
                {
                    TownData.All.Add(((MBObjectBase)currentSettlement).StringId, new TownData(((MBObjectBase)currentSettlement).StringId));
                }

                GameMenu.SwitchToMenu(FinanceMenu.MENU_ID);
                this.UpdateText();
                TownData.Find(((MBObjectBase)currentSettlement).StringId);
            }), false, 4, false);
            this.GameStarter.AddGameMenuOption(FinanceMenu.MENU_ID, "finance_prosperity", "Worker Export", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Recruit;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => InformationManager.ShowInquiry(new InquiryData(new TextObject("Worker Export").ToString(), new TextObject("Should extra workers be sent to work elsewhere, for profit? Prosperity will no longer increase.").ToString(), true, true, "Yes", "No", (Action)(() =>
            {
                TownData.Find(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId).WorkerExport = true;
                InformationManager.HideInquiry();
                this.UpdateText();
            }), (Action)(() =>
            {
                TownData.Find(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId).WorkerExport = false;
                InformationManager.HideInquiry();
                this.UpdateText();
            }), ""), false)), false, -1, false);
            this.GameStarter.AddGameMenuOption(FinanceMenu.MENU_ID, "finance_militia", "Militia Export", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Recruit;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => InformationManager.ShowInquiry(new InquiryData(new TextObject("Militia Export").ToString(), new TextObject("Should extra militia be sent to work elsewhere, for profit? Miliia will no longer increase.").ToString(), true, true, "Yes", "No", (Action)(() =>
            {
                TownData.Find(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId).MilitiaExport = true;
                InformationManager.HideInquiry();
                this.UpdateText();
            }), (Action)(() =>
            {
                TownData.Find(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId).MilitiaExport = false;
                InformationManager.HideInquiry();
                this.UpdateText();
            }), ""), false)), false, -1, false);
            this.GameStarter.AddGameMenuOption(FinanceMenu.MENU_ID, "finance_food", "Food Exports", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Trade;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => InformationManager.ShowInquiry(new InquiryData(new TextObject("Food Export").ToString(), new TextObject("Should extra food be exported, for profit? Food will no longer increase.").ToString(), true, true, "Yes", "No", (Action)(() =>
            {
                TownData.Find(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId).FoodExport = true;
                InformationManager.HideInquiry();
                this.UpdateText();
            }), (Action)(() =>
            {
                TownData.Find(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId).FoodExport = false;
                InformationManager.HideInquiry();
                this.UpdateText();
            }), ""), false)), false, -1, false);
            this.GameStarter.AddGameMenuOption(FinanceMenu.MENU_ID, "finance_rents", "Extra Rents", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Manage;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => InformationManager.ShowInquiry(new InquiryData(new TextObject("Extra Rent").ToString(), new TextObject("Should Citizens be charged extra rent? You need high security and loyalty, but those will not increase anymore.").ToString(), true, true, "Yes", "No", (Action)(() =>
            {
                TownData.Find(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId).ExtraRent = true;
                InformationManager.HideInquiry();
                this.UpdateText();
            }), (Action)(() =>
            {
                TownData.Find(((MBObjectBase)Hero.MainHero.CurrentSettlement).StringId).ExtraRent = false;
                InformationManager.HideInquiry();
                this.UpdateText();
            }), ""), false)), false, -1, false);
            this.GameStarter.AddGameMenuOption(FinanceMenu.MENU_ID, "finance_go_back", "Leave", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Leave;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => GameMenu.SwitchToMenu(MainMenu.MENU_ID)), false, -1, false);
        }

        public void UpdateText()
        {
            TownData current = TownData.Current;
            int num = (int)current.ProsperityProfit();
            MBTextManager.SetTextVariable("FINANCE_PROSPERITY_PROFIT", num.ToString() + (current.WorkerExport ? " - ON" : " - OFF"), false);
            num = (int)current.MilitiaProfit();
            MBTextManager.SetTextVariable("FINANCE_MILITIA_PROFIT", num.ToString() + (current.MilitiaExport ? " - ON" : " - OFF"), false);
            num = (int)current.FoodProfit();
            MBTextManager.SetTextVariable("FINANCE_FOOD_PROFIT", num.ToString() + (current.FoodExport ? " - ON" : " - OFF"), false);
            num = (int)current.RentProfit();
            MBTextManager.SetTextVariable("FINANCE_RENT_PROFIT", num.ToString() + (current.ExtraRent ? " - ON" : " - OFF"), false);
            MBTextManager.SetTextVariable("FINANCE_TOWN_PROFIT", (int)TownData.Current.Profit(), false);
        }
    }
}
