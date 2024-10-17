using System;
using System.Reflection;

namespace ReflectionRun.Editor
{
    public class EditorFactoryInstance : IReflectionRunFactory
    {
        public Action Init => EditorFactory.Init;
        public Func<FieldInfo, object, InfoBaseGroupBox> GetFieldInfoRow => EditorFactory.GetFieldInfoRow;
        public Func<MethodInfo, object, InfoBaseGroupBox> GetMethodInfoRow => EditorFactory.GetMethodInfoRow;
        public Func<PropertyInfo, object, InfoBaseGroupBox> GetPropertyInfoRow=> EditorFactory.GetPropertyInfoRow;
        public Func<Type, ValueView> GetDeepValueView => EditorFactory.GetDeepValueView;
        public Func<ParameterInfo, MethodParmaValueField> GetMethodParmaValueField => EditorFactory.GetMethodParmaValueField;
        public Func<Type, bool> CanCreateMethodParmaValueField => EditorFactory.CanCreateMethodParmaValueField;
        public Func<Type, bool, ValueView> GetMethodReturnValueView => EditorFactory.GetMethodReturnValueView;
    }
}