using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleBrowser;
using System.Threading.Tasks;
using RobotCorreioEstudo.Models;
using System.Text.RegularExpressions;

namespace RobotCorreioEstudo.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "Cep Invalido !!"};
        }

        // GET api/values/5
        public IEnumerable<string> Get(string id)
        {
            Regex regex = new Regex(@"^\d{8}");

            Match match = regex.Match(id);
            if (match.Success)
            {
                var browser = new Browser();
                var cep = PostCepToCorreios("" + id);
                Endereco endereco = new Endereco();
                //   browser.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.10 (KHTML, like Gecko) Chrome/8.0.552.224 Safari/534.10";
                //   browser.Navigate("http://www.buscacep.correios.com.br/sistemas/buscacep/buscaEndereco.cfm");
                //   var cepfield = browser.Find(ElementType.TextField, FindBy.Id, "cep");
                //  cepfield.Value = "" + id;
                //  var btnSubmit = browser.Find("input", FindBy.Value, "Buscar");
                //  btnSubmit.Click();

                //  browser.Navigate("http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaEndereco.cfm");

                string html = cep.Result;
                browser.SetContent(html);
                var tabelaEndereco = browser.Select("td");
                int count = 0;
                if (tabelaEndereco.Exists)
                {
                    foreach (var iterator in tabelaEndereco)
                    {
                        if (count == 0)
                        {
                            endereco.rua = iterator.Value.Split('&').First();
                            count++;
                            continue;
                        }
                        if (count == 1)
                        {
                            endereco.bairro = iterator.Value.Split('&').First();
                            count++;
                            continue;
                        }
                        if (count == 2)
                        {
                            endereco.cidade = iterator.Value.Split('/').First();
                            endereco.uf = iterator.Value.Split('/').Last();
                            count++;
                            continue;
                        }
                        if (count == 3)
                        {
                            endereco.cep = iterator.Value;
                            count++;

                        }

                    }
                    return new string[] { endereco.rua, endereco.bairro, endereco.cidade, endereco.uf, endereco.cep };
                }
                else
                {
                    return new string[] {"Cep Invalido !!" };
                }
                
            }
            else return new String[] { "Cep Invalido !!" };
          
              
        }

        static Task<string> PostCepToCorreios(string cep)
        {
           HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
                {
                    { "cep", cep },
                   
                };

            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync("http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaEndereco.cfm", content);
            var resultContent =   response.Result;

            return  resultContent.Content.ReadAsStringAsync();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}