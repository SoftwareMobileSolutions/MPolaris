namespace mpolaris.Models
{
    public class mp_vermapaModel
    {
        public class nivelpeligrosidad
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string descripcion { get; set; }
            public int color { get; set; }
            public int companyid { get; set; }
            public bool alarma { get; set; }
        }

        public class geodelictiva_r1
        {
            public int id { get; set; }
            public string geoname { get; set; }
            public string x { get; set; }
            public string y { get; set; }
            //public string cod { get; set; } 
            public string valorRenta { get; set; }
            // public int color { get; set; }// Determinado por el nivel peligrosidad
            public string grupodelictivo { get; set; }
            public string valorAlivian { get; set; }
            public int estadoRenta { get; set; }
            public int estadoAlivian { get; set; }
            public string mobiles { get; set; }
            public int canteventos { get; set; }
            public int formaPago { get; set; }
            public string peligrosidad { get; set; }

            public string departamento { get; set; }
            public string municipio { get; set; }
        }

        public class geodelictiva_r2
        {
            public int id { get; set; }
            public int orden { get; set; }
            public string lat { get; set; }
            public string lng { get; set; }
        }

        //Modal Reporte Anual por geozona
        public class rpGeoEventoAnual {
            public int incidenteid { get; set; }
            public string? fecha { get; set; }
            public string? asunto { get; set; }
            public string? geo { get; set; }
            public string? estado { get; set; }
            public string? vehiculo_apoyo  { get; set; }
            public string? placa  { get; set; }
            public string? vehiculo { get; set; }
            public string? motorista { get; set; }

            public float costo { get; set; }
        }
    }
    
}
