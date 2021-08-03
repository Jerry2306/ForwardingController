using System.Windows;
using System.Windows.Media;
using SharedItems.Model;

namespace ForwardingControllerGUI.Model
{
    public class ProgressBarModel : ObservableObject
    {
        public Visibility Visibility { get; set; }
        public string Header { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }

        public bool IsIntermediate { get; set; }
        public int Value { get; set; }
        public Brush Foreground { get; set; }

        public static readonly Brush MainColor = new SolidColorBrush(Color.FromRgb(126, 87, 194));
        public static readonly Brush ErrorColor = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        public ProgressBarModel() => Clear();

        public void Clear()
        {
            Visibility = Visibility.Collapsed;
            Header = string.Empty;
            Description1 = string.Empty;
            Description2 = string.Empty;

            IsIntermediate = true;
            Value = 0;
            Foreground = MainColor;
        }
    }
}