namespace AtWork.Shared.DTO.Horario
{
    public class DiaDTO
    {
        public string Dia_Da_Semana { get; set; }
        public TimeOnly Hora_Inicio { get; set; }
        public TimeOnly Hora_Final { get; set; }
        public string ST_Status { get; set; }
    }
}
