using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun.Editor
{
    internal class UnityObjectFieldInfoGroupBox<T> : FieldInfoGroupBox where T : Object
    {
        private ObjectField _valueField;
        protected VisualElement _applyBtn;

        public UnityObjectFieldInfoGroupBox(FieldInfo fieldInfo, object ins) : base(fieldInfo, ins, true)
        {
        }

        protected override void CreateInsView()
        {

            var objectGroupBox = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            objectGroupBox.PaddingZero();

            _valueField = GetField();
            objectGroupBox.Add(_valueField);

            _applyBtn = ReflectionRunUIUtil.GetButton("Apply", ApplyValue, _isEditor);
            objectGroupBox.Add(_applyBtn);

            _propertiesBtn = ReflectionRunUIUtil.GetButton("Properties", ShowFieldObj, _isEditor);
            objectGroupBox.Add(_propertiesBtn);


            _typeLabel = ReflectionRunUIUtil.GetButton("", ShowType, _isEditor);
            _typeLabel.style.color = ReflectionRunUIUtil.InfoGroupBoxTypeColor;

            objectGroupBox.Add(_typeLabel);

            layoutBox.Add(objectGroupBox);

        }

        protected override void UpdateValue()
        {
            Object value = null;
            if (_fieldInfo.IsStatic || _ins != null)
                value = _fieldInfo.GetValue(_ins) as Object;

            _isNull = value == null;
            _propertiesBtn.visible = !_isNull;
            _valueField.visible = value || !_fieldInfo.IsStatic;
            _applyBtn.visible = value || !_fieldInfo.IsStatic;
            _valueField.value = value;

            if (!_isNull)
            {
                _typeLabel.text = value.GetType().ToString();
            }
            else
            {
                _typeLabel.text = _fieldInfo.FieldType.ToString();
            }
        }


        protected virtual ObjectField GetField()
        {
            var field = new ObjectField();
            field.style.width = 200;
            field.objectType = _fieldInfo.FieldType;
            return field;
        }


        protected virtual void ApplyValue()
        {
            _fieldInfo.SetValue(_ins, _valueField.value);
        }

        protected override void ShowFieldObj()
        {
            var value = _fieldInfo.GetValue(_ins) as Object;

            if (value)
                EditorUtility.OpenPropertyEditor(value);
            else
                Debug.LogWarning($"{_fieldInfo.Name} value not as UnityObject!");
        }
    }

    internal class UnityObjectFieldInfoGroupBox : UnityObjectFieldInfoGroupBox<Object>
    {
        public UnityObjectFieldInfoGroupBox(FieldInfo fieldInfo, object ins) : base(fieldInfo, ins)
        {
        }
    }

    internal class GameObjectFieldInfoGroupBox : UnityObjectFieldInfoGroupBox<GameObject>
    {
        public GameObjectFieldInfoGroupBox(FieldInfo fieldInfo, object ins) : base(fieldInfo, ins)
        {
        }
    }

    internal class TransformFieldInfoGroupBox : UnityObjectFieldInfoGroupBox<Transform>
    {
        public TransformFieldInfoGroupBox(FieldInfo fieldInfo, object ins) : base(fieldInfo, ins)
        {
        }
    }

    internal class RectTransformFieldInfoGroupBox : UnityObjectFieldInfoGroupBox<RectTransform>
    {
        public RectTransformFieldInfoGroupBox(FieldInfo fieldInfo, object ins) : base(fieldInfo, ins)
        {
        }
    }
}