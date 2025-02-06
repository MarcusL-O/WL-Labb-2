using MongoDB.Driver;
using Wl_labb2.Models;
using Microsoft.Extensions.Configuration;

namespace Wl_labb2.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Snus> _snusCollection;

        public MongoDbService(IConfiguration config)
        {
            var connectionString = Environment.GetEnvironmentVariable("MongoDB_ConnectionString")
                                   ?? config["MongoDB:ConnectionString"]; // 🔥 Läser både från Azure och lokalt

            var databaseName = Environment.GetEnvironmentVariable("MongoDB_DatabaseName")
                               ?? config["MongoDB:DatabaseName"];

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _snusCollection = mongoDatabase.GetCollection<Snus>("SnusItems");
        }

        public async Task<List<Snus>> GetAllAsync() =>
            await _snusCollection.Find(_ => true).ToListAsync();

        public async Task<Snus?> GetByIdAsync(string id) =>
            await _snusCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Snus snus) =>
            await _snusCollection.InsertOneAsync(snus);

        public async Task UpdateAsync(string id, Snus updatedSnus) =>
            await _snusCollection.ReplaceOneAsync(x => x.Id == id, updatedSnus);

        public async Task DeleteAsync(string id) =>
            await _snusCollection.DeleteOneAsync(x => x.Id == id);
    }
}
