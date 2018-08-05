// Symbols是不可改变且唯一。
let sym1 = Symbol("hello");
let sym2 = Symbol("hello");
if (sym1 === sym2) {
    console.log('相等');
} else {
    console.log('不相等');
}

{
    const getClassNameSymbol = Symbol();

    class C{
        [getClassNameSymbol]() {
            return "C";
        }
    }
    
    let c = new C();
    let className = c[getClassNameSymbol];
    console.log(className);
}
