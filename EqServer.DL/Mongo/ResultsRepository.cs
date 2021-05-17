using EqModels.Models;
using EqServer.EqModels.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EqServer.DL.Mongo
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoConfig;
        private IMongoCollection<CalculationPack> _calculationPacks;
        public ResultsRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            _mongoConfig = mongoConfig;

            var client = new MongoClient(_mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _calculationPacks = database.GetCollection<CalculationPack>("Results");
        }

        public async Task<CalculationPack> Save(CalculationPack pack)
        {
            await _calculationPacks.InsertOneAsync(pack);

            return pack;
        }

        public async Task<IEnumerable<CalculationPack>> GetAll()
        {
            var result = await _calculationPacks.FindAsync(x => true);

            return result.ToEnumerable();
        }
    }
}
