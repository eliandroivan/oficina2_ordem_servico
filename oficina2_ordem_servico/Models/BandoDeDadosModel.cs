using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections;

namespace oficina2_ordem_servico.Models
{
    public class BandoDeDadosModel
    {
        public MySqlConnection ConexaoBD()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "localhost";
            conn_string.UserID = "root";
            conn_string.Password = "";
            conn_string.Database = "oficina2_ordem_servico";
            conn_string.SslMode = MySqlSslMode.None;

            return new MySqlConnection(conn_string.ToString());
        }
    }
}