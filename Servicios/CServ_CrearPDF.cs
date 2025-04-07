using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.Xml.Linq;
using Modelo;

namespace Servicios
{
    public static class CServ_CrearPDF
    {

        #region Properties PDF
        public static System.Drawing.Image ImgOapce { get; set; }
        public static System.Drawing.Image ImgSedex { get; set; }
        public static System.Drawing.Image ImgPanelDatos { get; set; }
        public static string RutaArchivo { get; set; }
        public static string PDFhtml { get; set; }
        public static string carpetaEspecifica { get; set; }
        public static string nombreArchivo { get; set; }
        public static string carpetaDocumentos { get; set; }
        public static string carpetaBase { get; set; }


        #endregion

        #region PropertiesKPI
        public static int ID_Pedido { get; set; }
        public static int OC { get; set; }
        #endregion

        #region PropertiesLiquidacion
        public static int ID_Venta { get; set; }
        public static string NombreCliente { get; set; }
        public static string CategoriaCliente { get; set; }
        public static double Descuento { get; set; }
        public static string UsuarioVendedor { get; set; }
        public static double SubtotalAcumulado { get; set; }
        public static double DescuentoAcumulado { get; set; }
        public static DateTime Fecha { get; set; }
        // public static List<CM_DatosVenta> ItemsVendidos = new List<CM_DatosVenta>();
        #endregion

        public static bool AbrirPDFExistente(int valor)
        {
            bool resultado = false;
            try
            {
                SaveFileDialog guardar = new SaveFileDialog();
                string carpetaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string carpetaBase = Path.Combine(carpetaDocumentos, "KeyFacturation");


                if (!Directory.Exists(carpetaBase))
                {
                    Directory.CreateDirectory(carpetaBase);
                }
                switch (valor)
                {
                    case 1:
                        carpetaEspecifica = Path.Combine(carpetaBase, "KPI"); 
                        nombreArchivo = ("KPI " + CM_DatosKPI_PDF.Interno+ ".pdf");
                        if (File.Exists(nombreArchivo))
                        {
                            Process.Start(new ProcessStartInfo(carpetaEspecifica + "\\" + nombreArchivo) { UseShellExecute = true });
                            resultado = true;
                        }
                        else { resultado =false ; }
                        break;
                    case 2:
                        resultado = false;
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
           
        }
        public static void GenerarPDF(int valor)
        {
            try
            {
                SaveFileDialog guardar = new SaveFileDialog();
                carpetaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                carpetaBase = Path.Combine(carpetaDocumentos, "KeyFacturation");


                if (!Directory.Exists(carpetaBase))
                {
                    Directory.CreateDirectory(carpetaBase);
                }


                switch (valor)
                {
                    case 1:
                        carpetaEspecifica = Path.Combine(carpetaBase, "KPI"); ;
                        nombreArchivo = ("KPI " + CM_DatosKPI_PDF.Interno + ".pdf");
                        RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Plantilla.html");
                        break;
                    case 2:
                        carpetaEspecifica = Path.Combine(carpetaBase, "PreFacturación");
                        nombreArchivo = "Liquidación N° " + OC.ToString() + ".pdf";
                        RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "PlantillaPreliquidar.html");
                        break;
                    case 3:
                        carpetaEspecifica = Path.Combine(carpetaBase, "KPI");
                        nombreArchivo = "Venta N° " + ID_Venta.ToString() + ".pdf";
                        break;
                    case 4:
                        carpetaEspecifica = Path.Combine(carpetaBase, "Pedidos de Compra");
                        nombreArchivo = "PC N° " + OC.ToString() + ".pdf";
                        RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Plantilla.html");
                        break;
                }
                if (!Directory.Exists(carpetaEspecifica))
                {
                    Directory.CreateDirectory(carpetaEspecifica);
                }
                guardar.InitialDirectory = carpetaEspecifica;
                guardar.FileName = nombreArchivo;


                PDFhtml = "";
                PDFhtml = File.ReadAllText(RutaArchivo.ToString());
                PDFhtml = CargarDatosPDF(PDFhtml, valor);

                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4, 55, 75, 25, 25);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        using (StringReader sr = new StringReader(PDFhtml))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        }
                        pdfDoc.Close();
                        stream.Close();
                    }
                    Process.Start(guardar.FileName);
                }
            }
            catch (Exception)
            {

                throw new Exception("La operación se ha realizado con éxito pero no se ha podido generar el PDF. Por favor, contáctes con el proveedor del sistema");
            }
        }
        private static string CargarDatosPDF(string PDFhtml, int valor)
        {


            System.Drawing.Image resizedOapce = ResizeImage(ImgOapce, 100, 50); // le paso las medidas necesarias
            System.Drawing.Image resizedSedex = ResizeImage(ImgSedex, 50, 50);


            // Guardar las imágenes redimensionadas en archivos temporales
            string oapceImagePath = Path.Combine(Path.GetTempPath(), "oapceImage.png");
            string sedexImagePath = Path.Combine(Path.GetTempPath(), "sedexImage.png");

            resizedOapce.Save(oapceImagePath, System.Drawing.Imaging.ImageFormat.Png);
            resizedSedex.Save(sedexImagePath, System.Drawing.Imaging.ImageFormat.Png);

            // Reemplazar @OC y @NUMERO con etiquetas <img> que usan rutas de archivo
            PDFhtml = PDFhtml.Replace("@LogoOapce", $"<img src=\"{oapceImagePath}\" style=\"max-width: 100px; max-height: 50px;\"/>");
            PDFhtml = PDFhtml.Replace("@LogoSedex", $"<img src=\"{sedexImagePath}\" style=\"max-width: 100px; max-height: 50px;\"/>");



            if (ImgPanelDatos != null)
            {


                // Opcional: redimensionar si querés que no sea tan grande en el PDF
                System.Drawing.Image resizedPanelDatos = ResizeImage(ImgPanelDatos, 395, 95); // ajustá según tu layout

                string panelDatosImagePath = Path.Combine(Path.GetTempPath(), "panelDatosImage.png");
                resizedPanelDatos.Save(panelDatosImagePath, System.Drawing.Imaging.ImageFormat.Png);
                PDFhtml = PDFhtml.Replace("@Foto", $"<img src=\"{panelDatosImagePath}\"/>");

            }
            else
            {
                PDFhtml = PDFhtml.Replace("@Foto", "<i>No hay imagen de indicadores disponible</i>");
            }

            PDFhtml = PDFhtml.Replace("@ID_KPI", CM_DatosKPI_PDF.ID_KPI.ToString());
            PDFhtml = PDFhtml.Replace("@HoyFecha", CM_DatosKPI_PDF.Fe_KPI.ToString("d"));
            PDFhtml = PDFhtml.Replace("@Cliente", CM_DatosKPI_PDF.Cliente);
            PDFhtml = PDFhtml.Replace("@Via", CM_DatosKPI_PDF.Via);
            PDFhtml = PDFhtml.Replace("@Canal", CM_DatosKPI_PDF.Canal);
            PDFhtml = PDFhtml.Replace("@Despacho", CM_DatosKPI_PDF.N_Despacho);
            PDFhtml = PDFhtml.Replace("@IngresoMercaderia", CM_DatosKPI_PDF.Fe_Arribo.ToString("d"));

         
            if (CM_DatosKPI_PDF.Fe_CierreIngreso.HasValue)
            {
                PDFhtml = PDFhtml.Replace("@CierreIngreso", CM_DatosKPI_PDF.Fe_CierreIngreso.Value.ToString("d"));
            }
            else
            {
                PDFhtml = PDFhtml.Replace("@CierreIngreso", "-");
            }

            if (CM_DatosKPI_PDF.Fe_FondosAduana.HasValue)
            {
                PDFhtml = PDFhtml.Replace("@FondosAduana", CM_DatosKPI_PDF.Fe_FondosAduana.Value.ToString("d"));
            }
            else
            {
                PDFhtml = PDFhtml.Replace("@FondosAduana", "-");
            }

            PDFhtml = PDFhtml.Replace("@DocOriginal", CM_DatosKPI_PDF.Fe_DocOriginal.ToString("d"));
            PDFhtml = PDFhtml.Replace("@Oficialización", CM_DatosKPI_PDF.Fe_Oficializacion.ToString("d"));
            PDFhtml = PDFhtml.Replace("@Retirocarga", CM_DatosKPI_PDF.Fe_RetiroCarga.ToString("d"));
            PDFhtml = PDFhtml.Replace("@TotalDiasHabiles", CM_DatosKPI_PDF.TotalDiasHabiles.ToString("d"));

            PDFhtml = PDFhtml.Replace("@Resultado", CM_DatosKPI_PDF.Resultado);

            string colorResultado = "#000"; // Valor por defecto (negro)
            string valorResultado = CM_DatosKPI_PDF.Resultado.ToLower();

            if (valorResultado.Contains("excelente"))
                colorResultado = "#007BFF"; // Celeste
            else if (valorResultado.Contains("requerido"))
                //colorResultado = "#28a745"; // Verde
            colorResultado = "#1A6F2E"; // Verde
            
            else if (valorResultado.Contains("no satisfactorio"))
             //   colorResultado = "#dc3545"; // Rojo
            colorResultado = "#C90000"; // Rojo

            PDFhtml = PDFhtml.Replace("@color", colorResultado);



            PDFhtml = PDFhtml.Replace("@TipoDesvio", CM_DatosKPI_PDF.TipoDesvio);
            PDFhtml = PDFhtml.Replace("@Motivo", CM_DatosKPI_PDF.MotivoDesvio);          

            /*
            switch (valor)
            {
                case 1:
                    PDFhtml = PDFhtml.Replace("@OC", "Orden de Compra");
                    PDFhtml = PDFhtml.Replace("@NUMERO", OC.ToString());

                    PDFhtml = PDFhtml.Replace("@Proveedor", CM_DatosOCDefinitiva.NombreEmpresa);
                    PDFhtml = PDFhtml.Replace("@PC", ID_Pedido.ToString());
                    PDFhtml = PDFhtml.Replace("@Fecha", CM_DatosOCDefinitiva.Fecha.ToString("d"));
                    PDFhtml = PDFhtml.Replace("@MatriculaProveedor", CM_DatosOCDefinitiva.MatriculaProveedor.ToString());
                    PDFhtml = PDFhtml.Replace("@CUITProveedor", CM_DatosOCDefinitiva.CUITProveedor);
                    PDFhtml = PDFhtml.Replace("@DireccionProv", CM_DatosOCDefinitiva.DireccionProv);
                    PDFhtml = PDFhtml.Replace("@CorreoProv", CM_DatosOCDefinitiva.CorreoProv);
                    PDFhtml = PDFhtml.Replace("@LocalidadProv", CM_DatosOCDefinitiva.LocalidadProv);
                    PDFhtml = PDFhtml.Replace("@PartidoProv", CM_DatosOCDefinitiva.PartidoProv);
                    PDFhtml = PDFhtml.Replace("@TelefonoProv", CM_DatosOCDefinitiva.TelefonoProv.ToString());

                    PDFhtml = PDFhtml.Replace("@NombreEmpresa", CM_DatosOCDefinitiva.NombreEmpresa.ToString());
                    PDFhtml = PDFhtml.Replace("@DireccionFarma", CM_DatosOCDefinitiva.DireccionFarma.ToString());
                    PDFhtml = PDFhtml.Replace("@CUITEmpresa", CM_DatosOCDefinitiva.CUITEmpresa.ToString());
                    PDFhtml = PDFhtml.Replace("@DireccionProv", CM_DatosOCDefinitiva.DireccionFarma.ToString());
                    PDFhtml = PDFhtml.Replace("@DomicilioEntrega", CM_DatosOCDefinitiva.DomicilioEntrega.ToString());
                    PDFhtml = PDFhtml.Replace("@Fe", CM_DatosOCDefinitiva.FechaInicioAct.ToString("d"));
                    PDFhtml = PDFhtml.Replace("@PartidoFarma", CM_DatosOCDefinitiva.PartidoFarma.ToString());
                    PDFhtml = PDFhtml.Replace("@LocalidadFarma", CM_DatosOCDefinitiva.LocalidadFarma.ToString());

                    PDFhtml = PDFhtml.Replace("@Total", "Total de la orden de compra");
                    foreach (var producto in ListadeItems)
                    {
                        FilaProductos += "<tr>";
                        FilaProductos += "<td>" + producto.NombreComercial + "</td>";
                        FilaProductos += "<td>" + producto.Monodroga + "</td>";
                        FilaProductos += "<td>" + producto.Marca + "</td>";
                        FilaProductos += "<td>" + producto.Cantidad.ToString() + "</td>";
                        FilaProductos += "<td>" + producto.PrecioUnitario.ToString("#,##0.00") + "</td>";
                        FilaProductos += "<td>" + producto.Subtotal.ToString("#,##0.00") + "</td>";
                        FilaProductos += "</tr>";
                        TotalOC += producto.Subtotal;

                    }

                    PDFhtml = PDFhtml.Replace("@Items", FilaProductos);
                    PDFhtml = PDFhtml.Replace("@TotOC", TotalOC.ToString("#,##0.00"));

                    PDFhtml = PDFhtml.Replace("@Usuario", CM_DatosOCDefinitiva.NombreApellido.ToString());
                    PDFhtml = PDFhtml.Replace("@AutoFecha", Fecha.ToString());
                    break;
                case 2:
                    PDFhtml = PDFhtml.Replace("@OC", "Remito de recepción");
                    PDFhtml = PDFhtml.Replace("@NUMERO", OC.ToString());

                    PDFhtml = PDFhtml.Replace("@Proveedor", CM_DatosOCDefinitiva.NombreEmpresa);
                    PDFhtml = PDFhtml.Replace("@PC", ID_Pedido.ToString());
                    PDFhtml = PDFhtml.Replace("@Fecha", CM_DatosOCDefinitiva.Fecha.ToString("d"));
                    PDFhtml = PDFhtml.Replace("@MatriculaProveedor", CM_DatosOCDefinitiva.MatriculaProveedor.ToString());
                    PDFhtml = PDFhtml.Replace("@CUITProveedor", CM_DatosOCDefinitiva.CUITProveedor);
                    PDFhtml = PDFhtml.Replace("@DireccionProv", CM_DatosOCDefinitiva.DireccionProv);
                    PDFhtml = PDFhtml.Replace("@CorreoProv", CM_DatosOCDefinitiva.CorreoProv);
                    PDFhtml = PDFhtml.Replace("@LocalidadProv", CM_DatosOCDefinitiva.LocalidadProv);
                    PDFhtml = PDFhtml.Replace("@PartidoProv", CM_DatosOCDefinitiva.PartidoProv);
                    PDFhtml = PDFhtml.Replace("@TelefonoProv", CM_DatosOCDefinitiva.TelefonoProv.ToString());

                    PDFhtml = PDFhtml.Replace("@NombreEmpresa", CM_DatosOCDefinitiva.NombreEmpresa.ToString());
                    PDFhtml = PDFhtml.Replace("@DireccionFarma", CM_DatosOCDefinitiva.DireccionFarma.ToString());
                    PDFhtml = PDFhtml.Replace("@CUITEmpresa", CM_DatosOCDefinitiva.CUITEmpresa.ToString());
                    PDFhtml = PDFhtml.Replace("@DireccionProv", CM_DatosOCDefinitiva.DireccionFarma.ToString());
                    PDFhtml = PDFhtml.Replace("@DomicilioEntrega", CM_DatosOCDefinitiva.DomicilioEntrega.ToString());
                    PDFhtml = PDFhtml.Replace("@Fe", CM_DatosOCDefinitiva.FechaInicioAct.ToString("d"));
                    PDFhtml = PDFhtml.Replace("@PartidoFarma", CM_DatosOCDefinitiva.PartidoFarma.ToString());
                    PDFhtml = PDFhtml.Replace("@LocalidadFarma", CM_DatosOCDefinitiva.LocalidadFarma.ToString());


                    PDFhtml = PDFhtml.Replace("@Total", "Precio sugerido para cada unidad");

                    foreach (var producto in ListadeItems)
                    {
                        FilaProductos += "<tr>";
                        FilaProductos += "<td>" + producto.NombreComercial + "</td>";
                        FilaProductos += "<td>" + producto.Monodroga + "</td>";
                        FilaProductos += "<td>" + producto.Marca + "</td>";
                        FilaProductos += "<td>" + producto.Cantidad.ToString() + "</td>";

                        double preciosugerido = consultaRepetido(producto.NombreComercial, producto.PrecioUnitario);

                        FilaProductos += "<td>" + producto.PrecioUnitario.ToString("#,##0.00") + "</td>";
                          double PrecioConProffit = 0;
                          PrecioConProffit = producto.PrecioUnitario * 1.30;
                        FilaProductos += "<td>" + preciosugerido.ToString("#,##0.00") + "</td>";
                        FilaProductos += "</tr>";
                        TotalOC += producto.Subtotal;

                    }

                    PDFhtml = PDFhtml.Replace("@Items", FilaProductos);
                    PDFhtml = PDFhtml.Replace("@TotOC", TotalOC.ToString("#,##0.00"));

                    PDFhtml = PDFhtml.Replace("@Usuario", CM_DatosOCDefinitiva.NombreApellido.ToString());
                    PDFhtml = PDFhtml.Replace("@AutoFecha", CM_DatosOCDefinitiva.Fecha.ToString());
                    CM_DatosOCDefinitiva.LimpiarDatos(true);
                    ID_Pedido = 0;
                    break;
            }*/
            return PDFhtml;

        }
        private static System.Drawing.Image ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = System.Drawing.Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static void CrearImagenIndicadores(TableLayoutPanel TLP_Indicadores) 
        {
            carpetaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            carpetaBase = Path.Combine(carpetaDocumentos, "KeyFacturation");


            if (!Directory.Exists(carpetaBase))
            {
                Directory.CreateDirectory(carpetaBase);
            }
            string carpetaImagenes = Path.Combine(carpetaBase, "Imagenes");
            if (!Directory.Exists(carpetaImagenes))
                Directory.CreateDirectory(carpetaImagenes);

            string rutaImagen = Path.Combine(carpetaImagenes, "PanelDatos.png");

            // Crear imagen del TableLayoutPanel
            Bitmap bmp = new Bitmap(TLP_Indicadores.Width, TLP_Indicadores.Height);
            TLP_Indicadores.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));

            // Guardar la imagen
            bmp.Save(rutaImagen, System.Drawing.Imaging.ImageFormat.Png);

            // También la cargamos en la propiedad para usarla en el PDF
            ImgPanelDatos = (System.Drawing.Image)bmp.Clone();
        }

    }
}
