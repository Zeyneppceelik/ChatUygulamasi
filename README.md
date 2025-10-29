💬 Chat Soket Uygulaması

Bu proje, C# dili kullanılarak Visual Studio ortamında geliştirilen bir TCP/IP tabanlı sohbet (chat) uygulamasıdır.
Uygulama, bir Sunucu (Server) ve bir veya daha fazla İstemci (Client) arasında gerçek zamanlı mesajlaşma imkânı sağlar.
Amaç, temel socket programlama mantığını kavramak ve ağ üzerinden veri iletimini pratik olarak göstermek.

🧠 Proje Amacı

Bu projenin temel amacı, istemci-sunucu mimarisi ve soket tabanlı iletişim yapısını anlamaktır.
Uygulama, ağ üzerinden veri gönderme, alma ve yönetme süreçlerini öğretmeyi hedefler.

⚙️ Özellikler

✅ TCP/IP tabanlı haberleşme
✅ Gerçek zamanlı mesaj alışverişi
✅ Aynı anda birden fazla istemci bağlantısı
✅ Sunucu tarafında bağlantı durumu takibi
✅ Basit ve kullanıcı dostu konsol arayüzü

🧩 Kullanılan Teknolojiler
Teknoloji	Açıklama
C#	Uygulama dili
.NET Framework / .NET 6	Çalışma platformu
System.Net.Sockets	Soket kütüphanesi
Visual Studio 2022	Geliştirme ortamı
Console Application	Arayüz tipi
🏗️ Proje Yapısı
📁 ChatUygulamasi
 ┣ 📂 ChatServer
 ┃ ┣ Program.cs
 ┃ ┗ ...
 ┣ 📂 ChatClient
 ┃ ┣ Program.cs
 ┃ ┗ ...
 ┣ ChatUygulamasi.sln
 ┗ README.md

🚀 Kurulum ve Çalıştırma
🔹 1. Projeyi Klonla
git clone https://github.com/Zeyneppceelik/ChatUygulamasi.git

🔹 2. Visual Studio’da Aç

ChatUygulamasi.sln dosyasına çift tıkla.

Visual Studio içinde açıldıktan sonra “Solution Explorer” üzerinden projeleri görebilirsin.

🔹 3. Multiple Startup Projects Ayarı

Solution’a sağ tıkla → Properties

Startup Project sekmesinde Multiple startup projects seçeneğini işaretle

Hem ChatServer hem ChatClient için Action = Start yap

Apply → OK butonlarına bas

Yeşil ▶️ Start tuşuna bas.
İki konsol penceresi açılacaktır:

Biri Server (Sunucu)

Diğeri Client (İstemci)

Server’ı başlattıktan sonra Client penceresinde bağlantı kurarak mesajlaşabilirsin. 💬

📡 Örnek Çalışma Ekranı
[SERVER]
Sunucu başlatıldı...
Client bağlandı: 127.0.0.1:5050
> Mesaj alındı: "Merhaba Server!"
> <img width="1111" height="494" alt="image" src="https://github.com/user-attachments/assets/e1a6801c-6e1d-4d2b-a4cc-2cf026dca7d5" />


[CLIENT]
Sunucuya bağlanıldı...
> Mesaj gönderildi: "Merhaba Server!"
> <img width="753" height="382" alt="image" src="https://github.com/user-attachments/assets/0ac5d160-7a58-47c7-80da-3b73d2132cfa" />


🧱 Öğrenilen Kavramlar

TCP/IP bağlantısı kurma

Port dinleme ve istemci kabul etme

Veri akışı (stream) üzerinden mesajlaşma

Thread kullanımı (isteğe bağlı geliştirme)

Çoklu istemci bağlantı mantığı

📎 Önemli Notlar

Aynı bilgisayarda test edeceksen IP adresini 127.0.0.1 (localhost) olarak gir.

Güvenlik duvarı port erişimini engelliyorsa, ilgili porta izin ver.

Proje hem eğitim hem ağ programlama temeli olarak kullanılabilir.

👩‍💻 Geliştirici

Zeynep Çelik
💻 GitHub: https://github.com/Zeyneppceelik

🏁 Lisans

Bu proje yalnızca eğitim amaçlıdır.
Ticari kullanım veya yeniden dağıtım için geliştiricinin izni gereklidir.
