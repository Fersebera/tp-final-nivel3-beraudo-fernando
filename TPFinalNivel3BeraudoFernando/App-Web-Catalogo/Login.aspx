<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="App_Web_Catalogo.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
     .validacion {
         color: red;
         font-size: 15px;
     }
 </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-4">
            <h2>Login</h2>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                <asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtEmail" runat="server" CssClass="validacion" />
            </div>
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" />
                <asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtPassword" runat="server" CssClass="validacion" />
            </div>
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnIngresar_Click" />
            <a href="/">Cancelar</a>
        </div>
    </div>
</asp:Content>
