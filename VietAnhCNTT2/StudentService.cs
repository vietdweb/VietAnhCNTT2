using System;
using System.Collections.Generic;
using System.Linq;

namespace VietAnhCNTT2
{
    /// Class: StudentService - Quản lý danh sách sinh viên và các tác vụ liên quan
    /// Author: Viet Anh
    internal class StudentService
    {
        private List<Student> students;

        public StudentService()
        {
            students = new List<Student>();
        }

        /// Khởi tạo danh sách sinh viên mẫu
        public void InitializeDefaultData()
        {
            students.Add(new Student("SV001", "VietANh", null, true, "rtmx24@gmail.com", "0353536645", "CNTT2", 8.5f, true));
            students.Add(new Student("SV002", "Nguyen Viet Nam", null, false, "nam@gmail.com", "0978611889", "KT", 6.7f, true));
        }

        /// Thêm sinh viên mới
        public (bool success, string message) AddStudent(Student student)
        {
            var validation = StudentValidator.ValidateStudent(student, students, checkDuplicate: true);
            if (!validation.isValid)
                return (false, validation.message);

            students.Add(student);
            return (true, "Đã thêm sinh viên.");
        }

        /// Lấy tất cả sinh viên
        public List<Student> GetAllStudents()
        {
            return students;
        }

        /// Tìm sinh viên theo mã
        public Student FindByMaSV(string masv)
        {
            return students.FirstOrDefault(x => x.masv == masv);
        }

        /// Tìm sinh viên gần đúng theo họ tên
        public List<Student> SearchByHoTen(string hoTen)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
                return new List<Student>();

            return students.Where(x => !string.IsNullOrWhiteSpace(x.hoTen) && x.hoTen.IndexOf(hoTen, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        /// Cập nhật thông tin sinh viên
        public (bool success, string message) UpdateStudent(string masv, Student updatedData)
        {
            var student = FindByMaSV(masv);
            if (student == null)
                return (false, "Sinh viên không tồn tại.");

            // Cập nhật các trường nếu có giá trị
            if (!string.IsNullOrWhiteSpace(updatedData.hoTen))
            {
                var nameCheck = StudentValidator.ValidateHoTen(updatedData.hoTen);
                if (!nameCheck.isValid)
                    return (false, nameCheck.message);
                student.hoTen = updatedData.hoTen;
            }

            if (!string.IsNullOrWhiteSpace(updatedData.email))
            {
                var emailCheck = StudentValidator.ValidateEmail(updatedData.email);
                if (!emailCheck.isValid)
                    return (false, emailCheck.message);
                student.email = updatedData.email;
            }

            if (!string.IsNullOrWhiteSpace(updatedData.soDienThoai))
                student.soDienThoai = updatedData.soDienThoai;

            if (!string.IsNullOrWhiteSpace(updatedData.nganhHoc))
                student.nganhHoc = updatedData.nganhHoc;

            if (updatedData.dtb > 0 || updatedData.dtb == 0)
            {
                var diemCheck = StudentValidator.ValidateDiem(updatedData.dtb);
                if (!diemCheck.isValid)
                    return (false, diemCheck.message);
                student.dtb = updatedData.dtb;
            }

            if (updatedData.ngaySinh.HasValue)
            {
                var dateCheck = StudentValidator.ValidateNgaySinh(updatedData.ngaySinh);
                if (!dateCheck.isValid)
                    return (false, dateCheck.message);
                student.ngaySinh = updatedData.ngaySinh;
            }

            return (true, "Đã cập nhật sinh viên.");
        }

        /// Xóa sinh viên theo mã
        public (bool success, string message) DeleteStudent(string masv)
        {
            var student = FindByMaSV(masv);
            if (student == null)
                return (false, "Sinh viên không tồn tại.");

            students.Remove(student);
            return (true, "Đã xóa sinh viên.");
        }

        /// Sắp xếp sinh viên theo họ tên
        public List<Student> SortByHoTen()
        {
            return students.OrderBy(x => x.hoTen).ToList();
        }

        /// Sắp xếp sinh viên theo điểm trung bình (giảm dần)
        public List<Student> SortByDiem()
        {
            return students.OrderByDescending(x => x.dtb).ToList();
        }

        /// Lấy danh sách sinh viên có điểm từ 8 trở lên
        public List<Student> GetStudentsWithDiem8Plus()
        {
            return students.Where(x => x.dtb >= 8).ToList();
        }

        /// Lấy sinh viên có điểm cao nhất
        public List<Student> GetStudentsWithHighestDiem()
        {
            if (!students.Any())
                return new List<Student>();

            var maxDiem = students.Max(x => x.dtb);
            return students.Where(x => Math.Abs(x.dtb - maxDiem) < 0.0001f).ToList();
        }

        /// Tính điểm trung bình của tất cả sinh viên
        public float CalculateAverageDiem()
        {
            if (!students.Any())
                return 0;

            return students.Average(x => x.dtb);
        }

        /// Thống kê sinh viên theo ngành
        public Dictionary<string, int> StatisticsByNganh()
        {
            return students.GroupBy(x => x.nganhHoc ?? "Chưa xác định").ToDictionary(g => g.Key, g => g.Count());
        }

        /// Thống kê sinh viên theo trạng thái
        public Dictionary<string, int> StatisticsByTrangThai()
        {
            return students.GroupBy(x => x.trangThai ? "Đang học" : "Không học").ToDictionary(g => g.Key, g => g.Count());
        }

        /// Kiểm tra sinh viên có tồn tại không
        public bool StudentExists(string masv)
        {
            return FindByMaSV(masv) != null;
        }

        /// Sử dụng dữ liệu mẫu
        public int GetStudentCount()
        {
            return students.Count;
        }
    }
}
