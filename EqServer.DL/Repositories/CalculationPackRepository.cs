using EqModels.Models;
using EqServer.DL.Interfaces;
using EqServer.EqModels.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EqServer.DL.Repositories
{
    public class CalculationPackRepository : ICalculationPackRepository
    {
        private readonly IMongoCollection<CalculationPack> _calculationPacks;

        public CalculationPackRepository(IOptions<MongoDbConfiguration> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            var database = client.GetDatabase(config.Value.DatabaseName);

            _calculationPacks = database.GetCollection<CalculationPack>("CalculationPack");
        }
    }
}
