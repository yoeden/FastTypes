using System;

namespace FastTypes.Query
{
    public interface ITypeQueryBuilderAppend
    {
        ITypeQueryBuilderTargets And();
    }

    public interface ITypeQueryBuilderModifiers : ITypeQueryBuilderPreparation
    {
        ITypeQueryBuilderModifiers WithCriteria(ITypeQueryCriteria criteria);

        ITypeQueryBuilderModifiers Tag<T>(T tag);

        ITypeQueryBuilderModifiers NotPublic();

        ITypeQueryBuilderModifiers WithPropertyOfType<T>();
        ITypeQueryBuilderModifiers WithPropertyOfType(Type t);

        ITypeQueryBuilderModifiers WithMethodOfType<T>();
        ITypeQueryBuilderModifiers WithMethodOfType(Type t);

        ITypeQueryBuilderModifiers WithAttribute<T>() where T : Attribute;
        ITypeQueryBuilderModifiers WithAttribute(Type t);

        ITypeQueryBuilderModifiers AssignableTo<T>();
        ITypeQueryBuilderModifiers AssignableTo(Type t);
    }
}