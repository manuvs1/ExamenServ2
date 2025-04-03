using Servicios_Jue.Clases;
using Servicios_Jue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicios_Jue.Controllers
{
    [RoutePrefix("api/Prendas")]
    public class PrendasController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Prenda> ConsultarTodos()
        {
            clsPrenda Prenda = new clsPrenda();
            return Prenda.ConsultarTodos();
        }
        [HttpGet]
        [Route("Consultar")]
        public Prenda Consultar(int Codigo)
        {
            clsPrenda Prenda = new clsPrenda();
            return Prenda.Consultar(Codigo);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Prenda prenda)
        {
            clsPrenda Prenda = new clsPrenda();
            Prenda.prenda = prenda;
            return Prenda.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Prenda prenda)
        {
            clsPrenda Prenda = new clsPrenda();
            Prenda.prenda = prenda;
            return Prenda.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Prenda prenda)
        {
            clsPrenda Prenda = new clsPrenda();
            Prenda.prenda = prenda;
            return Prenda.Eliminar();
        }

    }
}