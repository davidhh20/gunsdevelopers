<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_Principal.master" AutoEventWireup="false" CodeFile="SSA_NuevaSubasta.aspx.vb" Inherits="Subasta_SSA_NuevaSubasta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">

<table cellpadding="5" cellspacing="0" border="0" align="center" >
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Código:"></asp:Label></td>
        <td colspan="5">
            <asp:TextBox ID="txtIdSubasta" runat="server" Enabled="False" MaxLength="6" SkinID="caja_p"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revCodigo" runat="server" ControlToValidate="txtIdSubasta"
                ErrorMessage="Formato de código inválido." ValidationExpression="[\d]+" ValidationGroup="Registrar">(*)</asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Fecha de publicación:"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtFechaPubli" runat="server" AutoCompleteType="None" CausesValidation="false" SkinID="caja_p"></asp:TextBox>
            <asp:ImageButton ID="imbFechaPubli" runat="server" CausesValidation="false" ImageUrl="~/Imagenes/i.p.calendar.gif" />
            <ajaxToolkit:CalendarExtender ID="cexFechaPubli" runat="server" Format="dd/MM/yyyy"
                PopupButtonID="imbFechaPubli" TargetControlID="txtFechaPubli">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:MaskedEditValidator ID="mevTextoFechaPubli" runat="server" ControlExtender="meeTextoFechaPubli"
                ControlToValidate="txtFechaPubli" Display="Dynamic"
                EmptyValueBlurredText="(*)" EmptyValueMessage="Ingrese una fecha de publicación." 
                IsValidEmpty="false"
                ErrorMessage="mevTextoFechaPubli"
                InvalidValueBlurredMessage="(*)" InvalidValueMessage="Ingrese una fecha de publicación válida."
                ValidationGroup="Registrar" SetFocusOnError="True"></ajaxToolkit:MaskedEditValidator>
            <ajaxToolkit:MaskedEditExtender ID="meeTextoFechaPubli" runat="server" AutoComplete="true"
                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" TargetControlID="txtFechaPubli">
            </ajaxToolkit:MaskedEditExtender>        
        </td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Fecha de inicio:"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtFechaIni" runat="server" AutoCompleteType="None" CausesValidation="false" SkinID="caja_p"></asp:TextBox>
            <asp:ImageButton ID="imbFechaIni" runat="server" CausesValidation="false" ImageUrl="~/Imagenes/i.p.calendar.gif" />
            <ajaxToolkit:CalendarExtender ID="cexFechaIni" runat="server" Format="dd/MM/yyyy"
                PopupButtonID="imbFechaIni" TargetControlID="txtFechaIni">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:MaskedEditValidator ID="mevTextoFechaIni" runat="server" ControlExtender="meeTextoFechaIni"
                ControlToValidate="txtFechaIni" Display="Dynamic"
                EmptyValueBlurredText="(*)" EmptyValueMessage="Ingrese una fecha de inicio." 
                IsValidEmpty="false"
                ErrorMessage="mevTextoFechaIni"
                InvalidValueBlurredMessage="(*)" InvalidValueMessage="Ingrese una fecha de inicio válida."
                ValidationGroup="Registrar" SetFocusOnError="True"></ajaxToolkit:MaskedEditValidator>
            <ajaxToolkit:MaskedEditExtender ID="meeTextoFechaIni" runat="server" AutoComplete="true"
                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" TargetControlID="txtFechaIni">
            </ajaxToolkit:MaskedEditExtender>
        </td>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Fecha final:"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtFechaFin" runat="server" AutoCompleteType="None" CausesValidation="false" SkinID="caja_p"></asp:TextBox>
            <asp:ImageButton ID="imbFechaFin" runat="server" CausesValidation="false" ImageUrl="~/Imagenes/i.p.calendar.gif" />
            <ajaxToolkit:CalendarExtender ID="cexFechaFin" runat="server" Format="dd/MM/yyyy"
                PopupButtonID="imbFechaFin" TargetControlID="txtFechaFin">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:MaskedEditValidator ID="mevTextoFechaFin" runat="server" ControlExtender="meeTextoFechaFin"
                ControlToValidate="txtFechaFin" Display="Dynamic"
                EmptyValueBlurredText="(*)" EmptyValueMessage="Ingrese una fecha de inicio." 
                IsValidEmpty="false"
                ErrorMessage="mevTextoFechaFin"
                InvalidValueBlurredMessage="(*)" InvalidValueMessage="Ingrese una fecha final válida."
                ValidationGroup="Registrar" SetFocusOnError="True"></ajaxToolkit:MaskedEditValidator><asp:CustomValidator
                    ID="cvFecha" runat="server" ClientValidationFunction="fComparaFecha" Display="Dynamic"
                    ErrorMessage="La fecha final debe ser mayor a la fecha inicial." ValidationGroup="Registrar">(*)</asp:CustomValidator><asp:CustomValidator
                        ID="cvFilasArticulos" runat="server" ClientValidationFunction="fExistenFilas"
                        Display="Dynamic" ErrorMessage="Debe agregar por lo menos un artículo." ValidationGroup="Registrar">&nbsp;</asp:CustomValidator>
            <ajaxToolkit:MaskedEditExtender ID="meeTextoFechaFin" runat="server" AutoComplete="true"
                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" TargetControlID="txtFechaFin">
            </ajaxToolkit:MaskedEditExtender>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="text-align:right;">
            &nbsp;<asp:ValidationSummary ID="vs" runat="server" DisplayMode="List"
                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Registrar" />
            &nbsp;&nbsp;
            <asp:HiddenField ID="hidIdArticuloDetalle" runat="server" />
            <asp:Button ID="btnEliminar" runat="server" Text="[ Eliminar Item ]" style="display:none;" />
            <asp:Button ID="btnBuscar" runat="server" Text="[ Open Agregar Artículo ]" style="display:none;" />
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" />
            <asp:Button ID="btnGrabar" runat="server" Text="Registrar" ValidationGroup="Registrar" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" /></td>
    </tr>
    <tr>
        <td colspan="6" style="text-align:center;">
            <ajax:UpdatePanel ID="upLista" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grvListaDetalle" runat="server" AutoGenerateColumns="false" Width="700px" 
                        DataKeyNames="id_detsubasta, id_articulo" OnRowDataBound="grvListaDetalle_RowDataBound">
                        <Columns>
                            <QNET:RowSelectionField SelectionMode="Single">
                                <itemstyle width="5px" />
                            </QNET:RowSelectionField>
                            <asp:TemplateField HeaderText="Código">
                                <ItemTemplate><%#Eval("BE_Articulo.ccodart")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción">
                                <ItemTemplate><%#Eval("BE_Articulo.cdescrip_breve")%></ItemTemplate>
                                <ItemStyle  HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Marca">
                                <ItemTemplate><%#Eval("BE_Articulo.cmarca")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modelo">
                                <ItemTemplate><%#Eval("BE_Articulo.cmodelo")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Año">
                                <ItemTemplate><%#Eval("BE_Articulo.canio")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="des_estado" HeaderText="Estado Subasta" />
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkEliminar" runat="Server" ImageUrl="~/Imagenes/i.p.eliminar.gif"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="btnAceptarBusquedaArticulo" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnEliminar" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                </Triggers>
            </ajax:UpdatePanel>
        </td>
    </tr>
</table>

<!--Busqueda de Artículos-->
<asp:Panel ID="pnlBusquedaArticulo" CssClass="cssTablaPanel" runat="server" style="display:none; height:300px">
<table  class="cssTablaPanel">
    <tr>
        <td colspan="8"><asp:Label ID="lbl09343" runat="server" CssClass="tit_02" Font-Bold="true" Text="BUSCAR ARTÍCULO"/></td>
    </tr>
    <tr><td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="Código:"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtCodart" runat="server" MaxLength="6" Width="60px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revCodigoArticulo" runat="server" ControlToValidate="txtIdSubasta"
                ErrorMessage="Formato de código inválido." ValidationExpression="[\d]+" ValidationGroup="Buscar">(*)</asp:RegularExpressionValidator></td>
        <td>
            <asp:Label ID="Label6" runat="server" Text="Tipo Artículo:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlTipoArticulo" runat="server" CausesValidation="true"/></td>
        <td>
            <asp:Label ID="Label7" runat="server" Text="SubTipo:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlSubTipoArticulo" runat="server">
            </asp:DropDownList></td>
        <td>
            <asp:Label ID="Label8" runat="server" Text="Estado:"></asp:Label></td>
        <td>
            <asp:Label ID="Label9" runat="server" Text="REGISTRADO"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="8" style="text-align:right">
            <asp:Button ID="btnBusquedaArticulo" runat="server" Text="Buscar" ValidationGroup="Buscar" />
              
        </td>
    </tr>
    <tr>
        <td colspan="8" style="text-align:center">
            <ajax:UpdatePanel ID="upBusquedaArticulo" runat="server">
                <ContentTemplate>
                    <div id="__ContenidoBusqueda__">
                    <asp:GridView ID="grvLista" PageSize="7" runat="server" AutoGenerateColumns="false" 
                        Width="100%" OnRowDataBound="grvLista_RowDataBound"
                         DataKeyNames="id_articulo">
                        <columns>
                            <QNET:RowSelectionField SelectionMode="Single">
                                <itemstyle width="5px" />
                            </QNET:RowSelectionField>
                            <asp:BoundField DataField="ccodart" HeaderText="Código" />
                            <asp:BoundField DataField="cdescrip_breve" HeaderText="Descripción" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="cmarca" HeaderText="Marca" />
                            <asp:BoundField DataField="cmodelo" HeaderText="Modelo" />
                            <asp:BoundField DataField="canio" HeaderText="Año" />
                            <asp:BoundField DataField="des_estado" HeaderText="Estado" />
                            <asp:TemplateField HeaderText="Fotos">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkFotos" runat="Server" ImageUrl="~/Imagenes/i.p.buscar.gif"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Detalle">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkDetalle" runat="Server" ImageUrl="~/Imagenes/i.p.detalle.gif"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSel" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </columns>
                        <RowStyle HorizontalAlign="center" />
                        <EmptyDataRowStyle HorizontalAlign="center" />
                    </asp:GridView>
                    <QNET:DataNavigator ID="dnvListado" runat="server" AllowCustomPaging="true" WidthTextBoxNumPagina="25px"
                        Width="100%" GridViewId="grvLista" ButtonText="Ir" PageIndicatorFormat="Página {0} / {1}" Visible="false" />
                    </div>                        
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnBusquedaArticulo" EventName="Click" />
                </Triggers>
            </ajax:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="8" align="center">
            <asp:Button ID="btnOpenFotos" runat="server" Text="[Abrir Fotos]" style="display:none" />
            <asp:Button ID="btnSeleccionarDetalle" runat="server" Text="[ Seleccionar Detalle]" style="display:none" />
            <asp:Button ID="btnOpenDetalle" runat="server" Text="[Abrir Detalle]" style="display:none" />
            <asp:CustomValidator ID="cvArticulos" runat="server" ClientValidationFunction="fSeleccion"
                ErrorMessage="Seleccione por lo menos un artículo." ValidationGroup="Agregar2">&nbsp;</asp:CustomValidator>
            <asp:ValidationSummary ID="vsAgregar2" runat="server" DisplayMode="List" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="Agregar2" />
            <asp:ValidationSummary ID="vsBusquedaArticulo" runat="server" DisplayMode="List"
                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Buscar" />
            <asp:HiddenField ID="hidIdArticulo" runat="server" />
            <asp:HiddenField ID="hidRutaImagen2" runat="server" />
            <asp:HiddenField ID="hidRutaImagen3" runat="server" />
            <asp:HiddenField ID="hidRutaImagen1" runat="server" />
            <asp:Button ID="btnAceptarBusquedaArticulo" runat="server" Text="Aceptar" ValidationGroup="Agregar2" />
            <asp:Button ID="btnCerrarBusquedaArticulo" runat="server" Text="Cerrar" />&nbsp;     
        </td>
    </tr>
</table>
    </asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeBusquedaArticulo" runat="server" 
    BackgroundCssClass="modalBackground"
    CancelControlID="btnCerrarBusquedaArticulo" 
    PopupControlID="pnlBusquedaArticulo" 
    TargetControlID="btnBuscar">
</ajaxToolkit:ModalPopupExtender>
<!--Fin Busqueda de Artículos-->


<!--Fotos de Artículo-->                 
<asp:Panel ID="pnlDetalleFoto" runat="server" style="display:none">
    <table  class="cssTablaPanel" style="text-align:center;width:600px;height:400px;">
        <tr style="height:360px;">
            <td><img id="imgFoto" runat="server" alt="sinFoto" src="" style="width: 500px; height: 450px" /></td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="hlkFoto1" runat="server" Text="1"/>&nbsp;
                <asp:HyperLink ID="hlkFoto2" runat="server" Text="2"/>&nbsp;
                <asp:HyperLink ID="hlkFoto3" runat="server" Text="3"/>&nbsp;
                <br />
                <br />
                <asp:Button ID="btnCerrarDetalleFoto" runat="server" Text="Cerrar" /></td>
        </tr>
    </table>
    <ajax:UpdatePanel ID="upDetalleFoto" runat="server">
        <Triggers>
            <ajax:AsyncPostBackTrigger ControlID="btnOpenFotos" EventName="Click" />
        </Triggers>
    </ajax:UpdatePanel>    
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeFormularioDetalle" runat="server" 
                    BackgroundCssClass="modalBackground"
                    CancelControlID="btnCerrarDetalleFoto" 
                    PopupControlID="pnlDetalleFoto" 
                    TargetControlID="btnOpenFotos">
             </ajaxToolkit:ModalPopupExtender>
<!--Fin Fotos de Artículo-->     


<!--Detalle de Artículo-->     
    <asp:Panel ID="pnlDetalleTecnico" runat="server" style="display:none">
        <table class="cssTablaPanel">
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Font-Bold="true">DATOS TÉCNICOS DEL ARTÍCULO</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <ajax:UpdatePanel ID="upDetalleTecnico" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Código:"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCodigoArticuloDetalle" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Descripción:"></asp:Label></td>
                                    <td>
                                        <asp:Literal ID="ltDescripcionArticuloDetalle" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label13" runat="server" Text="Datos Técnicos:"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Literal ID="ltDatosArticuloDetalle" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <ajax:AsyncPostBackTrigger ControlID="btnSeleccionarDetalle" EventName="Click" />
                            <ajax:AsyncPostBackTrigger ControlID="btnOpenDetalle" EventName="Click" />
                        </Triggers>
                    </ajax:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="text-align:center;">
                    <asp:Button ID="btnCerrarDetalleTecnico" runat="server" Text="Cerrar" />
                </td>
            </tr>
        </table>
    </asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeDetalleTecnico" runat="server" 
    BackgroundCssClass="modalBackground"
    CancelControlID="btnCerrarDetalleTecnico" 
    PopupControlID="pnlDetalleTecnico" 
    TargetControlID="btnOpenDetalle">
</ajaxToolkit:ModalPopupExtender>
<!--Fin Detalle de Artículo-->     
</asp:Content>

