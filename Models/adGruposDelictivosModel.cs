namespace mpolaris.Models
{
    public class adGruposDelictivosModel
    {
        //Tipo incidente
        public class DDL_CatIncidentes
        {
            public int? id { get; set; }
            public string? nombre { get; set; }
        }

        //Vehículos
        public class DDL_vehiculos
        {
            public int nivel { get; set;}
            public string cod { get; set; }
            public string name { get; set; }
           // public int valorid { get; set; }
            public string parent { get; set; }
            public string grandparent { get; set; }
            public string urlimagen { get; set; }
            public string plate { get; set; }
            public int motoristaid { get; set; }
            public string nombremotorista { get; set; }
        }

        //Motoristas
        public class DDL_motoristas
        {
            public int? id { get; set; }
            public string? nombre { get; set; }
        }

        //Geozonas
        public class DDL_geozonas
        {
            public int? id { get; set; }
            public string? nombre { get; set; }
        }

        //Geozonas Detalle
        public class DDL_geozonas_Detalle
        {
            public int? id { get; set; }
            public int orden { get; set; }
            public string lat { get; set; }
            public string lng { get; set; }
            //public string? nombre { get; set; }
        }

        //DDL Rutas
        public class DDL_Ruta
        {
            public int? id { get; set; }
            public string? nombre { get; set; }
        }

        public class Grid_ListadoEventos
        {
            public int eventoid { get; set; }
            public string? fecha { get; set; }
            public int incidenteid { get; set; }
            public string? incidentenombre { get; set; }
            public string? descripcion { get; set; }
            public double costo { get; set; }
            public int motoristaid { get; set; }
            public string? nombremotorista { get; set; }
            public string? telefonomotorista { get; set; }
            public int mobileid { get; set; }
            public string? alias { get; set; }
            public string? placa { get; set; }
            public int geozonaid { get; set; }
            public string? nombregeozona { get; set; }
            public int rutaid { get; set; }
            public string estado { get; set; }
            public string contactovecino { get; set; }
        }
       
        public class tipoinconvenientes_model {
            public int id { get; set; }
            public string descripcion { get; set; }
        }
        public class incidentescat_model
        {
            public int id { get; set; }
            public string nombre { get; set; }
        }
        public class pagorenta_model
        {
            public int id { get; set; }
            public string nombre { get; set; }
        }

        public class msj
        {
            public int tipo { get; set; }
            public string mensaje { get;}
        }

    }
}
