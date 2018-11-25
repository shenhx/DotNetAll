var app = new Vue({
    el:'#app',
    data:{
        isAllChecked:true, // 全选
        list:[{
            checked:true,
            id:1,
            name:'iphone 8',
            price:5600,
            count:1
        },{
            checked:true,
            id:2,
            name:'iphone 8 plus',
            price:6600,
            count:2
        },{
            checked:true,
            id:3,
            name:'ipad 2',
            price:2600,
            count:1
        }]
    },
    computed:{
        totalPrice:function(){
            var total = 0;
            for(var i = 0;i< this.list.length;i++){
                var item = this.list[i];
                if(!item.checked)
                    continue;
                total += item.price * item.count;
            }
            // 千分位分隔
            return total.toString().replace(/\B(?=(\d{3})+$)/g,',');
        }
    },
    methods:{
        handleAdd:function(index){
            this.list[index].count++;
        },
        handleRemove:function(index){
            if(this.list[index].count <= 1){
                return;
            }
            this.list[index].count--;
        },
        handleRemove:function(index){
            this.list.splice(index,1);
        },
        handleSelectAll : function(){
            this.checkAll = !this.checkAll;
            for(var item of this.list){
                item.checked = this.checkAll;
            }
        },
        handleCheckChange:function(index){
            this.list[index].checked = !this.list[index].checked;
        }
    }
});