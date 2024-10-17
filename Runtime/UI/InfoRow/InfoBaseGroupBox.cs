using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    public class InfoBaseGroupBox: GroupBox
    {
        protected VisualElement layoutBox;
        protected object _ins;
        public string infoName;
        protected ReflectionClassWindow _window;
        protected bool _isEditor;
        
        public InfoBaseGroupBox(object ins,bool isEditor)
        {
            _ins = ins;
            _isEditor = isEditor;
            layoutBox = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            Add(layoutBox);
            style.backgroundColor = new Color(0.25f, 0.25f, 0.25f, 1);
        }

        protected void CreateDeclaringTypeView(VisualElement box , Type type)
        {
            if (type != null)
            {
                var declaring = new Label(type.Name);
                declaring.style.color = new Color(0.75f, 0.55f, 1, 1);
                var dot = new Label(".");
                
                dot.PaddingZero();
                dot.MarginZero();
                box.Add(declaring);
                box.Add(dot);
            }
        }

        public virtual void Update()
        {
            
        }

        public void SetParent(ReflectionClassWindow window)
        {
            _window = window;
        }

        public virtual void Hide()
        {
            style.display = DisplayStyle.None;
        }
        public virtual void Show()
        {
            style.display = DisplayStyle.Flex;
        }
    }
}