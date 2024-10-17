using System;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    internal sealed class ObjectButton : Button
    {
        public object Obj { get; private set; }
        public ObjectButton(string text,object obj, Action<object> onClick=null)
        {
            this.text = text;
            Obj = obj;
            clicked += () => onClick?.Invoke(Obj);

        }
    }
}