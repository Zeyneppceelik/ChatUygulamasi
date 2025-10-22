using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

// Projenizin isminin "ChatClientGUI" olduðundan emin olun.
// Farklýysa, buradaki "ChatClientGUI" yazýsýný kendi proje adýnýzla deðiþtirin.
namespace ChatClientGUI
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;

        // Form ilk oluþturulduðunda çalýþan metot (Constructor)
        public Form1()
        {
            // Bu satýr, form üzerine koyduðunuz butonlarý, textbox'larý vb. çizer.
            // Asla silinmemelidir. Bir önceki hatanýzýn sebebi bu satýrýn olmamasýydý.
            InitializeComponent();
        }

        // Form yüklendiðinde baþlangýç ayarlarýný yapan metot
        private void Form1_Load(object sender, EventArgs e)
        {
            // Program ilk açýldýðýnda "Gönder" butonu ve mesaj kutusu pasif olsun.
            btnSend.Enabled = false;
            txtMessageBox.Enabled = false;
        }

        // "Baðlan" butonuna týklandýðýnda çalýþacak kod
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Kullanýcý adý boþ mu diye kontrol et
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Lütfen bir kullanýcý adý girin.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Metodun devamýný çalýþtýrma
            }

            try
            {
                // Sunucuya baðlanmayý dene
                client = new TcpClient("127.0.0.1", 7890);
                stream = client.GetStream();

                // 1. Adým: Kullanýcý adýný sunucuya gönder
                string username = txtUsername.Text;
                byte[] usernameBytes = Encoding.UTF8.GetBytes(username);
                stream.Write(usernameBytes, 0, usernameBytes.Length);

                // 2. Adým: Sunucudan gelen mesajlarý dinlemek için arkaplanda bir thread baþlat
                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true; // Ana form kapanýnca bu thread de otomatik kapansýn
                receiveThread.Start();

                AppendTextToChatBox("Sunucuya baðlandý!");

                // Baðlantý baþarýlý olduðunda arayüzü güncelle
                btnConnect.Enabled = false;
                txtUsername.ReadOnly = true;
                btnSend.Enabled = true;
                txtMessageBox.Enabled = true;
                txtMessageBox.Focus(); // Ýmleci mesaj yazma kutusuna odakla
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sunucuya baðlanýlamadý: " + ex.Message, "Baðlantý Hatasý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // "Gönder" butonuna týklandýðýnda çalýþacak kod
        private void btnSend_Click(object sender, EventArgs e)
        {
            // Baðlantý var mý ve mesaj kutusu boþ deðil mi diye kontrol et
            if (client != null && client.Connected && !string.IsNullOrWhiteSpace(txtMessageBox.Text))
            {
                string message = txtMessageBox.Text;
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);

                // Kendi mesajýmýzý da "[Siz]:" olarak ekranda gösterelim
                AppendTextToChatBox($"[Siz]: {message}");
                txtMessageBox.Clear(); // Mesaj kutusunu temizle
            }
        }

        // Arkaplanda sürekli çalýþarak sunucudan gelen mesajlarý dinleyen metot
        private void ReceiveMessages()
        {
            byte[] buffer = new byte[4096]; // Gelen veriler için 4KB'lýk bir alan
            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    // Eðer 0 byte okunursa, sunucu baðlantýyý kapatmýþ demektir
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    AppendTextToChatBox(message); // Gelen mesajý ekrana yazdýr
                }
            }
            catch
            {
                // Hata oluþursa veya baðlantý aniden koparsa
            }
            finally
            {
                // Döngü bittiðinde veya hata olduðunda baðlantýnýn koptuðunu belirt
                AppendTextToChatBox("Sunucuyla baðlantý koptu.");
            }
        }

        // ÖNEMLÝ: Arayüz elemanlarýný (TextBox gibi) baþka bir thread'den güvenli bir þekilde güncellemek için bu metot þart!
        private void AppendTextToChatBox(string text)
        {
            // Eðer bu metot, arayüzün kendi thread'i dýþýnda bir yerden (örneðin ReceiveMessages'dan) çaðrýldýysa...
            if (InvokeRequired)
            {
                //...bu metodu arayüz thread'i üzerinden tekrar çaðýr.
                this.Invoke(new Action<string>(AppendTextToChatBox), new object[] { text });
                return;
            }
            // Artýk arayüz thread'indeyiz, textbox'ý güvenle güncelleyebiliriz.
            txtChatBox.AppendText(text + Environment.NewLine);
        }

        // Form üzerindeki 'X' butonuna basýldýðýnda, uygulama kapanmadan önce çalýþýr
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Program kapanýrken baðlantýyý da düzgünce kapat.
            client?.Close();
        }

        // Mesaj yazma kutusundayken Enter'a basýldýðýnda da mesajý göndermek için
        private void txtMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Enter'a basýnca "Gönder" butonuna týklanmýþ gibi yap
                btnSend.PerformClick();
                // Enter tuþunun "bip" sesini engelle
                e.SuppressKeyPress = true;
            }
        }
    }
}

