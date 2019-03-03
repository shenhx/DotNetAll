Vue.component('list',{
    props:{
        list:{
            type:Array,
            defult:function(){
                return [];
            }
        }
    },
    render:function(h){
        var _this = this;
        var list = [];
        this.list.forEach(function(msg,index) {
            var node = h('div',{
                attrs:{
                    class:'list-item'
                }
            },[
                h('span',msg.username+'：'),
                h('div',{
                    attrs:{
                        class:'list-msg'
                    }
                },[
                    h('p',msg.message),
                    h('a',{
                        attrs:{
                            class:'list-reply'
                        },
                        on:{
                            click:function(){
                                _this.handleReply(index);
                            }
                        }
                    },'回复'),
                    h('a',{
                        attrs:{
                            class:'list-delete'
                        },
                        on:{
                            click:function(){
                                _this.handleDelete(index);
                            }   
                        }
                    },'删除')
                ])
            ]);
            list.push(node);
        });
        if(this.list.length){
            return h('div',{
                attrs:{
                    class:'list'
                }
            },[
                list
            ]);
        }else{
            return h('div',{
                attrs:{
                    class:'list-nothing'
                }
            },'留言列表为空');
        }
    },
    methods:{
        handleReply:function(index){
            this.$emit('reply',index);
        },
        handleDelete:function(index){
            this.list.splice(index,1);
        }
    }
});