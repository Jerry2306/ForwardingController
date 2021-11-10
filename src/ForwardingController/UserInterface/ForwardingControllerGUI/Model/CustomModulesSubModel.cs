using CustomModule.Contract;
using SharedItems.Model;
using System.Collections.Generic;
using System.Windows.Media;

namespace ForwardingControllerGUI.Model
{
    public class CustomModulesSubModel : ObservableObject
    {
        public bool IsStartButtonEnabled { get; set; }
        public bool IsStopButtonEnabled { get; set; }
        public Brush ActiveColor { get; set; }
        public string ActiveText { get; set; }
        public List<string> Logs { get; set; }

        private Brush _activeColor = Brushes.Green;
        private Brush _inactiveColor = Brushes.Red;
        public CustomModulesSubModel(ICustomModuleController module)
        {
            var isRunning = module.IsRunning;

            IsStartButtonEnabled = !isRunning;
            IsStopButtonEnabled = isRunning;

            ActiveColor = isRunning ? _activeColor : _inactiveColor;
            ActiveText = isRunning ? "Active" : "Inactive";

            Logs = new List<string>(module.TempLog);
        }
    }
}