using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Reflection;
using Newtonsoft.Json.Linq;

namespace FastTypes.Tests.Reflection.Fields
{
    public class InstanceAsObjectAndReturnAsSelfFieldTests : FieldTests<int>
    {
        public override int GetValue<TType>(TType instance, string name)
        {
            return GetField<TType>(name)
                .GetValue(instance);
        }

        public override void SetValue<TType>(TType instance, string name, object value)
        {
            GetField<TType>(name).SetValue(instance, (int)value);
        }

        public override FastFieldKnownValue<int> GetField<TType>(string name)
        {
            return FastType
                .Of(typeof(TType))
                .Field<int>(name);
        }

        public override int Value() => 23;
        public override object BadValue() => string.Empty;
    }


    // Work smarter, not harder ;)
    //TODO: Make all the reflection api tests like that
}
