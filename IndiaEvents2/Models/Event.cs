//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IndiaEvents2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public string EventName { get; set; }
        public string EventType { get; set; }
        public Nullable<int> EventFee { get; set; }
        public string Events { get; set; }
        public Nullable<System.DateTime> EventFromDate { get; set; }
        public Nullable<System.DateTime> EventToDate { get; set; }
        public string CollegeName { get; set; }
        public string Department { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public byte[] Poster { get; set; }
        public string Posters { get; set; }
        public string Website { get; set; }
        public int EventID { get; set; }
    }
}