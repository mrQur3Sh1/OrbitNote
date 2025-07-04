# 🪐 OrbitNote — A Modern MVVM WPF Notebook App

OrbitNote is a clean, professional notebook application built using **C#**, **WPF**, and the **MVVM architecture**. It allows users to manage notes with a beautiful UI, structured note lifecycle, and robust export functionality.

---

## ✨ Features

- 📝 Create, edit, and delete notes
- 🔄 Organize notes by state: Draft, Final, Archived
- 🔍 Search and filter through your note collection
- 💾 Save notes in `.dat` (binary), `.xml`, and `.html`
- 📊 Visualize note statistics with pie chart summaries
- 🧠 Built with **strict MVVM** — zero code-behind logic
- 📨 In-app communication via a custom MessageBus
- 🎨 Dark theme and professional UI styling

---

## 🧱 Folder Structure

```
OrbitNote/
│
├── Business.Model/                  # Data models (e.g., DiaryEntry.cs)
├── Logic.Ui/                        # ViewModels, RelayCommand, ViewModelLocator
├── Services.MessageBus/             # AppMessenger for MVVM messaging
├── Services.SerializationService/   # Save/load logic for .dat, .xml, .html
├── Ui.Desktop/                      # Views (MainWindow.xaml, NoteEditor.xaml, etc.)
├── AwpAppData/                      # notes.dat, notes.xml, notes.html
│
├── App.xaml                         # Application startup
├── OrbitNote.sln                    # Visual Studio Solution file
```

---

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2022+
- .NET Framework (WPF Desktop)

### Installation

```bash
git clone https://github.com/your-username/OrbitNote.git
cd OrbitNote
```

1. Open the `OrbitNote.sln` file in Visual Studio
2. Build the project
3. Run the application (`F5`)

---


- 📋 MainWindow with note list and toolbar
- ✍️ NoteEditor with formatting and state
- 🔍 SearchWindow with filters
- 📊 StatisticsWindow showing pie chart

---

## 🛠 Technologies Used

| Technology          | Purpose                              |
|---------------------|--------------------------------------|
| C#                  | Core programming language            |
| WPF (XAML)          | Desktop UI framework                 |
| MVVM Pattern        | Clean separation of concerns         |
| RelayCommand        | Command bindings (no code-behind)    |
| AppMessenger        | Decoupled communication              |
| File I/O            | Note serialization in multiple formats |

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork this repository 🍴  
2. Create a new branch: `git checkout -b feature-name`  
3. Commit your changes: `git commit -m 'Add feature'`  
4. Push to the branch: `git push origin feature-name`  
5. Open a Pull Request ✅

---

## 📄 License

This project is licensed under the MIT License — use it freely for personal and professional use.

---

> 🪐 OrbitNote — where every note completes its cycle.
