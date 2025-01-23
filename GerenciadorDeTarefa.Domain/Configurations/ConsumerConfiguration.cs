using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefa.Domain.Configurations
{
    public class ConsumerConfiguration
    {
        public bool AutoAck { get; set; } = false;
        public bool NoLocal { get; set; } = false;
        public bool Exclusive { get; set; } = false;
        public IDictionary<string, object> Arguments { get; set; } = null;
        public string ConsumerTag => Guid.NewGuid().ToString();
    }
}