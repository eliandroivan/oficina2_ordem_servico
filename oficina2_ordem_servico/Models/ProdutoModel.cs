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
    public class ProdutoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Tipo de produto")]
        public TipoProdutoModel TipoProduto { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Referência ")]
        [MaxLength(20, ErrorMessage = "Tamanho máximo excedido")]
        public string Referencia { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Descrição do produto ")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo excedido")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Unidade ")]
        [MaxLength(20, ErrorMessage = "Tamanho máximo excedido")]
        public string Unidade { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Quantidade ")]
        public float Quantidade { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Valor unitário ")]
        public float ValorUnitario { get; set; }

        public IEnumerable ListarProduto()
        {
            return ListarProduto(0);
        }
        public IEnumerable ListarProduto(int id)
        {
            List<ProdutoModel> listaProduto = new List<ProdutoModel>();
            StringBuilder query = new StringBuilder();
            query.AppendLine("select");
            query.AppendLine(" produto.id, tipo_produto.descricao as tipo_produto, produto.referencia, produto.descricao, produto.unidade, produto.quantidade,produto.valor_unitario");
            query.AppendLine(" from produto");
            query.AppendLine(" inner join tipo_produto on tipo_produto.id=produto.id_tipo_produto");

            if (id > 0)
            {
                query.AppendLine(" where id_tipo_produto = ");
                query.AppendLine(id.ToString());
                query.AppendLine(" order by descricao asc;");
            }

            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();

                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ProdutoModel produto = new ProdutoModel();
                    produto.Id = leitor.GetInt32("Id");
                    produto.TipoProduto = new TipoProdutoModel();
                    produto.TipoProduto.TipoProduto = leitor.GetString("tipo_produto");
                    produto.Referencia = leitor.GetString("referencia");
                    produto.Descricao = leitor.GetString("descricao");
                    produto.Unidade = leitor.GetString("unidade");
                    produto.Quantidade = leitor.GetFloat("quantidade");
                    produto.ValorUnitario = leitor.GetFloat("valor_unitario");
                    listaProduto.Add(produto);
                }
                leitor.Close();
            }

            return listaProduto;
        }
    }
}