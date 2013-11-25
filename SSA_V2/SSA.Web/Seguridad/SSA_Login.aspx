<%@ Page Language="VB" MasterPageFile="~/MasterPage/SSA_CompradorPublica.master" 
    AutoEventWireup="false" CodeFile="SSA_Login.aspx.vb
    " Inherits="Seguridad_Login" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" Runat="Server">

 <table width="100%"  border="0">
    <tr>
       <td style="width:40%;" valign="top">
         <table width="100%"  border="0">
           <tr id="__Administra__" style="display:none;">
            <td valign="top" align="center" >
                <asp:Button ID="btnAdministrar" runat="server" Text="Administrar Subasta" 
                    CssClass="button_naranja_xg" />
               </td>
           </tr>
           <tr id="__pnLogin__">
            <td>
                <table width="100%"  border="0">
                   <tr>
                     <td>
                        <ajax:UpdatePanel ID="upProxy" runat="server">
                     <ContentTemplate>        
                        <table cellpadding="5" border="0">
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Usuario:"></asp:Label></td>
                <td><asp:TextBox ID="txtUsuario" runat="server" CssClass="caja_g"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsuario" ControlToValidate="txtUsuario" runat="server" ValidationGroup="Login" ErrorMessage="Ingrese el usuario.">(*)</asp:RequiredFieldValidator></td>    
            </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="Contraseña:"></asp:Label></td>
                <td><asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="caja_g"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvClave" ControlToValidate="txtClave" runat="server" ValidationGroup="Login" ErrorMessage="Ingrese la clave.">(*)</asp:RequiredFieldValidator></td>    
            </tr>
            <tr>
                <td colspan="2">
                    
                </td>
            </tr>
        </table>
                     </ContentTemplate>
                     <Triggers>
                        <ajax:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click" />
                        
                     </Triggers>
                </ajax:UpdatePanel>        
             </td>
                   </tr>
                   <tr>
              <td align="center" colspan="2">
                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" 
                      CssClass="button_naranja_m" ValidationGroup="Login" Height="24px" Width="81px"/>
              </td>
           </tr>
                   <tr><td>&nbsp;</td></tr>
                   <tr>
             <td align="center">
                <asp:HyperLink ID="hlkUsuario" runat="server" NavigateUrl="~/Comprador/SSA_RegistrarComprador.aspx">Registrar usuario</asp:HyperLink> |
                <asp:HyperLink ID="hlkClave" runat="server" NavigateUrl="~/Comprador/SSA_OlvidoClave.aspx">Olvidé mi contraseña</asp:HyperLink> 
             </td>
           </tr>
                </table>
            </td>
           </tr> 
      
            
         </table>
       </td> 
       <td style="width:60%;">
            <table>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                        <ajax:UpdatePanel ID="upnListaSubasta" runat="server">
                            <ContentTemplate>
                               <asp:DataList ID="dtSubasta" runat="server" RepeatDirection="Horizontal" RepeatColumns="3"
                               RepeatLayout="Table" GridLines="Both" >
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      
                                 
                                        <table border="0">
                                            <tr>
                                                <td colspan="2"><b><%# Eval("cdescrip_breve") %></b></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Image ID="imgArticulo" runat="server" ImageUrl='<%# Eval("crutimage1") %>' Width="100px" Height="100px" /> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left">
                                                    <asp:Button ID="btnDetalle" runat="server" Text="Ver Detalle" CssClass="button_naranja_m" Width="81px" Height="24px"/>&nbsp;
                                                     <asp:Button ID="btnOfertar" runat="server" Text="Ofertar" CssClass="button_naranja_m" Width="81px" Height="24px"/>
                                                </td>
                                          

                                            </tr>
                                        </table>
                        


                                
                            </ItemTemplate>
                              </asp:DataList>&nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <ajax:AsyncPostBackTrigger ControlID="btnRegistrarOferta" EventName="Click" />
                           
                            </Triggers>
                        </ajax:UpdatePanel>
                    
                    </td>
                </tr> 
                <tr>
                    <td colspan="2">
                        
                    </td>
                </tr>
          
            </table>
           </td>
      
    </tr>

    
 </table>
    <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Login" />
     <asp:Button ID="btnDescripcion" runat="server" Text="[Ver Detalle]" style="display:none;" />
        <asp:Button ID="btnDetalleSubasta" runat="server" Text="[Ver Detalle de Subasta]" style="display:none;" />
<asp:HiddenField ID="hidIdArticulo" runat="server" /><asp:HiddenField ID="hidIdOferta" runat="server" /><asp:HiddenField ID="hidIdDetSubasta" runat="server" />
<asp:Button ID="btnOpenOferta" runat="server" Text="[Open oferta]" style="display:none;" />
<asp:Button ID="btnOpenDetalleTecnico" runat="server" Text="[Open Detalle Tecnico]" style="display:none;" />
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
    <tr id="rowLogin" style="display:inline;">
        <td>
            <ajax:UpdatePanel ID="upRegistro" runat="server">
                <ContentTemplate>
                    <table border="0" style="width:500px;">
                       <tr>
                            <td colspan="3">
                                <asp:Label ID="Label8" runat="server" Text="Para poder ofertar ustede debe ingresar su usuario y clave "></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Usuario:" Font-Bold="True" /></td>
                            <td>
                                <asp:TextBox ID="txtUsuarioOferta" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUsuarioOferta" runat="server" ControlToValidate="txtUsuarioOferta"
                                    Display="Dynamic" ErrorMessage="Ingrese su usuario." SetFocusOnError="True"
                                    ValidationGroup="LoginOferta">(*)</asp:RequiredFieldValidator></td>
                            <td style="text-align:right;">
                                <asp:Label ID="Label7" runat="server" Text="Contraseña:" Font-Bold="True"/>
                                <asp:TextBox ID="txtClaveUsuario" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvClaveO" runat="server" ControlToValidate="txtClaveUsuario"
                                Display="Dynamic" ErrorMessage="Ingrese su código." SetFocusOnError="True"
                                ValidationGroup="LoginOferta">(*)</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;<asp:Label ID="Label10" runat="server" Text="**Si no está registrado haga click "></asp:Label>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Comprador/SSA_RegistrarComprador.aspx">aquí</asp:HyperLink></td>
                            <td colspan="2" style="text-align:right;">
                                &nbsp; &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                </Triggers>
            </ajax:UpdatePanel>
        </td>
        <td>
            <table border="0">
                <tr><td>
                                 </td></tr>
                <tr><td>
                    &nbsp;</td></tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                      <asp:Button ID="btnRegistrar" runat="server" Text="Ingresar" ValidationGroup="LoginOferta"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="LoginOferta" />
        </td>
    </tr>
    <tr id="rowOferta" style="display:none;">
        <td>
            <ajax:UpdatePanel ID="upOferta" runat="server">
                <ContentTemplate>
                    <table border="0" style="width:500px;">     
                       <tr>
                            <td colspan="4">
                                <asp:Label ID="Label6" runat="server" Text="Bienvenido(s) Sr.(es): "></asp:Label>
                                <asp:Label ID="lblUsuarioNombre" runat="server"></asp:Label>        
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label11" runat="server" Text="Registre su oferta :"></asp:Label></td>
                        </tr>
                   
                 
                        <tr >
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Moneda:" Font-Bold="True" />
                                <asp:Label ID="lblMoneda" runat="server" /></td>
                            <td>
                                </td>
                            <td colspan="2">
                                &nbsp;<asp:Label ID="Label5" runat="server" Text="Monto Ofertado:" Font-Bold="True"/>
                                <asp:TextBox ID="txtOferta" runat="server" SkinID="caja_p" style="text-align:right;" MaxLength="12" Width="112px" /><asp:RequiredFieldValidator ID="rfvOferta" runat="server" ControlToValidate="txtOferta"
                                    Display="Dynamic" ErrorMessage="Ingrese un monto de oferta." SetFocusOnError="True"
                                    ValidationGroup="Ofertar">(*)</asp:RequiredFieldValidator><asp:CompareValidator ID="cvOfertar"
                                        runat="server" ControlToValidate="txtOferta" Display="Dynamic" ErrorMessage="El monto de oferta ingresado no es válido."
                                        Operator="GreaterThan" SetFocusOnError="True" Type="Double" ValidationGroup="Ofertar"
                                        ValueToCompare="1">(*)</asp:CompareValidator><asp:RegularExpressionValidator ID="revPrecio" runat="server" ControlToValidate="txtOferta"
                                        Display="Dynamic" ErrorMessage="Formato de precio ofertado no váildo. (##.00)" SetFocusOnError="True"
                                        ValidationGroup="Ofertar">(*)</asp:RegularExpressionValidator></td>
                        </tr>
                 
                    
                    </table>
                </ContentTemplate>
                <Triggers>
                    <ajax:AsyncPostBackTrigger ControlID="btnDetalleSubasta" EventName="Click" />
                    <ajax:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                </Triggers>
            </ajax:UpdatePanel>
        </td>
        <td>
            <table border="0">
                <tr><td>
                                </td></tr>
                <tr><td></td></tr>
                <tr>
                    <td>
                                <asp:Button ID="btnRegistrarOferta" runat="server" Text="Registrar" ValidationGroup="Ofertar" /></td>
                </tr>
                <tr>
                    <td>
                       </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="Ofertar" />
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
             <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" style="display:none;"/>     
             <asp:Button ID="btnCerrarDetalleOferta" runat="server" Text="Cerrar"  />
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
            <ajax:AsyncPostBackTrigger ControlID="btnDescripcion" EventName="Click" />
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

 
<script language="javascript" type="text/javascript">
    document.getElementById("<%=txtUsuario.ClientID%>").focus();
</script>
</asp:Content>
