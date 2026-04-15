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
            if (dias < 0)
            {
                throw new ArgumentException("Los días de anticipación no pueden ser negativos.");
            }
            if (dias > 1825)
            {
                throw new ArgumentException("El máximo de días de anticipación permitido es 1825 (5 años).");
            }
            mppConfiguracion.GuardarDiasAvisoVencimiento(dias);
        }

        public int ObtenerDiasAvisoVencimiento()
        {
            return mppConfiguracion.ObtenerDiasAvisoVencimiento();
        }



    }
}
