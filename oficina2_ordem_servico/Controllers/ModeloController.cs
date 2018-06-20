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
    public class ModeloController : Controller
    {
        // GET: Modelo
        public ActionResult Index()
        {
            ModeloModel listaModelo = new ModeloModel();
            return View(listaModelo.ListarModelo());
        }

        // GET: Modelo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Modelo/Create
        public ActionResult Cadastrar()
        {
            ViewBag.id_marca = new SelectList
              (
                  new MarcaModel().ListarMarca(),
                  "id",
                  "descricao"
              );            
            return View();
        }

        // POST: Modelo/Create
        [HttpPost]
        public ActionResult Cadastrar(FormCollection inputs)
        {
            ViewBag.id_marca = new SelectList
              (
                  new MarcaModel().ListarMarca(),
                  "id",
                  "descricao"
              );
            try
            {
                // TODO: Add insert logic here
                BandoDeDadosModel bd = new BandoDeDadosModel();
                MySqlConnection conexao = bd.ConexaoBD();
                string query = "INSERT INTO modelo (descricao,id_marca) VALUES (@descricao,@id_marca)";

                using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    comando2.Parameters.AddWithValue("@descricao", inputs["descricao"].ToUpper());
                    comando2.Parameters.AddWithValue("@id_marca", inputs["id_marca"]);
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

        // GET: Modelo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Modelo/Edit/5
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

        // GET: Modelo/Delete/5
        //deleta no banco de dados e retorna pra index
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM modelo WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: Modelo/Delete/5
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
