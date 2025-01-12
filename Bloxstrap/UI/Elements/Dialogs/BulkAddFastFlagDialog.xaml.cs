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

namespace Roforge.UI.Elements.Dialogs
{
    /// <summary>
    /// Interaction logic for BulkAddFastFlagDialog.xaml
    /// </summary>
    public partial class BulkAddFastFlagDialog
    {
        public MessageBoxResult Result = MessageBoxResult.Cancel;

        public BulkAddFastFlagDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Select a JSON file"
            };

            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            JsonTextBox.Text = System.IO.File.ReadAllText(dialog.FileName);
        }
    }
}
