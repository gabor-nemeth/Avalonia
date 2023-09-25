using System.Linq;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.UnitTests;
using Xunit;

namespace Avalonia.Markup.Xaml.UnitTests
{
    public class StyleTests : XamlTestBase
    {
        [Fact]
        public void Binding_Should_Be_Assigned_To_Setter_Value_Instead_Of_Bound()
        {
            using (UnitTestApplication.Start(TestServices.MockPlatformWrapper))
            {
                var xaml = "<Style Selector='Button' xmlns='https://github.com/avaloniaui'><Setter Property='Content' Value='{Binding}'/></Style>";
                var style = (Style)AvaloniaRuntimeXamlLoader.Load(xaml);
                var setter = (Setter)(style.Setters.First());

                Assert.IsType<Binding>(setter.Value);
            }
        }

        [Fact]
        public void Selector_MenuItem_IsSelected()
        {
            using (UnitTestApplication.Start(TestServices.MockPlatformWrapper))
            {
                var xaml = "<Style Selector='MenuItem[(SelectingItemsControl.IsSelected)=True]' xmlns='https://github.com/avaloniaui'><Setter Property='Background' Value='Green'/></Style>";
                var style = (Style)AvaloniaRuntimeXamlLoader.Load(xaml);
                var setter = (Setter)(style.Setters.First());

                Assert.IsAssignableFrom<IBrush>(setter.Value);
            }
        }

        [Fact]
        public void Setter_With_TwoWay_Binding_Should_Update_Source()
        {
            using (UnitTestApplication.Start(TestServices.MockThreadingInterface))
            {
                var data = new Data
                {
                    Foo = "foo",
                };

                var control = new TextBox
                {
                    DataContext = data,
                };

                var style = new Style()
                {
                    Setters =
                    {
                        new Setter
                        {
                            Property = TextBox.TextProperty,
                            Value = new Binding
                            {
                                Path = "Foo",
                                Mode = BindingMode.TwoWay
                            }
                        }
                    }
                };

                StyleHelpers.TryAttach(style, control);
                Assert.Equal("foo", control.Text);

                control.Text = "bar";
                Assert.Equal("bar", data.Foo);
            }
        }

        private class Data
        {
            public string Foo { get; set; }
        }
    }
}
