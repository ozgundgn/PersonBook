

using EventRabbitMQ.Events;

namespace RabbitMq.Test.Models
{
    public class EventTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new ReportCreatedEvent { Uuid = Guid.NewGuid(), Location = "deneme", PersonsCount = 1, PhoneNumbersCount = 2,CreationDate=DateTime.Now.Date, } };

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
