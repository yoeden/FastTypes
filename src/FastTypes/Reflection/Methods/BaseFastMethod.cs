using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseFastMethod : FastMember
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected BaseFastMethod(MethodInfo info)
        {
            Name = info.Name;
            IsStatic = info.IsStatic;
            ReturnType = info.ReturnType;
            Parameters = info.GetParameters();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVoid => ReturnType == typeof(void);

        /// <summary>
        /// 
        /// </summary>
        public Type ReturnType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<ParameterInfo> Parameters { get; }

        /// <inheritdoc />
        public override string Name { get; }

        /// <inheritdoc />
        public override bool IsStatic { get; }
    }
}