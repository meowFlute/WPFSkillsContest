using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ContestEntry.Model;
using System.Windows;

namespace ContestEntry.Services
{
    /// <summary>
    /// Interface for creating new customers and reading all of the customers and adding orders to the database
    /// using entity framework
    /// </summary>
    public interface IDataAccessService
    {
        ObservableCollection<Customer> GetCustomers();
        ObservableCollection<Order> GetCustomerOrders(Customer customer);
        int AddNewOrder(Order newOrder);
    }

    /// <summary>
    /// Class implementing IDataAccessService interface
    /// </summary>
    public class DataAccessService : IDataAccessService
    {
        SampleDatabaseEntities dataContext;
        /// <summary>
        /// sets the context to the sample database entities
        /// </summary>
        public DataAccessService()
        {
            dataContext = new SampleDatabaseEntities();
        }
        
        /// <summary>
        /// uses the sample database context to return a list of customers
        /// </summary>
        /// <returns>A list of customers from the database</returns>
        public ObservableCollection<Customer> GetCustomers()
        {
            ObservableCollection<Customer> Customers = new ObservableCollection<Customer>();
            foreach (Customer customer in dataContext.Customers)
            {
                Customers.Add(customer);
            }
            return Customers;
        }

        /// <summary>
        /// Takes in a customer and returns the orders in the database whose customerID matches the customerID given
        /// </summary>
        /// <param name="customer">Customer whose orders we want</param>
        /// <returns>an observable collection of orders pertaining to the customer passed in</returns>
        public ObservableCollection<Order> GetCustomerOrders(Customer customer)
        {
            ObservableCollection<Order> Orders = new ObservableCollection<Order>();
            foreach(Order order in dataContext.Orders.Where(x=>x.Customer.CustomerID.Equals(customer.CustomerID)))
            {
                Orders.Add(order);
            }
            return Orders;
        }

        /// <summary>
        /// A function to add new orders to the database
        /// </summary>
        /// <param name="newOrder">new order to be added to database</param>
        /// <returns>Id of the order that has been added to database</returns>
        public int AddNewOrder(Order newOrder)
        {
            dataContext.Orders.Add(newOrder);
            try
            {
                dataContext.SaveChanges();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return newOrder.Id;
        }
    }
}
