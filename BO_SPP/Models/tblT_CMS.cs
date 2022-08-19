using Microsoft.AspNetCore.Http;
using BO_SPP.Common;

namespace BO_SPP.Models
{
    public class tblT_CMS
    {
        public string Action                    { get; set; }
        public string Tipe                    { get; set; }

        public string ID                        { get; set; }
        public string ID_IN                     { get; set; }
        public string Lang                      { get; set; }
        public string Title                     { get; set; }
        public string SubTitle                  { get; set; }
        public string GridTitle                 { get; set; }
        public string GridContent               { get; set; }
        public string LabelTombol               { get; set; }
        public string Link                      { get; set; }
        public string Description               { get; set; }
        public string Filename                  { get; set; }
        public string Ekstension                { get; set; }
        public string Filename1                 { get; set; }
        public string Ekstension1               { get; set; }

        public string Title_ID                  { get; set; }
        public string SubTitle_ID               { get; set; }
        public string GridTitle_ID              { get; set; }
        public string GridContent_ID            { get; set; }
        public string LabelTombol_ID            { get; set; }
        public string Link_ID                   { get; set; }
        public string Description_ID            { get; set; }
        public string Filename_ID               { get; set; }
        public string Ekstension_ID             { get; set; }
        public string Filename1_ID              { get; set; }
        public string Ekstension1_ID            { get; set; }

        public string Title_EN                  { get; set; }
        public string SubTitle_EN               { get; set; }
        public string GridTitle_EN              { get; set; }
        public string GridContent_EN            { get; set; }
        public string LabelTombol_EN            { get; set; }
        public string Link_EN                   { get; set; }
        public string Description_EN            { get; set; }
        public string Filename_EN               { get; set; }
        public string Ekstension_EN             { get; set; }
        public string Filename1_EN              { get; set; }
        public string Ekstension1_EN            { get; set; }

        public string Value1                    { get; set; }
        public string Value2                    { get; set; }
        public string Value3                    { get; set; }
        public string Value4                    { get; set; }
        public string Value5                    { get; set; }
        public string Value6                    { get; set; }
        public string Value7                    { get; set; }
        public string Value8                    { get; set; }
        public string Value9                    { get; set; }

        public string Value1_ID                 { get; set; }
        public string Value2_ID                 { get; set; }
        public string Value3_ID                 { get; set; }
        public string Value4_ID                 { get; set; }
        public string Value5_ID                 { get; set; }
        public string Value6_ID                 { get; set; }
        public string Value7_ID                 { get; set; }
        public string Value8_ID                 { get; set; }
        public string Value9_ID                 { get; set; }

        public string Value1_EN                 { get; set; }
        public string Value2_EN                 { get; set; }
        public string Value3_EN                 { get; set; }
        public string Value4_EN                 { get; set; }
        public string Value5_EN                 { get; set; }
        public string Value6_EN                 { get; set; }
        public string Value7_EN                 { get; set; }
        public string Value8_EN                 { get; set; }
        public string Value9_EN                 { get; set; }

        public string Status                    { get; set; }
        private string updatedby; public string UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }

        public string s_UpdatedOn               { get; set; }
                                                
        public IFormFile Upload_ID              { get; set; }
        public IFormFile Upload_EN              { get; set; }

        public IFormFile Upload1_ID             { get; set; }
        public IFormFile Upload1_EN             { get; set; }


    }
}
