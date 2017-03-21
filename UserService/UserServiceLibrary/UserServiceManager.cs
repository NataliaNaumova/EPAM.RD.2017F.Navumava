using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using Logger;
using UserServiceLibrary.Configuration;
using UserServiceLibrary.Interfaces;

namespace UserServiceLibrary
{
    /// <summary>
    /// Provides access to master and slave instances of a user service layer in non-distributed mode.
    /// </summary>
    public class UserServiceManager 
    {
        #region Fields

        private AppDomain _masterDomain;
        private ICollection<AppDomain> _slavesDomains;

        private IUserService _master;
        private ICollection<IUserService> _slaves;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the master user service instance.
        /// </summary>
        public IUserService Master
        {
            get { return _master; }
        }

        /// <summary>
        /// Gets the collection of slave service instances.
        /// </summary>
        public ICollection<IUserService> Slaves
        {
            get { return _slaves; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the UserServiceManager class.
        /// </summary>
        public UserServiceManager() : this(null, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserServiceManager class with specified logger.
        /// </summary>
        /// <param name="logger">Logger for all user services.</param>
        public UserServiceManager(ILogger logger) 
            : this(logger, null, null, null)
        {
        }


        /// <summary>
        /// Initializes a new instance of the UserServiceManager class with specified logger and id generating function.
        /// </summary>
        /// <param name="logger">Logger for all user services.</param>
        /// <param name="idGenerator">Id generating function.</param>
        /// <param name="seed">Initial value for id generating function.</param>
        public UserServiceManager(ILogger logger,
            Func<int, int> idGenerator, int seed = 0)
            : this(logger, null, null, idGenerator, seed)
        {
        }


        /// <summary>
        /// Initializes a new instance of the UserServiceManager class with specified user validator, 
        /// user storage serializer and id generating function. 
        /// </summary>
        /// <param name="userValidator">Validator for user instances.</param>
        /// <param name="userServiceStorageSerializer">Serializer for user storage.</param>
        /// <param name="idGenerator">Id generating function.</param>
        /// <param name="seed">Initial value for id generating function.</param>
        public UserServiceManager(IUserValidator userValidator,
            IUserServiceStorageSerializer userServiceStorageSerializer,
            Func<int, int> idGenerator, int seed = 0)
            : this(null, userValidator, userServiceStorageSerializer, idGenerator, seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserServiceManager class with specified logger, user validator, 
        /// user storage serializer and id generating function.
        /// </summary>
        /// <param name="logger">Logger for all user services.</param>
        /// <param name="userValidator">Validator for user instances.</param>
        /// <param name="userServiceStorageSerializer">Serializer for user storage.</param>
        /// <param name="idGenerator">Id generating function.</param>
        /// <param name="seed">Initial value for id generating function.</param>
        public UserServiceManager(ILogger logger, IUserValidator userValidator,
            IUserServiceStorageSerializer userServiceStorageSerializer, Func<int, int> idGenerator,
            int seed = 0)
        {
            _slaves = new List<IUserService>();
            _slavesDomains = new List<AppDomain>();
            CreateMaster(logger, userValidator, userServiceStorageSerializer,
                idGenerator, seed);
            CreateSlavesCollection(logger, userValidator, userServiceStorageSerializer,
                idGenerator, seed);
        }

        #endregion

        #region PrivateMethods

        private void CreateMaster(ILogger logger, IUserValidator userValidator,
            IUserServiceStorageSerializer userServiceStorageSerializer, Func<int, int> idGenerator,
            int seed)
        {
            AppDomainSetup appDomainSetup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Master")
            };

            _masterDomain = AppDomain.CreateDomain
                ("Master", null, appDomainSetup);

            IPEndPoint ipEndPoint = 
                new IPEndPoint(IPAddress.Parse(UserServiceSettings.Settings.Master.Address), 
                UserServiceSettings.Settings.Master.Port);

            _master = (UserService) _masterDomain.CreateInstanceAndUnwrap
                (Assembly.GetExecutingAssembly().FullName, typeof (UserService).FullName,
                    false, BindingFlags.CreateInstance, null, new object[]
                    {
                        true, ipEndPoint, logger, userValidator,
                        userServiceStorageSerializer, idGenerator,
                        seed
                    }, null, null);
        }

        private void CreateSlavesCollection(ILogger logger, IUserValidator userValidator,
            IUserServiceStorageSerializer userServiceStorageSerializer, Func<int, int> idGenerator,
            int seed = 0)
        {
            foreach (UserServiceConfiguration userServiceConfiguration in UserServiceSettings.Settings.Slaves)
            {
                IPEndPoint ipEndPoint =
                    new IPEndPoint(IPAddress.Parse(userServiceConfiguration.Address),
                        userServiceConfiguration.Port);
                CreateSlave(ipEndPoint, logger, userValidator, userServiceStorageSerializer,
                    idGenerator, seed);
            }
        }

        private void CreateSlave(IPEndPoint ipEndPoint, ILogger logger, IUserValidator userValidator,
            IUserServiceStorageSerializer userServiceStorageSerializer, Func<int, int> idGenerator,
            int seed)
        {
            AppDomainSetup appDomainSetup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Slave")
            };

            AppDomain slaveDomain = AppDomain.CreateDomain
                ("Slave", null, appDomainSetup);

            _slavesDomains.Add(slaveDomain);

            IUserService slave = (UserService)slaveDomain.CreateInstanceAndUnwrap
                (Assembly.GetExecutingAssembly().FullName, typeof(UserService).FullName,
                    false, BindingFlags.CreateInstance, null, new object[]
                    {
                        false, ipEndPoint, logger, userValidator,
                        userServiceStorageSerializer, idGenerator,
                        seed
                    }, null, null);

            _slaves.Add(slave);
        }

        #endregion
    }
}
