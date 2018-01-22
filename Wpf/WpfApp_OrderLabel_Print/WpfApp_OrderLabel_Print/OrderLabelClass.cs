using KDY.Framework.MVVM;
using KDY.IP.DTO.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfApp_OrderLabel_Print
{
    /// <summary>
    /// 医嘱标签
    /// </summary>
    public class OrderLableInfo : ModelBase
    {
        private AdviceInfo _advListInfo;
        private ObservableCollection<AdviceInfo> _advListInfos;
        private bool _selected;
        private byte[] _adviceLableBytes;
        private bool _rePrint;

        /// <summary>
        /// 拆分后的医嘱主信息（包含病人信息、医嘱头信息）
        /// </summary>
        public AdviceInfo AdvListInfo
        {
            get { return _advListInfo; }
            set
            {
                SetProperty(ref _advListInfo, value);
            }
        }

        /// <summary>
        /// 拆分后的医嘱列表信息（包含药品信息）
        /// </summary>
        public ObservableCollection<AdviceInfo> AdvListInfos
        {
            get { return _advListInfos; }
            set { SetProperty(ref _advListInfos, value); }
        }

        /// <summary>
        /// 选中标记
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        /// <summary>
        /// 执行单ID
        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// 执行单ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用于生成贴瓶单的数据格式
        /// </summary>
        public byte[] AdviceLabelBytes
        {
            get { return _adviceLableBytes; }
            set { SetProperty(ref _adviceLableBytes, value); }
        }

        /// <summary>
        /// 重打标记
        /// </summary>
        public bool RePrint
        {
            get { return _rePrint; }
            set { _rePrint = value; }
        }
    }

    /// <summary>
    /// 医嘱拆分-拆分后的医嘱
    /// </summary>
    public class AdviceInfo : ModelBase
    {
        #region 构造方法
        public AdviceInfo()
        {
        }
        //默认构造结束
        #endregion

        #region 私有属性
        protected bool _isCheck;
        protected string _bedNo;
        protected string _patName;
        protected string _patSex;
        protected long _grpNo;
        protected string _itemName;
        protected string _frequenName;
        protected string _usageName;
        protected DateTime? _execTime;
        protected long _id;
        protected string _dosage;
        protected string _patAge;
        protected string _ipNo;
        protected string _seq;
        protected string _advTypeName;
        protected string _grpNoName;
        protected string _patNo;
        protected string _deptId;
        protected string _deptName;
        protected string _advBillNo;
        #endregion

        #region 公有属性
        /// <summary>
        /// 选择
        /// </summary>
        public virtual bool IsCheck
        {
            get { return _isCheck; }
            set
            {
                SetProperty(ref _isCheck, value);
            }
        }

        /// <summary>
        /// 床号
        /// </summary>
        public virtual string BedNo
        {
            get { return _bedNo; }
            set
            {
                SetProperty(ref _bedNo, value);
            }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual string PatAge
        {
            get { return _patAge; }
            set
            {
                SetProperty(ref _patAge, value);
            }
        }

        /// <summary>
        /// 住院号
        /// </summary>
        public virtual string IpNo
        {
            get { return _ipNo; }
            set
            {
                SetProperty(ref _ipNo, value);
            }
        }
        /// <summary>
        /// 医嘱序号
        /// </summary>
        public virtual string Seq
        {
            get { return _seq; }
            set
            {
                SetProperty(ref _seq, value);
            }
        }

        /// <summary>
        /// 医嘱类型
        /// </summary>
        public virtual string AdvTypeName
        {
            get { return _advTypeName; }
            set
            {
                SetProperty(ref _advTypeName, value);
            }
        }
        /// <summary>
        /// 组号
        /// </summary>
        public virtual string GrpNoName
        {
            get { return _grpNoName; }
            set
            {
                SetProperty(ref _grpNoName, value);
            }
        }
        /// <summary>
        /// 记账号
        /// </summary>
        public virtual string PatNo
        {
            get { return _patNo; }
            set
            {
                SetProperty(ref _patNo, value);
            }
        }
        /// <summary>
        /// 病区Id
        /// </summary>
        public virtual string DeptId
        {
            get { return _deptId; }
            set
            {
                SetProperty(ref _deptId, value);
            }
        }

        /// <summary>
        /// 执行科室
        /// </summary>
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                SetProperty(ref _deptName, value);
            }
        }

        /// <summary>
        /// 医嘱序号
        /// </summary>
        public virtual string AdvBillNo
        {
            get { return _advBillNo; }
            set
            {
                SetProperty(ref _advBillNo, value);
            }
        }

        /// <summary>
        /// 医嘱用量
        /// </summary>
        public virtual string Dosage
        {
            get { return _dosage; }
            set
            {
                SetProperty(ref _dosage, value);
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string PatName
        {
            get { return _patName; }
            set
            {
                SetProperty(ref _patName, value);
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual string PatSex
        {
            get { return _patSex; }
            set
            {
                SetProperty(ref _patSex, value);
            }
        }

        /// <summary>
        /// 组号
        /// </summary>
        public virtual long GrpNo
        {
            get { return _grpNo; }
            set
            {
                SetProperty(ref _grpNo, value);
            }
        }

        /// <summary>
        /// 药名
        /// </summary>
        public virtual string ItemName
        {
            get { return _itemName; }
            set
            {
                SetProperty(ref _itemName, value);
            }
        }

        /// <summary>
        /// 频次
        /// </summary>
        public virtual string FrequenName
        {
            get { return _frequenName; }
            set
            {
                SetProperty(ref _frequenName, value);
            }
        }

        /// <summary>
        /// 用法
        /// </summary>
        public virtual string UsageName
        {
            get { return _usageName; }
            set
            {
                SetProperty(ref _usageName, value);
            }
        }

        /// <summary>
        /// 执行时间
        /// </summary>
        public virtual DateTime? ExecTime
        {
            get { return _execTime; }
            set
            {
                SetProperty(ref _execTime, value);
            }
        }

        /// <summary>
        /// 执行单ID
        /// </summary>
        public virtual long Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        #endregion

        #region 辅助资料
        protected AdvTypeEnum _advType;
        /// <summary>
        /// 类型
        /// </summary>
        public virtual AdvTypeEnum AdvType
        {
            get { return _advType; }
            set
            {
                SetProperty(ref _advType, value);
            }
        }

        #endregion

    }

    /// <summary>
    /// 医嘱拆分-病人信息
    /// </summary>
    public class PatientInfo : ModelBase
    {
        #region 构造方法
        public PatientInfo()
        {
        }
        //默认构造结束
        #endregion

        #region 私有属性
        protected bool _isCheckPat;
        protected string _bedNo;
        protected string _patName;
        protected string _patNo;
        protected string _patId;
        #endregion

        #region 公有属性
        /// <summary>
        /// 复选框
        /// </summary>
        public virtual bool IsCheckPat
        {
            get { return _isCheckPat; }
            set
            {
                SetProperty(ref _isCheckPat, value);
            }
        }

        /// <summary>
        /// 记账号
        /// </summary>
        public virtual string PatNo
        {
            get { return _patNo; }
            set
            {
                SetProperty(ref _patNo, value);
            }
        }

        /// <summary>
        /// 病人Id
        /// </summary>
        public virtual string PatId
        {
            get { return _patId; }
            set
            {
                SetProperty(ref _patId, value);
            }
        }

        /// <summary>
        /// 床号
        /// </summary>
        public virtual string BedNo
        {
            get { return _bedNo; }
            set
            {
                SetProperty(ref _bedNo, value);
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string PatName
        {
            get { return _patName; }
            set
            {
                SetProperty(ref _patName, value);
            }
        }

        #endregion

    }

    /// <summary>
    /// 病区信息
    /// </summary>
    public class DepartmentInfo
    {
        public long DeptId { get; set; }
        public string DeptNumber { get; set; }
        public string DeptName { get; set; }
    }
}
