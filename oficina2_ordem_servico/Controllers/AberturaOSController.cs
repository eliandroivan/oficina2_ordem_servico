using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using oficina2_ordem_servico.Models;
using X.PagedList;

namespace oficina2_ordem_servico.Controllers
{
    public class AberturaOSController : Controller
    {
        //GET: Ajax
        public ActionResult UltimoNumeroOS()
        {
            var serializador = new JavaScriptSerializer();
            var resultado = serializador.Serialize(new AberturaOSModel().UltimoNumeroOS());
            return Content(resultado);
        }


        /*
        // GET: AberturaOS
        public ActionResult Index(int pagina = 1)
        {            
            AberturaOSModel lista = new AberturaOSModel();
            var listaOrdenada = lista.ListarAberturaOS().OrderBy(n => n.Numero)
                                                    .ToPagedList(pagina, 5);
            return View(listaOrdenada);
        }
        */
        // GET: AberturaOS
        public ActionResult Index(string sortOrder = "", string currentFilter = "", string searchString = "", string ColunaPesquisa="", int pagina = 1)
        {
            //https://docs.microsoft.com/pt-br/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application

            ViewBag.CurrentSort = sortOrder;
            if (searchString != "")
            {
                pagina = 1;
            }
            else
            {
                searchString = currentFilter.ToUpper();
            }

            ViewBag.CurrentFilter = searchString.ToUpper();
            AberturaOSModel lista = new AberturaOSModel();
            //var listaOrdenada = lista.ListarAberturaOS();
            var listaOrdenada = from s in lista.ListarAberturaOS()
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                switch (ColunaPesquisa)
                {                    
                    case "Cliente":
                        listaOrdenada = listaOrdenada.Where(s => s.Cliente.Nome.Contains(searchString.ToUpper()));
                        break;
                    case "Placa":
                        listaOrdenada = listaOrdenada.Where(s => s.Veiculo.Placa.Contains(searchString.ToUpper()));
                        break;                    
                    case "Consultor Usuário":
                        listaOrdenada = listaOrdenada.Where(s => s.ConsultorUsuario.Nome.Contains(searchString.ToUpper()));
                        break;
                    case "Reclamação":
                        listaOrdenada = listaOrdenada.Where(s => s.Reclamacao.Contains(searchString.ToUpper()));
                        break;
                        //default:
                        //    break;
                }
                
            }
            switch (sortOrder)
            {
                case "Id":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Id);
                    break;
                case "Numero":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Numero);
                    break;
                case "Situação":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Situacao.Descricao);
                    break;
                case "TipoOS":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.TipoOS.Descricao);
                    break;
                case "Cliente":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Cliente.Nome);
                    break;
                case "Placa":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Veiculo.Placa);
                    break;
                case "Quilometragem Entrada":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.QuilometragemEntrada);
                    break;
                case "Consultor Usuário":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.ConsultorUsuario.Nome);
                    break;
                case "Abertura":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Abertura);
                    break;
                case "Previsao Entrega":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.PrevisaoEntrega);
                    break;
                case "Reclamação":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Reclamacao);
                    break;
                default:  // numero ascending 
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Numero);
                    break;
            }
            return View(listaOrdenada.ToPagedList(pagina, 5));            
        }

        // GET: AberturaOS
        public ActionResult Index2(string sortOrder="", string currentFilter="", string searchString = "", int pagina = 1)
        {
           
            ViewBag.CurrentSort = sortOrder;
            if (searchString != "")
            {
                pagina = 1;
            }
            else
            {
                searchString = currentFilter.ToUpper();
            }

            ViewBag.CurrentFilter = searchString.ToUpper();
            AberturaOSModel lista = new AberturaOSModel();
            //var listaOrdenada = lista.ListarAberturaOS();
            var listaOrdenada = from s in lista.ListarAberturaOS()
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                listaOrdenada = listaOrdenada.Where(s => s.Veiculo.Placa.Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Id":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Id);
                    break;
                case "Numero":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Numero);
                    break;
                case "Situação":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Situacao);
                    break;
                case "TipoOS":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.TipoOS);
                    break;
                case "Cliente":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Cliente.Nome);
                    break;
                case "Placa":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Veiculo.Placa);
                    break;
                case "Quilometragem Entrada":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.QuilometragemEntrada);
                    break;
                case "Consultor Usuário":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.ConsultorUsuario.Nome);
                    break;
                case "Abertura":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Abertura);
                    break;
                case "Previsao Entrega":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.PrevisaoEntrega);
                    break;
                case "Reclamação":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Reclamacao);
                    break;
                default:  // numero ascending 
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Numero);
                    break;
            }


            return View(listaOrdenada.ToPagedList(pagina, 5));
            
            

        }

        // GET: AberturaOS/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AberturaOS/Create
        public ActionResult Cadastrar()
        {
            ViewData["UltimoNumeroOS"] = new AberturaOSModel().UltimoNumeroOS();
            
            ViewBag.id_situacao = new SelectList
                (
                  new SituacaoModel().ListarSituacao(),
                  "id",
                  "descricao"
                  
              );
            ViewBag.id_tipo_os = new SelectList
             (
                 new TipoOSModel().ListarTipoOS(),
                 "id",
                 "descricao"
             );
            ViewBag.id_cliente = new SelectList
                (
                  new ClienteModel().ListarCliente(),
                  "id",
                  "nome"
              );
            ViewBag.id_veiculo = new SelectList
             (
                 new List<VeiculoModel>(),
                 "id",
                 "placa"
             );
            ViewBag.id_consultor_usuario = new SelectList
            (
                new ConsultorUsuarioModel().ListarConsultorUsuario(),
                "id",
                "nome"
            );
            return View();
        }

        // POST: AberturaOS/Create
        [HttpPost]
        public ActionResult Cadastrar(FormCollection inputs)
        {
            ViewBag.id_situacao = new SelectList
                (
                  new SituacaoModel().ListarSituacao(),
                  "id",
                  "descricao"
              );
            ViewBag.id_tipo_os = new SelectList
             (
                 new TipoOSModel().ListarTipoOS(),
                 "id",
                 "descricao"
             );
            ViewBag.id_cliente = new SelectList
                (
                  new ClienteModel().ListarCliente(),
                  "id",
                  "nome"
              );
            ViewBag.id_veiculo = new SelectList
             (
                 new List<VeiculoModel>(),
                 "id",
                 "placa"
             );
            ViewBag.id_consultor_usuario = new SelectList
            (
                new ConsultorUsuarioModel().ListarConsultorUsuario(),
                "id",
                "nome"
            );
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "INSERT INTO ordem_servico (numero,id_situacao,id_tipo_os,id_veiculo,quilometragem_entrada,id_cliente,id_consultor_usuario,abertura,previsao_entrega,reclamacao_cliente) VALUES (@numero,@id_situacao,@id_tipo_os,@id_veiculo,@quilometragem_entrada,@id_cliente,@id_consultor_usuario,@abertura,@previsao_entrega,@reclamacao_cliente)";

            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.Parameters.AddWithValue("@numero", inputs["Numero"].ToUpper());
                comando2.Parameters.AddWithValue("@id_situacao", 1);
                comando2.Parameters.AddWithValue("@id_tipo_os", inputs["id_tipo_os"]);
                comando2.Parameters.AddWithValue("@id_veiculo", inputs["id_veiculo"]);
                comando2.Parameters.AddWithValue("@quilometragem_entrada", inputs["QuilometragemEntrada"]);
                comando2.Parameters.AddWithValue("@id_cliente", inputs["id_cliente"]);
                comando2.Parameters.AddWithValue("@id_consultor_usuario", inputs["id_consultor_usuario"]);
                comando2.Parameters.AddWithValue("@abertura", inputs["Abertura"]);
                comando2.Parameters.AddWithValue("@previsao_entrega", inputs["PrevisaoEntrega"]);
                comando2.Parameters.AddWithValue("@reclamacao_cliente", inputs["Reclamacao"].ToUpper());
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            /*
            EnviarEmailModel envio = new EnviarEmailModel();
            StringBuilder CorpoEmail = new StringBuilder();
            CorpoEmail.AppendLine("Olá ");
            //buscar email Cliente
            CorpoEmail.AppendLine(inputs[);
            CorpoEmail.AppendLine(" from ordem_servico");
            envio.Email("dracon0621@gmail.com", "Ordem de Serviço - Abertura", "corpo do texto teste");
            */
            EnviarEmailModel envio = new EnviarEmailModel();
            envio.Email("dracon0621@gmail.com", "Ordem de Serviço - Abertura", "corpo do texto teste");
            try
            {
                // TODO: Add insert logic here
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AberturaOS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AberturaOS/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AberturaOS/Delete/5
        //deleta no banco de dados e retorna pra index
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM ordem_servico WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: AberturaOS/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
