# DotNetPatcher
.NET Framework 程序注入和patch

- **.NET 劫持注入原理**：通过 `.config` 配置文件劫持程序的 `AppDomainManager`，并在构造函数中监听程序集加载事件，从而在目标程序运行时动态注入代码并修改其行为。适用于以下 `.NET` 框架版本：
  - **.NET Framework 4.0 及以上版本**：通过配置文件和 `AppDomainManager` 劫持的方式适用于 `.NET Framework`。
  - **.NET Core 和 .NET 5+**：虽然 `.NET Core` 和 `.NET 5+` 的运行时机制与 `.NET Framework` 有所不同，但类似的技术仍然可以通过自定义 `AppDomainManager` 或其他运行时注入技术实现。
- **补丁原理**：基于**Harmony**。



主程序的`config`文件需要加入：

```xml
<runtime>
    <appDomainManagerType value="Patcher.Adm" />
    <appDomainManagerAssembly value="Patcher" />
</runtime>
```

例如`Demo.exe.config`：

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8"/>
    </startup>
    
    <runtime>
        <appDomainManagerType value="Patcher.Adm" />
        <appDomainManagerAssembly value="Patcher" />
     </runtime>
</configuration>
```

