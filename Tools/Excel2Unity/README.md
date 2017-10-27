# Excel2Unity

* 目标功能

    * 导出txt文本文件以及生成各个语言的解析版本(针对客户端和服务器)
    
* 目前功能 
    * 导出txt文本文件并生成相应的解析代码到unity(客户端)
   
* 安装须知

    * 1, 源代码使用的python3，需要安装python3安装包
    
    * 2, 导入xlrd包
        
    * 3, IDE使用的`pycharm`, 推荐使用`pycharm`开发python
   
* 代码修改

    打开`Config.py`文件, 修改几个配置属性
    
    ```python
    # Unity输出根目录
    UNITY_TABLE_ROOT_DIR = "./TestTable/Assets/"
    
    # Unity数据目录
    UNITY_TABLE_DATA_DIR = "GameData/TableData/"
    
    # Unity代码目录
    UNITY_TABLE_CODE_DIR = "Scripts/Table/"   
    ```
    
    有一个配置行暂时不需要修改, 目前配置文件都默认生成在Resources文件夹下
    ， 通过Resources.Load()加载，资源动态加载还没有完成
    ```python
    # Unity使用资源路径读取
    UINTY_TABLE_USE_RESOURCE_PATH_READ = True
    ```
    
*   Excel配置使用
    
    ![](https://raw.githubusercontent.com/xieliujian/Excel2Unity/master/Snapshots/QQ%E6%88%AA%E5%9B%BE20170928092831.bmp)
    
    * Excel表头有5行
    
    * 第一行为字段注释，在生成代码中显示
    
    * 第二行有三个字段可以选择 `C, S, CS` , 分别用来代码这个字段是客户端
    所有，服务器所有，还是客户端服务器共有
    
    * 第三行是字段的类型, 目前有 `Int, float, string, List[int], List[float], 
     List[string], Map[int|int], Map[int|float], Map[int|string], 
     Map[string|int], Map[string|float], Map[string|string]`这些类型可供选择,
     应该是涵盖了常用的类型，字段类型后期可以增加
     
    * 第四行填写字段名
    
    * 第五行是指字段是否为key，这个表格工具支持多个字段`key`和没有`key`
        
        * 没有`key`的情况下，生成的表格的数据管理类的数据使用`List`管理
        
        * 有一个`key`的情况下，生成的表格的数据管理类的数据使用`Dictionary`管理
        
        * 多个`key`的情况下，也是使用`Dictionary`来管理，但是获取数据的函数会有不同
        可能会有多个形参
    
    
    