using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FoolStuff.Models
{
    // Modelli utilizzati come parametri per le azioni AccountController.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "Token di accesso esterno")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password corrente")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La lunghezza di {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nuova password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma nuova password")]
        [Compare("NewPassword", ErrorMessage = "La nuova password e la password di conferma non corrispondono.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La lunghezza di {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma password")]
        [Compare("Password", ErrorMessage = "La password e la password di conferma non corrispondono.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModelCustomUser
    {
        [Required]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La lunghezza di {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Phone { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Provider di accesso")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Chiave provider")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "La lunghezza di {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nuova password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma nuova password")]
        [Compare("NewPassword", ErrorMessage = "La nuova password e la password di conferma non corrispondono.")]
        public string ConfirmPassword { get; set; }
    }
}
