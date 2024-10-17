using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    internal class DeepFieldValueView : ObjectValueView
    {
        protected VisualElement _fieldGroup;

        internal DeepFieldValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override void OnCreateVisualElement()
        {
            layoutBox = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            layoutBox.PaddingZero();
            var group = new GroupBox();
            group.PaddingZero();
            group.MarginZero();
            _typeBtn = ReflectionRunUIUtil.GetButton("", ShowType,_isEditor);
            _typeBtn.style.color = ReflectionRunUIUtil.InfoGroupBoxTypeColor;
            group.Add(_typeBtn);

            if(_isEditor)
            {
                _propertiesBtn = ReflectionRunUIUtil.GetButton("Properties", ShowProperties, _isEditor);
                group.Add(_propertiesBtn);
                _propertiesBtn.DisplayStyle(false);
            }
            
            layoutBox.Add(group);
            
            _fieldGroup = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            _fieldGroup.PaddingZero();
            _fieldGroup.MarginZero();
            
            layoutBox.Add(_fieldGroup);
            Add(layoutBox);
        }
        
        public override void SetValue(object value)
        {
            _value = value;
            var isNull = value == null;
            
            if(_isEditor)
                _propertiesBtn.DisplayStyle(!isNull);
            
            if (isNull)
            {
                _typeBtn.text = _defaultType.ToString();
                
            }
            else
            {
                _typeBtn.text = value.GetType().ToString();
            }

            CreateFieldView(isNull);
        }

        public override void SetType(Type defaultType)
        {
            _defaultType = defaultType;
            _typeBtn.text = _defaultType.ToString();
        }

        private void CreateFieldView(bool isNull)
        {
            _fieldGroup.Clear();
            if (isNull)
                return;
            var fields = _value.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            
            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var group = new GroupBox();
                var label = new Label(field.Name);
                label.style.minWidth = 50;
                group.Add(label);

                var view = ReflectionRunCenter.GetMethodReturnValueView(_isEditor, field.FieldType);
                view.SetValue(field.GetValue(_value));
                view.GetReflectionScriptableObject = GetReflectionScriptableObject;
                
                group.Add(view);
                group.style.borderRightWidth = 2;
                group.style.borderRightColor = Color.yellow;
                _fieldGroup.Add(group);
            }
        }
    }
}