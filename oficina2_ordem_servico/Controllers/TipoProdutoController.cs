﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using oficina2_ordem_servico.Models;

namespace oficina2_ordem_servico.Controllers
{
    public class TipoProdutoController : Controller
    {
        // GET: TipoProduto
        public ActionResult Index()
        {
            TipoProdutoModel listaTipoProduto = new TipoProdutoModel();
            return View(listaTipoProduto.ListarTipoProduto());
        }

        // GET: TipoProduto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoProduto/Create
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: TipoProduto/Create
        [HttpPost]
        public ActionResult Cadastrar(FormCollection inputs)
        {
            try
            {
                // TODO: Add insert logic here
                BandoDeDadosModel bd = new BandoDeDadosModel();
                MySqlConnection conexao = bd.ConexaoBD();
                string query = "INSERT INTO tipo_produto (descricao) VALUES (@tipoProduto)";
                using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    comando2.Parameters.AddWithValue("@tipoProduto", inputs["TipoProduto"].ToUpper());
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

        // GET: TipoProduto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoProduto/Edit/5
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

        // GET: TipoProduto/Delete/5
        //deleta no banco de dados e retorna pra index
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM tipo_produto WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: TipoProduto/Delete/5
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
