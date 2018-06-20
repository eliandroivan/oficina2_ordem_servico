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
    public class ExecucaoOSController : Controller
    {
        //Ajax
        [HttpPost]
        public void InserirItens(InserirItem it)
        {
            
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            conexao.Open();
            var id_OS = it.id_os[0];
            for (int i = 0; i < it.Itens.Count; i++)
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO os_produto");
                query.AppendLine("(id_ordem_servico, id_produto, quantidade)");
                query.AppendLine(" values ( @idOs, @IdProduto, @qtdItens)");
                MySqlCommand comando = new MySqlCommand(query.ToString(), conexao);
                comando.Parameters.AddWithValue("@idOs", id_OS);
                comando.Parameters.AddWithValue("@IdProduto", it.Itens[i]);
                comando.Parameters.AddWithValue("@qtdItens", it.qtdItem[i]);
                //comando2.Parameters.AddWithValue("@ValorItensOs", it.Preco[i]);

                comando.ExecuteNonQuery();

            }
            conexao.Close();


            FechamentoOSModel ValorTotal = new FechamentoOSModel();
            ValorTotal.ValorTotal = FechamentoOSModel.ValorTotalOS(id_OS);
            BandoDeDadosModel bd2 = new BandoDeDadosModel();
            MySqlConnection conexao2 = bd2.ConexaoBD();
            StringBuilder query2 = new StringBuilder();
            query2.AppendLine("UPDATE ");
            query2.AppendLine("ordem_servico ");
            query2.AppendLine("SET ");
            query2.AppendLine("valor_total = @ValorTotal ");
            query2.AppendLine("WHERE id = @id_os");
            using (MySqlCommand comando2 = new MySqlCommand(query2.ToString(), conexao2))
            {
                conexao2.Open();
                comando2.Parameters.AddWithValue("@id_os", id_OS);
                comando2.Parameters.AddWithValue("@ValorTotal", ValorTotal.ValorTotal);
                comando2.ExecuteNonQuery();
                conexao2.Close();
            }

            /*
            FechamentoOSModel valorTotal = new FechamentoOSModel();
            valorTotal.ValorTotalOS(Convert.ToInt32(it.id_os[0]));
            var teste = valorTotal;
            */
        }

        //GET: Ajax
        public ActionResult ListaProdutos()
        {
            var serializador = new JavaScriptSerializer();
            var resultado = serializador.Serialize(new ProdutoModel().ListarProduto());

            return Content(resultado);
        }

        // GET: ExecucaoOS
        public ActionResult Index()
        {
            ExecucaoOSModel listaExecucaoOS = new ExecucaoOSModel();
            return View(listaExecucaoOS.ListarExecucaoOS());
        }

        // GET: ExecucaoOS/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExecucaoOS/Create
        public ActionResult Cadastrar()
        {
            ViewBag.id_produto = new SelectList
                (
                  new ProdutoModel().ListarProduto(),
                  "Id",
                  "Descricao",
                  "ValorUnitario"
              );
            ViewBag.id_os = new SelectList
                (
                  new AberturaOSModel().ListarAberturaOS(),
                  "id",
                  "Numero"
              );
            return View();
        }

        // POST: ExecucaoOS/Create
        [HttpPost]
        public ActionResult Cadastrar(FormCollection collection)
        {
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

        // GET: ExecucaoOS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExecucaoOS/Edit/5
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

        // GET: ExecucaoOS/Delete/5
        //deleta no banco de dados e retorna pra index
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM os_produto WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: ExecucaoOS/Delete/5
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
