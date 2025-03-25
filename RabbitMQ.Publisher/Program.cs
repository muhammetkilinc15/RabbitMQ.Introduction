// 1- Bağlantı Oluşturma
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://eoetypiy:O5oUx3bmcItd-i9gf9SFEtn4eDZduJyy@cow.rmq2.cloudamqp.com/eoetypiy");

// 2- Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connetion = await factory.CreateConnectionAsync();
using IChannel channel = await connetion.CreateChannelAsync();

// fanout exchange oluşturma
await channel.ExchangeDeclareAsync(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);

// Queue Oluşturma
for(int i=0;i<100; i++)
{
    await Task.Delay(200);
    byte[] body = Encoding.UTF8.GetBytes($"Hello RabbitMQ {i}");
    await channel.BasicPublishAsync(exchange: "fanout-exchange-example", routingKey: string.Empty, body: body);
}

Console.Write("Press any key to exit...");
Console.Read();