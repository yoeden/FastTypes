using System.Runtime.CompilerServices;

namespace FastTypes.Tests
{
    public static class Traits
    {
        public static class Reflection
        {
            public const string Tag = "Reflection";

            public const string Properties = "Properties";
            public const string PropertiesGet = "Properties - Get";
            public const string PropertiesSet = "Properties - Set";
            public const string Method = "Methods";
            public const string Activator = "Activators";
        }

        public static class Compiler
        {
            public const string Tag = "Compiler";

            public const string ExpressionTree = "ExpressionTree";
            public const string IL = "IL";
        }

        public static class FastType
        {
            public const string Tag = "FastType";
        }

        public static class Query
        {
            public const string Tag = "Query";

            public const string Builder = "Builder";
            public const string Tags = "Tags";
            public const string Scanner = "Scanner";
            public const string Criterias = "Criterias";
            public const string Instanciator = "Instanciator";
        }

        public static class DataStructures
        {
            public const string Tag = "Data Structures";

            public const string UnmodifiableFastDictionaryByInt = "UnmodifiableFastDictionaryByInt";
            public const string UnmodifiableFastDictionaryByString = "UnmodifiableFastDictionaryByString";
            public const string LockableSet = "LockableSet";
            public const string LockableList = "LockableList";
        }

        public static class Copy
        {
            public const string Tag = "Copy";

            public const string Object = "Object";
            public const string Generic = "Generic";
        }
    }
}