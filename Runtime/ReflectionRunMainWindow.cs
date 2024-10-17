using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    /// <summary>
    /// 主窗口，显示各种模块
    /// </summary>
    public class ReflectionRunMainWindow : VisualElement
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;
        
        protected List<Type> _typeList = new List<Type>();
        protected GroupBox _resultBox;
        protected VisualElement _pageBox;
        protected Label _pageLabel;
        protected List<IReflectionModule> _modules;
        protected IReflectionModule _findActiveModules;
        public bool IsEditor { get; protected set; }


        private int _page = 0;
        private const int PageRowCount = 20;

        public ReflectionRunMainWindow(bool isEditor)
        {
            IsEditor = isEditor;
            CreateGUI();
        }

        protected VisualElement rootVisualElement => this;
        
        public void CreateGUI()
        {
            ReflectionRunCenter.Init();
            VisualElement root = this;
            
            _modules = new List<IReflectionModule>();
            
            CreateModule();
            
            _pageBox = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            root.Add(_pageBox);
            
            _pageBox.Add(ReflectionRunUIUtil.GetButton("<<",UpPage,IsEditor));
            _pageBox.Add(ReflectionRunUIUtil.GetButton(">>",NextPage,IsEditor));
            _pageLabel = new Label("0/0");
            _pageBox.Add(_pageLabel);

            _pageBox.DisplayStyle(false);

            _resultBox = new GroupBox();
            root.Add(_resultBox);

            if(m_VisualTreeAsset)
            {

            }
            
        }

        protected virtual List<MethodInfo> GetCreateMethods()
        {
            var factory = typeof(ModuleFactory);
            var all = new List<MethodInfo>();
            var modules= factory.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            all.AddRange(modules);
            all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_ModuleFactory));
            return all;
        }

        /// <summary>
        /// 创建module
        /// </summary>
        protected void CreateModule()
        {
            var all = GetCreateMethods();
            foreach (var moduleCreate in all)
            {
                var moduleGroup = new GroupBox();
                try
                {
                    var module = moduleCreate.Invoke(null, null);
                    if (module is IReflectionModule reflectionModule)
                    {
                        reflectionModule.Init(this);
                        reflectionModule.CreateModelVisualElement(moduleGroup,IsEditor);
                        _modules.Add(reflectionModule);
                    }
                    else
                    {
                        Debug.LogError($"{module.GetType().FullName} is not IReflectionModule!");
                    }
                    rootVisualElement.Add(moduleGroup);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        
        public void OnStringFind(string typeName,IReflectionModule module)
        {
            _findActiveModules = module;
            FindType(typeName);
            _resultBox.Clear();
            _page = 0;
            
            if (_typeList.Count <= 0)
            {
                _resultBox.Add(new Label("no find"));
            }
            else
            {
                var len = _typeList.Count;
                if (_typeList.Count > PageRowCount)
                {
                    _pageBox.DisplayStyle(true);
                    _pageLabel.text = $"{PageRowCount}/{_typeList.Count}";
                }
                else
                {
                    _pageBox.DisplayStyle(false);
                }
                
                RefreshResult();
            }
        }

        private void UpPage()
        {
            if (_page > 0)
                _page--;
            _pageLabel.text = $"{_page * PageRowCount + PageRowCount}/{_typeList.Count}";
            RefreshResult();
        }

        private void NextPage()
        {
            _page++;
            _pageLabel.text = $"{_page * PageRowCount + PageRowCount}/{_typeList.Count}";
            RefreshResult();
        }

        private void RefreshResult()
        {
            _resultBox.Clear();
            var len = Math.Min(_typeList.Count, (_page + 1) * PageRowCount);
            for (var i = _page * PageRowCount; i < len; i++)
            {
                var index = i;
                _resultBox.Add( ReflectionRunUIUtil.GetButton(_typeList[i].FullName, () => { OnClickResultBtn(index); },IsEditor));
            }
        }

        private void OnClickResultBtn(int index)
        {
            _findActiveModules?.SwitchType(_typeList[index]);
        }



        
        /// <summary>
        /// 查找type
        /// </summary>
        /// <param name="typeName"></param>
        private void FindType(string typeName)
        {
            typeName = typeName.ToLower();
            var allAss = AppDomain.CurrentDomain.GetAssemblies();
            _typeList.Clear();
            foreach (var assembly in allAss)
            {
                //var type = assembly.GetType(typeName, false, true);
                // if (type != null) 
                //     _typeList.Add(type);
                var allType = assembly.GetTypes();
                foreach (var type in allType)
                {
                    if (type.FullName.ToLower().Contains(typeName))
                    {
                        bool add = true;
                        if (_findActiveModules != null)
                            add &= _findActiveModules.FilterType(type);
                        if(add)
                            _typeList.Add(type);
                    }
                }
            }

            GC.Collect();
        }
        

        private void OnDestroy()
        {
            _typeList.Clear();
        }
    }
}
