
/*namespace PboneUtils.DataStructures
{
    public struct FishingChances
    {
        public const int MaxCommon = 2;
        public const int MaxUncommon = 3;
        public const int MaxRare = 4;
        public const int MaxVeryRare = 5;
        public const int MaxExtremelyRare = 6;

        public int Common;
        public int Uncommon;
        public int Rare;
        public int VeryRare;
        public int ExtremelyRare;

        public FishingChances(Player player)
        {
            int power = player.FishingLevel();

            Common = 150 / power;
            Uncommon = 300 / power;
            Rare = 1050 / power;
            VeryRare = 2250 / power;
            ExtremelyRare = 4500 / power;

            Common = Math.Min(Common, MaxCommon);
            Uncommon = Math.Min(Uncommon, MaxUncommon);
            Rare = Math.Min(Rare, MaxRare);
            VeryRare = Math.Min(VeryRare, MaxVeryRare);
            ExtremelyRare = Math.Min(ExtremelyRare, MaxExtremelyRare);
        }

        public static bool Check(int value) => Main.rand.NextBool(value);
    }
}*/
