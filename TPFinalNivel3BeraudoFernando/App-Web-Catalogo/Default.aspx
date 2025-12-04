<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="App_Web_Catalogo.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label runat="server" Text="Filtrar" />
                <asp:TextBox runat="server" ID="txtFiltro" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" CssClass="form-control" />
            </div>
        </div>
        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
            <div class="mb-3">
                <asp:CheckBox Text="Filtro Avanzado" CssClass="" ID="chkAvanzadoHome" runat="server"
                    AutoPostBack="true" OnCheckedChanged="chkAvanzadoHome_CheckedChanged" />
            </div>
        </div>
    </div>

    <%if (chkAvanzadoHome.Checked)
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


    <%-- LINEA DE LISTADO --%>
    <br />

    <div class="form-check form-check-reverse">
        <asp:CheckBox CssClass="form-check-label" ID="chkLista" runat="server"
            AutoPostBack="true" OnCheckedChanged="chkLista_CheckedChanged" Text="☰" />
    </div>



    <%if (!chkLista.Checked)
        {%>

    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="repRepetidor" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src="<%#Eval("ImagenUrl")%>" class="d-flex justify-content-center align-items-center" alt="foto" style="height: auto; width: 50%">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <p class="card-text"><%#Eval("Brand")%></p>
                            <p class="card-text">$ <%#Eval("Precio")%></p>

                            <%if (negocio.Seguridad.sesionActiva(Session["trainee"]))
                                {%>
                            <asp:Button Text="Detalle" CssClass="btn btn-primary" ID="btnDetalle" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" OnClick="btnDetalle_Click" />
                            <asp:Button Text="⭐" CssClass="btn btn-outline-warning" ID="btnFavoritos" runat="server" OnClick="btnFavoritos_Click" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" />
                            <%} %>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <%}
        else
        { %>

    <%--    <ol class="list-group list-group-numbered">
        <asp:Repeater ID="repRepetidor2" runat="server">
            <ItemTemplate>
                <li class="list-group-item d-flex justify-content-between align-items-start">
                    <div class="ms-2 me-auto">
                        <div class="fw-bold"><%#Eval("Nombre")%></div>
                        <%#Eval("Brand")%>
                    </div>
                    <label>Precio: $ <%#Eval("Precio")%></label>
                    <asp:Button Text="Detalle" CssClass="btn btn-primary" ID="btnDetalle" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" OnClick="btnDetalle_Click" />
                    <asp:Button Text="⭐" CssClass="btn btn-outline-warning" ID="btnFavoritos" runat="server" OnClick="btnFavoritos_Click" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" />
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ol>--%>


    <table class="table">
        <thead>
            <tr>
                <th scope="col">Nombre</th>
                <th scope="col">Marca</th>
                <th scope="col">Precio</th>
                <%if (negocio.Seguridad.sesionActiva(Session["trainee"]))
                    {%>
                <th scope="col">Ver / Agregar Fav</th>
                <%} %>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repRepetidor2" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("Nombre")%></td>
                        <td><%#Eval("Brand")%></td>
                        <td><%#Eval("Precio")%></td>


                        <%if (negocio.Seguridad.sesionActiva(Session["trainee"]))
                            {%>
                        <td>
                            <asp:Button Text="Detalle" CssClass="btn btn-primary" ID="btnDetalle" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" OnClick="btnDetalle_Click" />
                            <asp:Button Text="⭐" CssClass="btn btn-outline-warning" ID="btnFavoritos" runat="server" OnClick="btnFavoritos_Click" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" /></td>
                        <%} %>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </tbody>
    </table>




    <%} %>

    <!-- Toast de Bootstrap (USE COPILOT! que desgracia) -->
    <div aria-live="polite" aria-atomic="true" class="position-relative">
        <div class="toast-container position-fixed bottom-0 end-0 p-3">
            <div id="toastFavorito" class="toast align-items-center text-bg-success border-0" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="3000">
                <div class="d-flex">
                    <div class="toast-body">
                        ✅ El artículo fue agregado a favoritos       
                    </div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Cerrar"></button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
