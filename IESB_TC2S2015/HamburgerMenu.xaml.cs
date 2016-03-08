using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IESB_TC2S2015
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HamburgerMenu : Page
    {
        public new Frame Frame => App.RootFrame;
        public ObservableCollection<Model.Contato> ListaDeContatos { get; set; }

        public HamburgerMenu()
        {
            this.InitializeComponent();

            Frame.Navigated += Frame_Navigated;
        }

        private async void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView()
                .AppViewBackButtonVisibility =
                Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;

            //Verifica se a lista de contatos já foi carregada.
            if (ListaDeContatos?.Count > 0)
                return;

            //Caso a lista de contatos ainda não foi carregada, carregue utilizando o arquivo json.
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///SampleData/ListaDeContatos.json"));
            using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
            {
                string json = await sRead.ReadToEndAsync();
                ListaDeContatos = JsonConvert.DeserializeObject<ObservableCollection<Model.Contato>>(json);
            }

            MySplitView.Content = Frame;
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var favoritos = ListaDeContatos.Where(contato => contato.IsFavorito);
            Frame.Navigate(typeof(Favoritos), favoritos);
        }

        private void ListViewItem_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Todos), ListaDeContatos);
        }

        private void ListViewItem_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            var emails = ListaDeContatos.Where(contato => !string.IsNullOrWhiteSpace(contato.Email));
            Frame.Navigate(typeof(Emails), emails);
        }

        private void ListViewItem_Tapped_3(object sender, TappedRoutedEventArgs e)
        {
            var telefones = ListaDeContatos.Where(contato => !string.IsNullOrWhiteSpace(contato.Telefone));
            Frame.Navigate(typeof(Telefones), telefones);
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
    }
}
