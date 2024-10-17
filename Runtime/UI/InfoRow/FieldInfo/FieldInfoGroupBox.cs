using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    /// <summary>
    /// FieldInfo显示
    /// </summary>
    public class FieldInfoGroupBox: InfoBaseGroupBox
    {
        protected FieldInfo _fieldInfo;
        protected ReflectionScriptableObject _scriptable;
        
        protected VisualElement _propertiesBtn;
        protected Button _typeLabel;
        protected Label _valueStringLabel;
        protected bool _isNull;

        public FieldInfoGroupBox(FieldInfo fieldInfo, object ins,bool isEditor) : base(ins,isEditor)
        {
            _fieldInfo = fieldInfo;
            infoName = _fieldInfo.Name.ToLower();
            CreateInfoNameView();
        }
        

        private void CreateInfoNameView()
        {
            var hLayout=ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            hLayout.style.width = 300;
            hLayout.style.minWidth = 300;
            var declaringType = _fieldInfo.DeclaringType;
            
            CreateDeclaringTypeView(hLayout, declaringType);
            
            var n = new Label(_fieldInfo.Name);
            n.style.color = ReflectionRunUIUtil.FieldColor;
            n.style.unityFontStyleAndWeight = FontStyle.Bold;
            hLayout.Add(n);
            layoutBox.Add(hLayout);
            
            CreateInsView();
        }

        protected virtual void CreateInsView()
        {
            
            var groupBox = ReflectionRunUIUtil.GetNoPaddingGroupBox();
            groupBox.MarginZero();
            _typeLabel = ReflectionRunUIUtil.GetButton("",ShowType,_isEditor);
            _typeLabel.style.color = ReflectionRunUIUtil.InfoGroupBoxTypeColor;
            groupBox.Add(_typeLabel);
            
            var hLayout = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            hLayout.PaddingZero();
            
            if(_isEditor)
            {
                _propertiesBtn = ReflectionRunUIUtil.GetButton("Properties", ShowFieldObj, _isEditor);
                hLayout.Add(_propertiesBtn);
            }
            
            _valueStringLabel = new Label("");
            hLayout.Add(_valueStringLabel);
            
            groupBox.Add(hLayout);
            
            layoutBox.Add(groupBox);
        }

        public override void Update()
        {
            UpdateValue();
        }

        protected virtual void UpdateValue()
        {
            object value = null;
            if(_fieldInfo.IsStatic|| _ins!=null)
                value= _fieldInfo.GetValue(_ins);
            
            _isNull = value == null;
            if(_isEditor)
                _propertiesBtn.DisplayStyle(!_isNull);
            
            if(!_isNull)
            {
                if (!_scriptable)
                    _scriptable = _window.CreateViewScriptableObject<ReflectionScriptableObject>();
                _scriptable.Obj = value;
                _typeLabel.text = value.GetType().ToString();
                _valueStringLabel.text = value.ToString();
            }
            else
            {
                _typeLabel.text = _fieldInfo.FieldType.ToString();
                _valueStringLabel.text = "Null";
            }
            
        }

        protected virtual void ShowType()
        {
            if (_isNull)
            {
                ShowFieldType();
            }
            else
            {
                ShowFieldObjType();
            }
        }

        protected virtual void ShowFieldObjType()
        {
            ReflectionRunCenter.CreateClassWindow(_isEditor, _fieldInfo.GetValue(_ins));
        }
        
        protected virtual void ShowFieldType()
        {
            ReflectionRunCenter.CreateClassWindow(_isEditor,_fieldInfo.FieldType);
        }
        
        protected virtual void ShowFieldObj()
        {
            if (_scriptable)
                ReflectionRunCenter.OpenPropertyEditor(_isEditor, _scriptable);
        }
    }

    internal class ListFieldInfoGroupBox : FieldInfoGroupBox
    {
        protected Label _listTypeLabel;
        public ListFieldInfoGroupBox(FieldInfo fieldInfo, object ins,bool isEditor) : base(fieldInfo, ins,isEditor)
        {
        }
        
        protected override void CreateInsView()
        {
            
            var groupBox = ReflectionRunUIUtil.GetNoPaddingGroupBox();
            groupBox.MarginZero();
            _typeLabel = ReflectionRunUIUtil.GetButton("",ShowType,_isEditor);
            _typeLabel.style.color = ReflectionRunUIUtil.InfoGroupBoxTypeColor;
            groupBox.Add(_typeLabel);
            
            var hLayout = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            hLayout.PaddingZero();
            
            if(_isEditor)
            {
                _propertiesBtn = ReflectionRunUIUtil.GetButton("Properties", ShowFieldObj, _isEditor);
                hLayout.Add(_propertiesBtn);
            }
            
            hLayout.Add(ReflectionRunUIUtil.GetButton("List", ShowList,_isEditor));
            
            _valueStringLabel = new Label("");
            hLayout.Add(_valueStringLabel);

            _listTypeLabel = new Label("");
            hLayout.Add(_listTypeLabel);
            
            groupBox.Add(hLayout);
            
            layoutBox.Add(groupBox);
        }

        protected IList _list;
        
        protected override void UpdateValue()
        {
            object value = null;
            if(_fieldInfo.IsStatic|| _ins!=null)
                value= _fieldInfo.GetValue(_ins);
            
            _isNull = value == null;
            if(_isEditor)
                _propertiesBtn.DisplayStyle(!_isNull);
            
            if(!_isNull)
            {
                if (!_scriptable)
                    _scriptable = _window.CreateViewScriptableObject<ReflectionScriptableObject>();
                _list = (IList)value;
                _scriptable.Obj = value;
                var type = value.GetType();
                _typeLabel.text = type.ToString();
                _valueStringLabel.text = $"Count: <color=#0a0>{_list.Count}</color>";
                if (value.GetType().IsArray)
                {
                    _listTypeLabel.text =$" type: {type.ToString()}" ;
                }
                else
                {
                    _listTypeLabel.text =$" type: {type.GenericTypeArguments[0]}" ;
                }
            }
            else
            {
                _typeLabel.text = _fieldInfo.FieldType.ToString();
                _valueStringLabel.text = "Null";
            }
            
        }

        protected virtual void ShowList()
        {
            if (_list == null)
            {
                Debug.LogError("list is null");
                return;
            }

            ReflectionRunCenter.CreateListViewWindow(_isEditor, _list);
        }
    }
}