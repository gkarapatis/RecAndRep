using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using RecAndRep.BOL;
using RecAndRep.Server.Business;
using RecAndRep.BOL.Model;
using RecAndRep.Server;

namespace RecAndRep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServerHubConnectionHandler _serverHubConnectionService;

        private List<string> _clients = new List<string>();

        CommandRepository repository = new CommandRepository();

        IEnumerable<RecAndRep.BOL.Model.Command> commands;

        public MainWindow()
        {            
            InitializeComponent();
            new InitializeCommands(repository).Initialize();

            SignalRServerManager.Instance.Start();
            _serverHubConnectionService = new ServerHubConnectionHandler("Server Machine", responseMessage, clientConected, clientDisconnected);
            Task.Run(() => _serverHubConnectionService.ConnectAsync());
            commands = repository.Get();
            CommandsGrid.ItemsSource = commands;
        }



        public void responseMessage(string name, string key, string status)
        {
            this.Dispatcher.Invoke(() =>
            {
                logToWindow($"Client {name}: {key} {status}");
            });
        }

        public void clientConected(string name)
        {
            this.Dispatcher.Invoke(() =>
            {
                logToWindow($"Client {name}: Connected");
                _clients.Add(name);
            });
        }

        public void clientDisconnected(string name)
        {
            this.Dispatcher.Invoke(() =>
            {
                logToWindow($"Client {name}: Disonnected");
                _clients.Remove(name);
            });
        }

        private DateTime lastButton_Click;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lastButton_Click!= DateTime.MinValue && (DateTime.Now - lastButton_Click).TotalMilliseconds < 500)
                return;
            lastButton_Click = DateTime.Now;

            var client = _clients.LastOrDefault();
            if (client == null)
            {
                logToWindow($"No client found.");
                return;
            }


            if (CommandsGrid.SelectedItems.Count == 0)
            {
                logToWindow($"No action selected.");
                return;
            }

            foreach (var item in CommandsGrid.SelectedItems)
            {
                _serverHubConnectionService.SendToClientUser(client, refId(), ((Command)item).Description);
            }
        }

        int _refId = 100;
        private string refId()
        {
            _refId++;
            return _refId.ToString();
        }

        private void addNewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(newActionTxt.Text))
            {
                repository.Add(new Command() { Description = newActionTxt.Text });
            }
            commands = repository.Get();
            CommandsGrid.ItemsSource = commands;
        }

        private void logToWindow(string text)
        {
            RichTextBoxConsole.AppendText(($"{text} \n"));
            RichTextBoxConsole.ScrollToEnd();
        }
    }
}
