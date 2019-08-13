using System;
using System.Collections.Generic;
using System.Text;

namespace Producer.EventGrid
{
    /// <summary>
    /// This class models the schema required by EventGrid
    /// </summary>
    /// <typeparam name="T">The type of the Event Data</typeparam>
    public class EventGridEvent<T>
        where T : class
    {
        public string Id = Guid.NewGuid().ToString();

        public string Subject;

        public string EventType;

        public string EventTime;

        public T Data;
    }
}
