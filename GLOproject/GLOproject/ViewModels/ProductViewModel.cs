using GLOproject.DataBase.Entity;
using GLOproject.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GLOproject.Models;
using GLOproject.Commands;

namespace GLOproject.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly AppDbContext _dbContext;
        private readonly ProductModel _productsM;
        private readonly StoreCommand _employeeStore;
        public ObservableCollection<Product> ProductsDataList { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        private Category _selectedCategory;
        private ObservableCollection<Product> _allProducts;
        private bool _isAdmin;

        public bool IsNotGuest
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged(nameof(IsNotGuest));
                }
            }
        }
        private bool _isSave;

        public bool IsSave
        {
            get { return _isSave; }
            set
            {
                if (_isSave != value)
                {
                    _isSave = value;
                    OnPropertyChanged(nameof(IsSave));
                }
            }
        }

        public ProductViewModel(AppDbContext dbContext, StoreCommand employeeStore)
        {
            _employeeStore = employeeStore;
            _dbContext = dbContext;
            _productsM = new ProductModel();
            _allProducts = new ObservableCollection<Product>(_dbContext.Products);

            UpdateFilteredProducts();
            LoadCategories();

            SaveCommand = new SaveCommand(_dbContext);

            PropertyChanged += ProductsVM_PropertyChanged;

            if (_employeeStore.CurrentEmployee.RoleID != 1)
            {
                IsNotGuest = true;
            }
            IsSave = _employeeStore.CurrentEmployee.RoleID == 2;
        }

        private void LoadCategories()
        {
            Categories.Clear();
            var categories = _dbContext.Categories.ToList(); // Убедитесь что категории загружены в список

            foreach (var category in categories)
            {
                Categories.Add(category);
            }

            if (Categories.Any())
                SelectedCategory = Categories.First(); // Установить первую категорию по умолчанию
        }

        private void ProductsVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Search))
            {
                UpdateFilteredProducts();
            }
            else if (e.PropertyName == nameof(SelectedCategory))
            {
                UpdateCategoryFilter();
            }
        }

        private void UpdateFilteredProducts()
        {
            ProductsDataList.Clear();

            foreach (var product in _allProducts)
            {
                if (MatchesSearch(product) && (_selectedCategory == null || product.Category == _selectedCategory))
                {
                    ProductsDataList.Add(product);
                }
            }
        }

        private void UpdateCategoryFilter()
        {
            ProductsDataList.Clear();

            foreach (var product in _allProducts)
            {
                if (MatchesSearch(product) && (product.Category == _selectedCategory || _selectedCategory == _dbContext.Categories.FirstOrDefault(c => c.ID == 1)))
                {
                    ProductsDataList.Add(product);
                }
            }
        }

        private bool MatchesSearch(Product product)
        {
            return string.IsNullOrEmpty(Search) || product.Title.ToLower().Contains(Search.ToLower());
        }

        public string Search
        {
            get { return _productsM.Search; }
            set { _productsM.Search = value; OnPropertyChanged(nameof(Search)); }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); UpdateCategoryFilter(); }
        }

        public ICommand SaveCommand { get; set; }
    }
}
