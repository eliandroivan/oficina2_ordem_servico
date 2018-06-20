using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using oficina2_ordem_servico.Models;

namespace oficina2_ordem_servico.Controllers
{
    public class ConsultorUsuarioController : Controller
    {
        // GET: ConsultorUsuario
        public ActionResult Index()
        {
            ConsultorUsuarioModel consultorUsuario = new ConsultorUsuarioModel();
            return View(consultorUsuario.ListarConsultorUsuario());
        }

        // GET: ConsultorUsuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ConsultorUsuario/Create
        [AllowAnonymous]
        public ActionResult Cadastrar()
        {
            ViewBag.Estado = new SelectList
             (
                 new EstadoModel().ListarEstado(),
                 "id",
                 "sigla",
                 "nome"
             );
            ViewBag.Cidade = new SelectList
              (
                  new CidadeModel().ListarCidade(),
                  "id",
                  "nome",
                  "id_estado"
              );
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

        // POST: ConsultorUsuario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Cadastrar(FormCollection inputs)
        {
            ViewBag.Estado = new SelectList
             (
                 new EstadoModel().ListarEstado(),
                 "id",
                 "sigla",
                 "nome"
             );
            ViewBag.Cidade = new SelectList
              (
                  new CidadeModel().ListarCidade(),
                  "id",
                  "nome",
                  "id_estado"
              );
            /*
            ViewBag.id_cidade = new SelectList
              (
                  new List<CidadeModel>(),
                  "id",
                  "nome",
                  "id_estado"
              );
              */
            // Cria um StringBuilder para passarmos os bytes gerados para ele
            StringBuilder strBuilder = new StringBuilder();
            if (ModelState.IsValid)
            {
                // Cria uma nova intância do objeto que implementa o algoritmo para
                // criptografia MD5
                MD5 md5Hasher = MD5.Create();
                // Criptografa o valor passado
                byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(inputs["LoginSenha"]));

                
                // Converte cada byte em um valor hexadecimal e adiciona ao string builder
                // and format each one as a hexadecimal string.
                for (int i = 0; i < valorCriptografado.Length; i++)
                {
                    strBuilder.Append(valorCriptografado[i].ToString("x2"));
                }
                
                

            }
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "INSERT INTO consultor_usuario (nome,cpf_cnpj,email,telefone1,telefone2,logradouro,logradouro_numero,logradouro_complemento,bairro,id_cidade,id_estado,cep,login_usuario,login_senha) VALUES (@nome,@cpfCnpj,@email,@telefone1,@telefone2,@logradouro,@logradouro_numero,@logradouro_complemento,@bairro,@id_cidade,@id_estado,@cep,@login_usuario,@login_senha)";

            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.Parameters.AddWithValue("@nome", inputs["Nome"].ToUpper());
                comando2.Parameters.AddWithValue("@cpfCnpj", inputs["CpfCnpj"]);
                comando2.Parameters.AddWithValue("@email", inputs["Email"].ToLower());
                comando2.Parameters.AddWithValue("@telefone1", inputs["Telefone1"]);
                comando2.Parameters.AddWithValue("@telefone2", inputs["Telefone2"]);
                comando2.Parameters.AddWithValue("@logradouro", inputs["Logradouro"].ToUpper());
                comando2.Parameters.AddWithValue("@logradouro_numero", inputs["LogradouroNumero"].ToUpper());
                comando2.Parameters.AddWithValue("@logradouro_complemento", inputs["LogradouroComplemento"].ToUpper());
                comando2.Parameters.AddWithValue("@bairro", inputs["Bairro"].ToUpper());
                comando2.Parameters.AddWithValue("@id_cidade", inputs["id_cidade"]);
                comando2.Parameters.AddWithValue("@id_estado", inputs["id_estado"]);
                comando2.Parameters.AddWithValue("@cep", inputs["CEP"]);
                comando2.Parameters.AddWithValue("@login_usuario", inputs["LoginUsuario"].ToUpper());
                comando2.Parameters.AddWithValue("@login_senha", strBuilder.ToString());
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
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

        // GET: ConsultorUsuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ConsultorUsuario/Edit/5
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

        // GET: ConsultorUsuario/Delete/5
        //deleta no banco de dados e retorna pra index
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM consultor_usuario WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
            return RedirectToAction("Index");
        }

        // POST: ConsultorUsuario/Delete/5
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
