using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.Load("WindowsFormsApplication1");
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if(type.GetInterface("ISrc") == typeof(ISrc))
                {
                    StringBuilder sbResult = new StringBuilder();
                    PropertyInfo[] propertyInfos = type.GetProperties();
                    ClassMapperAttribute classMapperAttr = (ClassMapperAttribute)Attribute.GetCustomAttribute(type, typeof(ClassMapperAttribute));
                    Type targetType =  classMapperAttr.TargetClassType;
                    PropertyInfo[] targetPropertyInfos = targetType.GetProperties();
                    foreach (PropertyInfo srcPropertyInfo in propertyInfos)
                    {
                        PropertyInfo targetPropertyInfo = targetPropertyInfos.FirstOrDefault(p => srcPropertyInfo.Name.Equals(p.Name));
                        if (targetPropertyInfo == null)
                        {
                            sbResult.Append("字段：").Append(srcPropertyInfo.Name).Append("=> 匹配不上").AppendLine();
                        }
                        else
                        {
                            sbResult.Append("字段：").Append(srcPropertyInfo.Name).Append("=>").Append(targetPropertyInfo.Name).AppendLine();
                        }
                    }
                    Console.WriteLine("类型：{0}匹配情况，\r\n{1}",type.Name,sbResult.ToString());   
                }
            }
            //string json = "{\"Result\":{\"ResponseStatus\":{\"ErrorCode\":500,\"IsSuccess\":false,\"Errors\":[{\"FieldName\":\"FINSTYPE\",\"Message\":\"字段“结算方式”是必填项\",\"DIndex\":0},{\"FieldName\":\"FNAME,FIDCARD_NO\",\"Message\":\"编码为“124312”的病人基本资料，姓名和证件号组合唯一性校验\",\"DIndex\":4}],\"SuccessEntitys\":[{\"Id\":4401029,\"Number\":\"312716\",\"DIndex\":1},{\"Id\":4401030,\"Number\":\"312717\",\"DIndex\":2},{\"Id\":4403537,\"Number\":\"312718\",\"DIndex\":3}],\"SuccessMessages\":[],\"MsgCode\":0},\"NeedReturnData\":[]}}";
            //var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<K3CloudSaveJsonResult>(json);
        }
    }
}
