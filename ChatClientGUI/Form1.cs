using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

// Projenizin isminin "ChatClientGUI" oldu�undan emin olun.
// Farkl�ysa, buradaki "ChatClientGUI" yaz�s�n� kendi proje ad�n�zla de�i�tirin.
namespace ChatClientGUI
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;

        // Form ilk olu�turuldu�unda �al��an metot (Constructor)
        public Form1()
        {
            // Bu sat�r, form �zerine koydu�unuz butonlar�, textbox'lar� vb. �izer.
            // Asla silinmemelidir. Bir �nceki hatan�z�n sebebi bu sat�r�n olmamas�yd�.
            InitializeComponent();
        }

        // Form y�klendi�inde ba�lang�� ayarlar�n� yapan metot
        private void Form1_Load(object sender, EventArgs e)
        {
            // Program ilk a��ld���nda "G�nder" butonu ve mesaj kutusu pasif olsun.
            btnSend.Enabled = false;
            txtMessageBox.Enabled = false;
        }

        // "Ba�lan" butonuna t�kland���nda �al��acak kod
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Kullan�c� ad� bo� mu diye kontrol et
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("L�tfen bir kullan�c� ad� girin.", "Uyar�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Metodun devam�n� �al��t�rma
            }

            try
            {
                // Sunucuya ba�lanmay� dene
                client = new TcpClient("127.0.0.1", 7890);
                stream = client.GetStream();

                // 1. Ad�m: Kullan�c� ad�n� sunucuya g�nder
                string username = txtUsername.Text;
                byte[] usernameBytes = Encoding.UTF8.GetBytes(username);
                stream.Write(usernameBytes, 0, usernameBytes.Length);

                // 2. Ad�m: Sunucudan gelen mesajlar� dinlemek i�in arkaplanda bir thread ba�lat
                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true; // Ana form kapan�nca bu thread de otomatik kapans�n
                receiveThread.Start();

                AppendTextToChatBox("Sunucuya ba�land�!");

                // Ba�lant� ba�ar�l� oldu�unda aray�z� g�ncelle
                btnConnect.Enabled = false;
                txtUsername.ReadOnly = true;
                btnSend.Enabled = true;
                txtMessageBox.Enabled = true;
                txtMessageBox.Focus(); // �mleci mesaj yazma kutusuna odakla
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sunucuya ba�lan�lamad�: " + ex.Message, "Ba�lant� Hatas�", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // "G�nder" butonuna t�kland���nda �al��acak kod
        private void btnSend_Click(object sender, EventArgs e)
        {
            // Ba�lant� var m� ve mesaj kutusu bo� de�il mi diye kontrol et
            if (client != null && client.Connected && !string.IsNullOrWhiteSpace(txtMessageBox.Text))
            {
                string message = txtMessageBox.Text;
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);

                // Kendi mesaj�m�z� da "[Siz]:" olarak ekranda g�sterelim
                AppendTextToChatBox($"[Siz]: {message}");
                txtMessageBox.Clear(); // Mesaj kutusunu temizle
            }
        }

        // Arkaplanda s�rekli �al��arak sunucudan gelen mesajlar� dinleyen metot
        private void ReceiveMessages()
        {
            byte[] buffer = new byte[4096]; // Gelen veriler i�in 4KB'l�k bir alan
            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    // E�er 0 byte okunursa, sunucu ba�lant�y� kapatm�� demektir
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    AppendTextToChatBox(message); // Gelen mesaj� ekrana yazd�r
                }
            }
            catch
            {
                // Hata olu�ursa veya ba�lant� aniden koparsa
            }
            finally
            {
                // D�ng� bitti�inde veya hata oldu�unda ba�lant�n�n koptu�unu belirt
                AppendTextToChatBox("Sunucuyla ba�lant� koptu.");
            }
        }

        // �NEML�: Aray�z elemanlar�n� (TextBox gibi) ba�ka bir thread'den g�venli bir �ekilde g�ncellemek i�in bu metot �art!
        private void AppendTextToChatBox(string text)
        {
            // E�er bu metot, aray�z�n kendi thread'i d���nda bir yerden (�rne�in ReceiveMessages'dan) �a�r�ld�ysa...
            if (InvokeRequired)
            {
                //...bu metodu aray�z thread'i �zerinden tekrar �a��r.
                this.Invoke(new Action<string>(AppendTextToChatBox), new object[] { text });
                return;
            }
            // Art�k aray�z thread'indeyiz, textbox'� g�venle g�ncelleyebiliriz.
            txtChatBox.AppendText(text + Environment.NewLine);
        }

        // Form �zerindeki 'X' butonuna bas�ld���nda, uygulama kapanmadan �nce �al���r
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Program kapan�rken ba�lant�y� da d�zg�nce kapat.
            client?.Close();
        }

        // Mesaj yazma kutusundayken Enter'a bas�ld���nda da mesaj� g�ndermek i�in
        private void txtMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Enter'a bas�nca "G�nder" butonuna t�klanm�� gibi yap
                btnSend.PerformClick();
                // Enter tu�unun "bip" sesini engelle
                e.SuppressKeyPress = true;
            }
        }
    }
}

