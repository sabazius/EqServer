using System;
using System.Collections.Generic;
using System.Text;

namespace EqServer.DL.DataFlow
{
    public interface IResultConsumerDataFlow
    {
        void ProcessMessage(byte[] msg);
    }
}
