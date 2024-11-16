using SharedModels;

namespace DoctorService.Interfaces;

public interface IPhysicianService
{
    DoctorOut AddDoctor(DoctorIn doctorIn);
    MeasurementOut GetMeasurement(int measurementId);
}