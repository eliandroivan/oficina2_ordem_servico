using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using oficina2_ordem_servico.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace oficina2_ordem_servico.Controllers
{
    public class LoginController : Controller
    {
        // GET: TesteLogins/Create
        [AllowAnonymous]
        public ActionResult Entrar()
        {
            var nome = User.Identity.Name; // obter nome do usuario
            return View();
        }

        // POST: TesteLogins/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Entrar(FormCollection inputs)
        {
            //if (ModelState.IsValid)
            //{
            // Cria uma nova intância do objeto que implementa o algoritmo para
            // criptografia MD5
            MD5 md5Hasher = MD5.Create();
                // Criptografa o valor passado
                byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(inputs["LoginSenha"]));

                // Cria um StringBuilder para passarmos os bytes gerados para ele
                StringBuilder strBuilder = new StringBuilder();
                // Converte cada byte em um valor hexadecimal e adiciona ao string builder
                // and format each one as a hexadecimal string.
                for (int i = 0; i < valorCriptografado.Length; i++)
                {
                    strBuilder.Append(valorCriptografado[i].ToString("x2"));
                }
                strBuilder.ToString();

                string query = "select * from consultor_usuario WHERE login_usuario= '" + inputs["LoginUsuario"] + "' and login_senha = '" + strBuilder.ToString() +"'";

                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                conn_string.Server = "localhost";
                conn_string.UserID = "root";
                conn_string.Password = "";
                conn_string.Database = "oficina2_ordem_servico";
                conn_string.SslMode = MySqlSslMode.None;

                using (MySqlConnection conexao = new MySqlConnection(conn_string.ToString()))
                {
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        conexao.Open();
                        MySqlDataReader leitor = comando.ExecuteReader();
                        if (leitor.HasRows)
                        {
                        FormsAuthentication.SetAuthCookie(inputs["LoginUsuario"], false);
                        return RedirectToAction("Index", "Home");
                    }
                        leitor.Close();
                    }
                }


            // }

            return View();
        }

        // GET: TesteLogin
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Entrar");
        }




        // GET: TesteLogin
        public ActionResult Index()
        {
            return View();
        }

        // GET: TesteLogin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TesteLogin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TesteLogin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: TesteLogin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TesteLogin/Edit/5
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

        // GET: TesteLogin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TesteLogin/Delete/5
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
