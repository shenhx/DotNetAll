
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.7.2053.0
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly demo
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module demo.exe
// MVID: {35CECF14-04D6-4E7E-AE4B-94F2DFB943BD}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x06EB0000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi beforefieldinit ILLearning.Program
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint
    // Code size       50 (0x32)
    .maxstack  2
    .locals init (int64 V_0,
             string V_1,
             bool V_2)
    IL_0000:  nop
    IL_0001:  ldc.i4.3  // 将所提供的 int32 类型的值作为 int32 推送到计算堆栈上。
    IL_0002:  conv.i8 // 转换成long
    IL_0003:  stloc.0 // 出栈，赋值到a中
    IL_0004:  ldstr      "hello,kitty" // 字符串入栈
    IL_0009:  stloc.1  // 出栈，赋值给b
    IL_000a:  ldloc.0  // 	将索引 0 处的局部变量加载到计算堆栈上。
    IL_000b:  ldc.i4.5  // 将所提供的 int32 类型的值作为 int32 推送到计算堆栈上。
    IL_000c:  conv.i8
    IL_000d:  cgt
    IL_000f:  stloc.2
    IL_0010:  ldloc.2
    /*
    * Brfalse	如果 value 为 false、空引用（Visual Basic 中的 Nothing）或零，则将控制转移到目标指令。
    * Brfalse.S	如果 value 为 false、空引用或零，则将控制转移到目标指令。
    * Brtrue	如果 value 为 true、非空或非零，则将控制转移到目标指令。
    * Brtrue.S	如果 value 为 true、非空或非零，则将控制转移到目标指令（短格式）。
    */
    IL_0011:  brfalse.s  IL_0022

    IL_0013:  nop
    IL_0014:  ldstr      "hello world"
    IL_0019:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_001e:  nop
    IL_001f:  nop
    /*
    * Br	无条件地将控制转移到目标指令。
    * Br.S	无条件地将控制转移到目标指令（短格式）。
    */
    IL_0020:  br.s       IL_002b

    IL_0022:  nop
    IL_0023:  ldloc.1
    IL_0024:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_0029:  nop
    IL_002a:  nop
    IL_002b:  call       string [mscorlib]System.Console::ReadLine()
    IL_0030:  pop
    IL_0031:  ret
  } // end of method Program::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  nop
    IL_0007:  ret
  } // end of method Program::.ctor

} // end of class ILLearning.Program


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file C:\Users\87953\Desktop\Demos\demo2.res
