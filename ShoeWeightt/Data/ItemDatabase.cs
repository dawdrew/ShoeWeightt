using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using SQLite;
using ShoeWeightt.Services;
using ShoeWeightt.Models;

namespace ShoeWeightt.Data
{
    public class ItemDatabase : IDataStore<Item>
    { 
        readonly List<Item> items;
        public ItemDatabase()
        {
            items = new List<Item>()
                {
                new Item {Text = "SUCK MY COCK", Description="THIS APP SUCKS ASIAN DICK."},
                //new Item { Id = Guid.NewGuid(int), Text = "Second item", Description="This is an item description." },
                //new Item { Id = Guid.NewGuid(int), Text = "Third item", Description="This is an item description." },
                //new Item { Id = Guid.NewGuid(int), Text = "Fourth item", Description="This is an item description." },
                //new Item { Id = Guid.NewGuid(int), Text = "Fifth item", Description="This is an item description." },
                //new Item { Id = Guid.NewGuid(), Text = "Sixth item", Description="This is an item description." },
                };
            Console.WriteLine(items[0]);
            Console.WriteLine(items[0].Id);
            Console.WriteLine(items[0].Text);
        }

        readonly SQLiteAsyncConnection _database;

        public ItemDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Item>().Wait();
        }
        public async Task<List<Item>> GetItemsAsync()
        {
            List<Item> testing = new List<Item>();
            
            foreach (Item i in await _database.Table<Item>().ToListAsync())
            {
                testing.Add(i);
                Console.WriteLine(i.Id);
                Console.WriteLine(i.Text);
                Console.WriteLine(i.Description);
            }
            return await _database.Table<Item>().ToListAsync();

        }
        
        public Task<Item> GetItemAsync(int id)
        {
            return _database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
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

        public async Task<int> DeleteItemAsync(Item item)
        {
            //items.Remove(item);
            return await _database.DeleteAsync(item);
            //return await Task.FromResult(true);
        }

    }
}