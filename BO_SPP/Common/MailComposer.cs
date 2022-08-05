
using System.Data;

namespace BO_SPP.Common
{
    public class MailComposer
    {
        static readonly string _domain_name = ConfigurationManager.AppSetting["FileConfiguration:domain_name"];
        static readonly string _domain_name_BO = ConfigurationManager.AppSetting["FileConfiguration:domain_name_BO"];

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

        public static string compose_mail_body_kirim_dumas_pengadu(string Email, string Nomor, string TanggalKirim, int isRegistered, string NewRandomPassword, string New_User_Verification_ID)
        {
            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu,</p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda telah membuat Pengaduan pada aplikasi <strong> SPP - PTSMI </strong> sebagai berikut:</p>" +
                            "   <p> Email : <strong> " + Email + "</strong></p>" +
                            "   <p> Password : <strong> " + NewRandomPassword + "</strong></p>" +
                            "   <p> Nomor Tiket Aduan : <strong> " + Nomor + "</strong></p>" +
                            "   <p> Tanggal Pengaduan : <strong> " + TanggalKirim + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Pengaduan Anda akan kami proses ke tahapan berikutnya untuk pemeriksaan informasi oleh Petugas kami.</p>" +
                            "   <p></p>" +
                            "   <p></p> " +
                            "   <p> Anda perlu melakukan aktivasi akun pada aplikasi SPP PT SMI untuk mendapatkan informasi seputar penanganan pengaduan Anda.</p>" +
                            "   <p> <strong> Klik tombol dibawah ini </strong> untuk segera mengaktifkan akun Anda</p>" +
                            "   <p> <a href='" + _domain_name + "?ID=" + New_User_Verification_ID + "' target='_blank' rel='noopener' title='Klik untuk melanjutkan verifikasi email'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Verifikasi Akun</button></a></p> " +
                            "   <p> Email verifikasi ini hanya valid (berlaku) dalam kurun waktu 24 jam kedepan</p>" +
                            "   <p> Terimakasih sudah menggunakan Aplikasi SPP - PT SMI sebagai Layanan Pengaduan</p>" +
                            "   <p><br/></p> " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda membuat pengaduan pada aplikasi SPP - PT SMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";



            if (isRegistered == 1)
            {
                HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu,</p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda telah membuat Pengaduan pada aplikasi <strong> SPP - PTSMI </strong> sebagai berikut:</p>" +
                            "   <p> Email : <strong> " + Email + "</strong></p>" +
                            "   <p> Nomor Tiket Aduan : <strong> " + Nomor + "</strong></p>" +
                            "   <p> Tanggal Pengaduan : <strong> " + TanggalKirim + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Pengaduan Anda akan kami proses ke tahapan berikutnya untuk pemeriksaan informasi oleh Petugas kami.</p>" +
                            "   <p></p>" +
                            "   <p></p> " +
                            "   <p> Terimakasih sudah menggunakan Aplikasi SPP - PTSMI sebagai Layanan Pengaduan</p>" +
                            "   <p><br/></p> " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda membuat pengaduan pada aplikasi SPP - PT SMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";
            }

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Pengaduan Baru Ke Pelapor via Admin SPP' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[Email]", Email);
                HTML = HTML.Replace("[Nomor]", Nomor);
                HTML = HTML.Replace("[TanggalKirim]", TanggalKirim);
                HTML = HTML.Replace("[NewRandomPassword]", NewRandomPassword);
                HTML = HTML.Replace("[New_User_Verification_ID]", New_User_Verification_ID);
            }

            return HTML;
        }

        public static string compose_mail_body_tanggapan_ke_Admin_SPP(string ID, string Fullname, string Nomor)
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

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Tanggapan Delegator Ke Admin SPP' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[ID]", ID);
                HTML = HTML.Replace("[Fullname]", Fullname);
                HTML = HTML.Replace("[Nomor]", Nomor);
            }

            return HTML;
        }

        public static string compose_mail_body_tanggapan_ke_Delegator(string ID, string Fullname, string Nomor)
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
                            "   <p><a href='" + _domain_name_BO + "/Pengaduan/PengaduanForm?action=view&ID=" + ID + "' target='_blank' rel='noopener' title='Klik untuk memberikan respon'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Respon Tanggapan</button></a></p> " +
                            "   <p>Tombol Respon Pengaduan ini hanya valid (berlaku) sampai dengan perubahan status dari pengaduan ini.</p>    " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda terdaftar sebagai Petugas Delegator pada aplikasi SPP - PTSMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Tanggapan Ke Delegator' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[ID]", ID);
                HTML = HTML.Replace("[Fullname]", Fullname);
                HTML = HTML.Replace("[Nomor]", Nomor);
            }

            return HTML;
        }

        public static string compose_mail_body_member_delegator_baru(string Email, string Fullname, string DelegatorName)
        {
            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu: <strong> " + Fullname + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda telah diundang untuk bergabung ke dalam Grup " + DelegatorName + " sebagai Anggota Delegator pada <strong>Aplikasi SPP - PTSMI </strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Silahkan login ke Aplikasi Back Office SPP PT SMI dengan menggunakan email <strong>" + Email + "</strong></p>" +
                            "   <p></p> " +
                            "   <p> <strong> Klik tombol dibawah ini </strong></p>" +
                            "   <p><a href='" + _domain_name_BO + "' target='_blank' rel='noopener' title='Klik untuk login ke Aplikasi Back Office SPP PT SMI'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Login Back office SPP</button></a></p> " +
                            "   <p></p>    " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda terdaftar sebagai Anggota Delegator pada aplikasi SPP - PTSMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Member Delegator Baru' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[Email]", Email);
                HTML = HTML.Replace("[Fullname]", Fullname);
                HTML = HTML.Replace("[DelegatorName]", DelegatorName);
            }

            return HTML;
        }

        public static string compose_mail_body_tanggapan_ke_pelapor(string Nomor)
        {
            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu,</p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda telah menerima tanggapan dari petugas kami atas pengaduan pada aplikasi <strong> SPP - PT SMI </strong> sebagai berikut:</p>" +
                            "   <p> Nomor Tiket Laporan: <strong> " + Nomor + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda dapat <strong>memberikan respon</strong> atas tanggapan tersebut.</p>" +
                            "   <p></p> " +
                            "   <p> <strong> Klik tombol dibawah ini untuk masuk ke Aplikasi SPP PT SMI </strong></p>" +
                            "   <p><a href='" + _domain_name + "' target='_blank' rel='noopener' title='Klik untuk login'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Login</button></a></p> " +
                            "   <p>Tombol Respon Pengaduan ini hanya valid (berlaku) sampai dengan perubahan status dari pengaduan ini.</p>    " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda telah mengirimkan pengaduan pada aplikasi SPP - PTSMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Tanggapan Ke Pelapor' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[Nomor]", Nomor);
            }

            return HTML;
        }

        public static string compose_mail_body_submit_delegasi(string ID, string Fullname, string Nomor, string EmailPelapor, string DelegatorName, string TanggalKirim)
        {
            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu: <strong> " + Fullname + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda telah menerima Delegasi Pengaduan pada aplikasi <strong> SPP - PTSMI </strong> sebagai berikut:</p>" +
                            "   <p> Nomor Tiket Laporan: <strong> " + Nomor + "</strong></p>" +
                            "   <p> Tanggal Pengaduan : <strong> " + TanggalKirim + "</strong></p>" +
                            "   <p> Email Pelapor : <strong> " + EmailPelapor + "</strong></p>" +
                            "   <p> Grup Delegator : <strong> " + DelegatorName + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Dimohon untuk <strong>memberikan respon</strong> atas pengaduan tersebut.</p>" +
                            "   <p></p> " +
                            "   <p> <strong> Klik tombol dibawah ini </strong></p>" +
                            "   <p><a href='" + _domain_name_BO + "/Pengaduan/PengaduanForm?ID=" + ID + "' target='_blank' rel='noopener' title='Klik untuk memberikan respon'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Respon Pengaduan</button></a></p> " +
                            "   <p>Tombol Respon Pengaduan ini hanya valid (berlaku) sampai dengan perubahan status dari pengaduan ini.</p>    " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda terdaftar sebagai anggota Grup Delegator " + DelegatorName + " pada aplikasi SPP - PTSMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Submit Delegasi' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[ID]", ID);
                HTML = HTML.Replace("[Fullname]", Fullname);
                HTML = HTML.Replace("[Nomor]", Nomor);
                HTML = HTML.Replace("[EmailPelapor]", EmailPelapor);
                HTML = HTML.Replace("[DelegatorName]", DelegatorName);
                HTML = HTML.Replace("[TanggalKirim]", TanggalKirim);
            }

            return HTML;
        }

        public static string compose_mail_body_submit_dari_delegator_ke_admin_spp(string ID, string Fullname, string Nomor, string EmailPelapor, string DelegatorName, string TanggalKirim, string Status)
        {
            if (Status == "Ditindak lanjut") Status = "Selesai di tindak lanjuti";
            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu: <strong> " + Fullname + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Grup Delegator " + DelegatorName + " telah melaporkan hasil tindak lanjut dari pengaduan pada aplikasi <strong> SPP - PTSMI </strong> sebagai berikut:</p>" +
                            "   <p> Nomor Tiket Pengaduan: <strong> " + Nomor + "</strong></p>" +
                            "   <p> Tanggal Pengaduan : <strong> " + TanggalKirim + "</strong></p>" +
                            "   <p> Email Pelapor : <strong> " + EmailPelapor + "</strong></p>" +
                            "   <p> Hasil Tindak Lanjut : <strong> " + Status + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Dimohon untuk <strong>memberikan respon</strong> atas pengaduan tersebut.</p>" +
                            "   <p></p> " +
                            "   <p> <strong> Klik tombol dibawah ini </strong></p>" +
                            "   <p><a href='" + _domain_name_BO + "/Pengaduan/PengaduanForm?ID=" + ID + "' target='_blank' rel='noopener' title='Klik untuk memberikan respon'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Respon Pengaduan</button></a></p> " +
                            "   <p>Tombol Respon Pengaduan ini hanya valid (berlaku) sampai dengan perubahan status dari pengaduan ini.</p>    " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda terdaftar sebagai Admin SPP pada aplikasi SPP - PTSMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Respon Dari Delegator Ke Admin Spp' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[ID]", ID);
                HTML = HTML.Replace("[Fullname]", Fullname);
                HTML = HTML.Replace("[Nomor]", Nomor);
                HTML = HTML.Replace("[EmailPelapor]", EmailPelapor);
                HTML = HTML.Replace("[DelegatorName]", DelegatorName);
                HTML = HTML.Replace("[TanggalKirim]", TanggalKirim);
                HTML = HTML.Replace("[Status]", Status);
            }

            return HTML;
        }

        public static string compose_mail_body_kirim_respon_ke_pelapor(string ID, string Nomor, string TanggalKirim, string Status)
        {
            if (Status == "Ditolak Admin SPP") Status = "Pengaduan tidak dapat diproses";
            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu,</p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda telah menerima respon dari Pengaduan pada aplikasi <strong> SPP - PTSMI </strong> sebagai berikut:</p>" +
                            "   <p> Nomor Tiket Laporan: <strong> " + Nomor + "</strong></p>" +
                            "   <p> Tanggal Laporan : <strong> " + TanggalKirim + "</strong></p>" +
                            "   <p> Respon/Hasil : <strong> " + Status + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p></p> " +
                            "   <p> <strong> Klik tombol dibawah ini </strong> untuk mengakses halaman pengaduan Anda.</p>" +
                            "   <p><a href='" + _domain_name + "/Pengaduan/PengaduanForm?ID=" + ID + "' target='_blank' rel='noopener' title='Klik untuk mengakses halaman pengaduan ini'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Akses Pengaduan</button></a></p> " +
                            "   <p>Silahkan menghubungi layanan SPP PT SMI jika Anda ingin menanyakan lebih lanjut tentang pengaduan ini.</p>    " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda membuat pengaduan pada aplikasi SPP - PTSMI. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'Respon Akhir Ke Pelapor' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[ID]", ID);
                HTML = HTML.Replace("[Nomor]", Nomor);
                HTML = HTML.Replace("[TanggalKirim]", TanggalKirim);
                HTML = HTML.Replace("[Status]", Status);
            }


            return HTML;
        }

        public static string compose_mail_body_new_user_admin(string Fullname, string Email, string Roles)
        {
            string HTML = "   <div class='email-wrapper'>" +
                            "   <p> Yang terhormat Bapak/Ibu: <strong> " + Fullname + "</strong></p>" +
                            "   <p><br/></p>" +
                            "   <p> Anda telah didaftarkan pada aplikasi <strong> Back Office SPP PT.SMI </strong> sebagai berikut:</p>" +
                            "   <p> Email: <strong> " + Email + "</strong></p>" +
                            "   <p> Role(s) : <strong> " + Roles + "</strong></p>" +
                            "   <p></p>" +
                            "   <p></p> " +
                            "   <p> <strong> Klik tombol dibawah ini untuk login ke Aplikasi Back Office SPP PT.SMI </strong></p>" +
                            "   <p><a href='" + _domain_name_BO + "' target='_blank' rel='noopener' title='Klik untuk login'><button style='padding: 10px; background-color: aquamarine; font-size: 14px; font-weight: bold; cursor: pointer'>Login</button></a></p> " +
                            "   <p>Silahkan gunakan email dan kata kunci yang Anda miliki untuk login ke Aplikasi Back Office SPP PT.SMI</p>    " +
                            "   <p><br/></p> " +
                            "   <p></p>    <p></p>  <p><strong> PT Sarana Multi Infrastruktur (Persero). </strong></p>    <p></p>    <p><sub><em> *) Anda menerima email ini karena Anda didaftarkan sebagai " + Roles + " pada aplikasi SPP PT SMI oleh System Administrator. Bila Anda merasa ini adalah kesalahan, harap abaikan email ini. <br> Email ini dikirim secara otomatis, jangan membalas email ini</em></sub></p>" +
                            "   </div>";

            DataTable dt = mssql.GetDataTable("SELECT Konten FROM tblT_EmailSetting WHERE Tipe = 'User Baru Internal' AND Status = 'Aktif'");
            if (dt.Rows.Count > 0)
            {
                HTML = dt.Rows[0]["Konten"].ToString();
                HTML = HTML.Replace("[Fullname]", Fullname);
                HTML = HTML.Replace("[Email]", Email);
                HTML = HTML.Replace("[Roles]", Roles);
            }

            return HTML;
        }
    }
}
