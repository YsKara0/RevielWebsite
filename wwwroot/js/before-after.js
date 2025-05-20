// Önce-Sonra Resim Karşılaştırma Özelliği (guzel ozellik oldugunu dusunerek ekledim tam istedigin gibi olmadi ama)
document.addEventListener('DOMContentLoaded', function() {
    initBeforeAfter();
});

function initBeforeAfter() {
    const container = document.querySelector('.img-comp-container');
    
    if (!container) return; // Container yoksa işlemi sonlandır
    
    const beforeDiv = document.querySelector('.img-comp-before');
    const slider = document.querySelector('.img-comp-slider');
    const sliderBtn = document.querySelector('.slider-button');
    
    // Başlangıç pozisyonunu ayarla
    positionSlider(50);
    
    // Fare olayları
    let isDragging = false;
    
    slider.addEventListener('mousedown', startDrag);
    window.addEventListener('mouseup', stopDrag);
    window.addEventListener('mousemove', drag);
    
    // Dokunmatik olaylar
    slider.addEventListener('touchstart', startDrag);
    window.addEventListener('touchend', stopDrag);
    window.addEventListener('touchcancel', stopDrag);
    window.addEventListener('touchmove', drag);
    
    // Otomatik animasyon - sayfa yüklendiğinde çalışacak
    setTimeout(() => {
        animateSlider();
    }, 1000);
    
    function startDrag(e) {
        e.preventDefault();
        isDragging = true;
        
        // Animasyonu durdur
        slider.style.transition = 'none';
        beforeDiv.style.transition = 'none';
    }
    
    function stopDrag() {
        isDragging = false;
    }
    
    function drag(e) {
        if (!isDragging) return;
        
        let x;
        
        if (e.type.includes('mouse')) {
            x = e.pageX;
        } else {
            x = e.touches[0].pageX;
        }
        
        // Konteyner üzerindeki göreceli pozisyonu hesapla
        const rect = container.getBoundingClientRect();
        const containerX = x - rect.left;
        const containerWidth = rect.width;
        
        // Yüzde hesapla
        let percent = (containerX / containerWidth) * 100;
        
        // Sınırları kontrol et
        percent = Math.min(Math.max(percent, 0), 100);
        
        positionSlider(percent);
    }
    
    function positionSlider(percent) {
        // Slider'ı ve beforeDiv'i pozisyonla
        slider.style.left = `${percent}%`;
        beforeDiv.style.width = `${percent}%`;
    }
    
    // Sayfa yüklendiğinde soldan sağa doğru animasyon
    function animateSlider() {
        slider.style.transition = 'left 1.5s ease-in-out';
        beforeDiv.style.transition = 'width 1.5s ease-in-out';
        
        // 10% -> 90% -> 50%
        setTimeout(() => positionSlider(10), 0);
        setTimeout(() => positionSlider(90), 700);
        setTimeout(() => positionSlider(50), 1400);
        
        // Animasyon bitince geçişleri kaldır
        setTimeout(() => {
            slider.style.transition = 'none';
            beforeDiv.style.transition = 'none';
        }, 2000);
    }
}
