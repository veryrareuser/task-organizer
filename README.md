# Student Task Organizer

A simple console application for students who want to keep track of their assignments and deadlines. Tasks are entered through a simple numbered menu, stored in memory while the program runs, and written to a plain-text file so your list is still there the next time you start the app.

## Features

- **Add a task** — enter a task name and its deadline.
- **List all tasks** — shows every task with its deadline and its current status (`Selesai` / `Belum`).
- **Mark a task as done** — pick a task by number and mark it as finished.
- **Delete a task** — remove a task from the list by number.
- **Persistent storage** — the task list is saved to `tasks.txt` and reloaded automatically the next time the app starts, so nothing is lost between sessions.

## Tech Stack

| Component  | Choice              |
| ---------- | ------------------- |
| Language   | C#                  |
| Framework  | .NET 9 (`net9.0`)   |
| App type   | Console application |
| Storage    | Plain-text file     |
| Packages   | None (no NuGet deps) |

## How to Run

Requirements: the [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (or a newer SDK that can target `net9.0`).

```bash
cd task_organizer
dotnet restore
dotnet run
```

The app starts with a menu:

```
===== STUDENT TASK ORGANIZER =====
1. Tambah Tugas
2. Lihat Daftar Tugas
3. Tandai Tugas Selesai
4. Hapus Tugas
5. Keluar
Pilih menu (1-5):
```

## Data Storage

Tasks are persisted in a file named `tasks.txt`, created in the working directory of the running executable (the project folder when you use `dotnet run`). Each line holds one task in the format `title|deadline|isDone`, and the file is rewritten on every change. Because it is a plain-text file, you can open it to inspect or back up your list — and it is excluded from version control via `.gitignore`.
