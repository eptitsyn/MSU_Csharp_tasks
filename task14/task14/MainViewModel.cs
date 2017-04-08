﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using task14.Annotations;

namespace task14
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string filepath;
        public string Filepath {
            get { return filepath; }
            set
            {
                if (filepath != value)
                {
                    filepath = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string text { get; set; }
        private MainWindow mainWindow { get; set; }

        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MainViewModel(MainWindow window)
        {
            model = new MainModel();
            mainWindow = window;
            InitCommands();
        }

        private MainModel model { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private ICommand exitCommand { get; set; }

        /// <summary>
        /// 
        /// 
        /// </summary>
        public RelayCommand NewCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand SaveAsCommand { get; private set; }
        public RelayCommand UndoCommand { get; private set; }
        public RelayCommand RedoCommand { get; private set; }
        public RelayCommand CutCommand { get; private set; }
        public RelayCommand CopyCommand { get; private set; }
        public RelayCommand PasteCommand { get; private set; }
        //public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand SelectAllCommand { get; private set; }
        public RelayCommand DateTimeCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }

        private void InitCommands()
        {
            NewCommand = new RelayCommand(o => New());
            LoadCommand = new RelayCommand(o => Load());
            SaveCommand = new RelayCommand(o => Save());
            SaveAsCommand = new RelayCommand(o => SaveAs());
            UndoCommand = new RelayCommand(o => Undo(),o => CanUndo());
            RedoCommand = new RelayCommand(o => Redo(), o => CanRedo());
            CutCommand = new RelayCommand(o => Cut(), o => CanCut());
            CopyCommand = new RelayCommand(o => Copy(), o => CanCopy());
            PasteCommand = new RelayCommand(o => Paste());
            //DeleteCommand = new RelayCommand(o => Delete());
            SelectAllCommand= new RelayCommand(o => SelectAll());
            DateTimeCommand = new RelayCommand(o => DateTime());
            ExitCommand = new RelayCommand(o => Exit());
        }

        private void New()
        {
            model.New();
            Text = model.text;
            Filepath = model.Filepath;
        }

        private void Load()
        {
            model.Open();
            Text = model.text;
            Filepath = model.Filepath;
        }
        private void Save()
        {
            model.Save();
            Filepath = model.Filepath;
        }

        private bool CanSave()
        {
            return true;
        }

        private void SaveAs()
        {
            model.SaveAs();
            Filepath = model.Filepath;
        }
        private void Undo()
        {
            mainWindow.TextBox.Undo();
        }
        private bool CanUndo()
        {
            return mainWindow.TextBox.CanUndo;
        }
        private void Redo()
        {
            mainWindow.TextBox.Redo();
        }
        private bool CanRedo()
        {
            return mainWindow.TextBox.CanRedo;
        }
        private void Cut()
        {
            mainWindow.TextBox.Cut();
        }
        private bool CanCut()
        {
            return (mainWindow.TextBox.SelectionLength > 0);
        }
        private void Copy()
        {
            mainWindow.TextBox.Copy();;
        }
        private bool CanCopy()
        {
            return (mainWindow.TextBox.SelectionLength > 0);
        }
        private void Paste()
        {
            mainWindow.TextBox.Paste();
        }
        public bool WordWrap { get; set; }
        //private void Delete()
        //{
        //    mainWindow.TextBox.Delete();
        //}
        //private bool CanDelete()
        //{
        //    return (mainWindow.TextBox.SelectionLength > 0);
        //}
        private void SelectAll()
        {
            mainWindow.TextBox.SelectAll();
        }
        private void DateTime()
        {
            int oldcarretpos = mainWindow.TextBox.CaretIndex;
            Text = Text.Insert(mainWindow.TextBox.CaretIndex, System.DateTime.Now.ToString("HH:mm:ss yyyy-M-d"));
            mainWindow.TextBox.CaretIndex = oldcarretpos + System.DateTime.Now.ToString("HH:mm:ss yyyy-M-d").Length;//костыль, чтоб курсор в начало не упрыгивал
        }
        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private void RaisePropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChangedEventHandler h = PropertyChanged;
            if (h != null) h(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// status bar
        /// </summary>
        //private void StatusBar()
        //{
        //    StatusBarVisiblity = (StatusBarVisiblity == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
        //    MessageBox.Show(StatusBarVisiblity.ToString());
        //}

        private Visibility statusBarVisiblity { get; set; }

        public Visibility StatusBarVisiblity
        {
            get { return statusBarVisiblity; }
            set
            {
                if (statusBarVisiblity != value)
                {
                    statusBarVisiblity = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsStatusBarVisible
        {
            get { return (statusBarVisiblity == Visibility.Visible); }
            set { StatusBarVisiblity = (value) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}