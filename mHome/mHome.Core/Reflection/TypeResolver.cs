using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace mHome.Core.Reflection {
	[PublicAPI]
    public interface ITypeResolver<out T> {
        IEnumerable<TypeInfo> GetKnownTypes();
        T[] GetKnownInstances();
    }
    
    [PublicAPI]
    public class TypeResolver<T> : ITypeResolver<T> {
        private TypeInfo[]? _types;
        private T[]? _instances;
        private readonly object _lock = new();

        private readonly Assembly _assembly;

        #region Constructor methods
        public TypeResolver(Assembly assembly) {
            _assembly = assembly;
        }
        #endregion
        
        #region ITypeResolver implementation
        public IEnumerable<TypeInfo> GetKnownTypes() {
            lock (_lock) {
                if (_types != null) return _types;
                
                _types = _assembly.DefinedTypes
                    .Where(t => t.ImplementedInterfaces.Contains(typeof(T)))
                    .Where(t => !t.IsAbstract)
                    .Where(t => !t.ImplementedInterfaces.Contains(typeof(INoResolve)))
                    .ToArray();

                return _types;
            }
        }

        public T[] GetKnownInstances() {
            var types = GetKnownTypes();
            lock (_lock) {
                _instances = types
                    .Select(t => (T)Activator.CreateInstance(t)!)
                    .ToArray();

                return _instances;
            }
        }
        #endregion
    }
}