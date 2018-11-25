var app = new Vue({
    el:'#app',
    data:{
        text:'',
        message:'',
        pickedLanguage:'js',
        chkboxLanguage:[],
        selectedLanguage:'',
        selectedLanguage2:[],
        selectedLanguage3:'',
        options:[{
            text:'Html',
            value:'html'
        },{
            text:'Javascript',
            value:'js'
        },{
            text:'Css',
            value:'css'
        }],
        picked:false,
        value:123,
        toggle:false,
        value1:1,
        value2:2
    },
    computed:{
        
    },
    methods:{
        handleInput:function(e){
            this.message = e.target.value;
        }
    }
});