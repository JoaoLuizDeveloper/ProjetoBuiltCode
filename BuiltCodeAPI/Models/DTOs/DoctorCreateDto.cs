﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static BuiltCodeAPI.Models.Doctor;

namespace BuiltCodeAPI.Models.DTOs
{
    public class DoctorCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CRM { get; set; }
        [Required]
        public string CRMUF { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}