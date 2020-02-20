using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using SetsDemonstration.Model;
using SomeSets;

namespace SetsDemonstration.ViewModel {
    public class MainWindowViewModel : BaseViewModel {
        public ObservableCollection<string> SetsType { get; }

        public ObservableCollection<string> SetsOperations { get; }

        public ObservableCollection<MySetBaseWrapper> MySetsWrapper { get; }

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

        private readonly Dictionary<int, string> _imgResultPaths = new Dictionary<int, string> {
            {0, "../Images/DontExists.png"},
            {1, "../Images/SomeExists.png"},
            {2, "../Images/AllExists.png"}
        };

        private string _imgResultPath;

        public string ImgResultPath {
            get => _imgResultPath;
            set {
                _imgResultPath = value;
                OnPropertyChanged(nameof(ImgResultPath));
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
                Interaction.AddNewSet((string) sType, 1024, MySetsWrapper);
            });

            AddValueInSetCommand = new DelegateCommand(AddValueInSet, CanInteractWithSet);

            RemoveValueFromSetCommand = new DelegateCommand(RemoveValueFromSet, CanInteractWithSet);

            ExistsValueInSetCommand = new DelegateCommand(ExistsValueInSet, CanInteractWithSet);

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
                ImgResultPath = _imgResultPaths[result];
            } catch (IndexOutOfMySetRangeException e) {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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