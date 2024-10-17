using System;
using UnityEngine;

namespace ReflectionRun
{
        public class ReflectionMethodScriptableObject : ScriptableObject
        {


#if ODIN_INSPECTOR
                [Sirenix.OdinInspector.Button("填充默认参数(index)")]
#endif
                public void FullDefaultParam(int index)
                {
                        param[index] = Activator.CreateInstance(types[index]);
                }
                
#if ODIN_INSPECTOR
                [Sirenix.OdinInspector.Button("填充默认参数(全部)")]
#endif
                public void FullDefaultAll()
                {
                    for (var i = 0; i < param.Length; i++)
                    {
                        param[i] = Activator.CreateInstance(types[i]);
                    }
                }
                
#if ODIN_INSPECTOR
                [Sirenix.OdinInspector.ShowInInspector]
#endif
                public object[] param;

                public Action OnInvoke;

                public Type[] types;

#if ODIN_INSPECTOR
                [Sirenix.OdinInspector.Button]
#endif
                public void Invoke()
                {
                        OnInvoke?.Invoke();
                }

        }
}