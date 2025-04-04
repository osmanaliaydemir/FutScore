# FutScore Projesi TODO Listesi

## 1. Veri Doğrulama ve Validasyon
- [x] Match entity'sinde StadiumId alanı için validasyon eklenmeli
- [x] CreateMatchDto ve UpdateMatchDto'da tüm alanlar için validasyon kuralları eklenmeli
- [x] Takım seçimlerinde aynı takımın hem ev sahibi hem deplasman olarak seçilmesi engellenmeli
- [x] Maç tarihi validasyonu (geçmiş tarih kontrolü) eklenmeli

## 2. Kullanıcı Arayüzü İyileştirmeleri
- [x] Maç listesinde filtreleme özelliği eklenmeli
- [ ] Maç detay sayfasında takım logoları gösterilmeli
- [x] Maç durumu için renk kodlaması eklenmeli (örn: Planlandı - Mavi, Canlı - Yeşil)
- [x] Responsive tasarım iyileştirmeleri yapılmalı
- [ ] Loading state'leri eklenmeli

## 3. Performans İyileştirmeleri
- [ ] Maç listesi için sayfalama (pagination) eklenmeli
- [ ] Gereksiz veri çekme işlemleri optimize edilmeli
- [ ] Cache mekanizması eklenmeli
- [ ] Lazy loading implementasyonu yapılmalı

## 4. Güvenlik
- [ ] Kullanıcı yetkilendirme sistemi geliştirilmeli
- [ ] API endpoint'leri için rate limiting eklenmeli
- [ ] XSS ve CSRF koruması güçlendirilmeli
- [ ] Input sanitization eklenmeli

## 5. Test
- [ ] Unit testler yazılmalı
- [ ] Integration testler eklenmeli
- [ ] UI testleri eklenmeli
- [ ] Performance testleri yapılmalı

## 6. Kod Kalitesi
- [ ] AutoMapper profilleri düzenlenmeli
- [ ] Repository pattern implementasyonu iyileştirilmeli
- [ ] Exception handling merkezi hale getirilmeli
- [ ] Loglama sistemi geliştirilmeli
- [ ] Kod tekrarları azaltılmalı

## 7. Özellik Eksiklikleri
- [ ] Maç istatistikleri eklenmeli
- [ ] Oyuncu performans değerlendirmeleri eklenmeli
- [ ] Maç yorumları/notları eklenmeli
- [ ] Maç bildirimleri sistemi eklenmeli
- [ ] Maç sonuçları için otomatik bildirim sistemi

## 8. Deployment ve DevOps
- [ ] CI/CD pipeline'ı kurulmalı
- [ ] Docker container'ları hazırlanmalı
- [ ] Monitoring ve alerting sistemi kurulmalı
- [ ] Backup ve recovery planı oluşturulmalı

## 9. Dokümantasyon
- [ ] API dokümantasyonu oluşturulmalı
- [ ] Kod dokümantasyonu geliştirilmeli
- [ ] Kullanıcı kılavuzu hazırlanmalı
- [ ] Deployment dokümantasyonu eklenmeli

## 10. Paket Yönetimi
- [ ] AutoMapper paket versiyonları güncellenmeli
- [ ] Diğer paketlerin versiyonları kontrol edilmeli
- [ ] Gereksiz paketler kaldırılmalı
- [ ] Paket bağımlılıkları optimize edilmeli
