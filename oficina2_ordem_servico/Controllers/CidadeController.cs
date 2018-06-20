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
    public class CidadeController : Controller
    {
        //GET: Ajax
        [HttpGet]
        public ActionResult ListaCidadeEstado(int id_consulta)
        {
            string id_coluna = "id_estado";
            var serializador = new JavaScriptSerializer();
            var resultado = serializador.Serialize(new CidadeModel().ListarCidade(id_consulta, id_coluna));

            return Content(resultado);
        }

        // GET: Cidade
        public ActionResult Index()
        {
            CidadeModel listaCidade = new CidadeModel();
            return View(listaCidade.ListarCidade());
        }

        // GET: Cidade/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cidade/Create
        public ActionResult Cadastrar()
        {
            ViewBag.Estado = new SelectList
              (
                  new EstadoModel().ListarEstado(),
                  "id",
                  "nome",
                  "sigla"
              );
            return View();
        }

        // POST: Cidade/Create
        [HttpPost]
        public ActionResult Cadastrar(FormCollection inputs)
        {
            ViewBag.id_estado = new SelectList
              (
                  new EstadoModel().ListarEstado(),
                  "id",
                  "nome",
                  "sigla"
              );
            try
            {
                // TODO: Add insert logic here
                BandoDeDadosModel bd = new BandoDeDadosModel();
                MySqlConnection conexao = bd.ConexaoBD();
                string query = "INSERT INTO cidade (nome,id_estado) VALUES (@nome, @id_estado)";

                using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    comando2.Parameters.AddWithValue("@nome", inputs["nome"].ToUpper());
                    comando2.Parameters.AddWithValue("@id_estado", inputs["id_estado"]);
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

        // GET: Cidade/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cidade/Edit/5
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

        // GET: Cidade/Delete/5
        //deleta no banco de dados e retorna pra index
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM cidade WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: Cidade/Delete/5
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
