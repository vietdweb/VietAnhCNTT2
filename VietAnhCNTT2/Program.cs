using System;

namespace VietAnhCNTT2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("CHƯƠNG TRÌNH QUẢN LÝ SINH VIÊN\n");

            // Khởi tạo StudentService
            StudentService studentService = new StudentService();
            studentService.InitializeDefaultData();

            // Khởi tạo StudentConsoleView
            StudentConsoleView consoleView = new StudentConsoleView(studentService);

            // Khởi tạo MenuManager và chạy
            MenuManager menuManager = new MenuManager(studentService, consoleView);
            menuManager.Run();
        }
    }
}