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
    public class ClienteModel : PessoaModel
    {
        public IEnumerable ListarCliente()
        {
            return ListarCliente(0);
        }
        internal IEnumerable ListarCliente(int id)
        {
            List<ClienteModel> listaCliente = new List<ClienteModel>();
            StringBuilder query = new StringBuilder();

            if (id > 0)
            {
                query.AppendLine("select");
                query.AppendLine(" cliente.id, cliente.nome, cliente.cpf_cnpj, cliente.email, cliente.telefone1, cliente.telefone2, cliente.logradouro, cliente.logradouro_numero, cliente.logradouro_complemento, cliente.bairro, cidade.nome as cidade, estado.sigla as estado, cliente.cep");
                query.AppendLine(" from cliente");
                query.AppendLine(" inner join cidade on cidade.id = cliente.id_cidade");
                query.AppendLine(" inner join estado on estado.id = cliente.id_estado");
                query.AppendLine(" where cliente.id = ");
                query.AppendLine(id.ToString());
                //query.AppendLine(" order by cliente.nome");
            }
            else
            {
                query.AppendLine("select");
                query.AppendLine(" cliente.id, cliente.nome, cliente.cpf_cnpj, cliente.email, cliente.telefone1, cliente.telefone2, cliente.logradouro, cliente.logradouro_numero, cliente.logradouro_complemento, cliente.bairro, cidade.nome as cidade, estado.sigla as estado, cliente.cep");
                query.AppendLine(" from cliente");
                query.AppendLine(" inner join cidade on cidade.id = cliente.id_cidade");
                query.AppendLine(" inner join estado on estado.id = cliente.id_estado");
            }
            //string query = "select cliente.id, cliente.nome, cliente.cpf_cnpj, cliente.email, cliente.logradouro, cliente.logradouro_numero, cliente.logradouro_complemento, cliente.bairro, cidade.nome as cidade, estado.sigla as estado, cliente.cep from cliente inner join cidade on cidade.id = cliente.id_cidade inner join estado on estado.id = cliente.id_estado order by cliente.nome asc;";
            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ClienteModel cliente = new ClienteModel();
                    cliente.Id = leitor.GetInt32("id");
                    cliente.Nome = leitor.GetString("nome");
                    cliente.CpfCnpj = leitor.GetString("cpf_cnpj");
                    cliente.Email = leitor.GetString("email");
                    cliente.Telefone1 = leitor.GetString("telefone1");
                    cliente.Telefone2 = leitor.GetString("telefone2");
                    cliente.Logradouro = leitor.GetString("logradouro");
                    cliente.LogradouroNumero = leitor.GetString("logradouro_numero");
                    if (!leitor.IsDBNull(9))
                    {
                        cliente.LogradouroComplemento = null;
                    }
                    else
                    {
                        cliente.LogradouroComplemento = leitor.GetString("logradouro_complemento");
                    }
                    // dados NULL dá erro:::                        cliente.LogradouroComplemento = leitor.GetString("logradouro_complemento");
                    cliente.Bairro = leitor.GetString("bairro");
                    cliente.Cidade = new CidadeModel();
                    cliente.Cidade.Nome = leitor.GetString("cidade");
                    cliente.Estado = new EstadoModel();
                    cliente.Estado.Sigla = leitor.GetString("estado");
                    cliente.CEP = leitor.GetString("cep");
                    //cliente.NomeCpfCnpj
                    listaCliente.Add(cliente);
                }
                leitor.Close();
            }
            return listaCliente;
        }
    }
}