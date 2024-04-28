﻿
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using ServiceBusQueue;

string connectionString = "Endpoint=sb://app-namespace1000.servicebus.windows.net/;SharedAccessKeyName=orderpolicy;SharedAccessKey=rnH2u8ANdxX1AusoFGNSV/52MMnP7sukKCVR1cXHLnI=;EntityPath=orders";
string queueName = "orders";

List<Order> orders = new List<Order>()
    {
        new Order(){OrderID="01",Quantity=100,UnitPrice=9.99F},
        new Order(){OrderID="02",Quantity=200,UnitPrice=10.99F},
        new Order(){OrderID="03",Quantity=300,UnitPrice=8.99F}
    };


await SendMessage(orders);

async Task SendMessage(List<Order> orders)
{
    ServiceBusClient serviceBusClient = new ServiceBusClient(connectionString);
    ServiceBusSender serviceBusSender= serviceBusClient.CreateSender(queueName);

    // Add the messages to a batch
    ServiceBusMessageBatch serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync();
    foreach (Order order in orders)    
    {
        ServiceBusMessage serviceBusMessage = new ServiceBusMessage(JsonConvert.SerializeObject(order));
        serviceBusMessage.TimeToLive = TimeSpan.FromSeconds(30);

        if (!serviceBusMessageBatch.TryAddMessage(
            serviceBusMessage))
        {
            throw new Exception("Issue with the message size");
        }
    }

    Console.WriteLine("Sending messages");
    await serviceBusSender.SendMessagesAsync(serviceBusMessageBatch);
    
    await serviceBusSender.DisposeAsync();
    await serviceBusClient.DisposeAsync();
}