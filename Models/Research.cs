using System;
using System.ComponentModel.DataAnnotations;

namespace reviel.Models
{
    public class Research
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir.")]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Özet alanı zorunludur.")]
        [StringLength(500, ErrorMessage = "Özet en fazla 500 karakter olabilir.")]
        [Display(Name = "Özet")]
        public string Summary { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Yayınlanma Tarihi")]
        public DateTime PublicationDate { get; set; }

        [StringLength(100, ErrorMessage = "Yazar adı en fazla 100 karakter olabilir.")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "PDF URL alanı zorunludur.")]
        [Display(Name = "PDF Dosyası")]
        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        [StringLength(500, ErrorMessage = "PDF URL'si en fazla 500 karakter olabilir.")]
        public string PdfUrl { get; set; } = string.Empty;
    }
}
