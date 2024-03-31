using MCM.Abstractions.Base.Global;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace SettlementFinances
{
    internal class CaravanReplenisher
    {
        [SaveableField(0)]
        public static List<string> All = new List<string>();

        public static void DailyTick()
        {
            Hero leader = null;
            foreach (string str in All)
            {
                string entry = str;
                Hero first = Hero.FindFirst(h => h.StringId == entry);
                if (first != null && !first.IsPrisoner)
                {
                    leader = first;
                    break;
                }
            }
            if (leader == null)
            {
                return;
            }

            float caravanReplenishChance = GlobalSettings<Configuration>.Instance.CaravanReplenishChance;
            if (MBRandom.RandomFloat < caravanReplenishChance)
            {
                ReplenishCaravanWithLeader(leader);
            }
        }

        public static void MobilePartyDestroyed(MobileParty party, PartyBase pbase)
        {
            if (!GlobalSettings<Configuration>.Instance.CaravanReplenishEnable || !party.IsCaravan || party.Party.Name.Length <= 11)
            {
                return;
            }

            Hero first = Hero.FindFirst(h => party.Party.Name.ToString().Contains(h.Name.ToString()));
            if (first?.Clan == Hero.MainHero.Clan && pbase.PrisonRoster.Contains(first?.CharacterObject))
            {
                Put(first);
            }
        }

        public static void ReplenishCaravanWithLeader(Hero leader)
        {
            if (!GlobalSettings<Configuration>.Instance.CaravanReplenishEnable || leader.CurrentSettlement == null)
            {
                return;
            }
            MobileParty mobileParty = CaravanPartyComponent.CreateCaravanParty(leader, leader.CurrentSettlement, true, null, null, 10);
            mobileParty.PartyTradeGold = GlobalSettings<Configuration>.Instance.CaravanStartingGold;
            mobileParty.ItemRoster.Add(new ItemRosterElement(DefaultItems.Meat, 5));
            mobileParty.Initialize();
            Pop(leader);
            InformationManager.DisplayMessage(new InformationMessage(leader.Name?.ToString() + " started a new caravan at " + leader.CurrentSettlement.Name?.ToString() + "."));
        }

        public static void Put(Hero hero)
        {
            All.Add(hero.StringId);
        }

        public static Hero Pop(string id)
        {
            Hero first = Hero.FindFirst(h2 => h2.StringId == id);
            All.Remove(id);
            return first;
        }

        public static Hero Pop(Hero hero)
        {
            return Pop(hero.StringId);
        }
    }
}
