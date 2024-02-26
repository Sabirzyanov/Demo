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
using Demo.Core.Context;
using Demo.Core.Repositories;
using Demo.WPF.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Demo.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class ProductListModel
        {
            public int Id { get; set; }
            public string ProductType { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public string Articul { get; set; }
            public string Materials { get; set; }
            public byte[] Image { get; set; }
        }

        private const string DefaultOrder = "По умолчанию";
        private const string AscendingOrder = "От А до Я";
        private const string DescendingOrder = "От Я до А";
        private const string UniversalFilter = "Все";

        private const string FilterKey = "filter";
        private const string SearchKey = "search";
        private const string SortKey = "sort";

        private const string SearchPlaceholder = "Введите для поиска";
        
        private List<ProductListModel> _products = new();
        private List<ProductListModel> _paginatedProducts = new();
        
        private Dictionary<string, Action> _listModifications = new ();

        public MainWindow()
        {
            InitializeComponent();
            
            Init();
            SortComboBox.ItemsSource = new[]
            {
                DefaultOrder,
                AscendingOrder,
                DescendingOrder
            };

            SortComboBox.SelectedItem = DefaultOrder;
            var filters = GlobalContext.MaterialTypeRepository.GetAll().Select(x => x.Name).ToArray();
            FilterComboBox.ItemsSource = filters;

            FilterComboBox.SelectedItem = UniversalFilter;
            
            LvProducts.ItemsSource = _paginatedProducts;
        }

        private void Init()
        {
            var dbContext = new DBContext(new DbContextOptions<DBContext>());

            GlobalContext.MaterialRepository = new MaterialRepository(dbContext);
            GlobalContext.MaterialTypeRepository = new MaterialTypeRepository(dbContext);
            GlobalContext.ProductRepository = new ProductRepository(dbContext);
        }
        
        private void Tb_Search_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox currentTb = sender as TextBox;
            string text = currentTb.Text;

            _listModifications.Remove(SearchKey);
            if (!text.IsNullOrEmpty() && text != SearchPlaceholder)
            {
                _listModifications.Add(SearchKey, () => LvProducts.ItemsSource = ListEditingFunctions.Search(LvProducts.ItemsSource.Cast<ProductListModel>(), text));   
            }

            ApplyModifications();
        }

        private void Tb_Search_OnLostFocus(object sender, RoutedEventArgs e)
        {
            _listModifications.Remove(SearchKey);
            ApplyModifications();
            
            TextBox currentTb = sender as TextBox;
            string text = currentTb.Text;

            if (text.IsNullOrEmpty())
            {
                SearchTextBox.Text = SearchPlaceholder;   
            }
        }

        private void OnSortingChanged(object sender, SelectionChangedEventArgs e)
        {
            string order = (sender as ComboBox).SelectedItem as string;

            _listModifications.Remove(SortKey);
            
            if (order != DefaultOrder)
            {
                _listModifications.Add(SortKey, () => LvProducts.ItemsSource = ListEditingFunctions.Order(LvProducts.ItemsSource.Cast<ProductListModel>(), order));
            }

            ApplyModifications();
        }

        private void OnFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            var filter = (sender as ComboBox).SelectedItem as string;

            _listModifications.Remove(FilterKey);
            
            if (filter != UniversalFilter)
            {
                _listModifications.Add(FilterKey, () => LvProducts.ItemsSource = ListEditingFunctions.FilterByType(LvProducts.ItemsSource.Cast<ProductListModel>(), filter));
            }

            ApplyModifications();
        }

        private void ApplyModifications()
        {
            if (LvProducts is null)
            {
                return;
            }
            
            LvProducts.ItemsSource = _paginatedProducts;

            foreach (var key in _listModifications.Keys)
            {
                _listModifications[key]();
            }
            
            LvProducts.Items.Refresh();
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
        }
    }
}