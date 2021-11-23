using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ColorProfileConverter.ViewModels;
using Microsoft.Win32;

namespace ColorProfileConverter.Commands
{
    class LoadImageCommand : ICommand
    {
        private MainWindowViewModel viewModel;

        public LoadImageCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {   
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png)|*.png";
            if (dialog.ShowDialog() == true)
            {
                viewModel.SourceImage = new Bitmap(dialog.FileName);
                viewModel.TargetImage = viewModel.SourceImage;
            }
        }
    }
}
