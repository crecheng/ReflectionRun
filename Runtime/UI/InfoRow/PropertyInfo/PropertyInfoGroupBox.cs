using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    public class PropertyInfoGroupBox : InfoBaseGroupBox
    {
        private PropertyInfo _propertyInfo;

        public PropertyInfoGroupBox(PropertyInfo propertyInfo, object ins, bool isEditor) : base(ins,isEditor)
        {
            _propertyInfo = propertyInfo;
            infoName = _propertyInfo.Name.ToLower();
            CreateInfoNameView();
        }

        private void CreateInfoNameView()
        {
            var hLayout=ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            hLayout.style.width = 300;
            hLayout.style.minWidth = 300;
            var declaringType = _propertyInfo.DeclaringType;
            
            CreateDeclaringTypeView(hLayout, declaringType);
            
            var n = new Label(_propertyInfo.Name);
            n.style.color = ReflectionRunUIUtil.PropertyColor;
            n.style.unityFontStyleAndWeight = FontStyle.Bold;
            hLayout.Add(n);
            layoutBox.Add(hLayout);
        }
    }
}