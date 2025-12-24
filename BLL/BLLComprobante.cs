using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using MPP;


namespace BLL
{


    public class BLLComprobante
    {
        MPPComprobante access;

        public BLLComprobante()
        {
            access = new MPPComprobante();
        }

        public List<BEComprobante> listaComprobantes()
        {
            return access.ListaComprobantes();
        }

        public void GuardarNuevoComprobante(BEComprobante beComprobante)
        {
            access.GuardarNuevoComprobante(beComprobante);
        }

        public List<BEComprobante> filtroComprobantes(DateTime inicio, DateTime final, BECliente cliente)

        {
            return access.FiltroComprobantes(inicio, final, cliente);
        }


    }
}
