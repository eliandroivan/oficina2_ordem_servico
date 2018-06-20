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
    public class CidadeModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Cidade")]
        [MaxLength(70, ErrorMessage = "Tamanho máximo excedido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Estado")]
        public EstadoModel Estado { get; set; }

        public IEnumerable ListarCidade()
        {
            return ListarCidade(0, "tudo");
        }

        public IEnumerable ListarCidade(int id_consulta, string id_coluna="")
        {
            List<CidadeModel> listaCidade = new List<CidadeModel>();
            StringBuilder query = new StringBuilder();
            query.AppendLine("select");
            query.AppendLine(" cidade.id, cidade.nome, cidade.id_estado, estado.sigla as estado_sigla");
            query.AppendLine(" from cidade");
            query.AppendLine(" inner join estado on estado.id = cidade.id_estado");
            if (id_consulta == 0)
            {
                query.AppendLine(" order by cidade.id asc");
            }
            else
            {
                query.AppendLine(" where ");
                query.AppendLine(id_coluna);
                query.AppendLine(" = ");
                query.AppendLine(id_consulta.ToString());
                query.AppendLine(" order by cidade.nome asc");
            }

            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();

                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    CidadeModel cidade = new CidadeModel();
                    cidade.Id = leitor.GetInt32("Id");
                    cidade.Nome = leitor.GetString("nome");
                    cidade.Estado = new EstadoModel();
                    if (id_consulta > 0)
                    {
                        cidade.Estado.Id = leitor.GetInt32("id_estado");
                    }
                    else
                    {
                        cidade.Estado.Sigla = leitor.GetString("estado_sigla");
                    }
                    listaCidade.Add(cidade);
                }
                leitor.Close();
            }

            return listaCidade;
        }
    }
}