using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ControlCatalog.Pages
{
    public class ToolTipPage : UserControl
    {
        public ToolTipPage()
        {
            this.InitializeComponent();
            this.DataContext = new ItemsViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        class ItemsViewModel
        {
            public ObservableCollection<ItemViewModel> Items { get; } = new ObservableCollection<ItemViewModel>();

            public ItemsViewModel()
            {
                for (var i = 0; i < 300; i++)
                {
                    Items.Add(new ItemViewModel($"Text {i}"));
                }
            }
            
            public class ItemViewModel
            {
                public string Text { get; }

                public ItemViewModel(string text)
                {
                    Text = text;
                }
            }
        }
        
    }
}
