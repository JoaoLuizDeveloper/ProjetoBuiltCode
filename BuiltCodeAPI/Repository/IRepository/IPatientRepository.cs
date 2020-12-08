﻿using BuiltCodeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltCodeAPI.Repository.IRepository
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetPatients();
        Patient GetPatient(Guid patientsId);
        bool PatientExists(string name);
        bool PatientExists(Guid id);
        bool CreatePatient(Patient patient);
        bool UpdatePatient(Patient patient);
        bool DeletePatient(Patient patient);
        bool Save();
    }
}