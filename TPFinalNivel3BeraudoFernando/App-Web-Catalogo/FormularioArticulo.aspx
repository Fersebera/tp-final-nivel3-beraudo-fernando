<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="App_Web_Catalogo.FormularioArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style>
       .validacion {
           color: red;
           font-size: 15px;
       }
   </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox ID="txtId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtCodigo" class="form-label">Código</label>
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtCodigo" runat="server" CssClass="validacion" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtNombre" runat="server" cssClass="validacion" />
            </div>
            <div class="mb-3">
                <label for="txtMarca" class="form-label">Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="txtCategoria" class="form-label">Categoría</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="txtPrecio" class="form-label">Precio</label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ErrorMessage="Sólo números" ControlToValidate="txtPrecio" runat="server" CssClass="validacion" ValidationExpression="^\d+(,\d{1,4})?$" />
            </div>
            <div class="mb-3">
                <asp:Button runat="server" Text="Aceptar" ID="btnAceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                <a href="ArticulosLista.aspx">Cancelar</a>
            </div>
        </div>

        <div class="col-6">
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtUrlImg" class="form-label">URL de Imagen</label>
                        <asp:TextBox ID="txtUrlImg" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUrlImg_TextChanged"></asp:TextBox>
                    </div>
                    <asp:Image ImageUrl="https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg" runat="server" ID="imgArticulo" Width="60%" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="row">
        <div class="col-6">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <asp:Button runat="server" Text="Eliminar" ID="btnEliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
                    </div>
                                      

                    <%if (OkEliminacion)
                        { %>
                    <div class="mb-3">
                        <asp:CheckBox runat="server" Text="Confirmar Eliminación" ID="chkConfirmaEliminacion" />
                        <asp:Button runat="server" Text="Eliminar" ID="btnConfirmaEliminar" CssClass="btn btn-outline-danger" OnClick="btnConfirmaEliminar_Click" />
                    </div>
                    <%} %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
