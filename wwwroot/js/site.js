// Site-wide JavaScript

// Footer'ın her zaman sayfanın altına sabitlenmesini sağlar
function adjustFooterPosition() {
    const pageHeight = document.documentElement.scrollHeight;
    const windowHeight = window.innerHeight;
    const footer = document.querySelector('.footer');
    
    if (pageHeight <= windowHeight) {
        // Sayfa yüksekliği pencere yüksekliğinden küçükse, footer'ı en alta sabitle(footer bazen yukari cikiyordu kendi kendine cozmek icin denedim)
        footer.style.position = 'fixed';
        footer.style.bottom = '0';
        footer.style.left = '0';
    } else {
        // Sayfa yüksekliği pencere yüksekliğinden büyükse, footer normal akışta kalsın
        footer.style.position = 'relative';
        footer.style.bottom = 'auto';
    }
}

// Sayfa yüklendiğinde ve yeniden boyutlandırıldığında footer pozisyonunu ayarla
document.addEventListener('DOMContentLoaded', function() {
    adjustFooterPosition();
    
    // Sayfa içeriği değişebileceğinden, biraz gecikmeyle tekrar kontrol et
    setTimeout(adjustFooterPosition, 200);
    
    window.addEventListener('resize', adjustFooterPosition);
});
