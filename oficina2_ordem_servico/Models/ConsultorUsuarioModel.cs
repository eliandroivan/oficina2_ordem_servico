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
    public class ConsultorUsuarioModel : PessoaModel
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Login do usuário")]
        [MaxLength(50, ErrorMessage = "Tamanho máximo excedido")]
        public string LoginUsuario { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Senha do usuário")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo excedido")]
        [DataType(DataType.Password)]
        public string LoginSenha { get; set; }

        public IEnumerable ListarConsultorUsuario()
        {
            return ListarConsultorUsuario(0);
        }
        internal IEnumerable ListarConsultorUsuario(int id)
        {
            List<ConsultorUsuarioModel> listaConsultorUsuario = new List<ConsultorUsuarioModel>();
            StringBuilder query = new StringBuilder();
            if (id > 0)
            {
                query.AppendLine("select");
                query.AppendLine(" consultor_usuario.id, consultor_usuario.nome,  consultor_usuario.cpf_cnpj, consultor_usuario.email, consultor_usuario.telefone1, consultor_usuario.telefone2, consultor_usuario.logradouro, consultor_usuario.logradouro_numero, consultor_usuario.logradouro_complemento, consultor_usuario.bairro, cidade.nome as cidade, estado.sigla as estado, consultor_usuario.cep, consultor_usuario.login_usuario");
                query.AppendLine(" from consultor_usuario");
                query.AppendLine(" inner join cidade on cidade.id = consultor_usuario.id_cidade");
                query.AppendLine(" inner join estado on estado.id = consultor_usuario.id_estado");
                query.AppendLine(" where id = ");
                query.AppendLine(id.ToString());
                query.AppendLine(" order by cliente.nome");
            }
            else
            {
                query.AppendLine("select");
                query.AppendLine(" consultor_usuario.id, consultor_usuario.nome, consultor_usuario.cpf_cnpj, consultor_usuario.email, consultor_usuario.telefone1, consultor_usuario.telefone2, consultor_usuario.logradouro, consultor_usuario.logradouro_numero, consultor_usuario.logradouro_complemento, consultor_usuario.bairro, cidade.nome as cidade, estado.sigla as estado, consultor_usuario.cep, consultor_usuario.login_usuario");
                query.AppendLine(" from consultor_usuario");
                query.AppendLine(" inner join cidade on cidade.id = consultor_usuario.id_cidade");
                query.AppendLine(" inner join estado on estado.id = consultor_usuario.id_estado");
            }

            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ConsultorUsuarioModel consultorUsuario = new ConsultorUsuarioModel();
                    consultorUsuario.Id = leitor.GetInt32("id");
                    consultorUsuario.Nome = leitor.GetString("nome");
                    consultorUsuario.CpfCnpj = leitor.GetString("cpf_cnpj");
                    consultorUsuario.Email = leitor.GetString("email");
                    consultorUsuario.Telefone1 = leitor.GetString("telefone1");
                    consultorUsuario.Telefone2 = leitor.GetString("telefone2");
                    consultorUsuario.Logradouro = leitor.GetString("logradouro");
                    consultorUsuario.LogradouroNumero = leitor.GetString("logradouro_numero");
                    if (!leitor.IsDBNull(9))
                    {
                        consultorUsuario.LogradouroComplemento = null;
                    }
                    else
                    {
                        consultorUsuario.LogradouroComplemento = leitor.GetString("logradouro_complemento");
                    }
                    // dados NULL dá erro:::                        cliente.LogradouroComplemento = leitor.GetString("logradouro_complemento");
                    consultorUsuario.Bairro = leitor.GetString("bairro");
                    consultorUsuario.Cidade = new CidadeModel();
                    consultorUsuario.Cidade.Nome = leitor.GetString("cidade");
                    consultorUsuario.Estado = new EstadoModel();
                    consultorUsuario.Estado.Sigla = leitor.GetString("estado");
                    consultorUsuario.CEP = leitor.GetString("cep");
                    consultorUsuario.LoginUsuario = leitor.GetString("login_usuario");
                    //cliente.NomeCpfCnpj
                    listaConsultorUsuario.Add(consultorUsuario);
                }
                leitor.Close();
            }
            return listaConsultorUsuario;
        }

    }
}