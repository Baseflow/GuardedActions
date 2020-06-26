using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace GuardedActions.MvvmCross
{
    public class MvxServiceCollection : IServiceCollection
    {
        public MvxServiceCollection(
            )
        {
        }

        public ServiceDescriptor this[int index] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public int Count => throw new System.NotImplementedException();

        public bool IsReadOnly => throw new System.NotImplementedException();

        public void Add(ServiceDescriptor item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(ServiceDescriptor item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(ServiceDescriptor item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
