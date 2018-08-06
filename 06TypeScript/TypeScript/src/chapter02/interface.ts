

// 接口:限定参数类型，可以动态检测
interface ILabelObj{
    label?: string 
}

// 接口中索引签名
interface ILabelObj2{
    label?: string;
    [propName: string]: any;
}

function printLabel(labelObj: ILabelObj) {
    console.log(labelObj.label);
}

function printLabel2(labelObj: ILabelObj2,key:string) {
    console.log(labelObj[key]);
}

// printLabel({label2: "fffffff" }); // error
// printLabel({label2: "fffffff" } as ILabelObj) // compile ok,runtime error,undefined
printLabel2({label3:"fffff",label2:"fdfdfdfdfd"},"label2")