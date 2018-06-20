using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace oficina2_ordem_servico.Models
{
    public class SituacaoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Descrição da situação")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo excedido")]
        public string Descricao { get; set; }

        public IEnumerable ListarSituacao()
        {
            return ListarSituacao(0);
        }

        public IEnumerable ListarSituacao(int id)
        {
            List<SituacaoModel> listaSituacao = new List<SituacaoModel>();
            string query = "select * from situacao";

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
                    SituacaoModel situacao = new SituacaoModel();
                    situacao.Id = leitor.GetInt32("id");
                    situacao.Descricao = leitor.GetString("descricao");
                    listaSituacao.Add(situacao);
                }
                leitor.Close();
            }
            return listaSituacao;

        }
    }
}