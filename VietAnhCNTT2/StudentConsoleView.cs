using System;
using System.Collections.Generic;

namespace VietAnhCNTT2
{
    /// <summary>
    /// Class: StudentConsoleView - Xử lý tất cả UI/UX tương tác với người dùng qua console
    /// Author: Viet Anh
    /// </summary>
    internal class StudentConsoleView
    {
        private StudentService studentService;

        public StudentConsoleView(StudentService service)
        {
            studentService = service;
        }

        /// <summary>
        /// Hiển thị danh sách sinh viên
        /// </summary>
        public void DisplayStudentList(List<Student> list)
        {
            if (!list.Any())
            {
                Console.WriteLine("Danh sách trống.");
                return;
            }

            Console.WriteLine("\n========== DANH SÁCH SINH VIÊN ==========");
            foreach (var sv in list)
            {
                PrintStudent(sv);
            }
        }

        /// <summary>
        /// In thông tin chi tiết một sinh viên
        /// </summary>
        public void PrintStudent(Student s)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Mã: {s.masv}");
            Console.WriteLine($"Họ tên: {s.hoTen}");
            Console.WriteLine($"Ngày sinh: {(s.ngaySinh.HasValue ? s.ngaySinh.Value.ToString("yyyy-MM-dd") : "")}");
            Console.WriteLine($"Giới tính: {(s.gioiTinh ? "Nam" : "Nữ")}");
            Console.WriteLine($"Email: {s.email}");
            Console.WriteLine($"Điện thoại: {s.soDienThoai}");
            Console.WriteLine($"Ngành: {s.nganhHoc}");
            Console.WriteLine($"Điểm TB: {s.dtb}");
            Console.WriteLine($"Trạng thái: {(s.trangThai ? "Đang học" : "Không học")}");
            Console.WriteLine("-----------------------------");
        }

        /// <summary>
        /// Nhập thông tin sinh viên mới
        /// </summary>
        public void InputNewStudent()
        {
            Student sv = new Student();

            // Mã sinh viên
            Console.Write("Nhập mã sinh viên: ");
            sv.masv = Console.ReadLine();
            var maCheck = StudentValidator.ValidateMaSV(sv.masv);
            if (!maCheck.isValid)
            {
                Console.WriteLine(maCheck.message);
                return;
            }

            if (studentService.StudentExists(sv.masv))
            {
                Console.WriteLine("Mã sinh viên đã tồn tại.");
                return;
            }

            // Họ tên
            Console.Write("Nhập họ tên: ");
            sv.hoTen = Console.ReadLine();
            var nameCheck = StudentValidator.ValidateHoTen(sv.hoTen);
            if (!nameCheck.isValid)
            {
                Console.WriteLine(nameCheck.message);
                return;
            }

            // Ngày sinh (tùy chọn)
            Console.Write("Nhập ngày sinh (yyyy-MM-dd): ");
            var ns = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ns) && DateTime.TryParse(ns, out var dt))
            {
                sv.ngaySinh = dt;
            }

            // Giới tính (tùy chọn)
            Console.Write("Giới tính (Nam/Nữ): ");
            var gt = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(gt))
            {
                sv.gioiTinh = gt.Trim().ToLower() == "nam";
            }

            // Email (tùy chọn)
            Console.Write("Nhập email: ");
            sv.email = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(sv.email))
            {
                var emailCheck = StudentValidator.ValidateEmail(sv.email);
                if (!emailCheck.isValid)
                {
                    Console.WriteLine(emailCheck.message);
                    return;
                }
            }

            // Số điện thoại
            Console.Write("Nhập số điện thoại: ");
            sv.soDienThoai = Console.ReadLine() ?? "";

            // Ngành học
            Console.Write("Nhập ngành học: ");
            sv.nganhHoc = Console.ReadLine() ?? "";

            // Điểm trung bình
            Console.Write("Nhập điểm trung bình (0-10): ");
            var dtbStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dtbStr))
            {
                if (float.TryParse(dtbStr, out var f))
                {
                    var diemCheck = StudentValidator.ValidateDiem(f);
                    if (!diemCheck.isValid)
                    {
                        Console.WriteLine(diemCheck.message);
                        return;
                    }
                    sv.dtb = f;
                }
                else
                {
                    Console.WriteLine("Điểm không hợp lệ.");
                    return;
                }
            }

            // Trạng thái (tùy chọn)
            Console.Write("Trạng thái (true=đang học / false=không học):");
            var tt = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(tt) && bool.TryParse(tt, out var ttb))
            {
                sv.trangThai = ttb;
            }

            var result = studentService.AddStudent(sv);
            Console.WriteLine(result.message);
        }

        /// <summary>
        /// Tìm sinh viên theo mã
        /// </summary>
        public void SearchByMaSV()
        {
            Console.Write("Nhập mã sinh viên cần tìm: ");
            var masv = Console.ReadLine();

            var sv = studentService.FindByMaSV(masv);
            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            PrintStudent(sv);
        }

        /// <summary>
        /// Tìm sinh viên theo họ tên (gần đúng)
        /// </summary>
        public void SearchByHoTen()
        {
            Console.Write("Nhập họ tên hoặc một phần của họ tên: ");
            var hoTen = Console.ReadLine();

            var list = studentService.SearchByHoTen(hoTen);
            DisplayStudentList(list);
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên
        /// </summary>
        public void UpdateStudent()
        {
            Console.Write("Nhập mã sinh viên cần cập nhật: ");
            var masv = Console.ReadLine();

            if (!studentService.StudentExists(masv))
            {
                Console.WriteLine("Sinh viên không tồn tại.");
                return;
            }

            var sv = new Student();
            Console.WriteLine("Để trống để giữ nguyên trường thông tin\n");

            Console.Write("Họ tên: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) sv.hoTen = input;

            Console.Write("Email: ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) sv.email = input;

            Console.Write("Số điện thoại: ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) sv.soDienThoai = input;

            Console.Write("Ngành học: ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) sv.nganhHoc = input;

            Console.Write("Điểm trung bình: ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && float.TryParse(input, out var f))
            {
                sv.dtb = f;
            }

            var result = studentService.UpdateStudent(masv, sv);
            Console.WriteLine(result.message);
        }

        /// <summary>
        /// Xóa sinh viên
        /// </summary>
        public void DeleteStudent()
        {
            Console.Write("Nhập mã sinh viên cần xóa: ");
            var masv = Console.ReadLine();

            var result = studentService.DeleteStudent(masv);
            Console.WriteLine(result.message);
        }

        /// <summary>
        /// Hiển thị danh sách sinh viên sắp xếp theo họ tên
        /// </summary>
        public void DisplaySortByHoTen()
        {
            var sorted = studentService.SortByHoTen();
            Console.WriteLine("Sắp xếp theo họ tên:");
            DisplayStudentList(sorted);
        }

        /// <summary>
        /// Hiển thị danh sách sinh viên sắp xếp theo điểm (giảm dần)
        /// </summary>
        public void DisplaySortByDiem()
        {
            var sorted = studentService.SortByDiem();
            Console.WriteLine("Sắp xếp theo điểm (giảm dần):");
            DisplayStudentList(sorted);
        }

        /// <summary>
        /// Hiển thị sinh viên có điểm từ 8 trở lên
        /// </summary>
        public void DisplayDiem8Plus()
        {
            var list = studentService.GetStudentsWithDiem8Plus();
            Console.WriteLine("Danh sách sinh viên có điểm từ 8 trở lên:");
            DisplayStudentList(list);
        }

        /// <summary>
        /// Hiển thị sinh viên có điểm cao nhất
        /// </summary>
        public void DisplayHighestDiem()
        {
            var list = studentService.GetStudentsWithHighestDiem();
            Console.WriteLine("Sinh viên có điểm cao nhất:");
            DisplayStudentList(list);
        }

        /// <summary>
        /// Hiển thị điểm trung bình tổng
        /// </summary>
        public void DisplayAverageDiem()
        {
            var avg = studentService.CalculateAverageDiem();
            Console.WriteLine($"Điểm trung bình của tất cả sinh viên: {avg:F2}");
        }

        /// <summary>
        /// Hiển thị thống kê theo ngành
        /// </summary>
        public void DisplayStatisticsByNganh()
        {
            var stats = studentService.StatisticsByNganh();
            Console.WriteLine("\n========== THỐNG KÊ THEO NGÀNH ==========");
            if (!stats.Any())
            {
                Console.WriteLine("Không có dữ liệu.");
                return;
            }
            foreach (var item in stats)
            {
                Console.WriteLine($"{item.Key}: {item.Value} sinh viên");
            }
        }

        /// <summary>
        /// Hiển thị thống kê theo trạng thái
        /// </summary>
        public void DisplayStatisticsByTrangThai()
        {
            var stats = studentService.StatisticsByTrangThai();
            Console.WriteLine("\n========== THỐNG KÊ THEO TRẠNG THÁI ==========");
            if (!stats.Any())
            {
                Console.WriteLine("Không có dữ liệu.");
                return;
            }
            foreach (var item in stats)
            {
                Console.WriteLine($"{item.Key}: {item.Value} sinh viên");
            }
        }

        /// <summary>
        /// Hiển thị danh sách tất cả sinh viên
        /// </summary>
        public void DisplayAllStudents()
        {
            var list = studentService.GetAllStudents();
            Console.WriteLine("\n========== DANH SÁCH TẤT CẢ SINH VIÊN ==========");
            DisplayStudentList(list);
        }
    }
}
