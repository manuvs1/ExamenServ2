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
    [RoutePrefix("api/Clientes")]
    public class ClientesController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Cliente> ConsultarTodos()
        {
            clsCliente Cliente = new clsCliente();
            return Cliente.ConsultarTodos();
        }
        [HttpGet]
        [Route("ConsultarXDocumento")]
        public Cliente ConsultarXDocumento(string Documento)
        {

            clsCliente Cliente = new clsCliente();
            return Cliente.Consultar(Documento);
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Cliente cliente)
        {
            clsCliente Cliente = new clsCliente();
            Cliente.cliente = cliente;
            return Cliente.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Cliente cliente)
        {
            clsCliente Cliente = new clsCliente();
            Cliente.cliente = cliente;
            return Cliente.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Cliente cliente)
        {
            clsCliente Cliente = new clsCliente();
            Cliente.cliente = cliente;
            return Cliente.Eliminar();
        }

    }
}