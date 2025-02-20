using System;
using System.Diagnostics;
using System.Reflection;
using HarmonyLib;

namespace Patcher {
    public class Adm : AppDomainManager {
        public Adm() {
            // 输出调试信息，表示劫持成功
            Debug.WriteLine("劫持进入……");

            // 监听程序集加载事件
            AppDomain.CurrentDomain.AssemblyLoad += (sender, args) => {
                Debug.WriteLine("程序集: " + args.LoadedAssembly.FullName);

                // 检查加载的程序集是否为目标程序集（DemoApp.exe）
                if (args.LoadedAssembly.FullName.Contains("Demo")) {
                    Debug.WriteLine("目标程序集已加载: " + args.LoadedAssembly.FullName);

                    // 获取目标类型（MainWindow）
                    Type mainWindowType = args.LoadedAssembly.GetType("Demo.MainWindow");
                    if (mainWindowType == null) {
                        Console.WriteLine("[Patcher] 未找到 Demo.MainWindow 类型！");
                        return;
                    }

                    Console.WriteLine("[Patcher] 成功获取目标类型：Demo.MainWindow");

                    // 初始化 Harmony
                    var harmony = new Harmony("com.test.patch");

                    // 动态创建 Harmony 补丁
                    ApplyPatch(harmony, mainWindowType, "IsReg1", typeof(PatchHelper).GetMethod("IsReg1"));
                    ApplyPatch(harmony, mainWindowType, "IsReg2", typeof(PatchHelper).GetMethod("IsReg2"));

                    Console.WriteLine("[Patcher] 所有补丁已应用！");
                }
            };
        }

        static void ApplyPatch(Harmony harmony, Type targetType, string methodName, MethodInfo patchMethod) {
            MethodInfo targetMethod = targetType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            if (targetMethod == null) {
                Console.WriteLine($"[Patcher] 未找到目标方法: {methodName}");
                return;
            }

            var harmonyMethod = new HarmonyMethod(patchMethod);
            harmony.Patch(targetMethod, prefix: harmonyMethod);

            Console.WriteLine($"[Patcher] 成功挂钩方法: {methodName}");
        }


    }

    public static class PatchHelper {

        // 方法1：直接修改方法返回 true
        public static bool IsReg1(ref bool __result) {
            __result = true;  // 修改为 true
            return false;     // 阻止原方法执行
        }

        // 方法2：替换为新的方法
        public static bool IsReg2(ref bool __result) {
            __result = RegHelper.IsReg();  // 使用新的方法
            return false;
        }
    }

    public static class RegHelper {
        public static bool IsReg() {
            return true;
        }
    }


}
