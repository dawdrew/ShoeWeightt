using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite;

using ShoeWeightt.Models;

namespace ShoeWeightt.Data


{
    public class ItemDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ItemDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Item>().Wait();
        }
        public Task<List<Item>> GetItemAsync()
        {
            return _database.Table<Item>().ToListAsync();
        }
        
        public Task<Item> GetItemAsync(int id)
        {
            return _database.Table<Item>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Item item)
        {
            if (item.Id != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
        }
    }

        public Task<int> DeleteItemAsync(Item item)
        {
            return _database.DeleteAsync(item);
        }
    }
}