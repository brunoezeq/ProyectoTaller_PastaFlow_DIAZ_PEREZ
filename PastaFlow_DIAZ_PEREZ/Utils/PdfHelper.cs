using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Utils
{
    static class PdfHelper
    {
        public static void GenerarTicketReserva(
           string nombreLocal,
           string logoPath,
           string nombreCliente,
           string apellidoCliente,
           DateTime fechaHoraReserva,
           int cantidadPersonas,
           string estado,
           string cajero,
           string rutaSalida)
        {
            Document doc = new Document(PageSize.A6, 20, 20, 20, 20); // Tamaño pequeño tipo ticket
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // 🔹 Logo del local
                if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(60, 60);
                    logo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(logo);
                }

                // 🔹 Nombre del local
                Paragraph titulo = new Paragraph(nombreLocal.ToUpper(), new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                doc.Add(new Paragraph("\n----------------------------------------\n", new Font(Font.FontFamily.HELVETICA, 10)));
                doc.Add(new Paragraph("TICKET DE RESERVA", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                doc.Add(new Paragraph("----------------------------------------\n"));

                // 🔹 Datos de la reserva
                doc.Add(new Paragraph($"Cliente: {nombreCliente} {apellidoCliente}", new Font(Font.FontFamily.HELVETICA, 10)));
                doc.Add(new Paragraph($"Fecha: {fechaHoraReserva:dd/MM/yyyy}", new Font(Font.FontFamily.HELVETICA, 10)));
                doc.Add(new Paragraph($"Hora: {fechaHoraReserva:HH:mm}", new Font(Font.FontFamily.HELVETICA, 10)));
                doc.Add(new Paragraph($"Cantidad de Personas: {cantidadPersonas}", new Font(Font.FontFamily.HELVETICA, 10)));
                doc.Add(new Paragraph($"Estado: {estado}", new Font(Font.FontFamily.HELVETICA, 10)));
                doc.Add(new Paragraph($"Atendido por: {cajero}", new Font(Font.FontFamily.HELVETICA, 10)));

                doc.Add(new Paragraph("\n----------------------------------------", new Font(Font.FontFamily.HELVETICA, 10)));
                doc.Add(new Paragraph("¡Gracias por su reserva!", new Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC)));
                doc.Add(new Paragraph("----------------------------------------", new Font(Font.FontFamily.HELVETICA, 10)));

                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el ticket: " + ex.Message);
            }
            finally
            {
                if (doc.IsOpen())
                    doc.Close();
            }
        }
            public static void GenerarFacturaVenta(
                string nombreLocal,
                string logoPath,
                string numeroFactura,
                DateTime fecha,
                string cajero,
                DataTable productos,
                decimal totalVenta,
                string rutaSalida)
            {
                    Document doc = new Document(PageSize.A4, 40, 40, 40, 40);

                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                        doc.Open();

                        // 🔹 Logo
                        if (File.Exists(logoPath))
                        {
                            Image logo = Image.GetInstance(logoPath);
                            logo.ScaleAbsolute(80, 80);
                            logo.Alignment = Element.ALIGN_CENTER;
                            doc.Add(logo);
                        }

                        // 🔹 Encabezado
                        Paragraph titulo = new Paragraph(nombreLocal, new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);

                        doc.Add(new Paragraph($"Factura N°: {numeroFactura}"));
                        doc.Add(new Paragraph($"Fecha: {fecha:dd/MM/yyyy HH:mm}"));
                        doc.Add(new Paragraph($"Cajero: {cajero}"));
                        doc.Add(new Paragraph("\n"));

                        // 🔹 Tabla de productos
                        PdfPTable tabla = new PdfPTable(3);
                        tabla.WidthPercentage = 100;
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

                        doc.Add(new Paragraph("\nGracias por su compra!", new Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC)));

                        doc.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al generar la factura: " + ex.Message);
                    }
            }
    }
}

