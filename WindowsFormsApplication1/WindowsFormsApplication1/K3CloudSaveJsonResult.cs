using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class K3CloudSaveJsonResult
    {
        public Result Result { get; set; }
    }

    public class Result
    {
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ResponseStatus
    {
        public string ErrorCode { get; set; }
        public bool IsSuccess { get; set; }

        public List<Error> Errors { get; set; }
        public List<SuccessEntity> SuccessEntitys { get; set; }
        public int MsgCode { get; set; }
        public List<object> SuccessMessages { get; set; }
    }

    public class Error
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
        public int DIndex { get; set; }
    }

    public class SuccessEntity
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public int DIndex { get; set; }
    }

}
