namespace ProyectoGuitarra.Models
{
    public class SessionModel
    {
        public string? userName { get; set; } = null!;
        public string? email { get; set; } = null!;
        public string tipo { get; set; }  = null!;
        public string? password { get; set; } = null!;
        public string? tipoNavigation { get; set; }
        public string? token { get; set; }
        
        


    }
}