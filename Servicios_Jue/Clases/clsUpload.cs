using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Management;

namespace Servicios_Jue.Clases
{
    public class clsUpload
    {
        public string Datos { get; set; }
        public string Proceso { get; set; }
        public HttpRequestMessage request { get; set; }
        private List<string> Archivos;
        public async Task<HttpResponseMessage> GrabarArchivo(bool Actualizar)
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
            }
            string root = HttpContext.Current.Server.MapPath("~/Archivos");
            var provider = new MultipartFormDataStreamProvider(root);
            bool Existe = false;
            try
            {
                //Lee el contenido de los archivos
                await request.Content.ReadAsMultipartAsync(provider);
                if (provider.FileData.Count > 0)
                {
                    Archivos = new List<string>();
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        string fileName = file.Headers.ContentDisposition.FileName;
                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }
                        if (File.Exists(Path.Combine(root, fileName)))
                        {
                            if (Actualizar)
                            {
                                File.Delete(Path.Combine(root, fileName));
                                File.Move(file.LocalFileName, Path.Combine(root, fileName));
                                Existe = true;
                            }
                            else
                            {
                                File.Delete(Path.Combine(root, file.LocalFileName));
                                Existe = true;
                            }
                        }
                        else
                        {
                            Archivos.Add(fileName);
                            File.Move(file.LocalFileName, Path.Combine(root, fileName));
                        }
                    }
                    if (!Existe)
                    {
                        string RptaBD = ProcesarBD();
                        return request.CreateResponse(System.Net.HttpStatusCode.OK, "Se cargaron los archivos en el servidor, " + RptaBD);
                    }
                    else
                    {
                        return request.CreateErrorResponse(System.Net.HttpStatusCode.Conflict, "Ya existen los archivos en el servidor");
                    }
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage LeerArchivo(string archivo)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string Archivo = Path.Combine(Ruta, archivo);
                if (File.Exists(Archivo))
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var stream = new FileStream(Archivo, FileMode.Open, FileAccess.Read);
                    response.Content = new StreamContent(stream);
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = archivo;
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    return response;
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "El archivo no se encuentra en el servidor");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se pudo procesar el archivo. " + ex.Message);
            }
        }
        private string ProcesarBD()
        {
            switch (Proceso.ToUpper())
            {
                case "FotoPrenda":
                    clsPrenda prenda = new clsPrenda();
                    return prenda.GrabarImagenPrenda(Convert.ToInt32(Datos), Archivos);
                default:
                    return "No se ha definido el proceso en la base de datos";
            }
        }
    }
}