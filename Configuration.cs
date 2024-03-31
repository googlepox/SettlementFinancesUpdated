using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace SettlementFinances
{
    internal class Configuration : AttributeGlobalSettings<Configuration>
    {
        public override string Id => "Settlement.Finances";

        public override string FormatType => "json2";

        public override string DisplayName => "Settlement Finances";

        public override string FolderName => "SettlementFinances";

        [SettingPropertyFloatingInteger("Village Investment Income Multiplier", 0.1f, 5f, "0.00", Order = 1, RequireRestart = false, HintText = "Multiplier for the income received from village investments (They go from ~90 on food plantations on a bad roll to ~360 on silver galleries on a good roll)")]
        [SettingPropertyGroup("Village Settings")]
        public float VillageIncomeMultiplier { get; set; } = 1f;

        [SettingPropertyFloatingInteger("Investment Price Multiplier", 0.1f, 5f, "0.00", Order = 2, RequireRestart = false, HintText = "Multiplier for the price of village investments (They go from 2850 base on a good roll for food plantations to 15600 on a bad roll for silver galleries)")]
        [SettingPropertyGroup("Village Settings")]
        public float VillagePriceMultiplier { get; set; } = 1f;

        [SettingPropertyFloatingInteger("Investment Selling Multiplier", 0.1f, 5f, "0.00", Order = 3, RequireRestart = false, HintText = "Multiplier when selling village investments (fraction of the total buying price)")]
        [SettingPropertyGroup("Village Settings")]
        public float VillageSellMultiplier { get; set; } = 0.6f;

        [SettingPropertyInteger("Maximum Base Investments", 0, 5, "0.00", Order = 4, RequireRestart = false, HintText = "Maximum base number of investments of the player (CAUTION: After 14 investments, they will go off screen in the clan finance window.)")]
        [SettingPropertyGroup("Village Settings")]
        public int VillageMax { get; set; } = 2;

        [SettingPropertyFloatingInteger("Maximum Investments Per Clan Tier", 0f, 4f, "0.00", Order = 5, RequireRestart = false, HintText = "Maximum number of investments per clan tier")]
        [SettingPropertyGroup("Village Settings")]
        public float VillageMaxPerTier { get; set; } = 2f;

        [SettingPropertyFloatingInteger("Percentage Increases per Hearth", 1E-05f, 0.05f, "0.00%", Order = 6, RequireRestart = false, HintText = "Percentage to add to prices and profits per village hearth")]
        [SettingPropertyGroup("Village Settings")]
        public float VillagePercentagePerHearth { get; set; } = 0.0005f;

        [SettingPropertyInteger("Generous Investors Hearth Cap", 100, 10000, "0", Order = 7, RequireRestart = false, HintText = "The maximum amount of hearth a village to be able to get a hearth increase from your investments")]
        [SettingPropertyGroup("Village Settings")]
        public int VillageHearthCap { get; set; } = 1000;

        [SettingPropertyFloatingInteger("Generous Investors Hearth Gain", 0.1f, 50f, "0.00", Order = 8, RequireRestart = false, HintText = "How much hearth a village gains if it is maximally invested in. ( Partial investments yield partial results )")]
        [SettingPropertyGroup("Village Settings")]
        public float VillageHearthGain { get; set; } = 3f;

        [SettingPropertyInteger("Base Account Price", 0, 50000, "0", Order = 1, RequireRestart = false, HintText = "The base price for opening a new account.")]
        [SettingPropertyGroup("Bank Settings")]
        public int BankAccountPriceBase { get; set; } = 1000;

        [SettingPropertyInteger("Account Opening Price per Renown", 0, 500, "0", Order = 2, RequireRestart = false, HintText = "The price per clan renown for opening a new account.")]
        [SettingPropertyGroup("Bank Settings")]
        public int BankAccountPricePerRenown { get; set; } = 10;

        [SettingPropertyFloatingInteger("Town Interest Multiplier", 0.01f, 10f, "0.00", Order = 3, RequireRestart = false, HintText = "The interest multiplier for a town bank for each prosperity point of the town. ( At x1, the interest rate is: Prosperity/10,000 )")]
        [SettingPropertyGroup("Bank Settings")]
        public float BankInterestRate { get; set; } = 1f;

        [SettingPropertyInteger("Hard Cap for Loans", 0, 5000000, "0", Order = 4, RequireRestart = false, HintText = "The absolute maximum value of a loan.")]
        [SettingPropertyGroup("Loan Settings")]
        public int BankLoanCap { get; set; } = 1000000;

        [SettingPropertyInteger("Maximum Loan per Renown", 0, 10000, "0", Order = 5, RequireRestart = false, HintText = "The max loan you can take per renown point. ( Cannot go above the hard cap )")]
        [SettingPropertyGroup("Loan Settings")]
        public int BankMaxLoanPerRenown { get; set; } = 250;

        [SettingPropertyInteger("Loan Paying Days", 1, 300, "0", Order = 6, RequireRestart = false, HintText = "The amount of days you have to pay a loan back")]
        [SettingPropertyGroup("Loan Settings")]
        public int BankLoanDays { get; set; } = 21;

        [SettingPropertyFloatingInteger("Loan Interest Rate", 0f, 1f, "0%", Order = 7, RequireRestart = false, HintText = "The extra loan percentage you have to pay when paying a loan back. This is applied another time if you fail to pay on time.")]
        [SettingPropertyGroup("Loan Settings")]
        public float BankLoanInterest { get; set; } = 0.2f;

        [SettingPropertyInteger("Notable Influence Cap", 0, 1000, "0", Order = 8, RequireRestart = false, HintText = "The maximum amount of influence a notable can get from investments from your bank account.")]
        [SettingPropertyGroup("Bank Investment Settings")]
        public int NotableInfluenceCap { get; set; } = 400;

        [SettingPropertyInteger("Notable Influence Probability", 1, 5000000, "0", Order = 9, RequireRestart = false, HintText = "Every day, there is a *GOLD_IN_ACCOUNT* / *THIS_NUMBER* chance that the notables you chose to invest in gain an amount of influence. ( Eg. if you have gold in your account equal to this number, they will gain influence daily)")]
        [SettingPropertyGroup("Bank Investment Settings")]
        public int NotableInfluenceProbability { get; set; } = 1000000;

        [SettingPropertyInteger("Notable Influence Amount", 0, 20, "0", Order = 10, RequireRestart = false, HintText = "Every day, if the roll is succesful, by how much should notables' influence be increased.")]
        [SettingPropertyGroup("Bank Investment Settings")]
        public int NotableInfluenceAmount { get; set; } = 3;

        [SettingPropertyBool("Enable Caravan Replenishment?", Order = 1, RequireRestart = false, HintText = "Should caravan leaders start new caravans when eliberated from captivity after a lost battle?")]
        [SettingPropertyGroup("Caravan Settings")]
        public bool CaravanReplenishEnable { get; set; } = true;

        [SettingPropertyFloatingInteger("Caravan Replenish Chance", 0f, 1f, "0%", Order = 2, RequireRestart = false, HintText = "After eliberated, every day there is a chance that caravan leaders will start a caravan.")]
        [SettingPropertyGroup("Caravan Settings")]
        public float CaravanReplenishChance { get; set; } = 0.5f;

        [SettingPropertyInteger("Caravan Starting Gold", 0, 15000, "0", Order = 3, RequireRestart = false, HintText = "What amount of gold do rebuilt caravans start with? ( Note that they will take the extra gold needed from the owner )")]
        [SettingPropertyGroup("Caravan Settings")]
        public int CaravanStartingGold { get; set; } = 500;

        [SettingPropertyFloatingInteger("Caravan Tariff per Leader's Trade Skill", 0f, 50f, "0.00", Order = 4, RequireRestart = false, HintText = "Caravan leaders will pay a tariff equal to this * the leader's trade skill and some based on their steward skill.")]
        [SettingPropertyGroup("Caravan Settings")]
        public float CaravanTariffPerTrade { get; set; } = 2f;

        [SettingPropertyFloatingInteger("Caravan Tariff per Leader's Steward Skill", 0f, 50f, "0.00", Order = 4, RequireRestart = false, HintText = "Caravan leaders will pay a tariff equal to this * the leader's steward skill and some based on their trade skill.")]
        [SettingPropertyGroup("Caravan Settings")]
        public float CaravanTariffPerSteward { get; set; } = 2f;

        [SettingPropertyInteger("Profit per Extra Prosperity", 1, 500, "0", Order = 1, RequireRestart = false, HintText = "How much profit to get per extra prosperity when the option is set by the owner.")]
        [SettingPropertyGroup("Town Finances Settings")]
        public int TownProsperityProfit { get; set; } = 20;

        [SettingPropertyInteger("Profit per Extra Militia", 1, 500, "0", Order = 1, RequireRestart = false, HintText = "How much profit to get per extra militia when the option is set by the owner.")]
        [SettingPropertyGroup("Town Finances Settings")]
        public int TownMilitiaProfit { get; set; } = 25;

        [SettingPropertyInteger("Profit per Extra Food", 1, 500, "0", Order = 1, RequireRestart = false, HintText = "How much profit to get per extra food when the option is set by the owner.")]
        [SettingPropertyGroup("Town Finances Settings")]
        public int TownFoodProfit { get; set; } = 5;

        [SettingPropertyFloatingInteger("Rent Profit per Prosperity", 0.01f, 10f, "0.00", Order = 1, RequireRestart = false, HintText = "How much profit to get from rent per town prosperity when the option is set by the owner.")]
        [SettingPropertyGroup("Town Finances Settings")]
        public float TownRentProfit { get; set; } = 0.05f;

        [SettingPropertyInteger("Town Support Threshold", 1000, 100000, "0", Order = 1, RequireRestart = false, HintText = "Towns with less gold than this number will get financial support from their owners.")]
        [SettingPropertyGroup("Town Support Settings")]
        public int TownSupportThreshold { get; set; } = 10000;

        [SettingPropertyInteger("Town Owner Threshold", 1000, 100000, "0", Order = 2, RequireRestart = false, HintText = "Lords with more gold than this number will provide financial support to their poor towns.")]
        [SettingPropertyGroup("Town Support Settings")]
        public int OwnerSupportThreshold { get; set; } = 10000;

        [SettingPropertyFloatingInteger("Player Support Tax", 0f, 1f, "0.0%", Order = 1, RequireRestart = false, HintText = "When the player aids their towns financially, this inflation tax will be taken.")]
        [SettingPropertyGroup("Town Support Settings")]
        public float PlayerSupportTax { get; set; } = 0.3f;
    }
}
