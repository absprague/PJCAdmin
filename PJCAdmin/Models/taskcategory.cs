//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Runtime.Serialization;
namespace PJCAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    [DataContract]
    public partial class TaskCategory
    {
        public TaskCategory()
        {
            this.Tasks = new HashSet<Task>();
        }
        
        [IgnoreDataMember]
        public byte taskCategoryID { get; set; }
        [DataMember]
        public string categoryName { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
