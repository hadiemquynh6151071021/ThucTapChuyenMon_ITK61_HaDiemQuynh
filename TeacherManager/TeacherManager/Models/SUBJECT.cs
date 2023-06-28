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
    using System.Linq;
    using System.ComponentModel;
    public partial class SUBJECT
    {
        TeacherWorkEntities db = new TeacherWorkEntities();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUBJECT()
        {
            this.ARRANGE_TIME_SLOT = new HashSet<ARRANGE_TIME_SLOT>();
            this.TEST_SCHEDULE = new HashSet<TEST_SCHEDULE>();
            this.MAKEUP_LESSON = new HashSet<MAKEUP_LESSON>();
        }
        public List<TIME_SLOT> GetTIME_SLOTs(DateTime Day)
        {
            var slots = db.SUBJECTs
                   .Join(db.ARRANGE_TIME_SLOT, subject => subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (subject, arrangeTimeSlot) => new { subject, arrangeTimeSlot })
                   .Join(db.TIME_SLOT, temp => temp.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (temp, timeSlot) => new { temp.subject, temp.arrangeTimeSlot, timeSlot })
                   .Join(db.DAYs, temp => temp.timeSlot.ID_DAY, day => day.ID, (temp, day) => new { temp.subject, temp.arrangeTimeSlot, temp.timeSlot, day })
                   .Where(temp => temp.day.NAME.ToString() == Day.DayOfWeek.ToString() && temp.subject.ID == this.ID)
                   .Select(temp => temp.timeSlot)
                   .ToList();
            return slots;
        }
        public int ID { get; set; }
        public Nullable<int> ID_TEACHER { get; set; }
        public Nullable<int> ID_CLASSROOM { get; set; }
        [DisplayName("Môn")]
        public string NAME { get; set; }
        [DisplayName("Ngày bắt đầu")]
        public Nullable<System.DateTime> START_DAY { get; set; }
        [DisplayName("Ngày kết thúc")]
        public Nullable<System.DateTime> END_DAY { get; set; }
        [DisplayName("Số tín chỉ")]
        public Nullable<int> NUMBER_OF_CREDIT { get; set; }
        public Nullable<int> ID_ROOM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ARRANGE_TIME_SLOT> ARRANGE_TIME_SLOT { get; set; }
        public virtual CLASSROOM CLASSROOM { get; set; }
        public virtual ROOM ROOM { get; set; }
        public virtual TEACHER TEACHER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TEST_SCHEDULE> TEST_SCHEDULE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAKEUP_LESSON> MAKEUP_LESSON { get; set; }
    }
}
