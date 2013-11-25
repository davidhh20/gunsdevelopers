<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_Principal.master" AutoEventWireup="false" CodeFile="SSA_OfertaComprador.aspx.vb" Inherits="Reporte_SSA_OfertaComprador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">
<center>
    <asp:GridView ID="grvLista" runat="server" AutoGenerateColumns="false" CellPadding="1"
        OnRowDataBound="grvLista_RowDataBound" RowStyle-HorizontalAlign="Left" Width="800px">
        <Columns>
            <QNET:RowSelectionField SelectionMode="Single">
                <itemstyle width="5px" />
            </QNET:RowSelectionField>
            <asp:BoundField DataField="id_subasta" HeaderText="Código" />
            <asp:BoundField DataField="s_dfecha" HeaderText="Fecha de registro" />
            <asp:BoundField DataField="s_dfpublicacion" HeaderText="Fecha de publicación" />
            <asp:BoundField DataField="s_dfinicio" HeaderText="Fecha de inicio" />
            <asp:BoundField DataField="s_dfinal" HeaderText="Fecha fin" />
            <asp:BoundField DataField="des_estado" HeaderText="Estado" />
            <asp:TemplateField HeaderText="Ver Reporte">
                <ItemTemplate>
                    <asp:HyperLink ID="hlk01" runat="server" ImageUrl="~/Imagenes/i.p.buscar.gif"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle HorizontalAlign="center" />
    </asp:GridView>
    <QNET:DataNavigator ID="dnvListado" runat="server" AllowCustomPaging="true" WidthTextBoxNumPagina="25px"
                            Width="99%" GridViewId="grvLista" ButtonText="Ir" PageIndicatorFormat="Página {0} / {1}"
                            Visible="false" />
    <asp:HiddenField ID="hidOperacion" runat="server" />
</center>
<script language="javascript" type="text/javascript">
    function AbrirVentana01(idSubasta){
        window.open("SSA_Print_OfertaComprador.aspx?id="+idSubasta,null,'directories=no, menubar=no, status=yes, toolbar=no, scrollbars=yes, resizable=no, width=800px')
    }
    function AbrirVentana02(idSubasta){
        window.open("SSA_Print_Rentabilidad.aspx?id="+idSubasta,null,'directories=no, menubar=no, status=yes, toolbar=no, scrollbars=yes, resizable=no, width=800px')
    }
</script>

</asp:Content>


