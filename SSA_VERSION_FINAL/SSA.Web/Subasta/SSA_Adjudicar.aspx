<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_Principal.master" AutoEventWireup="false" CodeFile="SSA_Adjudicar.aspx.vb" Inherits="Subasta_SSA_Adjudicar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">
<center>
<table>
    <tr>
        <td>
            <ajax:UpdatePanel ID="upLista" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grvLista" runat="server" AutoGenerateColumns="false" 
                        Width="800px" OnRowDataBound="grvLista_RowDataBound">
                        <Columns>
                            <QNET:RowSelectionField SelectionMode="Single">
                                <itemstyle width="5px" />
                            </QNET:RowSelectionField>                        
                            <asp:BoundField DataField="id_subasta" HeaderText="N° Subasta" />
                            <asp:BoundField DataField="id_detsubasta" HeaderText="N° Detalle" />
                            <asp:BoundField DataField="id_articulo" HeaderText="Cod. Artículo" />
                            <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="left">
                                <ItemTemplate><%#Eval("BE_Articulo.cdescrip_breve")%></ItemTemplate>
                            </asp:TemplateField>    
                            <asp:BoundField DataField="des_estado" HeaderText="Estado Detalle" />
                            <asp:TemplateField HeaderText="Ver Ofertas">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkOfertas" runat="server" ImageUrl="~/Imagenes/i.p.detalle.gif" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle HorizontalAlign="center" />
                    </asp:GridView>
                    <QNET:DataNavigator ID="dnvListado" runat="server" AllowCustomPaging="true" WidthTextBoxNumPagina="25px"
                        Width="100%" GridViewId="grvLista" ButtonText="Ir" PageIndicatorFormat="Página {0} / {1}" Visible="false" />
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="btnGrabarAdjudicacion" EventName="Click"  />
                    <ajax:AsyncPostBackTrigger ControlID="btnVendido" EventName="Click"  />
                </Triggers>
            </ajax:UpdatePanel>
            <asp:HiddenField ID="hidIdDetalleSubasta" runat="server" /><asp:HiddenField ID="hidIdTxtComentario" runat="server" /><asp:HiddenField ID="hidIdChk" runat="server" EnableViewState="False" />
            <asp:Button ID="btnOpenListaOfertas" runat="server" Text="[Abrir Lista de Ofertas por Detalle Subasta]" style="display:none;" />
            <asp:Button ID="btnListarOfertas" runat="server" Text="[Listar Ofertas segun DetSubasta]" style="display:none;" />
            <asp:Button ID="btnOpenEnvioCorreo" runat="server" Text="[Abrir el Envio de Correo]" style="display:none;" />
        </td>
    </tr>
</table>
</center>
<!--Inicio de la lista de Ofertas -->
<asp:Panel ID="pnlListaOferta" runat="server" CssClass="cssTablaPanel" style="display:none">
<table style="text-align:center">
    <tr>
        <th colspan="4"><asp:Label ID="lbl02137834" runat="server" Text="LISTA DE OFERTAS"/></th>
    </tr>
    <tr style="text-align:left;">
        <td>
            <asp:Label ID="Label2" runat="server" Text="Código:" Font-Bold="True"></asp:Label>
            <asp:Label ID="lblCodigo" runat="server"></asp:Label></td>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Estado:" Font-Bold="True"></asp:Label>
            <asp:Label ID="lblEstado" runat="server"></asp:Label></td>
        <td>
            <asp:Label ID="Label5" runat="server" Text="N° de Siniestro:" Font-Bold="True"></asp:Label>
            <asp:Label ID="lblNroSiniestro" runat="server"></asp:Label></td>
        <td>
            <asp:Label ID="Label7" runat="server" Text="Precio Base:" Font-Bold="True"></asp:Label>
            <asp:Label ID="lblPrecioBase" runat="server"></asp:Label></td>
    </tr>
    <tr><td colspan="4">&nbsp;</td></tr>
    <tr>
        <td colspan="4">
            <ajax:UpdatePanel ID="upListaOferta" runat="server">
                <ContentTemplate>
                <div style="overflow:scroll; height:350px;">
                    <asp:GridView ID="grvListaOferta" runat="server" AutoGenerateColumns="false"
                        Width="750px" OnRowDataBound="grvListaOferta_RowDataBound"
                        DataKeyNames="id_oferta, id_adjudicacion">
                        <Columns>
                            <QNET:RowSelectionField SelectionMode="Single">
                                <itemstyle width="5px" />
                            </QNET:RowSelectionField>
                            <asp:BoundField DataField="id_oferta" HeaderText="Oferta" />
                            <asp:TemplateField HeaderText="Usuario" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate><%#Eval("BE_Usuario.NombreApellido")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="nprecio_oferta" HeaderText="Precio Ofertado" />
                            <asp:BoundField DataField="" HeaderText="Marcado" />
                            <asp:TemplateField HeaderText="Enviar Correo">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkCorreo" runat="server" ImageUrl="~/Imagenes/i.p.email.gif" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="" HeaderText="Vendido" />
                            <asp:TemplateField HeaderText="Adjudicar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comentario">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Width="200px" Enabled="false"   />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle HorizontalAlign="center" />
                    </asp:GridView>
                </div>    
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="btnListarOfertas" EventName="Click"  />
                    <ajax:AsyncPostBackTrigger ControlID="btnVendido" EventName="Click"  />
                    <ajax:AsyncPostBackTrigger ControlID="btnGrabarAdjudicacion" EventName="Click"  />
                </Triggers>
            </ajax:UpdatePanel>        
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnVendido" runat="server" Text="Vendido" />&nbsp;&nbsp;
            <asp:Button ID="btnGrabarAdjudicacion" runat="server" Text="Adjudicar" />&nbsp;&nbsp;
            <asp:Button ID="btnCerrarListaOfertas" runat="server" Text="Cerrar" /></td>
    </tr>
</table>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeBusquedaArticulo" runat="server" 
    BackgroundCssClass="modalBackground"
    CancelControlID="btnCerrarListaOfertas" 
    PopupControlID="pnlListaOferta" 
    TargetControlID="btnOpenListaOfertas">
</ajaxToolkit:ModalPopupExtender>
<!--Final de la lista de Ofertas -->

<!--Inicio Envio Correos-->
<asp:Panel ID="pnlEnvioCorreo" runat="server" CssClass="cssTablaPanel" style="display:none; width:500px;">
    <table style="width:100%;">
        <tr>
            <th colspan="4"><asp:Label ID="Label26" runat="server" Text="ENVÍO DE CORREO"></asp:Label></th>
        </tr>
        <tr>
            <td colspan="4"><asp:Label ID="lbl023283834" runat="server" Text="Detalle Artículo" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4"><asp:Label ID="lblDescripcionArticuloEnvioCorreo" runat="server"></asp:Label></td>        
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Código:"></asp:Label>
                <asp:Label ID="lblCodigoEnvio" runat="server"></asp:Label></td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Marca:"></asp:Label>
                <asp:Label ID="lblMarcaEnvio" runat="server"></asp:Label></td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Modelo:"></asp:Label>
                <asp:Label ID="lblModeloEnvio" runat="server"></asp:Label></td>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Año:"></asp:Label>
                <asp:Label ID="lblAnioEnvio" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4"><asp:Label ID="Label3" runat="server" Text="Detalle del Correo" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="Fecha:"></asp:Label></td>
            <td>
                <asp:Label ID="lblFecha" runat="server"></asp:Label></td>
            <td>
                <asp:Label ID="Label16" runat="server" Text="Hora:"></asp:Label>
                <asp:Label ID="lblHora" runat="server"></asp:Label></td>
            <td>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label18" runat="server" Text="Correo 1:"></asp:Label></td>
            <td colspan="3">
                <asp:Label ID="lblCorreo1" runat="server"></asp:Label>
                <asp:HiddenField ID="hidCorreo1" runat="server" EnableViewState="False" /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label19" runat="server" Text="Correo 2:"></asp:Label></td>
            <td colspan="3">
                <asp:Label ID="lblCorreo2" runat="server"></asp:Label>
                <asp:HiddenField ID="hidCorreo2" runat="server" EnableViewState="False" /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label20" runat="server" Text="Postor:"></asp:Label></td>
            <td colspan="3">
                <asp:Label ID="lblPostor" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label24" runat="server" Text="Asunto:"></asp:Label></td>
            <td colspan="3">
                <asp:TextBox ID="txtAsuntoCorreo" runat="server" style="width:90%;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAsunto" runat="server" ControlToValidate="txtAsuntoCorreo"
                    Display="Dynamic" ErrorMessage="Ingrese un asunto de correo." SetFocusOnError="True"
                    ValidationGroup="Correo">(*)</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label25" runat="server" Text="Mensaje:"></asp:Label>
                <ajax:UpdatePanel ID="upProxy" runat="server">
                    <Triggers>
                        <ajax:AsyncPostBackTrigger ControlID="btnEnviarCorreo" EventName="Click" />
                    </Triggers>
                </ajax:UpdatePanel>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtCuerpoCorreo" runat="server" TextMode="MultiLine" style="width:90%; height:150px;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvMensaje" runat="server" ControlToValidate="txtCuerpoCorreo"
                    Display="Dynamic" ErrorMessage="Ingrese un mensaje de correo." SetFocusOnError="True"
                    ValidationGroup="Correo">(*)</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align:center;" colspan="4">
                &nbsp;<asp:Button ID="btnEnviarCorreo" runat="server" Text="Enviar" ValidationGroup="Correo" />
                <asp:Button ID="btnCerrarEnvioCorreo" runat="server" Text="Cerrar" />
                <asp:ValidationSummary ID="vsCorreo" runat="server" DisplayMode="List" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="Correo" />
            </td>
        </tr>
    </table>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeEnvioCorreo" runat="server" 
    BackgroundCssClass="modalBackground"
    CancelControlID="btnCerrarEnvioCorreo" 
    PopupControlID="pnlEnvioCorreo" 
    TargetControlID="btnOpenEnvioCorreo"></ajaxToolkit:ModalPopupExtender>
<!--Fin Envio Correos-->
</asp:Content>