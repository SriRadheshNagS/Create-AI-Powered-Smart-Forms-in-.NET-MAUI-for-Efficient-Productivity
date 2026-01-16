using Microsoft.Maui.Controls;
using System;
using Syncfusion.Maui.Core;

namespace AIDataForm
{
    public partial class DataFormDesktopUI : ContentView
    {
        public DataFormDesktopUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Tapped event of the TapGestureRecognizer control.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The tapped event args.</param>
        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var templateItem = (sender as Border)?.BindingContext as TemplateItem;
            var bindingContext = this.ParentGrid.BindingContext as DataFormGeneratorModel;
            if (templateItem != null && bindingContext != null)
            {
                entry.Text = templateItem.Description;
                bindingContext.FormTitle = templateItem.Title;
            }
        }
    }
}