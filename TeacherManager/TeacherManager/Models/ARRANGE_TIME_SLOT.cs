//------------------------------------------------------------------------------
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
    public partial class ARRANGE_TIME_SLOT
    {
        public int ID { get; set; }

        public Nullable<int> ID_SUBJECT { get; set; }
        public Nullable<int> ID_TIME_SLOT { get; set; }
    
        public virtual TIME_SLOT TIME_SLOT { get; set; }
        public virtual SUBJECT SUBJECT { get; set; }
    }
}
