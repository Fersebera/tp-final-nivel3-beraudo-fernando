<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ArticulosLista.aspx.cs" Inherits="App_Web_Catalogo.ArticulosLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Lista de Articulos</h1>

    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label runat="server" Text="Filtrar" />
                <asp:TextBox runat="server" ID="txtFiltro" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" CssClass="form-control" />
            </div>
        </div>
        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
            <div class="mb-3">
                <asp:CheckBox Text="Filtro Avanzado" CssClass="" ID="chkAvanzado" runat="server"
                    AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" />
            </div>
        </div>
    </div>


    <%if (chkAvanzado.Checked)
        { %>
    <div class="row align-items-center">
        <div class="col-3">
            <div class="mb-3">
                <asp:Label runat="server" Text="Campo" ID="lblCampo" />
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCampo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Nombre" />
                    <asp:ListItem Text="Marca" />
                    <asp:ListItem Text="Precio" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label runat="server" Text="Criterio" />
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCriterio"></asp:DropDownList>
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label runat="server" Text="Filtro" />
                <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
            </div>
        </div>
        <div class="col-3 d-flex align-items-center">
            <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary w-100" ID="btnBuscar" OnClick="btnBuscar_Click" />
        </div>
    </div>
    <%} %>

    <asp:GridView ID="dgvArticulos" runat="server" DataKeyNames="Id"
        CssClass="table" AutoGenerateColumns="false"
        OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
        OnPageIndexChanging="dgvArticulos_PageIndexChanging"
        AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Marca" DataField="Brand.Descripcion" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✏️" />
        </Columns>
    </asp:GridView>
    <a href="FormularioArticulo.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
