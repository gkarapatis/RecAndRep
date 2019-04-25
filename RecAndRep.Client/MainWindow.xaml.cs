using RecAndRep.Client.Business;
using RecAndRep.Client.Business.ActionResolver;
using RecAndRep.Client.Business.ActionResolver.Abstractions;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecAndRep.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientHubConnectionHandler _clientHubConnectionService;
        IActionResolver _resolver;

        public MainWindow()
        {
            InitializeComponent();
            _clientHubConnectionService = new ClientHubConnectionHandler($"Client_{new Random().Next(1, 1000)}", method);
            _resolver = ActionResolver.Instance;
            Task.Run(() => _clientHubConnectionService.ConnectAsync());
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _resolver.Parse(TestCommandtxt.Text);
        }

        public void method(string key, string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                RichTextBoxConsole.AppendText($"executing {key}: {message}\r\n");
                var response = _resolver.Parse(message);
                RichTextBoxConsole.AppendText($"status {key}: {response.Succeeded}\r\n");
                _clientHubConnectionService.Response(key, response.Succeeded? "Succeeded":response.ErrorMessage);
            });
        }

    }
}
