namespace CLINICAUHBenavides.BackEnd
{
    public class Paciente
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
    }
    public class Medico
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Especialidad { get; set; }
    }
    public class Detalle
    {
        public int NumeroConsulta { get; set; }
        public string Detalles { get; set; }
        public DateTime FechaAtencion { get; set; }
        public TimeSpan HoraAtencion { get; set; }
        public int NumeroConsultorio { get; set; }
        public string CedulaPaciente { get; set; }
        public int IDMedico { get; set; }
    }
}
