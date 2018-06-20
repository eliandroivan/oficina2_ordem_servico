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
    public class AberturaOSModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Situação")]
        public SituacaoModel Situacao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Tipo")]
        public TipoOSModel TipoOS { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Cliente")]
        public ClienteModel Cliente { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Veículo")]
        public VeiculoModel Veiculo { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Quilometragem de entrada")]
        public int QuilometragemEntrada { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Consultor Usuario")]
        public ConsultorUsuarioModel ConsultorUsuario { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Data e hora de abertura")]
        public DateTime Abertura { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Data e hora da previsao de entrega")]
        public DateTime PrevisaoEntrega { get; set; }

        

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Reclamação")]
        [DataType(DataType.MultilineText)]
        public string Reclamacao { get; set; }


        public string UltimoNumeroOS()
        {
            string query = "SELECT MAX(numero) AS numero FROM ordem_servico";
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string UltimoNumero = "0";
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    UltimoNumero = leitor.GetString("numero");                    
                }
                leitor.Close();
            }
            float valor = float.Parse(UltimoNumero);
            valor++;
            UltimoNumero = valor.ToString();

            return UltimoNumero;
        }

            public List<AberturaOSModel> ListarAberturaOS()
            {
            return ListarAberturaOS(0);
            }
            public List<AberturaOSModel> ListarAberturaOS(int id)
            {
                List<AberturaOSModel> listaAberturaOS = new List<AberturaOSModel>();
                StringBuilder query = new StringBuilder();

                query.AppendLine("select");
                query.AppendLine(" ordem_servico.id, ordem_servico.numero, situacao.descricao as situacao, tipo_os.descricao as tipo_os, modelo.descricao as modelo, veiculo.placa as placa,  ordem_servico.quilometragem_entrada, cliente.nome as cliente_nome, consultor_usuario.nome as consultor_usuario,  ordem_servico.abertura,  ordem_servico.previsao_entrega,  ordem_servico.reclamacao_cliente");
                query.AppendLine(" from ordem_servico");
                query.AppendLine(" inner join situacao on situacao.id=ordem_servico.id_situacao");
                query.AppendLine(" inner join tipo_os on tipo_os.id=ordem_servico.id_tipo_os");
                query.AppendLine(" inner join veiculo on veiculo.id=ordem_servico.id_veiculo");
                query.AppendLine(" inner join cliente on cliente.id=ordem_servico.id_cliente");
                query.AppendLine(" inner join consultor_usuario on consultor_usuario.id=ordem_servico.id_consultor_usuario");
    //            query.AppendLine(" inner join condicao_pagamento on condicao_pagamento.id=ordem_servico.id_condicao_pagamento");
                query.AppendLine(" inner join modelo on modelo.id=veiculo.id_modelo");
                query.AppendLine(" where ordem_servico.id_situacao = 1"); //LISTA SÓ O QUE ESTÁ ABERTO
                if (id > 0)
                {
                    query.AppendLine(" AND ordem_servico.id = ");
                    query.AppendLine(id.ToString());
                    query.AppendLine(" order by cliente_nome");
                }
                BandoDeDadosModel bd = new BandoDeDadosModel();
                MySqlConnection conexao = bd.ConexaoBD();

                using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
                {
                    conexao.Open();
                    MySqlDataReader leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        AberturaOSModel aberturaOS = new AberturaOSModel();
                        aberturaOS.Id = leitor.GetInt32("Id");
                        aberturaOS.Numero = leitor.GetString("numero");
                        aberturaOS.Situacao = new SituacaoModel();
                        aberturaOS.Situacao.Descricao = leitor.GetString("situacao");
                        aberturaOS.TipoOS = new TipoOSModel();
                        aberturaOS.TipoOS.Descricao = leitor.GetString("tipo_os");
                        aberturaOS.Veiculo = new VeiculoModel();
                        aberturaOS.Veiculo.Modelo = new ModeloModel();
                        aberturaOS.Veiculo.Modelo.Descricao = leitor.GetString("modelo");
                        aberturaOS.Veiculo.Placa = leitor.GetString("placa");
                        aberturaOS.QuilometragemEntrada = leitor.GetInt32("quilometragem_entrada");
                        aberturaOS.Cliente = new ClienteModel();
                        aberturaOS.Cliente.Nome = leitor.GetString("cliente_nome");
                        aberturaOS.ConsultorUsuario = new ConsultorUsuarioModel();
                        aberturaOS.ConsultorUsuario.Nome = leitor.GetString("consultor_usuario");
                        aberturaOS.Abertura = leitor.GetDateTime("abertura");
                        aberturaOS.PrevisaoEntrega = leitor.GetDateTime("previsao_entrega");
                        aberturaOS.Reclamacao = leitor.GetString("reclamacao_cliente");
                        /*
                        aberturaOS.Entrega = leitor.GetDateTime("entrega");
                        CapaOS.CondicaoPagamento = new CondicaoPagamentoModel();
                        CapaOS.CondicaoPagamento.Descricao = leitor.GetString("condicao_pagamento");
                        */
                        listaAberturaOS.Add(aberturaOS);
                    }
                    leitor.Close();
                }
                return listaAberturaOS;
            }
    }
}