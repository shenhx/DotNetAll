enum Color {Red,Green,Black};
// get enum value
let c : Color = Color.Red;
// get enum name
{
    let name : string = Color[Color.Green];
}

enum E{
    Foo = 1,
    Bar,
    HaHa
}

function f(x: E) {
    if (x === E.Bar || x === E.Foo) {// typescript中不能同时利用!==进行判断,发生短路问题
        console.log("Error!")
    } else {
        console.log("Ok");
    }
}

f(E.HaHa);