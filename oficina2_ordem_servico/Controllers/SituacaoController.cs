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
    public class SituacaoController : Controller
    {
        // GET: Situacao
        public ActionResult Index()
        {
            SituacaoModel listaSituacao = new SituacaoModel();
            return View(listaSituacao.ListarSituacao());
        }

        // GET: Situacao/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Situacao/Create
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: Situacao/Create
        [HttpPost]
        public ActionResult Cadastrar(FormCollection inputs)
        {
            try
            {
                // TODO: Add insert logic here
                BandoDeDadosModel bd = new BandoDeDadosModel();
                MySqlConnection conexao = bd.ConexaoBD();
                string query = "INSERT INTO situacao (descricao) VALUES (@descricao)";

                using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    comando2.Parameters.AddWithValue("@descricao", inputs["descricao"].ToUpper());
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

        // GET: Situacao/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Situacao/Edit/5
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

        // GET: Situacao/Delete/5
        //deleta no banco de dados e retorna pra index
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM situacao WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: Situacao/Delete/5
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
