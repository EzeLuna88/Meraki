using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using DAL;


namespace BLL
{


    public class BLLComprobante
    {
        DALComprobante access;

        public BLLComprobante()
        {
            access = new DALComprobante();
        }

        public List<BEComprobante> listaComprobantes()
        {
            return access.listaComprobantes();
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
