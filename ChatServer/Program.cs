using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic; // Dictionary için gerekli

class Server
{
    // Artık her istemciyi kullanıcı adıyla birlikte saklayacağız.
    static Dictionary<TcpClient, string> clients = new Dictionary<TcpClient, string>();
    static TcpListener server;

    static void Main(string[] args)
    {
        try
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 7890);
            server.Start();
            Console.WriteLine("Sunucu başlatıldı. İstemciler bekleniyor...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                // Her istemci için ayrı bir thread oluşturuyoruz.
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }
        catch (Exception e) { Console.WriteLine("Hata: " + e.Message); }
        finally { server?.Stop(); }
    }

    static void HandleClient(TcpClient client)
    {
        string username = "[Bilinmeyen]";
        try
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int byte_count;

            // 1. Adım: Kullanıcı adını oku
            // İstemciden gelen İLK mesaj her zaman kullanıcı adı olacak.
            byte_count = stream.Read(buffer, 0, buffer.Length);
            username = Encoding.UTF8.GetString(buffer, 0, byte_count);

            // Kullanıcıyı ve adını listeye ekle
            clients.Add(client, username);
            Console.WriteLine($"{username} bağlandı!");

            // Herkese yeni kullanıcının katıldığını duyur
            BroadcastMessage($"--- {username} sohbete katıldı. ---", null);

            // 2. Adım: Sohbet mesajlarını dinle
            while ((byte_count = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, byte_count);
                Console.WriteLine($"Gelen Mesaj [{username}]: {message}");

                // Mesajı "[kullanıcıadı]: mesaj" formatında herkese gönder
                BroadcastMessage($"[{username}]: {message}", client);
            }
        }
        catch (Exception)
        {
            // Hata veya bağlantı kopması
        }
        finally
        {
            // Kullanıcıyı listeden kaldır ve bağlantıyı kapat
            clients.Remove(client);
            client.Close();
            Console.WriteLine($"{username} bağlantısı koptu.");
            // Herkese kullanıcının ayrıldığını duyur
            BroadcastMessage($"--- {username} sohbetten ayrıldı. ---", null);
        }
    }

    static void BroadcastMessage(string message, TcpClient sender)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        foreach (var clientEntry in clients)
        {
            TcpClient client = clientEntry.Key;
            // 'sender' null ise (sunucu duyurusu gibi) veya
            // mesajı gönderen değilse mesajı yolla.
            if (client != sender)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                }
                catch { /* Hata alan istemci zaten finally bloğunda temizlenecek */ }
            }
        }
    }
}