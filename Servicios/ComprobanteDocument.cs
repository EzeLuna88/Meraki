using BE;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.ComponentModel;
using System.Xml.Linq;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace Servicios // O "namespace Meraki.Servicios" si lo armaste como carpeta adentro de Meraki
{
    public class ComprobanteDocument : IDocument
    {
        private readonly BEComprobante _comprobante;

        // Constructor que recibe los datos de la compra
        public ComprobanteDocument(BEComprobante comprobante)
        {
            _comprobante = comprobante;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        // Configuración de la página
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(5, Unit.Millimetre);
                page.DefaultTextStyle(x => x.FontSize(11));
                page.Content().Element(ComposeContent);
            });
        }

        // Diseño de todo el contenido (Cabecera, Cliente, Totales)
        void ComposeContent(IContainer container)
        {
            container.Column(col =>
            {
                // 1. Encabezado
                col.Item().Row(row =>
                {
                    row.RelativeItem().Text("MERAKI").FontSize(22).Bold();
                    row.ConstantItem(150).AlignRight().Column(c =>
                    {
                        c.Item().Text($"Comprobante N° {_comprobante.Numero}").FontSize(14).Bold();
                        c.Item().Text(_comprobante.Fecha.ToString("dd/MM/yyyy")).FontSize(12);
                    });
                });

                col.Item().PaddingTop(8).LineHorizontal(1);

                // 2. Datos del cliente y Logística (Horario, Tel, Comentarios)
                col.Item().PaddingTop(10).Row(row =>
                {
                    // Columna 1: Cliente (Le damos un ancho fijo para controlar la distancia)
                    row.ConstantItem(180).Column(c =>
                    {
                        c.Item().Text(_comprobante.Cliente.Nombre).FontSize(12).Bold();
                        c.Item().Text(_comprobante.Cliente.Direccion).FontSize(11);
                    });

                    // Columna 2: Horario y Teléfono (Pegado al cliente, con ancho fijo)
                    row.ConstantItem(120).Column(c =>
                    {
                        c.Item().Text($"{_comprobante.Cliente.HorarioDeApertura.ToString(@"hh\:mm")} – {_comprobante.Cliente.HorarioDeCierre.ToString(@"hh\:mm")}");
                        c.Item().Text(_comprobante.Cliente.Telefono);
                    });

                    // Columna 3: Comentarios (Ocupa todo el espacio restante a la derecha)
                    row.RelativeItem().Column(c =>
                    {
                        if (!string.IsNullOrWhiteSpace(_comprobante.Cliente.Comentarios))
                        {
                            c.Item()
                                .Border(0.5f)
                                .BorderColor(Colors.Grey.Medium)
                                .Background(Colors.Grey.Lighten4)
                                .Padding(5)
                                .Text(_comprobante.Cliente.Comentarios).FontSize(10).Italic();
                        }
                    });
                });

                // 3. Tabla de productos
                col.Item().PaddingTop(12).Element(ComposeTable);

                // 4. Totales y Bultos (Sin forma de pago)
                int totalBultos = 0;
                foreach (var item in _comprobante.ListaItems) { totalBultos += item.Cantidad; }

                col.Item().PaddingTop(8).AlignRight().Column(c =>
                {
                    c.Item().Text($"Total de bultos: {totalBultos}").FontSize(12).Bold();
                    c.Item().Text($"Total: {_comprobante.Total:c2}").FontSize(16).Bold();
                });
            });
        }

        // Diseño exclusivo de la tabla
        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(cols =>
                {
                    cols.ConstantColumn(55);  // Código
                    cols.RelativeColumn();     // Nombre + Renglón
                    cols.ConstantColumn(75);  // Precio unit
                    cols.ConstantColumn(45);  // Cantidad
                    cols.ConstantColumn(75);  // Total
                });

                // Encabezados
                table.Header(h =>
                {
                    h.Cell().Text("Código").FontSize(10).FontColor(Colors.Grey.Darken2);
                    h.Cell().Text("Producto").FontSize(10).FontColor(Colors.Grey.Darken2);
                    h.Cell().AlignRight().Text("P. Unit.").FontSize(10).FontColor(Colors.Grey.Darken2);
                    h.Cell().AlignRight().Text("Cant.").FontSize(10).FontColor(Colors.Grey.Darken2);
                    h.Cell().AlignRight().Text("Total").FontSize(10).FontColor(Colors.Grey.Darken2);
                    h.Cell().ColumnSpan(5).PaddingTop(2).LineHorizontal(1);
                });

                // Filas de productos
                foreach (var item in _comprobante.ListaItems)
                {
                    decimal precioUnit = item.Precio / item.Cantidad;

                    table.Cell().PaddingVertical(2).Text(item.Codigo);

                    // Celda del nombre con conector punteado
                    table.Cell().PaddingVertical(2).Row(row =>
                    {
                        row.AutoItem().Text(item.Nombre);
                        row.RelativeItem().PaddingBottom(2).AlignBottom().PaddingHorizontal(5).LineHorizontal(0.5f);
                    });

                    table.Cell().PaddingVertical(2).AlignRight().Text(precioUnit.ToString("c2"));
                    table.Cell().PaddingVertical(2).AlignRight().Text(item.Cantidad.ToString());
                    table.Cell().PaddingVertical(2).AlignRight().Text(item.Precio.ToString("c2"));
                }
            });
        }

        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Guardar(string rutaArchivo)
        {
            try
            {
                // El error salta acá mismo
                QuestPDF.Settings.License = LicenseType.Community;
                QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

                var doc = Document.Create(Compose);
                doc.GeneratePdf(rutaArchivo);
            }
            catch (Exception ex)
            {
                // Esto va a abrir una ventana con el error "desnudo"
                // Mostramos ex.ToString() porque incluye la "Inner Exception" que es la clave
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "ERROR PROFUNDO DE PDF");
                throw; // Re-lanzamos para que el programa sepa que falló
            }
        }
    }
}