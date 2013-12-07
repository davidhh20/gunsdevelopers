<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_Principal.master" AutoEventWireup="false" CodeFile="SSA_Parametros.aspx.vb" Inherits="Mantenimiento_SSA_Parametros"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">
<center>
<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Grupo:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlFiltroGrupo" runat="server" Width="250px">
            </asp:DropDownList></td>
        <td>
            <asp:Label ID="lb1" runat="server" Text="Descripción:"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtTexto" runat="server" MaxLength="100"></asp:TextBox></td>
        <td style="text-align:right;">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />&nbsp;
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" /></td>
    </tr>
    <tr>
        <td colspan="5">
            <ajax:UpdatePanel ID="upLista" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grvLista" runat="server" AutoGenerateColumns="false"
                        Width="700px" OnRowDataBound="grvLista_RowDataBound">
                        <Columns>
                            <QNET:RowSelectionField SelectionMode="Single">
                                <itemstyle width="5px" />
                            </QNET:RowSelectionField>
                            <asp:BoundField DataField="IdParametro" HeaderText="Id" />
                            <asp:BoundField DataField="cDescripcionGrupo" HeaderText="Tipo" />
                            <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="left" />
                            <asp:TemplateField HeaderText="Modificar" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkModificar" runat="server" ImageUrl="~/Imagenes/i.p.modificar.gif"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkEliminar" runat="server" ImageUrl="~/Imagenes/i.p.eliminar.gif"></asp:HyperLink>
                                    <asp:HiddenField ID="hidNombre" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                        </Columns>
                        <RowStyle HorizontalAlign="center" /> 
                    </asp:GridView>
                    <QNET:DataNavigator ID="dnvListado" runat="server" AllowCustomPaging="true" WidthTextBoxNumPagina="25px"
                        Width="100%" GridViewId="grvLista" ButtonText="Ir" PageIndicatorFormat="Página {0} / {1}" Visible="false" />                    
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnEliminar" EventName="Click" />
                </Triggers>
            </ajax:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
        <td colspan="3">
            <asp:Button ID="btnEliminar" runat="server" Text="[Eliminar]"  style="display:none"  />
            <asp:Button ID="btnOpenDetalle" runat="server" Text="[Open Detalle]"  style="display:none" /></td>
    </tr>
</table>
</center>
<!--NUEVO-->
<asp:Panel ID="pnlDetalle" runat="server" CssClass="cssTablaPanel" style="display:none">
    <table width="375px">
        <tr>
            <th colspan="2">
                <asp:Label ID="lblTitulo" runat="server"></asp:Label></th>
        </tr>
        <tr>
            <td style="width: 85px">
                <asp:Label ID="lbl2" runat="server" Text="Id:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtId" runat="server" Width="50px" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 85px">
                <asp:Label ID="Label2" runat="server" Text="Grupo:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlGrupo" runat="server" Width="250px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvTipo" runat="server" ControlToValidate="ddlGrupo"
                    Display="Dynamic" ErrorMessage="Seleccione un grupo." InitialValue="-1"
                    SetFocusOnError="True" ValidationGroup="Grabar">(*)</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 85px">
                <asp:Label ID="lbl3" runat="server" Text="Descripción:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100" Width="245px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv" runat="server" Display="Dynamic"
                    ErrorMessage="Ingrese una Descripción." ControlToValidate="txtDescripcion" ValidationGroup="Grabar">(*)</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <br />
                <asp:Button ID="btnGrabar" runat="server" Text="Grabar" ValidationGroup="Grabar" />
                <asp:Button ID="btnCerrarDetalle" runat="server" Text="Cerrar" />
                <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="Grabar" />
                <br />
                <br />
             
            </td>
        </tr>
    </table>
</asp:Panel>
    &nbsp;
<ajaxToolkit:ModalPopupExtender ID="mpeDetalle" runat="server" 
    BackgroundCssClass="modalBackground"
    CancelControlID="btnCerrarDetalle" 
    PopupControlID="pnlDetalle" 
    TargetControlID="btnOpenDetalle">
</ajaxToolkit:ModalPopupExtender>
</asp:Content>

