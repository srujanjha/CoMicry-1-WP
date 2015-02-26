using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private TheOatmealViewModel _theOatmealModel;
       private GarfieldGarfieldViewModel _garfieldGarfieldModel;
       private XKCDViewModel _xKCDModel;
       private SatMorningBreakfastCerealViewModel _satMorningBreakfastCerealModel;
       private NuklearPowerViewModel _nuklearPowerModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = TheOatmealModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public TheOatmealViewModel TheOatmealModel
        {
            get { return _theOatmealModel ?? (_theOatmealModel = new TheOatmealViewModel()); }
        }
 
        public GarfieldGarfieldViewModel GarfieldGarfieldModel
        {
            get { return _garfieldGarfieldModel ?? (_garfieldGarfieldModel = new GarfieldGarfieldViewModel()); }
        }
 
        public XKCDViewModel XKCDModel
        {
            get { return _xKCDModel ?? (_xKCDModel = new XKCDViewModel()); }
        }
 
        public SatMorningBreakfastCerealViewModel SatMorningBreakfastCerealModel
        {
            get { return _satMorningBreakfastCerealModel ?? (_satMorningBreakfastCerealModel = new SatMorningBreakfastCerealViewModel()); }
        }
 
        public NuklearPowerViewModel NuklearPowerModel
        {
            get { return _nuklearPowerModel ?? (_nuklearPowerModel = new NuklearPowerViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            TheOatmealModel.ViewType = viewType;
            GarfieldGarfieldModel.ViewType = viewType;
            XKCDModel.ViewType = viewType;
            SatMorningBreakfastCerealModel.ViewType = viewType;
            NuklearPowerModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadData(bool isNetworkAvailable)
        {
            var loadTasks = new Task[]
            { 
                TheOatmealModel.LoadItems(isNetworkAvailable),
                GarfieldGarfieldModel.LoadItems(isNetworkAvailable),
                XKCDModel.LoadItems(isNetworkAvailable),
                SatMorningBreakfastCerealModel.LoadItems(isNetworkAvailable),
                NuklearPowerModel.LoadItems(isNetworkAvailable),
            };
            await Task.WhenAll(loadTasks);
        }

        /// <summary>
        /// Refresh ViewModel items asynchronous
        /// </summary>
        public async Task RefreshData(bool isNetworkAvailable)
        {
            var refreshTasks = new Task[]
            { 
                TheOatmealModel.RefreshItems(isNetworkAvailable),
                GarfieldGarfieldModel.RefreshItems(isNetworkAvailable),
                XKCDModel.RefreshItems(isNetworkAvailable),
                SatMorningBreakfastCerealModel.RefreshItems(isNetworkAvailable),
                NuklearPowerModel.RefreshItems(isNetworkAvailable),
            };
            await Task.WhenAll(refreshTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await RefreshData(NetworkInterface.GetIsNetworkAvailable());
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
