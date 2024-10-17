using UnityEngine;

namespace ReflectionRun
{
    public class ReflectionScriptableObject : ScriptableObject
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        public object Obj;
    }
}