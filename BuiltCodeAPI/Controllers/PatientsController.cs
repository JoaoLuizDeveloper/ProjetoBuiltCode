﻿using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuiltCodeAPI.Models;
using BuiltCodeAPI.Repository.IRepository;
using System;

namespace BuiltCodeAPI.Controllers
{
    [Route("api/v{version:apiversion}/patients")]
    [ApiVersion("1.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PatientsController : ControllerBase
    {
        #region Construtor/Injection
        private readonly IPatientRepository _patients;
        private readonly IMapper _mapper;

        public PatientsController(IPatientRepository patients, IMapper mapper)
        {
            _patients = patients;
            _mapper = mapper;
        }
        #endregion

        #region Get List of Patients
        /// <summary>
        /// Get List of Patients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<Patient>))]
        public IActionResult GetPatients()
        {
            var objList = _patients.GetPatients();
            var objDTo = new List<Patient>();

            foreach (var obj in objList)
            {
                objDTo.Add(_mapper.Map<Patient>(obj));
            }

            return Ok(objDTo);
        }
        #endregion

        #region Get Individual Patient
        /// <summary>
        /// Get Individual Patient
        /// </summary>
        /// <param name="id">The id of the Patient</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetPatient")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetPatient(Guid id)
        {
            var obj = _patients.GetPatient(id);
            if (obj == null)
            {
                return NotFound();
            }

            var objDTO = _mapper.Map<Patient>(obj);
            return Ok(objDTO);
        }
        #endregion

        #region Criar, Atualizar e Deletar Marca
        /// <summary>
        /// CriarMarca
        /// </summary>
        /// <param name="patients">Criação de Marcas</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Patient))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CriarPatient([FromBody] Patient patients)
        {
            if (patients == null)
            {
                return BadRequest(ModelState);
            }

            if(_patients.PatientExists(patients.Name))
            {
                ModelState.AddModelError("", "The Patient already Exist");
                return StatusCode(404, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patientObj = _mapper.Map<Patient>(patients);

            if (!_patients.CreatePatient(patientObj))
            {
                ModelState.AddModelError("", $"Something went wrong when you trying to save {patientObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetMarca", new { version=HttpContext.GetRequestedApiVersion().ToString(), id= patientObj.Id }, patientObj);
        }

        /// <summary>
        /// Update Patient
        /// </summary>
        /// <param name="id, patientsDto">The Patient</param>
        /// <returns></returns>
        [HttpPatch("{id:int}", Name = "UpdatePatient")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePatient(Guid id, [FromBody] Patient patientsDto)
        {
            if (patientsDto == null || id != patientsDto.Id)
            {
                return BadRequest(ModelState);
            }

            var patientObj = _mapper.Map<Patient>(patientsDto);

            if (!_patients.UpdatePatient(patientObj))
            {
                ModelState.AddModelError("", $"Something went wrong when you trying to updating {patientObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete Patient
        /// </summary>
        /// <param name="id">The Doctor</param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "DeletePatient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePatient(Guid id)
        {
            if (!_patients.PatientExists(id))
            {
                return NotFound();
            }

            var patientObj = _patients.GetPatient(id);

            if (!_patients.DeletePatient(patientObj))
            {
                ModelState.AddModelError("", $"Something went wrong when you trying to delete {patientObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        #endregion
    }
}