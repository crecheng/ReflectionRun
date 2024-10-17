using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    internal class BaseValueFieldInfoGroupBox<T> : FieldInfoGroupBox where T : VisualElement, new ()
    {
        protected T _valueField;
        protected VisualElement _applyBtn;
        public BaseValueFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }

        protected override void CreateInsView()
        {
            _valueField = GetField();
            layoutBox.Add(_valueField);
            
            _applyBtn = ReflectionRunUIUtil.GetButton("Apply", ApplyValue,_isEditor);
            layoutBox.Add(_applyBtn);
            
            _typeLabel = ReflectionRunUIUtil.GetButton(_fieldInfo.FieldType.ToString(),ShowType,_isEditor);
            _typeLabel.style.color = ReflectionRunUIUtil.InfoGroupBoxTypeColor;
            _typeLabel.text = _fieldInfo.FieldType.ToString();
            layoutBox.Add(_typeLabel);
            
        }

        protected override void UpdateValue()
        {
            object value = null;
            if(_fieldInfo.IsStatic|| _ins!=null)
                value= _fieldInfo.GetValue(_ins);
            
            _isNull = value == null;
            _valueField.DisplayStyle(!_isNull);
            _applyBtn.DisplayStyle(!_isNull);
            if(!_isNull) 
                UpdateNotNullView(value);
        }
        

        protected virtual T GetField()
        {
            var field = new T();
            field.style.width = 100;
            return field;
        }

        protected virtual void UpdateNotNullView(object obj)
        {
            
        }

        protected virtual object GetFieldValue() => null;

        protected void ApplyValue()
        {
            _fieldInfo.SetValue(_ins, GetFieldValue());
        }
    }
    
    internal class EnumFieldInfoGroupBox : BaseValueFieldInfoGroupBox<EnumField>
    {
        public EnumFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }

        protected override EnumField GetField()
        {
            // var choices = new List<string>();
            // var res= Enum.GetValues(_fieldInfo.FieldType);
            // foreach (var re in res)
            // {
            //     choices.Add(re.ToString());
            // }
            // var field = new DropdownField(_fieldInfo.Name, choices, 0);
            var field = new EnumField();
            field.Init((Enum)Activator.CreateInstance(_fieldInfo.FieldType));
            field.style.width = 100;
            return field;
        }

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (Enum)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }

    #region 基础类型
    
    internal class BooleanFieldInfoGroupBox : BaseValueFieldInfoGroupBox<Toggle>
    {
        public BooleanFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        
        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (bool)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class StringFieldInfoGroupBox : BaseValueFieldInfoGroupBox<TextField>
    {
        public StringFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (string)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class IntegerFieldInfoGroupBox : BaseValueFieldInfoGroupBox<IntegerField>
    {
        public IntegerFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (int)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class UnsignedIntegerFieldInfoGroupBox : BaseValueFieldInfoGroupBox<UnsignedIntegerField>
    {
        public UnsignedIntegerFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        
        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (uint)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class LongFieldInfoGroupBox : BaseValueFieldInfoGroupBox<LongField>
    {
        public LongFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (long)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class UnsignedLongFieldInfoGroupBox : BaseValueFieldInfoGroupBox<UnsignedLongField>
    {
        public UnsignedLongFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (ulong)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class FloatFieldInfoGroupBox : BaseValueFieldInfoGroupBox<FloatField>
    {
        public FloatFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (float)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class DoubleFieldInfoGroupBox : BaseValueFieldInfoGroupBox<DoubleField>
    {
        public DoubleFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (double)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    #endregion

    #region Unity基础类型
    
    internal class Vector2FieldInfoGroupBox : BaseValueFieldInfoGroupBox<Vector2Field>
    {
        public Vector2FieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }

        protected override Vector2Field GetField()
        {
            var field = new Vector2Field();
            field.style.width = 150;
            field[0].style.width = 0;
            return field;
        }

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (Vector2)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class Vector3FieldInfoGroupBox : BaseValueFieldInfoGroupBox<Vector3Field>
    {
        public Vector3FieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        
        protected override Vector3Field GetField()
        {
            var field = new Vector3Field();
            field.style.width = 200;
            field[0].style.width = 0;
            return field;
        }

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (Vector3)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class Vector4FieldInfoGroupBox : BaseValueFieldInfoGroupBox<Vector4Field>
    {
        public Vector4FieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        
        protected override Vector4Field GetField()
        {
            var field = new Vector4Field();
            field.style.width = 250;
            field[0].style.width = 0;
            return field;
        }

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (Vector4)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class RectFieldInfoGroupBox : BaseValueFieldInfoGroupBox<RectField>
    {
        public RectFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        
        protected override RectField GetField()
        {
            var field = new RectField();
            field.style.width = 250;
            field[0].style.width = 0;
            return field;
        }

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (Rect)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class Vector2IntFieldInfoGroupBox : BaseValueFieldInfoGroupBox<Vector2IntField>
    {
        public Vector2IntFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        
        protected override Vector2IntField GetField()
        {
            var field = new Vector2IntField();
            field.style.width = 150;
            field[0].style.width = 0;
            return field;
        }

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (Vector2Int)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }
    
    internal class Vector3IntFieldInfoGroupBox : BaseValueFieldInfoGroupBox<Vector3IntField>
    {
        public Vector3IntFieldInfoGroupBox(FieldInfo fieldInfo, object ins, bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        
        protected override Vector3IntField GetField()
        {
            var field = new Vector3IntField();
            field.style.width = 200;
            field[0].style.width = 0;
            return field;
        }

        protected override void UpdateNotNullView(object obj)
        {
            _valueField.value = (Vector3Int)obj;
        }

        protected override object GetFieldValue()
        {
            return _valueField.value;
        }
    }

    #endregion
    

    
}