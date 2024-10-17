using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    internal class RuntimeMainWindows : VisualElement
    {
        protected VisualElement hLayout;
        protected VisualElement content;
        protected List<WindowData> _windows;
        protected WindowData _curr;
        internal RuntimeMainWindows()
        {
            style.unityFontStyleAndWeight = FontStyle.Bold;
            style.backgroundColor = new Color(0.22f, 0.22f, 0.22f, 0.7f);
            
            _windows = new List<WindowData>();
            hLayout = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            hLayout.style.height = 50;
            hLayout.style.minHeight = 50;
            hLayout.style.backgroundColor=new Color(0.13f, 0.13f, 0.13f, 0.7f);
            Add(hLayout);
            content = new VisualElement();

            
            Add(content);
            
            AddWindow(new ReflectionRunMainWindow(false),"main");
        }


        public void AddWindow(VisualElement window, string title)
        {
            var data = new WindowData()
            {
                title = title,
                window = window,
            };

            data.button = ReflectionRunUIUtil.GetObjectButton(title, data, (obj) =>
            {
                ShowWindow((WindowData)obj);
            });
            _windows.Add(data);
            hLayout.Add(data.button);
            ShowWindow(data);
        }

        public void RemoveWindow(VisualElement window)
        {
            for (var i = _windows.Count - 1; i >= 0; i--)
            {
                var data = _windows[i];
                if (data.window==window)
                {
                    _windows.RemoveAt(i);
                    hLayout.Remove(data.button);
                    if (_curr == data)
                    {
                        ShowWindow(_windows[^1]);
                    }
                    window.RemoveFromHierarchy();
                    return;
                }
            }
        }

        protected void ShowWindow(WindowData data)
        {
            content.Clear();
            content.Add(data.window);
            _curr = data;
        }
        
        
        protected class WindowData
        {
            public string title;
            public VisualElement window;
            public Button button;
        }


    }
}