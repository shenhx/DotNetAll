let tuple1 : [number,string];// 定义一个元组，允许包含多种类型
tuple1 = [10,"test"];
// tuple1 = ["test",10];// error
tuple1[3]="shenhx";
//tuple1[4] = false; // error 
console.log(tuple1[3]);
console.log(tuple1[2]);