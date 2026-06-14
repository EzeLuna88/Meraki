using BE;
using MPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPedido
    {
        MPPPedido access;

        public BLLPedido()
        {
            access = new MPPPedido();
        }

        public string GenerarNumeroPedido()
        {
            return access.ObtenerProximoNumeroPedido();
        }

        public bool GuardarPedido(BEPedido pedido)
        {
            // Acá podés poner validaciones extra en el futuro si querés
            return access.GuardarPedido(pedido);
        }

        public List<BEPedido> ListarPedidosEnPreparacion()
        {
            // Le pasamos directamente nuestro Enum convertido a número (1)
            return access.ListarPedidosPorEstado((int)EstadoPedido.EnPreparacion);
        }

        public List<BEPedido> ListarPedidosEnCamino()
        {
            return access.ListarPedidosPorEstado((int)EstadoPedido.EnCamino);
        }

        public BEPedido ObtenerPedidoCompleto(string numeroPedido)
        {
            return access.ObtenerPedidoCompleto(numeroPedido);
        }

        public void ActualizarPedidoEditado(BEPedido pedido)
        {
            access.ActualizarPedidoEditado(pedido);
        }

        public void CambiarEstadoPedido(string numeroPedido, EstadoPedido nuevoEstado)
        {
            access.CambiarEstadoPedido(numeroPedido, nuevoEstado);
        }
    }
}
