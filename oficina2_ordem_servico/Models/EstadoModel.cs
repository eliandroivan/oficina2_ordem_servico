using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace oficina2_ordem_servico.Models
{
    public class EstadoModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Estado")]
        [MaxLength(50, ErrorMessage = "Tamanho máximo excedido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Sigla")]
        [MaxLength(2, ErrorMessage = "Tamanho máximo excedido")]
        public string Sigla { get; set; }

        public IEnumerable ListarEstado()
        {
            return ListarEstado(0);
        }


        public IEnumerable ListarEstado(int id, int cidadeId = 0)
        {
            List<EstadoModel> listaEstado = new List<EstadoModel>();
            string query = "select * from estado";

            if (id > 0)
            {
                query += " where id = " + id.ToString() + " order by nome;";
            }

            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    EstadoModel estado = new EstadoModel();
                    estado.Id = leitor.GetInt32("Id");
                    estado.Nome = leitor.GetString("nome");
                    estado.Sigla = leitor.GetString("sigla");
                    listaEstado.Add(estado);
                }
                leitor.Close();
            }
            return listaEstado;
        }

    }
}