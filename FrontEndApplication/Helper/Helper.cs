namespace FrontEndApplication.Helper
{
    public class PatientAPI
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:7052/");
            return Client;
        }
    }
}
