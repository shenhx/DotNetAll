using IBatisNet.DataMapper;
using IBatisNetWinformDemo.Entities;
using System;
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
    public partial class FormProblemSolutions : Form
    {
        public FormProblemSolutions()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            ISqlMapper mapper = Mapper.Instance();//得到ISqlMapper实例
            //1.使用mapper.QueryForList查询数据
            IList<SampleMainEntity> resultList = mapper.QueryForList<SampleMainEntity>("QuerySampleMainAll", null);
            dgvDataShow.DataSource = resultList;
        }

        private void btnQuery2_Click(object sender, EventArgs e)
        {
            ISqlMapper mapper = Mapper.Instance();//得到ISqlMapper实例
            //1.使用mapper.QueryForList查询数据
            IList<SampleSummaryEntity> resultList = mapper.QueryForList<SampleSummaryEntity>("DoSampleSummary", null);
            dgvDataShow.DataSource = resultList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ISqlMapper mapper = Mapper.Instance();//得到ISqlMapper实例
            //1.使用mapper.QueryForList查询数据
            IList<SampleMainEntity> resultList = mapper.QueryForList<SampleMainEntity>("QuerySampleMainWithDetail", null);
            dgvDataShow.DataSource = resultList;
        }
    }
}
