using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SettlementFinances
{
    public class Main : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            Harmony.DEBUG = false;
            new Harmony("SettlementFinances").PatchAll();
            base.OnSubModuleLoad();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (!(game.GameType is Campaign))
            {
                return;
            } ((CampaignGameStarter)gameStarterObject).AddBehavior(new FinanceBehavior());
        }

        public override void OnGameEnd(Game game)
        {
        }
    }
}
