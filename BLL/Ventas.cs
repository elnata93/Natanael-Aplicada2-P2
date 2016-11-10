using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class Ventas : ClaseMaestra
    {
        ConexionDb conexion = new ConexionDb();
        public int VentaId { get; set; }
        public string Fecha { get; set; }
        public int ArticuloId { get; set; }
        public decimal Monto { get; set; }
        public List<VentasDetalle> DetalleVenta { get; set; }
        
        public Ventas()
        {
            this.Fecha = "";
            this.ArticuloId = 0;
            this.Monto = 0;
            this.DetalleVenta = new List<VentasDetalle>();
        }

        public Ventas(string fecha, int articuloId, decimal monto)
        {
            this.Fecha = fecha;
            this.ArticuloId = articuloId;
            this.Monto = monto;
        }


        public void AgregarArticulo(int articuloId, int cantidad, decimal precio)
        {
            DetalleVenta.Add(new VentasDetalle(articuloId, cantidad, precio));
        }

        public override bool Insertar()
        {
            int retorno = 0;
            Object identity;
            try
            {
                identity = conexion.Ejecutar(String.Format("insert into Ventas(Fecha,Monto) values('{0}',{1}') select @@Identity ", this.Fecha, this.Monto));

                int.TryParse(identity.ToString(), out retorno);
                this.VentaId = retorno;
                foreach (VentasDetalle item in DetalleVenta)
                {
                    conexion.Ejecutar(String.Format("insert into VentasDetalle(VentaId,ArticuloId,Cantidad,Precio) values({0},{1},{2},{3})", retorno, item.ArticuloId, item.Cantidad, item.Precio));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno > 0;
        }

        public override bool Editar()
        {

            bool retorno = false;
            try
            {
                retorno = conexion.Ejecutar(String.Format("update Ventas set Fecha='{0}',Monto={1} where VentaId={2}", this.Fecha, this.Monto, this.VentaId));
                if (retorno)
                {
                    conexion.Ejecutar(String.Format("delete from Ventas where VentaId={0}", this.VentaId));
                    foreach (VentasDetalle item in DetalleVenta)
                    {
                        conexion.Ejecutar(String.Format("insert into VentasDetalle(VentaId,ArticuloId,Cantidad,Precio) values({0},{1},{2},{3})", this.VentaId, item.ArticuloId, item.Cantidad, item.Precio));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return retorno;
        }


        public override bool Eliminar()
        {
            bool retorno = false;

            try
            {
                retorno = conexion.Ejecutar(String.Format("delete from VentasDetalles where VentaId={0}" + "delete from Ventas where VentaId={0}", this.VentaId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public override bool Buscar(int IdBuscado)
        {
            DataTable dt = new DataTable();
            DataTable data = new DataTable();
            try
            {
                dt = conexion.ObtenerDatos(String.Format("selec * from Ventas where VentaId=" + IdBuscado));
                if (dt.Rows.Count > 0)
                {
                    this.Fecha = dt.Rows[0]["Fecha"].ToString();
                    this.Monto = (decimal)dt.Rows[0]["Monto"];
                    data = conexion.ObtenerDatos(String.Format("select * from VentasDetalle where VentaId={0}", this.VentaId));
                    foreach (var item in data.Rows)
                    {
                        this.AgregarArticulo((int)data.Rows[0]["ArticuloId"], (int)data.Rows[0]["Cantidad"], (decimal)data.Rows[0]["Precio"]);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt.Rows.Count > 0;
        }

        public override DataTable Listado(string Campo, string Condicion, string Orden)
        {
            string OrdenFinal = "";
            if (Orden.Equals(""))
                OrdenFinal = "Order by" + Orden;
            return conexion.ObtenerDatos("select" + Campo + "from Ventas where" + Condicion + Orden);
        }

        public DataTable ListaArticulos(string Campo, string Condicion, string Orden)
        {
            string OrdenFinal = "";
            if (Orden.Equals(""))
                OrdenFinal = "Order by" + Orden;
            return conexion.ObtenerDatos("select" + Campo + "from Articulos where" + Condicion + Orden);
        }
    }
}
