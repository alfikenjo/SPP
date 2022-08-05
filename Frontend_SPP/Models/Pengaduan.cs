using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend_SPP.Models
{
    public class Pengaduan
    {        
        public string ID { get; set; }
        public string Sumber { get; set; }
        public string Jenis { get; set; }
        public string Nomor { get; set; }
        public string Kadar { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }
        public string Alamat { get; set; }
        public string Handphone { get; set; }
        public string Pekerjaan { get; set; }
        public string FileIdentitas { get; set; }
        public string FileIdentitas_Ekstension { get; set; }
        public string NamaTerlapor { get; set; }
        public string NIP { get; set; }
        public string AlamatTerlapor { get; set; }
        public string Jabatan { get; set; }
        public string Unit_Kerja { get; set; }
        public Guid? ID_Organisasi { get; set; }
        public string TempatKejadian { get; set; }
        public Nullable<DateTime> WaktuKejadian { get; set; }
        public string Kronologi { get; set; }
        public string FileEvidence { get; set; }
        public string FileEvidence_Ekstension { get; set; }
        public string Status { get; set; }

        public string Jenis_Pelanggaran { get; set; }
        public string TelaahBy { get; set; }
        public Nullable<DateTime> TelaahDate { get; set; }        
        public string PenyaluranBy { get; set; }
        public string PenyaluranByDate { get; set; }
        public Nullable<DateTime> PenyaluranDate { get; set; }
        public string TindakLanjutBy { get; set; }
        public string TindakLanjutByDate { get; set; }
        public Nullable<DateTime> TindakLanjutDate { get; set; }
        public string ResponBy { get; set; }
        public string ResponByDate { get; set; }
        public Nullable<DateTime> ResponDate { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string ProsesBy { get; set; }
        public Nullable<DateTime> ProsesDate { get; set; }
        public string Keterangan_Penyaluran { get; set; }
        public string Keterangan_Penyaluran_Filename { get; set; }
        public string Keterangan_Penyaluran_Ekstension { get; set; }
        public string Keterangan_Pemeriksaan { get; set; }
        public string Keterangan_Pemeriksaan_Filename { get; set; }
        public string Keterangan_Pemeriksaan_Ekstension { get; set; }
        public string Keterangan_Konfirmasi { get; set; }
        public string Keterangan_Konfirmasi_Filename { get; set; }
        public string Keterangan_Konfirmasi_Ekstension { get; set; }
        public string Keterangan_Respon { get; set; }
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
