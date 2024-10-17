using System;
using System.Collections.Generic;

namespace ReflectionRun
{
    /// <summary>
    /// 查找扩展的类
    /// </summary>
    public static class ReflectionRunExtensionsUtil
    {
        public static Type RR_ModuleFactory;
        public static Type RR_EditorModuleFactory;
        
        public static Type RR_FieldInfoBoxPreciseTypeEditorFactory;
        public static Type RR_FieldInfoBoxBaseTypeEditorFactory;
        public static Type RR_MethodParmaFieldPreciseTypeEditorFactory;
        public static Type RR_MethodParmaFieldBaseTypeEditorFactory;
        public static Type RR_MethodReturnViewPreciseTypeEditorFactory;
        public static Type RR_MethodReturnViewBaseTypeEditorFactory;
        
        public static Type RR_FieldInfoBoxPreciseTypeFactory;
        public static Type RR_FieldInfoBoxBaseTypeFactory;
        public static Type RR_MethodParmaFieldPreciseTypeFactory;
        public static Type RR_MethodParmaFieldBaseTypeFactory;
        public static Type RR_MethodReturnViewPreciseTypeFactory;
        public static Type RR_MethodReturnViewBaseTypeFactory;

        public static void Collect()
        {
            List<Find> find = new List<Find>(14);
            InitList(find);
            var allAss = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in allAss)
            {
                var allType = assembly.GetTypes();
                foreach (var type in allType)
                {
                    for (var i = find.Count - 1; i >= 0; i--)
                    {
                        var f = find[i];
                        if (string.Equals(type.Name, f.name))
                        {
                            f.action(type);
                            find.RemoveAt(i);
                            if(find.Count<=0)
                                return;
                            break;
                        }
                    }
                }
            }
        }

        private static void InitList(List<Find> find)
        {
            find.Add(new Find() { name = "RR_ModuleFactory", action = (type) => { RR_ModuleFactory = type; } });
            find.Add(new Find()
                { name = "RR_EditorModuleFactory", action = (type) => { RR_EditorModuleFactory = type; } });
            find.Add(new Find()
            {
                name = "RR_FieldInfoBoxPreciseTypeEditorFactory",
                action = (type) => { RR_FieldInfoBoxPreciseTypeEditorFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_FieldInfoBoxBaseTypeEditorFactory",
                action = (type) => { RR_FieldInfoBoxBaseTypeEditorFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_MethodParmaFieldPreciseTypeEditorFactory",
                action = (type) => { RR_MethodParmaFieldPreciseTypeEditorFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_MethodParmaFieldBaseTypeEditorFactory",
                action = (type) => { RR_MethodParmaFieldBaseTypeEditorFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_MethodReturnViewPreciseTypeEditorFactory",
                action = (type) => { RR_MethodReturnViewPreciseTypeEditorFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_MethodReturnViewBaseTypeEditorFactory",
                action = (type) => { RR_MethodReturnViewBaseTypeEditorFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_FieldInfoBoxPreciseTypeFactory",
                action = (type) => { RR_FieldInfoBoxPreciseTypeFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_FieldInfoBoxBaseTypeFactory", action = (type) => { RR_FieldInfoBoxBaseTypeFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_MethodParmaFieldPreciseTypeFactory",
                action = (type) => { RR_MethodParmaFieldPreciseTypeFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_MethodParmaFieldBaseTypeFactory",
                action = (type) => { RR_MethodParmaFieldBaseTypeFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_MethodReturnViewPreciseTypeFactory",
                action = (type) => { RR_MethodReturnViewPreciseTypeFactory = type; }
            });
            find.Add(new Find()
            {
                name = "RR_MethodReturnViewBaseTypeFactory",
                action = (type) => { RR_MethodReturnViewBaseTypeFactory = type; }
            });
        }

        private class Find
        {
            public string name;
            public Action<Type> action;
        }
    }
}