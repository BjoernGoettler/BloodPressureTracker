using Microsoft.EntityFrameworkCore;

namespace DatabaseService.Models;

[PrimaryKey(nameof(Ssn))]
public class PatientModel
{
    public string Ssn { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public MeasurementsModel Measurements { get; set; } = new MeasurementsModel();
}