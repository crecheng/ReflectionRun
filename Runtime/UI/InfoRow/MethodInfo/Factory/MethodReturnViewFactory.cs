using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    internal static partial class RuntimeFactory
    {
        private static class MethodReturnViewFactory
        {
            private static readonly Dictionary<Type, MethodInfo> _preciseTypeMethod =
                new Dictionary<Type, MethodInfo>();
            
            private static readonly Dictionary<Type, MethodInfo> _baseTypeMethod =
                new Dictionary<Type, MethodInfo>();

            private static Type _baseType = typeof(ValueView);

            internal static void MethodParmaValueFieldInit()
            {
                _preciseTypeMethod.Clear();
                var all = new List<MethodInfo>();
                
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(typeof(MethodReturnViewPreciseTypeFactory)));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_MethodReturnViewPreciseTypeFactory));
                
                foreach (var method in all)
                {
                    var type = IsCreateMethod(method);
                    if (type == null)
                        continue;
                    _preciseTypeMethod.Add(type, method);
                }
                
                _baseTypeMethod.Clear();
                all.Clear();
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(typeof(MethodReturnViewBaseTypeFactory)));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_MethodReturnViewBaseTypeFactory));
                
                foreach (var method in all)
                {
                    var type = IsCreateMethod(method);
                    if (type == null)
                        continue;
                    _baseTypeMethod.Add(type, method);
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
                    var groupObj = methodInfo.Invoke(null, new[] { (object)null ,(object)false});
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
                    var view = new EnumValueView(false);
                    view.SetType(type);
                    return view;
                }

                if(defaultView)
                {
                    var vd = GetErrorDefault();
                    vd.SetType(type);
                    return vd;
                }

                return null;
            }

            internal static DeepFieldValueView GetDeepFieldValueView(Type type)
            {
                var view = new DeepFieldValueView(false);
                view.SetType(type);
                return view;
            }

            private static ValueView GetErrorDefault()
            {
                var field = new ObjectValueView(false);
                return field;
            }
        }
    }
}