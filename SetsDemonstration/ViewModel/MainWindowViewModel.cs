using System.Windows;
using SetsDemonstration.Model;

namespace SetsDemonstration.ViewModel {
    public class MainWindowViewModel : BaseViewModel {
        private readonly Interaction _interaction = new Interaction();

        private string _inputText;
        public string InputText {
            get => _inputText;
            set {
                _inputText = value;
                OnPropertyChanged(nameof(InputText));
            }
        }

        private string _outputText;
        public string OutputText {
            get => _outputText;
            set {
                _outputText = value;
                OnPropertyChanged(nameof(OutputText));
            }
        }

        public void Process_OnClick(object sender, RoutedEventArgs e) {
            OutputText = _interaction.InitializeSet(_inputText).ToString();
        }
    }
}