using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietAnhCNTT2
{
    /// Class: Student - Lớp đại diện cho một sinh viên
    /// Author: Viet Anh
    internal class Student
    {
        // Constructor không tham số
        public Student() { }

        // Constructor đầy đủ
        public Student(string masv, string hoTen, DateTime? ngaySinh = null, bool gioiTinh = false, string email = "", string soDienThoai = "", string nganhHoc = "", float dtb = 0, bool trangThai = false)
        {
            this.masv = masv;
            this.hoTen = hoTen;
            this.ngaySinh = ngaySinh;
            this.gioiTinh = gioiTinh;
            this.email = email;
            this.soDienThoai = soDienThoai;
            this.nganhHoc = nganhHoc;
            this.dtb = dtb;
            this.trangThai = trangThai;
        }

        // Properties
        public string masv { get; set; }
        public string hoTen { get; set; }
        public DateTime? ngaySinh { get; set; }
        public bool gioiTinh { get; set; }
        public string email { get; set; }
        public string soDienThoai { get; set; }
        public string nganhHoc { get; set; }
        public float dtb { get; set; }
        public bool trangThai { get; set; }
    }
}
