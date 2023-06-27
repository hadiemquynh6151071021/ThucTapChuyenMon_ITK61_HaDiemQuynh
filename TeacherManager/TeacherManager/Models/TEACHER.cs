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
    using System.Linq;

    public partial class TEACHER
    {
        TeacherWorkEntities db = new TeacherWorkEntities();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TEACHER()
        {
            this.CLASSROOMs = new HashSet<CLASSROOM>();
            this.TEST_SCHEDULE = new HashSet<TEST_SCHEDULE>();
            this.SUBJECTs = new HashSet<SUBJECT>();
            this.APPLICATION_LEAVE = new HashSet<APPLICATION_LEAVE>();
        }
        public List<SUBJECT> GetSUBJECTs(DateTime date)
        {
            var result = db.SUBJECTs
               .Join(db.ARRANGE_TIME_SLOT, subject => subject.ID, arrangeTimeSlot => arrangeTimeSlot.ID_SUBJECT, (subject, arrangeTimeSlot) => new { subject, arrangeTimeSlot })
               .Join(db.TIME_SLOT, temp => temp.arrangeTimeSlot.ID_TIME_SLOT, timeSlot => timeSlot.ID, (temp, timeSlot) => new { temp.subject, temp.arrangeTimeSlot, timeSlot })
               .Join(db.DAYs, temp => temp.timeSlot.ID_DAY, day => day.ID, (temp, day) => new { temp.subject, temp.arrangeTimeSlot, temp.timeSlot, day })
               .Where(temp => temp.subject.ID_TEACHER == this.ID && temp.day.NAME.ToString() == date.DayOfWeek.ToString() && date > temp.subject.START_DAY && date < temp.subject.END_DAY)
               .GroupBy(temp => temp.subject.ID)
               .Select(group => group.FirstOrDefault().subject)
               .ToList();


            return result;
        }

        public List<TEST_SCHEDULE> GetExamsSUBJECTs(DateTime date)
        {
            var result = db.TEST_SCHEDULE.Where(m => m.ID_TEACHER_CHECK == this.ID && m.DATE == date).ToList();
            return result;
        }

        public string GetImgProfile(string Id_User)
        {
            TEACHER tEACHER = db.TEACHERs.Where(m => m.ID_USER == Id_User).First();
            return tEACHER.PROFILE_BACKGROUND;
        }
        public int ID { get; set; }
        public Nullable<int> ID_TEACHERTYPE { get; set; }
        public Nullable<int> ID_ACADEMIC_RANK { get; set; }
        public string NAME { get; set; }
        public string ID_USER { get; set; }
        public string PROFILE_BACKGROUND { get; set; }
        public Nullable<System.DateTime> DATE_BIRTHDAY { get; set; }
        public string POSITION { get; set; }
        public string ADDRESS { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLASSROOM> CLASSROOMs { get; set; }
        public virtual TEACHER_TYPE TEACHER_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TEST_SCHEDULE> TEST_SCHEDULE { get; set; }
        public virtual ACADEMIC_RANK ACADEMIC_RANK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUBJECT> SUBJECTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLICATION_LEAVE> APPLICATION_LEAVE { get; set; }
    }
}
