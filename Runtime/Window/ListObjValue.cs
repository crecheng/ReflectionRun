using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    /// <summary>
    /// 对于list的显示-一个index
    /// </summary>
    internal class ListObjValue : VisualElement
    {
        protected Label _indexLabel;
        protected VisualElement hLayout;
        protected VisualElement _group;
        protected ValueView _valueView;
        protected int _index;
        public Func<ReflectionScriptableObject> GetReflectionScriptableObject;
        public Action<int, object> OnApply;
        public bool IsEditor { get; protected set; }
        public ListObjValue(bool isEditor)
        {
            IsEditor = isEditor;
            hLayout = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            _indexLabel = new Label("");
            _indexLabel.style.width = 50;
            _indexLabel.style.paddingLeft = 15;
            hLayout.style.backgroundColor = new Color(0.3f, 0.3f, 0.3f);
            hLayout.Add(_indexLabel);
            _group = new GroupBox();
            _group.PaddingDefault();
            _group.MarginZero();
            hLayout.Add(_group);
            
            hLayout.Add(ReflectionRunUIUtil.GetButton("Apply",Apply,IsEditor));
            
            hLayout.MarginDefault();
            Add(hLayout);
        }

        public void SetValue(int index, object value)
        {
            _index = index;
            _indexLabel.text = index.ToString();
            _group.Clear();
            Type type = null;
            if (value == null)
                type = typeof(object);
            else
                type = value.GetType();

            _valueView = ReflectionRunCenter.GetMethodReturnValueView(IsEditor, type, false);
            if (_valueView == null)
                _valueView = ReflectionRunCenter.GetDeepValueView(IsEditor, type);
            _valueView.GetReflectionScriptableObject = GetReflectionScriptableObject;
            _valueView.SetValue(value);
            
            _group.Add(_valueView);
        }
        
        

        protected void Apply()
        {
            OnApply?.Invoke(_index,_valueView.Value);
        }
    }
}