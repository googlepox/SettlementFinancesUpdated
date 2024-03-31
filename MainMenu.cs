// Decompiled with JetBrains decompiler
// Type: SettlementFinances.MainMenu
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

using MCM.Abstractions.Base.Global;
using SettlementFinances.Menus;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace SettlementFinances
{
    internal class MainMenu : BaseMenu
    {
        public static string MENU_ID = "business_square";
        public static readonly string OPTION_ID = "business_square_button";
        public BankMenu bankMenu;
        public FinanceMenu financeMenu;
        public CaravanWorkshopMenu cwMenu;

        public MainMenu(CampaignGameStarter gameStarter)
          : base(gameStarter)
        {
        }

        protected override void RegisterMenu()
        {
            this.GameStarter.AddGameMenu(MainMenu.MENU_ID, "Welcome to the Business Square!", (OnInitDelegate)null, GameOverlays.MenuOverlayType.SettlementWithCharacters);
            this.GameStarter.AddGameMenuOption("town", MainMenu.OPTION_ID, "Go to the business square", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Bribe;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(args => GameMenu.SwitchToMenu(MainMenu.MENU_ID)), false, 4, false);
            this.bankMenu = new BankMenu(this.GameStarter);
            this.financeMenu = new FinanceMenu(this.GameStarter);
            this.cwMenu = new CaravanWorkshopMenu(this.GameStarter);
            this.GameStarter.AddGameMenuOption(MainMenu.MENU_ID, "trade__", "Market", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Trade;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => BankMenu.trade.OnConsequence(x)), false, -1, false);
            this.GameStarter.AddGameMenuOption(MainMenu.MENU_ID, "financial_support", "Financial Support", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Bribe;
                return Hero.MainHero.CurrentSettlement.OwnerClan == Hero.MainHero.Clan;
            }), (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                float x1 = 0.0f;
                InformationManager.ShowTextInquiry(new TextInquiryData("Financial Support", string.Format("Enter the amount to donate to your town (1 - {0}):", (object)Hero.MainHero.Gold), true, true, "Donate", "Cancel", (Action<string>)(amountText =>
          {
              float result = 0.0f;
              float.TryParse(amountText, out result);
              ((SettlementComponent)Hero.MainHero.CurrentSettlement.Town).ChangeGold((int)((double)result * (1.0 - (double)GlobalSettings<Configuration>.Instance.PlayerSupportTax)));
              Hero.MainHero.Gold -= (int)result;
          }), (Action)(() => InformationManager.HideInquiry()), false, (amountText => ((float.TryParse(amountText, out x1) && (double)x1 <= (double)Hero.MainHero.Gold && (double)x1 > 0.0), "").ToTuple<bool, string>())), false);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(MainMenu.MENU_ID, "business_square_back", "Leave", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Leave;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => GameMenu.SwitchToMenu(Hero.MainHero.CurrentSettlement.IsTown ? "town" : "castle")), false, -1, false);
        }
    }
}
