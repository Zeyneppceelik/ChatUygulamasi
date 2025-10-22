using System.Net.Sockets;
using System.Text;

class Client
{
    private static TcpClient client;
    private static NetworkStream stream;

    static void Main(string[] args)
    {
        try
        {
            client = new TcpClient("127.0.0.1", 7890);
            stream = client.GetStream();
            Console.WriteLine("Sunucuya bağlandı!");

            // 1. Adım: Kullanıcı adını al ve sunucuya gönder
            Console.Write("Lütfen kullanıcı adınızı girin: ");
            string username = Console.ReadLine();
            byte[] usernameBytes = Encoding.UTF8.GetBytes(username);
            stream.Write(usernameBytes, 0, usernameBytes.Length);

            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            Console.WriteLine("Mesajınızı yazıp Enter'a basın (çıkış için 'exit' yazın):");

            // 2. Adım: Sohbet mesajlarını gönder
            while (true)
            {
                string messageToSend = Console.ReadLine();
                if (messageToSend.ToLower() == "exit") break;

                byte[] buffer = Encoding.UTF8.GetBytes(messageToSend);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
        catch (Exception e) { Console.WriteLine("Bir hata oluştu: " + e.Message); }
        finally
        {
            if (client != null) client.Close();
        }
    }

    // ReceiveMessages metodu aynı kalabilir.
    private static void ReceiveMessages()
    {
        byte[] buffer = new byte[4096];
        int bytesRead;
        try
        {
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.WriteLine(receivedMessage); // Gelen mesajda zaten kullanıcı adı var
                Console.Write("Mesajınızı yazın: ");
            }
        }
        catch { Console.WriteLine("\nSunucuyla bağlantı koptu."); }
    }
}