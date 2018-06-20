using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using oficina2_ordem_servico.Models;

namespace oficina2_ordem_servico.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            ProdutoModel listaProduto = new ProdutoModel();
            return View(listaProduto.ListarProduto());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Produto/Create
        public ActionResult Cadastrar()
        {
            ViewBag.id_tipo_produto = new SelectList
                (
                  new TipoProdutoModel().ListarTipoProduto(),
                  "id",
                  "TipoProduto"
              );
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult Cadastrar(FormCollection inputs)
        {
            ViewBag.id_tipo_produto = new SelectList
                   (
                     new TipoProdutoModel().ListarTipoProduto(),
                     "id",
                     "TipoProduto"
                 );
            try
            {
                // TODO: Add insert logic here
                BandoDeDadosModel bd = new BandoDeDadosModel();
                MySqlConnection conexao = bd.ConexaoBD();
                string query = "INSERT INTO produto (id_tipo_produto,referencia,descricao,unidade,quantidade,valor_unitario) VALUES (@id_tipo_produto,@referencia,@descricao,@unidade,@quantidade,@valor_unitario)";

                using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    comando2.Parameters.AddWithValue("@id_tipo_produto", inputs["id_tipo_produto"]);
                    comando2.Parameters.AddWithValue("@referencia", inputs["Referencia"].ToUpper());
                    comando2.Parameters.AddWithValue("@descricao", inputs["Descricao"].ToUpper());
                    comando2.Parameters.AddWithValue("@unidade", inputs["Unidade"].ToUpper());
                    comando2.Parameters.AddWithValue("@quantidade", inputs["Quantidade"]);
                    comando2.Parameters.AddWithValue("@valor_unitario", inputs["ValorUnitario"]);
                    comando2.ExecuteNonQuery();
                    conexao.Close();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Produto/Edit/5
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

        // GET: Produto/Delete/5
        //deleta no banco de dados e retorna pra index
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM produto WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: Produto/Delete/5
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
