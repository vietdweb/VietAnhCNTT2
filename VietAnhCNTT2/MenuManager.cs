using System;

namespace VietAnhCNTT2
{
    /// <summary>
    /// Class: MenuManager - Quản lý menu và điều hướng các chức năng
    /// Author: Viet Anh
    /// </summary>
    internal class MenuManager
    {
        private StudentService studentService;
        private StudentConsoleView consoleView;

        public MenuManager(StudentService service, StudentConsoleView view)
        {
            studentService = service;
            consoleView = view;
        }

        /// <summary>
        /// Hiển thị menu chính
        /// </summary>
        public void DisplayMenu()
        {
            Console.WriteLine("\n========== CHỨC NĂNG QUẢN LÝ SINH VIÊN ==========");
            Console.WriteLine("1.  Thêm sinh viên");
            Console.WriteLine("2.  Hiển thị danh sách");
            Console.WriteLine("3.  Tìm sinh viên theo mã");
            Console.WriteLine("4.  Tìm gần đúng theo họ tên");
            Console.WriteLine("5.  Cập nhật sinh viên");
            Console.WriteLine("6.  Xóa sinh viên");
            Console.WriteLine("7.  Sắp xếp theo họ tên");
            Console.WriteLine("8.  Sắp xếp theo điểm trung bình");
            Console.WriteLine("9.  Hiển thị sinh viên có điểm từ 8 trở lên");
            Console.WriteLine("10. Hiển thị sinh viên có điểm cao nhất");
            Console.WriteLine("11. Tính điểm trung bình toàn bộ sinh viên");
            Console.WriteLine("12. Thống kê sinh viên theo ngành");
            Console.WriteLine("13. Thống kê sinh viên theo trạng thái");
            Console.WriteLine("14. Thoát");
            Console.WriteLine("=".PadRight(50, '='));
        }

        /// <summary>
        /// Chạy vòng lặp menu chính
        /// </summary>
        public void Run()
        {
            string choice;

            do
            {
                DisplayMenu();
                Console.Write("Bạn chọn chức năng: ");
                choice = Console.ReadLine() ?? "";

                HandleMenuChoice(choice);

            } while (choice != "14");

            Console.WriteLine("Bạn đã kết thúc chương trình.");
        }

        /// <summary>
        /// Xử lý lựa chọn menu
        /// </summary>
        private void HandleMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    consoleView.InputNewStudent();
                    break;

                case "2":
                    consoleView.DisplayAllStudents();
                    break;

                case "3":
                    consoleView.SearchByMaSV();
                    break;

                case "4":
                    consoleView.SearchByHoTen();
                    break;

                case "5":
                    consoleView.UpdateStudent();
                    break;

                case "6":
                    consoleView.DeleteStudent();
                    break;

                case "7":
                    consoleView.DisplaySortByHoTen();
                    break;

                case "8":
                    consoleView.DisplaySortByDiem();
                    break;

                case "9":
                    consoleView.DisplayDiem8Plus();
                    break;

                case "10":
                    consoleView.DisplayHighestDiem();
                    break;

                case "11":
                    consoleView.DisplayAverageDiem();
                    break;

                case "12":
                    consoleView.DisplayStatisticsByNganh();
                    break;

                case "13":
                    consoleView.DisplayStatisticsByTrangThai();
                    break;

                case "14":
                    // Thoát
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                    break;
            }

            // Tạm dừng để xem kết quả
            if (choice != "14")
            {
                Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
