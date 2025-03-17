// 1- Bağlantı Oluşturma
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://eoetypiy:O5oUx3bmcItd-i9gf9SFEtn4eDZduJyy@cow.rmq2.cloudamqp.com/eoetypiy");

// 2- Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connection = await factory.CreateConnectionAsync();
using IChannel channel = await connection.CreateChannelAsync();

// 3- Kuyruk Oluşturma
await channel.QueueDeclareAsync(queue: "hello", true, exclusive: false, false, null);

string input;

// 4- Mesaj Gönderme
for(int i = 0; i < 50; i++)
{
    input = $"Hello World! {i}";
    byte[] message = Encoding.UTF8.GetBytes(input);
    await channel.BasicPublishAsync(exchange: "", routingKey: "example-queue1", body: message);

}


// 5- Bağlantıyı Kapatma
await channel.CloseAsync();