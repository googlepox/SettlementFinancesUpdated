// Decompiled with JetBrains decompiler
// Type: SettlementFinances.SaveDefiner
// Assembly: SettlementFinances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F32235B7-729B-4F1B-B741-D870E5EF414A
// Assembly location: C:\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SettlementFinances\bin\Win64_Shipping_Client\SettlementFinances.dll

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
      this.AddClassDefinition(typeof (TownData), 1);
      this.AddClassDefinition(typeof (BankData), 2);
      this.AddClassDefinition(typeof (VillageData), 3);
    }

    protected override void DefineGenericClassDefinitions()
    {
    }

    protected override void DefineContainerDefinitions()
    {
      this.ConstructContainerDefinition(typeof (Dictionary<string, TownData>));
      this.ConstructContainerDefinition(typeof (Dictionary<string, VillageData>));
    }
  }
}
