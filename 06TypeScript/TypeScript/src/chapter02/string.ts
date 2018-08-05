// 定义模板类型，使用``应用相关内容赋值给变量
{
    let myName = `shenhx`;
    let myAge = 27;
    let sentence = `hello, my name is ${myName},i come from china and my age is ${myAge} years old.`
}

// 字符串自动拆分字符串
{
    // 自我介绍
    function selfIntroduce(template: any, name: string, age: number, experience?: string) {
        console.log(template);
        console.log(name);
        console.log(age);
        if (experience) {
            console.log(experience);
        }
    }

    let myName: string = "shenhx";
    let age1: number = 10;

    selfIntroduce `hello,my name is ${myName},i am ${age1} years old,i have no experience!`;
}