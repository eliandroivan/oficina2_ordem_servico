using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace oficina2_ordem_servico.Models
{
    public class TipoProdutoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Tipo do produto")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo excedido")]
        public string TipoProduto { get; set; }


        public IEnumerable ListarTipoProduto()
        {
            return ListarTipoProduto(0);
        }

        public IEnumerable ListarTipoProduto(int id)
        {
            List<TipoProdutoModel> listaTipoProduto = new List<TipoProdutoModel>();
            string query = "select * from tipo_produto";

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
                    TipoProdutoModel tipoProduto = new TipoProdutoModel();
                    tipoProduto.Id = leitor.GetInt32("id");
                    tipoProduto.TipoProduto = leitor.GetString("descricao");
                    listaTipoProduto.Add(tipoProduto);
                }
                leitor.Close();
            }
            return listaTipoProduto;

        }
    }
}