using IESB_TC2S2015.Commands;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace IESB_TC2S2015.ViewModel
{
    public class ContatosViewModel
    {
        public Frame Frame => App.RootFrame;
        public ObservableCollection<Model.Contato> ListaDeContatos { get; set; }
        public Model.Contato ContatoSelecionado { get; set; }

        public ContatosViewModel()
        {
            using (SQLiteConnection connection = new SQLiteConnection(new SQLitePlatformWinRT(),
                App.SQLitePath))
            {
                ListaDeContatos =
                    new ObservableCollection<Model.Contato>(connection.Table<Model.Contato>());
            }

            this.AdicionarCommand = new Command(Adicionar);
            this.AlterarCommand = new Command(Alterar);
            this.ExcluirCommand = new Command<Model.Contato>(Excluir);
            this.SalvarCommand = new Command<Model.Contato>(Salvar);
            this.CancelarCommand = new Command(Cancelar);
        }

        public Command AdicionarCommand { get; set; }
        public void Adicionar()
        {
            this.ContatoSelecionado = new Model.Contato();
            Frame.Navigate(typeof(Contato), this);
        }

        public ICommand SalvarCommand { get; set; }
        public void Salvar(Model.Contato contato)
        {
            using (SQLiteConnection connection =
                new SQLiteConnection(new SQLitePlatformWinRT(), App.SQLitePath))
            {
                connection.InsertOrReplace(contato);
            }
            Frame.GoBack();
        }

        public ICommand AlterarCommand { get; set; }
        public void Alterar()
        {
            Frame.Navigate(typeof(Contato), this);
        }

        public ICommand ExcluirCommand { get; set; }
        public void Excluir(Model.Contato contato)
        {
            using (SQLiteConnection connection = new SQLiteConnection(new SQLitePlatformWinRT(),
                App.SQLitePath))
            {
                connection.Delete(contato);
            }
            Frame.GoBack();
        }

        public ICommand CancelarCommand { get; set; }
        public void Cancelar()
        {
            Frame.GoBack();
        }
    }
}
