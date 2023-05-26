namespace mpolaris.Models
{
    public class rpEventosDelictivosModel
    {
        public class griddelictivo
        {
            public string grupo { get; set; }
            public string subgrupo { get; set; }
            public string subflota { get; set; }
            public string alias { get; set; }
            public string placa { get; set; }
            public string tipoVehiculo { get; set; }
            public string eventoid { get; set; }
            public string fecha { get; set; }
            public string incidenteid { get; set; }
            public string costo { get; set; }
            public string incidentenombre { get; set; }
            public string descripcion { get; set; }
            public string motoristaid { get; set; }
            public string nombremotorista { get; set; }
            public string telefonomotorista { get; set; }
            public string mobileid { get; set; }
            public string subflotaid { get; set; }
            public string geozonaid { get; set; }
            public string nombregeozona { get; set; }
            public string idtipogd { get; set; }
            public string tipogd { get; set; }
        }

        public class treeDelictivos
        {
            public int nivel { get; set; }
            public string cod { get; set; }
            public string name { get; set; }
            public int valorid { get; set; }
            public string parent { get; set; }
            public string grandparent { get; set; }
            public string urlimagen { get; set; }
            public string plate { get; set; }
            public int motoristaid { get; set; }
            public string nombremostorista { get; set; }
        }
    }
}
