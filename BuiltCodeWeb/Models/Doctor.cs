﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltCodeWeb.Models
{
    public class Doctor
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CRM { get; set; }
        [Required]
        public string CRMUF { get; set; }
        public DateTime DateCreated { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
