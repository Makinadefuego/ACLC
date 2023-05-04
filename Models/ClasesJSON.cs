using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCLC.Models
{

    public class ReservaJSON
    {
        public int idReserva { get; set; }
        public DateTime fechahora_reserva { get; set; }
        public int modulo_sreservacion { get; set; }
        public UsuarioJSON usuario { get; set; }
        public ComputadoraJSON computadora { get; set; }
        public LaboratorioJSON laboratorio { get; set; }
    }

    public class UsuarioJSON
    {
        public int boleta { get; set; }
        public string password { get; set; }
    }

    public class ComputadoraJSON
    {
        public int idComputadora { get; set; }
        public int lab_id { get; set; }
        public LaboratorioJSON laboratorio { get; set; }
    }

    public class LaboratorioJSON
    {
        public int idLaboratorio { get; set; }
    }

}
