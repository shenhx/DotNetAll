Vue.component('tabs',{
    template:'\
    <div class="tabs">\
        <div class="tabs-bar">\
            <div \
                :class="tabCls(item)"\
                v-for="(item,index) in navList"\
                @click="handleChange(index)">\
                {{item.label}}\
            </div>\
        </div>\
        <div class="tabs-content">\
            <slot></slot>\
        </div>\
    </div>',
    props:{
        value:{
            type:[String,Number]
        }
    },
    data:function(){
        return {
            navList:[],
            currentValue:this.value
        }
    },
    methods:{
        tabCls:function(item){
            return ['tabs-tab',{
                'tabs-tab-active':item.name === this.currentValue
            }];
        },
        handleChange:function(index){
            var nav = this.navList[index];
            var name = nav.name;
            this.currentValue = name;
            this.$emit('input',name);
            this.$emit('on-click',name);
        },
        getTabs(){
            return this.$children.filter(function(item){
                return item.$options.name === 'pane';
            });
        },
        updateNav(){
            this.navList = [];
            var that = this;
            this.getTabs().forEach((pane,index) => {
                that.navList.push({
                    label:pane.label,
                    name:pane.name||index
                });
                if(!pane.name)pane.name = index;
                if(index ===0){
                    if(!that.currentValue){
                        that.currentValue = pane.name || index;
                    }
                }
            });
            this.updateStatus();
        },
        updateStatus(){
            var tabs = this.getTabs();
            var that = this;
            tabs.forEach((tab) => {
                return tab.show = tab.name === that.currentValue;
            });
        }
    },
    watch:{
        value:function(val){
            return this.currentValue = val;
        },
        currentValue:function(){
            this.updateStatus();
        }
    }
})