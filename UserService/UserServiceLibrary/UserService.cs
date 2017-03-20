using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Logger;
using UserServiceLibrary.Configuration;
using UserServiceLibrary.Exceptions;
using UserServiceLibrary.Interfaces;

namespace UserServiceLibrary
{
    /// <summary>
    /// Represents a user service that allows to perform read and write operations on user storage.
    /// Allows working in master and slave mode.
    /// </summary>
    [Serializable]
    public class UserService : MarshalByRefObject, IUserService
    {
        #region Constants and Fields

        private static readonly Func<int, int> DefaultIdGenerator =
            currentId => ++currentId;

        private readonly ILogger _logger;
        private readonly IUserValidator _userValidator;
        private UserServiceStorage _userServiceStorage;
        private readonly IUserServiceStorageSerializer _userServiceStorageSerializer;
        private readonly Func<int, int> _idGenerator;
        private readonly ReaderWriterLockSlim _rwLockSlim;
        private readonly IPEndPoint _ipEndPoint;
        private bool _isMaster;
        private readonly ManualResetEventSlim _suspendSlaveListenerEvent;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets value indicating whether service is master.
        /// </summary>
        public bool IsMaster
        {
            get { return _isMaster; }
            set
            {
                _isMaster = value;
                if (LoggingSettings.Settings.IsEnabled)
                {
                    if (_isMaster)
                    {
                        _suspendSlaveListenerEvent.Reset();
                        _logger.Info("Service was switched to the master mode.");
                    }
                    else
                    {
                        _suspendSlaveListenerEvent.Set();
                        _logger.Info("Service was switched to the slave mode.");
                    }
                }
                
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new service instance in a slave mode.
        /// </summary>
        /// <param name="ipEndPoint">IP EndPoint of a service. Is mandatory.</param>
        public UserService(IPEndPoint ipEndPoint) : this(false, ipEndPoint, null, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new service instance.
        /// </summary>
        /// <param name="isMaster">Indicates whether service is in master mode.</param>
        /// <param name="ipEndPoint">IP EndPoint of a service. Is mandatory.</param>
        public UserService(bool isMaster, IPEndPoint ipEndPoint) : this(isMaster, ipEndPoint, null, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new service instance in a slave mode.
        /// </summary>
        /// <param name="ipEndPoint">IP EndPoint of a service. Is mandatory.</param>
        /// <param name="logger">Logger instance. If value is null - the default logger instance is used.</param>
        public UserService(IPEndPoint ipEndPoint, ILogger logger) 
            : this(false, ipEndPoint, logger, null, null, null)
        {
        }



        /// <summary>
        /// Initializes a new service instance in a slave mode.
        /// </summary>
        /// <param name="ipEndPoint">IP EndPoint of a service. Is mandatory.</param>
        /// <param name="logger">Logger instance. If value is null - the default logger instance is used.</param>
        /// <param name="userValidator">An instance of a class implementing user validating API. If value is null - the default user validator instance is used.</param>
        /// <param name="userServiceStorageSerializer">An instance of a class implementing user storage serializing API. If value is null - the default user serializer instance is used.</param>
        public UserService(IPEndPoint ipEndPoint, ILogger logger, IUserValidator userValidator, 
            IUserServiceStorageSerializer userServiceStorageSerializer)
            : this(false, ipEndPoint, logger, userValidator, userServiceStorageSerializer, null)
        {
        }

        /// <summary>
        /// Initializes a new service instance in a slave mode.
        /// </summary>
        /// <param name="ipEndPoint">IP EndPoint of a service. Is mandatory.</param>
        /// <param name="logger">Logger instance. If value is null - the default logger instance is used.</param>
        /// <param name="idGenerator">Function designed to generate user id. If value is null - the default id generating function is used.</param>
        /// <param name="seed">Initial value for id generating function. Is optional.</param>
        public UserService(IPEndPoint ipEndPoint, ILogger logger,
            Func<int, int> idGenerator, int seed = 0)
            : this(false, ipEndPoint, logger, null, null, idGenerator, seed)
        {
        }

        /// <summary>
        /// Initializes a new service instance.
        /// </summary>
        /// <param name="isMaster">Indicates whether service is in master mode.</param>
        /// <param name="ipEndPoint">IP EndPoint of a service. Is mandatory.</param>
        /// <param name="logger">Logger instance. If value is null - the default logger instance is used.</param>
        /// <param name="userValidator">An instance of a class implementing user validating API. If value is null - the default user validator instance is used.</param>
        /// <param name="userServiceStorageSerializer">An instance of a class implementing user storage serializing API. If value is null - the default user serializer instance is used.</param>
        /// <param name="idGenerator">Function designed to generate user id. If value is null - the default id generating function is used.</param>
        /// <param name="seed">Initial value for id generating function. Is optional.</param>
        public UserService(bool isMaster, IPEndPoint ipEndPoint, 
            ILogger logger, IUserValidator userValidator,
            IUserServiceStorageSerializer userServiceStorageSerializer, Func<int, int> idGenerator,
            int seed = 0)
        {
            _isMaster = isMaster;
            if (ipEndPoint == null)
            {
                Exception e = new ArgumentNullException(nameof(ipEndPoint));

                if (LoggingSettings.Settings.IsEnabled)
                {
                    _logger.Error(e, "An attempt of a creating service with null IP endpoint.");
                }

                throw e;
            }
            _ipEndPoint = ipEndPoint;
            _idGenerator = idGenerator ?? DefaultIdGenerator;
            _userValidator = userValidator ?? new UserValidator();
            _logger = logger ?? NLogLogger.Instance;
            _userServiceStorageSerializer = userServiceStorageSerializer ?? new UserServiceStorageSerializer();
            _userServiceStorage = new UserServiceStorage
            {
                CurrentId = seed,
                UserCollection = new List<User>()
            };
            _rwLockSlim = new ReaderWriterLockSlim();

            new Thread(ListenMaster).Start();
            _suspendSlaveListenerEvent = new ManualResetEventSlim(!_isMaster);

            if (LoggingSettings.Settings.IsEnabled)
            {
                _logger.Info("New user service was created.");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add a new user to a user storage. Available only for services running in master mode.
        /// </summary>
        /// <param name="user">An instance of a User class to add.</param>
        public void Add(User user)
        {
            if (!_isMaster)
            {
                Exception e = new OperationAccessDeniedException("Slave service doesn`t support add operation.");

                if (LoggingSettings.Settings.IsEnabled)
                {
                    _logger.Error(e, e.Message);
                }

                throw e;
            }

            if (user == null)
            {
                Exception e = new ArgumentNullException(nameof(user));

                if (LoggingSettings.Settings.IsEnabled)
                {
                    _logger.Error(e, "An attempt of a null user addition.");
                }

                throw e;
            }

            if (!_userValidator.IsValid(user))
            {
                Exception e = new InvalidUserException("User is not valid.");

                if (LoggingSettings.Settings.IsEnabled)
                {
                    _logger.Error(e, "An attempt of an invalid user addition.");
                }

                throw e;
            }

            _userServiceStorage.CurrentId = _idGenerator(_userServiceStorage.CurrentId);

            _rwLockSlim.EnterUpgradeableReadLock();

            try
            {
                if (_userServiceStorage.UserCollection.FirstOrDefault(u => u.Id == _userServiceStorage.CurrentId) != null)
                {
                    Exception e = new UserIdAlreadyExistsException("User id already exists.");

                    if (LoggingSettings.Settings.IsEnabled)
                    {
                        _logger.Error(e, "An attempt of an existing user addition.");
                    }

                    throw e;
                }

                user.Id = _userServiceStorage.CurrentId;

                _rwLockSlim.EnterWriteLock();
                try
                {
                    _userServiceStorage.UserCollection.Add(user);

                    if (LoggingSettings.Settings.IsEnabled)
                    {
                        _logger.Info($"A new user with ID = {user.Id} was added.");
                    }
                }
                finally
                {
                    _rwLockSlim.ExitWriteLock();
                }

            }
            finally
            {
                _rwLockSlim.ExitUpgradeableReadLock();
            }    
            
            NotifySlaves(new StorageUpdateNotification {UpdateType = UpdateType.Add, User = user.Clone()});      
        }

        /// <summary>
        /// Delete a user from a user storage. Available only for services running in master mode.
        /// </summary>
        /// <param name="user">An instance of a User class to delete.</param>
        public void Delete(User user)
        {
            if (!_isMaster)
            {
                Exception e = new OperationAccessDeniedException("Slave service doesn`t support delete operation.");

                if (LoggingSettings.Settings.IsEnabled)
                {
                    _logger.Error(e, e.Message);
                }

                throw e;
            }

            if (user == null)
            {
                Exception e = new ArgumentNullException(nameof(user));

                if (LoggingSettings.Settings.IsEnabled)
                {
                    _logger.Error(e, "An attempt of a null user deleting.");
                }

                throw e;
            }

            _rwLockSlim.EnterUpgradeableReadLock();

            try
            {
                if (_userServiceStorage.UserCollection.Contains(user))
                {
                    Exception e = new UserNotFoundException("User not found.");

                    if (LoggingSettings.Settings.IsEnabled)
                    {
                        _logger.Error(e, "An attempt of a nonexistent user deleting.");
                    }

                    throw e;
                }

                user.Id = _userServiceStorage.CurrentId;

                _rwLockSlim.EnterWriteLock();
                try
                {
                    _userServiceStorage.UserCollection.Remove(user);
                    if (LoggingSettings.Settings.IsEnabled)
                    {
                        _logger.Info($"A user with ID = {user.Id} was deleted.");
                    }
                }
                finally
                {
                    _rwLockSlim.ExitWriteLock();
                }

            }
            finally
            {
                _rwLockSlim.ExitUpgradeableReadLock();
            }

            NotifySlaves(new StorageUpdateNotification { UpdateType = UpdateType.Delete, User = user.Clone() });
        }

        /// <summary>
        /// Search users by a predicate.
        /// </summary>
        /// <param name="predicate">Function that defines a criterion for search.</param>
        /// <returns>Users instances that meet the criterion of search.</returns>
        public IEnumerable<User> Search(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                Exception e = new ArgumentNullException(nameof(predicate));

                if (LoggingSettings.Settings.IsEnabled)
                {
                    _logger.Error(e, "An attempt of search by a null predicate.");
                }

                throw e;
            }

            IEnumerable<User> users;

            _rwLockSlim.EnterReadLock();

            try
            {
                users = _userServiceStorage.UserCollection.Where(u => predicate(u)).ToList();

            }
            finally
            {
                _rwLockSlim.ExitReadLock();
            }

            return users;

        }

        /// <summary>
        /// Save state of service to a persistent user storage. Available only for services running in master mode.
        /// </summary>
        public void SaveState()
        {
            if (!_isMaster)
            {
                Exception e = new OperationAccessDeniedException("Slave service doesn`t support save state operation.");

                if (LoggingSettings.Settings.IsEnabled)
                {
                    _logger.Error(e, e.Message);
                }

                throw e;
            }

            try
            {
                _rwLockSlim.EnterReadLock();
                _userServiceStorageSerializer.Serialize(_userServiceStorage);
            }
            catch (Exception e)
            {
                Exception eWrapped = new UserServiceException("Error saving service state.", e);
                _logger.Error(eWrapped, eWrapped.Message + eWrapped.InnerException.Message);
                throw eWrapped;
            }
            finally
            {
                _rwLockSlim.ExitReadLock();
            }
        }

        /// <summary>
        /// Load state from persistent user storage. Available only for services running in master mode.
        /// </summary>
        public void LoadState()
        {
            if (!_isMaster)
            {
                Exception e = new OperationAccessDeniedException("Slave service doesn`t support load state operation.");

                if (LoggingSettings.Settings.IsEnabled)
                {
                    _logger.Error(e, e.Message);
                }

                throw e;
            }

            try
            {
                _rwLockSlim.EnterWriteLock();
                _userServiceStorage = _userServiceStorageSerializer.Deserialize();
            }
            catch (Exception e)
            {
                Exception eWrapped = new UserServiceException("Error loading service state.", e);
                _logger.Error(eWrapped, eWrapped.Message + eWrapped.InnerException.Message);
                throw eWrapped;
            }
            finally
            {
                _rwLockSlim.ExitWriteLock();
            }

            NotifySlaves(new StorageUpdateNotification
            {
                UpdateType = UpdateType.Restore,
                UserCollection = _userServiceStorage.UserCollection.Select(u => u.Clone()).ToList()
            });
        }

        #endregion

        #region Private Methods

        private void NotifySlaves(StorageUpdateNotification storageUpdateNotification)
        {
            if (!_isMaster)
            {
                Exception e = new OperationAccessDeniedException("Slave service doesn`t support notify operation.");

                if (LoggingSettings.Settings.IsEnabled)
                {
                    _logger.Error(e, e.Message);
                }

                throw e;
            }

            BinaryFormatter formatter = new BinaryFormatter();

            foreach (UserServiceConfiguration slaveServiceConfiguration in UserServiceSettings.Settings.Slaves)
            {
                using (TcpClient client = new TcpClient(slaveServiceConfiguration.Address, slaveServiceConfiguration.Port))
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        formatter.Serialize(stream, storageUpdateNotification);
                    }
                }
            }
        }

        private void ListenMaster()
        {           
            TcpListener listener = new TcpListener(_ipEndPoint.Address, _ipEndPoint.Port);

            try
            {

                listener.Start();

                while (true)
                {
                    _suspendSlaveListenerEvent.Wait();

                    if (_isMaster)
                    {
                        Exception e = new OperationAccessDeniedException("Master service doesn`t support listen operation.");

                        if (LoggingSettings.Settings.IsEnabled)
                        {
                            _logger.Error(e, e.Message);
                        }

                        throw e;
                    }

                    BinaryFormatter formatter = new BinaryFormatter();

                    TcpClient client = listener.AcceptTcpClient();

                    using (NetworkStream stream = client.GetStream())
                    {
                        StorageUpdateNotification storageUpdateNotification =
                            (StorageUpdateNotification)formatter.Deserialize(stream);


                        _rwLockSlim.EnterWriteLock();

                        try
                        {
                            switch (storageUpdateNotification.UpdateType)
                            {
                                case UpdateType.Add:
                                {
                                    _userServiceStorage.UserCollection.Add(storageUpdateNotification.User);

                                    if (LoggingSettings.Settings.IsEnabled)
                                    {
                                        _logger.Info(
                                            $"A new user with ID = {storageUpdateNotification.User.Id} was added.");
                                    }
                                    break;
                                }
                                case UpdateType.Delete:
                                {
                                    _userServiceStorage.UserCollection.Remove(storageUpdateNotification.User);
                                    if (LoggingSettings.Settings.IsEnabled)
                                    {
                                        _logger.Info(
                                            $"A user with ID = {storageUpdateNotification.User.Id} was deleted.");
                                    }
                                    break;
                                }
                                case UpdateType.Restore:
                                {
                                    _userServiceStorage.UserCollection = storageUpdateNotification.UserCollection;
                                    if (LoggingSettings.Settings.IsEnabled)
                                    {
                                        _logger.Info(
                                            "Storage was restored");
                                    }
                                    break;
                                }
                            }
                        }
                        finally
                        {
                            _rwLockSlim.ExitWriteLock();
                        }
                    }

                    client.Close();
                }
            }
            catch (Exception e)
            {
                Exception eWrapped = new UserServiceException("Error listening master.", e);
                _logger.Error(eWrapped, eWrapped.Message + eWrapped.InnerException.Message);
                throw eWrapped;
            }
            finally
            {
                listener.Stop();
            }
        }

        #endregion
    }
}
