using IESB_TC2S2015.ViewModel;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IESB_TC2S2015
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Contato : Page
    {
        public new Frame Frame => App.RootFrame;

        public Contato()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null && e.Parameter is ContatosViewModel)
                this.DataContext = e.Parameter as ContatosViewModel;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ("Windows.Mobile".Equals(AnalyticsInfo.VersionInfo.DeviceFamily))
                this.BottomAppBar.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ("Windows.Mobile".Equals(AnalyticsInfo.VersionInfo.DeviceFamily))
                this.BottomAppBar.Visibility = Visibility.Visible;
        }
    }
}
