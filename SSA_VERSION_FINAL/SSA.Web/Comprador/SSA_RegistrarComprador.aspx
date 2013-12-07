<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_CompradorPublica.master" AutoEventWireup="false" CodeFile="SSA_RegistrarComprador.aspx.vb" Inherits="Comprador_SSA_RegistrarComprador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">
    <center>
        <br />
        <table style="width: 80%; text-align:left;" cellpadding="3" border="0">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Tipo de Persona:"></asp:Label></td>
                <td colspan="3">
                    <input id="rbtNatural" type="radio" runat="server" checked /><asp:Label ID="lbl0002" runat="server" Text="Natural"/>
                    <input id="rbtJuridica" type="radio" runat="server" /><asp:Label ID="lbl0001" runat="server" Text="Jurídica"/>
                    <asp:CustomValidator ID="cvTipoPersona" runat="server" Display="Dynamic" ErrorMessage="Seleccione un tipo de persona."
                        ValidationGroup="Registrar" ClientValidationFunction="fTipoPersona">(*)</asp:CustomValidator>
                    <asp:Button ID="btnEjecuta" runat="server" Text="[ejecuta segun persona]" style="display:none;" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Nombres / Razón Social:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNombres" runat="server" ControlToValidate="txtNombre"
                        Display="Dynamic" ErrorMessage="Ingrese un nombre." SetFocusOnError="True" ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator></td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Apellidos:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtApellido" runat="server" MaxLength="100"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revApellido" runat="server" ControlToValidate="txtApellido"
                        Display="Dynamic" ErrorMessage="Formato de apellido no válido, solo letras y espacios."
                        SetFocusOnError="True" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="cvApellidos" runat="server"
                        Display="Dynamic" ErrorMessage="Ingrese los apellidos." ValidationGroup="Registrar" ClientValidationFunction="fApellido">(*)</asp:CustomValidator></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="DNI:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtDNI" runat="server" MaxLength="8"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtDNI"
                        Display="Dynamic" ErrorMessage="Formato de DNI no válido." SetFocusOnError="True"
                        ValidationExpression="\d{8}" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="cvDNI" runat="server" ClientValidationFunction="fDNI" Display="Dynamic"
                        ErrorMessage="Ingrese un número de DNI." ValidationGroup="Registrar">(*)</asp:CustomValidator></td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="RUC:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtRUC" runat="server" MaxLength="11"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revRUC" runat="server" ControlToValidate="txtRUC"
                        Display="Dynamic" ErrorMessage="Formato de RUC no válido." SetFocusOnError="True"
                        ValidationExpression="\d{11}" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="cvRUC" runat="server" Display="Dynamic"
                        ErrorMessage="Ingrese un número de RUC." ValidationGroup="Registrar" ClientValidationFunction="fRUC">(*)</asp:CustomValidator></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Correo Principal:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCorreo" runat="server" MaxLength="50" style="text-transform:lowercase;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" ControlToValidate="txtCorreo"
                        Display="Dynamic" ErrorMessage="Ingrese un correo principal." SetFocusOnError="True"
                        ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revCorreo" runat="server" ControlToValidate="txtCorreo"
                        Display="Dynamic" ErrorMessage="Formato de correo principal no válido." SetFocusOnError="True"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator></td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Correo Secundario<br>(opcional):"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCorreo2" runat="server" MaxLength="50" style="text-transform:lowercase;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revCorreo2" runat="server" ControlToValidate="txtCorreo2"
                        Display="Dynamic" ErrorMessage="Formato de correo secundario no válido." SetFocusOnError="True"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Usuario:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" MaxLength="20" style="text-transform:lowercase;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario"
                        Display="Dynamic" ErrorMessage="Ingrese un nombre de usuario." SetFocusOnError="True"
                        ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revUsuario" runat="server" ControlToValidate="txtUsuario"
                        Display="Dynamic" ErrorMessage="Formato de nombre de usuario no válido, solo letras."
                        SetFocusOnError="True" ValidationExpression="[a-zA-Z]+" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator></td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Teléfono:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTelefono" runat="server" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono"
                        Display="Dynamic" ErrorMessage="Ingrese un teléfono." SetFocusOnError="True"
                        ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtTelefono"
                        Display="Dynamic" ErrorMessage="Formato de teléfono no válido." SetFocusOnError="True"
                        ValidationExpression="[1-9]([\d]|[\s])+" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Clave:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtClave" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txtClave"
                        Display="Dynamic" ErrorMessage="Ingrese una clave." SetFocusOnError="True" ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator></td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Confirmar Clave:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtConfirmar" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvConfirma" runat="server" ControlToValidate="txtConfirmar"
                        Display="Dynamic" ErrorMessage="Ingrese confirmación de clave." SetFocusOnError="True"
                        ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvClave" runat="server" Display="Dynamic" ErrorMessage="La confirmación de la clave no coincide."
                        ValidationGroup="Registrar" ClientValidationFunction="fConfirmarClave" ControlToValidate="txtConfirmar" EnableViewState="False">(*)</asp:CustomValidator></td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td colspan="4" style="text-align:center;">
                    <asp:Button ID="btnBeforeRegistro" runat="server" Text="Registrar" ValidationGroup="Registrar" />
                    &nbsp; <asp:Button
                        ID="btnCancelar" runat="server" Text="Cancelar" />
                    <asp:Button ID="btnAceptar" runat="server" Text="[Registrar Proxy]" style="display:none;" /></td>
            </tr>
        </table>
        <ajax:UpdatePanel ID="upProxy" runat="server">
            <ContentTemplate></ContentTemplate>
            <Triggers>
                <ajax:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                <ajax:AsyncPostBackTrigger ControlID="btnBeforeRegistro" EventName="Click" />
            </Triggers>
        </ajax:UpdatePanel>
        <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Registrar" />
    </center>
</asp:Content>

