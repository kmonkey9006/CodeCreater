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
        [Required(ErrorMessage = "{0}不能为空")] 

        [HiddenInput(DisplayValue=false)]
        public Guid BID { get; set; }

        [DisplayName("线路")]
        [SelectList("/Inspect/GetLine?all=0",
             HttpVerbs.Post,
             DataValueField = "LineID",
             DataTextField = "Name",
             DataType = "DropDownList")]
        public Guid LineID { get; set; }

        [DisplayName("标点")]
        [SelectList("/Inspect/GetSegment?all=0",
             HttpVerbs.Post,
             CascadeFrom = "LineID",
             DataValueField = "SegmentID",
             DataTextField = "Name",
             Data = @"function(){
                        return { lineId: $(""#LineID"").val() };
                    }",
             DataType = "DropDownList")]
        public Guid SegmentID { get; set; }

        [DisplayName("工点")]
        [SelectList("/Inspect/GetSegment?all=0",
             HttpVerbs.Post,
             CascadeFrom = "SegmentID",
             DataValueField = "SiteID",
             DataTextField = "Name",
             Data = @"function(){
                        return { segmentID: $(""#SegmentID"").val() };
                    }",
             DataType = "DropDownList")]
        public Guid SiteID { get; set; }

        [DisplayName("金额")]
        public Decimal Money { get; set; }

        [DisplayName("附件")]
        [StringLength(4000, ErrorMessage = "{0}长度不可超出4000")]
        public string Enclosure { get; set; }

        [DisplayName("计取年月")]
        [StringLength(50, ErrorMessage = "{0}长度不可超出50")]
        public string RememberTime { get; set; }

        [DisplayName("创建人")]
        [StringLength(50, ErrorMessage = "{0}长度不可超出50")]
        public string CreateUser { get; set; }

        [DisplayName("创建时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> CreateTime { get; set; }
        }
}
