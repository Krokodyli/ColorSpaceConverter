﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ColorProfileConverter.Models;
using ColorProfileConverter.ViewModels;

namespace ColorProfileConverter.Commands
{
    class GenerateTargetImageCommand : ICommand
    {
        private MainWindowViewModel viewModel;

        public GenerateTargetImageCommand(MainWindowViewModel viewModel)
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
            return viewModel.SourceImage != null;
        }

        public void Execute(object parameter)
        {

            var converter = new ColorProfileBitmapConverter(viewModel.SourceColorProfile,
                                                            viewModel.TargetColorProfile);
            viewModel.TargetImage = converter.Convert(viewModel.SourceImage);
        }
    }
}
