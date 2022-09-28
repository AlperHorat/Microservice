# Microservice
Mikroservis mimarisi kullanılarak 2 adet servis projesi 1 adet console application projesi
ile rehbere kişi tanımlama ve kişiye iletişim(Email,telefon,konum) bilgilerini tanımlamaya yarayan ve
bu verilerin raporlanmasını kuyruk mekanizması kullanarak sağlayan bir projedir.

# Nasıl Kullanılır
-Contact Servisi ilk çalıştığında karşımıza swagger ekranı gelmektedir.
Bu ekrandan ilgili servisler kullanılarak kişi ekleme, kişi silme, kişiye iletişim bilgisi ekleme vb. faaliyetler yapılabilmektedir.

-Report Servisi ilk çalıştığında karşımıza swagger ekranı gelmektedir.
Bu ekrandan rapor oluşturma talebi, raporların listelenmesi, raporun detayı gibi faaliyetler yapılabilmektedir.

-ConsoleApplication direk çalıştığı andan itibaren gelebilecek rapor taleplerini dinlemeye başlamaktadır. Eğer ki rapor talebi
geldiği durumda süreçleri ilerletmektedir.

# Raporlama Süreci
-Sistemde gerekli datalar tanımlandıktan sonra rehberden rapor çekilmesi istenildiğinde
raporlarda darboğaz yaratmadan kuyruk mekanizması kullanılarak raporlamalar sonuçlandırılmaktadır.

-Sistemin en doğru şekilde çalışması için 3 projenin de çoklu çalıştırılması gerekmektedir.

-Consolapplication projesi çalışırken her an rapor taleplerini dinlemeye başlamaktadır.

-ReportService swagger dokümanı üzerinden "addreportrequest" servisi çalıştırıldığında rapor isteği talebi rapor talepleri kuyruğuna iletilir.

-Oluşturulan talep consol uygulaması tarafından kuyruktaki sıra ile algılanır ve rapor talebine istinaden ContactServise ile iletişime
geçerek gerekli dataları çeker.

-Consol uygulaması gelen datalar ile excel tablosu oluşturur ve kaydettiği yolu talepdatası üzerinde güncelleyerek
rapora dair dataları oluşturulan raporlar kuyruğuna gönderir.

-Oluşturulan raporlar kuyruğunda gelen rapor bilgileri incelenir ve rapor süreci tamamlandı olarak ilgili alanlar güncellenir ve rapor süreci tamamlanır.

 # Teknik bilgiler
 
 -2 adet servis projesi .NET CORE WEB API kullanılarak yazılmıştır.
 
 -1 adet consolapplication projesi .NET CORE CONSOLEAPPLICATION kullanılarak yazılmıştır.
 
 -1 adet unittest projesi .NET CORE XUNITTEST kullanılarak yazılmıştır.
 
 -Message Broker olarak RabbitMQ kullanılmıştır.
 
 -2 servis için 2 ayrı veritabanı PostgreSQL kullanılmıştır.
