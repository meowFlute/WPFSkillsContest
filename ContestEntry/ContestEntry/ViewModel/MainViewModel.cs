using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using ContestEntry.Model;
using ContestEntry.Services;
using System;

namespace ContestEntry.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region properties
        //fields
        ObservableCollection<Customer> _customers;
        ObservableCollection<Order> _orders;
        Customer _selectedCustomer;
        IDataAccessService _dataAccessService;
        DateTime _newOrderDate;
        decimal _newOrderTotal;
        string _selectedNewOrderStatus;

        //properties
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                RaisePropertyChanged("Customers");
            }
        }
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if(_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    GetCustomerOrders();
                    RaisePropertyChanged("SelectedCustomer");
                }
            }
        }
        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                RaisePropertyChanged("Orders");
            }
        }
        public DateTime NewOrderDate
        {
            get { return _newOrderDate; }
            set
            {
                _newOrderDate = value;
                RaisePropertyChanged("NewOrderDate");
            }
        }
        public decimal NewOrderTotal
        {
            get { return _newOrderTotal; }
            set
            {
                _newOrderTotal = value;
                RaisePropertyChanged("NewOrderTotal");
            }
        }
        public string SelectedNewOrderStatus
        {
            get { return _selectedNewOrderStatus; }
            set
            {
                _selectedNewOrderStatus = value;
                RaisePropertyChanged("SelectedNewOrderStatus");
            }
        }
        public ObservableCollection<string> OrderStatusChoices { get; set; }
        public RelayCommand AddOrderCommand { get; set; }
        public RelayCommand CancelOrderCommand { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataAccessService dataAccSrv)
        {
            _dataAccessService = dataAccSrv;

            Customers = new ObservableCollection<Customer>();
            Orders = new ObservableCollection<Order>();
            OrderStatusChoices = new ObservableCollection<string>();

            AddOrderCommand = new RelayCommand(AddOrder);


            GetCustomers();
            SelectedCustomer = Customers[0];
            NewOrderDate = DateTime.Now;
            NewOrderTotal = 0;
            InitializeStatusChoices();
            SelectedNewOrderStatus = OrderStatusChoices[0];
        }

        /// <summary>
        /// Takes all of the customers from the database into an observable collection
        /// </summary>
        void GetCustomers()
        {
            Customers.Clear();
            foreach( Customer customer in _dataAccessService.GetCustomers())
            {
                Customers.Add(customer);
            }
        }

        void GetCustomerOrders()
        {
            Orders.Clear();
            foreach( Order order in _dataAccessService.GetCustomerOrders(SelectedCustomer))
            {
                Orders.Add(order);
            }
        }

        void AddOrder()
        {
            //fill out the new order before sending it off
            Order NewOrder = new Order();
            NewOrder.Customer = SelectedCustomer;
            NewOrder.CustomerID = SelectedCustomer.CustomerID;
            NewOrder.ItemsTotal = NewOrderTotal;
            NewOrder.OrderDate = NewOrderDate;
            if (SelectedNewOrderStatus == OrderStatusChoices[0])
                NewOrder.OrderStatus = 1;
            else if (SelectedNewOrderStatus == OrderStatusChoices[1])
                NewOrder.OrderStatus = 2;
            else
                NewOrder.OrderStatus = 3;

            _dataAccessService.AddNewOrder(NewOrder);
            GetCustomerOrders();
        }

        void InitializeStatusChoices()
        {
            OrderStatusChoices.Add("New");
            OrderStatusChoices.Add("On Hold");
            OrderStatusChoices.Add("Delivered");
        }

        void CancelOrder()
        {

        }
        #endregion
    }
}