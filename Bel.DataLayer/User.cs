namespace Bel.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen İsim Giriniz!")]
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "İsim en fazla 250 karakter içermelidir!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen EMail Giriniz!")]
        [Display(Name = "EMail")]
        [StringLength(100, ErrorMessage = "EMail en fazla 100 karakter içermelidir!")]
        public string EMail { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre Giriniz!")]
        [Display(Name = "Password")]
        [StringLength(250, ErrorMessage = "Şifre en fazla 250 karakter içermelidir!")]
        public string Password { get; set; }
        public Nullable<int> RefUserRoleId { get; set; }
    }
}
