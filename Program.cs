using System;
using System.Collections.Generic;
using System.IO;

namespace StudentTaskOrganizer
{
    class Task
    {
        public string Title { get; set; }
        public string Deadline { get; set; }
        public bool IsDone { get; set; }

        public Task(string title, string deadline, bool isDone = false)
        {
            Title = title;
            Deadline = deadline;
            IsDone = isDone;
        }
    }

    class Program
    {
        static List<Task> taskList = new List<Task>();
        static string filePath = "tasks.txt";

        static void Main(string[] args)
        {
            LoadFromFile();

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("===== STUDENT TASK ORGANIZER =====");
                Console.WriteLine("1. Tambah Tugas");
                Console.WriteLine("2. Lihat Daftar Tugas");
                Console.WriteLine("3. Tandai Tugas Selesai");
                Console.WriteLine("4. Hapus Tugas");
                Console.WriteLine("5. Keluar");
                Console.Write("Pilih menu (1-5): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ShowTasks();
                        break;
                    case "3":
                        MarkTaskDone();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":
                        running = false;
                        SaveToFile();
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AddTask()
        {
            Console.Clear();
            Console.WriteLine("=== Tambah Tugas Baru ===");
            Console.Write("Masukkan nama tugas: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Masukkan deadline tugas: ");
            string deadline = Console.ReadLine() ?? "";

            taskList.Add(new Task(title, deadline));
            SaveToFile();

            Console.WriteLine("\nTugas berhasil ditambahkan!");
            Console.ReadKey();
        }

        static void ShowTasks()
        {
            Console.Clear();
            Console.WriteLine("=== Daftar Tugas ===");

            if (taskList.Count == 0)
            {
                Console.WriteLine("Belum ada tugas yang ditambahkan.");
            }
            else
            {
                for (int i = 0; i < taskList.Count; i++)
                {
                    Task t = taskList[i];
                    Console.WriteLine($"{i + 1}. {t.Title} - Deadline: {t.Deadline} - Status: {(t.IsDone ? "Selesai" : "Belum")}");
                }
            }

            Console.WriteLine("\nTekan tombol apa saja untuk kembali ke menu...");
            Console.ReadKey();
        }

        static void MarkTaskDone()
        {
            Console.Clear();
            Console.WriteLine("=== Tandai Tugas Selesai ===");

            if (taskList.Count == 0)
            {
                Console.WriteLine("Belum ada tugas untuk ditandai.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < taskList.Count; i++)
            {
                Task t = taskList[i];
                Console.WriteLine($"{i + 1}. {t.Title} - Status: {(t.IsDone ? "Selesai" : "Belum")}");
            }

            Console.Write("\nMasukkan nomor tugas yang ingin ditandai selesai: ");
            bool validInput = int.TryParse(Console.ReadLine(), out int index);

            if (!validInput || index < 1 || index > taskList.Count)
            {
                Console.WriteLine("Nomor tugas tidak valid!");
            }
            else
            {
                taskList[index - 1].IsDone = true;
                SaveToFile();
                Console.WriteLine("Tugas berhasil ditandai sebagai selesai!");
            }

            Console.WriteLine("Tekan tombol apa saja untuk kembali...");
            Console.ReadKey();
        }

        static void DeleteTask()
        {
            Console.Clear();
            Console.WriteLine("=== Hapus Tugas ===");

            if (taskList.Count == 0)
            {
                Console.WriteLine("Belum ada tugas untuk dihapus.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < taskList.Count; i++)
            {
                Task t = taskList[i];
                Console.WriteLine($"{i + 1}. {t.Title} - Deadline: {t.Deadline}");
            }

            Console.Write("\nMasukkan nomor tugas yang ingin dihapus: ");
            bool validInput = int.TryParse(Console.ReadLine(), out int index);

            if (!validInput || index < 1 || index > taskList.Count)
            {
                Console.WriteLine("Nomor tugas tidak valid!");
            }
            else
            {
                taskList.RemoveAt(index - 1);
                SaveToFile();
                Console.WriteLine("Tugas berhasil dihapus!");
            }

            Console.WriteLine("Tekan tombol apa saja untuk kembali...");
            Console.ReadKey();
        }

        static void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var t in taskList)
                {
                    writer.WriteLine($"{t.Title}|{t.Deadline}|{t.IsDone}");
                }
            }
        }

        static void LoadFromFile()
        {
            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    string title = parts[0];
                    string deadline = parts[1];
                    bool done = bool.Parse(parts[2]);

                    taskList.Add(new Task(title, deadline, done));
                }
            }
        }
    }
}
