using SharedModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatabaseService.Models;

namespace DatabaseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly MeasurementsContext _context;

        public DoctorController(MeasurementsContext context)
        {
            _context = context;
        }

        // GET: api/Doctor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorModel>>> GetDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }

        // GET: api/Doctor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorModel>> GetDoctorModel(int id)
        {
            var doctorModel = await _context.Doctors.FindAsync(id);

            if (doctorModel == null)
            {
                return NotFound();
            }

            return doctorModel;
        }

        // PUT: api/Doctor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctorModel(int id, DoctorModel doctorModel)
        {
            if (id != doctorModel.DoctorId)
            {
                return BadRequest();
            }

            _context.Entry(doctorModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Doctor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DoctorModel>> PostDoctorModel(DoctorIn doctorIn)
        {
            var doctorModel = new DoctorModel
            {
                Name = doctorIn.Name,
            };
            
            _context.Doctors.Add(doctorModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctorModel", new { id = doctorModel.DoctorId }, doctorModel);
        }

        // DELETE: api/Doctor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctorModel(int id)
        {
            var doctorModel = await _context.Doctors.FindAsync(id);
            if (doctorModel == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctorModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorModelExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}
