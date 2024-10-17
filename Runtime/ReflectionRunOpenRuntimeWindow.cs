using System;
using System.Collections;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    /// <summary>
    /// 运行时的窗口打开调用
    /// </summary>
    internal class ReflectionRunOpenRuntimeWindow : IReflectionRunOpenWindow
    {
        protected RuntimeMainWindows _windows;
        protected bool NeedWindows = false;
        public void SetMainWindow(RuntimeMainWindows windows)
        {
            _windows = windows;
            NeedWindows = true;
        }
        public VisualElement CreateClassWindow(Type type)
        {
            var windows=new ReflectionClassWindow(false);
            windows.SetType(type);

            if (NeedWindows)
            {
                windows.OnClose = _windows.RemoveWindow;
                _windows.AddWindow(windows,type.Name);
            }
            
            return windows;
        }
        public VisualElement CreateClassWindow(object obj)
        {
            var windows=new ReflectionClassWindow(false);
            windows.SetObject(obj);
            if (NeedWindows)
            {
                windows.OnClose = _windows.RemoveWindow;
                _windows.AddWindow(windows,obj.GetType().Name);
            }
            return windows;
        }
        public VisualElement CreateListViewWindow(IList list)
        {
            var windows=new ReflectionListObjWindow(false);
            windows.SetList(list);
            if (NeedWindows)
            {
                windows.OnClose = _windows.RemoveWindow;
                _windows.AddWindow(windows,list.GetType().Name);
            }
            return windows;
        }
    }
}