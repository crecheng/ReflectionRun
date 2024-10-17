using System;
using System.Reflection;

namespace ReflectionRun
{
    /// <summary>
    /// 创建类的方法工厂接口
    /// </summary>
    public interface IReflectionRunFactory
    {
        public virtual Action Init => null;
        public virtual Func<FieldInfo, object, InfoBaseGroupBox> GetFieldInfoRow => null;
        public virtual Func<MethodInfo, object, InfoBaseGroupBox> GetMethodInfoRow => null;
        public virtual Func<PropertyInfo, object, InfoBaseGroupBox> GetPropertyInfoRow => null;
        public virtual Func<Type, ValueView> GetDeepValueView => null;
        public virtual Func<ParameterInfo, MethodParmaValueField> GetMethodParmaValueField => null;
        public virtual Func<Type, bool> CanCreateMethodParmaValueField => null;
        public virtual Func<Type, bool,ValueView> GetMethodReturnValueView => null;
        
    }
}