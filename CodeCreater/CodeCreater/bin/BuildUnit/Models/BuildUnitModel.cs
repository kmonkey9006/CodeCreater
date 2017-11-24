using System;
using System.Web.Mvc;
using System.ComponentModel;
using RTSafe.Platform.Core;
using System.ComponentModel.DataAnnotations;
using RTSafe.Platform.KMvcControl.Attributes;

namespace RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmModules.Models
{
    public class BuildUnitModel : MapperModel
    {

        [DisplayName("主键ID")]
        [HiddenInput(DisplayValue=false)]
        public Guid BID { get; set; }
        [DisplayName("线路")]

        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(4000,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]

        public Guid LineID { get; set; }
        [DisplayName("标点")]

        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(4000,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]

        public Guid SegmentID { get; set; }
        [DisplayName("工点")]

        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(4000,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]

        public Guid SiteID { get; set; }
        [DisplayName("金额")]

        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(4000,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]

        public Decimal Money { get; set; }
        [DisplayName("附件")]

        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(4000,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]

        public string Enclosure { get; set; }
        [DisplayName("计取年月")]

        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(4000,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]

        public string RememberTime { get; set; }
        [DisplayName("创建人")]

        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(4000,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]

        public string CreateUser { get; set; }
        [DisplayName("创建时间")]

        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(4000,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> CreateTime { get; set; }        }
}
