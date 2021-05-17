using EqServer.DL.Kafka;
using EqServer.DL.Mongo;
using EqServer.EqModels.Models;
using MessagePack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace EqServer.DL.DataFlow
{
    public class ResultConsumerDataFlow : IResultConsumerDataFlow
    {
        private readonly ILogger<ResultConsumerDataFlow> _logger;

        private TransformBlock<byte[], CalculationPack> _deserializeBlock;
        private TransformBlock<CalculationPack, CalculationPack> _enqueueBlock;
        private TransformBlock<CalculationPack, CalculationPack> _validatioBlock;
        private ActionBlock<CalculationPack> _publishBlock;

        private IResultsRepository _resultReposutory;

        public ResultConsumerDataFlow(ILogger<ResultConsumerDataFlow> logger, IResultsRepository resultReposutory)
        {
            _logger = logger;

            _resultReposutory = resultReposutory;

            _deserializeBlock = new TransformBlock<byte[], CalculationPack>(msg => MessagePackSerializer.Deserialize<CalculationPack>(msg));

            _enqueueBlock = new TransformBlock<CalculationPack, CalculationPack>(result =>
            {
                ResultData._result.Enqueue(result);
                return result;
            });

            _validatioBlock = new TransformBlock<CalculationPack, CalculationPack>(result =>
            {
                ResultData._result.Enqueue(result);
                var isValid = result.Data[0].Equation.Values[0] * result.Data[0].Equation.Values[1] + result.Data[0].Equation.Values[2] == result.Data[0].Equation.Result;
                return isValid ? null : result;
            });

            _publishBlock = new ActionBlock<CalculationPack>(result =>
            {
                _resultReposutory.Save(result);
            });

            var linkOptions = new DataflowLinkOptions()
            {
                PropagateCompletion = true
            };

            _deserializeBlock.LinkTo(_enqueueBlock, linkOptions);
            _enqueueBlock.LinkTo(_validatioBlock, linkOptions);
            _validatioBlock.LinkTo(_publishBlock, linkOptions);

        }


        public async void ProcessMessage(byte[] msg)
        {
            await _deserializeBlock.SendAsync(msg);
        }
    }
}
