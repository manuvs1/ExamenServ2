using Servicios_Jue.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios_Jue.Clases
{
    public class clsPrenda
    {
        private DBExamenEntities dbExamen = new DBExamenEntities();
        public Prenda prenda { get; set; }

        public string Insertar()
        {
            try
            {
                dbExamen.Prendas.Add(prenda);
                dbExamen.SaveChanges();
                return "Prenda insertada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar la prenda: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                Prenda pre = Consultar(prenda.IdPrenda);
                if (pre == null)
                {
                    return "La prenda con el ID ingresado no existe, por lo tanto no se puede actualizar";
                }
                dbExamen.Prendas.AddOrUpdate(prenda);
                dbExamen.SaveChanges();
                return "Se actualizó la prenda correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar la prenda: " + ex.Message;
            }
        }

        public List<Prenda> ConsultarTodos()
        {
            return dbExamen.Prendas.OrderBy(p => p.TipoPrenda).ToList();
        }

        public Prenda Consultar(int IdPrenda)
        {
            return dbExamen.Prendas.FirstOrDefault(p => p.IdPrenda == IdPrenda);
        }

        public string Eliminar()
        {
            try
            {
                Prenda pre = Consultar(prenda.IdPrenda);
                if (pre == null)
                {
                    return "La prenda con el ID ingresado no existe, por lo tanto no se puede eliminar";
                }
                dbExamen.Prendas.Remove(pre);
                dbExamen.SaveChanges();
                return "Se eliminó la prenda correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar la prenda: " + ex.Message;
            }
        }

        public string Eliminar(int IdPrenda)
        {
            try
            {
                Prenda pre = Consultar(IdPrenda);
                if (pre == null)
                {
                    return "La prenda con el ID ingresado no existe, por lo tanto no se puede eliminar";
                }
                dbExamen.Prendas.Remove(pre);
                dbExamen.SaveChanges();
                return "Se eliminó la prenda correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar la prenda: " + ex.Message;
            }
        }
        public string GrabarImagenPrenda(int idPrenda, List<string> Imagenes)
        {
            try
            {
                foreach (string imagen in Imagenes)
                {
                    FotoPrenda imagenPrenda = new FotoPrenda();
                    imagenPrenda.idPrenda = idPrenda;
                    imagenPrenda.FotoPrenda1 = imagen;
                    dbExamen.FotoPrendas.Add(imagenPrenda);
                    dbExamen.SaveChanges();
                }
                return "Se grabó la información en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

    }
}
