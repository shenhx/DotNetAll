# 开始认识F#
~~~
- F#是一种跨越多种开发范式的强大语言。本章简要介绍F#的核心-F#编译器、工具及其在VisualStudio 11中的位置。
- 在本章中，您将创建几个简单的F#应用程序，然后我将指出用于F#开发的关键VisualStudio特性。我不想在这里过多地介绍VisualStudio，所以我鼓励你自己去探索IDE。
- 如果您已经熟悉VisualStudio，那么仍然应该浏览本章。创建和调试F#项目就像C#或VB.NET一样；然而，F#有一个独特的特性 当涉及到多文件项目时。
此外，F#还有一个名为F#Interactive的特性，它将极大地提高您的工作效率。不要错过！
~~~
> 入门Hello World
- 备注：编译器环境（C:\Program Files (x86)\Microsoft SDKs\F#\10.1\Framework\v4.0\fsc.exe）
- 新建简单的helloworld.fs文件，并且打印出Hello World
~~~
open System

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
~~~
> 第二个例子：hello world   
~~~
// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main (args : string[]) =
    if args.Length <> 2 then
        failwith "Error : Expected arguments <greeting> and <thing>"
    let greeting,thing = args.[0],args.[1]
    let timeOfDay = DateTime.Now.ToString("hh:mm tt")

    printfn "%s,%s at %s" greeting thing timeOfDay

    0 // exists and return code
~~~
> let关键字
~~~
- 这里的关键是let关键字将名称绑定到值。值得指出的是，与大多数其他编程语言不同，F#中的值是不可变的，这意味着它们不能。 初始化后更改。我们将在第3章中讨论为什么值是不变的，但现在只需说它与“函数式编程”有关就足够了。
~~~
> F# 命名风格
~~~
- F#也是区分大小写的，因此只有大小写不同的任何两个值都是不同的。
- 一个值的名称可以是字母、数字、下划线(_)和apos Troes(‘)的任意组合。但是，名称必须以字母或下划线开头。
~~~
- 注意（**这一点跟其他编程语言不同**）
~~~
- 您可以用一对勾标括住值的名称，在这种情况下，名称可以包含除制表符和新行之外的任何字符。这允许您引用公开的值和函数。 来自可能与F#关键字冲突的其他.NET语言。
- 例如：
let ``this.Isn't %A% good value Name$!@#`` = 5
~~~
> 代码块缩进风格（跟python类似）  
> .Net互操作
~~~
- .NET Framework包含各种库，从图形到数据库到Web服务，无所不包。F#可以通过直接调用任何.NET库来本机利用它。相反，任何用F#编写的代码都可以被其他.NET语言使用。这也意味着F#应用程序可以在任何支持.NET的平台上运行。
~~~
> 注释
- // 单行注释
- (* 多行注释 *)
- /// 类似C#中的XML注释说明，但是在visual studio中没有智能补全功能
> F# Interactive(交互)
~~~
- 到目前为止，您已经编写了一些F#代码并执行了它，本书的其余部分将有更多的示例。虽然您可以留下一个新项目的尾随来测试代码，但是VisualStudio提供了一个叫做F#Interactiveor或者FSI的工具。Fsi窗口不仅使阅读本书中的示例更加容易，而且还将帮助您编写应用程序。
- F#Interactive是一个称为REPL的工具，它代表读取、评估、打印、循环。它接受F#代码，编译并执行它，然后打印结果。这使您可以快速、轻松地 使用F#进行实验，无需创建新项目或构建完整的应用程序来测试代码片段的结果。
- 大多数VisualStudio配置使用Ctrl-ALT-F键盘组合启动F#交互式窗口.一旦FSI窗口可用，它将接受F#代码，直到您用;;和换行。输入的代码被编译并执行。
- FSI窗口打印引入的任何新值以及它们的类型。图1-4显示了valx：int=42，声明创建了一个int类型的值x，其值为42。如果fsi窗口计算 这是一个没有分配给值的表达式，而是将它赋值给它的名称。
~~~
> 管理F#代码文件
~~~
- 当您从F#编程开始时，您编写的大多数程序将只存在于FSI中，或者可能在单个代码文件中。然而，您的F#项目将迅速增长，并被一个跨多个文件，最终跨越多个项目。
- 当涉及到管理多个源文件的项目时，F#语言有一些独特的特性。在F#中，编译代码文件的顺序是重要的。
- 只能调用前面在代码文件中定义的函数和类，或者在使用函数或类的文件之前编译的单独代码文件中调用函数和类。如果你重新安排了源文件，您的程序可能不再构建！
- ** 在visual studio中可以借助alt+向上箭头和alt+向下箭头快速排序文件 **
~~~