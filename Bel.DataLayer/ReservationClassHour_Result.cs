//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bel.DataLayer
{
    using System;
    
    public partial class ReservationClassHour_Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hour { get; set; }
        public Nullable<int> RefMunicipalityClassId { get; set; }
        public Nullable<int> Id1 { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string AdvisorName { get; set; }
        public Nullable<int> RefUserId { get; set; }
        public Nullable<int> RefMunicipalityClassId1 { get; set; }
        public Nullable<int> RefSchoolId { get; set; }
        public Nullable<int> RefClassHourId { get; set; }
        public string StudentsJson { get; set; }
        public Nullable<System.DateTime> ReservationDate { get; set; }
    }
}