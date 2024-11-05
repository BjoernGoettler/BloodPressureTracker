namespace DatabaseService.Models;

public class MeasurementsModel
{
    public MeasurementsModel()
    {
        Patients = new List<PatientModel>();
    }
    public int MeasurementId { get; set; }
    public DateTime Date { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public DoctorModel Seen { get; set; }
    public  IList<PatientModel> Patients { get; set; }
}