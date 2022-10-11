using System;
using EventRabbitMQ.Events.Interfaces;

namespace EventRabbitMQ.Events
{
    public class ReportCreatedEvent : IEvent
    {
        public string Location { get; set; }
        public int PersonsCount { get; set; }
        public int PhoneNumbersCount { get; set; }
        public Guid Uuid { get; set; }

    }
}
