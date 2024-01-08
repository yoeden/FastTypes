namespace FastTypes.Tests
{
    public static class ExpectedValues
    {
        public static int RandomInt() => Random.Shared.Next();

        public const string Str = "ASD";
        public const int Int = 1337;
    }
}