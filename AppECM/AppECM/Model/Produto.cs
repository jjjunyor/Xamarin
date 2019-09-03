using AppECM.Services.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppECM.Model
{
    public class Produto : BaseService
    {
        private IList<Produto> _produto;
        public Produto()
        {
            _produto = new List<Produto>();
        }
        public int id { get; set; }
        public string descricao { get; set; }
        public int qtdDisponivel { get; set; }
        public double PrecoUnitario { get; set; }
        public string PrecoUnitarioFormatado { get; set; }
    }
}
