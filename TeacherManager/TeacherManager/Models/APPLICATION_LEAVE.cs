﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeacherManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class APPLICATION_LEAVE
    {
        public int ID { get; set; }
        public Nullable<int> ID_TEACHER { get; set; }
        [DisplayName("Ngày bắt đầu")]
        public Nullable<System.DateTime> DATESTART { get; set; }
        [DisplayName("Lý do")]
        public string REASON { get; set; }
        [DisplayName("Tình trạng")]
        public string STATUS { get; set; }
        [DisplayName("Ngày kết thúc")]
        public Nullable<System.DateTime> DATEEND { get; set; }
        [DisplayName("Hình thức nghỉ")]
        public string TYPELEAVE { get; set; }
        public virtual TEACHER TEACHER { get; set; }
    }
}
