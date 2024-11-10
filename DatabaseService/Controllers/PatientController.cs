using System.Collections.ObjectModel;
using System.Diagnostics;
using DatabaseService.DataInOutObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatabaseService.Models;
using MySql.Data.MySqlClient;
using Polly;

namespace DatabaseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController: ControllerBase
    {
        private readonly MeasurementsContext _context;
        private readonly AsyncPolicy _retryInsertPolicy;

        public PatientController(MeasurementsContext context)
        {
            _context = context;
            _retryInsertPolicy = Policy.Handle<MySqlException>().WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        [HttpPost]
        public async Task<ActionResult<PatientModel>> Post(PatientIn patientIn)
        {
            var patientModel = new PatientModel
            {
                Ssn = patientIn.SSN,
                Name = patientIn.Name,
                Email = patientIn.Email,
            };
            
            await _context.Patients.AddAsync(patientModel);
            await _context.SaveChangesAsync();
            
            return patientModel;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<PatientMeasurementsOut>> Post(MeasurementIn measurementIn)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Ssn == measurementIn.Ssn);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            patient.Measurements ??= new List<MeasurementsModel>();
          
            var measurement = new MeasurementsModel{
                Date = DateTime.Now,
                Systolic = measurementIn.Systolic,
                Diastolic = measurementIn.Diastolic,
                Patient = patient
            };
            
            patient.Measurements.Add(measurement);
            
            //_context.Patients.Update(patient);
            await _retryInsertPolicy.ExecuteAsync(async () => await _context.SaveChangesAsync());
            _context.Patients.Update(patient);
            
            var patientMeasurementOut = new PatientMeasurementsOut
            {
                Ssn = patient.Ssn,
                Measurements = new List<MeasurementOut>()
            };
            foreach (var existingMeasurements in patient.Measurements)
            {
                var measurementOut = new MeasurementOut
                {
                    Date = measurement.Date,
                    Systolic = measurementIn.Systolic,
                    Diastolic = measurement.Diastolic,
                    Seen = (existingMeasurements.Seen == null) ? false : true
                };
                patientMeasurementOut.Measurements.Add(measurementOut);
            }
            return patientMeasurementOut;
        }
    
    }
}

