// Decompiled with JetBrains decompiler
// Type: SettlementFinances.FinanceBehavior
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

using SettlementFinances.Menus;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace SettlementFinances
{
    internal class FinanceBehavior : CampaignBehaviorBase
    {
        private MainMenu _menu;
        private LandMenu _land_menu;

        public MainMenu Menu => this._menu;

        public override void RegisterEvents()
        {
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.LoadSavedData));
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.AddFinancesMenu));
            CampaignEvents.AfterSettlementEntered.AddNonSerializedListener((object)this, new Action<MobileParty, Settlement, Hero>(this.OnPartyEnter));
            CampaignEvents.GameMenuOpened.AddNonSerializedListener((object)this, new Action<MenuCallbackArgs>(this.GameMenuOpened));
            CampaignEvents.DailyTickEvent.AddNonSerializedListener((object)this, new Action(this.DailyTick));
            CampaignEvents.MobilePartyDestroyed.AddNonSerializedListener((object)this, new Action<MobileParty, PartyBase>(this.MobilePartyDestroyed));
            CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.OneNewGameCreatedEvent));
        }

        public void OneNewGameCreatedEvent(CampaignGameStarter starter)
        {
            VillageData.All = new Dictionary<string, VillageData>();
            BankData.CurrentBank = (BankData)null;
            TownData.All = new Dictionary<string, TownData>();
            CaravanReplenisher.All = new List<string>();
        }

        public void OnGameOverEvent()
        {
            VillageData.All = new Dictionary<string, VillageData>();
            BankData.CurrentBank = (BankData)null;
            TownData.All = new Dictionary<string, TownData>();
            CaravanReplenisher.All = new List<string>();
        }

        public void MobilePartyDestroyed(MobileParty party, PartyBase pbase)
        {
            CaravanReplenisher.MobilePartyDestroyed(party, pbase);
        }

        public void DailyTick()
        {
            BankData.DailyTick();
            SettlementSupport.DailyTick();
            CaravanReplenisher.DailyTick();
            VillageData.TotalDailyTick();
            TownData.TotalDailyTick();
        }

        public void GameMenuOpened(MenuCallbackArgs args)
        {
            BankMenu.GameMenuOpened(args);
            CaravanWorkshopMenu.GameMenuOpened(args);
        }

        private void OnPartyEnter(MobileParty party, Settlement settlement, Hero hero)
        {
        }

        private void AddFinancesMenu(CampaignGameStarter obj)
        {
            this._menu = new MainMenu(obj);
            this._land_menu = new LandMenu(obj);
        }

        private void LoadSavedData(CampaignGameStarter obj)
        {
            using (Dictionary<string, TownData>.Enumerator enumerator = TownData.All.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return;
                }

                TownData.Last = enumerator.Current.Value;
            }
        }

        public override void SyncData(IDataStore dataStore)
        {
            try
            {
                if (dataStore.SyncData<Dictionary<string, TownData>>("financeMapGlobal", ref TownData.All))
                {
                    ;
                }

                if (dataStore.SyncData<BankData>("financeBankGlobal", ref BankData.CurrentBank))
                {
                    ;
                }

                if (dataStore.SyncData<Dictionary<string, VillageData>>("financeVillageGlobal", ref VillageData.All))
                {
                    ;
                }

                if (dataStore.SyncData<List<string>>("financeCaravanSaver", ref CaravanReplenisher.All))
                {
                    ;
                }
            }
            catch
            {
                InformationManager.DisplayMessage(new InformationMessage("ERROR"));
            }
        }
    }
}
