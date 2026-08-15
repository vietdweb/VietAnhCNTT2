using System;
using System.Text.RegularExpressions;

namespace VietAnhCNTT2
{
    /// <summary>
    /// Class: StudentValidator - Kiểm tra tính hợp lệ của dữ liệu sinh viên
    /// Author: Viet Anh
    /// </summary>
    internal static class StudentValidator
    {
        /// <summary>
        /// Kiểm tra xem mã sinh viên có hợp lệ không
        /// </summary>
        public static (bool isValid, string message) ValidateMaSV(string masv)
        {
            if (string.IsNullOrWhiteSpace(masv))
                return (false, "Mã sinh viên không được để trống.");
            return (true, "");
        }

        /// <summary>
        /// Kiểm tra xem mã sinh viên có trùng trong danh sách không
        /// </summary>
        public static (bool isDuplicate, string message) CheckDuplicateMaSV(string masv, List<Student> students)
        {
            if (students.Any(x => x.masv == masv))
                return (true, "Mã sinh viên đã tồn tại.");
            return (false, "");
        }

        /// <summary>
        /// Kiểm tra họ tên có hợp lệ không
        /// </summary>
        public static (bool isValid, string message) ValidateHoTen(string hoTen)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
                return (false, "Họ tên không được để trống.");
            return (true, "");
        }

        /// <summary>
        /// Kiểm tra email có hợp lệ không
        /// </summary>
        public static (bool isValid, string message) ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (true, ""); // Email có thể để trống

            try
            {
                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                    return (false, "Email không hợp lệ.");
                return (true, "");
            }
            catch
            {
                return (false, "Email không hợp lệ.");
            }
        }

        /// <summary>
        /// Kiểm tra điểm trung bình có hợp lệ không (0-10)
        /// </summary>
        public static (bool isValid, string message) ValidateDiem(float dtb)
        {
            if (dtb < 0 || dtb > 10)
                return (false, "Điểm trung bình phải nằm trong khoảng 0-10.");
            return (true, "");
        }

        /// <summary>
        /// Kiểm tra ngày sinh có hợp lệ không
        /// </summary>
        public static (bool isValid, string message) ValidateNgaySinh(DateTime? ngaySinh)
        {
            if (ngaySinh.HasValue && ngaySinh.Value > DateTime.Now)
                return (false, "Ngày sinh không được trong tương lai.");
            return (true, "");
        }

        /// <summary>
        /// Kiểm tra toàn bộ dữ liệu sinh viên
        /// </summary>
        public static (bool isValid, string message) ValidateStudent(Student student, List<Student> students, bool checkDuplicate = true)
        {
            // Kiểm tra mã
            var maCheck = ValidateMaSV(student.masv);
            if (!maCheck.isValid)
                return (false, maCheck.message);

            // Kiểm tra trùng mã (nếu cần)
            if (checkDuplicate)
            {
                var dupCheck = CheckDuplicateMaSV(student.masv, students);
                if (dupCheck.isDuplicate)
                    return (false, dupCheck.message);
            }

            // Kiểm tra họ tên
            var nameCheck = ValidateHoTen(student.hoTen);
            if (!nameCheck.isValid)
                return (false, nameCheck.message);

            // Kiểm tra email
            var emailCheck = ValidateEmail(student.email);
            if (!emailCheck.isValid)
                return (false, emailCheck.message);

            // Kiểm tra điểm
            var diemCheck = ValidateDiem(student.dtb);
            if (!diemCheck.isValid)
                return (false, diemCheck.message);

            // Kiểm tra ngày sinh
            var dateCheck = ValidateNgaySinh(student.ngaySinh);
            if (!dateCheck.isValid)
                return (false, dateCheck.message);

            return (true, "");
        }
    }
}
