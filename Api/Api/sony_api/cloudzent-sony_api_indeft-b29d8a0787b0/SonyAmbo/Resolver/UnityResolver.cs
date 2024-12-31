using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Web.Http.Dependencies;


namespace SonyAmbo.Resolver
{
    /// <summary>
    /// Dependency Resolver
    /// </summary>
    public class UnityResolver : IDependencyResolver
    {
        /// <summary>
        /// 
        /// </summary>
        protected IUnityContainer _container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this._container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDependencyScope BeginScope()
        {
            var dependentChind = _container.CreateChildContainer();
            return new UnityResolver(dependentChind);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException) //ResolutionFailedException
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

    }
}