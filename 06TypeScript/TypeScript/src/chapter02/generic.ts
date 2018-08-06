
// 泛型约束
interface Lengthwise{
    length: number;
}

// 可行
function loggingIdentity<T extends Lengthwise>(args: T): T{
    console.log(args.length);
    return args;
}

// 不可行
function loggingIdentity2<T>(args: T): T{
    // console.log(args.length); //error
    return args;
}

// 可行，参数本身已经存在length属性
function loggingIdentity3<T> (args: T[]): T[]{
    console.log(args.length);
    return args;
}

// 高级：在泛型里面使用类类型
interface IAnimal{
    
}

class Dog implements IAnimal{

}

class Mouse implements IAnimal{

}

// 泛型约束
function animalFactory<A extends IAnimal>(a: new () => A): A{
    return new a();
}

let dog = animalFactory(Dog);
