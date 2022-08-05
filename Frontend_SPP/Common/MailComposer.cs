
using System.Data;

namespace Frontend_SPP.Common
{
    public class MailComposer
    {
        static readonly string _domain_name = ConfigurationManager.AppSetting["FileConfiguration:domain_name"];
        static readonly string _domain_name_BO = ConfigurationManager.AppSetting["FileConfiguration:domain_name_BO"];

        public static string compose_mail_body_register(string Email, string ID)
        {
            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/ Ibu pemilik akun email: <strong> " + Email + "</strong></p>" +
                            "   <p> Selamat datang di aplikasi <strong> SPP PT SMI </strong></p>" +
                            "   <p> Terima kasih telah mendaftar di aplikasi SPP PT SMI <strong> Klik tombol dibawah ini </strong> untuk segera mengaktifkan akun Anda</p>" +
                            "   <p><a href='" + _domain_name + "?ID=" + ID + "' target='_blank' rel='noopener' title='Klik untuk melanjutkan verifikasi email'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Verifikasi Akun</button></a></p> " +
                            "   <p>Email verifikasi ini hanya valid (berlaku) dalam kurun waktu 24 jam kedepan</p>    <p></p>    <p><strong> Salam Sehat!</strong></p>    <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda mendaftarkan diri pada aplikasi SPP PT SMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini.Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'New User Register' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[Email]", Email);
                HTML = HTML.Replace("[ID]", ID);
            }

            return HTML;
        }
        
        public static string compose_mail_body_password_reset(string New_User_Password_Forgotten_ID)
        {
            string HTML = "   <div class='email-wrapper'>" +
                          "   <p></p>" +
                          "   <p> Anda telah melakukan permintaan untuk reset password di <strong> SPP - PT SMI </strong></p>" +
                          "   <p> <strong> Klik tombol dibawah ini </strong> untuk melakukan reset password pada akun Anda</p>" +
                          "   <p><a href='" + _domain_name + "/account/renewpassword?ID=" + New_User_Password_Forgotten_ID + "' target='_blank' rel='noopener' title='Klik untuk melanjutkan reset password'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Reset Password</button></a></p> " +
                          "   <p>Email verifikasi ini hanya valid (berlaku) dalam kurun waktu 24 jam kedepan</p>    <p></p>     <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda mendaftarkan diri pada aplikasi SPP - PT SMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini.Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                          "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Password Reset' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[New_User_Password_Forgotten_ID]", New_User_Password_Forgotten_ID);
            }

            return HTML;
        }

        public static string compose_mail_body_kirim_dumas(string ID, string Fullname, string Nomor, string EmailPelapor, string TanggalKirim)
        {
            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu: <strong> " + Fullname + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda telah menerima Pengaduan pada aplikasi <strong> SPP - PTSMI </strong> sebagai berikut:</p>" +
                            "   <p> Nomor Tiket Laporan: <strong> " + Nomor + "</strong></p>" +
                            "   <p> Tanggal : <strong> " + TanggalKirim + "</strong></p>" +
                            "   <p> Email Pelapor : <strong> " + EmailPelapor + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Dimohon untuk <strong>memberikan respon</strong> atas pengaduan tersebut.</p>" +
                            "   <p></p> " +
                            "   <p> <strong> Klik tombol dibawah ini </strong></p>" +
                            "   <p><a href='" + _domain_name_BO + "/Pengaduan/PengaduanForm?action=view&ID=" + ID + "' target='_blank' rel='noopener' title='Klik untuk memberikan respon'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Respon Pengaduan</button></a></p> " +
                            "   <p>Tombol Respon Pengaduan ini hanya valid (berlaku) sampai dengan perubahan status dari pengaduan ini.</p>    " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda terdaftar sebagai Petugas Admin SPP pada aplikasi SPP - PTSMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Pengaduan Baru Ke Admin Spp' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[Fullname]", Fullname);
                HTML = HTML.Replace("[Nomor]", Nomor);
                HTML = HTML.Replace("[EmailPelapor]", EmailPelapor);
                HTML = HTML.Replace("[TanggalKirim]", TanggalKirim);
                HTML = HTML.Replace("[ID]", ID);
            }

            return HTML;
        }

        public static string compose_mail_body_kirim_dumas_pengadu(string Fullname, string Nomor, string TanggalKirim)
        {
            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu,</p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda telah membuat Pengaduan pada aplikasi <strong> SPP - PTSMI </strong> sebagai berikut:</p>" +
                            "   <p> Nomor Tiket Aduan : <strong> " + Nomor + "</strong></p>" +
                            "   <p> Tanggal Pengaduan : <strong> " + TanggalKirim + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Pengaduan Anda akan kami proses ke tahapan berikutnya untuk pemeriksaan informasi oleh Petugas kami.</p>" +
                            "   <p> Anda akan mendapatkan notifikasi dari kami untuk proses berikutnya.</p>" +
                            "   <p></p> " +
                            "   <p> Terimakasih sudah menggunakan Aplikasi SPP - PTSMI sebagai Layanan Pengaduan</p>" +
                            "   <p><br/></p> " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda membuat pengaduan pada aplikasi SPP - PT SMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Pengaduan Baru Ke Pelapor' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[Fullname]", Fullname);
                HTML = HTML.Replace("[Nomor]", Nomor);
                HTML = HTML.Replace("[TanggalKirim]", TanggalKirim);
            }

            return HTML;
        }

        public static string compose_mail_body_tanggapan_ke_petugas(string ID, string Fullname, string Nomor)
        {

            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu: <strong> " + Fullname + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda telah menerima Respon atas Tanggapan Pengaduan pada aplikasi <strong> SPP - PT SMI </strong> sebagai berikut:</p>" +
                            "   <p> Nomor Tiket Laporan: <strong> " + Nomor + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda dapat <strong>memberikan respon</strong> atas tanggapan tersebut.</p>" +
                            "   <p></p> " +
                            "   <p> <strong> Klik tombol dibawah ini </strong></p>" +
                            "   <p><a href='" + _domain_name_BO + "/Pengaduan/PengaduanForm?action=view&ID=" + ID + "' target='_blank' rel='noopener' title='Klik untuk memberikan respon'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Respon Pengaduan</button></a></p> " +
                            "   <p>Tombol Respon Pengaduan ini hanya valid (berlaku) sampai dengan perubahan status dari pengaduan ini.</p>    " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda terdaftar sebagai Petugas Admin SPP pada aplikasi SPP - PTSMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Tanggapan Ke Admin SPP' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[Fullname]", Fullname);
                HTML = HTML.Replace("[Nomor]", Nomor);
                HTML = HTML.Replace("[ID]", ID);
            }

            return HTML;
        }
    }
}
