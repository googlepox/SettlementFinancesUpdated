using MCM.Abstractions.Base.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace SettlementFinances.Menus
{
    internal class BankMenu : BaseMenu
    {
        public static string MENU_ID = "bank_menu";
        public static readonly string OPTION_ID = "bank_menu_button";
        public static GameMenuOption trade;
        public static GameMenuOption manage;
        public static GameMenuOption smithy;

        public BankMenu(CampaignGameStarter gameStarter)
          : base(gameStarter)
        {
        }

        protected override void RegisterMenu()
        {
            MBTextManager.SetTextVariable("BANK_INTEREST", 0, false);
            MBTextManager.SetTextVariable("CURRENT_SETTLEMENT", 0, false);
            MBTextManager.SetTextVariable("ACCOUNT_PRICE", 0, false);
            MBTextManager.SetTextVariable("ACCOUNT_GOLD", 0, false);
            MBTextManager.SetTextVariable("ACCOUNT_LOAN", "", false);
            MBTextManager.SetTextVariable("ACCOUNT_LOAN_TIME", "", false);
            this.GameStarter.AddGameMenu(BankMenu.MENU_ID, "Welcome to the Bank of {CURRENT_SETTLEMENT}!\nThe interest here is {BANK_INTEREST}% a day.\n{ACCOUNT_GOLD}{ACCOUNT_LOAN}", (OnInitDelegate)null, GameOverlays.MenuOverlayType.SettlementWithCharacters);
            this.GameStarter.AddGameMenuOption(MainMenu.MENU_ID, BankMenu.OPTION_ID, "Bank", (GameMenuOption.OnConditionDelegate)(args =>
            {
                if (!BankData.HaveBank || BankData.IsMainBank())
                {
                    args.optionLeaveType = GameMenuOption.LeaveType.Bribe;
                    args.Tooltip = new TextObject("");
                    args.IsEnabled = true;
                    return true;
                }
                args.optionLeaveType = GameMenuOption.LeaveType.Bribe;
                args.Tooltip = new TextObject("Your bank contract is signed with " + BankData.CurrentBank.Settlement.Name?.ToString() + ".");
                args.IsEnabled = false;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(args =>
            {
                Settlement currentSettlement = Hero.MainHero.CurrentSettlement;
                this.UpdateText();
                GameMenu.SwitchToMenu(BankMenu.MENU_ID);
            }), false, 4, false);
            this.GameStarter.AddGameMenuOption(BankMenu.MENU_ID, "open_bank_account", "Open Account ({ACCOUNT_PRICE} denars.)", x =>
            {
                Settlement currentSettlement = Hero.MainHero.CurrentSettlement;
                x.optionLeaveType = GameMenuOption.LeaveType.Continue;
                if ((double)Hero.MainHero.Gold >= (double)BankData.NewAccountPrice(currentSettlement))
                {
                    x.IsEnabled = true;
                    x.Tooltip = new TextObject("");
                }
                else
                {
                    x.IsEnabled = false;
                    x.Tooltip = new TextObject("You do not have enough gold.");
                }
                return !BankData.HaveBank;
            }, (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                Settlement currentSettlement = Hero.MainHero.CurrentSettlement;
                Hero.MainHero.Gold -= (int)BankData.NewAccountPrice(currentSettlement);
                BankData.CurrentBank = new BankData(((MBObjectBase)currentSettlement).StringId);
                this.UpdateText();
                GameMenu.SwitchToMenu(BankMenu.MENU_ID);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(BankMenu.MENU_ID, "bank_deposit_l", "Deposit", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Bribe;
                return BankData.IsMainBank();
            }), (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                float x1 = 0.0f;
                InformationManager.ShowTextInquiry(new TextInquiryData("Deposit", string.Format("Enter the amount to deposit (1 - {0}):", (object)Hero.MainHero.Gold), true, true, "Deposit", "Cancel", (Action<string>)(amountText =>
          {
              float result = 0.0f;
              float.TryParse(amountText, out result);
              BankData.CurrentBank.Gold += result;
              Hero.MainHero.Gold -= (int)result;
              this.UpdateText();
          }), (Action)(() =>
          {
              InformationManager.HideInquiry();
              this.UpdateText();
          }), false, (amountText => ((float.TryParse(amountText, out x1) && (double)x1 <= (double)Hero.MainHero.Gold && (double)x1 > 0.0), "").ToTuple<bool, string>()), ""), false);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(BankMenu.MENU_ID, "bank_withdraw_l", "Withdraw", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Bribe;
                return BankData.IsMainBank();
            }), (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                float x1 = 0.0f;
                InformationManager.ShowTextInquiry(new TextInquiryData("Withdraw", string.Format("Enter the amount to withdraw (1 - {0}):", (object)BankData.CurrentBank.Gold), true, true, "Withdraw", "Cancel", (Action<string>)(amountText =>
          {
              float result = 0.0f;
              float.TryParse(amountText, out result);
              BankData.CurrentBank.Gold -= result;
              Hero.MainHero.Gold += (int)result;
              this.UpdateText();
          }), (Action)(() =>
          {
              InformationManager.HideInquiry();
              GameMenu.SwitchToMenu(BankMenu.MENU_ID);
              this.UpdateText();
          }), false, (amountText => ((float.TryParse(amountText, out x1) && (double)x1 <= (double)BankData.CurrentBank.Gold && (double)x1 > 0.0), "").ToTuple<bool, string>()), ""), false);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(BankMenu.MENU_ID, "bank_take_loan", "Take a Loan", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Trade;
                return BankData.HaveBank && (double)BankData.CurrentBank.Loan <= 0.0;
            }), (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                float x1 = 0.0f;
                InformationManager.ShowTextInquiry(new TextInquiryData("Loan", string.Format("Enter the loan you want (1 - {0}). You will have to pay back " + 100.ToString() + GlobalSettings<Configuration>.Instance.BankLoanInterest.ToString() + " of it in " + GlobalSettings<Configuration>.Instance.BankLoanDays.ToString() + " days:", (object)BankData.CurrentBank.MaxLoan), true, true, "Take", "Cancel", (Action<string>)(amountText =>
          {
              float result = 0.0f;
              float.TryParse(amountText, out result);
              BankData.CurrentBank.SetUpLoan(result);
              GameMenu.SwitchToMenu(BankMenu.MENU_ID);
              this.UpdateText();
          }), (Action)(() =>
          {
              InformationManager.HideInquiry();
              this.UpdateText();
          }), false, (amountText => ((float.TryParse(amountText, out x1) && (double)x1 <= (double)BankData.CurrentBank.MaxLoan), "").ToTuple<bool, string>()), ""), false);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(BankMenu.MENU_ID, "bank_pay_loan", "Pay the Loan {ACCOUNT_LOAN_TIME}", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Trade;
                if (BankData.HaveBank && (double)Hero.MainHero.Gold >= (double)BankData.CurrentBank.Loan)
                {
                    x.IsEnabled = true;
                }
                else
                {
                    x.IsEnabled = false;
                    x.Tooltip = new TextObject("You don't have enough gold.");
                }
                return BankData.HaveBank && (double)BankData.CurrentBank.Loan > 0.0;
            }), (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                BankData.CurrentBank.PayLoan();
                this.UpdateText();
                GameMenu.SwitchToMenu(BankMenu.MENU_ID);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(BankMenu.MENU_ID, "bank_invest_destination", "Choose investment receivers", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Recruit;
                return BankData.HaveBank;
            }), (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData("Investment", "Choose where your deposited gold is invested. You will get the same interest rate but the receivers will thrive.", new List<InquiryElement>()
              {
          new InquiryElement((object) null, BankData.GANG_LEADERS, (ImageIdentifier) null),
          new InquiryElement((object) null, BankData.MERCHANTS, (ImageIdentifier) null),
          new InquiryElement((object) null, BankData.ARTISANS, (ImageIdentifier) null),
          new InquiryElement((object) null, BankData.HEADMEN, (ImageIdentifier) null),
          new InquiryElement((object) null, BankData.LANDOWNERS, (ImageIdentifier) null)
              }, false, 1, 1, "Ok", "Cancel", (Action<List<InquiryElement>>)(elements =>
              {
                  if (elements.Count == 0)
                  {
                      return;
                  }

                  BankData.CurrentBank.investment = elements[0].Title;
                  InformationManager.HideInquiry();
              }), (Action<List<InquiryElement>>)(elements => InformationManager.HideInquiry())), false);
                this.UpdateText();
                GameMenu.SwitchToMenu(BankMenu.MENU_ID);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(BankMenu.MENU_ID, "Close_bank_account", "Close Account", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Continue;
                if (BankData.HaveBank && (double)BankData.CurrentBank.Loan > 0.0)
                {
                    x.IsEnabled = false;
                    x.Tooltip = new TextObject("You cannot close your accout while haveing an unpaid loan.");
                }
                else
                {
                    x.IsEnabled = true;
                    x.Tooltip = new TextObject("");
                }
                return BankData.IsMainBank();
            }), (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                Hero.MainHero.Gold += (int)BankData.CurrentBank.Gold;
                BankData.CurrentBank = (BankData)null;
                this.UpdateText();
                GameMenu.SwitchToMenu(BankMenu.MENU_ID);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(BankMenu.MENU_ID, "business_square_go_back", "Leave", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Leave;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => GameMenu.SwitchToMenu(MainMenu.MENU_ID)), false, -1, false);
        }

        public void UpdateText()
        {
            MBTextManager.SetTextVariable("BANK_INTEREST", BankData.InterestFor(Hero.MainHero.CurrentSettlement).ToString(), false);
            MBTextManager.SetTextVariable("CURRENT_SETTLEMENT", Hero.MainHero.CurrentSettlement.Name, false);
            MBTextManager.SetTextVariable("ACCOUNT_PRICE", ((int)BankData.NewAccountPrice(Hero.MainHero.CurrentSettlement)).ToString(), false);
            if (BankData.HaveBank)
            {
                MBTextManager.SetTextVariable("ACCOUNT_LOAN", (double)BankData.CurrentBank.Loan == 0.0 ? "" : "\nYou have taken a loan of " + ((int)BankData.CurrentBank.Loan).ToString() + " denars.\nYou are not getting interest payments.", false);
            }

            if (BankData.HaveBank)
            {
                MBTextManager.SetTextVariable("ACCOUNT_LOAN_TIME", (double)BankData.CurrentBank.LoanDays >= 0.0 ? "(" + BankData.CurrentBank.LoanDays.ToString() + " days remaining)" : " (Overdue!)", false);
            }

            MBTextManager.SetTextVariable("ACCOUNT_GOLD", BankData.HaveBank ? "You have " + ((int)BankData.CurrentBank.Gold).ToString() + " denars deposited here." : "", false);
        }

        public static void GameMenuOpened(MenuCallbackArgs args)
        {
            if (args.MenuContext.GameMenu.StringId != "town")
            {
                return;
            }

            try
            {
                GameMenuOption gameMenuOption1 = args.MenuContext.GameMenu.MenuOptions.First<GameMenuOption>((Func<GameMenuOption, bool>)(x => x.IdString == "trade"));
                BankMenu.trade = gameMenuOption1;
                gameMenuOption1.OnCondition = (GameMenuOption.OnConditionDelegate)(arg2s => false);
                GameMenuOption gameMenuOption2 = args.MenuContext.GameMenu.MenuOptions.First<GameMenuOption>((Func<GameMenuOption, bool>)(x => x.IdString == "manage_town"));
                BankMenu.manage = gameMenuOption2;
                gameMenuOption2.OnCondition = (GameMenuOption.OnConditionDelegate)(arg2s => false);
                GameMenuOption gameMenuOption3 = args.MenuContext.GameMenu.MenuOptions.First<GameMenuOption>((Func<GameMenuOption, bool>)(x => x.IdString == "smithy"));
                BankMenu.smithy = gameMenuOption3;
                gameMenuOption3.OnCondition = (GameMenuOption.OnConditionDelegate)(arg2s => false);
            }
            catch
            {
            }
        }
    }
}
