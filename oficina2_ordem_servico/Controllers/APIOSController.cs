using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using oficina2_ordem_servico.Models;

namespace oficina2_ordem_servico.Controllers
{
    public class APIOSController : ApiController
    {
        //precisa acrescentar um código na pasta APP_START arquivo WebApiConfig.cs


        // GET: api/APIOS
        public IEnumerable Get()
        {

            var lista = new APIModel().ListaAPI().OrderBy(i => i.Numero);
            return lista;

            //var clientes = new ClienteModel().ListarCliente();
            //return clientes;
            try
            {
                
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: api/APIOS/5
        public IEnumerable Get(int id)
        {
            
            try
            {
                var clientes = new ClienteModel().ListarCliente(id);
                return clientes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*
        // GET: api/APIOS
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        

        // GET: api/APIOS/5
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST: api/APIOS
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/APIOS/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/APIOS/5
        public void Delete(int id)
        {
        }
    }
}
