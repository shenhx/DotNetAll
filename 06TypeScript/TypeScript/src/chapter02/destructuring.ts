// 解构，比较新颖的名词

// ?表示参数是可选的
/*
function f(b?:number):void{
console.log(b);
}

function f1(b:number):void{
console.log(b);
}
*/

// 声明一个解构类型
type C = {a:string,b?:number};
function f({a,b=100}:C):void{
    console.log(a);
    console.log(b);
}
f({a:"100"});


