using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class VentasDetalle
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        public VentasDetalle()
        {
            this.ArticuloId = 0;
            this.Cantidad = 0;
            this.Precio = 0;
        }

        public VentasDetalle(int articulo, int cantida, decimal precio)
        {
            this.ArticuloId = articulo;
            this.Cantidad = cantida;
            this.Precio = precio;
        }
    }
}
