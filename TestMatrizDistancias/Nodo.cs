using System;
using System.Collections.Generic;
using System.Text;

namespace TestMatrizDistancias
{
    public class Nodo
    {
        public int Id { get; set; }
        public GeoPoint GeoLocation { get; set; }
        public List<ConexionNodo> Aristas { get; set; }
    }
}
