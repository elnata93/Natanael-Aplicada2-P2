using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;


namespace Natanael_Aplicada2_P2
{
    public partial class Registro_Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FechaTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
                CargarArticulos();
                FechaTextBox.Enabled = false;
                MontoTextBox.Enabled = false;
            }
        }

        private void CargarArticulos()
        {
            Ventas venta = new Ventas();
            ArticuloDropDownList.DataSource = venta.ListaArticulos(" * "," 1=1 "," ");
            ArticuloDropDownList.DataTextField = "Descripcion";
            ArticuloDropDownList.DataValueField = "ArticuloId";
            ArticuloDropDownList.DataBind();
        }

        private void LlenarCampos(Ventas venta)
        {
            IdTextBox.Text = venta.VentaId.ToString();
            FechaTextBox.Text = venta.Fecha;
            MontoTextBox.Text = venta.Monto.ToString();
            //foreach (GridViewRow item in VentaGridView.Rows)
            //{
            //    venta.AgregarArticulo(Convert.ToInt32(item.Cells[0]), Convert.ToInt32(item.Cells[1]), Convert.ToDecimal(item.Cells[2]));

            //}
            VentaGridView.DataSource = venta.DetalleVenta;
            VentaGridView.DataBind();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();
            if (IdTextBox.Text == "")
            {
                Mensajes("Introdusca el ID");
            }
            else
            if (Utilitarios.ConvertirToEntero(IdTextBox.Text) != 0)
            {
                if (venta.Buscar(Utilitarios.ConvertirToEntero(IdTextBox.Text)))
                {
                    LlenarCampos(venta);
                }
                else
                {
                    Mensajes("Id no exite");
                }
            }
            else
            {
                Mensajes("Id no encontrado");
            }
        }

        protected void AgregarButton_Click(object sender, EventArgs e)
        {
            Ventas venta;
            
            if (Session["venta"] == null)
            {
                Session["venta"] = new Ventas();
            }
            venta = (Ventas)Session["venta"];
            //if (CantidadTextBox.Text.Length == 0 || PrecioTextBox.Text.Length == 0)
            //{
            //    Mensajes("Llene los Campos Faltantes");
            //}
            
                venta.AgregarArticulo(Convert.ToInt32(ArticuloDropDownList.Text), Convert.ToInt32(CantidadTextBox.Text), Convert.ToInt32(PrecioTextBox.Text));

                VentaGridView.DataSource = venta.DetalleVenta;
                VentaGridView.DataBind();
            
                int canti = Utilitarios.ConvertirToEntero(PrecioTextBox.Text);
                int prec = Utilitarios.ConvertirToEntero(CantidadTextBox.Text);
                int monto = Utilitarios.ConvertirToEntero(MontoTextBox.Text);
                MontoTextBox.Text = ( monto + (prec * canti).ToString());
           
        }

        private void Limpiar(Ventas venta)
        {
            FechaTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
            IdTextBox.Text = string.Empty;
            FechaTextBox.Text = FechaTextBox.Text;
            MontoTextBox.Text = string.Empty;
            ArticuloDropDownList.SelectedIndex = 0;
            CantidadTextBox.Text = string.Empty;
            PrecioTextBox.Text = string.Empty;
            VentaGridView.DataSource = null;
            VentaGridView.DataBind();
            venta.LimpiarLista();
            Session["venta"] = new Ventas();
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();
            Limpiar(venta);
        }

        private void LlenarDatos(Ventas venta)
        {
            venta.Fecha = FechaTextBox.Text;
            venta.Monto = Convert.ToDecimal(MontoTextBox.Text);
            foreach(GridViewRow item in VentaGridView.Rows)
            {
                venta.AgregarArticulo(Utilitarios.ConvertirToEntero(item.Cells[1].Text), Utilitarios.ConvertirToEntero(item.Cells[2].Text), Utilitarios.ConvertirToDecimal(item.Cells[3].Text));
            }
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {

            Ventas venta = new Ventas();
            if (MontoTextBox.Text.Length == 0 || CantidadTextBox.Text.Length == 0 || PrecioTextBox.Text.Length == 0 || VentaGridView.Rows.Count == 0)
            {
                Mensajes("Complete los Campos");
            }else
            if (Utilitarios.ConvertirToEntero(IdTextBox.Text) == 0)
            {

                LlenarDatos(venta);
                if (venta.Insertar())
                {
                    Mensajes("Venta Guardada");
                    //Utilitarios.ShowToastr(this, " Ventas de Articulos ", " Venta Guardada ", " éxito ");
                }
                else
                {
                    Mensajes("Error al Guardar");
                }
                Limpiar(venta);
            }
            else
            if (Utilitarios.ConvertirToEntero(IdTextBox.Text) > 0)
            {
                if (venta.Buscar(Utilitarios.ConvertirToEntero(IdTextBox.Text)))
                {
                    LlenarDatos(venta);
                    if (venta.Editar())
                    {
                        Mensajes("Venta Editada");
                        
                    }
                    else
                    {
                        Mensajes("Error al Editar");
                    }
                }
                Limpiar(venta);
            }
        }

        private void Mensajes(string mensaje)
        {
            Response.Write("<script>alert('" + mensaje + "');</script>");
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();
            if (IdTextBox.Text.Length == 0)
            {
                Mensajes("Debe Ingresar el ID");
            }
            else
            if(venta.Buscar(Utilitarios.ConvertirToEntero(IdTextBox.Text)))
            {
                venta.Eliminar();
                Mensajes("Venta Eliminada");
                Limpiar(venta);
            }
            else
            {
                Mensajes("Error Venta no se Elimino");
                Limpiar(venta);
            }
        }
    }
}