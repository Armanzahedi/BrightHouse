using System;
using System.Collections.Generic;
using System.Text;

namespace SpadCompanyPanel.Core
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        string InsertUser { get; set; }
        DateTime? InsertDate { get; set; }
        string UpdateUser { get; set; }
        DateTime? UpdateDate { get; set; }
        bool IsDeleted { get; set; }
    }
    public abstract class BaseEntity : IBaseEntity
    {
        public abstract int Id { get; set; }
        public abstract string InsertUser { get; set; }
        public abstract DateTime? InsertDate { get; set; }
        public abstract string UpdateUser { get; set; }
        public abstract DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
