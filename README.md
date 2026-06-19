RENTALY- ARAÇ KİRALAMA YÖNETİM SİSTEMİ

ASP.NET Core MVC mimarisiyle geliştirilmiş, hem son kullanıcıların araç kiralayabildiği şık bir müşteri arayüzü hem de tüm operasyonun yönetilebildiği gelişmiş bir Admin Paneli içeren tam kapsamlı bir araç kiralama web uygulamasıdır.

🚀 Öne Çıkan Özellikler
Müşteri Arayüzü
Akıllı Filtreleme ve Müsaitlik: Tarih, lokasyon, marka ve kategoriye göre dinamik arama. Seçilen tarihlerde çakışan rezervasyonu olan araçlar otomatik elenir.

Yapay Zeka Destekli OCR: Tesseract.js ile kimlik fotoğrafından (dosya veya kamera) isim ve soyisim bilgilerini otomatik okuyarak form alanına doldurma.

Hızlı ve Standart Rezervasyon: Anlık fiyat hesaplama ve e-posta adresi üzerinden çift kayıt önleme (deduplikasyon) mekanizması.

Admin Paneli (Area)
Dashboard: Toplam araç, rezervasyon, müşteri ve ciro istatistikleri; kategoriye/şubeye göre araç dağılım grafikleri.

Filo ve Müşteri Yönetimi: Araç (kart/liste görünümü, pagination, filtreleme), Marka, Model, Kategori, Şube ve DTO katmanlı Müşteri yönetimi.

Rezervasyon Onay Akışı: Onayla/Reddet sistemi; onay durumunda müşteriye otomatik HTML bilgilendirme maili ve indirim kuponu gönderimi.

İçerik Yönetimi: Ana sayfadaki tüm alanların (İşleyiş adımları, İstatistikler, Ödüller, Yorumlar, SSS) modal tabanlı CRUD yönetimi.

🏗️ Mimari Yapı ve Tasarım Desenleri
Proje, kurumsal yazılım standartlarına uygun olarak 5 Katmanlı Mimari (N-Tier) yapısıyla tasarlanmıştır:

Katmanlar: EntityLayer | DataAccessLayer | BusinessLayer | DTOLayer | WebUI

Repository Pattern: Kod tekrarını önlemek için asenkron ve T önekli (TGetListAsync, TInsertAsync vb.) Generic Repository yapısı.

Unit of Work: Veri tabanı işlemlerinde veri tutarlılığını sağlamak için transaction yönetimi.

AutoMapper: Müşteri kayıt süreçlerinde DTO ve Entity eşlemeleri için otomatik mapping.

EF Core Code First: Veri tabanı tasarımı ve ilişkileri C# sınıfları üzerinden yönetilmiştir.

🛠️ Kullanılan Teknolojiler
Backend: .NET 8 (ASP.NET Core MVC)

Veri Tabanı & ORM: SQL Server & Entity Framework Core

Dönüşümler & Mail: AutoMapper & Gmail SMTP (System.Net.Mail)

OCR (Kimlik Okuma): Tesseract.js (Tarayıcı tabanlı)

Frontend & UI: Bootstrap 5, MDB UI, Tabler Icons, Jarallax (Parallax), Owl Carousel


Proje Görselleri


<img width="1665" height="926" alt="1" src="https://github.com/user-attachments/assets/68d5486a-b5aa-45f8-9c8d-50bffc2f5858" />

<img width="913" height="945" alt="2" src="https://github.com/user-attachments/assets/c1ae8f55-a608-4a8e-8eac-1db090098273" />

<img width="859" height="939" alt="3" src="https://github.com/user-attachments/assets/4454f647-f480-4cea-a57a-02fb05a00487" />

<img width="1089" height="942" alt="4" src="https://github.com/user-attachments/assets/f26eedb9-ebbe-409d-a896-633702adbd1e" />

<img width="1119" height="815" alt="5" src="https://github.com/user-attachments/assets/de0f8666-d882-4723-a93a-b5178062ac71" />

<img width="1064" height="758" alt="Ekran görüntüsü 2026-06-19 161418" src="https://github.com/user-attachments/assets/b8fa721f-7f74-4992-8131-506472703525" />

<img width="1900" height="932" alt="Ekran görüntüsü 2026-06-19 161527" src="https://github.com/user-attachments/assets/fe2e42f5-89bf-4b7d-afe2-528672c9cc1a" />

<img width="1909" height="923" alt="Ekran görüntüsü 2026-06-19 161544" src="https://github.com/user-attachments/assets/e920c925-633e-4fcf-9608-ceddfd9d3555" />

<img width="1884" height="902" alt="Ekran görüntüsü 2026-06-19 161556" src="https://github.com/user-attachments/assets/070cc8be-8441-4645-8023-e63c329b6982" />

<img width="1907" height="934" alt="Ekran görüntüsü 2026-06-19 161632" src="https://github.com/user-attachments/assets/97cdf831-373b-42c9-9075-68dd4dacef69" />


