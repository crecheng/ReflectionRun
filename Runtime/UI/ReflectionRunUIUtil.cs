using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    public static class ReflectionRunUIUtil
    {
        public static Color InfoGroupBoxTypeColor = new Color(0.27f, 0.86f, 0.9f);
        public static Color PropertyColor = new Color(0, 0.75f, 0.15f, 1);
        public static Color MethodColor = new Color(0.2f, 0.8f, 0.5f, 1);
        public static Color FieldColor = new Color(0.35f, 0.75f, 0.65f, 1);
        public static VisualElement GetHorizontalLayoutGroup()
        {
            VisualElement horizontalLayout = new VisualElement();
            horizontalLayout.AddToClassList("horizontalLayout");
            horizontalLayout.style.flexDirection = FlexDirection.Row;
            horizontalLayout.style.justifyContent = Justify.FlexStart;
            horizontalLayout.style.alignItems = Align.Center;
            return horizontalLayout;
        }
        
        
        public static Button GetButton(string text, Action action,bool isEditor)
        {
            var btn = new Button(action);
            btn.text = text;
            if (!isEditor)
                ButtonRuntimeStyle(btn);
            
            return btn;
        }
        
        internal static ObjectButton GetObjectButton(string text,object obj, Action<object> action = null)
        {
            var btn = new ObjectButton(text,obj,action);
            ButtonRuntimeStyle(btn);
            return btn;
        }

        public static void ButtonRuntimeStyle(VisualElement btn)
        {
            btn.style.backgroundColor = new Color(0.35f, 0.35f, 0.35f);
            btn.style.color=new Color(0.77f, 0.77f, 0.77f);
            
            btn.style.paddingTop = 7;
            btn.style.paddingBottom = 7;
            
            btn.style.paddingLeft = 5;
            btn.style.paddingRight = 5;
        }
        

        public static GroupBox GetNoPaddingGroupBox()
        {
            var groupBox = new GroupBox();
            groupBox.style.paddingBottom = 0;
            groupBox.style.paddingLeft = 0;
            groupBox.style.paddingRight = 0;
            groupBox.style.paddingTop = 0;
            return groupBox;
        }

        public static void PaddingZero(this VisualElement visualElement)
        {
            visualElement.style.paddingTop = 0;
            visualElement.style.paddingBottom = 0;
            
            visualElement.style.paddingLeft = 0;
            visualElement.style.paddingRight = 0;
        }
        
        public static void DisplayStyle(this VisualElement visualElement, bool show)
        {
            visualElement.style.display =
                show ? UnityEngine.UIElements.DisplayStyle.Flex : UnityEngine.UIElements.DisplayStyle.None;
        }
        public static void PaddingDefault(this VisualElement visualElement)
        {
            visualElement.style.paddingTop = 5;
            visualElement.style.paddingBottom = 5;
            
            visualElement.style.paddingLeft = 3;
            visualElement.style.paddingRight = 0;
        }
        
        public static void MarginZero(this VisualElement visualElement)
        {
            visualElement.style.marginTop = 0;
            visualElement.style.marginBottom = 0;
            
            visualElement.style.marginLeft = 0;
            visualElement.style.marginRight = 0;
        }
        
        public static void MarginDefault(this VisualElement visualElement)
        {
            visualElement.style.marginTop = 6;
            visualElement.style.marginBottom = 1;
            
            visualElement.style.marginLeft = 3;
            visualElement.style.marginRight = 3;

        }
    }
}