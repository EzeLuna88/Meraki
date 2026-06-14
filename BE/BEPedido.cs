using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum EstadoPedido
    {
        EnPreparacion = 1,
        EnCamino = 2,
        Entregado = 3
    }

    public class BEPedido
    {
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public BECliente Cliente { get; set; }
        public List<BEItem> ListaItems { get; set; }
        public decimal Total { get; set; }
        public bool PagoEfectivo { get; set; }
        public DateTime FechaEnvio { get; set; }
        public EstadoPedido Estado {  get; set; }

    }

        
}
