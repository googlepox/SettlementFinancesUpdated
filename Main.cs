// Decompiled with JetBrains decompiler
// Type: SettlementFinances.Main
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

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
