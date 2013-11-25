Imports System.Collections.Generic
Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common
Imports QNET.Web.UI.Controls

Partial Class Subasta_SSA_NuevaSubasta
    Inherits QNET.Common.BasePage

    Dim PaginaGoto As String = "SSA_PublicarSubasta.aspx"

#Region " Propiedades "

    Public Property MiDetalleSubasta() As List(Of BE_Detalle_Subasta)
        Get
            Return DirectCast(Session("__miDetalle__"), List(Of BE_Detalle_Subasta))
        End Get
        Set(ByVal value As List(Of BE_Detalle_Subasta))
            Session("__miDetalle__") = value
        End Set
    End Property

#End Region

#Region " Pagina "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ScriptCliente()
        ScriptCliente_BusquedaArticulo()
        ScriptCustomValidator()
        GridEffects.RegisterGridForEffects(grvLista)
        GridEffects.RegisterGridForEffects(grvListaDetalle)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dnvListado.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridListaArticulos)
        If Not Page.IsPostBack Then
            MiDetalleSubasta = Nothing

            If String.IsNullOrEmpty(Request.QueryString("ItemSubmit")) Then
                Response.Redirect(PaginaGoto)
                Exit Sub
            Else
                Select Case Request.QueryString("ItemSubmit")
                    Case MyConfig.getParamGetAdd
                        CType(Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.SubastaNuevo
                        btnGrabar.OnClientClick = String.Format("return RegistrarItem('{0}')", Resources.SSA_Mensajes.msgSubastaConfirmarRegistro)
                    Case MyConfig.getParamGetUpdate
                        btnGrabar.Enabled = False
                        btnAgregar.Enabled = False
                        CType(Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.SubastaModificar
                        btnGrabar.OnClientClick = String.Format("return RegistrarItem('{0}')", Resources.SSA_Mensajes.msgSubastaConfirmarModificar)
                        If Not String.IsNullOrEmpty(Request.QueryString("idIttem")) Then
                            txtIdSubasta.Text = Request.QueryString("idIttem").ToString
                        Else
                            txtIdSubasta.Text = "0"
                        End If
                        Dim objBLT_Subasta As New BLT_Subasta
                        Dim objBE_Subasta As BE_Subasta
                        objBE_Subasta = objBLT_Subasta.Seleccionar(Convert.ToInt32(txtIdSubasta.Text), MyConfig.getEstadoDetalleSubastaEliminado)
                        If objBE_Subasta IsNot Nothing Then
                            SeleccionarDatos(objBE_Subasta)
                        End If
                    Case Else
                        Response.Redirect(PaginaGoto)
                        Exit Sub
                End Select
            End If
            'Setear Propiedades de Controles
            hlkFoto1.NavigateUrl = "JavaScript:ColocarFoto(1)"
            hlkFoto2.NavigateUrl = "JavaScript:ColocarFoto(2)"
            hlkFoto3.NavigateUrl = "JavaScript:ColocarFoto(3)"
            btnCancelar.OnClientClick = "return Cancelar()"
            btnAgregar.OnClientClick = "return AgregarArticulo()"
            grvLista.EmptyDataText = Resources.SSA_Texto.FilasVacias
            CargarCombosTipo()
            'revPrecio.ValidationExpression = MyConfig.getExpresionDecimales
            'revIndemnizacion.ValidationExpression = MyConfig.getExpresionDecimales
            'txtCodArticulo.Attributes.Add("onkeypress", "SoloEnteros()")
            'txtAnno.Attributes.Add("onkeypress", "SoloEnteros()")
            'txtNroSiniestro.Attributes.Add("onkeypress", "SoloEnteros()")
            'txtPrecio.Attributes.Add("onkeypress", "SoloDecimales(this)")
            'txtIndemnizacion.Attributes.Add("onkeypress", "SoloDecimales(this)")
        End If
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim objBLT_Subasta As New BLT_Subasta
        Dim objBE_Subasta As New BE_Subasta
        Dim BE As BE_Detalle_Subasta
        Dim Tx As ResultadoTransaccion = Nothing
        Dim _MiDetalle As New List(Of BE_Detalle_Subasta)

        If String.IsNullOrEmpty(txtIdSubasta.Text) Then
            objBE_Subasta.id_subasta = 0
        Else
            objBE_Subasta.id_subasta = Convert.ToInt32(txtIdSubasta.Text)
        End If
        objBE_Subasta.dfpublicacion = Convert.ToDateTime(txtFechaPubli.Text)
        objBE_Subasta.dfinicio = Convert.ToDateTime(txtFechaIni.Text)
        objBE_Subasta.dfinal = Convert.ToDateTime(txtFechaFin.Text)
        objBE_Subasta.nestadsub = MyConfig.getEstadoSubastaPublicada
        objBE_Subasta.nestado = MyConfig.getEstadoSubastaActiva
        For Each gvr As GridViewRow In grvListaDetalle.Rows
            BE = New BE_Detalle_Subasta
            BE.id_detsubasta = Convert.ToInt32(grvListaDetalle.DataKeys(gvr.RowIndex).Item("id_detsubasta").ToString)
            BE.BE_Articulo.id_articulo = Convert.ToInt32(grvListaDetalle.DataKeys(gvr.RowIndex).Item("id_articulo").ToString)
            BE.BE_Articulo.nestado = MyConfig.getEstadoArticuloEnSubasta
            BE.nestdetsub = MyConfig.getEstadoDetalleSubastaEnSubasta 'MyConfig.getEstadoDetalleSubastaRegistrado
            BE.nestado = MyConfig.getEstadoDetalleSubastaRegistrado
            _MiDetalle.Add(BE)
        Next
        objBE_Subasta.MiDetalle = _MiDetalle

        If Request.QueryString("ItemSubmit").Equals(MyConfig.getParamGetUpdate) Then

        Else
            Tx = objBLT_Subasta.Insertar(objBE_Subasta, GlobalEntity.Instance.Usuario.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                Util.RegisterScript(upLista, "__ExitoRegistrar__", String.Format("Registrar('{0}')", Resources.SSA_Mensajes.msgSubastaExitoRegistrar))
            Else
                Util.RegisterAsyncAlert(upLista, "__ErrorRegistrar__", Resources.SSA_Mensajes.msgSubastaErrorRegistrar)
            End If
        End If
    End Sub

    Protected Sub grvListaDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_Detalle_Subasta = CType(e.Row.DataItem, BE_Detalle_Subasta)
            If BE.BE_Articulo.canio = 0 Then e.Row.Cells(5).Text = ""
            Dim hlkEliminar As HyperLink = DirectCast(e.Row.FindControl("hlkEliminar"), HyperLink)
            hlkEliminar.NavigateUrl = String.Format("JavaScript:EliminarItem('{0}')", BE.id_articulo)
            hlkEliminar.Enabled = btnGrabar.Enabled
        End If
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim IdArticulo As Integer = Convert.ToInt32(hidIdArticuloDetalle.Value)

        Dim _MiDetalleSubasta As New List(Of BE_Detalle_Subasta)
        If MiDetalleSubasta IsNot Nothing Then
            Dim _item As BE_Detalle_Subasta = Nothing
            _MiDetalleSubasta = DirectCast(MiDetalleSubasta, List(Of BE_Detalle_Subasta))
            For Each item As BE_Detalle_Subasta In _MiDetalleSubasta
                If item.id_articulo = IdArticulo Then
                    _item = item
                    Exit For
                End If
            Next
            If _item IsNot Nothing Then
                _MiDetalleSubasta.Remove(_item)
            End If
            MiDetalleSubasta = _MiDetalleSubasta
            grvListaDetalle.DataSource = MiDetalleSubasta
            grvListaDetalle.DataBind()
        End If
    End Sub

#End Region

#Region " Metodos "

    Private Sub SeleccionarDatos(ByVal objBE_Subasta As BE_Subasta)
        txtFechaPubli.Text = objBE_Subasta.s_dfpublicacion
        txtFechaIni.Text = objBE_Subasta.s_dfinicio
        txtFechaFin.Text = objBE_Subasta.s_dfinal
        MiDetalleSubasta = objBE_Subasta.MiDetalle
        grvListaDetalle.DataSource = MiDetalleSubasta
        grvListaDetalle.DataBind()
    End Sub

    Private Sub CargarCombosTipo()
        'El combo subtipo de articulo se va llenar en el index del TIPO_ARTICULO
        Dim objBLT_TipoArticulo As New BLT_TipoArticulo
        Dim objBLT_Parametro As New BLT_Parametro

        Dim objBE_TipoArticulo As New BE_TipoArticulo
        objBE_TipoArticulo.nEstado = MyConfig.getTipoArticuloActivo
        Util.VinculaDropDownList(ddlTipoArticulo, objBLT_TipoArticulo.Listar(objBE_TipoArticulo), "IdTipoArticulo", "cDescripcion")
        Util.AddAllItemToDDL(ddlTipoArticulo)

        Dim objBLT_SubTipoArticulo As New BLT_SubTipoArticulo
        Dim objBE_SubTipoArticulo As New BE_SubTipoArticulo
        objBE_SubTipoArticulo.IdTipoArticulo = 0
        objBE_SubTipoArticulo.nEstado = MyConfig.getSubTipoArticuloActivo
        Util.VinculaDropDownList(ddlSubTipoArticulo, objBLT_SubTipoArticulo.Listar(objBE_SubTipoArticulo), "IdSubTipoArticulo", "cDescripcion")
        Util.AddAllItemToDDL(ddlSubTipoArticulo)
    End Sub

#End Region

#Region " Detalle Técnico del Artículo "

    Protected Sub btnSeleccionarDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeleccionarDetalle.Click
        Dim objBLT_Articulo As New BLT_Articulo
        Dim objBE_Articulo As BE_Articulo
        objBE_Articulo = objBLT_Articulo.Seleccionar(Convert.ToInt32(hidIdArticulo.Value))
        lblCodigoArticuloDetalle.Text = ""
        ltDatosArticuloDetalle.Text = ""
        ltDescripcionArticuloDetalle.Text = ""
        If objBE_Articulo IsNot Nothing Then
            lblCodigoArticuloDetalle.Text = objBE_Articulo.ccodart
            ltDatosArticuloDetalle.Text = objBE_Articulo.cdescrip_breve
            ltDescripcionArticuloDetalle.Text = objBE_Articulo.Cdattec
            Util.RegisterAsyncPressButton(upDetalleTecnico, "__AbrirDetalleTecnicoArticulo__", btnOpenDetalle.ClientID)
        End If
    End Sub

#End Region

#Region " Busqueda de Articulo "

    Protected Sub btnAceptarBusquedaArticulo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarBusquedaArticulo.Click
        'grvListaDetalle. (MiDetalleSubasta)
        Dim BE As BE_Detalle_Subasta
        Dim _MiDetalleSubasta As New List(Of BE_Detalle_Subasta)
        If MiDetalleSubasta IsNot Nothing Then
            _MiDetalleSubasta = DirectCast(MiDetalleSubasta, List(Of BE_Detalle_Subasta))
        End If

        For Each gvr As GridViewRow In grvLista.Rows
            If DirectCast(gvr.FindControl("chkSel"), CheckBox).Checked Then
                BE = New BE_Detalle_Subasta
                BE.id_detsubasta = 0
                BE.BE_Articulo.id_articulo = Convert.ToInt32(grvLista.DataKeys(gvr.RowIndex).Item("id_articulo").ToString)
                BE.BE_Articulo.ccodart = Convert.ToInt32(gvr.Cells(1).Text)
                BE.BE_Articulo.cdescrip_breve = gvr.Cells(2).Text
                BE.BE_Articulo.cmarca = gvr.Cells(3).Text
                BE.BE_Articulo.cmodelo = gvr.Cells(4).Text
                If Not String.IsNullOrEmpty(gvr.Cells(5).Text) Then
                    BE.BE_Articulo.canio = Convert.ToInt32(gvr.Cells(5).Text)
                End If
                BE.nestdetsub = MyConfig.getEstadoDetalleSubastaRegistrado
                BE.nestado = MyConfig.getEstadoDetalleSubastaActiva
                BE.des_estado = "REGISTRADO"
                _MiDetalleSubasta.Add(BE)
            End If
        Next

        MiDetalleSubasta = _MiDetalleSubasta
        grvListaDetalle.DataSource = MiDetalleSubasta
        grvListaDetalle.DataBind()

        Util.RegisterAsyncPressButton(upLista, "__CerrarBusquedaArticulo__", btnCerrarBusquedaArticulo.ClientID)
    End Sub

    Private Function BindGridListaArticulos(ByVal sender As Object, ByVal e As EventArgs) As DataNavigatorParams
        Dim objBE_Articulo As New BE_Articulo
        Dim objBLT_Articulo As New BLT_Articulo
        Dim objPaginador As New Paginador
        Dim MisCodigosDeDetalle As String = String.Empty

        objPaginador.NumeroRegistros = grvLista.PageSize
        objPaginador.NumeroPagina = dnvListado.CurrentPage

        If txtCodart.Text.Length = 0 Then
            objBE_Articulo.ccodart = -1
        Else
            objBE_Articulo.ccodart = Convert.ToInt32(txtCodart.Text)
        End If

        If MiDetalleSubasta IsNot Nothing Then
            For Each item As BE_Detalle_Subasta In MiDetalleSubasta
                MisCodigosDeDetalle = String.Format("{0},{1}", item.BE_Articulo.id_articulo, MisCodigosDeDetalle)
            Next
        End If

        objBE_Articulo.nestado = MyConfig.getEstadoArticuloRegistrado
        objBE_Articulo.id_tipo_articulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue.ToString)
        objBE_Articulo.id_subtipo_articulo = Convert.ToInt32(ddlSubTipoArticulo.SelectedValue.ToString)
        objBE_Articulo.nestado2 = MyConfig.getEstadoArticuloEliminado

        Dim lista As IList = objBLT_Articulo.ListarPaginado(objBE_Articulo, MisCodigosDeDetalle, objPaginador)

        dnvListado.Visible = (objPaginador.TotalResgistros > objPaginador.NumeroRegistros)
        Return New DataNavigatorParams(lista, objPaginador.TotalResgistros)

    End Function

    Protected Sub btnBusquedaArticulo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusquedaArticulo.Click
        dnvListado.InvokeBind()
    End Sub

    Protected Sub grvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_Articulo = CType(e.Row.DataItem, BE_Articulo)
            Dim hlkFotos As HyperLink = DirectCast(e.Row.FindControl("hlkFotos"), HyperLink)
            Dim hlkDetalle As HyperLink = DirectCast(e.Row.FindControl("hlkDetalle"), HyperLink)
            'setear los hyperlink
            If String.IsNullOrEmpty(BE.crutimage1) And String.IsNullOrEmpty(BE.crutimage2) And String.IsNullOrEmpty(BE.crutimage3) Then
                hlkFotos.NavigateUrl = "JavaScript:MensajeSinFotos()"
            Else
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
                hlkFotos.NavigateUrl = String.Format("JavaScript:VerFotosArticulo('{0}','{1}','{2}')", foto1, foto2, foto3)
            End If
            hlkDetalle.NavigateUrl = String.Format("JavaScript:VerDetalleArticulo('{0}')", BE.id_articulo)
            'seteamos celdas
            If BE.canio = 0 Then e.Row.Cells(5).Text = ""
        End If
    End Sub

#End Region

#Region " Scripts "

    Private Sub ScriptCustomValidator()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCustomValidator__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function fComparaFecha(source, arguments)")
            sScript.AppendLine("{")
            sScript.AppendLineFormat("Fini = document.getElementById('{0}').value;", txtFechaIni.ClientID)
            sScript.AppendLineFormat("Ffin = document.getElementById('{0}').value;", txtFechaFin.ClientID)
            sScript.AppendLine("   arguments.IsValid = ComparaFechas(Fini,Ffin);")
            sScript.AppendLine("}")
            sScript.AppendLine("function fSeleccion(source, arguments)")
            sScript.AppendLine("{")
            sScript.AppendLine("    f = document.forms[0].elements")
            sScript.AppendLine("    i = false;")
            sScript.AppendLine("    for(x=0; x<f.length; x++)")
            sScript.AppendLine("    {")
            sScript.AppendLine("        if(f[x].type=='checkbox'){")
            sScript.AppendLine("            if(f[x].checked){")
            sScript.AppendLine("                i = true;")
            sScript.AppendLine("                break;")
            sScript.AppendLine("            }")
            sScript.AppendLine("        }")
            sScript.AppendLine("    }")
            sScript.AppendLine("    arguments.IsValid = i;")
            sScript.AppendLine("}")
            sScript.AppendLine("function fExistenFilas(source, arguments)")
            sScript.AppendLine("{")
            sScript.AppendLineFormat("obj = document.getElementById('{0}')", grvListaDetalle.ClientID)
            sScript.AppendLine("    arguments.IsValid = (obj!=null)")
            sScript.AppendLine("}")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCustomValidator__", sScript.ToString(), True)
        End If
    End Sub

    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function RegistrarItem(msg)")
            sScript.AppendLine("{")
            sScript.AppendLine("    if(Page_ClientValidate('Registrar'))")
            sScript.AppendLine("    {")
            sScript.AppendLine("        return confirm(msg)")
            sScript.AppendLine("    }")
            sScript.AppendLine("}")
            sScript.AppendLine("function Cancelar()")
            sScript.AppendLine("{")
            sScript.AppendLineFormat("  location.href = '{0}'; ", PaginaGoto)
            sScript.AppendLine("        return false;")
            sScript.AppendLine("}")
            sScript.AppendLine("function Registrar(msg)")
            sScript.AppendLine("{")
            sScript.AppendLine("    alert(msg);")
            sScript.AppendLineFormat("  location.href = '{0}'; ", PaginaGoto)
            sScript.AppendLine("}")
            sScript.AppendLine("function AgregarArticulo()")
            sScript.AppendLine("{")
            sScript.AppendLine("    if(Page_ClientValidate('Agregar')){")
            sScript.AppendLineFormat("document.getElementById('{0}').value='';", txtCodart.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').selectedIndex=0;", ddlTipoArticulo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').selectedIndex=0;", ddlSubTipoArticulo.ClientID)
            sScript.AppendLineFormat("document.getElementById('__ContenidoBusqueda__').innerHTML='';", grvLista.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click()", btnBuscar.ClientID)
            sScript.AppendLine("    }")
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")
            sScript.AppendLine("function EliminarItem(IdArticulo)")
            sScript.AppendLine("{")
            sScript.AppendLineFormat("if(confirm('{0}'))", Resources.SSA_Mensajes.msgDetalleSubastaConfirmarEliminar)
            sScript.AppendLine("      {")
            sScript.AppendLineFormat("    document.getElementById('{0}').value = IdArticulo;", hidIdArticuloDetalle.ClientID)
            sScript.AppendLineFormat("    document.getElementById('{0}').click()", btnEliminar.ClientID)
            sScript.AppendLine("      }")
            sScript.AppendLine("}")
            sScript.AppendLine("function ComparaFechas(dtf1, dtf2)")
            sScript.AppendLine("{")
            sScript.AppendLine("//la fecha 2 debe ser necesariamente mayor a fecha1")
            sScript.AppendLine("   if(dtf1.length==0 || dtf1.length==0 )")
            sScript.AppendLine("   {")
            sScript.AppendLine("       return true;")
            sScript.AppendLine("   }")
            sScript.AppendLine("	fi = dtf1.split('/');")
            sScript.AppendLine("	ff = dtf2.split('/');")
            sScript.AppendLine("	_fechai = fi[2]*10000 + fi[1]*100 + fi[0];")
            sScript.AppendLine("	_fechaf = ff[2]*10000 + ff[1]*100 + ff[0];")
            sScript.AppendLine("	n = _fechaf - _fechai;")
            sScript.AppendLine("	if(n>0) return true;")
            sScript.AppendLine("	else return false;")
            sScript.AppendLine("}")
            sScript.AppendLine("")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), True)
        End If
    End Sub

    Private Sub ScriptCliente_BusquedaArticulo()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente_BusquedaArticulo__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function ColocarFoto(n)")
            sScript.AppendLine("{")
            sScript.AppendLine("    ruta = '';")
            sScript.AppendLine("    switch(n){")
            sScript.AppendLine("        case 1:")
            sScript.AppendLineFormat("      ruta = document.getElementById('{0}').value", hidRutaImagen1.ClientID)
            sScript.AppendLine("            break;")
            sScript.AppendLine("        case 2:")
            sScript.AppendLineFormat("      ruta = document.getElementById('{0}').value", hidRutaImagen2.ClientID)
            sScript.AppendLine("            break;")
            sScript.AppendLine("        case 3:")
            sScript.AppendLineFormat("      ruta = document.getElementById('{0}').value", hidRutaImagen3.ClientID)
            sScript.AppendLine("            break;")
            sScript.AppendLine("    }")
            sScript.AppendLineFormat("      document.getElementById('{0}').src = ruta;", imgFoto.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("function VerDetalleArticulo(idArticulo){")
            sScript.AppendLineFormat("document.getElementById('{0}').value=idArticulo", hidIdArticulo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click()", btnSeleccionarDetalle.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("function MensajeSinFotos(){")
            sScript.AppendLineFormat("alert('{0}')", Resources.SSA_Mensajes.msgArticuloNoHayFotos)
            sScript.AppendLine("}")
            sScript.AppendLine("function VerFotosArticulo(img1, img2, img3){")
            sScript.AppendLineFormat("document.getElementById('{0}').value=img1", hidRutaImagen1.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=img2", hidRutaImagen2.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=img3", hidRutaImagen3.ClientID)
            sScript.AppendLine("      ColocarFoto(1);")
            sScript.AppendLineFormat("document.getElementById('{0}').click()", btnOpenFotos.ClientID)
            sScript.AppendLine("}")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente_BusquedaArticulo__", sScript.ToString(), True)
        End If
    End Sub

#End Region

End Class
