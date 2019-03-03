Vue.component('vInput',{
    props:{
        value:{
            type:[String,Number],
            default:''
        }
    },
    render:function(h){
        var _this = this;
        return h('div',[
            h('span','昵称：'),
            h('input',{
                attrs:{
                    type:'text'
                },
                refs:'username',
                domProps:{
                    value:this.value
                },
                on:{
                    input:function(event){
                        _this.value = event.target.value;
                        _this.$emit('input',event.target.value);
                    }
                }
            })
        ]);
    },
    methods:{
        focus:function(){
            this.$refs.username.focus();
        }
    }
});

Vue.component('vTextarea',{
    props:{
        value:{
            type:String,
            default:''
        }
    },
    render:function(f){
        var _this = this;
        return f('div',[
            f('span','留言内容：'),
            f('textarea',{
                attrs:{
                    placeholder:'请输入留言内容'
                },
                domProps:{
                    value:this.value
                },
                refs:'message',
                on:{
                    input:function(event){
                        _this.value = event.target.value;
                        _this.$emit('input',event.target.value);
                    }
                }
            })
        ]);
    },
    methods:{
        focus:function(){
            this.$refs.message.focus();
        }
    }
});