using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace ReflectionRun
{
    /// <summary>
    /// 反射信息显示窗口
    /// </summary>
    public class ReflectionClassWindow : VisualElement
    {
        private Type _type;
        private object _obj;
        
        private List<MethodInfo> _methodInfos;
        private List<PropertyInfo> _propertyInfos;
        private List<FieldInfo> _fieldInfos;
        private List<ScriptableObject> _scriptables;
        
        private GroupBox _resultBox;
        private VisualElement _baseTypeBox;
        private TextField _searchField;
        private bool _isBaseGenericType;
        private ToggleButton _isUpdate;

        #region BindingFlags
        
        private ToggleButton _tNoPublic;
        private ToggleButton _tInstance;
        private ToggleButton _tStatic;
        private ToggleButton _tDeclaredOnly;
        private ToggleButton _tGenericType;
        
        #endregion

        private ToggleButton _tMethod;
        private ToggleButton _tProperty;
        private ToggleButton _tField;

        public VisualElement rootVisualElement => this;
        
        public Action<VisualElement> OnClose;

        public bool IsEditor { get; protected set; }

        public ReflectionClassWindow(bool isEditor)
        {
            IsEditor = isEditor;
            CreateGUI();
        }
        
        private List<InfoBaseGroupBox> _viewList;
        
        private void CreateGUI()
        {
            ReflectionRunCenter.Init();
            _methodInfos = new List<MethodInfo>();
            _propertyInfos = new List<PropertyInfo>();
            _fieldInfos = new List<FieldInfo>();
            _viewList = new List<InfoBaseGroupBox>();
            _scriptables = new List<ScriptableObject>();
            
            VisualElement root = rootVisualElement;
            
            if (!IsEditor)
            {
                var btn = ReflectionRunUIUtil.GetButton("关闭", OnDestroy ,IsEditor);
                btn.style.minHeight = 50;
                btn.style.height = 50;
                rootVisualElement.Add(btn);
            }
            
            var view = new ScrollView(ScrollViewMode.Vertical);

            #region 第一行baseType

            var baseTypeBox = new GroupBox();
            baseTypeBox.PaddingZero();
            _baseTypeBox = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            baseTypeBox.Add(_baseTypeBox);
            
            root.Add(baseTypeBox);

            #endregion

            //第二行BindingFlags
            CreateBindingFlagsToggle();
            //第三行TypeFlag
            CreateTypeFlagToggle();

            #region 第四行 搜索 刷新 update
            
            var groupBox3 = new GroupBox();
            groupBox3.PaddingZero();
            var hGroup=ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            groupBox3.Add(hGroup);
            
            _searchField = new TextField();
            _searchField.RegisterValueChangedCallback(OnSearch);
            _searchField.style.width = 300;
            hGroup.Add(_searchField);
            
            hGroup.Add(ReflectionRunUIUtil.GetButton("刷新",UpdateInline,IsEditor));

            _isUpdate = new ToggleButton("update");
            hGroup.Add(_isUpdate);
            
            rootVisualElement.Add(groupBox3);
            
            #endregion
            
            _resultBox = new GroupBox("method");
            view.Add(_resultBox);
            root.Add(view);
            if (!IsEditor)
            {
                RuntimeStyle();
            }
        }

        private void RuntimeStyle()
        {
            ReflectionRunUIUtil.ButtonRuntimeStyle(_tNoPublic);
            ReflectionRunUIUtil.ButtonRuntimeStyle(_tInstance);
            ReflectionRunUIUtil.ButtonRuntimeStyle(_tStatic);
            ReflectionRunUIUtil.ButtonRuntimeStyle(_tDeclaredOnly);
            ReflectionRunUIUtil.ButtonRuntimeStyle(_tGenericType);
            ReflectionRunUIUtil.ButtonRuntimeStyle(_tProperty);
            ReflectionRunUIUtil.ButtonRuntimeStyle(_tField);
            ReflectionRunUIUtil.ButtonRuntimeStyle(_tMethod);
        }

        public void Updata()
        {
            if(_isUpdate.Value)
                UpdateInline();
        }

        private void UpdateInline()
        {
            foreach (var groupBox in _viewList)
            {
                groupBox.Update();
            }
        }

        
        private void CreateBindingFlagsToggle()
        {
            var toggleGroup=new GroupBox();
            toggleGroup.PaddingZero();

            var horizontalLayout = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            toggleGroup.Add(horizontalLayout);
            
            _tNoPublic=new ToggleButton("NoPublic",ChangeToggleFilter);
            _tInstance=new ToggleButton("Instance",ChangeToggleFilter);
            _tStatic=new ToggleButton("Static",ChangeToggleFilter);
            _tDeclaredOnly=new ToggleButton("DeclaredOnly",ChangeToggleFilter);
            _tGenericType=new ToggleButton("BaseGenericType",ChangeToggleFilter);
            
            _tNoPublic.Value = true;
            _tInstance.Value = true;
            _tStatic.Value = true;
            _tDeclaredOnly.Value = true;
            _tGenericType.Value = true;
            
            horizontalLayout.Add(_tNoPublic);
            horizontalLayout.Add(_tInstance);
            horizontalLayout.Add(_tStatic);
            horizontalLayout.Add(_tDeclaredOnly);
            horizontalLayout.Add(_tGenericType);
            
            rootVisualElement.Add(toggleGroup);
        }

        private void CreateTypeFlagToggle()
        {
            var typeFlagGroup = new GroupBox();
            typeFlagGroup.PaddingZero();
            
            var horizontalLayout = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            typeFlagGroup.Add(horizontalLayout);
            
            _tProperty=new ToggleButton("Property",ChangeToggleFilter);
            _tField=new ToggleButton(@"Field",ChangeToggleFilter);
            _tMethod=new ToggleButton("Method",ChangeToggleFilter);

            _tProperty.Value = true;
            _tField.Value = true;
            _tMethod.Value = true;

            // _tProperty.style.color = ReflectionRunUIUtil.PropertyColor;
            // _tField.style.color = ReflectionRunUIUtil.FieldColor;
            // _tMethod.style.color = ReflectionRunUIUtil.MethodColor;

            _tProperty.style.unityFontStyleAndWeight = FontStyle.Bold;
            _tField.style.unityFontStyleAndWeight = FontStyle.Bold;
            _tMethod.style.unityFontStyleAndWeight = FontStyle.Bold;

            horizontalLayout.Add(_tField);
            horizontalLayout.Add(_tMethod);
            horizontalLayout.Add(_tProperty);
            
            rootVisualElement.Add(typeFlagGroup);
        }
        
        public void SetType(Type type)
        {
            _type = type;
            _resultBox.Clear();
            _viewList.Clear();
            _methodInfos.Clear();
            var baseType = _type.BaseType;
            _tGenericType.DisplayStyle(false);
            if (baseType != null )
            {
                _baseTypeBox.Add(new Label("BaseType:"));
                var btn = ReflectionRunUIUtil.GetButton(baseType.ToString(), ShowBaseType,IsEditor);
                btn.style.color = ReflectionRunUIUtil.InfoGroupBoxTypeColor;
                _baseTypeBox.Add(btn);
                if (baseType.IsGenericType)
                {
                    _tGenericType.DisplayStyle(true);
                    _isBaseGenericType = true;
                }
            }
            FindTypeInfo();
            RefreshView();
        }

        private void ShowBaseType()
        {
            ReflectionRunCenter.CreateClassWindow(IsEditor, _type.BaseType);
        }

        public void SetObject(object obj)
        {
            _obj = obj;
            SetType(obj.GetType());
        }
        
        private void OnSearch(ChangeEvent<string> evt)
        {
            SearchFitter(evt.newValue);
        }

        private void SearchFitter(string fitterString)
        {
            if (string.IsNullOrEmpty(fitterString))
            {
                foreach (var groupBox in _viewList)
                {
                    groupBox.Show();
                }
                return;
            }
            
            var search = fitterString.ToLower();
            foreach (var groupBox in _viewList)
            {
                if (!groupBox.infoName.Contains(search))
                {
                    groupBox.Hide();
                }
                else
                {
                    groupBox.Show();
                }
            }
        }


        private void ChangeToggleFilter(bool value)
        {
            FindTypeInfo();
            RefreshView();
        }
        /// <summary>
        /// 查找相关方法
        /// </summary>
        private void FindTypeInfo()
        {
            _methodInfos.Clear();
            _propertyInfos.Clear();
            _fieldInfos.Clear();
            
            BindingFlags p = BindingFlags.Public |
                             (_tInstance.Value ? BindingFlags.Instance : BindingFlags.Default) |
                             (_tNoPublic.Value ? BindingFlags.NonPublic : BindingFlags.Default) |
                             (_tStatic.Value ? BindingFlags.Static : BindingFlags.Default) |
                             (_tDeclaredOnly.Value ? BindingFlags.DeclaredOnly : BindingFlags.Default);
            
            if(_tMethod.Value)
                _methodInfos.AddRange(_type.GetMethods(p));
            if(_tProperty.Value)
                _propertyInfos.AddRange(_type.GetProperties(p));
            if(_tField.Value)
                _fieldInfos.AddRange(_type.GetFields(p));
            
            if (_isBaseGenericType&& _tGenericType.Value)
            {
                if(_tMethod.Value)
                    _methodInfos.AddRange(_type.BaseType.GetMethods(p));
                if(_tProperty.Value)
                    _propertyInfos.AddRange(_type.BaseType.GetProperties(p));
                if(_tField.Value)
                    _fieldInfos.AddRange(_type.BaseType.GetFields(p));
            }
        }
        

        private void RefreshView()
        {
            _resultBox.Clear();
            _viewList.Clear();
            FieldInfoView();
            MethodInfoView();
            PropertyInfoView();
            SearchFitter(_searchField.value);
            UpdateInline();
        }

        private void PropertyInfoView()
        {
            if (!_tProperty.Value)
                return;
            for (var i = 0; i < _propertyInfos.Count; i++)
            {
                AddInfoGroupBox(ReflectionRunCenter.GetPropertyInfoRow(IsEditor, _propertyInfos[i], _obj));
            }

        }

        private void FieldInfoView()
        {
            if (!_tField.Value)
                return;

            for (var i = 0; i < _fieldInfos.Count; i++)
            {
                AddInfoGroupBox(ReflectionRunCenter.GetFieldInfoRow(IsEditor, _fieldInfos[i], _obj));
            }

        }

        private void MethodInfoView()
        {
            if (!_tMethod.Value)
                return;

            for (var i = 0; i < _methodInfos.Count; i++)
            {
                AddInfoGroupBox(ReflectionRunCenter.GetMethodInfoRow(IsEditor, _methodInfos[i], _obj));
            }
        }

        private void AddInfoGroupBox(InfoBaseGroupBox groupBox)
        {
            groupBox.SetParent(this);
            _viewList.Add(groupBox);
            _resultBox.Add(groupBox);
        }

        public T CreateViewScriptableObject<T>() where T : ScriptableObject
        {
            var obj = ScriptableObject.CreateInstance<T>();
            _scriptables.Add(obj);
            return obj as T;
        }

        
        public void OnDestroy()
        {
            _methodInfos?.Clear();
            _fieldInfos?.Clear();
            _propertyInfos?.Clear();
            _viewList?.Clear();
            foreach (var scriptable in _scriptables)
            {
                Object.DestroyImmediate(scriptable);
            }

            _scriptables?.Clear();
            _type = null;
            OnClose?.Invoke(this);
        }
    }
}