using System;
using System.Reflection;
using System.Reflection.Emit;

namespace FastTypes.Clone
{
    internal static class CopyCompiler
    {
        internal static Func<object, object> CompileObjectSignature(Type t, MethodInfo methodInfo)
        {
            DynamicMethod method = new(
                "DeepCopy_Obj",
                typeof(object),
                new[] { typeof(object) }
            );

            var il = method.GetILGenerator();

            if (t.IsValueType)
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Unbox_Any, t);
                il.Emit(OpCodes.Call, methodInfo);
                il.Emit(OpCodes.Box, t);

                il.Emit(OpCodes.Ret);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Castclass, t);
                il.Emit(OpCodes.Call, methodInfo);

                il.Emit(OpCodes.Ret);
            }

            return (Func<object, object>)method.CreateDelegate(typeof(Func<object, object>));
        }

        public static Delegate Compile(Type t)
        {
            //
            var args = new[] { t };
            DynamicMethod method = new(
                "DeepCopy",
                t,
                args
            );
            var il = method.GetILGenerator();

            CopyILEmitter.EmitCopy(il, t);

            var compiled = method.CreateDelegate(typeof(Func<,>).MakeGenericType(t, t));
            return compiled;
        }
    }
}