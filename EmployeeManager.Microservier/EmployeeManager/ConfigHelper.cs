namespace EmployeeManager
{
    public class ConfigHelper
    {
        private readonly IConfiguration _configuration;

        public ConfigHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetDefaultConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }
    }
}
