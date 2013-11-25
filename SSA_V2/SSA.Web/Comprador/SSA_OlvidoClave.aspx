<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_CompradorPublica.master" AutoEventWireup="false" CodeFile="SSA_OlvidoClave.aspx.vb" Inherits="Comprador_SSA_OlvidoClave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">
<center>
<table style="text-align:left" cellpadding="4" cellspacing="0" border="0" align="center">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Usuario:"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario"
                Display="Dynamic" ErrorMessage="Ingrese el usuario.">(*)</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Correo:"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" ControlToValidate="txtCorreo"
                Display="Dynamic" ErrorMessage="Ingrese el correo." SetFocusOnError="True">(*)</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revCorreo" runat="server" Display="Dynamic" ErrorMessage="Formato de correo no válido."
                SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtCorreo">(*)</asp:RegularExpressionValidator></td>
    </tr>
    <tr>
    <tr>
        <td colspan="2" style="text-align:center">
            <asp:Button ID="btnAceptar" runat="server" Text="[Aceptar Proxy]" style="display:none;" />
            <asp:Button ID="btnBeforeRegistro" runat="server" Text="Aceptar" ValidationGroup="Registrar" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
            <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="True"
                ShowSummary="False" />
        </td>
    </tr>
</table>
    <ajax:UpdatePanel ID="upProxy" runat="server">
        <Triggers>
            <ajax:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
            <ajax:AsyncPostBackTrigger ControlID="btnBeforeRegistro" EventName="Click" />
        </Triggers>
    </ajax:UpdatePanel>
</center>
</asp:Content>

