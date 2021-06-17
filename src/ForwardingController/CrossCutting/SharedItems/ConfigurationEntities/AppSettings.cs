namespace SharedItems.ConfigurationEntities
{
    public class AppSettings
    {
        public CommonConfigurationEntity CommonConfiguration { get; set; }
        public DatabaseConfigurationEntity DatabaseConfiguration { get; set; }
        public ForwardingConfigurationEntity ForwardingConfiguration { get; set; }
        public LoggingConfigurationEntity LoggingConfiguration { get; set; }
        public CustomModulesConfigurationEntity CustomModulesConfiguration { get; set; }
    }
}