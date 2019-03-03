var app = new Vue({
    el:'#app',
    data:{
        username:'',
        message:'',
        list:[]
    },
    methods:{
        handleSend:function(){
            if('' === this.username){
                alert('请输入昵称');
                this.$refs.username.focus();
                return;
            }
            if('' === this.message){
                alert('请输入留言内容');
                this.$refs.message.focus();
                return;
            }
            this.list.push({
                username:this.username,
                message:this.message
            });
            this.message = '';
        },
        handleReply:function(index){
            var name = this.list[index].username;
            this.message='回复@'+name+'：';
            this.$refs.message.focus();
        }
    }
});