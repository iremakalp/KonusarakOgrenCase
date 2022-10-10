## Konuşarak Öğren Backend Developer Case

### Veritabanı Kurulumu
- DataAccess katmanı : **dotnet ef database update**
- WebUI katmanı Identity kurulumu için : **dotnet ef database update**

### Kullanıcı Girişi
##### Admin :Email="admin@gmail.com", Password: 12345Test.
##### SysAdmin :Email="sysadmin@gmail.com", Password: 12345Tes.
##### Customer :Email="customer1@gmail.com", Password: 12345Test

### Kullanılan Yapılar
- .Net Core 6.0
- Asp.Net Core Identity
- Entity Framework
- Autofac
- Mssql Veritabanı

### Katmanlar
- **Entities Katmanı**: Veritabanı Tabloları
- **DataAccess Katmanı** : Veritabanı işlemleri
- **Business Katmanı**: İş kuralları
- **Core Katmanı**: Tüm projelerde ortak kullanılan yapılar, Repository
- **WebUI Katmanı**: Arayüz katmanı
