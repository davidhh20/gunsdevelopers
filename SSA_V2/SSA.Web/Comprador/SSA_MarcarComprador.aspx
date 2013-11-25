<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_Principal.master" AutoEventWireup="false" CodeFile="SSA_MarcarComprador.aspx.vb" Inherits="Administrador_SSA_MarcarComprador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">
<table width="100%">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Tipo Documento:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlTipoDocumento" runat="server">
                <asp:ListItem Selected="True" Value="-1">--Todos--</asp:ListItem>
                <asp:ListItem Value="0">DNI</asp:ListItem>
                <asp:ListItem Value="1">RUC</asp:ListItem>
            </asp:DropDownList></td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Documento:"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtDocumento" runat="server" MaxLength="11"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDocumento" runat="server" ControlToValidate="txtDocumento"
                Display="Dynamic" ErrorMessage="Formato de Documento no Válido." SetFocusOnError="True"
                ValidationExpression="[\d]+" ValidationGroup="Buscar">(*)</asp:RegularExpressionValidator>
            <asp:CustomValidator ID="cvDocumento" runat="server" ClientValidationFunction="fDocumento"
                Display="Dynamic" ErrorMessage="Ingrese un número de documento válido." ValidationGroup="Buscar">(*)</asp:CustomValidator></td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Código Usuario:"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtCodUsuario" runat="server" MaxLength="6"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revCodUsuario" runat="server" ControlToValidate="txtCodUsuario"
                ErrorMessage="El código de usuario debe ser dígitos." SetFocusOnError="True" ValidationExpression="[\d]+"
                ValidationGroup="Buscar">(*)</asp:RegularExpressionValidator></td>
        <td style="text-align:right"><asp:Button ID="btnBuscar" runat="server" Text="Buscar" ValidationGroup="Buscar" />
            <asp:Button ID="btnGrabar" runat="server" Text="Marcar" style="display:none;" />
            <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="Buscar" />
            <asp:HiddenField ID="hidLongDocumento" runat="server" Value="0" />
            <asp:HiddenField ID="hidTipoMarca" runat="server" Value="0" />
            <asp:HiddenField ID="hidIdUsuario" runat="server" Value="0" />
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td colspan="7">
            <ajax:UpdatePanel ID="upLista" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grvUsuario" runat="server" style="width:100%"  AutoGenerateColumns="False" 
                        OnRowDataBound="grvUsuario_RowDataBound">
                        <Columns>
                            <QNET:RowSelectionField SelectionMode="Single">
                                <itemstyle width="5px" />
                            </QNET:RowSelectionField>
                            <asp:BoundField DataField="id_usuario" HeaderText="Código" />
                            <asp:BoundField DataField="des_TipoPersona" HeaderText="Tipo Persona" />
                            <asp:BoundField DataField="NombreApellido" HeaderText="Nombres / Razon Social" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="nruc" HeaderText="RUC" />
                            <asp:BoundField DataField="ndni" HeaderText="DNI" />
                            <asp:TemplateField HeaderText="Marcar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMarca" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkModificar" runat="server" ImageUrl="~/Imagenes/i.p.modificar.gif"></asp:HyperLink>
                                    <asp:HiddenField ID="hidNom" runat="server" />
                                    <asp:HiddenField ID="hidApe" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>                        
                    </asp:GridView>
                    <QNET:DataNavigator ID="dnvListado" runat="server" AllowCustomPaging="true" WidthTextBoxNumPagina="25px"
                        Width="100%" GridViewId="grvUsuario" ButtonText="Ir" PageIndicatorFormat="Página {0} / {1}" Visible="false" />
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                </Triggers>
            </ajax:UpdatePanel>
            &nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="7" style="text-align:right; height: 12px;">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" style="display:none;" />
            <asp:Button ID="btnOpenDetalle" runat="server" Text="[Open Detalle]" style="display:none;" /></td>
    </tr>
</table>


<!--inicio modificar datos de personal-->
<asp:Panel ID="pnlDetalle" CssClass="cssTablaPanel" runat="server" style="display:none">
        <table style="width: 80%; text-align:left;" cellpadding="3" border="0">
            <tr>
                <th colspan="4"><asp:Label ID="Label11" runat="server" Text="DATOS DEL COMPRADOR"/></th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Tipo de Persona:"></asp:Label></td>
                <td colspan="3">
              <input id="rbtNatural" type="radio" runat="server"  /><asp:Label ID="lbl0002" runat="server" Text="Natural"/>
                    <input id="rbtJuridica" type="radio" runat="server" /><asp:Label ID="lbl0001" runat="server" Text="Jurídica"/>
                    <asp:CustomValidator ID="cvTipoPersona" runat="server" Display="Dynamic" ErrorMessage="Seleccione un tipo de persona."
                        ValidationGroup="Registrar" ClientValidationFunction="fTipoPersona">(*)</asp:CustomValidator>
                    <asp:Button ID="btnEjecuta" runat="server" Text="[ejecuta segun persona]" style="display:none;" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Nombres / Razón Social:"></asp:Label></td>
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
                    <asp:Label ID="Label6" runat="server" Text="DNI:"></asp:Label></td>
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
                    <asp:Label ID="Label9" runat="server" Text="Correo Principal:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCorreo" runat="server" MaxLength="50" style="text-transform:lowercase;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" ControlToValidate="txtCorreo"
                        Display="Dynamic" ErrorMessage="Ingrese un correo principal." SetFocusOnError="True"
                        ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revCorreo" runat="server" ControlToValidate="txtCorreo"
                        Display="Dynamic" ErrorMessage="Formato de correo principal no válido." SetFocusOnError="True"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator></td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Correo Secundario<br>(opcional):"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCorreo2" runat="server" MaxLength="50" style="text-transform:lowercase;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revCorreo2" runat="server" ControlToValidate="txtCorreo2"
                        Display="Dynamic" ErrorMessage="Formato de correo secundario no válido." SetFocusOnError="True"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Teléfono:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTelefono" runat="server" MaxLength="30"></asp:TextBox><asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono"
                        Display="Dynamic" ErrorMessage="Ingrese un teléfono." SetFocusOnError="True"
                        ValidationGroup="Registrar">(*)</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtTelefono"
                        Display="Dynamic" ErrorMessage="Formato de teléfono no válido." SetFocusOnError="True"
                        ValidationExpression="[1-9]([\d]|[\s])+" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator></td>
                <td>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center;">
                    <asp:Button ID="btnBeforeRegistro" runat="server" Text="Registrar" ValidationGroup="Registrar" />
                    <asp:Button ID="btnCerrarDetalle" runat="server" Text="Cancelar" />
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
        <asp:ValidationSummary ID="vs01" runat="server" DisplayMode="List" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Registrar" />
</asp:Panel>
 <ajaxToolkit:ModalPopupExtender ID="mpeDetalle" runat="server" 
    BackgroundCssClass="modalBackground"
    CancelControlID="btnCerrarDetalle" 
    PopupControlID="pnlDetalle" 
    TargetControlID="btnOpenDetalle">
</ajaxToolkit:ModalPopupExtender>

<!--fin modificar datos de personal-->
</asp:Content>