// contact.js - İletişim sayfası için JavaScript fonksiyonları

document.addEventListener('DOMContentLoaded', function() {
    // Form gönderimi için event listener
    const contactForm = document.getElementById('contactForm');
    if (contactForm) {
        contactForm.addEventListener('submit', function(event) {
            event.preventDefault();
            
            // Form verilerini al
            const formData = new FormData(contactForm);
            const formValues = {
                name: formData.get('name'),
                email: formData.get('email'),
                subject: formData.get('subject'),
                message: formData.get('message')
            };
            
            // Form verilerini konsola yazdır (geliştirme aşamasında)
            console.log('Form verileri:', formValues);
            
            // Gerçek uygulamada bu kısımda bir API'ye istek gönderilir
            // Örnek API çağrısı (şu an devre dışı):
            /*
            fetch('/api/contact', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formValues)
            })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    showSuccessMessage();
                    contactForm.reset();
                } else {
                    showErrorMessage(data.message || 'Bir hata oluştu. Lütfen tekrar deneyin.');
                }
            })
            .catch(error => {
                console.error('Hata:', error);
                showErrorMessage('Bir hata oluştu. Lütfen tekrar deneyin.');
            });
            */
            
            // Şimdilik sadece başarılı mesajı gösteriyoruz (geliştirme aşamasında)
            showSuccessMessage();
            contactForm.reset();
        });
    }
    
    // Başarılı mesajı göster
    function showSuccessMessage() {
        // Alert elemanı oluştur
        const alertElement = document.createElement('div');
        alertElement.className = 'alert alert-success fade show mt-3';
        alertElement.role = 'alert';
        alertElement.innerHTML = `
            <strong>Teşekkürler!</strong> Mesajınız başarıyla gönderildi. En kısa sürede sizinle iletişime geçeceğiz.
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        `;
        
        // Alert'i forma ekle
        const formContainer = document.querySelector('.contact-form-container');
        formContainer.insertBefore(alertElement, contactForm);
        
        // 5 saniye sonra alert'i kaldır
        setTimeout(() => {
            alertElement.classList.remove('show');
            setTimeout(() => {
                alertElement.remove();
            }, 150);
        }, 5000);
    }
    
    // Hata mesajı göster
    function showErrorMessage(message) {
        // Alert elemanı oluştur
        const alertElement = document.createElement('div');
        alertElement.className = 'alert alert-danger fade show mt-3';
        alertElement.role = 'alert';
        alertElement.innerHTML = `
            <strong>Hata!</strong> ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        `;
        
        // Alert'i forma ekle
        const formContainer = document.querySelector('.contact-form-container');
        formContainer.insertBefore(alertElement, contactForm);
        
        // 5 saniye sonra alert'i kaldır
        setTimeout(() => {
            alertElement.classList.remove('show');
            setTimeout(() => {
                alertElement.remove();
            }, 150);
        }, 5000);
    }
});
