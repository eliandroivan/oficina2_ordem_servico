using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace oficina2_ordem_servico.Models
{
    public class CondicaoPagamentoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Condição de Pagamento")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo excedido")]
        public string Descricao { get; set; }



        public IEnumerable ListarCondicaoPagamento()
        {
            return ListarCondicaoPagamento(0);
        }

        public IEnumerable ListarCondicaoPagamento(int id)
        {
            List<CondicaoPagamentoModel> listaCondicaoPagamento = new List<CondicaoPagamentoModel>();
            string query = "select * from condicao_pagamento";

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
                    CondicaoPagamentoModel condicaoPagamento = new CondicaoPagamentoModel();
                    condicaoPagamento.Id = leitor.GetInt32("id");
                    condicaoPagamento.Descricao = leitor.GetString("descricao");
                    listaCondicaoPagamento.Add(condicaoPagamento);
                }
                leitor.Close();
            }
            return listaCondicaoPagamento;

        }
    }
}