# RabbitMQ

## Message Queue Nedir?
Message Queue, yazılım sistemlerinde iletişim için kullanılan bir yapıdır. Birbirinden bağımsız sistemler arasında veri alışverişi yapmak için kullanılır. Gönderilen mesajları kuyrukta saklar ve sonradan işlenmesini sağlar.

- **Producer (Publisher):** Mesajları kuyruğa ekleyen bileşen.
- **Consumer:** Kuyruktaki mesajları işleyen bileşen.

## Message Queue'nun Amacı
Bazı senaryolarda sistemler arası senkron haberleşmek kullanıcı deneyimi açısından uygun olmayabilir. Örneğin, bir e-ticaret uygulamasında ödeme sonrası fatura oluşturma işlemi, kullanıcıyı bekletmek yerine asenkron bir şekilde gerçekleştirilebilir.

## Senkron & Asenkron İletişim
- **Senkron:** İstek yapan sistem, yanıt alana kadar bekler.
- **Asenkron:** İstek yapan sistem, yanıt beklemeden işlemi devam ettirir.

## Message Broker Nedir?
Mesaj kuyruğunu yöneten ve Publisher-Consumer arasındaki iletişimi sağlayan sistemdir. Örnek teknolojiler:
- RabbitMQ
- Kafka
- ActiveMQ
- ZeroMQ
- NSQ

## RabbitMQ Nedir?
- Açık kaynaklı bir Message Queue sistemidir.
- Erlang diliyle geliştirilmiştir.
- Platform bağımsızdır.
- Zengin dokümantasyona sahiptir.
- Cloud ortamında hizmeti mevcuttur.

## RabbitMQ Kullanım Senaryoları
- **Ölçeklenebilirlik**: Büyük yük altındaki sistemlerde iş yükünü bölmek için kullanılır.
- **Asenkron İşlemler**: Kullanıcıyı bekletmeden uzun süren işlemleri arka planda yapmak için uygundur.
- **Bağımsız Servisler**: Mikroservis mimarisinde farklı servislerin haberleşmesini sağlar.

## Cloud AMQP
Ücretsiz RabbitMQ ortamı oluşturmak için: [CloudAMQP](https://customer.cloudamqp.com/login)

## RabbitMQ Bileşenleri
### 1. Exchange
Mesajları belirli kurallara göre kuyruğa yönlendirir.

### 2. Binding
Exchange ile Queue arasında bağlantıyı kurar.

### 3. Exchange Türleri
#### **Direct Exchange**
Mesajlar, belirli bir routing key'e sahip olan kuyruğa yönlendirilir. Örnek: Sipariş durumu yönetimi.

#### **Fanout Exchange**
Mesajlar, routing key'e bakılmaksızın tüm bağlı kuyruklara dağıtılır. Örnek: Bildirim sistemleri.

#### **Topic Exchange**
Mesajlar belirli pattern'lere göre ilgili kuyruğa yönlendirilir. Örnek: Log yönetimi.

#### **Headers Exchange**
Mesajlar, başlık bilgilerine göre ilgili kuyruğa yönlendirilir.

## RabbitMQ C# Örnekleri
### 1. RabbitMQ.Client Kütüphanesi Yükleme
```sh
dotnet add package RabbitMQ.Client --version 7.1.1
```

### 2. Publisher (Üretici)
```csharp
using System;
using System.Text;
using RabbitMQ.Client;

class Program {
    static void Main() {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel()) {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = "Merhaba RabbitMQ!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine("[x] Gönderildi: {0}", message);
        }
    }
}
```

### 3. Consumer (Tüketici)
```csharp
using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class Program {
    static void Main() {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel()) {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) => {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("[x] Alındı: {0}", message);
            };
            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);
            Console.WriteLine("[*] Mesajlar bekleniyor...");
            Console.ReadLine();
        }
    }
}
```

## RabbitMQ ile Gelişmiş Kuyruk Mimarisi
- **Round-Robin Dispatching:** Mesajlar, worker process'ler arasında eşit olarak dağıtılır.
- **Message Acknowledgement:** Mesajın başarıyla işlendiğini doğrulamak için kullanılır.
- **Durable Queue:** Mesajların kalıcı olması için kuyrukların dayanıklı (durable) olarak ayarlanması gerekir.

## Sonuç
RabbitMQ, ölçeklenebilir ve yüksek performanslı sistemler geliştirmek için güçlü bir mesaj kuyruğu çözümüdür. Özellikle mikroservisler, asenkron işlemler ve büyük ölçekli uygulamalar için uygundur.
