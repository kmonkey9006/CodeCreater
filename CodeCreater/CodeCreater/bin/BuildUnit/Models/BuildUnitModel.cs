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
        [StringLength(16, ErrorMessage = "长度不可超出16")]
        [HiddenInput(DisplayValue=false)]
        public Guid BID { get; set; }
        [DisplayName("线路")]        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(16,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]
        [StringLength(16, ErrorMessage = "长度不可超出16")]
        [SelectList("LineID",HttpVerbs.Post,DataValueField = "/Inspect/GetLine?all=0",DataTextField = "Name",DataType = "DropDownList")]
        public Guid LineID { get; set; }
        [DisplayName("标点")]        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(16,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]
        [StringLength(16, ErrorMessage = "长度不可超出16")]
        [SelectList("/Inspect/GetSegment?all=0",HttpVerbs.Post,DataValueField = "SegmentID",DataTextField = "Name",Data =@"function(){return { lineID: $(""#LineID"").val() }; }",DataType = "DropDownList")]
        public Guid SegmentID { get; set; }
        [DisplayName("工点")]        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(16,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]
        [StringLength(16, ErrorMessage = "长度不可超出16")]
        public Guid SiteID { get; set; }
        [DisplayName("金额")]        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(18,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]
        [StringLength(18, ErrorMessage = "长度不可超出18")]
        public Decimal Money { get; set; }
        [DisplayName("附件")]        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(4000,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]
        [StringLength(4000, ErrorMessage = "长度不可超出4000")]
        [Upload(true)]
        public string Enclosure { get; set; }
        [DisplayName("计取年月")]        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(50,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]
        [StringLength(50, ErrorMessage = "长度不可超出50")]
        [RegularExpression(@"[1-9]+\d{3}\-(?:0[1-9]|1[0-2]|[1-9]{1})")]
        public string RememberTime { get; set; }
        [DisplayName("创建人")]        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(50,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]
        [StringLength(50, ErrorMessage = "长度不可超出50")]
        public string CreateUser { get; set; }
        [DisplayName("创建时间")]        [Required(ErrorMessage = "{0}不能为空")] 
        [StringLength(23,MinimumLength = 1, ErrorMessage ="{0}长度在{2}-{1}之间")]
        [StringLength(23, ErrorMessage = "长度不可超出23")]
[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{{0:yyyy-MM-dd}}")]
        public Nullable<System.DateTime> CreateTime { get; set; }        }
}
