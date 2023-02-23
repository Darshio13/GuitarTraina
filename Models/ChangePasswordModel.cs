namespace ProyectoGuitarra.Models
{
    public class ChangePasswordModel
    {
        public string? email { get; set; }
        public string? token { get; set; }
        public string? password { get; set; }
    }
}