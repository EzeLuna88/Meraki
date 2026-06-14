using BE;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios
{
    public class GeneradorListaDePrecios
    {
        public static void Generar(Dictionary<string, List<BEProducto>> datosAgrupados, string rutaDestino, string rutaLogo, bool esImprimible = false)
        {
            // 2. 🎨 LÓGICA DE COLORES DINÁMICA
            // Si es imprimible (Light), fondo blanco y texto negro. Si no (Premium), fondo oscuro.

            var colorFondoBase = esImprimible ? "#FFFFFF" : "#121212";
            var colorFondoFila = esImprimible ? "#F5F5F5" : "#1A1A1A"; // Un gris muy suave para alternar en blanco

            var colorTextoPrincipal = esImprimible ? "#1E1E1E" : "#FFFFFF"; // Negro oscuro o Blanco
            var colorTextoGris = esImprimible ? "#666666" : "#AAAAAA"; // Gris más oscuro para que se lea en blanco

            // Los bordó se mantienen intactos en ambos diseños porque son la identidad de la marca
            var colorBordoMeraki = "#921A40";
            var colorBordoOscuro = "#400F1C";

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.MarginHorizontal(1, Unit.Centimetre);
                    page.MarginTop(0.4f, Unit.Centimetre);
                    page.MarginBottom(1, Unit.Centimetre);

                    // 3. 🖌️ APLICAMOS LOS COLORES DINÁMICOS AL FONDO Y TEXTO POR DEFECTO
                    page.PageColor(colorFondoBase);
                    page.DefaultTextStyle(x => x.FontFamily("Segoe UI").FontColor(colorTextoPrincipal));

                    page.Header().Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().AlignMiddle().Column(c =>
                            {
                                if (!string.IsNullOrEmpty(rutaLogo) && System.IO.File.Exists(rutaLogo))
                                {
                                    
                                        c.Item().Width(90).Image(rutaLogo);
                                   
                                }
                                else
                                {
                                    c.Item().Text("MERAKI").FontSize(28).Bold().FontColor(colorBordoMeraki).LetterSpacing(0.1f);
                                    c.Item().Text("BEBIDAS & DISTRIBUCIÓN").FontSize(10).Bold().FontColor(colorTextoGris).LetterSpacing(0.2f);
                                }
                            });

                            row.ConstantItem(160).AlignMiddle().Column(c =>
                            {
                                c.Item().AlignRight().Text("LISTA DE PRECIOS").FontSize(10).Bold().FontColor(colorBordoMeraki);
                                c.Item().AlignRight().Text($"Última modif.: {DateTime.Now:dd/MM/yyyy}").FontSize(9).FontColor(colorTextoGris);
                            });
                        });

                        col.Item().PaddingTop(2).PaddingBottom(8).Height(2).Background(colorBordoMeraki);
                    });

                    page.Content().PaddingTop(10).Column(colReporte =>
                    {
                        colReporte.Spacing(35);

                        foreach (var grupo in datosAgrupados)
                        {
                            string nombreCategoria = grupo.Key;
                            List<BEProducto> productosDeLaCategoria = grupo.Value;

                            colReporte.Item().Column(bloqueCategoria =>
                            {
                                bloqueCategoria.Spacing(5);

                                bloqueCategoria.Item().Text(nombreCategoria.ToUpper())
                                    .FontColor(colorBordoMeraki)
                                    .FontSize(16)
                                    .Bold()
                                    .LetterSpacing(0.05f);

                                bloqueCategoria.Item().Table(tabla =>
                                {
                                    tabla.ColumnsDefinition(columnas =>
                                    {
                                        columnas.RelativeColumn(3.5f);
                                        columnas.RelativeColumn(1.2f);
                                        columnas.RelativeColumn(1.6f);
                                        columnas.RelativeColumn(1.7f);
                                    });

                                    // ENCABEZADOS DE LA TABLA
                                    tabla.Header(header =>
                                    {
                                        // ⚠️ Acá el texto del header SIEMPRE es blanco para que contraste con el fondo bordó oscuro
                                        header.Cell().Background(colorBordoOscuro).Padding(8).Text("PRODUCTO").FontColor("#FFFFFF").Bold().FontSize(10);
                                        header.Cell().Background(colorBordoOscuro).Padding(8).AlignCenter().Text("UNIDADES").FontColor("#FFFFFF").Bold().FontSize(10);
                                        header.Cell().Background(colorBordoOscuro).Padding(8).AlignRight().Text("PRECIO TOTAL").FontColor("#FFFFFF").Bold().FontSize(10);
                                        header.Cell().Background(colorBordoOscuro).Padding(8).AlignRight().Text("P. UNITARIO").FontColor("#FFFFFF").Bold().FontSize(10);
                                    });

                                    bool filaAlterna = false;
                                    float sizeFuenteItems = 11f;

                                    foreach (var prod in productosDeLaCategoria)
                                    {
                                        // Intercalamos entre el fondo dinámico de la fila o el fondo dinámico de la página
                                        var fondoFila = filaAlterna ? colorFondoFila : colorFondoBase;

                                        string nombreCompleto = prod.ToString();
                                        string sufijoAQuitar = " x " + prod.Unidad;

                                        string productoYMedida = nombreCompleto.EndsWith(sufijoAQuitar)
                                            ? nombreCompleto.Substring(0, nombreCompleto.Length - sufijoAQuitar.Length)
                                            : nombreCompleto;

                                        int unidades = prod.Unidad;
                                        double precioTotal = (double)prod.PrecioMayorista;
                                        double precioUnitario = unidades > 0 ? (precioTotal / unidades) : precioTotal;

                                        // Celda 1
                                        tabla.Cell().Background(fondoFila).Padding(8).AlignLeft().Text(productoYMedida)
                                            .FontSize(sizeFuenteItems).Bold().FontColor(colorTextoPrincipal); // 👈 Forzamos el color dinámico

                                        // Celda 2
                                        tabla.Cell().Background(fondoFila).Padding(8).AlignCenter().Text(unidades.ToString())
                                            .FontSize(sizeFuenteItems)
                                            .FontColor(colorTextoGris) // 👈 Forzamos el gris dinámico
                                            .Bold();

                                        // Celda 3
                                        tabla.Cell().Background(fondoFila).Padding(8).AlignRight().Text($"$ {precioTotal:N2}")
                                            .FontSize(sizeFuenteItems)
                                            .FontColor(colorTextoPrincipal) // 👈 Forzamos el color dinámico
                                            .Bold();

                                        // Celda 4
                                        tabla.Cell().Background(fondoFila).Padding(8).AlignRight().Text($"$ {precioUnitario:N2}")
                                            .FontSize(sizeFuenteItems)
                                            .FontColor(colorBordoMeraki).Bold();

                                        filaAlterna = !filaAlterna;
                                    }
                                });
                            });
                        }
                    });

                    page.Footer().Column(col =>
                    {
                        col.Item().PaddingTop(15).Height(1).Background(colorFondoFila);
                        col.Item().PaddingTop(5).Row(row =>
                        {
                            row.RelativeItem().Text("Precios sujetos a modificaciones sin previo aviso.").FontSize(8).FontColor(colorTextoGris).Italic();
                            row.RelativeItem().AlignRight().Text(x =>
                            {
                                x.Span("Página ").FontSize(8).FontColor(colorTextoGris);
                                x.CurrentPageNumber().FontSize(8).Bold().FontColor(colorBordoMeraki);
                            });
                        });
                    });
                });
            }).GeneratePdf(rutaDestino);
        }


        
    }
}

