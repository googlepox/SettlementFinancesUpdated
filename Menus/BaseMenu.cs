// Decompiled with JetBrains decompiler
// Type: SettlementFinances.Menus.BaseMenu
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

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
