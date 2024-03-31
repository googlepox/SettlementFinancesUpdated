// Decompiled with JetBrains decompiler
// Type: SettlementFinances.Menus.CaravanWorkshopMenu
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SettlementFinances.Menus
{
    internal class CaravanWorkshopMenu : BaseMenu
    {
        public static string MENU_ID = "caravan_menu";
        public static readonly string OPTION_ID = "caravan_menu_button";

        public CaravanWorkshopMenu(CampaignGameStarter gameStarter)
          : base(gameStarter)
        {
        }

        protected override void RegisterMenu()
        {
            this.GameStarter.AddGameMenu(CaravanWorkshopMenu.MENU_ID, "You arrive at the quarter where notables can sell you caravans or workshops.", (OnInitDelegate)null, GameOverlays.MenuOverlayType.SettlementWithCharacters);
            this.GameStarter.AddGameMenuOption(MainMenu.MENU_ID, CaravanWorkshopMenu.OPTION_ID, "Notable Quarters", (GameMenuOption.OnConditionDelegate)(args =>
            {
                args.optionLeaveType = GameMenuOption.LeaveType.Recruit;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(args =>
            {
                Settlement currentSettlement = Hero.MainHero.CurrentSettlement;
                GameMenu.SwitchToMenu(CaravanWorkshopMenu.MENU_ID);
            }), false, 4, false);
            this.GameStarter.AddGameMenuOption(CaravanWorkshopMenu.MENU_ID, "caravan_buy_1", "Buy a basic caravan (15000{GOLD_ICON})", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Continue;
                if (Hero.MainHero.Gold < 15000)
                {
                    x.Tooltip = new TextObject("You don't have enough gold to buy a basic caravan.");
                    x.IsEnabled = false;
                }
                if (!Clan.PlayerClan.Companions.Any<Hero>((Func<Hero, bool>)(c => c.PartyBelongedTo == MobileParty.MainParty)))
                {
                    x.Tooltip = new TextObject("You have no companions in your party to lead the caravan.");
                    x.IsEnabled = false;
                }
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                CaravanWorkshopMenu.StartCaravan(15000, 10000, 29, false);
                GameMenu.SwitchToMenu(CaravanWorkshopMenu.MENU_ID);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(CaravanWorkshopMenu.MENU_ID, "caravan_buy_2", "Buy an elite caravan (20000{GOLD_ICON})", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Continue;
                if (Hero.MainHero.Gold < 20000)
                {
                    x.Tooltip = new TextObject("You don't have enough gold to buy an elite caravan.");
                    x.IsEnabled = false;
                }
                if (!Clan.PlayerClan.Companions.Any<Hero>((Func<Hero, bool>)(c => c.PartyBelongedTo == MobileParty.MainParty)))
                {
                    x.Tooltip = new TextObject("You have no companions in your party to lead the caravan.");
                    x.IsEnabled = false;
                }
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x =>
            {
                CaravanWorkshopMenu.StartCaravan(20000, 10000, 9, true);
                GameMenu.SwitchToMenu(CaravanWorkshopMenu.MENU_ID);
            }), false, -1, false);
            this.GameStarter.AddGameMenuOption(CaravanWorkshopMenu.MENU_ID, "workshop_buy1", "{WORKSHOP_BUY1}", (GameMenuOption.OnConditionDelegate)(x => false), (GameMenuOption.OnConsequenceDelegate)(x => { }), false, -1, false);
            this.GameStarter.AddGameMenuOption(CaravanWorkshopMenu.MENU_ID, "workshop_buy2", "{WORKSHOP_BUY2}", (GameMenuOption.OnConditionDelegate)(x => false), (GameMenuOption.OnConsequenceDelegate)(x => { }), false, -1, false);
            this.GameStarter.AddGameMenuOption(CaravanWorkshopMenu.MENU_ID, "workshop_buy3", "{WORKSHOP_BUY3}", (GameMenuOption.OnConditionDelegate)(x => false), (GameMenuOption.OnConsequenceDelegate)(x => { }), false, -1, false);
            this.GameStarter.AddGameMenuOption(CaravanWorkshopMenu.MENU_ID, "workshop_buy4", "{WORKSHOP_BUY4}", (GameMenuOption.OnConditionDelegate)(x => false), (GameMenuOption.OnConsequenceDelegate)(x => { }), false, -1, false);
            this.GameStarter.AddGameMenuOption(CaravanWorkshopMenu.MENU_ID, "caravan_go_back", "Leave", (GameMenuOption.OnConditionDelegate)(x =>
            {
                x.optionLeaveType = GameMenuOption.LeaveType.Leave;
                return true;
            }), (GameMenuOption.OnConsequenceDelegate)(x => GameMenu.SwitchToMenu(MainMenu.MENU_ID)), false, -1, false);
        }

        public static void GameMenuOpened(MenuCallbackArgs args)
        {
            if (args.MenuContext.GameMenu.StringId != CaravanWorkshopMenu.MENU_ID || !Hero.MainHero.CurrentSettlement.IsTown)
            {
                return;
            }

            Town town = Hero.MainHero.CurrentSettlement.Town;
            int i = 0;
            foreach (Workshop workshop1 in town.Workshops)
            {
                Workshop workshop = workshop1;
                i++;
                if (i != 1)
                {
                    if (i > 4)
                    {
                        break;
                    }

                    int cost = Campaign.Current.Models.WorkshopModel.GetCostForPlayer(workshop);
                    GameMenuOption gameMenuOption = args.MenuContext.GameMenu.MenuOptions.First<GameMenuOption>((Func<GameMenuOption, bool>)(o => o.IdString == "workshop_buy" + i.ToString()));
                    TextObject textObject = new TextObject("Buy the " + ((SettlementArea)workshop).Name?.ToString() + " (" + cost.ToString() + "{GOLD_ICON})");
                    MBTextManager.SetTextVariable("WORKSHOP_BUY" + i.ToString(), textObject.ToString(), false);
                    gameMenuOption.OnCondition = (GameMenuOption.OnConditionDelegate)(x =>
                    {
                        x.optionLeaveType = GameMenuOption.LeaveType.Continue;
                        if (Hero.MainHero.Gold < cost)
                        {
                            x.Tooltip = new TextObject("You don't have enough gold.");
                            x.IsEnabled = false;
                        }
                        if (Campaign.Current.Models.WorkshopModel.GetMaxWorkshopCountForClanTier(Hero.MainHero.Clan.Tier) <= Hero.MainHero.OwnedWorkshops.Count)
                        {
                            x.Tooltip = new TextObject("You own too many workshops.");
                            x.IsEnabled = false;
                        }
                        if (((SettlementArea)workshop).Owner == Hero.MainHero)
                        {
                            x.Tooltip = new TextObject("You already own this workshop.");
                            x.IsEnabled = false;
                        }
                        return true;
                    });
                    gameMenuOption.OnConsequence = (GameMenuOption.OnConsequenceDelegate)(x =>
                    {
                        InformationManager.ShowInquiry(new InquiryData(new TextObject("Buy workshop").ToString(), new TextObject("Are you sure you want to buy the " + ((SettlementArea)workshop).Name?.ToString() + " for " + cost.ToString() + " denars?").ToString(), true, true, "Yes", "No", (Action)(() =>
              {
                  ChangeOwnerOfWorkshopAction.ApplyByPlayerBuying(workshop);
                  InformationManager.DisplayMessage(new InformationMessage("The workshop is now yours."));
                  GameMenu.SwitchToMenu(CaravanWorkshopMenu.MENU_ID);
              }), (Action)(() => GameMenu.SwitchToMenu(CaravanWorkshopMenu.MENU_ID)), ""), false);
                        GameMenu.SwitchToMenu(CaravanWorkshopMenu.MENU_ID);
                    });
                }
            }
        }

        public static void StartCaravan(int pay, int budget, int troops, bool isElite)
        {
            List<InquiryElement> inquiryElements = new List<InquiryElement>();
            foreach (Hero companion in (List<Hero>)Clan.PlayerClan.Companions)
            {
                if (companion != Hero.MainHero && companion.PartyBelongedTo == Hero.MainHero.PartyBelongedTo)
                {
                    ImageIdentifier imageIdentifier = new ImageIdentifier(CharacterCode.CreateFrom((BasicCharacterObject)companion.CharacterObject));
                    InquiryElement inquiryElement = new InquiryElement((object)companion, companion.Name.ToString(), imageIdentifier);
                    inquiryElements.Add(inquiryElement);
                }
            }
            MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData("Choose a leader", "Choose which companion you want to send to lead the Caravan", inquiryElements, false, 1, 1, "Ok", "Cancel", (Action<List<InquiryElement>>)(elements =>
            {
                if (elements.Count == 0)
                {
                    return;
                }

                Hero identifier = (Hero)elements[0].Identifier;
                CharacterObject characterObject1 = identifier.CharacterObject;
                LeaveSettlementAction.ApplyForCharacterOnly(characterObject1.HeroObject);
                MobileParty.MainParty.MemberRoster.AddToCounts(characterObject1, -1, false, 0, 0, true, -1);
                MobileParty mobileParty = CaravanPartyComponent.CreateCaravanParty(Hero.MainHero, Hero.MainHero.CurrentSettlement, false, identifier, (ItemRoster)null, troops);
                mobileParty.PartyTradeGold = budget;
                if (isElite)
                {
                    CharacterObject characterObject2 = (CharacterObject)null;
                    foreach (TroopRosterElement troopRosterElement in mobileParty.MemberRoster.GetTroopRoster())
                    {
                        if (characterObject2 == null)
                        {
                            characterObject2 = troopRosterElement.Character;
                        }
                        else if (troopRosterElement.Character.IsMounted && troopRosterElement.Character.Level > characterObject2.Level)
                        {
                            characterObject2 = troopRosterElement.Character;
                        }
                    }
                    mobileParty.MemberRoster.AddToCounts(characterObject2, 20, false, 0, 0, true, -1);
                }
                GiveGoldAction.ApplyForCharacterToSettlement(Hero.MainHero, Settlement.CurrentSettlement, pay, false);
                InformationManager.DisplayMessage(new InformationMessage("A new caravan was dispatched at " + Hero.MainHero.CurrentSettlement.Name.ToString() + " lead by " + ((Hero)elements[0].Identifier).Name.ToString() + "."));
                GameMenu.SwitchToMenu(CaravanWorkshopMenu.MENU_ID);
                InformationManager.HideInquiry();
            }), (Action<List<InquiryElement>>)(elements =>
            {
                GameMenu.SwitchToMenu(CaravanWorkshopMenu.MENU_ID);
                InformationManager.HideInquiry();
            })), false);
        }
    }
}
