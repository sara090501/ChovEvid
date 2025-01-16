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
using System.Windows.Shapes;

namespace ChovEvidWpf
{
    /// <summary>
    /// Interaction logic for DogsWindow.xaml
    /// </summary>
    public partial class DogsWindow : Window
    {
        private ApiClient _apiClient;

        public DogsWindow()
        {
            InitializeComponent();

            _apiClient = new ApiClient("https://localhost:7179/");
            _ = LoadDataAsync();
            
        }
        private async Task LoadDataAsync()
        {
            try
            {
                var dogs = await _apiClient.GetAllDogs();
                DogsListView.ItemsSource = dogs;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Chyba pri načítavaní dát: {ex.Message}");
            }
        }

        private void DogsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DogsListView.SelectedItem != null)
            {
                RemoveDogRecord.IsEnabled = true;
            }
        }
    }
}
