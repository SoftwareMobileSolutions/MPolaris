namespace mpolaris.Models
{
    public class rpLlamadasRealizadasModel
    {
        public int idEventos { get; set; }
        public string fechageneracion { get; set; }
        public string usuario { get; set; }
        public string nombre_user { get; set; }
        public string apellido_user { get; set; }
        public string tipoevento { get; set; }
        public string estadochequeo { get; set; }
        public string tipollamada { get; set; }
        public string llamada { get; set; }
        public string comentario { get; set; }
		public int mobileid { get; set; }
		public string vehiculo { get; set; }
		public string placa { get; set; }
	}
}
