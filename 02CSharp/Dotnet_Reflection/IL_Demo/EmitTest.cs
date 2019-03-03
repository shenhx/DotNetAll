using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ILLearning
{
    class EmitTest
    {
        static void Main(string[] args)
        {
            #region Emit练习
            // specify a new assembly name 
            var assemblyName = new AssemblyName("World");
            // create assemblybuilder
            /*
            * AssemblyBuilderAccess枚举值说明：
            * AssemblyBuilderAccess.Run; 表示程序集可被执行，但不能被保存。　　
            * AssemblyBuilderAccess.Save; 表示程序集可被保存，但不能被执行。　　
            * AssemblyBuilderAccess.RunAndSave; 表示程序集可被保存并能被执行。
            * AssemblyBuilderAccess.ReflectionOnly; 表示程序集只能用于反射上下文环境中，不能被执行。　
            * AssemblyBuilderAccess.RunAndCollect; 表示程序集可以被卸载并且内存会被回收。
            * 
             */
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            // create module builder 
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("WorldModule", "HelloWorld.exe");

            // create type builder for a class Name
            var typeBuilder = moduleBuilder.DefineType("HelloWorld", TypeAttributes.Public);

            // create method buider
            var methodBuilder = typeBuilder.DefineMethod("SayHello", MethodAttributes.Public | MethodAttributes.Static, null, null);

            // get il genereator
            var il = methodBuilder.GetILGenerator();

            il.Emit(OpCodes.Ldstr, "Hello，World");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine"));
            il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ret);

            var helloworldClassType = typeBuilder.CreateType();
            assemblyBuilder.SetEntryPoint(helloworldClassType.GetMethod("SayHello"));

            assemblyBuilder.Save("HelloWorld.exe");

            #endregion
            Console.WriteLine(
                     "Hi, a Hello Emit assembly has been generated for you.");
            Console.ReadLine();
        }
    }
}