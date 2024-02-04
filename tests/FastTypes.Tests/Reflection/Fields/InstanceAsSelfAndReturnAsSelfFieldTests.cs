using FastTypes.Reflection;

namespace FastTypes.Tests.Reflection.Fields
{
    public class InstanceAsSelfAndReturnAsSelfFieldTests : FieldTests<int>
    {
        public override int GetValue<TType>(TType instance, string name)
        {
            return (int)GetField<TType>(name)
                .GetValue(instance);
        }

        public override void SetValue<TType>(TType instance, string name, object value)
        {
            GetField<TType>(name).SetValue(instance, (int)value);
        }

        public override FastField<TType, int> GetField<TType>(string name)
        {
            return FastType
                .Of<TType>()
                .Field<int>(name);
        }

        public override int Value() => 23;
        public override object BadValue() => string.Empty;
    }
}