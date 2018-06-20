using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oficina2_ordem_servico.Models
{
    public class InserirItem
    {
        public List<int>  id_os { get; set; }
        public List<string> Itens { get; set; }
        //public List<float> Preco { get; set; }
        public List<float> qtdItem { get; set; }
    }
}