using System;
using System.Linq;
using HSS.Core;
using HSS.Models;
using HSS.Core.Data;
using HSS.Services.Common;
using HSS.Core.Caching;

namespace HSS.Services.Customers
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public partial class CustomerService : ICustomerService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        private const string CUSTOMER_ALL_KEY = "zh.customer.all-{0}";

        private const string CUSTOMER_PATTERN_KEY = "zh.customer.";

        #endregion

        #region Fields

        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<GenericAttribute> _gaRepository;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public CustomerService(ICacheManager cacheManager,
            IRepository<Customer> customerRepository,
            IRepository<GenericAttribute> gaRepository,
            IGenericAttributeService genericAttributeService)
        {
            this._cacheManager = cacheManager;
            this._customerRepository = customerRepository;
            this._gaRepository = gaRepository;
            this._genericAttributeService = genericAttributeService;
        }

        #endregion
        
        #region Method
        public virtual void DeleteCustomer(Customer entity)
        {
            if (entity == null)
                throw new ArgumentNullException("customer");
            
            entity.Deleted = true;

            UpdateCustomer(entity);
        }

        public virtual IPagedList<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int customerId)
        {
            if (customerId == 0)
                return null;

            return _customerRepository.GetById(customerId);
        }

        public virtual Customer GetGuestCustomer()
        {
            var customer = new Customer
            {
                LevelId = -1,
                CreateOn = DateTime.Now,
                Deleted = false,
                LastActivityDate = DateTime.Now,
                LastIpAddress = "",
                Id = 0,
                LoginName = "Guest",
                Password = "",
            };

            return customer;
        }

        public virtual Customer InsertGuestCustomer()
        {
            var customer = new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                Active = true,
                CreateOn = DateTime.Now,
                LastActivityDate = DateTime.Now,
                Deleted = false,
                LastIpAddress = "",
            };
            
            _customerRepository.Insert(customer);

            return customer;
        }


        public virtual Customer GetCustomerByName(string loginName)
        {
            if (String.IsNullOrWhiteSpace(loginName))
                return null;

            var query = from c in _customerRepository.Table
                        where c.LoginName == loginName
                        orderby c.Id
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        public virtual void InsertCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Insert(customer);
            
        }

        public virtual void UpdateCustomer(Customer entity)
        {
            if (entity == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Update(entity);


            //cache
            _cacheManager.RemoveByPattern(CUSTOMER_PATTERN_KEY);
        }

        public virtual Customer GetCustomerByGuid(Guid customerGuid)
        {
            if (customerGuid == Guid.Empty)
                return null;

            var query = from c in _customerRepository.Table
                        where c.CustomerGuid == customerGuid
                        orderby c.Id
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        #endregion
    }
}
