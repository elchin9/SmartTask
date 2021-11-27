using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace SmartSolution.SharedKernel.Infrastructure.IntegrationEvents
{
    public abstract class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id { get; }

        public DateTime CreationDate { get; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string Type { get; set; }

        public virtual string Serialize()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.SerializeObject(this, settings);
        }
    }
}
