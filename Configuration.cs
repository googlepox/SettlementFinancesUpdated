using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace SettlementFinances
{
    // Token: 0x02000004 RID: 4
    internal class Configuration : AttributeGlobalSettings<Configuration>
    {
        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000016 RID: 22 RVA: 0x00002966 File Offset: 0x00000B66
        public override string Id => "Settlement.Finances";

        public override string FormatType => "json2";

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x06000017 RID: 23 RVA: 0x0000296D File Offset: 0x00000B6D
        public override string DisplayName => "Settlement Finances";

        // Token: 0x17000008 RID: 8
        // (get) Token: 0x06000018 RID: 24 RVA: 0x00002974 File Offset: 0x00000B74
        public override string FolderName => "SettlementFinances";

        // Token: 0x17000009 RID: 9
        // (get) Token: 0x06000019 RID: 25 RVA: 0x0000297B File Offset: 0x00000B7B
        // (set) Token: 0x0600001A RID: 26 RVA: 0x00002983 File Offset: 0x00000B83
        [SettingPropertyFloatingInteger("Village Investment Income Multiplier", 0.1f, 5f, "0.00", Order = 1, RequireRestart = false, HintText = "Multiplier for the income received from village investments (They go from ~90 on food plantations on a bad roll to ~360 on silver galleries on a good roll)")]
        [SettingPropertyGroup("Village Settings")]
        public float VillageIncomeMultiplier { get; set; } = 1f;

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x0600001B RID: 27 RVA: 0x0000298C File Offset: 0x00000B8C
        // (set) Token: 0x0600001C RID: 28 RVA: 0x00002994 File Offset: 0x00000B94
        [SettingPropertyFloatingInteger("Investment Price Multiplier", 0.1f, 5f, "0.00", Order = 2, RequireRestart = false, HintText = "Multiplier for the price of village investments (They go from 2850 base on a good roll for food plantations to 15600 on a bad roll for silver galleries)")]
        [SettingPropertyGroup("Village Settings")]
        public float VillagePriceMultiplier { get; set; } = 1f;

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x0600001D RID: 29 RVA: 0x0000299D File Offset: 0x00000B9D
        // (set) Token: 0x0600001E RID: 30 RVA: 0x000029A5 File Offset: 0x00000BA5
        [SettingPropertyFloatingInteger("Investment Selling Multiplier", 0.1f, 5f, "0.00", Order = 3, RequireRestart = false, HintText = "Multiplier when selling village investments (fraction of the total buying price)")]
        [SettingPropertyGroup("Village Settings")]
        public float VillageSellMultiplier { get; set; } = 0.6f;

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x0600001F RID: 31 RVA: 0x000029AE File Offset: 0x00000BAE
        // (set) Token: 0x06000020 RID: 32 RVA: 0x000029B6 File Offset: 0x00000BB6
        [SettingPropertyInteger("Maximum Base Investments", 0, 5, "0.00", Order = 4, RequireRestart = false, HintText = "Maximum base number of investments of the player (CAUTION: After 14 investments, they will go off screen in the clan finance window.)")]
        [SettingPropertyGroup("Village Settings")]
        public int VillageMax { get; set; } = 2;

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x06000021 RID: 33 RVA: 0x000029BF File Offset: 0x00000BBF
        // (set) Token: 0x06000022 RID: 34 RVA: 0x000029C7 File Offset: 0x00000BC7
        [SettingPropertyFloatingInteger("Maximum Investments Per Clan Tier", 0f, 4f, "0.00", Order = 5, RequireRestart = false, HintText = "Maximum number of investments per clan tier")]
        [SettingPropertyGroup("Village Settings")]
        public float VillageMaxPerTier { get; set; } = 2f;

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x06000023 RID: 35 RVA: 0x000029D0 File Offset: 0x00000BD0
        // (set) Token: 0x06000024 RID: 36 RVA: 0x000029D8 File Offset: 0x00000BD8
        [SettingPropertyFloatingInteger("Percentage Increases per Hearth", 1E-05f, 0.05f, "0.00%", Order = 6, RequireRestart = false, HintText = "Percentage to add to prices and profits per village hearth")]
        [SettingPropertyGroup("Village Settings")]
        public float VillagePercentagePerHearth { get; set; } = 0.0005f;

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x06000025 RID: 37 RVA: 0x000029E1 File Offset: 0x00000BE1
        // (set) Token: 0x06000026 RID: 38 RVA: 0x000029E9 File Offset: 0x00000BE9
        [SettingPropertyInteger("Generous Investors Hearth Cap", 100, 10000, "0", Order = 7, RequireRestart = false, HintText = "The maximum amount of hearth a village to be able to get a hearth increase from your investments")]
        [SettingPropertyGroup("Village Settings")]
        public int VillageHearthCap { get; set; } = 1000;

        // Token: 0x17000010 RID: 16
        // (get) Token: 0x06000027 RID: 39 RVA: 0x000029F2 File Offset: 0x00000BF2
        // (set) Token: 0x06000028 RID: 40 RVA: 0x000029FA File Offset: 0x00000BFA
        [SettingPropertyFloatingInteger("Generous Investors Hearth Gain", 0.1f, 50f, "0.00", Order = 8, RequireRestart = false, HintText = "How much hearth a village gains if it is maximally invested in. ( Partial investments yield partial results )")]
        [SettingPropertyGroup("Village Settings")]
        public float VillageHearthGain { get; set; } = 3f;

        // Token: 0x17000011 RID: 17
        // (get) Token: 0x06000029 RID: 41 RVA: 0x00002A03 File Offset: 0x00000C03
        // (set) Token: 0x0600002A RID: 42 RVA: 0x00002A0B File Offset: 0x00000C0B
        [SettingPropertyInteger("Base Account Price", 0, 50000, "0", Order = 1, RequireRestart = false, HintText = "The base price for opening a new account.")]
        [SettingPropertyGroup("Bank Settings")]
        public int BankAccountPriceBase { get; set; } = 1000;

        // Token: 0x17000012 RID: 18
        // (get) Token: 0x0600002B RID: 43 RVA: 0x00002A14 File Offset: 0x00000C14
        // (set) Token: 0x0600002C RID: 44 RVA: 0x00002A1C File Offset: 0x00000C1C
        [SettingPropertyInteger("Account Opening Price per Renown", 0, 500, "0", Order = 2, RequireRestart = false, HintText = "The price per clan renown for opening a new account.")]
        [SettingPropertyGroup("Bank Settings")]
        public int BankAccountPricePerRenown { get; set; } = 10;

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x0600002D RID: 45 RVA: 0x00002A25 File Offset: 0x00000C25
        // (set) Token: 0x0600002E RID: 46 RVA: 0x00002A2D File Offset: 0x00000C2D
        [SettingPropertyFloatingInteger("Town Interest Multiplier", 0.01f, 10f, "0.00", Order = 3, RequireRestart = false, HintText = "The interest multiplier for a town bank for each prosperity point of the town. ( At x1, the interest rate is: Prosperity/10,000 )")]
        [SettingPropertyGroup("Bank Settings")]
        public float BankInterestRate { get; set; } = 1f;

        // Token: 0x17000014 RID: 20
        // (get) Token: 0x0600002F RID: 47 RVA: 0x00002A36 File Offset: 0x00000C36
        // (set) Token: 0x06000030 RID: 48 RVA: 0x00002A3E File Offset: 0x00000C3E
        [SettingPropertyInteger("Hard Cap for Loans", 0, 5000000, "0", Order = 4, RequireRestart = false, HintText = "The absolute maximum value of a loan.")]
        [SettingPropertyGroup("Loan Settings")]
        public int BankLoanCap { get; set; } = 1000000;

        // Token: 0x17000015 RID: 21
        // (get) Token: 0x06000031 RID: 49 RVA: 0x00002A47 File Offset: 0x00000C47
        // (set) Token: 0x06000032 RID: 50 RVA: 0x00002A4F File Offset: 0x00000C4F
        [SettingPropertyInteger("Maximum Loan per Renown", 0, 10000, "0", Order = 5, RequireRestart = false, HintText = "The max loan you can take per renown point. ( Cannot go above the hard cap )")]
        [SettingPropertyGroup("Loan Settings")]
        public int BankMaxLoanPerRenown { get; set; } = 250;

        // Token: 0x17000016 RID: 22
        // (get) Token: 0x06000033 RID: 51 RVA: 0x00002A58 File Offset: 0x00000C58
        // (set) Token: 0x06000034 RID: 52 RVA: 0x00002A60 File Offset: 0x00000C60
        [SettingPropertyInteger("Loan Paying Days", 1, 300, "0", Order = 6, RequireRestart = false, HintText = "The amount of days you have to pay a loan back")]
        [SettingPropertyGroup("Loan Settings")]
        public int BankLoanDays { get; set; } = 21;

        // Token: 0x17000017 RID: 23
        // (get) Token: 0x06000035 RID: 53 RVA: 0x00002A69 File Offset: 0x00000C69
        // (set) Token: 0x06000036 RID: 54 RVA: 0x00002A71 File Offset: 0x00000C71
        [SettingPropertyFloatingInteger("Loan Interest Rate", 0f, 1f, "0%", Order = 7, RequireRestart = false, HintText = "The extra loan percentage you have to pay when paying a loan back. This is applied another time if you fail to pay on time.")]
        [SettingPropertyGroup("Loan Settings")]
        public float BankLoanInterest { get; set; } = 0.2f;

        // Token: 0x17000018 RID: 24
        // (get) Token: 0x06000037 RID: 55 RVA: 0x00002A7A File Offset: 0x00000C7A
        // (set) Token: 0x06000038 RID: 56 RVA: 0x00002A82 File Offset: 0x00000C82
        [SettingPropertyInteger("Notable Influence Cap", 0, 1000, "0", Order = 8, RequireRestart = false, HintText = "The maximum amount of influence a notable can get from investments from your bank account.")]
        [SettingPropertyGroup("Bank Investment Settings")]
        public int NotableInfluenceCap { get; set; } = 400;

        // Token: 0x17000019 RID: 25
        // (get) Token: 0x06000039 RID: 57 RVA: 0x00002A8B File Offset: 0x00000C8B
        // (set) Token: 0x0600003A RID: 58 RVA: 0x00002A93 File Offset: 0x00000C93
        [SettingPropertyInteger("Notable Influence Probability", 1, 5000000, "0", Order = 9, RequireRestart = false, HintText = "Every day, there is a *GOLD_IN_ACCOUNT* / *THIS_NUMBER* chance that the notables you chose to invest in gain an amount of influence. ( Eg. if you have gold in your account equal to this number, they will gain influence daily)")]
        [SettingPropertyGroup("Bank Investment Settings")]
        public int NotableInfluenceProbability { get; set; } = 1000000;

        // Token: 0x1700001A RID: 26
        // (get) Token: 0x0600003B RID: 59 RVA: 0x00002A9C File Offset: 0x00000C9C
        // (set) Token: 0x0600003C RID: 60 RVA: 0x00002AA4 File Offset: 0x00000CA4
        [SettingPropertyInteger("Notable Influence Amount", 0, 20, "0", Order = 10, RequireRestart = false, HintText = "Every day, if the roll is succesful, by how much should notables' influence be increased.")]
        [SettingPropertyGroup("Bank Investment Settings")]
        public int NotableInfluenceAmount { get; set; } = 3;

        // Token: 0x1700001B RID: 27
        // (get) Token: 0x0600003D RID: 61 RVA: 0x00002AAD File Offset: 0x00000CAD
        // (set) Token: 0x0600003E RID: 62 RVA: 0x00002AB5 File Offset: 0x00000CB5
        [SettingPropertyBool("Enable Caravan Replenishment?", Order = 1, RequireRestart = false, HintText = "Should caravan leaders start new caravans when eliberated from captivity after a lost battle?")]
        [SettingPropertyGroup("Caravan Settings")]
        public bool CaravanReplenishEnable { get; set; } = true;

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x0600003F RID: 63 RVA: 0x00002ABE File Offset: 0x00000CBE
        // (set) Token: 0x06000040 RID: 64 RVA: 0x00002AC6 File Offset: 0x00000CC6
        [SettingPropertyFloatingInteger("Caravan Replenish Chance", 0f, 1f, "0%", Order = 2, RequireRestart = false, HintText = "After eliberated, every day there is a chance that caravan leaders will start a caravan.")]
        [SettingPropertyGroup("Caravan Settings")]
        public float CaravanReplenishChance { get; set; } = 0.5f;

        // Token: 0x1700001D RID: 29
        // (get) Token: 0x06000041 RID: 65 RVA: 0x00002ACF File Offset: 0x00000CCF
        // (set) Token: 0x06000042 RID: 66 RVA: 0x00002AD7 File Offset: 0x00000CD7
        [SettingPropertyInteger("Caravan Starting Gold", 0, 15000, "0", Order = 3, RequireRestart = false, HintText = "What amount of gold do rebuilt caravans start with? ( Note that they will take the extra gold needed from the owner )")]
        [SettingPropertyGroup("Caravan Settings")]
        public int CaravanStartingGold { get; set; } = 500;

        // Token: 0x1700001E RID: 30
        // (get) Token: 0x06000043 RID: 67 RVA: 0x00002AE0 File Offset: 0x00000CE0
        // (set) Token: 0x06000044 RID: 68 RVA: 0x00002AE8 File Offset: 0x00000CE8
        [SettingPropertyFloatingInteger("Caravan Tariff per Leader's Trade Skill", 0f, 50f, "0.00", Order = 4, RequireRestart = false, HintText = "Caravan leaders will pay a tariff equal to this * the leader's trade skill and some based on their steward skill.")]
        [SettingPropertyGroup("Caravan Settings")]
        public float CaravanTariffPerTrade { get; set; } = 2f;

        // Token: 0x1700001F RID: 31
        // (get) Token: 0x06000045 RID: 69 RVA: 0x00002AF1 File Offset: 0x00000CF1
        // (set) Token: 0x06000046 RID: 70 RVA: 0x00002AF9 File Offset: 0x00000CF9
        [SettingPropertyFloatingInteger("Caravan Tariff per Leader's Steward Skill", 0f, 50f, "0.00", Order = 4, RequireRestart = false, HintText = "Caravan leaders will pay a tariff equal to this * the leader's steward skill and some based on their trade skill.")]
        [SettingPropertyGroup("Caravan Settings")]
        public float CaravanTariffPerSteward { get; set; } = 2f;

        // Token: 0x17000020 RID: 32
        // (get) Token: 0x06000047 RID: 71 RVA: 0x00002B02 File Offset: 0x00000D02
        // (set) Token: 0x06000048 RID: 72 RVA: 0x00002B0A File Offset: 0x00000D0A
        [SettingPropertyInteger("Profit per Extra Prosperity", 1, 500, "0", Order = 1, RequireRestart = false, HintText = "How much profit to get per extra prosperity when the option is set by the owner.")]
        [SettingPropertyGroup("Town Finances Settings")]
        public int TownProsperityProfit { get; set; } = 20;

        // Token: 0x17000021 RID: 33
        // (get) Token: 0x06000049 RID: 73 RVA: 0x00002B13 File Offset: 0x00000D13
        // (set) Token: 0x0600004A RID: 74 RVA: 0x00002B1B File Offset: 0x00000D1B
        [SettingPropertyInteger("Profit per Extra Militia", 1, 500, "0", Order = 1, RequireRestart = false, HintText = "How much profit to get per extra militia when the option is set by the owner.")]
        [SettingPropertyGroup("Town Finances Settings")]
        public int TownMilitiaProfit { get; set; } = 25;

        // Token: 0x17000022 RID: 34
        // (get) Token: 0x0600004B RID: 75 RVA: 0x00002B24 File Offset: 0x00000D24
        // (set) Token: 0x0600004C RID: 76 RVA: 0x00002B2C File Offset: 0x00000D2C
        [SettingPropertyInteger("Profit per Extra Food", 1, 500, "0", Order = 1, RequireRestart = false, HintText = "How much profit to get per extra food when the option is set by the owner.")]
        [SettingPropertyGroup("Town Finances Settings")]
        public int TownFoodProfit { get; set; } = 5;

        // Token: 0x17000023 RID: 35
        // (get) Token: 0x0600004D RID: 77 RVA: 0x00002B35 File Offset: 0x00000D35
        // (set) Token: 0x0600004E RID: 78 RVA: 0x00002B3D File Offset: 0x00000D3D
        [SettingPropertyFloatingInteger("Rent Profit per Prosperity", 0.01f, 10f, "0.00", Order = 1, RequireRestart = false, HintText = "How much profit to get from rent per town prosperity when the option is set by the owner.")]
        [SettingPropertyGroup("Town Finances Settings")]
        public float TownRentProfit { get; set; } = 0.05f;

        // Token: 0x17000024 RID: 36
        // (get) Token: 0x0600004F RID: 79 RVA: 0x00002B46 File Offset: 0x00000D46
        // (set) Token: 0x06000050 RID: 80 RVA: 0x00002B4E File Offset: 0x00000D4E
        [SettingPropertyInteger("Town Support Threshold", 1000, 100000, "0", Order = 1, RequireRestart = false, HintText = "Towns with less gold than this number will get financial support from their owners.")]
        [SettingPropertyGroup("Town Support Settings")]
        public int TownSupportThreshold { get; set; } = 10000;

        // Token: 0x17000025 RID: 37
        // (get) Token: 0x06000051 RID: 81 RVA: 0x00002B57 File Offset: 0x00000D57
        // (set) Token: 0x06000052 RID: 82 RVA: 0x00002B5F File Offset: 0x00000D5F
        [SettingPropertyInteger("Town Owner Threshold", 1000, 100000, "0", Order = 2, RequireRestart = false, HintText = "Lords with more gold than this number will provide financial support to their poor towns.")]
        [SettingPropertyGroup("Town Support Settings")]
        public int OwnerSupportThreshold { get; set; } = 10000;

        // Token: 0x17000026 RID: 38
        // (get) Token: 0x06000053 RID: 83 RVA: 0x00002B68 File Offset: 0x00000D68
        // (set) Token: 0x06000054 RID: 84 RVA: 0x00002B70 File Offset: 0x00000D70
        [SettingPropertyFloatingInteger("Player Support Tax", 0f, 1f, "0.0%", Order = 1, RequireRestart = false, HintText = "When the player aids their towns financially, this inflation tax will be taken.")]
        [SettingPropertyGroup("Town Support Settings")]
        public float PlayerSupportTax { get; set; } = 0.3f;
    }
}
