<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_Principal.master" AutoEventWireup="false" CodeFile="SSA_ListarArticulos.aspx.vb" Inherits="Articulos_SSA_ListarArticulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">
<table align="center" cellpadding="3" cellspacing="0" border="0" width="100%" >
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Código:"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtCodart" runat="server" MaxLength="6" Width="60px"></asp:TextBox></td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Tipo Artículo:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlTipoArticulo" runat="server">
            </asp:DropDownList></td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="SubTipo:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlSubTipoArticulo" runat="server">
            </asp:DropDownList></td>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Estado:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlEstado" runat="server">
            </asp:DropDownList></td>
        <td style="text-align:right">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" /></td>
    </tr>
    <tr>
    <td>&nbsp;</td></tr>
    <tr>
        <td colspan="9" style="text-align:center">
            <ajax:UpdatePanel ID="upLista" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grvLista" runat="server" AutoGenerateColumns="false" Width="100%"
                        OnRowDataBound="grvLista_RowDataBound" >
                        <columns>
                            <asp:BoundField DataField="ccodart" HeaderText="Código" />
                            <asp:BoundField DataField="des_tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="subdes_tipo" HeaderText="Sub Tipo" />
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
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkModificar" runat="Server" ImageUrl="~/Imagenes/i.p.modificar.gif"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkElimina" runat="Server" ImageUrl="~/Imagenes/i.p.eliminar.gif"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </columns>
                        <RowStyle HorizontalAlign="center" />
                        <EmptyDataRowStyle HorizontalAlign="center" />
                    </asp:GridView>
                    <QNET:DataNavigator ID="dnvListado" runat="server" AllowCustomPaging="true" WidthTextBoxNumPagina="25px"
                        Width="100%" GridViewId="grvLista" ButtonText="Ir" PageIndicatorFormat="Página {0} / {1}" Visible="false" />
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnEliminar" EventName="Click" />
                </Triggers>
            </ajax:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="9">
            <asp:Button ID="btnEliminar" runat="server" Text="[Eliminar item]" style="display:none" />
            <asp:Button ID="btnOpenFotos" runat="server" Text="[Abrir Fotos]" style="display:none" />&nbsp;
            <asp:HiddenField ID="hidIdArticulo" runat="server" />
            <asp:HiddenField ID="hidRutaImagen2" runat="server" />
            <asp:HiddenField ID="hidRutaImagen3" runat="server" />
            <asp:HiddenField ID="hidRutaImagen1" runat="server" />
        </td>
    </tr>
</table>

<asp:Panel ID="pnlDetalle" runat="server" class="cssTablaPanel" style="display:none;" >
    <table  class="cssTablaPanel" style="text-align:center;width:600px;height:400px;">
        <tr style="height:370px;">
            <td><img id="imgFoto" runat="server" alt="sinFoto" src="" style="width: 500px; height: 450px" /></td>
        </tr>
        <tr>
        <tr >
            <td>
            
                <asp:HyperLink ID="hlkFoto1" runat="server" Text="1"/>&nbsp;
                <asp:HyperLink ID="hlkFoto2" runat="server" Text="2"/>&nbsp;
                <asp:HyperLink ID="hlkFoto3" runat="server" Text="3"/>&nbsp;
                <br />
                <br />
                <asp:Button ID="btnCerrarDetalle" runat="server" Text="Cerrar" /></td>
        </tr>
    </table>
    <ajax:UpdatePanel ID="upFoto" runat="server">
        <Triggers>
            <ajax:AsyncPostBackTrigger ControlID="btnOpenFotos" EventName="Click" />
        </Triggers>
    </ajax:UpdatePanel>    
    </asp:Panel>

 <ajaxToolkit:ModalPopupExtender ID="mpeFormularioDetalle" runat="server" 
                    BackgroundCssClass="modalBackground"
                    CancelControlID="btnCerrarDetalle" 
                    PopupControlID="pnlDetalle" 
                    TargetControlID="btnOpenFotos">
             </ajaxToolkit:ModalPopupExtender>
             
</asp:Content>

