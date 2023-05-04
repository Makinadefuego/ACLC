using SCLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCLC.Services
{
    interface IReservarComputadoras
    {
        Task<bool> ReservarComputadora(List<Computadora> computadoras, DateTime fecha, Usuario usuario);
    }
}
