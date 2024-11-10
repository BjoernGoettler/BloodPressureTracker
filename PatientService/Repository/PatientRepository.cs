using SharedModels;
using Monitoring;
using RestSharp;
using Serilog;

namespace PatientService.Repository;

public class PatientRepository
{
    private readonly RestClient _restClient;
    public PatientRepository()
    {
        var _restClient = new RestClient("http://database:8080");
    }

    public async Task<PatientOut> AddPatient(PatientIn patientIn)
    {
        MonitorService.Log.Information("Adding patient to database");
        var request = new RestRequest("/api/Patient", Method.Post).AddJsonBody(patientIn);
        var response = await _restClient.PostAsync<PatientOut>(request);
        return response;
    }
    
}