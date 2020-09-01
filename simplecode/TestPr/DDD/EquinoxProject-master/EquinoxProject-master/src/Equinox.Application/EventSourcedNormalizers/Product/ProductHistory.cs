using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Equinox.Application.EventSourcedNormalizers.Customer;
using Equinox.Domain.Core.Events;

namespace Equinox.Application.EventSourcedNormalizers.Product
{
    public static class ProductHistory
    {
        public static IList<ProductHistoryData> HistoryData { get; set; }

        public static IList<ProductHistoryData> ToJavaScriptProductHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<ProductHistoryData>();
            ProductHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<ProductHistoryData>();
            var last = new ProductHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new ProductHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Price = string.IsNullOrWhiteSpace(change.Price) || change.Price == last.Price
                        ? ""
                        : change.Price,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    Timestamp = change.Timestamp,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void ProductHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<ProductHistoryData>(e.Data);
                historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "ProductRegisteredEvent":
                        historyData.Action = "Registered";
                        historyData.Who = !string.IsNullOrWhiteSpace(e.User) ? e.User : "Anonymous";
                        break;
                    case "ProductUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Who = !string.IsNullOrWhiteSpace(e.User) ? e.User : "Anonymous";
                        break;
                    case "ProductRemovedEvent":
                        historyData.Action = "Removed";
                        historyData.Who = !string.IsNullOrWhiteSpace(e.User) ? e.User : "Anonymous";
                        break;
                    default:
                        historyData.Action = "Unrecognized";
                        historyData.Who = !string.IsNullOrWhiteSpace(e.User) ? e.User : "Anonymous";
                        break;

                }
                HistoryData.Add(historyData);
            }
        }
    }
}