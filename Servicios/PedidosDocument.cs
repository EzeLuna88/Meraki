using BE;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IContainer = QuestPDF.Infrastructure.IContainer;


namespace Servicios
{
    public class PedidosDocument : IDocument
    {
        private readonly BEPedido _pedido;

        public PedidosDocument(BEPedido pedido)
        {
            _pedido = pedido;
        }

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
                        // Dividimos el título y el número en dos renglones separados
                        c.Item().Text("Pedido N°").FontSize(14).Bold();
                        c.Item().Text(_pedido.Numero).FontSize(14).Bold();

                        // La fecha queda en el tercer renglón
                        c.Item().Text(_pedido.Fecha.ToString("dd/MM/yyyy")).FontSize(12);
                    });
                });

                col.Item().PaddingTop(8).LineHorizontal(1);

                // 2. Datos del cliente y Logística (Horario, Tel, Comentarios)
                col.Item().PaddingTop(10).Row(row =>
                {
                    // Columna 1: Cliente (Le damos un ancho fijo para controlar la distancia)
                    row.ConstantItem(180).Column(c =>
                    {
                        c.Item().Text(_pedido.Cliente.Nombre).FontSize(12).Bold();
                        c.Item().Text(_pedido.Cliente.Direccion).FontSize(11);
                        c.Item().Text(_pedido.Cliente.Localidad).FontSize(10).Italic();
                    });

                    // Columna 2: Horario y Teléfono (Pegado al cliente, con ancho fijo)
                    row.ConstantItem(120).Column(c =>
                    {
                        c.Item().Text($"{_pedido.Cliente.HorarioDeApertura.ToString(@"hh\:mm")} – {_pedido.Cliente.HorarioDeCierre.ToString(@"hh\:mm")}");
                        c.Item().Text(_pedido.Cliente.Telefono);
                    });

                    // Columna 3: Comentarios (Ocupa todo el espacio restante a la derecha)
                    row.RelativeItem().Column(c =>
                    {
                        if (!string.IsNullOrWhiteSpace(_pedido.Cliente.Comentarios))
                        {
                            c.Item()
                                .Border(0.5f)
                                .BorderColor(Colors.Grey.Medium)
                                .Background(Colors.Grey.Lighten4)
                                .Padding(5)
                                .Text(_pedido.Cliente.Comentarios).FontSize(10).Italic();
                        }
                    });
                });

                // 3. Tabla de productos
                col.Item().PaddingTop(12).Element(ComposeTable);

                // 4. Totales, Bultos y Fecha de Envío
                int totalBultos = 0;
                foreach (var item in _pedido.ListaItems) { totalBultos += item.Cantidad; }

                col.Item().PaddingTop(8).Row(row =>
                {
                    // IZQUIERDA (Tu zona amarilla): Fecha de envío
                    // RelativeItem toma todo el espacio sobrante y empuja los totales a la derecha
                    row.RelativeItem().AlignBottom().Text(text =>
                    {
                        text.Span("Fecha de envío: ").FontSize(14);
                        text.Span($"{_pedido.FechaEnvio:dd/MM/yyyy}").FontSize(14).Bold();
                    });

                    // DERECHA: Los bultos y el total (Se mantiene exactamente igual)
                    row.AutoItem().AlignRight().Column(c =>
                    {
                        c.Item().Text($"Total de bultos: {totalBultos}").FontSize(12).Bold();
                        c.Item().Text($"Total: {_pedido.Total:c2}").FontSize(16).Bold();
                    });
                });
            });
        }

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
                foreach (var item in _pedido.ListaItems)
                {
                    decimal precioUnit = item.PrecioUnitario;
                    decimal precioTotal = item.PrecioUnitario * item.Cantidad;

                    table.Cell().PaddingVertical(2).Text(item.Codigo);

                    // Celda del nombre con conector punteado
                    table.Cell().PaddingVertical(2).Row(row =>
                    {
                        row.AutoItem().Text(item.Nombre);
                        row.RelativeItem().PaddingBottom(2).AlignBottom().PaddingHorizontal(5).LineHorizontal(0.5f);
                    });

                    table.Cell().PaddingVertical(2).AlignRight().Text(precioUnit.ToString("c2"));
                    table.Cell().PaddingVertical(2).AlignRight().Text(item.Cantidad.ToString());
                    table.Cell().PaddingVertical(2).AlignRight().Text(precioTotal.ToString());
                }
            });
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public DocumentSettings GetSettings() => DocumentSettings.Default;
    }
}
