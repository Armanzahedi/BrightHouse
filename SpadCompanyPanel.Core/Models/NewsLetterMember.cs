﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class NewsLetterMember : IBaseEntity
    {
        public int Id {get; set;}
        [Required(ErrorMessage = "لطفا ایمیل خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string Email { get; set; }
        public string InsertUser {get; set;}
        public DateTime? InsertDate {get; set;}
        public string UpdateUser {get; set;}
        public DateTime? UpdateDate {get; set;}
        public bool IsDeleted {get; set;}
    }
}
