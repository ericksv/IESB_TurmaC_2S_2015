using IESB_TC2S2015.Commands;
using IESB_TC2S2015.Model;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace IESB_TC2S2015.ViewModel
{
    public class ContatosViewModel
    {
        Frame frame => App.RootFrame;

        public ObservableCollection<Model.Contato>
            Lista
        { get; set; }
        public Model.Contato ContatoSelecionado { get; set; }

        public ContatosViewModel()
        {
            using (SQLiteConnection dbConnection =
                new SQLiteConnection(new SQLitePlatformWinRT(),
                App.SQLitePath))
            {
                Lista =
                    new ObservableCollection<Model.Contato>
                    (
                        dbConnection.Table<Model.Contato>()
                    );
            }

            this.IncluirCommand =
                new Commands.Command(Incluir);
            this.SalvarCommand =
                new Commands.Command(Salvar);
            this.EditarCommand =
                new Commands.Command(Editar);
            this.CancelarCommand =
                new Commands.Command(Cancelar);
            this.DeletarCommand =
                new Commands.Command(Deletar);
        }

        public ICommand IncluirCommand { get; set; }
        public void Incluir()
        {
            this.ContatoSelecionado = new Model.Contato();
            frame.Navigate(typeof(View.Contato),
                this);
        }

        public ICommand EditarCommand { get; set; }
        public void Editar()
        {
            frame.Navigate(typeof(View.Contato),
                this);
        }

        public ICommand CancelarCommand { get; set; }
        public void Cancelar()
        {
            frame.GoBack();
        }

        public ICommand SalvarCommand { get; set; }
        public void Salvar()
        {
            using (SQLiteConnection dbConnection =
                new SQLiteConnection(new SQLitePlatformWinRT(),
                App.SQLitePath))
            {
                dbConnection
                    .InsertOrReplace(this.ContatoSelecionado);
            }

            frame.GoBack();
        }

        public ICommand DeletarCommand { get; set; }
        public void Deletar()
        {
            using (SQLiteConnection dbConnection =
                new SQLiteConnection(new SQLitePlatformWinRT(),
                App.SQLitePath))
            {
                dbConnection
                    .Delete(this.ContatoSelecionado);
            }

            frame.GoBack();
        }
    }
}
