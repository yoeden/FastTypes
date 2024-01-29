using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using FastTypes.Compiler;

namespace FastTypes.Clone.AnotherTake
{
    internal sealed class GenericCollectionInterfaceCopyAction : DeepCopyAction
    {
        public GenericCollectionInterfaceCopyAction(IDeepCopyEvaluator evaluator) : base(evaluator)
        {
        }

        public override bool Copy(FluentIL il, Type type)
        {
            //TODO: As of now immutable collections are supported, but they can be cloned as if theyre a complex object (<-- Check that)

            if (
                !type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>))
                )
                return false;

            var target = CopyTarget.FromType(type);

            if (!IsDictionaryType(type) && GetIndexer(type) != null)
            {
                //Indexer
                CopyCollectionWithIndexer(il, type);
            }
            else
            {
                //Enumerable
                CopyCollectionWithEnumerator(il, type);
            }

            return true;
        }

        private static bool IsDictionaryType(Type type)
        {
            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
        }

        private static MethodInfo GetIndexer(Type type) =>
            type
                .GetProperties()
                .FirstOrDefault(p => p.GetIndexParameters().Length != 0 && p.GetIndexParameters()[0].ParameterType == typeof(int))?.GetMethod;

        private void CopyCollectionWithEnumerator(FluentIL il, Type type)
        {
            var target = CopyTarget.FromType(type);

            var collectionInterface = type.GetInterfaces().First(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>));
            var elementType = collectionInterface.GetGenericArguments()[0];

            //
            var count = collectionInterface.GetProperty("Count").GetMethod;
            var add = type.GetMethod("Add", new Type[] { elementType }) ?? collectionInterface.GetMethod("Add", new Type[] { elementType });
            var capacityCtor = target.Constructors.FirstOrDefault(ctor => ctor.IsMatching<int>())?.Constructor;
            var emptyDefaultCtor = target.GetDefaultCtor();

            //
            var src = il.DeclareLocal(type, "src");
            var dst = il.DeclareLocal(type, "dst");

            il.StoreLocal(src);

            if (capacityCtor != null)
            {
                //
                il.LoadLocal(src)
                    .Call(count)
                    .New(capacityCtor)
                    .StoreLocal(dst);
            }
            else
            {
                il
                    .New(emptyDefaultCtor)
                    .StoreLocal(dst);
            }

            //
            il
            .ForEach(type, src, (il, loadCurrent) =>
            {
                il.LoadLocal(dst);
                loadCurrent();
                Evaluate(elementType);
                il.Call(add);

                if (add.ReturnType != typeof(void))
                {
                    if (add.ReturnType == type)
                    {
                        //Immutable collection
                        il.StoreLocal(dst);
                    }
                    else
                    {
                        //For example, HashSet returns bool, we dont care about it
                        il.Pop();
                    }
                }
            })
            .LoadLocal(dst);
        }

        private void CopyCollectionWithIndexer(FluentIL il, Type type)
        {
            //
            var collectionInterface = type.GetInterfaces().First(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>));
            var elementType = collectionInterface.GetGenericArguments()[0];

            //
            var src = il.DeclareLocal(type, "src");
            var dst = il.DeclareLocal(type, "dst");
            var n = il.DeclareLocal(typeof(int), "n");

            //
            var count = type.GetProperty("Count").GetMethod;
            var indexer = type.GetProperties().FirstOrDefault(info =>
                info.GetIndexParameters().Length == 1 &&
                info.GetIndexParameters()[0].ParameterType == typeof(int) &&
                info.PropertyType == elementType)?.GetMethod;
            var add = type.GetMethod("Add", new Type[] { elementType }) ?? collectionInterface.GetMethod("Add", new Type[] { elementType });
            var capacityCtor = type.GetConstructor(new[] { typeof(int) });
            var emptyDefaultCtor = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(info => info.GetParameters().Length == 0);

            //
            il
                .StoreLocal(src)
                //
                .LoadLocal(src)
                .Call(count)
                .StoreLocal(n);

            if (capacityCtor != null)
            {
                il
                    //
                    .LoadLocal(n)
                    .New(capacityCtor)
                    .StoreLocal(dst);
            }
            else
            {
                il
                    .New(emptyDefaultCtor)
                    .StoreLocal(dst);
            }

            il
                .For(0, 1, n, (il, i) =>
                {
                    il
                        //
                        .LoadLocal(dst)
                        //
                        .LoadLocal(src)
                        .LoadLocal(i)
                        .Call(indexer);

                    Evaluate(elementType);

                    il.Call(add);

                    if (add.ReturnType != typeof(void))
                    {
                        if (add.ReturnType == type)
                        {
                            //Immutable collection
                            il.StoreLocal(dst);
                        }
                        else
                        {
                            //For example, HashSet returns bool, we dont care about it
                            il.Pop();
                        }
                    }

                })
            .LoadLocal(dst);
        }
    }
}