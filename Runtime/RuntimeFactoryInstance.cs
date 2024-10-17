using System;
using System.Reflection;

namespace ReflectionRun
{
    internal class RuntimeFactoryInstance: IReflectionRunFactory
    {
        public Action Init => RuntimeFactory.Init;
        public Func<FieldInfo, object, InfoBaseGroupBox> GetFieldInfoRow => RuntimeFactory.GetFieldInfoRow;
        public Func<MethodInfo, object, InfoBaseGroupBox> GetMethodInfoRow => RuntimeFactory.GetMethodInfoRow;
        public Func<PropertyInfo, object, InfoBaseGroupBox> GetPropertyInfoRow => RuntimeFactory.GetPropertyInfoRow;
        public Func<Type, ValueView> GetDeepValueView => RuntimeFactory.GetDeepValueView;
        public Func<ParameterInfo, MethodParmaValueField> GetMethodParmaValueField => RuntimeFactory.GetMethodParmaValueField;
        public Func<Type, bool> CanCreateMethodParmaValueField => RuntimeFactory.CanCreateMethodParmaValueField;
        public Func<Type, bool, ValueView> GetMethodReturnValueView => RuntimeFactory.GetMethodReturnValueView;
    }
}