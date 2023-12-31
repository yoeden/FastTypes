namespace FastTypes.Features.Query
{
    internal sealed class TypeSelector : ITypeSelector
    {
        private bool _isClass;
        private bool _isValueType;
        private bool _isInterface;
        private bool _isEnums;

        public ITypeSelector Classes()
        {
            _isClass = true;
            return this;
        }

        public ITypeSelector Interfaces()
        {
            _isInterface = true;
            return this;
        }

        public ITypeSelector ValueTypes()
        {
            _isValueType = true;
            return this;
        }

        public ITypeSelector Enums()
        {
            _isEnums = true;
            return this;
        }

        public TypeCriteria Create() => new(_isClass, _isValueType, _isInterface, _isEnums);
    }
}