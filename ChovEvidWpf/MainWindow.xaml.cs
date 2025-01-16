using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChovEvidWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApiClient _apiClient;

        public MainWindow()
        {
            InitializeComponent();
            _apiClient = new ApiClient("https://localhost:7179/");
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var breedingStations = await _apiClient.GetAllBreedingStations();
                BreedingStationsListView.ItemsSource = breedingStations;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Chyba pri načítavaní dát: {ex.Message}");
            }
        }
    }
}