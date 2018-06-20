using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using oficina2_ordem_servico.Models;

namespace oficina2_ordem_servico.Controllers
{
    public class FechamentoOSController : Controller
    {
        //GET: Ajax
        public ActionResult ValorTotalOS(int id)
        {
            var serializador = new JavaScriptSerializer();
            var resultado = serializador.Serialize(FechamentoOSModel.ValorTotalOS(id));
            return Content(resultado);
        }

        //GET: Ajax
        public ActionResult ListaItens(int id)
        {
            var serializador = new JavaScriptSerializer();
            var resultado = serializador.Serialize(new ExecucaoOSModel().ListarExecucaoOS(id));

            return Content(resultado);
        }

        // GET: FechamentoOS
        public ActionResult Index()
        {
            FechamentoOSModel listaFechamentoOS = new FechamentoOSModel();
            return View(listaFechamentoOS.ListarFechamento().OrderBy(o => o.Id));
        }

        // GET: FechamentoOS/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FechamentoOS/Create
        public ActionResult Cadastrar()
        {
            

            ViewBag.id_condicao_pagamento = new SelectList
            (
                new CondicaoPagamentoModel().ListarCondicaoPagamento(),
                "id",
                "descricao"
            );
            ViewBag.id_os = new SelectList
                (
                  new AberturaOSModel().ListarAberturaOS(),
                  "id",
                  "Numero"
              );
            return View();
        }

        // POST: FechamentoOS/Create
        [HttpPost]
        public ActionResult Cadastrar(FormCollection inputs)
        {
            ViewBag.id_condicao_pagamento = new SelectList
            (
                new CondicaoPagamentoModel().ListarCondicaoPagamento(),
                "id",
                "descricao"
            );
            ViewBag.id_os = new SelectList
                (
                  new AberturaOSModel().ListarAberturaOS(),
                  "id",
                  "Numero"
              );
            
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            StringBuilder query = new StringBuilder();
            query.AppendLine("UPDATE ");
            query.AppendLine("ordem_servico ");
            query.AppendLine("SET ");
            query.AppendLine("id_situacao = 2, ");
            query.AppendLine("entrega = @Entrega, ");
            query.AppendLine("id_condicao_pagamento = @id_condicao_pagamento, ");
            query.AppendLine("valor_total = @ValorTotal ");
            query.AppendLine("WHERE id = @id_os");

            using (MySqlCommand comando2 = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                comando2.Parameters.AddWithValue("@id_os", inputs["id_os"]);
                //comando2.Parameters.AddWithValue("@id_situacao", inputs["id_situacao"]);
                comando2.Parameters.AddWithValue("@entrega", inputs["Entrega"]);
                comando2.Parameters.AddWithValue("@id_condicao_pagamento", inputs["id_condicao_pagamento"]);
                comando2.Parameters.AddWithValue("@ValorTotal", float.Parse(inputs["ValorTotal"]));
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
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

        // GET: FechamentoOS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FechamentoOS/Edit/5
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

        // GET: FechamentoOS/Delete/5
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

        // POST: FechamentoOS/Cancelar/5
        [HttpPost]
        public ActionResult Cancelar(string id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            StringBuilder query = new StringBuilder();
            query.AppendLine("UPDATE ");
            query.AppendLine("ordem_servico ");
            query.AppendLine("SET ");
            query.AppendLine("id_situacao = 3 ");            
            query.AppendLine("WHERE id = ");
            query.AppendLine(id);

            using (MySqlCommand comando2 = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
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
