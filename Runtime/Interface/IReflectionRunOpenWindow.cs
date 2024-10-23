using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    /// <summary>
    /// 窗口相关接口
    /// </summary>
    public interface IReflectionRunOpenWindow
    {
        public VisualElement CreateClassWindow(Type type);
        public VisualElement CreateClassWindow(object obj);
        public VisualElement CreateListViewWindow(IList list);

        public void NewWindow(VisualElement visualElement,string title);

        public virtual void OpenPropertyEditor(UnityEngine.Object obj)
        {
            Debug.Log("only editor window!");
        }
    }
}