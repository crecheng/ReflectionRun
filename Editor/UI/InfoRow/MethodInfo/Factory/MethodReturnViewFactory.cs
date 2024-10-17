using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun.Editor
{
    internal static partial class EditorFactory
    {
        private static class MethodReturnViewEditorFactory
        {
            private static readonly Dictionary<Type, MethodInfo> _preciseTypeMethod =
                new Dictionary<Type, MethodInfo>();
            
            private static readonly Dictionary<Type, MethodInfo> _baseTypeMethod =
                new Dictionary<Type, MethodInfo>();

            private static Type _baseType = typeof(ValueView);

            internal static void MethodParmaValueFieldInit()
            {
                _preciseTypeMethod.Clear();
                List<MethodInfo> all = new List<MethodInfo>();
                
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(typeof(MethodReturnViewPreciseTypeEditorFactory)));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_MethodReturnViewPreciseTypeEditorFactory));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_MethodReturnViewPreciseTypeFactory));
                all.AddRange(ReflectionRunEditorUtil.GetMethodReturnViewPreciseTypeMethod());
                
                foreach (var method in all)
                {
                    var type = IsCreateMethod(method);
                    if (type == null)
                        continue;
                    _preciseTypeMethod[type]=method;
                }
                
                _baseTypeMethod.Clear();
                all.Clear();
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(typeof(MethodReturnViewBaseTypeEditorFactory)));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_MethodReturnViewBaseTypeEditorFactory));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_MethodReturnViewBaseTypeFactory));
                all.AddRange(ReflectionRunEditorUtil.GetMethodReturnViewBaseTypeMethod());
                
                foreach (var method in all)
                {
                    var type = IsCreateMethod(method);
                    if (type == null)
                        continue;
                    _baseTypeMethod[type]=method;
                }
            }

            private static Type IsCreateMethod(MethodInfo methodInfo)
            {
                if (methodInfo.ReturnParameter == null)
                    return null;
                var reType = methodInfo.ReturnParameter.ParameterType;
                if (!_baseType.IsAssignableFrom(reType))
                    return null;
                ;
                var param = methodInfo.GetParameters();
                
                if (param.Length != 2)
                    return null;
                return param[0].ParameterType;
            }

            private static ValueView CreateFromMethodInfo(MethodInfo methodInfo)
            {
                try
                {
                    var groupObj = methodInfo.Invoke(null, new[] { (object)null ,true});
                    if (groupObj is ValueView valueField)
                    {
                        return valueField;
                    }
                    else
                    {
                        Debug.LogError($"MethodReturnViewPreciseTypeFactory method return error!");
                        return GetErrorDefault();
                    }

                }
                catch (Exception e)
                {
                    Debug.LogError($"MethodReturnViewPreciseTypeFactory method  error!\n{e.ToString()}");
                    return GetErrorDefault();
                }
            }

            internal static ValueView GetValueViewFromFactory(Type type,bool defaultView)
            {
                if (_preciseTypeMethod.TryGetValue(type, out var methodInfo))
                {
                    var view = CreateFromMethodInfo(methodInfo);
                    view.SetType(type);
                    return view;
                }
                else
                {
                    foreach (var (key, methodInfoB) in _baseTypeMethod)
                    {
                        if (key.IsAssignableFrom(type))
                        {
                            var view=CreateFromMethodInfo(methodInfoB);
                            view.SetType(type);
                            return view;
                        }
                    }
                }

                if (type.IsEnum)
                {
                    ReflectionRunEditorUtil.GetEnumValueView(type);
                }

                if(defaultView)
                {
                    var vd = GetErrorDefault();
                    vd.SetType(type);
                    return vd;
                }

                return null;
            }

            internal static ValueView GetDeepFieldValueView(Type type)
            {

                return ReflectionRunEditorUtil.GetDeepFieldValueView(type);
            }

            private static ValueView GetErrorDefault()
            {
                return ReflectionRunEditorUtil.GetValueViewDefault();
            }
        }
    }
}