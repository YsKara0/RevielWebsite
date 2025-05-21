// Anasayfa JavaScript kodları
document.addEventListener('DOMContentLoaded', function() {

    // Hero bölümü için parçacık efekti
    const heroSection = document.querySelector('.hero-section');
    if (heroSection) {
        createParticles();
    }

    // Özellikler kartları için animasyon
    const featureCards = document.querySelectorAll('.feature-card');
    if (featureCards.length > 0) {
        initFeatureAnimations();
    }
});

// Arka plan parçacıkları oluştur bu dongüde baloncuklar için birden fazla fonksıyon var baloncukların boyutları ve animasyonları rastgele ayarlanıyor
function createParticles() {
    const heroSection = document.querySelector('.hero-section');
    const particleCount = 30; 
    
    for (let i = 0; i < particleCount; i++) {
        const particle = document.createElement('div');
        particle.classList.add('hero-particle');
        
        // Rastgele boyut, pozisyon ve animasyon gecikmesi
        const size = Math.random() * 50 + 15; // 15px ile 65px arası (daha büyük ve görünür baloncuklar)
        const posX = Math.random() * 100; // 0% ile 100% arası
        const posY = Math.random() * 100; // 0% ile 100% arası
        const delay = Math.random() * 4; // 0s ile 4s arası
        const duration = Math.random() * 5 + 5; // 5s ile 10s arası
        const opacity = Math.random() * 0.2 + 0.3; // 0.3 ile 0.5 arası opaklık (daha belirgin)
        
        particle.style.width = `${size}px`;
        particle.style.height = `${size}px`;
        particle.style.left = `${posX}%`;
        particle.style.top = `${posY}%`;
        particle.style.animationDelay = `${delay}s`;
        particle.style.animationDuration = `${duration}s`;
        particle.style.opacity = opacity;        // Bazı baloncuklarda farklı renk tonu ve animasyon kullanmak için ekledim
        const useAlternativeAnimation = Math.random() > 0.7; // Daha fazla alternatif animasyon için eşik düşürüldü
        if (useAlternativeAnimation) {
            particle.style.animation = `bubbleFloat ${duration}s infinite ease-in-out ${delay}s`;
            particle.style.backgroundColor = 'rgba(121, 82, 179, ' + (opacity + 0.1) + ')'; 
            particle.style.boxShadow = '0 0 5px rgba(121, 82, 179, 0.4)'; 
            particle.style.zIndex = '2';
        }
        
        heroSection.appendChild(particle);
    }
}

// Özellik kartları animasyonu
function initFeatureAnimations() {
    const featureCards = document.querySelectorAll('.feature-card');
    
    // Gözlemci oluştur
    const observer = new IntersectionObserver((entries) => {
        entries.forEach((entry) => {
            if (entry.isIntersecting) {
                // Animasyonla kartı göster
                entry.target.style.opacity = "1";
                entry.target.style.transform = "translateY(0)";
                entry.target.classList.add('animated');
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.1 });
    
    // Her kartı gözlemle
    featureCards.forEach((card, index) => {
        card.style.opacity = "0";
        card.style.transform = "translateY(30px)";
        card.style.transition = `opacity 0.5s ease, transform 0.5s ease`;
        card.style.transitionDelay = `${index * 0.1}s`;
        
        observer.observe(card);
    });
    
    // Sayfa yenilenmesi durumunda bile kartların görünür kalmasını sağlama
    window.addEventListener('load', () => {
        setTimeout(() => {
            featureCards.forEach(card => {
                if (!card.classList.contains('animated')) {
                    card.style.opacity = "1";
                    card.style.transform = "translateY(0)";
                    card.classList.add('animated');
                }
            });
        }, 500);
    });
}
