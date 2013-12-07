Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common
Imports QNET.Web.UI.Controls
Imports Microsoft.VisualBasic

Partial Class Subasta_SSA_Oferta
    Inherits QNET.Common.BasePage
    ''' <summary>
    ''' Va listar todos los DETALLES DE SUBASTA  que esten en esatdo ENSUBASTA y lo asociara a la
    ''' oferta del usuario que este logueado, si la tuviera
    ''' </summary>
#Region " Página "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        GridEffects.RegisterGridForEffects(grvLista)
        ScriptCliente()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dnvListado.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridLista)
        If Not Page.IsPostBack Then
            CType(Master, MasterPage_SSA_Comprador).setTitulo = Resources.SSA_Texto.SubastaOfertar
            hidIdUsuario.Value = GlobalEntity.Instance.Usuario.id_usuario
            CargarCombosTipo()
            'dnvListado.InvokeBind()
            txtOferta.Attributes.Add("onkeypress", "SoloDecimales(this)")
            imgArticulo01.Attributes.Add("onclick", "ColocarImagen(this.src)")
            imgArticulo02.Attributes.Add("onclick", "ColocarImagen(this.src)")
            imgArticulo03.Attributes.Add("onclick", "ColocarImagen(this.src)")
            btnRegistrarOferta.OnClientClick = "return RegistrarOferta()"
            revPrecio.ValidationExpression = MyConfig.getExpresionDecimales
            btnDetallesTecnicos.OnClientClick = "return OpenDetalleTecnico();"
        End If
    End Sub

    Protected Sub ddlTipoArticulo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoArticulo.SelectedIndexChanged

        If Convert.ToInt32(ddlTipoArticulo.SelectedValue.ToString) = -1 Then
            ddlSubTipoArticulo.Items.Clear()
        Else
            Dim objBLT_SubTipoArticulo As New BLT_SubTipoArticulo
            Dim objBE_SubTipoArticulo As New BE_SubTipoArticulo
            objBE_SubTipoArticulo.IdTipoArticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue.ToString)
            objBE_SubTipoArticulo.nEstado = MyConfig.getSubTipoArticuloActivo
            Util.VinculaDropDownList(ddlSubTipoArticulo, objBLT_SubTipoArticulo.Listar(objBE_SubTipoArticulo), "IdSubTipoArticulo", "cDescripcion")
        End If
        Util.AddAllItemToDDL(ddlSubTipoArticulo)

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        dnvListado.InvokeBind()
    End Sub

    Protected Sub grvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_ListaOferta = CType(e.Row.DataItem, BE_ListaOferta)
            Dim hlk As HyperLink = DirectCast(e.Row.FindControl("hlkEliminar"), HyperLink)
            Dim hlk2 As HyperLink = DirectCast(e.Row.FindControl("hlkOfertar"), HyperLink)
            If Not BE.id_oferta.HasValue Then
                e.Row.Cells(4).Text = String.Empty
            End If
            hlk.Visible = BE.id_oferta.HasValue
            'hlk2.Visible = Not BE.id_oferta.HasValue
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

            Dim img As Image = DirectCast(e.Row.FindControl("img"), Image)
            img.ImageUrl = foto1
            img.Attributes.Add("style", "cursor:hand")
            'verificamos si la fecha de hoy esta en el rango válido de la subasta
            Dim FechaInicio As DateTime = BE.dfechaInicio.Date
            Dim FechaFin As DateTime = BE.dfechaFin.Date
            Dim FechaEntre As DateTime = DateTime.Now.Date
            Dim JS As String = String.Format("JavaScript:OfertarItem('{0}', '{1}', '{2}', '{3}','{4}','{5}','{6}')", BE.id_articulo, BE.id_detsubasta, BE.id_oferta, BE.nprecio_oferta, foto1, foto2, foto3)
            Dim JSeliminar As String = String.Format("JavaScript:EliminarItem('{0}','{1}')", BE.id_articulo, BE.id_oferta)
            'verificamos que la subasta no este cerrada
            If BE.nestado4 = MyConfig.getEstadoSubastaCerrado Then
                If Not BE.id_oferta.HasValue Then
                    JS = "JavaScript:SubastaCerrada();"
                End If
                JSeliminar = "JavaScript:SubastaCerrada();"
            Else
                If FechaEntre >= FechaInicio AndAlso FechaEntre <= FechaFin Then
                    img.Attributes.Add("onclick", JS)
                Else
                    'tambien verificamos que si ya ha registrado la oferta le permita ver su detalle
                    If Not BE.id_oferta.HasValue Then
                        JS = "JavaScript:NoSePuedeOfertar();"
                    End If
                    JSeliminar = "JavaScript:NoSePuedeOfertar();"
                End If
            End If
            hlk.NavigateUrl = JSeliminar
            hlk2.NavigateUrl = JS

        End If
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim objBE_Oferta As New BE_Oferta
        objBE_Oferta.id_oferta = Convert.ToInt32(hidIdOferta.Value)
        objBE_Oferta.nestado = MyConfig.getEstadoOfertaEliminada
        Dim objBLT_Oferta As New BLT_Oferta
        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_Oferta.Eliminar(objBE_Oferta, objBE_Oferta.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            Util.RegisterAsyncAlert(upLista, "__ExitoEliminarOferta__", Resources.SSA_Mensajes.msgOfertaExitoEliminar)
            dnvListado.InvokeBindKeepPage()
        Else
            Util.RegisterAsyncAlert(upLista, "__ErrorEliminarOferta__", Resources.SSA_Mensajes.msgOfertaErrorEliminar)
        End If
    End Sub

    Protected Sub btnDetalleSubasta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetalleSubasta.Click

        Dim BE As New BE_ListaOferta
        BE.id_articulo = Convert.ToInt32(hidIdArticulo.Value)
        BE.id_detsubasta = Convert.ToInt32(hidIdDetSubasta.Value)
        BE.nestado = MyConfig.getEstadoOfertaEliminada
        Dim objBLT_Oferta As New BLT_Oferta
        BE = objBLT_Oferta.Seleccionar(BE)
        If BE IsNot Nothing Then
            'lblPrecioBase.Text = BE.BE_Articulo.nprecio_base
            ltDetallesTecnicos.Text = BE.BE_Articulo.Cdattec.Replace(Chr(13), "<br>")
            If BE.nprecio_oferta.HasValue Then
                lblMayorOferta.Text = BE.nprecio_oferta
            Else
                lblMayorOferta.Text = "No Hay Registrados aún."
            End If
            lblMoneda.Text = "SOLES"
            Util.RegisterScript(upLista, "__SetDescripcion01__", String.Format("document.getElementById('{0}').innerHTML='{1}'", ltDescripcionArticulo.ClientID, BE.BE_Articulo.cdescrip_breve))
            Util.RegisterScript(upLista, "__SetDescripcion02__", String.Format("document.getElementById('{0}').innerHTML='{1}'", lblDescripcionDetallesTecnicos.ClientID, BE.BE_Articulo.cdescrip_breve))
            Util.RegisterAsyncPressButton(upLista, "__AbrirDetalleOferta__", btnOpenOferta.ClientID)
        End If
    End Sub

    Protected Sub btnRegistrarOferta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarOferta.Click
        Dim objBE_Oferta As New BE_Oferta
        objBE_Oferta.id_oferta = 0
        objBE_Oferta.id_usuario = Convert.ToInt32(Convert.ToInt32(hidIdUsuario.Value))
        objBE_Oferta.id_detsubasta = Convert.ToInt32(Convert.ToInt32(hidIdDetSubasta.Value))
        objBE_Oferta.nestado = MyConfig.getEstadoOfertaActiva
        objBE_Oferta.nprecio_oferta = Convert.ToDecimal(txtOferta.Text)

        Dim objBLT_Oferta As New BLT_Oferta
        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_Oferta.Insertar(objBE_Oferta, objBE_Oferta.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            Util.RegisterAsyncAlert(upLista, "__ExitoRegistrarOferta__", Resources.SSA_Mensajes.msgOfertaExitoRegistrar)
            dnvListado.InvokeBindKeepPage()
            Util.RegisterAsyncPressButton(upLista, "__CerrarPopupDetalleOferta__", btnCerrarDetalleOferta.ClientID)
        Else
            Util.RegisterAsyncAlert(upLista, "__ErrorRegistrarOferta__", Resources.SSA_Mensajes.msgOfertaErrorRegistrar)
        End If
    End Sub

#End Region

#Region " Metodos "

    Private Function BindGridLista(ByVal sender As Object, ByVal e As EventArgs) As DataNavigatorParams

        Dim objBE_ListaOferta As New BE_ListaOferta
        Dim objBLT_Oferta As New BLT_Oferta
        Dim objPaginador As New Paginador

        objPaginador.NumeroRegistros = Int32.MaxValue 'grvLista.PageSize
        objPaginador.NumeroPagina = dnvListado.CurrentPage

        objBE_ListaOferta.id_tipo_articulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue.ToString)
        objBE_ListaOferta.id_subtipo_articulo = Convert.ToInt32(ddlSubTipoArticulo.SelectedValue.ToString)
        objBE_ListaOferta.id_usuario = Convert.ToInt32(hidIdUsuario.Value)
        objBE_ListaOferta.nestado5 = MyConfig.getEstadoSubastaPublicada
        objBE_ListaOferta.nestado3 = MyConfig.getEstadoDetalleSubastaEnSubasta
        objBE_ListaOferta.nestado = MyConfig.getEstadoOfertaEliminada
        objBE_ListaOferta.nestado4 = MyConfig.getEstadoSubastaEliminada
        objBE_ListaOferta.nestado2 = MyConfig.getEstadoDetalleSubastaEliminado

        Dim lista As IList = objBLT_Oferta.ListarPaginado(objBE_ListaOferta, objPaginador)

        dnvListado.Visible = (objPaginador.TotalResgistros > objPaginador.NumeroRegistros)
        Return New DataNavigatorParams(lista, objPaginador.TotalResgistros)

    End Function

    Private Sub CargarCombosTipo()
        'El combo subtipo de articulo se va llenar en el index del TIPO_ARTICULO
        Dim objBLT_TipoArticulo As New BLT_TipoArticulo
        Dim objBLT_Parametro As New BLT_Parametro

        Dim objBE_TipoArticulo As New BE_TipoArticulo
        objBE_TipoArticulo.nEstado = MyConfig.getTipoArticuloActivo
        Util.VinculaDropDownList(ddlTipoArticulo, objBLT_TipoArticulo.Listar(objBE_TipoArticulo), "IdTipoArticulo", "cDescripcion")
        Util.AddAllItemToDDL(ddlTipoArticulo)
        Util.AddAllItemToDDL(ddlSubTipoArticulo)

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
            sScript.AppendLine("function OpenDetalleTecnico(){")
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnOpenDetalleTecnico.ClientID)
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")
            sScript.AppendLine("function RegistrarOferta(){")
            sScript.AppendLine("    if(Page_ClientValidate('Ofertar')){")
            sScript.AppendLineFormat("  return confirm('{0}');", Resources.SSA_Mensajes.msgOfertaConfirmarRegistrar)
            sScript.AppendLine("    }")
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")
            sScript.AppendLine("function EliminarItem(idArticulo, idOferta){")
            sScript.AppendLineFormat("if(confirm('{0}'))", Resources.SSA_Mensajes.msgOfertaConfirmarEliminar)
            sScript.AppendLine("    {")
            sScript.AppendLineFormat("document.getElementById('{0}').value=idArticulo;", hidIdArticulo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value = idOferta;", hidIdOferta.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnEliminar.ClientID)
            sScript.AppendLine("    }")
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
            sScript.AppendLineFormat("document.getElementById('{0}').disabled='';", btnRegistrarOferta.ClientID)
            sScript.AppendLine("    if(idOferta>0)")
            sScript.AppendLine("    {")
            sScript.AppendLineFormat("document.getElementById('{0}').value = montoOferta;", txtOferta.ClientID)
            sScript.AppendLineFormat("  document.getElementById('{0}').disabled='disabled';", btnRegistrarOferta.ClientID)
            sScript.AppendLine("    }")
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnDetalleSubasta.ClientID)
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

End Class
