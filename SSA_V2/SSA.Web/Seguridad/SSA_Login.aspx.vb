Imports SSA.BusinessLogic
Imports SSA.BusinessEntity
Imports System.Configuration
Imports QNET.Common
Imports System.Collections.Generic
Partial Class Seguridad_Login
    Inherits System.Web.UI.Page

#Region "Page"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ScriptCliente()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.dnvListaSubasta.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridLista)
        If Not Page.IsPostBack Then
            Dim sbSccript As MyStringBuilder = New MyStringBuilder()
            If GlobalEntity.EsNulo Then
                sbSccript.AppendLine("document.getElementById('__pnLogin__').style.display = 'inline';")
                sbSccript.AppendLine("document.getElementById('__Administra__').style.display = 'none';")
            Else
                sbSccript.AppendLine("document.getElementById('__pnLogin__').style.display = 'none';")
                sbSccript.AppendLine("document.getElementById('__Administra__').style.display = 'inline';")
            End If
            Util.RegisterScript(upnListaSubasta, "__Visible__", sbSccript.ToString())

            DirectCast(Me.Master, MasterPage_SSA_Publica).setTitulo = "Iniciar Sesión"

            DirectCast(Me.Master, MasterPage_SSA_Publica).setTitulo2 = "Subastas Activas"

            Dim objBE_ListaOferta As New BE_ListaOferta
            objBE_ListaOferta.id_tipo_articulo = -1
            objBE_ListaOferta.id_subtipo_articulo = -1
            objBE_ListaOferta.nestado5 = MyConfig.getEstadoSubastaPublicada
            objBE_ListaOferta.nestado3 = MyConfig.getEstadoDetalleSubastaEnSubasta
            objBE_ListaOferta.nestado4 = MyConfig.getEstadoSubastaEliminada
            objBE_ListaOferta.nestado = MyConfig.getEstadoOfertaEliminada
            objBE_ListaOferta.nestado2 = MyConfig.getEstadoDetalleSubastaEliminado

            CargarDataList(objBE_ListaOferta)
            btnAdministrar.OnClientClick = String.Format("window.location = '{0}';", "../Subasta/SSA_Ofertar.aspx")
            imgArticulo01.Attributes.Add("onclick", "ColocarImagen(this.src)")
            imgArticulo02.Attributes.Add("onclick", "ColocarImagen(this.src)")
            imgArticulo03.Attributes.Add("onclick", "ColocarImagen(this.src)")
            txtOferta.Attributes.Add("onkeypress", "SoloDecimales(this)")
            btnRegistrarOferta.OnClientClick = "return RegistrarOferta()"
            revPrecio.ValidationExpression = MyConfig.getExpresionDecimales
        End If
    

    End Sub
#End Region

#Region "DataList"
    Protected Sub dtSubasta_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtSubasta.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
             e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim BE As BE_ListaOferta = CType(e.Item.DataItem, BE_ListaOferta)
            Dim img As Image = DirectCast(e.Item.FindControl("imgArticulo"), Image)
            Dim foto1 As String = MyConfig.getRutaSinFoto
            Dim foto2 As String = MyConfig.getRutaSinFoto
            Dim foto3 As String = MyConfig.getRutaSinFoto
            If Not String.IsNullOrEmpty(BE.crutimage1) Then
                foto1 = String.Format("{0}{1}", MyConfig.getRutaWebImagenes, BE.crutimage1)
            End If
            If Not String.IsNullOrEmpty(BE.crutimage2) Then
                foto2 = String.Format("{0}{1}", MyConfig.getRutaWebImagenes, BE.crutimage2)
            End If
            If Not String.IsNullOrEmpty(BE.crutimage3) Then
                foto3 = String.Format("{0}{1}", MyConfig.getRutaWebImagenes, BE.crutimage3)
            End If

            img.ImageUrl = foto1

            Dim btnOferta As Button = CType(e.Item.FindControl("btnOfertar"), Button)

            Dim FechaInicio As DateTime = BE.dfechaInicio.Date
            Dim FechaFin As DateTime = BE.dfechaFin.Date
            Dim FechaEntre As DateTime = DateTime.Now.Date
            Dim JS As String = String.Format("return OfertarItem('{0}', '{1}', '{2}', '{3}','{4}','{5}','{6}')", BE.id_articulo, BE.id_detsubasta, BE.id_oferta, BE.nprecio_oferta, foto1, foto2, foto3)

            'verificamos que la subasta no este cerrada
            If BE.nestado4 = MyConfig.getEstadoSubastaCerrado Then
                If Not BE.id_oferta.HasValue Then
                    JS = "JavaScript:SubastaCerrada();"
                End If
            Else
                If FechaEntre >= FechaInicio AndAlso FechaEntre <= FechaFin Then
                    img.Attributes.Add("onclick", JS)
                End If
            End If

            btnOferta.OnClientClick = JS

            Dim btnDetalle As Button = CType(e.Item.FindControl("btnDetalle"), Button)
            btnDetalle.OnClientClick = String.Format("return OpenDetalleTecnico('{0}', '{1}')", BE.id_articulo, BE.id_detsubasta)

        End If

    End Sub
#End Region

#Region "Botones"
    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        Dim objBE_Usuario As New BE_Usuario
        Dim objBLT_Usuario As New BLT_Usuario
        objBE_Usuario.cusuario = txtUsuario.Text
        objBE_Usuario.cclave = txtClave.Text
        objBE_Usuario.ccorreo = String.Empty

        objBE_Usuario = objBLT_Usuario.Seguridad_ObtenerUsuario(objBE_Usuario)

        If objBE_Usuario IsNot Nothing Then
            Dim idPerfilAdministrador As Byte = Convert.ToByte(ConfigurationManager.AppSettings("PerfilAdministrador"))
            Dim idPerfilComprador As Byte = Convert.ToByte(ConfigurationManager.AppSettings("PerfilComprador"))
            GlobalEntity.Instance.Usuario = objBE_Usuario
            GlobalEntity.Instance.Usuario.cusuario = txtUsuario.Text
            Select Case objBE_Usuario.nIdPerfil
                Case idPerfilAdministrador
                    FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, True)
                    Response.Redirect("../Seguridad/SSA_Opciones.aspx")
                Case idPerfilComprador
                    FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, False)
                    Response.Redirect("../Subasta/SSA_Ofertar.aspx")
            End Select
        Else
            ScriptManager.RegisterClientScriptBlock(upProxy, upProxy.GetType, "__ErrorLogin__", String.Format("alert('{0}')", Resources.SSA_Mensajes.msgSeguridadNoHayUsuario), True)
        End If

    End Sub

    Protected Sub btnDetalleSubasta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetalleSubasta.Click
        Dim BE As New BE_ListaOferta
        BE.id_articulo = Convert.ToInt32(hidIdArticulo.Value)
        BE.id_detsubasta = Convert.ToInt32(hidIdDetSubasta.Value)
        BE.nestado = MyConfig.getEstadoOfertaEliminada
        Dim objBLT_Oferta As New BLT_Oferta
        BE = objBLT_Oferta.Seleccionar(BE)
        Dim sbSccript As MyStringBuilder = New MyStringBuilder()
        If GlobalEntity.EsNulo Then
            lblUsuarioNombre.Text = ""
            sbSccript.AppendLineFormat("document.getElementById('{0}').value = '';", lblUsuarioNombre.ClientID)
            sbSccript.AppendLine("document.getElementById('rowOferta').style.display = 'none';")
            sbSccript.AppendLine("document.getElementById('rowLogin').style.display = 'inline';")
        Else
            lblUsuarioNombre.Text = GlobalEntity.Instance.Usuario.cnombres
            sbSccript.AppendLineFormat("document.getElementById('{0}').value = '{1}';", lblUsuarioNombre.ClientID, GlobalEntity.Instance.Usuario.cnombres)
            sbSccript.AppendLine("document.getElementById('rowOferta').style.display = 'inline';")
            sbSccript.AppendLine("document.getElementById('rowLogin').style.display = 'none';")
            'sbSccript.AppendLine("document.getElementById('__pnLogin__').style.display = 'none';")
            'sbSccript.AppendLine("document.getElementById('__Administra__').style.display = 'inline';")
            Util.RegisterScript(upnListaSubasta, "__Asigna__", sbSccript.ToString())
        End If
        If BE IsNot Nothing Then
            lblMoneda.Text = "SOLES"
            Util.RegisterAsyncPressButton(upnListaSubasta, "__AbrirDetalleOferta__", btnOpenOferta.ClientID)
        End If
    End Sub

    Protected Sub btnDescripcion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDescripcion.Click
        Dim BE As New BE_ListaOferta
        BE.id_articulo = Convert.ToInt32(hidIdArticulo.Value)
        BE.id_detsubasta = Convert.ToInt32(hidIdDetSubasta.Value)
        BE.nestado = MyConfig.getEstadoOfertaEliminada
        Dim objBLT_Oferta As New BLT_Oferta
        BE = objBLT_Oferta.Seleccionar(BE)
        If BE IsNot Nothing Then
            ltDetallesTecnicos.Text = BE.BE_Articulo.Cdattec.Replace(Chr(13), "<br>")
            Util.RegisterScript(upnListaSubasta, "__SetDescripcion01__", String.Format("document.getElementById('{0}').innerHTML='{1}';", ltDescripcionArticulo.ClientID, BE.BE_Articulo.cdescrip_breve))
            Util.RegisterScript(upnListaSubasta, "__SetDescripcion02__", String.Format("document.getElementById('{0}').innerHTML='{1}';", lblDescripcionDetallesTecnicos.ClientID, BE.BE_Articulo.cdescrip_breve))
            Util.RegisterAsyncPressButton(upnListaSubasta, "__AbrirDetalleTecnico2__", btnOpenDetalleTecnico.ClientID)
        End If
    End Sub

    Protected Sub btnRegistrarOferta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarOferta.Click
        'Validar que ya fue ofertado el articulo
        Dim objBE_ListaOferta As New BE_ListaOferta


        Dim objBLT_Oferta As New BLT_Oferta
        Dim objListaOferta As New List(Of BE_ListaOferta)
        objBE_ListaOferta.id_usuario = GlobalEntity.Instance.Usuario.id_usuario
        objBE_ListaOferta.id_detsubasta = Convert.ToInt32(Convert.ToInt32(hidIdDetSubasta.Value))
        objBE_ListaOferta.nestado5 = MyConfig.getEstadoSubastaPublicada
        objBE_ListaOferta.nestado3 = MyConfig.getEstadoDetalleSubastaEnSubasta
        objBE_ListaOferta.nestado4 = MyConfig.getEstadoSubastaEliminada
        objBE_ListaOferta.nestado = MyConfig.getEstadoOfertaEliminada
        objBE_ListaOferta.nestado2 = MyConfig.getEstadoDetalleSubastaEliminado
        objListaOferta = objBLT_Oferta.ListarSubastaValida(objBE_ListaOferta)

        If objListaOferta.Count = 0 Then
            Dim objBE_Oferta As New BE_Oferta
            objBE_Oferta.id_oferta = 0
            objBE_Oferta.id_usuario = GlobalEntity.Instance.Usuario.id_usuario
            objBE_Oferta.id_detsubasta = Convert.ToInt32(Convert.ToInt32(hidIdDetSubasta.Value))
            objBE_Oferta.nestado = MyConfig.getEstadoOfertaActiva
            objBE_Oferta.nprecio_oferta = Convert.ToDecimal(txtOferta.Text)


            Dim Tx As ResultadoTransaccion = Nothing
            Tx = objBLT_Oferta.Insertar(objBE_Oferta, objBE_Oferta.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                Util.RegisterAsyncAlert(upnListaSubasta, "__ExitoRegistrarOferta__", Resources.SSA_Mensajes.msgOfertaExitoRegistrar)

                Util.RegisterAsyncPressButton(upnListaSubasta, "__CerrarPopupDetalleOferta__", btnCerrarDetalleOferta.ClientID)
            Else
                Util.RegisterAsyncAlert(upnListaSubasta, "__ErrorRegistrarOferta__", Resources.SSA_Mensajes.msgOfertaErrorRegistrar)
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(upnListaSubasta, upnListaSubasta.GetType, "__ErrorLogin__", String.Format("alert('{0}')", "Ya realizo la oferta"), True)
        End If

    End Sub
#End Region

#Region " Scripts "

    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function NoSePuedeOfertar(){")
            sScript.AppendLineFormat("alert('{0}');", Resources.SSA_Mensajes.msgOfertaNoSePuedeOfertar)
            sScript.AppendLine("}")
            sScript.AppendLine("function OpenDetalleTecnico(idArticulo, idDetSubasta){")
            sScript.AppendLineFormat("document.getElementById('{0}').value = idArticulo;", hidIdArticulo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value = idDetSubasta;", hidIdDetSubasta.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnDescripcion.ClientID)
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")
            sScript.AppendLine("function RegistrarOferta(){")
            sScript.AppendLine("    if(Page_ClientValidate('Ofertar')){")
            sScript.AppendLineFormat("  return confirm('{0}');", Resources.SSA_Mensajes.msgOfertaConfirmarRegistrar)
            sScript.AppendLine("    }")
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")
            sScript.AppendLine("function OfertarItem(idArticulo, idDetSubasta, idOferta, montoOferta, img1, img2, img3){")
            sScript.AppendLineFormat("document.getElementById('{0}').value = idArticulo;", hidIdArticulo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value = idDetSubasta;", hidIdDetSubasta.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value = idOferta;", hidIdOferta.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').src = img1;", imgArticuloPrincipal.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').src = img1;", imgArticulo01.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').src = img2;", imgArticulo02.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').src = img3;", imgArticulo03.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value = '';", txtOferta.ClientID)
            ' sScript.AppendLineFormat("document.getElementById('{0}').disabled='';", btnRegistrarOferta.ClientID)
            sScript.AppendLine("    if(idOferta>0)")
            sScript.AppendLine("    {")
            sScript.AppendLineFormat("document.getElementById('{0}').value = montoOferta;", txtOferta.ClientID)
            sScript.AppendLineFormat("  document.getElementById('{0}').disabled='disabled';", btnRegistrarOferta.ClientID)
            sScript.AppendLine("    }")
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnDetalleSubasta.ClientID)
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")
            sScript.AppendLine("function ColocarImagen(src){")
            sScript.AppendLineFormat("document.getElementById('{0}').src=src;", imgArticuloPrincipal.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("function SubastaCerrada(){")
            sScript.AppendLineFormat("alert('{0}')", Resources.SSA_Mensajes.msgOfertaSubastaCerrada)
            sScript.AppendLine("}")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), True)
        End If
    End Sub

#End Region

#Region "Metodos"
    Private Sub CargarDataList(ByVal objListaOferta As BE_ListaOferta)
        Dim objBLT_Oferta As New BLT_Oferta
        Dim objPaginador As New Paginador
        Dim lista As IList = objBLT_Oferta.ListarPaginado(objListaOferta, objPaginador)

        dtSubasta.DataSource = lista
        dtSubasta.DataBind()
    End Sub
#End Region

    
    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        Dim objBE_Usuario As New BE_Usuario
        Dim objBLT_Usuario As New BLT_Usuario
        objBE_Usuario.cusuario = txtUsuarioOferta.Text
        objBE_Usuario.cclave = txtClaveUsuario.Text
        objBE_Usuario.ccorreo = String.Empty

        objBE_Usuario = objBLT_Usuario.Seguridad_ObtenerUsuario(objBE_Usuario)

        If objBE_Usuario IsNot Nothing Then
            Dim idPerfilAdministrador As Byte = Convert.ToByte(ConfigurationManager.AppSettings("PerfilAdministrador"))
            Dim idPerfilComprador As Byte = Convert.ToByte(ConfigurationManager.AppSettings("PerfilComprador"))
            GlobalEntity.Instance.Usuario = objBE_Usuario
            GlobalEntity.Instance.Usuario.cusuario = txtUsuarioOferta.Text
            lblUsuarioNombre.Text = objBE_Usuario.cnombres
            DirectCast(Me.Master, MasterPage_SSA_Publica).setUsuario = GlobalEntity.Instance.Usuario.cusuario
            Dim sbSccript As MyStringBuilder = New MyStringBuilder()
            'sbSccript.AppendLineFormat("alert('{0}');", objBE_Usuario.cnombres)
            'sbSccript.AppendLineFormat("document.getElementById('{0}').value = '{1}';", txtUsuarioNombre.ClientID, objBE_Usuario.cnombres)
            sbSccript.AppendLine("document.getElementById('rowOferta').style.display = 'inline';")
            sbSccript.AppendLine("document.getElementById('rowLogin').style.display = 'none';")
            sbSccript.AppendLine("document.getElementById('__pnLogin__').style.display = 'none';")
            sbSccript.AppendLine("document.getElementById('__Administra__').style.display = 'inline';")
            Util.RegisterScript(upRegistro, "__Muestra__", sbSccript.ToString())

        Else
            ScriptManager.RegisterClientScriptBlock(upRegistro, upProxy.GetType, "__ErrorLogin__", String.Format("alert('{0}')", Resources.SSA_Mensajes.msgSeguridadNoHayUsuario), True)
        End If
    End Sub


    Protected Sub btnAdministrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdministrar.Click
        FormsAuthentication.RedirectFromLoginPage(GlobalEntity.Instance.Usuario.cusuario, True)
        Response.Redirect("../Subasta/SSA_Ofertar.aspx")
    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Util.RegisterAsyncPressButton(upnListaSubasta, "__CerrarPopupDetalleOferta__", btnCerrarDetalleOferta.ClientID)
    End Sub
End Class
