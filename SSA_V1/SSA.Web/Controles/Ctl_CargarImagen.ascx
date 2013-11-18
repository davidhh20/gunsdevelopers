<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Ctl_CargarImagen.ascx.vb" Inherits="Controles_Ctl_CargarImagen" %>

<asp:Panel ID="pnlCarga" runat="server" Width="250px">
    <asp:FileUpload ID="FileUp" runat="server" Width="200px" CssClass="CajaTexto" />
    <asp:ImageButton ID="ibtnAdjuntar" runat="server" ImageUrl="~/Imagenes/i.p.adjuntar.gif"
                ValidationGroup="CargarArchivo"/>
    <br />
<asp:Label ID="lblErrorArchivo" runat="server" Font-Bold="True"
                Font-Names="Arial" Font-Size="10px" ForeColor="Red"></asp:Label></asp:Panel>

<asp:Panel ID="pnlResultado" runat="server" style="display:none; width:250px">
    <table>
        <tr>
            <td><asp:Image ID="imgFile" runat="server" Height="47px" Width="44px" /></td>
            <td><asp:Label ID="lblPropiedades" runat="server" Width="55px"/>
                <asp:HyperLink ID="hlkElimina" runat="server" ImageUrl="~/Imagenes/i.p.eliminar.gif"/></td>
        </tr>
    </table>
    <asp:HiddenField ID="hidRutaArchivo" runat="server" />
</asp:Panel><asp:Button ID="btnEliminar" runat="server" Text="Eliminar" style="display:none" />
<asp:Button ID="btnSubir" runat="server" Text="[Subir Archivo]" style="display:none" />