﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace StandardModel
{
    public delegate void RequestData(Socket soc, _baseModel _0x01);
    public delegate void RequestDataDtu(Socket soc, byte[] _0x01,String ip,int prot);
    public class _base_manage
    {
        class modelData {
             public string Request;
            public string type;
            public bool dtu;
            public RequestData rd;
            public RequestDataDtu rd2;
        }
        List<modelData> listmode = new List<modelData>();
     
        /// <summary>
        /// 请求数据集事件
        /// </summary>
        //public event RequestData  RequestDataEvent = null; 
       


        public delegate void errorMessage(Socket soc,_baseModel _0x01, string message);

        public void AddListen(String Request, RequestData rd,String type)
        {
            modelData md = new modelData();
            md.Request = Request;
            md.rd = rd;
            md.type = type;
            md.dtu = false;
            listmode.Add(md);
        }
        public void AddListen(String Request, RequestDataDtu rd, String type,bool dtu)
        {
            modelData md = new modelData();
            md.Request = Request;
            md.rd2 = rd;
            md.type = type;
            md.dtu = dtu;
            listmode.Add(md);
        }
        public void DeleteListen(String Reques)
        {
            int count = listmode.Count;
            modelData[] mds = new modelData[count];
            listmode.CopyTo(mds);
            foreach (modelData md in mds)
            {
                listmode.Remove(md);
            }
        }
        /// <summary>
        /// 错误返回事件
        /// </summary>
        public event errorMessage errorMessageEvent = null;
        public void init(String data, Socket soc)
        {
            _baseModel _0x01= Newtonsoft.Json.JsonConvert.DeserializeObject<_baseModel>(data);
            string message = "";
            try
            {
                if (_0x01 != null && _0x01.Token != "")
                {
                    int count = listmode.Count;
                    modelData[] mds = new modelData[count];
                    listmode.CopyTo(mds);
                    foreach (modelData md in mds)
                    {
                        if (md.Request == _0x01.Request)
                        {
                            if (md.rd2 != null && md.dtu==true)
                            {

                                RequestDataDtu rdh = new RequestDataDtu(md.rd2);
                                byte[] b= Newtonsoft.Json.JsonConvert.DeserializeObject<byte[]>(_0x01.Root); 
                                rdh(soc, b, _0x01.Token.Split('|')[0],Convert.ToInt32(_0x01.Token.Split('|')[1]));
                                if (md.type == "once")
                                {
                                    listmode.Remove(md);
                                }
                            }
                            if (md.rd != null && md.dtu == false)
                            {
                               
                                //md.rd(soc, _0x01);
                                RequestData rdh = new RequestData(md.rd);
                                rdh(soc, _0x01);
                                if (md.type == "once")
                                {
                                    listmode.Remove(md); 
                                }
                            }
                        }
                    }
                                    //根据具体功能不同，代码不同
                                    //if (RequestDataEvent != null)
                                    //    RequestDataEvent(soc, _0x01,_0x01.Request); 
                 
                }
            }
            catch (Exception ex)
            {
                _0x01.Parameter = ex.Message;
                message = Newtonsoft.Json.JsonConvert.SerializeObject(_0x01);

                errorMessageEvent(soc,_0x01, ex.Message);
            }
        }
    }
}
