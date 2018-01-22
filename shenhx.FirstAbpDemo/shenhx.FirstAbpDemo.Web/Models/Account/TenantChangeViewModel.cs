using Abp.AutoMapper;
using shenhx.FirstAbpDemo.Sessions.Dto;

namespace shenhx.FirstAbpDemo.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}