using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeWeightt.Services
{
    public interface IDataStore<T>
    {
        Task<int> SaveItemAsync(T item);
        //Task<bool> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(T item);
        Task<T> GetItemAsync(int id);
        Task<List<T>> GetItemAsync();
    }
}
