<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_Comprador.master" AutoEventWireup="false" CodeFile="SSA_Ofertar.aspx.vb" Inherits="Subasta_SSA_Oferta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">

<center>
<table cellpadding="5" cellspacing="0" border="0" width="700px">
    <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="Tipo Artículo:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlTipoArticulo" runat="server" CausesValidation="true" AutoPostBack="true">
            </asp:DropDownList></td>
        <td>
            <asp:Label ID="Label7" runat="server" Text="SubTipo:"></asp:Label></td>
        <td>
            <ajax:UpdatePanel ID="upSubTipo" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlSubTipoArticulo" runat="server">
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="ddlTipoArticulo" EventName="SelectedIndexChanged" />
                </Triggers>
            </ajax:UpdatePanel>
        </td>
        <td style="text-align: right;">
            <asp:HiddenField ID="hidIdUsuario" runat="server" /><asp:HiddenField ID="hidIdArticulo" runat="server" /><asp:HiddenField ID="hidIdOferta" runat="server" /><asp:HiddenField ID="hidIdDetSubasta" runat="server" />
            &nbsp;&nbsp;
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
        </td>
    </tr>
    <tr><td colspan="5">
        <asp:Button ID="btnEliminar" runat="server" Text="[ Eliminar Oferta ]" style="display:none;" />
        <asp:Button ID="btnDetalleSubasta" runat="server" Text="[Ver Detalle de Subasta]" style="display:none;" />
        <asp:Button ID="btnOpenOferta" runat="server" Text="[Open oferta]" style="display:none;" />
        <asp:Button ID="btnOpenDetalleTecnico" runat="server" Text="[Open Detalle Tecnico]" style="display:none;" />
        </td></tr>
    <tr>
        <td colspan="5">
            <ajax:UpdatePanel ID="upLista" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grvLista" runat="server" AutoGenerateColumns="false" Width="100%"
                        DataKeyNames="id_subasta, id_detsubasta, id_articulo, id_oferta" OnRowDataBound="grvLista_RowDataBound">
                        <Columns>
                            <QNET:RowSelectionField SelectionMode="Single">
                                <itemstyle width="5px" />
                            </QNET:RowSelectionField>
                            <asp:BoundField DataField="ccodart" HeaderText="Código" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="img" runat="server" Width="80px" Height="80px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="cdescrip_breve" HeaderText="Descripción" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="nprecio_oferta" HeaderText="Precio Ofertado" />
                            <asp:BoundField DataField="s_dfecha" HeaderText="Fecha de Registro" />
                            <asp:TemplateField HeaderText="Ofertar">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkOfertar" runat="server" ImageUrl="~/Imagenes/i.p.adjuntar.gif" />                                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkEliminar" runat="server" ImageUrl="~/Imagenes/i.p.eliminar.gif" />
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
                    <ajax:AsyncPostBackTrigger ControlID="btnEliminar" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnRegistrarOferta" EventName="Click" />
                </Triggers>
            </ajax:UpdatePanel>
        </td>
    </tr>
</table>   
</center>

<!--Detalle de la Oferta -->
<asp:Panel ID="pnlDetalleOferta" CssClass="cssTablaPanel" runat="server" style="display:none; width:700px;">
    <table border="0" cellpadding="1" cellspacing="0">
        <tr>
            <th colspan="2" ><asp:Label ID="Label4" runat="server" Text="REGISTRAR OFERTA" />
            </th></tr>
        <tr><td colspan="2"><asp:label ID="ltDescripcionArticulo" runat="server"/></td></tr>
        <tr>
            <td rowspan="3" style="vertical-align: top; height:450px;">
                <img id="imgArticuloPrincipal" src="" runat="server" alt="imagePrincipal" style="width: 500px; height: 101%" /></td>
            <td style="vertical-align: top">
                <img id="imgArticulo01" src="" runat="server" alt="image01" style="width: 200px; height: 150px; cursor: hand;" /></td>
        </tr>
        <tr>
            <td>
                <img id="imgArticulo02" src="" runat="server" alt="image02" style="width: 200px; height: 150px; cursor: hand;" /></td>
        </tr>
        <tr>
            <td>
                <img id="imgArticulo03" src="" runat="server" alt="image03" style="width: 200px; height: 150px; cursor: hand;" /></td>
        </tr>
   </table>
   <table style="width:100%">
    <tr>
        <td>
            <ajax:UpdatePanel ID="upOferta" runat="server">
                <ContentTemplate>
                    <table border="0" style="width:500px;">     
                        <tr>
                            <td><asp:Label ID="Label2" runat="server" Text="Moneda:" Font-Bold="True" /></td>
                            <td><asp:Label ID="lblMoneda" runat="server" /></td>
                            <td style="text-align:right;">
                                <asp:Label ID="Label3" runat="server" Text="Monto Ofertado:" />&nbsp;
                                <asp:TextBox ID="txtOferta" runat="server" SkinID="caja_p" style="text-align:right;" MaxLength="12" />
                                <asp:RequiredFieldValidator ID="rfvOferta" runat="server" ControlToValidate="txtOferta"
                                    Display="Dynamic" ErrorMessage="Ingrese un monto de oferta." SetFocusOnError="True"
                                    ValidationGroup="Ofertar">(*)</asp:RequiredFieldValidator><asp:CompareValidator ID="cvOfertar"
                                        runat="server" ControlToValidate="txtOferta" Display="Dynamic" ErrorMessage="El monto de oferta ingresado no es válido."
                                        Operator="GreaterThan" SetFocusOnError="True" Type="Double" ValidationGroup="Ofertar"
                                        ValueToCompare="1">(*)</asp:CompareValidator><asp:RegularExpressionValidator ID="revPrecio" runat="server" ControlToValidate="txtOferta"
                                        Display="Dynamic" ErrorMessage="Formato de precio ofertado no váildo. (##.00)" SetFocusOnError="True"
                                        ValidationGroup="Ofertar">(*)</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnDetallesTecnicos" runat="server" Text="Detalles Técnicos" SkinID="button_g" /></td>
                            <td colspan="2" style="text-align:right;">
                                &nbsp;&nbsp;
                                <asp:Label ID="Label1" runat="server" Text="Mayor Oferta:" Font-Bold="True" />&nbsp;<asp:Label ID="lblMayorOferta" runat="server" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="btnDetalleSubasta" EventName="Click" />
                </Triggers>
            </ajax:UpdatePanel>
        </td>
        <td>
            <table border="0">
                <tr><td><asp:Button ID="btnRegistrarOferta" runat="server" Text="Registrar" ValidationGroup="Ofertar" /></td></tr>
                <tr><td><asp:Button ID="btnCerrarDetalleOferta" runat="server" Text="Cerrar" /></td></tr>
            </table>
            <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="Ofertar" />
        </td>
    </tr>
   </table>   
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeDetalleOferta" runat="server" 
    BackgroundCssClass="modalBackground"
    CancelControlID="btnCerrarDetalleOferta" 
    PopupControlID="pnlDetalleOferta" 
    TargetControlID="btnOpenOferta">
</ajaxToolkit:ModalPopupExtender>
<!--Fin del Detalle de la Oferta -->



<!--Inicio de Detalle Tecnicos-->
<asp:Panel ID="pnlDetalleTecnico" CssClass="cssTablaPanel" runat="server" style="display:none; width:300px;">
    <ajax:UpdatePanel ID="upDetalleTecnico" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="1" cellspacing="0" width="100%">
                <tr>
                    <th>
                        <asp:Label ID="lbl0943784" runat="server" Text="DETALLES TÉCNICOS" />
                    </th>
                </tr>
                <tr>
                    <td><asp:label ID="lblDescripcionDetallesTecnicos" runat="server"/><br /><br /></td>
                </tr><tr>
                    <td><div style="overflow:scroll; width:100%; height:250px;">
                            <asp:Literal ID="ltDetallesTecnicos" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr><td>&nbsp;</td></tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <ajax:AsyncPostBackTrigger ControlID="btnDetalleSubasta" EventName="Click" />
        </Triggers>
    </ajax:UpdatePanel>
    <center>
        <asp:Button ID="btnCerrarDetalleTecnico" runat="server" Text="Cerrar" />
    </center>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender
    ID="mpeDetalleTecnicos" runat="server" 
    BackgroundCssClass="modalBackground"
    CancelControlID="btnCerrarDetalleTecnico" 
    PopupControlID="pnlDetalleTecnico" 
    TargetControlID="btnOpenDetalleTecnico">
</ajaxToolkit:ModalPopupExtender>
<!--Fin de Detalle Tecnicos-->
</asp:Content>

