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

        protected void BuscarButton_Click(object sender, EventArgs e)
        {

        }

        protected void AgregarButton_Click(object sender, EventArgs e)
        {
            DataTable data = (DataTable)Session["Ventas"];
            data.Rows.Add(ArticuloDropDownList.SelectedValue, CantidadTextBox.Text, PrecioTextBox.Text);
            Session["Ventas"] = data;
            VentaGridView.DataSource = data;
            VentaGridView.DataBind();

            //Ventas venta;
            //if (Session["venta"] == null)
            //{
            //    Session["venta"] = new Ventas();
            //}
            //venta = (Ventas)Session["venta"];
            //venta.AgregarArticulo(Convert.ToInt32(ArticuloDropDownList.Text), Convert.ToInt32(CantidadTextBox.Text), Convert.ToInt32(PrecioTextBox.Text));

            //VentaGridView.DataSource = venta.DetalleVenta;
            //VentaGridView.DataBind();
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {

        }
        private void LlenarDatos(Ventas venta)
        {
            venta.Fecha = FechaTextBox.Text;
            venta.Monto = Convert.ToDecimal(MontoTextBox.Text);
            foreach(GridViewRow item in VentaGridView.Rows)
            {
                venta.AgregarArticulo(Convert.ToInt32(item.Cells[0].Text),Convert.ToInt32(item.Cells[1].Text),Convert.ToDecimal(item.Cells[2]));
            }
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();
            if (Utilitarios.ConvertirToEntero(IdTextBox.Text) == 0)
            {

                LlenarDatos(venta);
                if (venta.Insertar())
                {
                    Mensajes("Venta Guardada");
                }
                else
                {
                    Mensajes("Error al Guardar");
                }
                //Limpiar();
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
                //Limpiar();
            }
        }

        private void Mensajes(string mensaje)
        {
            Response.Write("<script>alert('" + mensaje + "');</script>");
        }
        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();
            if (Utilitarios.ConvertirToEntero(IdTextBox.Text) == 0)
            {
                Mensajes("Debe Ingresar el ID");
            }
            else
                if (venta.Buscar(Utilitarios.ConvertirToEntero(IdTextBox.Text)))
            {
                venta.Eliminar();
                Mensajes("Articulo Eliminado");
                //Limpiar();
            }
            else
            {
                Mensajes("Error Articulo no se Elimino");
                //Limpiar();
            }
        }
    }
}