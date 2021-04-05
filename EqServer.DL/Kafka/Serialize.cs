using Confluent.Kafka;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace EqServer.DL.Kafka
{
    public class MsgPackSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return MessagePackSerializer.Serialize<T>(data);
        }
    }
}
