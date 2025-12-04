<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="App_Web_Catalogo.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Mi Perfil</h3>
    <div class="row">
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
            </div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                <asp:RangeValidator ErrorMessage="Máximo 50 caracteres" ControlToValidate="txtNombre" runat="server" Type="Integer" MinimumValue="1" MaximumValue="50" CssClass="validacion"/>
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" ClientIDMode="Static" />
                <asp:RangeValidator ErrorMessage="Máximo 50 caracteres" ControlToValidate="txtApellido" runat="server" Type="Integer" MinimumValue="1" MaximumValue="50" CssClass="validacion"/>
                
                <%-- SOLO NUMEROS --%>
                <%-- ^[0-9]+$ --%>
                <%-- SOLO EMAIL --%>
                <%-- ^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$ --%>

            </div>          
        </div>
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Imagen Perfil</label>
                <input type="file" id="txtImagen" runat="server" class="form-control" />
            </div>
            <asp:Image ID="imgNuevoPerfil" ImageUrl="https://img.freepik.com/vector-premium/vector-icono-imagen-predeterminado-pagina-imagen-faltante-diseno-sitio-web-o-aplicacion-movil-no-hay-foto-disponible_87543-11093.jpg"
                runat="server" CssClass="img-fluid mb-3" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:Button Text="Guardar" runat="server" ID="btnGuardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" OnClientClick="return validar()" />
            <a href="/">Regresar</a>
        </div>
    </div>
</asp:Content>
