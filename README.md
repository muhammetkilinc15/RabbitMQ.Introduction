# RabbitMQ Nedir?

RabbitMQ, açık kaynak kodlu bir mesaj kuyruğu (message broker) sistemidir. AMQP (Advanced Message Queuing Protocol) protokolünü kullanarak çalışan bu sistem, dağıtık yapılar ve mikroservis mimarileri için mesaj iletimi sağlar.

RabbitMQ, farklı bileşenlerin bağımsız çalışmasını ve birbirleriyle asenkron olarak haberleşmesini sağlar. **Kuyruk (Queue)** yapısı sayesinde mesajları geçici olarak saklar ve tüketicilere (consumers) iletir.

---

## 🛠 Ne Durumlarda Kullanılır?

RabbitMQ, aşağıdaki durumlarda tercih edilir:

- **Mikroservis Mimarilerinde:** Servisler arasında asenkron iletişim sağlamak için.
- **Yük Dengeleme (Load Balancing):** Yoğun iş yüklerini farklı tüketicilere bölerek performans iyileştirmesi yapmak için.
- **Olay Tabanlı Mimariler (Event-Driven Architecture):** Olay bazlı sistemlerde veri akışını yönetmek için.
- **Arkaplan İşlemleri (Background Processing):** Ağır işlemleri arka planda çalıştırmak için.
- **Dağıtık Sistemlerde:** Farklı bileşenlerin haberleşmesini sağlamak için.

---

## 🏷 RabbitMQ Türleri

RabbitMQ, **Exchange (Değişim)** tipi ile mesajları yönlendirir. Temel **Exchange Türleri**:

1. **Direct Exchange**  
   - Belirli bir anahtar (routing key) ile mesaj yönlendirilir.
   - **Örnek:** `direct_logs`
   
2. **Fanout Exchange**  
   - Gelen mesajları, tüm bağlı kuyruklara yayınlar (broadcast).
   - **Örnek:** `fanout_logs`
   
3. **Topic Exchange**  
   - Wildcard (`*` ve `#`) ile belirli bir desene uygun mesajları yönlendirir.
   - **Örnek:** `topic_logs`
   
4. **Headers Exchange**  
   - Mesajlar, header bilgilerine göre yönlendirilir.

---

## 🔧 RabbitMQ Kullanımı - Basit Kod Örnekleri

### 1️⃣ RabbitMQ Sunucusunu Çalıştırma

Docker kullanarak RabbitMQ'yu başlatabilirsin:

```sh
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

