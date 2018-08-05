// // 完整写法
// let myAdd: (baseVal: number, increment: number) => number = function (x: number, y: number): number {
//     return x + y;
// }

// // 简化写法
// let myAdd2= function (x:number,y:number):number {
//     return x + y;
// }

// // 类型推断
// let myAdd3: (baseVal: number, increment: number) => number = function (x, y) {
//     return x + y;
// }

let deck = {
    suits: ["hearts", "clubs","fff","ffdc"],
    cards: Array(52),
    createCardPicker: function () {
        let that = this; // 改进办法1
        return function () {
            let pickedCard = Math.floor(Math.random() * 52);
            let pickedSuit = Math.floor(pickedCard / 13);
            console.log(pickedCard);
            console.log(pickedSuit);
            console.log(that.suits[pickedSuit]); // 无法直接读取到最外层的this
            return { suit: that.suits[pickedSuit], card: pickedCard % 13 };
        }
    }
}

// 改进办法2：使用箭头函数
let deck2 = {
    suits: ["hearts", "clubs","fff","ffdc"],
    cards: Array(52),
    createCardPicker : function () {
        return () => {
            let pickedCard = Math.floor(Math.random() * 52);
            let pickedSuit = Math.floor(pickedCard / 13);
            return { suit: this.suits[pickedSuit], card: pickedCard % 13 };
        }
    }
}

// 解决--noImplicitThis问题
interface Card{
    suit: string;
    card: number;
}
interface Deck{
    suits: string[];
    cards: number[];
    createCardPicker(this: Deck) : () => Card;
}

let deck3:Deck = {
    suits: ["hearts", "clubs","fff","ffdc"],
    cards: Array(52),
    createCardPicker : function (this:Deck) {
        return () => {
            let pickedCard = Math.floor(Math.random() * 52);
            let pickedSuit = Math.floor(pickedCard / 13);
            return { suit: this.suits[pickedSuit], card: pickedCard % 13 }; // 这里的this已经明确是Deck类型
        }
    }
}

let cardPicker = deck3.createCardPicker();
let { suit, card } = cardPicker();
// console.log(typeof suit);
console.log(`card:${card} of ${suit}`);