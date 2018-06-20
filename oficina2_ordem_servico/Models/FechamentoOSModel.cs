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
    public class FechamentoOSModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Ordem de serviço")]
        public AberturaOSModel AberturaOS { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Condição de pagamento")]
        public CondicaoPagamentoModel CondicaoPagamento { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Data e hora da entrega")]
        public DateTime Entrega { get; set; }

        [Display(Name = "Valor total da OS")]
        public float ValorTotal { get; set; }

        public static float ValorTotalOS(int IdOS)
        {
            float valorTotalOS=0;
            StringBuilder query = new StringBuilder();

            query.AppendLine("select");
            query.AppendLine(" SUM(os_produto.quantidade*produto.valor_unitario) as valor_total ");
            query.AppendLine(" from os_produto ");
            query.AppendLine(" inner join ordem_servico on ordem_servico.id=os_produto.id_ordem_servico");
            query.AppendLine(" inner join produto on produto.id=os_produto.id_produto");
            query.AppendLine(" where id_ordem_servico=");
            query.AppendLine(IdOS.ToString());
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();


            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                leitor.Read();
                if (!leitor.IsDBNull(0))
                {
                    valorTotalOS = leitor.GetFloat("valor_total");
                }
                
                leitor.Close();
            }
            return valorTotalOS;
        }

        public List<FechamentoOSModel> ListarFechamento()
        {
            return ListarFechamento(0);
        }

        public List<FechamentoOSModel> ListarFechamento(int id, string id_coluna="")
        {
            List<FechamentoOSModel> listaFechamentoOS = new List<FechamentoOSModel>();
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT ");
            query.AppendLine("ordem_servico.id, ordem_servico.numero, situacao.descricao AS situacao, cliente.nome, veiculo.placa, ordem_servico.entrega, condicao_pagamento.descricao AS pagamento, ordem_servico.valor_total ");
            query.AppendLine("FROM ordem_servico ");
            query.AppendLine("INNER JOIN situacao ON situacao.id=ordem_servico.id_situacao ");
            query.AppendLine("INNER JOIN veiculo ON veiculo.id=ordem_servico.id_veiculo ");
            query.AppendLine("INNER JOIN cliente ON cliente.id=ordem_servico.id_cliente ");
            query.AppendLine("LEFT JOIN condicao_pagamento ON condicao_pagamento.id=ordem_servico.id_condicao_pagamento ");
            query.AppendLine("WHERE ");
            query.AppendLine("(ordem_servico.id_situacao=2) OR (ordem_servico.id_situacao=3) ");
            if ((id>0) && (id_coluna == ""))
            {
                query.AppendLine("AND ordem_servico.id = ");
                query.AppendLine(id.ToString());
            }
            else if ((id > 0) && (id_coluna != ""))
            {
                query.AppendLine("AND ordem_servico.");
                query.AppendLine(id_coluna);
                query.AppendLine(" = ");
                query.AppendLine(id.ToString());
            }

            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    FechamentoOSModel fechamentoOS = new FechamentoOSModel();
                    fechamentoOS.Id = leitor.GetInt32("Id");
                    fechamentoOS.AberturaOS = new AberturaOSModel();
                    fechamentoOS.AberturaOS.Numero = leitor.GetString("numero");
                    fechamentoOS.AberturaOS.Situacao = new SituacaoModel();
                    fechamentoOS.AberturaOS.Situacao.Descricao = leitor.GetString("situacao");
                    fechamentoOS.AberturaOS.Cliente = new ClienteModel();
                    fechamentoOS.AberturaOS.Cliente.Nome = leitor.GetString("nome");
                    fechamentoOS.AberturaOS.Veiculo = new VeiculoModel();
                    fechamentoOS.AberturaOS.Veiculo.Placa = leitor.GetString("placa");
                    fechamentoOS.CondicaoPagamento = new CondicaoPagamentoModel();
                    if (fechamentoOS.AberturaOS.Situacao.Descricao.ToUpper() != "CANCELADA")
                    {
                        fechamentoOS.Entrega = leitor.GetDateTime("entrega");
                        fechamentoOS.CondicaoPagamento.Descricao = leitor.GetString("pagamento");
                        fechamentoOS.ValorTotal = leitor.GetFloat("valor_total");
                    }

                    
                    listaFechamentoOS.Add(fechamentoOS);
                }
                leitor.Close();
            }




            return listaFechamentoOS;
        }
    }
}