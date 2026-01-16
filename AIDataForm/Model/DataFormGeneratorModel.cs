using Syncfusion.Maui.AIAssistView;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AIDataForm
{
    public class DataFormGeneratorModel : INotifyPropertyChanged
    {
        private bool showDataForm, showAssistView, showSubmitButton, showInputView, showOfflineLabel;

        internal string? FormTitle { get; set; }

        public DataFormGeneratorModel()
        {
            showInputView = true;
            showOfflineLabel = true;

            Templates = new ObservableCollection<TemplateItem>
            {
                new TemplateItem { Font="\ue763", Title = "Contact Form", Description = "Create a form to capture user details." },
                new TemplateItem { Font="\ue761", Title = "Employment Details", Description = "Create a form to capture employment details." },
                new TemplateItem { Font="\ue73a", Title = "Feedback Form", Description = "Create a form to receive client feedback." }
            };
        }

        public ObservableCollection<TemplateItem> Templates { get; set; }

        public ObservableCollection<IAssistItem> Messages { get; set; } = new ObservableCollection<IAssistItem>();

        public bool ShowDataForm
        {
            get => this.showDataForm;
            set { this.showDataForm = value; RaisePropertyChanged(nameof(this.ShowDataForm)); }
        }

        public bool ShowAssistView
        {
            get => this.showAssistView;
            set { this.showAssistView = value; RaisePropertyChanged(nameof(this.ShowAssistView)); }
        }

        public bool ShowInputView
        {
            get => this.showInputView;
            set { this.showInputView = value; RaisePropertyChanged(nameof(this.ShowInputView)); }
        }

        public bool ShowSubmitButton
        {
            get => this.showSubmitButton;
            set { this.showSubmitButton = value; RaisePropertyChanged(nameof(this.ShowSubmitButton)); }
        }

        public bool ShowOfflineLabel
        {
            get => this.showOfflineLabel;
            set { this.showOfflineLabel = value; RaisePropertyChanged(nameof(this.ShowOfflineLabel)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public class TemplateItem
    {
        public string? Font { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}