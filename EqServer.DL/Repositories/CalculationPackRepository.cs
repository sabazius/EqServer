using EqModels.Models;
using EqServer.DL.Caches;
using EqServer.DL.Interfaces;
using EqServer.EqModels.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EqServer.DL.Repositories
{
    public class CalculationPackRepository : ICalculationPackRepository
    {
        private readonly IMongoCollection<CalculationPack> _calculationPacks;
        private Cache<int, CalculationPack> _cache;

        public CalculationPackRepository(IOptions<MongoDbConfiguration> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            var database = client.GetDatabase(config.Value.DatabaseName);

            _calculationPacks = database.GetCollection<CalculationPack>(nameof(CalculationPack));

            
            _cache = new Cache<int, CalculationPack>();

           

            _cache.Init(_calculationPacks.Find(p => true).ToEnumerable().ToDictionary(p => p.Id));
        }

        public async Task<CalculationPack> Create(CalculationPack calc)
        {
            //await _calculationPacks. InsertOneAsync(calc);

            var result = _cache.GetOrAdd(calc.Id, () => CreatePack(calc));

            return calc;
        }

        public async Task<CalculationPack> Get(int id)
        {
            var result = await _calculationPacks.FindAsync(x => x.Id == id);

            return result.FirstOrDefault();
        }

        private CalculationPack CreatePack(CalculationPack calc)
        {
             _calculationPacks.InsertOneAsync(calc);

            return calc;
        }

    }
}
