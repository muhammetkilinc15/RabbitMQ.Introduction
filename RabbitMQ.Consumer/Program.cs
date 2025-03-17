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

// 3- Kuyruk Dinleme
await channel.QueueDeclareAsync(queue: "example-queue", true, exclusive: false, false, null);

// 4- Mesaj Alımı
AsyncEventingBasicConsumer consumer = new(channel);
await channel.BasicConsumeAsync(queue: "example-queue1", autoAck: false, consumer: consumer);
consumer.ReceivedAsync += async (sender, eventArgs) =>
{
    byte[] body = eventArgs.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Gelen Mesaj: " + message);
    await channel.BasicAckAsync(eventArgs.DeliveryTag, false);
    await channel.BasicAckAsync(deliveryTag: eventArgs.DeliveryTag, multiple: false);
};

await channel.BasicConsumeAsync("example-queue", true, consumer);

// 5- Bağlantıyı Kapatma
Console.ReadLine();
await channel.CloseAsync();