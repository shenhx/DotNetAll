/**
 * 设计思路：
 * 1. 明确需求，规划好API。
 * 2. 一个Vue组件的API只来自props、events和slots，确定好这3部分的命名、规则。
 */
var app = new Vue({
    el:'#app',
    data:{
        value:5
    }
});