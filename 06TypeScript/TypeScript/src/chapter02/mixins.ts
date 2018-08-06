// 使用混入mixin
class Disposable {
    isDisposed: boolean;
    dispose() {
        this.isDisposed = true;
    }
}

class Activatable {
    isActive: boolean;
    activate() {
        this.isActive = true;
    }

    deactivate() {
        this.isActive = false;
    }
}

// 注意这里用implements实现待混入的类型
class SmartObject implements Disposable, Activatable {
    constructor() {
        setInterval(() => {
            console.log(`constructor中，${this.isActive}+:${this.isDisposed}`)
        }, 500);
    }

    interact() {
        this.activate();
    }

    // 实现Disposable
    isDisposed: boolean = false;
    dispose: () => void;
    // 实现Activatable
    isActive: boolean = false;
    activate: () => void;
    deactivate: () => void;
}

applyMixins(SmartObject, [Disposable, Activatable]);

let smartObj = new SmartObject();
setInterval(() => smartObj.interact(), 1000);


// 将具体的属性和方法绑定到混入对象中
function applyMixins(derivedCtor: any, baseCtors: any[]) {
    baseCtors.forEach(baseCtor => {
        Object.getOwnPropertyNames(baseCtor.prototype).forEach(name => {
            derivedCtor.prototype[name] = baseCtor.prototype[name];
        });
    });
}