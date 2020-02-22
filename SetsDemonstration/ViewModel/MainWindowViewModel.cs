using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SetsDemonstration.Model;
using SomeSets;

namespace SetsDemonstration.ViewModel {
    public class MainWindowViewModel : BaseViewModel {
        public ObservableCollection<string> SetsType { get; }

        public ObservableCollection<string> SetsOperations { get; }

        public ObservableCollection<MySetBaseWrapper> MySetsWrapper { get; }

        private string _mySetSize;

        public string MySetSize {
            get => _mySetSize;
            set {
                _mySetSize = value;
                OnPropertyChanged(nameof(MySetSize));
            }
        }

        private MySetBaseWrapper _selectedMySetWrapper;

        public MySetBaseWrapper SelectedMySetWrapper {
            get => _selectedMySetWrapper;
            set {
                _selectedMySetWrapper = value;
                OnPropertyChanged(nameof(SelectedMySetWrapper));
            }
        }

        private MySetBaseWrapper _leftMySetForCompute;

        public MySetBaseWrapper LeftMySetForCompute {
            get => _leftMySetForCompute;
            set {
                _leftMySetForCompute = value;
                OnPropertyChanged(nameof(LeftMySetForCompute));
            }
        }

        private MySetBaseWrapper _rightMySetForCompute;

        public MySetBaseWrapper RightMySetForCompute {
            get => _rightMySetForCompute;
            set {
                _rightMySetForCompute = value;
                OnPropertyChanged(nameof(RightMySetForCompute));
            }
        }

        public MySetBase ResultOfComputeMySet { get; set; }

        private readonly Dictionary<int, Brush> _checkingResultBrushes = new Dictionary<int, Brush> {
            {0, Brushes.Red},
            {1, Brushes.Yellow},
            {2, Brushes.Green}
        };

        private Brush _checkingResultBrush;

        public Brush CheckingResultBrush {
            get => _checkingResultBrush;
            set {
                _checkingResultBrush = value;
                OnPropertyChanged(nameof(CheckingResultBrush));
            }
        }

        private Visibility _checkingResultVisibility;

        public Visibility CheckingResultVisibility {
            get => _checkingResultVisibility;
            set {
                _checkingResultVisibility = value;
                OnPropertyChanged(nameof(CheckingResultVisibility));
            }
        }

        public MainWindowViewModel() {
            SetsType = new ObservableCollection<string> {
                "Логическое множество",
                "Битовое множество",
                "Мультимножество"
            };

            SetsOperations = new ObservableCollection<string> {
                "+",
                "*"
            };

            MySetsWrapper = new ObservableCollection<MySetBaseWrapper>();

            AddNewSetCommand = new DelegateCommand((sType) => {
                if (ulong.TryParse(MySetSize, out ulong result))
                    Interaction.AddNewSet((string) sType, result, MySetsWrapper);
            }, o => MySetSize != null && Regex.IsMatch(MySetSize, @"^[\d]{1,7}$"));

            AddValueInSetCommand = new DelegateCommand(AddValueInSet, CanInteractWithSet);

            RemoveValueFromSetCommand = new DelegateCommand(RemoveValueFromSet, CanInteractWithSet);

            ExistsValueInSetCommand = new DelegateCommand(ExistsValueInSet, CanInteractWithSet);

            ExistsValueInSetButton_LostFocusCommand = new DelegateCommand(o => {
                CheckingResultVisibility = Visibility.Hidden;
            });

            ComputeOperationCommand = new DelegateCommand(ComputeOperation, CanComputeOperation);
        }

        public ICommand AddNewSetCommand { get; }

        public ICommand AddValueInSetCommand { get; }

        private void AddValueInSet(object text) {
            try {
                Interaction.AddValueInSet(ClearInput((string) text), SelectedMySetWrapper.MySet);
                OnPropertyChanged(nameof(SelectedMySetWrapper));
            } catch (IndexOutOfMySetRangeException e) {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand RemoveValueFromSetCommand { get; }

        private void RemoveValueFromSet(object text) {
            try {
                Interaction.RemoveValueFromSet(ClearInput((string) text), SelectedMySetWrapper.MySet);
                OnPropertyChanged(nameof(SelectedMySetWrapper));
            } catch (IndexOutOfMySetRangeException e) {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand ExistsValueInSetCommand { get; }

        private void ExistsValueInSet(object text) {
            try {
                Interaction.ExistsValueInSet(ClearInput((string) text), SelectedMySetWrapper.MySet, out int result);
                CheckingResultBrush = _checkingResultBrushes[result];
                CheckingResultVisibility = Visibility.Visible;
            } catch (IndexOutOfMySetRangeException e) {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand ExistsValueInSetButton_LostFocusCommand { get; }

        private bool CanInteractWithSet(object text) {
            return SelectedMySetWrapper != null && IsInputCorrect((string) text);
        }

        public ICommand ComputeOperationCommand { get; }

        private void ComputeOperation(object operation) {
            Interaction.ComputeOperation((string) operation, LeftMySetForCompute.MySet, RightMySetForCompute.MySet,
                                          out MySetBase resultSet);
            ResultOfComputeMySet = resultSet;
            OnPropertyChanged(nameof(ResultOfComputeMySet));
        }

        private bool CanComputeOperation(object operation) {
            return LeftMySetForCompute != null && RightMySetForCompute != null && operation != null &&
                   LeftMySetForCompute.MySet.GetType() == RightMySetForCompute.MySet.GetType();
        }

        private static string ClearInput(string text) {
            return Regex.Replace(text.Trim(), @"\s+", " ");
        }

        private static bool IsInputCorrect(string text) {
            return Regex.IsMatch(text, @"^[\d\s]+$");
        }
    }
}