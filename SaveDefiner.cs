using System.Collections.Generic;
using TaleWorlds.SaveSystem;

namespace SettlementFinances
{
    public class SaveDefiner : SaveableTypeDefiner
    {
        public SaveDefiner()
          : base(991335077)
        {
        }

        protected override void DefineClassTypes()
        {
            this.AddClassDefinition(typeof(TownData), 1);
            this.AddClassDefinition(typeof(BankData), 2);
            this.AddClassDefinition(typeof(VillageData), 3);
        }

        protected override void DefineGenericClassDefinitions()
        {
        }

        protected override void DefineContainerDefinitions()
        {
            this.ConstructContainerDefinition(typeof(Dictionary<string, TownData>));
            this.ConstructContainerDefinition(typeof(Dictionary<string, VillageData>));
        }
    }
}
