using FastTypes.Reflection;

namespace FastTypes.Tests.Reflection.Fields
{
    public class InstanceAsObjectAndReturnAsObjectFieldTests : FieldTests<int>
    {
        public override int GetValue<TType>(TType instance, string name)
        {
            return (int)FastType
                .Of(typeof(TType))
                .Field(name)
                .GetValue(instance);
        }

        public override void SetValue<TType>(TType instance, string name, object value)
        {
            FastType
                .Of(typeof(TType))
                .Field(name)
                .SetValue(instance, value);
        }

        public override FastField GetField<TType>(string name)
        {
            return FastType
                .Of(typeof(TType))
                .Field(name);
        }

        public override int Value() => 23;
        public override object BadValue() => string.Empty;

    }
}