using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using static CoffeMachineUI.Utilities.CoffeeMachineEnum;
using Drink = Store.ApplicationCore.Entities.Drink;
using System.Windows;
using CoffeeMachineUI.Utilities;

namespace CoffeMachineUI.ViewModel
{
    public class DrinkViewModel : INotifyPropertyChanged
    {
        private Drink lastDrink;
        private static HttpClient client = new HttpClient();
        
        #region Constructor
        public DrinkViewModel()
        {
            GetDrinkCommand = new RelayCommand(DoGetDrinkCommand, CanExecuteGetDrinkCommand);
            GetMyLastDrinkCommand = new RelayCommand(DoGetMyLastDrinkCommand);
        }
        #endregion

        #region Properties
        private ObservableCollection<DrinkType> _drinkTypes = new ObservableCollection<DrinkType>();
        /// <summary>
        /// List of drink types
        /// </summary>
        public ObservableCollection<DrinkType> DrinkTypes
        {
            get
            {
                if (!_drinkTypes.Any())
                {
                    _drinkTypes.Add(DrinkType.Tea);
                    _drinkTypes.Add(DrinkType.Coffee);
                    _drinkTypes.Add(DrinkType.Choclate);
                }
                return _drinkTypes;
            }
        }

        private List<int> _sugarQuantityChoice = new List<int> { 0, 1, 2, 3, 4 };
        /// <summary>
        /// List to select quantity of Sugar
        /// </summary>
        public List<int> SugarQuantityChoice
        {
            get { return _sugarQuantityChoice; }
        }

        private DrinkType _drinkType;
        /// <summary>
        /// Type of drink
        /// </summary>
        public DrinkType DrinkType
        {
            get { return _drinkType; }
            set
            {
                _drinkType = value;
                OnPropertyChanged(nameof(DrinkType));
            }
        }

        private int _sugarQuantity;
        /// <summary>
        /// Quantity of sugar
        /// </summary>
        public int SugarQuantity
        {
            get { return _sugarQuantity; }
            set
            {
                _sugarQuantity = value;
                OnPropertyChanged(nameof(SugarQuantity));
            }
        }
        private bool _useOwnMug;
        /// <summary>
        /// Use own mug or not
        /// </summary>
        public bool UseOwnMug
        {
            get { return _useOwnMug; }
            set
            {
                _useOwnMug = value;
                OnPropertyChanged(nameof(UseOwnMug));
            }
        }

        private bool _useBadge;
        /// <summary>
        /// Use badge to save your drink choice
        /// </summary>
        public bool UseBadge
        {
            get { return _useBadge; }
            set
            {
                _useBadge = value;
                if (!_useBadge) Badge = null;
                OnPropertyChanged(nameof(UseBadge));
                OnPropertyChanged(nameof(Badge));
            }
        }

        private int ? _badge;
        /// <summary>
        /// Badge of customer
        /// </summary>
        public int ? Badge
        {
            get { return _badge; }
            set
            {
                _badge = value;
                OnPropertyChanged(nameof(Badge));
                OnPropertyChanged(nameof(BtnLastDrinkEnabled));
            }
        }

        /// <summary>
        /// Is Button get last drink enabled
        /// </summary>
        public bool BtnLastDrinkEnabled
        {
            get { return Badge!=null?true :false; }
        }

        #endregion

        #region Commands
        public RelayCommand GetDrinkCommand { get; set; }
        private async void DoGetDrinkCommand()
        {
            if (lastDrink != null)
            {
                lastDrink.Type = (int)DrinkType;
                lastDrink.SugarQt = SugarQuantity;
                lastDrink.UseOwnMug = UseOwnMug;
                lastDrink = await UpdateDrinkAsync(lastDrink);
                MessageBox.Show($"Your drink is ready : {(DrinkType)lastDrink.Type} with {lastDrink.SugarQt} sugar");
            }
            else
            {
                var drink = new Drink
                {
                    Type = (int)DrinkType,
                    SugarQt = SugarQuantity,
                    UseOwnMug = UseOwnMug,
                    Badge = Badge
                };
              var dr = await CreateDrinkAsync(drink);
              MessageBox.Show($"Your drink is ready : {(DrinkType)dr.Type} with {dr.SugarQt} sugar");
            }
        }

        private bool CanExecuteGetDrinkCommand()
        {
            return  true;
        }

        public RelayCommand GetMyLastDrinkCommand { get; set; }
        private async void DoGetMyLastDrinkCommand()
        {
            lastDrink = await GetDrinkAsync(string.Format(CoffeeMachineConst.ApiUrl + @"/{0}", Badge));
            if (lastDrink != null)
            {
                DrinkType = (DrinkType)lastDrink.Type;
                SugarQuantity = lastDrink.SugarQt;
                UseOwnMug = lastDrink.UseOwnMug;
            } 
        }
        #endregion

        #region Methods
        private async Task<Drink?> GetDrinkAsync(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
              return  await response.Content.ReadAsAsync<Drink>();
            }
            else
            {
                MessageBox.Show($"Badge {Badge} {response.ReasonPhrase} - please choose your drink");
                return null;
            }
        }

        private async Task<Drink> CreateDrinkAsync(Drink drink)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(CoffeeMachineConst.ApiUrl, drink);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated drink from the response body.
            drink = await response.Content.ReadAsAsync<Drink>();
            return drink;
        }

        private async Task<Drink> UpdateDrinkAsync(Drink drink)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(string.Format(CoffeeMachineConst.ApiUrl + @"/{0}", Badge), drink);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated drink from the response body.
            drink = await response.Content.ReadAsAsync<Drink>();
            return drink;
        }
        #endregion

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }

}
