using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Dron
    {
        public int NumeroSerie { get; set; }
        public string Modelo { get; set; }
        public int PesoLimite { get; set; }
        public int CapacidadBateria { get; set; }
        public string Estado { get; set; }
        public List<Medicamento> Medicamentos { get; set; } 
    }
}
