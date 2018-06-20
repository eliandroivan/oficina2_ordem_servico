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
    public class CondicaoPagamentoController : Controller
    {
        // GET: CondicaoPagamento
        public ActionResult Index()
        {
            CondicaoPagamentoModel condicaoPagamento = new CondicaoPagamentoModel();
            return View(condicaoPagamento.ListarCondicaoPagamento());
        }

        // GET: CondicaoPagamento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CondicaoPagamento/Create
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: CondicaoPagamento/Create
        [HttpPost]
        public ActionResult Cadastrar(FormCollection inputs)
        {
            try
            {
                // TODO: Add insert logic here
                BandoDeDadosModel bd = new BandoDeDadosModel();
                MySqlConnection conexao = bd.ConexaoBD();
                string query = "INSERT INTO condicao_pagamento (descricao) VALUES (@descricao)";
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

        // GET: CondicaoPagamento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CondicaoPagamento/Edit/5
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

        //deleta no banco de dados e retorna pra index
        // GET: CondicaoPagamento/Delete/5
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM condicao_pagamento WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();                
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: CondicaoPagamento/Delete/5
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
