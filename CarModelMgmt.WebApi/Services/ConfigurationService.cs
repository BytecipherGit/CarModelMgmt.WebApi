namespace CarModelMgmt.WebApi.Services
{
    public class ConfigurationService
    {
        public class EncryptionSettings
        {
            public string Key { get; set; }
            public string IV { get; set; }
        }

        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EncryptionSettings GetEncryptionSettings()
        {
            var encryptionSettings = new EncryptionSettings();
            _configuration.GetSection("EncryptionSettings").Bind(encryptionSettings);
            return encryptionSettings;
        }
    }
}
