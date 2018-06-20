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
    public class VeiculoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Cliente")]
        public ClienteModel Cliente { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Placa")]
        [MaxLength(8, ErrorMessage = "Tamanho máximo excedido")]
        [RegularExpression("[A-Z]{3}[0-9]{4}", ErrorMessage = "Deve ser da seguinte forma AAA0000")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Chassi")]
        [MaxLength(17, ErrorMessage = "Tamanho máximo excedido")]
        //[RegularExpression("[A-Z]{3}[0-9]{4}", ErrorMessage = "Deve ser da seguinte forma AAA0000")] Esta errado
        public string Chassi { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Marca")]
        public MarcaModel Marca { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Modelo")]
        public ModeloModel Modelo { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Ano de fabricação")]
        [MaxLength(4, ErrorMessage = "Tamanho máximo excedido")]
        public int AnoFabricacao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Ano do modelo")]
        [MaxLength(4, ErrorMessage = "Tamanho máximo excedido")]
        public int AnoModelo { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Cor")]
        [MaxLength(30, ErrorMessage = "Tamanho máximo excedido")]
        public string Cor { get; set; }

        public List<VeiculoModel> ListarVeiculo()
        {
            return ListarVeiculo(0);
        }
        //public static List<VeiculoModel> ListarVeiculo(int id)
        public List<VeiculoModel> ListarVeiculo(int id)
        {
            List<VeiculoModel> listaVeiculo = new List<VeiculoModel>();
            StringBuilder query = new StringBuilder();

            if (id > 0)
            {
                query.AppendLine("select");
                query.AppendLine(" veiculo.id, cliente.nome as cliente_nome, cliente.cpf_cnpj as cliente_cpf_cnpj, marca.descricao as marca, modelo.descricao as modelo, veiculo.chassi, veiculo.placa, veiculo.ano_fabricacao, veiculo.ano_modelo, veiculo.cor");
                query.AppendLine(" from veiculo");
                query.AppendLine(" inner join cliente on cliente.id=veiculo.id_cliente");
                query.AppendLine(" inner join marca on marca.id=veiculo.id_marca");
                query.AppendLine(" inner join modelo on modelo.id=veiculo.id_modelo");
                query.AppendLine(" where id_cliente = ");
                query.AppendLine(id.ToString());
                query.AppendLine(" order by cliente_nome");
            }
            else
            {
                query.AppendLine("select");
                query.AppendLine(" veiculo.id, cliente.nome as cliente_nome, cliente.cpf_cnpj as cliente_cpf_cnpj, marca.descricao as marca, modelo.descricao as modelo, veiculo.chassi, veiculo.placa, veiculo.ano_fabricacao, veiculo.ano_modelo, veiculo.cor");
                query.AppendLine(" from veiculo");
                query.AppendLine(" inner join cliente on cliente.id=veiculo.id_cliente");
                query.AppendLine(" inner join marca on marca.id=veiculo.id_marca");
                query.AppendLine(" inner join modelo on modelo.id=veiculo.id_modelo");
            }

            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    VeiculoModel veiculo = new VeiculoModel();

                    veiculo.Id = leitor.GetInt32("id");
                    veiculo.Cliente = new ClienteModel();
                    veiculo.Cliente.Nome = leitor.GetString("cliente_nome");
                    veiculo.Cliente.CpfCnpj = leitor.GetString("cliente_cpf_cnpj");
                    veiculo.Marca = new MarcaModel();
                    veiculo.Marca.Descricao = leitor.GetString("marca");
                    veiculo.Modelo = new ModeloModel();
                    veiculo.Modelo.Descricao = leitor.GetString("modelo");
                    veiculo.Chassi = leitor.GetString("chassi");
                    veiculo.Placa = leitor.GetString("placa");
                    veiculo.AnoFabricacao = leitor.GetInt32("ano_fabricacao");
                    veiculo.AnoModelo = leitor.GetInt32("ano_modelo");
                    veiculo.Cor = leitor.GetString("cor"); 
                    listaVeiculo.Add(veiculo);
                }
                leitor.Close();
            }

            
            return listaVeiculo;

        }
    }
}