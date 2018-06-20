using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace oficina2_ordem_servico.Models
{
    public class MarcaModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Marca")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo excedido")]
        public string Descricao { get; set; }

        public IEnumerable ListarMarca()
        {
            return ListarMarca(0);
        }

        public IEnumerable ListarMarca(int id)
        {
            List<MarcaModel> listaMarca = new List<MarcaModel>();
            string query = "select * from marca";

            if (id > 0)
            {
                query += " where id = " + id.ToString() + " order by descricao;";
            }


            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    MarcaModel marca = new MarcaModel();
                    marca.Id = leitor.GetInt32("id");
                    marca.Descricao = leitor.GetString("descricao");
                    listaMarca.Add(marca);
                }
                leitor.Close();
            }
            return listaMarca;

        }

    }
}