// 1- Bağlantı Oluşturma
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://eoetypiy:O5oUx3bmcItd-i9gf9SFEtn4eDZduJyy@cow.rmq2.cloudamqp.com/eoetypiy");

// 2- Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connetion = await factory.CreateConnectionAsync();
using IChannel channel = await connetion.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "direct-exchange", type: ExchangeType.Direct);

// Queue Oluşturma
while (true)
{
    Console.WriteLine("Bir mesaj yazınız");
    string message = Console.ReadLine();
    byte[] body = Encoding.UTF8.GetBytes(message);
    await channel.BasicPublishAsync(exchange: "direct-exchange", routingKey: "direct-queue", body: body);

    if (message == "exit")
    {
        break;
    }
}
Console.Read();