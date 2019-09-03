using System;
using System.Collections.Generic;
using System.Text;

namespace AppECM.Model
{
    public class Pedido
    {
        
        public int idUsuario { get; set; }

        public int idProduto { get; set; }
        public int quantidade { get; set; }
        public int idPedido { get; set; }

        public string pedidoformatado { get; set; }
        public string login { get; set; }
        


    }
}
