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
    public class ModeloModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Modelo")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo excedido")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Marca")]
        public MarcaModel Marca { get; set; }


        public IEnumerable ListarModelo()
        {
            return ListarModelo(0);
        }

        public IEnumerable ListarModelo(int id)
        {
            List<ModeloModel> listaModelo = new List<ModeloModel>();
            StringBuilder query = new StringBuilder();

            if (id > 0)
            {
                query.AppendLine("select");
                query.AppendLine(" modelo.id, modelo.descricao, marca.descricao as marca");
                query.AppendLine(" from modelo");
                query.AppendLine(" inner join marca on marca.id=modelo.id_marca");
                query.AppendLine(" where id_marca = ");
                query.AppendLine(id.ToString());
                query.AppendLine(" order by modelo.descricao");
            }
            else
            {
                query.AppendLine("select");
                query.AppendLine(" modelo.id, modelo.descricao, marca.descricao as marca");
                query.AppendLine(" from modelo");
                query.AppendLine(" inner join marca on marca.id=modelo.id_marca");
            }


            BandoDeDadosModel bd = new BandoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloModel modelo = new ModeloModel();
                    modelo.Id = leitor.GetInt32("id");
                    modelo.Descricao = leitor.GetString("descricao");
                    modelo.Marca = new MarcaModel();
                    //modelo.Marca.Id = leitor.GetInt32("id_marca");
                    modelo.Marca.Descricao = leitor.GetString("marca");
                    listaModelo.Add(modelo);
                    //listaModelo.OrderBy(p => p.Id);

                }
                leitor.Close();
            }
            return listaModelo;

        }
    }
}