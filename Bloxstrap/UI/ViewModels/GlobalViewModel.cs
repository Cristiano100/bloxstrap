﻿using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Roforge.UI.ViewModels
{
    public static class GlobalViewModel
    {
        public static ICommand OpenWebpageCommand => new RelayCommand<string>(OpenWebpage);

       

        public static bool IsNotFirstRun => !App.IsFirstRun;

        private static void OpenWebpage(string? location)
        {
            if (location is null)
                return;

            Utilities.ShellExecute(location);
        }

        
    }
}
