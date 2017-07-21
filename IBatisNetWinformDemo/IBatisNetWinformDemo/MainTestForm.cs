using IBatisNet.Common.Logging;
using IBatisNet.DataMapper;
using IBatisNetWinformDemo.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IBatisNetWinformDemo
{
    public partial class MainTestForm : Form
    {
        public MainTestForm()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                MessageBox.Show("请输入内容！");
                txtSearch.Focus();
                return;
            }
            log4net.ILog log1 = log4net.LogManager.GetLogger(this.GetType());
            log1.Debug("fdfd");
            ILog log = LogManager.GetLogger(this.GetType());
            log.Debug("查询");
            log.Error("查询");
            ISqlMapper mapper = Mapper.Instance();//得到ISqlMapper实例
            //1.使用mapper.QueryForList查询数据
            IList<SampleEntity> resultList = mapper.QueryForList<SampleEntity>("QueryByName", txtSearch.Text.Trim());
            
            //2.使用存储过程查询数据
            //ISqlMapper mapper = Mapper.Instance();//得到ISqlMapper实例
            ////ISqlMapSession _session = mapper.CreateSqlMapSession();
            //IList<SampleEntity> resultList = new List<SampleEntity>();
            //try
            //{
            //    Hashtable ht = new Hashtable();
            //    ht.Add("curTemp", null);
            //    ht.Add("returnNo", 0);
            //    ht.Add("message", "");
            //    ht.Add("xmlParam", txtSearch.Text.Trim());
            //    //SampleEntity sampleEntity = new SampleEntity() { as_xmlParm = txtSearch.Text.Trim() };
            //    mapper.QueryForList("GetSampleDataProcedure", ht, resultList); 
            //}
            //finally
            //{
            //    //if (_session != null)
            //    //    _session.CloseConnection();
            //}

            //3.改进入参不用hashtable
            //ISqlMapper mapper = Mapper.Instance();//得到ISqlMapper实例
            //ParameterEntity<SampleEntity> parameterEntity = new ParameterEntity<SampleEntity>()
            //{
            //    curTemp = null,
            //    returnNo = 0,
            //    message="",
            //    xmlParam = txtSearch.Text.Trim()
            //};
           
            //IList<SampleEntity> resultList = new List<SampleEntity>();
            //mapper.QueryForList("GetSampleDataProcedure", parameterEntity, resultList); 

            dgvDataShow.DataSource = resultList;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text.Trim()))
            {
                MessageBox.Show("请输入内容！");
                txtId.Focus();
                return;
            }
            ISqlMapper mapper = Mapper.Instance();//得到ISqlMapper实例
            SampleEntity sampleEntity = new SampleEntity { Id = txtId.Text.Trim() };
            int i = mapper.Delete("DeleteSample", sampleEntity);
            MessageBox.Show(i.ToString());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text.Trim()))
            {
                MessageBox.Show("请输入内容！");
                txtId.Focus();
                return;
            }
            ISqlMapper mapper = Mapper.Instance();//得到ISqlMapper实例
            SampleEntity sampleEntity = new SampleEntity { Id = txtId.Text.Trim(),Name=txtName.Text.Trim(),Remark=txtRemark.Text.Trim() };
            int i = mapper.Update("UpdateSample", sampleEntity);
            MessageBox.Show(i.ToString());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("请输入内容！");
                txtName.Focus();
                return;
            }
            ISqlMapper mapper = Mapper.Instance();//得到ISqlMapper实例
            SampleEntity sampleEntity = new SampleEntity {Id = Guid.NewGuid().ToString("N"),Name = txtName.Text.Trim(), Remark = txtRemark.Text.Trim() };
            mapper.Insert("InsertSample", sampleEntity);
            MessageBox.Show(sampleEntity.Id);
        }
    }
}
