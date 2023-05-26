namespace mpolaris.Models
{
    public class rpPagosRealizadosModel
    {
        public class r1_pagos
        {
            public int incidenteid { get; set; }
            public string motorista { get; set; }
            public string placa { get; set; }
            public string alias { get; set; }
            public string geozona { get; set; }
            public string geozonaid { get; set; }
            public string fecha { get; set; }
            public string descripcion { get; set; }
            public int eventoid { get; set; }
            public float costo { get; set; }
            public float lng { get; set; }
            public float lat { get; set; }
            public string ruta { get; set; }
            public string contacto { get; set; }
            public string estado { get; set; }
            public string mobileapoyo { get; set; }
            public string tipopagorenta { get; set; }
            public string fechapagorenta { get; set; }
            public string incidente { get; set; }
            public string inconveneniente { get; set; }
        }
        public class r1_geodetalle
        {
            public int id { get; set; }
            public int orden { get; set; }
            public float lat { get; set; }
            public float lng { get; set; }
        }
    }
}
