namespace SettingsUtils.SharedItems.ConfigurationEntities
{
    public class CustomModulesConfigurationEntity
    {
        public string CustomModulesFolder { get; set; }
        public bool LoadCustomModules { get; set; }
        public int ModuleLoadTimeout { get; set; }
        public int ModuleStopTimeout { get; set; }
    }
}