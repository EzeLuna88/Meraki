using MPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLConfiguracion
    {
        MPPConfiguracion mppConfiguracion;

        public BLLConfiguracion()
        {
            mppConfiguracion = new MPPConfiguracion();
        }

        public void GuardarDiasAvisoVencimiento(int dias)
        {
            mppConfiguracion.GuardarDiasAvisoVencimiento(dias);
        }

        public int ObtenerDiasAvisoVencimiento()
        {
            return mppConfiguracion.ObtenerDiasAvisoVencimiento();
        }



    }
}
