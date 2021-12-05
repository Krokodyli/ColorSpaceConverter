using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ColorProfileConverter.ViewModels;
using Microsoft.Win32;

namespace ColorProfileConverter.Commands
{
    class SaveImageCommand : ICommand
    {
        private MainWindowViewModel viewModel;

        public SaveImageCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.TargetImage != null;
        }

        public void Execute(object parameter)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Image files (*.png)|*.png";
            if(dialog.ShowDialog() == true)
                viewModel.TargetImage.Save(dialog.FileName);
        }
    }
}
