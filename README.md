# 🚀 AstraTech Apps Backend (.NET 8)

## 📘 Deskripsi Proyek
AstraTech Apps Backend adalah layanan backend berbasis **.NET 8** yang dirancang menggunakan pendekatan **Clean Architecture** untuk memastikan struktur kode yang modular, mudah diuji, dan scalable.

Proyek ini menyediakan API utama yang mengelola data yang terkait dengan sistem AstraTech Apps.

> ⚠️ **PERINGATAN:**
> Repository ini **BUKAN UNTUK PENGEMBANGAN LANGSUNG**.  
> DILARANG melakukan **push** ke repository ini.  
> Gunakan repository turunan (fork / repo pribadi) untuk melakukan pengembangan, lalu lakukan *merge request* atau *pull request* sesuai prosedur tim.

---

## 🧱 Struktur Folder
Struktur proyek disusun dengan pendekatan berbasis layer:

```
├── Controllers/           # Endpoint API
│   ├── AuthController.cs
│   ├── InstitusiController.cs
│
├── DTOs/                  # Data Transfer Objects (request & response)
│   ├── Auth/
│   ├── Institusi/
│
├── Helpers/               # Utility dan fungsi umum
├── Libs/                  # Library / ekstensi tambahan
├── Models/                # Entity model untuk ORM
├── Repositories/          # Abstraksi data layer
│   ├── Implementations/
│   ├── Interfaces/
│
├── Services/              # Logika bisnis
│   ├── Implementations/
│   ├── Interfaces/
│
├── appsettings.json       # Konfigurasi aplikasi
├── Dockerfile             # Konfigurasi Docker
├── Program.cs             # Entry point aplikasi
├── astratech-apps-backend.csproj
└── .gitlab-ci.yml         # Pipeline CI/CD (jika digunakan)
```

---

## 🛠️ Tech Stack
- **.NET 8.0 (Minimal)**
- **C# 12**
- **Entity Framework Core**
- **SQL Server**
- **Swagger (Swashbuckle)** untuk dokumentasi API
- **Dependency Injection (DI)** untuk pengelolaan service
- **Docker** untuk containerization
- **GitLab CI/CD** untuk otomatisasi build & deploy

---

## 🚀 Cara Menjalankan Proyek (Local)
Pastikan Anda telah menginstal:
- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- SQL Server / SQL Express
- Visual Studio 2022 atau VS Code

Langkah-langkah:
```bash
# 1️⃣ Clone repository
git clone <url-repo-pengembangan-anda>

# 2️⃣ Masuk ke folder proyek
cd astratech-apps-backend

# 3️⃣ Restore dependencies
dotnet restore

# 4️⃣ Jalankan migrasi database (jika ada)
dotnet ef database update

# 5️⃣ Jalankan aplikasi
dotnet run
```

Akses API di browser:
```
https://localhost:5001/swagger (sesuaikan)
```
## 🚀 Setting Environment Variable Windows
Tambahkan key berikut

DECRYPT_KEY_JWT  : 518d96e5383b5606c4722f60f0ce7f9d8710a4bd383312815341ea1a664abd21
DECRYPT_KEY_CONNECTION_STRING : 80ad226fefefa6565197e091d6c465d2
---

## 🧩 Panduan Kontribusi
1. **Jangan lakukan push langsung** ke repository utama ini.  
2. Gunakan **repo pengembangan** seperti essa backend / sia backend sesuai dengan project kalian. 
3. Gunakan **branch dev**.  
4. Pastikan kode mengikuti standar dan guideline tim.

---

## 🧠 Catatan Tambahan
- Semua konfigurasi rahasia (seperti connection string, API key, dll) **tidak disimpan di repo publik**.  
- Gunakan file `appsettings.Development.json` atau environment variables untuk kebutuhan lokal.  
- CI/CD otomatis akan menolak build yang dilakukan dari repository unauthorized.

---

## 🪪 Lisensi
Hak cipta dilindungi sepenuhnya.  
Diperuntukkan untuk penggunaan **internal AstraTech Apps**.
