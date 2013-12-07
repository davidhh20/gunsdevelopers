<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Ctl_ListaOfertas.ascx.vb" Inherits="Controles_Ctl_ListaOfertas" %>

<center>
<table>
    <tr style="text-align: left;">
        <td>
            <asp:Label ID="Label2" runat="server" Text="Código:" Font-Bold="True"></asp:Label>
            <asp:Label ID="lblCodigo" runat="server"></asp:Label></td>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Descripción:"></asp:Label>
            <asp:Label ID="lblDescripcion" runat="server"></asp:Label></td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="Label7" runat="server" Text="Precio Base:" Font-Bold="True"></asp:Label>
            <asp:Label ID="lblPrecioBase" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="grvListaOferta" runat="server" AutoGenerateColumns="false" DataKeyNames="id_oferta, id_adjudicacion"
                OnRowDataBound="grvListaOferta_RowDataBound" Width="750px">
                <Columns>
                    <asp:BoundField DataField="id_oferta" HeaderText="Oferta" />
                    <asp:TemplateField HeaderText="Usuario" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%#Eval("BE_Usuario.NombreApellido")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="nprecio_oferta" HeaderText="Precio Ofertado" />
                    <asp:BoundField DataField="" HeaderText="Marcado" />
                    <asp:BoundField DataField="" HeaderText="Vendido" />
                    <asp:TemplateField HeaderText="Adjudicado">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comentario" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%#Eval("BE_Adjudicacion.cComentario")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle HorizontalAlign="center" />
            </asp:GridView>
            <asp:GridView ID="grvCopy" runat="server" AutoGenerateColumns="false" DataKeyNames="id_oferta, id_adjudicacion"
                OnRowDataBound="grvListaOferta_RowDataBound" Width="750px">
                <Columns>
                    <asp:BoundField DataField="id_oferta" HeaderText="Oferta" />
                    <asp:TemplateField HeaderText="Usuario" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%#Eval("BE_Usuario.NombreApellido")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="nprecio_oferta" HeaderText="Precio Ofertado" />
                    <asp:BoundField DataField="" HeaderText="Marcado" />
                    <asp:BoundField DataField="" HeaderText="Vendido" />
                    <asp:TemplateField HeaderText="Adjudicado">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comentario" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%#Eval("BE_Adjudicacion.cComentario")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle HorizontalAlign="center" />
            </asp:GridView>
        </td>
    </tr>
</table>
<br /><br />
</center>
