using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace oficina2_ordem_servico.Models
{
    public class PessoaModel
    {
        [Key]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Você precisa entrar com o {0}")] 0 é o valor do display name
        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Nome")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo excedido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Número do CPF/CNPJ")]
        [CustomValidationCPFAttribute(ErrorMessage = "CPF inválido")]
        [MaxLength(14, ErrorMessage = "Tamanho máximo excedido")]
        public string CpfCnpj { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Insira um email válido")]
        public string Email { get; set; }

        [Display(Name = "Telefone 1")]
        [Required(ErrorMessage = "O campo é obrigatório")]
        [RegularExpression("[0-9]{11}", ErrorMessage = "Deve ser da seguinte forma (00)00000-0000, somente números")]
        public string Telefone1 { get; set; }

        [Display(Name = "Telefone 2")]
        [Required(ErrorMessage = "O campo é obrigatório")]
        [RegularExpression("[0-9]{11}", ErrorMessage = "Deve ser da seguinte forma (00)00000-0000, somente números")]
        public string Telefone2 { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Logradouro", Description = "Digite o nome da rua/avenida.")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo excedido")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Número", Description = "Digite o número do lote.")]
        [MaxLength(10, ErrorMessage = "Tamanho máximo excedido")]
        public string LogradouroNumero { get; set; }

        [Display(Name = "Complemento")]
        [MaxLength(20, ErrorMessage = "Tamanho máximo excedido")]
        public string LogradouroComplemento { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Bairro")]
        [MaxLength(40, ErrorMessage = "Tamanho máximo excedido")]
        public string Bairro { get; set; }

        //[Required(ErrorMessage = "O campo é obrigatório")]
        //[Display(Name = "Cidade")]
        public CidadeModel Cidade { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Cidade")]
        public int id_cidade { get; set; }

        //[Required(ErrorMessage = "O campo é obrigatório")]
        //[Display(Name = "Estado")]
       public EstadoModel Estado { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Estado")]
        public int id_estado { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [MaxLength(9, ErrorMessage = "Tamanho máximo excedido")]
        [RegularExpression("^[0-9]{5}-[0-9]{3}$")]
        public string CEP { get; set; }

        //public string NomeCpfCnpj { get { return string.Format("{0} - {1}", this.Nome, this.CpfCnpj); } }
    }
}