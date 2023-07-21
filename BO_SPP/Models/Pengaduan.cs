using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BO_SPP.Common;

namespace BO_SPP.Models
{
    public class Pengaduan
    {        
        public string ID { get; set; }
        public string Jenis { get; set; }
        public string Sumber { get; set; }
        public string Nomor { get; set; }

        private string email; public string Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string handphone; public string Handphone { get { return handphone; } set { handphone = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string tempatkejadian; public string TempatKejadian { get { return tempatkejadian; } set { tempatkejadian = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string kronologi; public string Kronologi { get { return kronologi; } set { kronologi = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string penyaluranby; public string PenyaluranBy { get { return penyaluranby; } set { penyaluranby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string tindaklanjutby; public string TindakLanjutBy { get { return tindaklanjutby; } set { tindaklanjutby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string responby; public string ResponBy { get { return responby; } set { responby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string createdby; public string CreatedBy { get { return createdby; } set { createdby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string updatedby; public string UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string prosesby; public string ProsesBy { get { return prosesby; } set { prosesby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        //private string jenis_pelanggaran; public string Jenis_Pelanggaran { get { return jenis_pelanggaran; } set { jenis_pelanggaran = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string keterangan_penyaluran; public string Keterangan_Penyaluran { get { return keterangan_penyaluran; } set { keterangan_penyaluran = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string keterangan_pemeriksaan; public string Keterangan_Pemeriksaan { get { return keterangan_pemeriksaan; } set { keterangan_pemeriksaan = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string keterangan_konfirmasi; public string Keterangan_Konfirmasi { get { return keterangan_konfirmasi; } set { keterangan_konfirmasi = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string keterangan_respon; public string Keterangan_Respon { get { return keterangan_respon; } set { keterangan_respon = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }

        public string enc_Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Handphone { get { return handphone; } set { handphone = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_TempatKejadian { get { return tempatkejadian; } set { tempatkejadian = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Kronologi { get { return kronologi; } set { kronologi = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_PenyaluranBy { get { return penyaluranby; } set { penyaluranby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_TindakLanjutBy { get { return tindaklanjutby; } set { tindaklanjutby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_ResponBy { get { return responby; } set { responby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_CreatedBy { get { return createdby; } set { createdby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_ProsesBy { get { return prosesby; } set { prosesby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        //public string enc_Jenis_Pelanggaran { get { return jenis_pelanggaran; } set { jenis_pelanggaran = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Keterangan_Penyaluran { get { return keterangan_penyaluran; } set { keterangan_penyaluran = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Keterangan_Pemeriksaan { get { return keterangan_pemeriksaan; } set { keterangan_pemeriksaan = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Keterangan_Konfirmasi { get { return keterangan_konfirmasi; } set { keterangan_konfirmasi = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Keterangan_Respon { get { return keterangan_respon; } set { keterangan_respon = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }

        public string Jenis_Pelanggaran { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Pekerjaan { get; set; }
        public string FileIdentitas { get; set; }
        public string FileIdentitas_Ekstension { get; set; }
        public string NamaTerlapor { get; set; }
        public string NIP { get; set; }
        public string AlamatTerlapor { get; set; }
        public string Jabatan { get; set; }
        public string Unit_Kerja { get; set; }
        public string DelegatorID { get; set; }
        public string DelegatorName { get; set; }
        public Nullable<DateTime> WaktuKejadian { get; set; }
        public string FileEvidence { get; set; }
        public string FileEvidence_Ekstension { get; set; }
        public string Status { get; set; }
        public string TelaahBy { get; set; }
        public string TelaahByDate { get; set; }
        public Nullable<DateTime> TelaahDate { get; set; }        
        public string PenyaluranByDate { get; set; }
        public Nullable<DateTime> PenyaluranDate { get; set; }
        public string TindakLanjutByDate { get; set; }
        public Nullable<DateTime> TindakLanjutDate { get; set; }
        public string ResponByDate { get; set; }
        public Nullable<DateTime> ResponDate { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
      
        public Nullable<DateTime> ProsesDate { get; set; }
        public string Keterangan_Penyaluran_Filename { get; set; }
        public string Keterangan_Penyaluran_Ekstension { get; set; }
        public string Keterangan_Pemeriksaan_Filename { get; set; }
        public string Keterangan_Pemeriksaan_Ekstension { get; set; }
        public string Keterangan_Konfirmasi_Filename { get; set; }
        public string Keterangan_Konfirmasi_Ekstension { get; set; }
        public string Keterangan_Respon_Filename { get; set; }
        public string Keterangan_Respon_Ekstension { get; set; }
        public string Keterangan_Feedback { get; set; }
        public string Rating { get; set; }
        public string FeedbackBy { get; set; }
        public string FeedbackDate { get; set; }

        public string UnitKerja { get; set; }
        public string _CreatedOn { get; set; }
        public string _WaktuKejadian { get; set; }
        public string s_WaktuKejadian { get; set; }

        public IFormFile Upload { get; set; }
        public IFormFile UploadPemeriksaan { get; set; }
        public IFormFile UploadKonfirmasi { get; set; }
        public IFormFile UploadRespon { get; set; }
        public IFormFile UploadFileIdentitas { get; set; }
        public IFormFileCollection UploadFileEvidence { get; set; }

        public string FilepathFileIdentitas { get; set; }
        public string FilepathFileEvidence { get; set; }

        public string FilepathFilePenyaluran { get; set; }
        public string FilepathFilePemeriksaan { get; set; }
        public string FilepathFileKonfirmasi { get; set; }
        public string FilepathFileRespon { get; set; }

        public int Unread_Tanggapan_Admin_SPP { get; set; }
        public int Unread_Tanggapan_Pelapor { get; set; }
        public int Unread_Tanggapan_Internal_Admin_SPP { get; set; }
        public int Unread_Tanggapan_Internal_Delegator { get; set; }



        


    }
}
