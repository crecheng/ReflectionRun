using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    public interface IReflectionModule
    {
        public virtual bool IsOnlyEditor => false;

        /// <summary>
        /// 绘制UI
        /// </summary>
        /// <param name="rootVisualElement"></param>
        /// <param name="isEditor"></param>
        public void CreateModelVisualElement(VisualElement rootVisualElement,bool isEditor);

        /// <summary>
        /// 选择对于类型时
        /// </summary>
        /// <param name="type"></param>
        public void SwitchType(Type type);

        /// <summary>
        /// 实例化试调用
        /// </summary>
        /// <param name="window"></param>
        public void Init(ReflectionRunMainWindow window);
        
        

        /// <summary>
        /// 类型筛选
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool FilterType(Type type)
        {
            return true;
        }
    }
}