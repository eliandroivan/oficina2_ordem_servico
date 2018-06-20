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
    public class ClienteController : Controller
    {
        //GET: Ajax
        public ActionResult ListaVeiculos(int id)
        {
            var serializador = new JavaScriptSerializer();
            var resultado = serializador.Serialize(new VeiculoModel().ListarVeiculo(id));
            //var resultado = serializador.Serialize(new VeiculoModel().ListarVeiculo(id));
            return Content(resultado);
        }


        // GET: Cliente
        public ActionResult Index()
        {
            ClienteModel listaCliente = new ClienteModel();
            return View(listaCliente.ListarCliente());
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cliente/Create
        public ActionResult Cadastrar()
        {
            Listas();
            /*
            ViewBag.id_cidade = new SelectList
              (
                  new List<CidadeModel>(),
                  "id",
                  "nome",
                  "id_estado"
              );
              */
            return View();
        }

        public void Listas()
        {
            ViewBag.id_estado = new SelectList
            (
             new EstadoModel().ListarEstado(),
             "id",
             "sigla",
             "nome"
            );
                ViewBag.id_cidade = new SelectList
                      (
                          new CidadeModel().ListarCidade(),
                          "id",
                          "nome",
                          "id_estado"
                      );
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Cadastrar(ClienteModel inputs)
        {
            
            
            if (ModelState.IsValid)
            {

                /*
                ViewBag.id_cidade = new SelectList
                  (
                      new List<CidadeModel>(),
                      "id",
                      "nome",
                      "id_estado"
                  );
                  */
                BandoDeDadosModel bd = new BandoDeDadosModel();
                MySqlConnection conexao = bd.ConexaoBD();
                string query = "INSERT INTO cliente (nome,cpf_cnpj,email,telefone1,telefone2,logradouro,logradouro_numero,logradouro_complemento,bairro,id_cidade,id_estado,cep) VALUES (@nome,@cpfCnpj,@email,@telefone1,@telefone2,@logradouro,@logradouro_numero,@logradouro_complemento,@bairro,@id_cidade,@id_estado,@cep)";

                using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    comando2.Parameters.AddWithValue("@nome", inputs.Nome);
                    comando2.Parameters.AddWithValue("@cpfCnpj", inputs.CpfCnpj);
                    comando2.Parameters.AddWithValue("@email", inputs.Email.ToLower());
                    comando2.Parameters.AddWithValue("@telefone1", inputs.Telefone1);
                    comando2.Parameters.AddWithValue("@telefone2", inputs.Telefone2);
                    comando2.Parameters.AddWithValue("@logradouro", inputs.Logradouro.ToUpper());
                    comando2.Parameters.AddWithValue("@logradouro_numero", inputs.LogradouroNumero.ToUpper());
                    comando2.Parameters.AddWithValue("@logradouro_complemento", inputs.LogradouroComplemento.ToUpper());
                    comando2.Parameters.AddWithValue("@bairro", inputs.Bairro.ToUpper());
                    comando2.Parameters.AddWithValue("@id_cidade", inputs.id_cidade);
                    comando2.Parameters.AddWithValue("@id_estado", inputs.id_estado);
                    comando2.Parameters.AddWithValue("@cep", inputs.CEP);
                    comando2.ExecuteNonQuery();
                    conexao.Close();
                    
                    
                }
                    Listas();
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
            Listas();
            return View();

    }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cliente/Edit/5
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

        // GET: Cliente/Delete/5
        //deleta no banco de dados e retorna pra index
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM cliente WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: Cliente/Delete/5
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
