using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Settlements;

namespace SettlementFinances.Menus
{
    internal abstract class BaseMenu
    {
        protected readonly CampaignGameStarter GameStarter;

        public BaseMenu(CampaignGameStarter gameStarter)
        {
            this.GameStarter = gameStarter;
            this.RegisterMenu();
        }

        protected abstract void RegisterMenu();

        public static bool IsPlayerOwner(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
            Settlement currentSettlement = Settlement.CurrentSettlement;
            return currentSettlement.OwnerClan == Clan.PlayerClan && currentSettlement.MapFaction == Hero.MainHero.MapFaction && currentSettlement.IsFortification;
        }
    }
}
