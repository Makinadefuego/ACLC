using SCLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCLC.Services
{
    public class ComputadorasReservacionServicio : IReservarComputadoras
    {
        public async Task<bool> ReservarComputadora(List<Computadora> computadoras, DateTime fecha, Usuario usuario)
        {
            bool resultado = false;
            var client = new HttpClient();
            string fechaFinal = fecha.ToString("dd/MM/yyyy");
            //string fechaFinal = "04/30/2023";
            string parametros = "?usuario=" + usuario.boleta + "&date=" + fechaFinal;
            //string url = Direccion.direccionNancy + "Reservacion/ReservarComputadora" + parametros;
            string url = Direccion.direccionLocal + "Reservacion/ReservarComputadora" + parametros;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsync("", null);
            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }
            Console.WriteLine(response);
            return resultado;
        }
    }

}
