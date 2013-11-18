<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_Principal.master" AutoEventWireup="false" CodeFile="SSA_PublicarSubasta.aspx.vb" Inherits="Subasta_SSA_PublicarSubasta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">
    <table cellpadding="2" cellspacing="0" width="100%" >
        <tr>
            <td>
                <asp:Label ID="lblFilIdArticulo" runat="server" Text="Código:"></asp:Label>
                <asp:TextBox ID="txtCodigoSubasta" runat="server" MaxLength="6"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revCodigo" runat="server" ErrorMessage="Ingrese sólo dígitos en el código."
                    SetFocusOnError="True" ValidationExpression="[\d]+" ValidationGroup="Buscar" ControlToValidate="txtCodigoSubasta">(*)</asp:RegularExpressionValidator>
                <asp:Label ID="Label2" runat="server" Text="Estado :"></asp:Label>
                <asp:DropDownList ID="ddlEstadoSubasta" runat="server">
                </asp:DropDownList>
            </td>
            <td style="text-align: right;">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" ValidationGroup="Buscar" />
                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" />
                <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="Buscar" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <ajax:UpdatePanel ID="upLista" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvLista" runat="server" AutoGenerateColumns="false" Width="100%"
                            CellPadding="1" RowStyle-HorizontalAlign="Left" OnRowDataBound="grvLista_RowDataBound">
                            <Columns>
                                <QNET:RowSelectionField SelectionMode="Single">
                                    <itemstyle width="5px" />
                                </QNET:RowSelectionField>
                                <asp:BoundField DataField="id_subasta" HeaderText="Código"/>
                                <asp:BoundField DataField="s_dfecha" HeaderText="Fecha de registro" />
                                <asp:BoundField DataField="s_dfpublicacion" HeaderText="Fecha de publicación" />
                                <asp:BoundField DataField="s_dfinicio" HeaderText="Fecha de inicio" />
                                <asp:BoundField DataField="s_dfinal" HeaderText="Fecha fin" />
                                <asp:BoundField DataField="des_estado" HeaderText="Estado" />
                                <asp:TemplateField HeaderText="Modificar">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlkModificar" runat="server" ImageUrl="~/Imagenes/i.p.modificar.gif"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlkEliminar" runat="server" ImageUrl="~/Imagenes/i.p.eliminar.gif"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cerrar Subasta">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlkCerrar" runat="server" ImageUrl="~/Imagenes/i.p.cerrar.gif"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle HorizontalAlign="center" />
                        </asp:GridView>
                        <QNET:DataNavigator ID="dnvListado" runat="server" AllowCustomPaging="true" WidthTextBoxNumPagina="25px"
                            Width="99%" GridViewId="grvLista" ButtonText="Ir" PageIndicatorFormat="Página {0} / {1}"
                            Visible="false" />
                    </ContentTemplate>
                    <Triggers>
                        <ajax:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        <ajax:AsyncPostBackTrigger ControlID="btnEliminar" EventName="Click" />
                    </Triggers>
                </ajax:UpdatePanel>
                <asp:Button ID="btnEliminar" runat="server" Text="[Eliminar Subasta]" style="display:none" />
                <asp:Button ID="btnOpenDetalle" runat="server" Text="[Cerrar Subasta]" style="display:none" />
                <asp:Button ID="btnListarDetalle" runat="server" Text="[Listar Detalle de Subasta]" style="display:none" />
                <asp:HiddenField ID="hidIdSubasta" runat="server" />
            </td>
        </tr>
    </table>
 
 <!--Inicio detalle de la subasta-->
<asp:Panel ID="pnlDetalle" runat="server" CssClass="cssTablaPanel" Style="display: none">
    <table style="text-align:center;">
        <tr>
            <th><asp:Label ID="lbl01" runat="server" Text="DETALLE DE LA SUBASTA" /></th>
        </tr>
        <tr>
            <td style="text-align:left;"><asp:Label ID="Label1" runat="server" Text="Subasta N° :  " Font-Bold="true" /><asp:Label ID="lblIdSubasta" runat="server"/></td>
        </tr>
        <tr>
            <td>
                <ajax:UpdatePanel ID="upDetalle" runat="server">
                    <ContentTemplate>
                    <div style="overflow:scroll; height:200px;">
                        <asp:GridView ID="grvListaDetalle" runat="server" AutoGenerateColumns="false" Width="700px"
                                DataKeyNames="id_detsubasta">
                            <Columns>
                                <QNET:RowSelectionField SelectionMode="Single">
                                    <itemstyle width="5px" />
                                </QNET:RowSelectionField>
                                <asp:TemplateField HeaderText="Código">
                                    <ItemTemplate>
                                        <%#Eval("BE_Articulo.ccodart")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción">
                                    <ItemTemplate>
                                        <%#Eval("BE_Articulo.cdescrip_breve")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marca">
                                    <ItemTemplate>
                                        <%#Eval("BE_Articulo.cmarca")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modelo">
                                    <ItemTemplate>
                                        <%#Eval("BE_Articulo.cmodelo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="des_estado" HeaderText="Estado Subasta" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    </ContentTemplate>
                    <Triggers>
                        <ajax:AsyncPostBackTrigger ControlID="btnGrabarCerrarSubasta" EventName="Click" />
                        <ajax:AsyncPostBackTrigger ControlID="btnListarDetalle" EventName="Click" />
                    </Triggers>
                </ajax:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button id="btnGrabarCerrarSubasta" runat="server" Text="Cerrar Subasta"></asp:Button>&nbsp;&nbsp;
                <asp:Button id="btnCerrarDetalle" runat="server" Text="Cancelar"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Panel>
 <ajaxToolkit:ModalPopupExtender ID="mpeBusquedaArticulo" runat="server" 
    BackgroundCssClass="modalBackground"
    CancelControlID="btnCerrarDetalle" 
    PopupControlID="pnlDetalle" 
    TargetControlID="btnOpenDetalle">
</ajaxToolkit:ModalPopupExtender>
<!--Fin detalle de la subasta-->
</asp:Content>

