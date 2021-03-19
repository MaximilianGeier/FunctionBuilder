using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FunctionBuilder.Logic;

namespace FunctionBuilder.GUI
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public void InputButton_Click(object sender, RoutedEventArgs e )
        {
            _ = this.Find<Button>("InputButton");
            var OutputResult = this.Find<TextBlock>("OutputResult");
            var InputBox = this.Find<TextBox>("InputBox");

            string equation = Rpn.StartRpn(InputBox.Text);
            OutputResult.Text = equation;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
        }
    }
}
