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
do
{
    // 4- Mesaj Gönderme
    Console.Write("Enter message (type 'exit' to quit): ");
    input = Console.ReadLine();
    if (!string.IsNullOrEmpty(input) && !input.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
    {
        byte[] message = Encoding.UTF8.GetBytes(input);
        await channel.BasicPublishAsync(exchange: "", routingKey: "example-queue", body: message);
    }
} while (!string.IsNullOrEmpty(input) && input.ToLower() != "exit");

// 5- Bağlantıyı Kapatma
await channel.CloseAsync();