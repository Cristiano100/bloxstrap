﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Roforge.Integrations;
using Roforge.UI.ViewModels.ContextMenu;

namespace Roforge.UI.Elements.ContextMenu
{
    /// <summary>
    /// Interaction logic for ServerInformation.xaml
    /// </summary>
    public partial class ServerInformation
    {
        public ServerInformation(ActivityWatcher activityWatcher)
        {
            DataContext = new ServerInformationViewModel(this, activityWatcher);
            InitializeComponent();
        }
    }
}
