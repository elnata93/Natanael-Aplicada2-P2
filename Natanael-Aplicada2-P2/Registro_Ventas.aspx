<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro_Ventas.aspx.cs" Inherits="Natanael_Aplicada2_P2.Registro_Ventas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="panel panel-success">
            <div class="panel-heading">Registro de Ventas</div>
                <div class="panel-body">
                    <div class="form-horizontal col-md-12" role="form">
        
                        <asp:Label ID="Label1" text="ID:" runat="server"></asp:Label>
				        <asp:TextBox ID="IdTextBox" runat="server"></asp:TextBox>
				        <asp:Button ID="BuscarButton" runat="server" CssClass="btn btn-success" text="Buscar" OnClick="BuscarButton_Click" />

				        <asp:Label ID="Laabel2" text="Fecha" runat="server"></asp:Label>
				        <asp:TextBox ID="FechaTextBox" runat="server"></asp:TextBox>
				        <asp:Label ID="Label3" text="Moton" runat="server"></asp:Label>
				        <asp:TextBox ID="MontoTextBox" runat="server"></asp:TextBox>

				        <asp:Label ID="label4" text="Articulo:" runat="server"></asp:Label>
				        <asp:DropDownList ID="ArticuloDropDownList" runat="server"></asp:DropDownList>
				        <asp:Label ID="Label5" text="Cantidad" runat="server"></asp:Label>
				        <asp:TextBox ID="CantidadTextBox" runat="server"></asp:TextBox>
				        <asp:Label ID="Label2" text="Precio" runat="server"></asp:Label>
				        <asp:TextBox ID="PrecioTextBox" runat="server"></asp:TextBox>
                        <asp:Button ID="AgregarButton" CssClass="btn btn-warning" runat="server" Text="Agregar" OnClick="AgregarButton_Click" />
                        <asp:GridView ID="VentaGridView" runat="server" Width="100%">
                        </asp:GridView>

				        <asp:Button ID="NuevoButton" Cssclass="btn btn-primary" runat="server" text="Nuevo" OnClick="NuevoButton_Click" />
				        <asp:Button ID="GuardarButton" CssClass="btn btn-success" runat="server" text="Guardar" OnClick="GuardarButton_Click" />
				        <asp:Button ID="EliminarButton" CssClass="btn btn-danger" runat="server" text="Eliminar" OnClick="EliminarButton_Click" />
                        
                </div>   
            </div>
        </div>
    </div>   
</asp:Content>
