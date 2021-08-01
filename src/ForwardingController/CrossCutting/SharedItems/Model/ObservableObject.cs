using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SharedItems.Model
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Put this function in the 'set' of your property to raise PropertyChangedEvent manually
        /// </summary>
        /// <param name="source"></param>
        public void OnPropertyChanged([CallerMemberName]string source = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(source));
    }
}