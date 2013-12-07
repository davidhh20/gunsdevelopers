<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_Reportes.master" AutoEventWireup="false" CodeFile="SSA_Print_Rentabilidad.aspx.vb" Inherits="Reporte_SSA_Print_Rentabilidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">

<table>
    <tr>
        <td>
            <asp:Label ID="lb01" runat="server" Text="(*) Los que estan resaltados en rojo no fueron vendidos" ForeColor="red" Visible="false" /></td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="grvLista" runat="server" AutoGenerateColumns="False" ShowFooter="true">
                <Columns>
                    <asp:BoundField DataField="ccodart" HeaderText="Código" />
                    <asp:BoundField DataField="cdescrip_breve" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderText="Usuario">
                        <ItemTemplate><%#Eval("BE_Usuario.NombreApellido")%></ItemTemplate>
                    </asp:TemplateField>            
                    <asp:BoundField DataField="nprecio_base" HeaderText="Precio Base" />
                    <asp:BoundField DataField="nprecio_oferta" HeaderText="Precio Ofertado" />
                    <asp:BoundField DataField="rentabilidad" HeaderText="Rentabilidad" />
                    <asp:BoundField DataField="cComentario" HeaderText="Comentario" />
                </Columns>
            </asp:GridView>        
        </td>
    </tr>
</table>
    
</asp:Content>

