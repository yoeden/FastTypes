namespace FastTypes.Tests.Reflection.Properties
{
    public class StubPropertyClass
    {
        public int ValueTypePropSetGet { set; get; }
        public int PropGetOnly { get; }
        public int PropSetOnly { set { /*Do Nothing*/ } }
    }
}