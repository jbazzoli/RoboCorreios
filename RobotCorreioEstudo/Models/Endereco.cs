using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RobotCorreioEstudo.Models
{
    public class Endereco 
    {
       

        public string rua {set; get;}
        public string bairro { set; get; }
        public string cidade { set; get; }
        public string uf { set; get; }
        public string cep { set; get; }

    }
}
