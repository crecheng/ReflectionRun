## 反射工具
在项目里提供一个可以反射可视化界面，能够在并且能够在运行时查看修改值，并且支持对部分参数的方法直接调用，有Odin的情况下体验更好，推荐Odin3.3以上

### 主窗口
Tools/ReflectionRun/MainWindow

### Editor和Runtime均可以使用
相关判断在ReflectionRunCenter中
Runtime中使用需要自行使用UIDocument创建管理，
ReflectionRunCenter.CreateRuntimeMainWindow会创建一个简单的管理界面

### 使用
#### ReflectionClassWindow窗口
第一行为baseType，点击查看baseType反射信息
第二，三行为反射信息筛选
第四行为，搜索框-刷新值按钮-update刷新值

#### 通用
Properties按钮功能为在PropertiesWindow中查看该值（使用odin显示）
蓝色Type类型按钮，显示反射信息

#### 反射信息显示
fieldInfo
对于已经重写类型显示的field，支持查看，修改
没有的重写类型显示，会显示ToString

MethodInfo
对于方法的参数，如果全部被重写显示，支持直接输入参数，并且invoke，如果没有，将会打开PropertiesWindow，使用odin工具输入参数
对于有返回值的方法，结果会直接在面板上显示，对于已经重写类型显示，支持查看，没有的重写类型显示，会显示ToString

### 如何扩展
#### 扩展添加模块-在主窗口显示
1. 继承并实现 IReflectionModule
2. 创建 partial class ModuleFactory
3. 添加创建 Module方法，方法需要是 private static，并且返回 Module

#### 重写field显示
1. 继承 FieldInfoGroupBox
2. 创建 static class RR_FieldInfoBoxBaseTypeFactory(判断基类) 或则 RR_FieldInfoBoxPreciseTypeFactory(准确类型判断)
3. 在 Factory 添加创建方法，方法需要是private static，并且返回IReflectionModule
4. 方法格式固定---private static 返回值(InfoBaseGroupBox) 方法名( 重写类型，FieldInfo，object ,isEditor)

#### 重写method参数输入显示
方法同上
1. 继承 MethodParmaValueField
2. 创建 RR_MethodParmaFieldPreciseTypeFactory 或则 RR_MethodParmaFieldBaseTypeFactory
3. 添加方法
4. 方法格式固定---private static 返回值(MethodParmaValueField) 方法名( 重写类型，ParameterInfo ,isEditor)

#### 重写method返回值显示
方法同上
1. 继承 ValueView
2. 创建 RR_MethodReturnViewPreciseTypeFactory 或则 RR_MethodReturnViewBaseTypeFactory
3. 添加方法
4. 方法格式固定---private static 返回值(ValueView) 方法名( 重写类型 ,isEditor)

#### 重写类名相关

通用模块-Runtime和Editor都可以使用  
RR_ModuleFactory  

编辑器模块-仅Editor可以使用  
RR_EditorModuleFactory  

##### 以下仅Editor可以使用  

Field精准类型扩展显示
RR_FieldInfoBoxPreciseTypeEditorFactory  

Field基于基类扩展显示
RR_FieldInfoBoxBaseTypeEditorFactory  

Method参数精准类型扩展显示
RR_MethodParmaFieldPreciseTypeEditorFactory

Method参数基于基类扩展显示
RR_MethodParmaFieldBaseTypeEditorFactory

值精准类型扩扩展显示
RR_MethodReturnViewPreciseTypeEditorFactory

值基于基类扩展显示
RR_MethodReturnViewBaseTypeEditorFactory

##### 以下Runtime和Editor都可以使用

Field精准类型扩展显示
RR_FieldInfoBoxPreciseTypeFactory  

Field基于基类扩展显示
RR_FieldInfoBoxBaseTypeFactory  

Method参数精准类型扩展显示
RR_MethodParmaFieldPreciseTypeFactory  

Method参数基于基类扩展显示
RR_MethodParmaFieldBaseTypeFactory  

值精准类型扩扩展显示
RR_MethodReturnViewPreciseTypeFactory  

值基于基类扩展显示
RR_MethodReturnViewBaseTypeFactory  