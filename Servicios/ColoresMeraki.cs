using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Servicios
{
    public static class ColoresMeraki
    {
        // Tonos principales de la marca
        public static readonly Color BordoOscuro = Color.FromArgb(64, 15, 28);
        public static readonly Color BordoPrincipal = Color.FromArgb(146, 26, 64);
        public static readonly Color RosaPalido = Color.FromArgb(217, 171, 171);

        // Tonos neutros y fondos
        public static readonly Color GrisFondo = Color.FromArgb(240, 240, 240);
        public static readonly Color BlancoTexto = Color.White;
        public static readonly Color TextoOscuro = Color.Black; // Por si lo necesitás para etiquetas

        // Colores de estado (¡Ideales para tus botones de "Entregado" o "Pendiente"!)
        public static readonly Color ExitoVerde = Color.FromArgb(46, 204, 113);
        public static readonly Color AlertaNaranja = Color.FromArgb(243, 156, 18);
        public static readonly Color ErrorRojo = Color.FromArgb(231, 76, 60);
    }
}
