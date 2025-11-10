using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;

namespace PastaFlow_DIAZ_PEREZ.Utils
{
    static class PdfHelper
    {
        public static void GenerarFacturaVenta(
            string nombreLocal,
            string logoPath,
            string numeroFactura,
            DateTime fecha,
            string cajero,
            DataTable productos,
            decimal totalVenta,
            string rutaSalida,
            string imagenExtraPath = null) // permite imagen adicional (p.ej. platopastas.png o un sello)
        {
            Document doc = new Document(PageSize.A4, 40, 40, 40, 40);

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // Logo superior (centrado)
                if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(80, 80);
                    logo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(logo);
                }

                // Encabezado
                Paragraph titulo = new Paragraph(nombreLocal, new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                doc.Add(new Paragraph($"Factura N°: {numeroFactura}"));
                doc.Add(new Paragraph($"Fecha: {fecha:dd/MM/yyyy HH:mm}"));
                doc.Add(new Paragraph($"Cajero: {cajero}"));
                doc.Add(new Paragraph("\n"));

                // Tabla de productos
                PdfPTable tabla = new PdfPTable(3) { WidthPercentage = 100 };
                tabla.AddCell("Producto");
                tabla.AddCell("Cantidad");
                tabla.AddCell("Subtotal");

                foreach (DataRow row in productos.Rows)
                {
                    tabla.AddCell(row["Producto"].ToString());
                    tabla.AddCell(row["Cantidad"].ToString());
                    tabla.AddCell($"${row["Subtotal"]:0.00}");
                }

                doc.Add(tabla);
                doc.Add(new Paragraph("\n-----------------------------------"));
                doc.Add(new Paragraph($"TOTAL: ${totalVenta:0.00}", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                doc.Add(new Paragraph("-----------------------------------"));

                // Imagen extra opcional (por ejemplo platopastas.png)
                if (!string.IsNullOrEmpty(imagenExtraPath) && File.Exists(imagenExtraPath))
                {
                    doc.Add(new Paragraph("\n"));
                    Image extra = Image.GetInstance(imagenExtraPath);
                    extra.ScaleToFit(140f, 140f);
                    extra.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(extra);
                }

                doc.Add(new Paragraph("\nGracias por su compra!", new Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC)));
                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar la factura: " + ex.Message);
            }
        }

        public static void GenerarTicketReserva(
        string nombreLocal,
        string logoPath,
        string numeroReserva,
        DateTime fechaReserva,
        string cliente,
        int cantidadPersonas,
        string estado,
        string cajero,
        string rutaSalida)
        {
            Document doc = new Document(PageSize.A6, 25, 25, 25, 25); // tamaño ticket chico

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // 🔹 Logo
                if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(60, 60);
                    logo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(logo);
                }

                // 🔹 Encabezado
                Paragraph titulo = new Paragraph(nombreLocal, new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph($"Ticket de Reserva Nº: {numeroReserva}", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD)));
                doc.Add(new Paragraph($"Fecha y hora: {fechaReserva:dd/MM/yyyy HH:mm}", new Font(Font.FontFamily.HELVETICA, 9)));
                doc.Add(new Paragraph($"Cliente: {cliente}", new Font(Font.FontFamily.HELVETICA, 9)));
                doc.Add(new Paragraph($"Cantidad de personas: {cantidadPersonas}", new Font(Font.FontFamily.HELVETICA, 9)));
                doc.Add(new Paragraph($"Estado: {estado}", new Font(Font.FontFamily.HELVETICA, 9)));
                doc.Add(new Paragraph($"Registrado por: {cajero}", new Font(Font.FontFamily.HELVETICA, 9)));
                doc.Add(new Paragraph(" "));

                // 🔹 Mensaje final
                Paragraph gracias = new Paragraph("¡Gracias por su reserva!", new Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC));
                gracias.Alignment = Element.ALIGN_CENTER;
                doc.Add(gracias);

                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el ticket de reserva: " + ex.Message);
            }
        }



    }
}

