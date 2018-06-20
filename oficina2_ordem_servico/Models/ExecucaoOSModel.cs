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
    public class ExecucaoOSModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Ordem de serviço")]
        public AberturaOSModel AberturaOS { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Produto")]
        public ProdutoModel Produto { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Quantidade")]
        public float Quantidade { get; set; }

        

        public IEnumerable ListarExecucaoOS()
        {
            return ListarExecucaoOS(0);
        }
        public IEnumerable ListarExecucaoOS(int id)
        {
            List<ExecucaoOSModel> listaExecucaoOS = new List<ExecucaoOSModel>();
            StringBuilder query = new StringBuilder();

            query.AppendLine("select");
            query.AppendLine(" os_produto.id, ordem_servico.numero as numero, tipo_produto.descricao as tipo_produto, produto.descricao as produto_descricao, os_produto.quantidade, produto.valor_unitario as valor_unitario ");
            query.AppendLine(" from os_produto ");
            query.AppendLine(" inner join ordem_servico on ordem_servico.id=os_produto.id_ordem_servico");
            query.AppendLine(" inner join produto on produto.id=os_produto.id_produto");
            query.AppendLine(" inner join tipo_produto on tipo_produto.id=produto.id_tipo_produto");
            if (id > 0)
            {
                query.AppendLine(" where os_produto.id_ordem_servico = ");
                query.AppendLine(id.ToString());
                query.AppendLine(" order by os_produto.id");
            }
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ExecucaoOSModel execucaoOS = new ExecucaoOSModel();
                    execucaoOS.Id = leitor.GetInt32("Id");
                    execucaoOS.AberturaOS = new AberturaOSModel();
                    execucaoOS.AberturaOS.Numero = leitor.GetString("numero");
                    execucaoOS.Produto = new ProdutoModel();
                    execucaoOS.Produto.TipoProduto = new TipoProdutoModel();
                    execucaoOS.Produto.TipoProduto.TipoProduto = leitor.GetString("tipo_produto");
                    execucaoOS.Produto.Descricao = leitor.GetString("produto_descricao");
                    execucaoOS.Quantidade = leitor.GetFloat("quantidade");
                    execucaoOS.Produto.ValorUnitario = leitor.GetFloat("valor_unitario");
                    listaExecucaoOS.Add(execucaoOS);
                }
                leitor.Close();
            }
            return listaExecucaoOS;
        }
    }
}