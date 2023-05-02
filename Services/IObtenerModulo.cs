using SCLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCLC.Services
{
    public interface IObtenerModulo
    {
        Task<List<Reservacion>> ObtenerModulo(int modulo, int lab, DateTime fecha);
    }
}
