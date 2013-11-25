<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_Principal.master" 
AutoEventWireup="false" CodeFile="SSA_NuevoArticulo.aspx.vb" 
Inherits="Articulos_SSA_NuevoArticulo" %>

<%@ Register Src="../Controles/Ctl_CargarImagen.ascx" TagName="Ctl_CargarImagen"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">
<center>
<table style="text-align:left;">
    <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Código:"></asp:Label></td><td>
            <asp:HiddenField ID="hidIdArticulo" runat="server" Value="0" />
        <asp:TextBox ID="txtCodArticulo" runat="server" MaxLength="6"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCodArticulo" runat="server" ControlToValidate="txtCodArticulo"
                Display="Dynamic" ErrorMessage="Ingrese el código de artículo." SetFocusOnError="True"
                ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revCodigo" runat="server" ControlToValidate="txtCodArticulo"
                Display="Dynamic" SetFocusOnError="True" ValidationExpression="[\d]+" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator>
            <asp:CompareValidator ID="cvCodigo" runat="server" ControlToValidate="txtCodArticulo"
                Display="Dynamic" ErrorMessage="El código de artículo debe ser mayor a 0." Operator="GreaterThan"
                SetFocusOnError="True" Type="Integer" ValidationGroup="Registrar" ValueToCompare="0">(*)</asp:CompareValidator></td><td>
        <asp:Label ID="Label2" runat="server" Text="Tipo Artículo:"></asp:Label></td><td>
        <asp:DropDownList ID="ddlTipoArticulo" runat="server" AutoPostBack="True">
        </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipoArticulo" runat="server" ControlToValidate="ddlTipoArticulo"
                Display="Dynamic" ErrorMessage="Seleccione un tipo de artículo." InitialValue="-1"
                SetFocusOnError="True" ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator></td><td>
        <asp:Label ID="Label3" runat="server" Text="SubTipo:"></asp:Label></td><td>
            <ajax:UpdatePanel ID="upSubTipo" runat="server">
                <ContentTemplate>
        <asp:DropDownList ID="ddlSubTipoArticulo" runat="server">
        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvSubTipoArticulo" runat="server" ControlToValidate="ddlSubTipoArticulo"
                        Display="Dynamic" ErrorMessage="Seleccione un subTipo de artículo." InitialValue="-1"
                        SetFocusOnError="True" ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="ddlTipoArticulo" EventName="SelectedIndexChanged" />
                    <ajax:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                </Triggers>
            </ajax:UpdatePanel>
        </td></tr>
    <tr><td style="height: 26px">
        <asp:Label ID="Label4" runat="server" Text="Marca:"></asp:Label></td><td style="height: 26px">
        <asp:TextBox ID="txtMarca" runat="server" MaxLength="50"></asp:TextBox></td><td style="height: 26px">
        <asp:Label ID="Label5" runat="server" Text="Modelo:"></asp:Label></td><td style="height: 26px">
        <asp:TextBox ID="txtModelo" runat="server" MaxLength="50"></asp:TextBox></td><td style="height: 26px">
        <asp:Label ID="Label6" runat="server" Text="Año:"></asp:Label></td><td style="height: 26px">
        <asp:TextBox ID="txtAnno" runat="server" MaxLength="4"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revAnio" runat="server" ControlToValidate="txtAnno"
                Display="Dynamic" ErrorMessage="Formato de año no válido (yyyy)." SetFocusOnError="True"
                ValidationExpression="^\d{4}$" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator>
            <asp:CompareValidator ID="cvAnno" runat="server" ControlToValidate="txtAnno" Display="Dynamic"
                ErrorMessage="El año ingresado no es válido." Operator="GreaterThan" SetFocusOnError="True"
                Type="Integer" ValidationGroup="Registrar" ValueToCompare="1900">(*)</asp:CompareValidator></td></tr>
    <tr>
        <td>
            <asp:Label ID="Label17" runat="server" Text="Descripción:"></asp:Label></td>
        <td colspan="5">
            <asp:TextBox ID="txtDescripcion" runat="server" Width="95%" MaxLength="300"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion"
                Display="Dynamic" ErrorMessage="Ingrese una descripción." SetFocusOnError="True"
                ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="Label7" runat="server" Text="Datos Técnicos:"></asp:Label></td>
    </tr>
    <tr><td colspan="6">
        <asp:TextBox ID="txtDatosTecnicos" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox></td></tr>
    <tr><td>
        <asp:Label ID="Label8" runat="server" Text="N° Siniestro:"></asp:Label></td><td>
        <asp:TextBox ID="txtNroSiniestro" runat="server" MaxLength="6"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revNroSiniestro" runat="server" ControlToValidate="txtNroSiniestro"
                Display="Dynamic" ErrorMessage="Formato de número de siniestro no válido." SetFocusOnError="True"
                ValidationExpression="[\d]+" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator>
            <asp:CompareValidator ID="cvNroSiniestro" runat="server" ControlToValidate="txtNroSiniestro"
                Display="Dynamic" ErrorMessage="El número de siniestro debe ser mayor a 0." Operator="GreaterThan"
                SetFocusOnError="True" Type="Integer" ValidationGroup="Registrar" ValueToCompare="0">(*)</asp:CompareValidator></td><td>
        <asp:Label ID="Label9" runat="server" Text="Precio Base:"></asp:Label></td><td>
        <asp:TextBox ID="txtPrecio" runat="server" MaxLength="12"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txtPrecio"
                Display="Dynamic" ErrorMessage="Ingrese el precio base." SetFocusOnError="True"
                ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPrecio" runat="server" ControlToValidate="txtPrecio"
                Display="Dynamic" ErrorMessage="Formato de precio base no váildo. (##.00)" SetFocusOnError="True"
                ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator>
            <asp:CompareValidator ID="cvPrecio" runat="server" ControlToValidate="txtPrecio"
                Display="Dynamic" ErrorMessage="El monto del precio base debe ser mayor a 0."
                Operator="GreaterThan" SetFocusOnError="True" Type="Double" ValidationGroup="Registrar"
                ValueToCompare="0">(*)</asp:CompareValidator></td><td>
        <asp:Label ID="Label10" runat="server" Text="Indemnización:"></asp:Label></td><td>
        <asp:TextBox ID="txtIndemnizacion" runat="server" MaxLength="12"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revIndemnizacion" runat="server" ControlToValidate="txtIndemnizacion"
                Display="Dynamic" ErrorMessage="Formato de indemnización no válido. (##.00)"
                SetFocusOnError="True" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator>
            <asp:CompareValidator ID="cvIndemnizacion" runat="server" ControlToValidate="txtIndemnizacion"
                Display="Dynamic" ErrorMessage="El monto de la indemnización debe ser mayor a 0."
                Operator="GreaterThan" SetFocusOnError="True" Type="Double" ValidationGroup="Registrar"
                ValueToCompare="0">(*)</asp:CompareValidator></td></tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="Label11" runat="server" Text="Datos del Siniestro:"></asp:Label></td>
    </tr>
    <tr><td colspan="6">
        <asp:TextBox ID="txtDatosSiniestro" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox></td></tr>
    <tr><td colspan="6">
        <asp:Label ID="Label12" runat="server" Text="Detalle del Siniestro:"></asp:Label></td></tr>
    <tr><td colspan="6">
        <asp:TextBox ID="txtDetalleSiniestro" runat="server" TextMode="MultiLine" Width="95%" Visible="False"></asp:TextBox></td></tr>
    <tr>
        <td colspan="2">
        <asp:Label ID="Label14" runat="server" Text="Foto 1:"></asp:Label><br />
        <uc1:Ctl_CargarImagen ID="Ctl_Foto1" runat="server" />
    </td><td colspan="2">
            <asp:Label ID="Label15" runat="server" Text="Foto 2:"></asp:Label><br />
            <uc1:Ctl_CargarImagen ID="Ctl_Foto2" runat="server" />
        </td><td colspan="2">
            <asp:Label ID="Label16" runat="server" Text="Foto 3:"></asp:Label><br />
                <uc1:Ctl_CargarImagen ID="Ctl_Foto3" runat="server" />
            </td></tr>
    <tr><td>
        <asp:Label ID="Label13" runat="server" Text="Estado"></asp:Label></td><td><asp:DropDownList ID="ddlEstado" runat="server" Enabled="False">
    </asp:DropDownList></td><td></td><td></td><td></td><td></td></tr>
    <tr>
        <td colspan="6" style="text-align:right;">
            <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="Registrar" />
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" ValidationGroup="Registrar" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" /></td>
    </tr>
</table>
</center>
</asp:Content>

