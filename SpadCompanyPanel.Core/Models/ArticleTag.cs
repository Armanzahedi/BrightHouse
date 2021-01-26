using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpadCompanyPanel.Core.Models
{
    public class ArticleTag : IBaseEntity
    {
        public int Id { get; set; }
        [MaxLength(300)]
        public string Title { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
