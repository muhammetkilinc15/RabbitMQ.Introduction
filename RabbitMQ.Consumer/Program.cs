using RabbitMQ.Client;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

//// Consumer Program.cs



// 1- Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://eoetypiy:O5oUx3bmcItd-i9gf9SFEtn4eDZduJyy@cow.rmq2.cloudamqp.com/eoetypiy");

// 2- Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connetion = await factory.CreateConnectionAsync();
using IChannel channel = await connetion.CreateChannelAsync();


// 3- Exchange Tanımlama
await channel.ExchangeDeclareAsync(exchange: "topic-exchange-example", type: ExchangeType.Topic);


// 4- Queue Tanımlama
QueueDeclareOk resultQueue = await channel.QueueDeclareAsync();




Console.Write("Dinlenecek topik: ");
string? topic = Console.ReadLine();
string queueName = resultQueue.QueueName;

// 5- Queue ile Exchange Bagla
await channel.QueueBindAsync(
    queue: queueName, 
    exchange: "topic-exchange-example", 
    routingKey: topic!);


AsyncEventingBasicConsumer consumer = new(channel);
await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer);

consumer.ReceivedAsync += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
    return Task.CompletedTask;
};


Console.Read();