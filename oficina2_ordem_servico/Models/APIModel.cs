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
    public class APIModel
    {
        public string Numero { get; set; }
        public string Situacao { get; set; }
        public string TipoOS { get; set; }
        public string Placa { get; set; }
        public DateTime Abertura { get; set; }
        public float ValorTotal { get; set; }


        public List<APIModel> ListaAPI()
        {
            List<APIModel> listaAPI = new List<APIModel>();
            StringBuilder query = new StringBuilder();        
            query.AppendLine("SELECT ");
            query.AppendLine("ordem_servico.numero, situacao.descricao AS situacao, tipo_os.descricao AS tipo_os, veiculo.placa AS placa, ordem_servico.abertura, ordem_servico.valor_total ");
            query.AppendLine("FROM ordem_servico ");
            query.AppendLine("INNER JOIN veiculo ON veiculo.id=ordem_servico.id_veiculo ");
            query.AppendLine("INNER JOIN tipo_os on tipo_os.id=ordem_servico.id_tipo_os ");
            query.AppendLine("INNER JOIN situacao ON situacao.id=ordem_servico.id_situacao ");

            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    APIModel item = new APIModel();
                    item.Numero = leitor.GetString("numero");
                    item.Situacao = leitor.GetString("situacao");
                    item.TipoOS = leitor.GetString("tipo_os");
                    item.Placa = leitor.GetString("placa");
                    item.Abertura = leitor.GetDateTime("abertura");
                    if (leitor.IsDBNull(5))
                    {
                        item.ValorTotal = 0;
                    }
                    else
                    {
                        item.ValorTotal = leitor.GetFloat("valor_total");
                    }
                    
                    listaAPI.Add(item);
                }
                leitor.Close();
            }
            return listaAPI;
        }
    }
}