using Servicios_Jue.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios_Jue.Clases
{
    public class clsCliente
    {
        private DBExamenEntities dbExamen = new DBExamenEntities();
        public Cliente cliente { get; set; }
        public string Insertar()
        {
            try
            {
                dbExamen.Clientes.Add(cliente);
                dbExamen.SaveChanges();
                return "Cliente insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el cliente: " + ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {

                Cliente clie = Consultar(cliente.Documento);
                if (clie == null)
                {
                    return "El cliente con el documento ingresado no existe, por lo tanto no se puede actualizar";
                }
                dbExamen.Clientes.AddOrUpdate(cliente);
                dbExamen.SaveChanges();
                return "Se actualizó el cliente correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el cliente: " + ex.Message;
            }
        }
        public List<Cliente> ConsultarTodos()
        {
            return dbExamen.Clientes
                .OrderBy(c => c.Nombre)
                .ToList();
        }
        public Cliente Consultar(string Documento)
        {

            return dbExamen.Clientes.FirstOrDefault(c => c.Documento == Documento);
        }
        public string Eliminar()
        {
            try
            {

                Cliente clie = Consultar(cliente.Documento);
                if (clie == null)
                {
                    return "El cliente con el documento ingresado no existe, por lo tanto no se puede eliminar";
                }

                dbExamen.Clientes.Remove(clie);
                dbExamen.SaveChanges();
                return "Se eliminó el cliente correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el cliente: " + ex.Message;
            }
        }
        public string Eliminar(string Documento)
        {
            try
            {

                Cliente clie = Consultar(cliente.Documento);
                if (clie == null)
                {
                    return "El cliente con el documento ingresado no existe, por lo tanto no se puede eliminar";
                }

                dbExamen.Clientes.Remove(clie);
                dbExamen.SaveChanges();
                return "Se eliminó el cliente correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el cliente: " + ex.Message;
            }
        }

    }
}