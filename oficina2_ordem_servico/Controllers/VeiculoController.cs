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
    public class VeiculoController : Controller
    {
        //GET: Ajax
        public ActionResult ListaModelosMarca(int id)
        {
            var serializador = new JavaScriptSerializer();
            var resultado = serializador.Serialize(new ModeloModel().ListarModelo(id));
            //VeiculoModel.ListarVeiculo(id).OrderBy(x >= a);
            return Content(resultado);
        }


        // GET: Veiculo
        public ActionResult Index()
        {
            VeiculoModel listaVeiculo = new VeiculoModel();
            return View(listaVeiculo.ListarVeiculo());
        }

        // GET: Veiculo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Veiculo/Create
        public ActionResult Cadastrar()
        {
            ViewBag.id_cliente = new SelectList
               (
                 new ClienteModel().ListarCliente(),
                 "id",
                 "nome"
             );
            ViewBag.id_marca = new SelectList
              (
                  new MarcaModel().ListarMarca(),
                  "id",
                  "descricao"
              );
            ViewBag.id_modelo = new SelectList
              (
                  new List<ModeloModel>(),
                  "id",
                  "descricao",
                  "id_marca"
              );
            return View();
        }

        // POST: Veiculo/Create
        [HttpPost]
        public ActionResult Cadastrar(FormCollection inputs)
        {
            ViewBag.id_cliente = new SelectList
               (
                 new ClienteModel().ListarCliente(),
                 "id",
                 "nome"
             );
            ViewBag.id_marca = new SelectList
              (
                  new MarcaModel().ListarMarca(),
                  "id",
                  "descricao"
              );
            ViewBag.id_modelo = new SelectList
              (
                  new List<ModeloModel>(),
                  "id",
                  "descricao",
                  "id_marca"
              );
            try
            {
                // TODO: Add insert logic here
                BandoDeDadosModel bd = new BandoDeDadosModel();
                MySqlConnection conexao = bd.ConexaoBD();
                string query = "INSERT INTO veiculo (id_cliente,id_marca,id_modelo,placa,chassi,ano_fabricacao,ano_modelo,cor) VALUES (@id_cliente,@id_marca,@id_modelo,@placa,@chassi,@anoFabricacao,@anoModelo,@cor)";

                using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    comando2.Parameters.AddWithValue("@id_cliente", inputs["id_cliente"]);
                    comando2.Parameters.AddWithValue("@id_marca", inputs["id_marca"]);
                    comando2.Parameters.AddWithValue("@id_modelo", inputs["id_modelo"]);
                    comando2.Parameters.AddWithValue("@placa", inputs["Placa"].ToUpper());
                    comando2.Parameters.AddWithValue("@chassi", inputs["Chassi"].ToUpper());
                    comando2.Parameters.AddWithValue("@anoFabricacao", inputs["AnoFabricacao"]);
                    comando2.Parameters.AddWithValue("@anoModelo", inputs["AnoModelo"]);
                    comando2.Parameters.AddWithValue("@cor", inputs["Cor"].ToUpper());
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

        // GET: Veiculo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Veiculo/Edit/5
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

        // GET: Veiculo/Delete/5
        //deleta no banco de dados e retorna pra index
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM veiculo WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: Veiculo/Delete/5
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
