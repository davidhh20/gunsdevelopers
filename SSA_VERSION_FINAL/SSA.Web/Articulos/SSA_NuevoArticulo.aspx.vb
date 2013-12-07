Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common

Partial Class Articulos_SSA_NuevoArticulo
    Inherits QNET.Common.BasePage

#Region " Page "
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ScriptCliente()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If String.IsNullOrEmpty(Request.QueryString("ItemSubmit")) Then
                Response.Redirect("SSA_ListarArticulos.aspx")
                Exit Sub
            Else
                Select Case Request.QueryString("ItemSubmit")
                    Case MyConfig.getParamGetAdd
                        CType(Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.ArticuloNuevo
                        CargarCombos()
                        ddlEstado.SelectedValue = MyConfig.getEstadoArticuloRegistrado
                        btnRegistrar.OnClientClick = String.Format("return RegistrarItem('{0}')", Resources.SSA_Mensajes.msgArticuloConfirmarRegistro)
                        btnRegistrar.Text = "Registrar"
                    Case MyConfig.getParamGetUpdate
                        CType(Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.ArticuloModificar
                        hidIdArticulo.Value = Request.QueryString("idIttem").ToString
                        Dim objBLT_Articulo As New BLT_Articulo
                        Dim objBE_Articulo As BE_Articulo
                        objBE_Articulo = objBLT_Articulo.Seleccionar(Convert.ToInt32(hidIdArticulo.Value))
                        If objBE_Articulo IsNot Nothing Then
                            CargarCombos()
                            btnRegistrar.Text = "Modificar"
                            SeleccionarDatos(objBE_Articulo)
                            If Convert.ToInt32(ddlEstado.SelectedValue.ToString) = MyConfig.getEstadoArticuloRegistrado Then
                                btnRegistrar.OnClientClick = String.Format("return RegistrarItem('{0}')", Resources.SSA_Mensajes.msgArticuloConfirmarModificar)
                            Else
                                btnRegistrar.OnClientClick = String.Format("alert('{0}') ; return false;", Resources.SSA_Mensajes.msgArticuloNoModificar)
                            End If
                        End If
                    Case Else
                            Response.Redirect("SSA_ListarArticulos.aspx")
                            Exit Sub
                End Select
            End If
            'Setear Propiedades de Controles
            btnCancelar.OnClientClick = "return Cancelar()"
            revPrecio.ValidationExpression = MyConfig.getExpresionDecimales
            revIndemnizacion.ValidationExpression = MyConfig.getExpresionDecimales
            txtCodArticulo.Attributes.Add("onkeypress", "SoloEnteros()")
            txtAnno.Attributes.Add("onkeypress", "SoloEnteros()")
            txtNroSiniestro.Attributes.Add("onkeypress", "SoloEnteros()")
            txtPrecio.Attributes.Add("onkeypress", "SoloDecimales(this)")
            txtIndemnizacion.Attributes.Add("onkeypress", "SoloDecimales(this)")
        End If
    End Sub

    Protected Sub ddlTipoArticulo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoArticulo.SelectedIndexChanged
        Cargar_SubTipo()
    End Sub

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        Dim objBLT_Articulo As New BLT_Articulo
        Dim BE As New BE_Articulo

        BE.id_articulo = Convert.ToInt32(hidIdArticulo.Value)
        BE.id_tipo_articulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue.ToString)
        BE.id_subtipo_articulo = Convert.ToInt32(ddlSubTipoArticulo.SelectedValue.ToString)
        If String.IsNullOrEmpty(txtCodArticulo.Text) Then
            BE.ccodart = 0
        Else
            BE.ccodart = Convert.ToInt32(txtCodArticulo.Text)
        End If
        If String.IsNullOrEmpty(txtAnno.Text) Then
            BE.canio = 0
        Else
            BE.canio = Convert.ToInt32(txtAnno.Text)
        End If
        BE.cdescrip_breve = txtDescripcion.Text
        If SinEnter_Tab_Blancos(txtDatosTecnicos.Text) = 0 Then
            BE.Cdattec = String.Empty
        Else
            BE.Cdattec = txtDatosTecnicos.Text.Trim
        End If
        BE.cmarca = txtMarca.Text.Trim.ToUpper
        BE.cmodelo = txtModelo.Text.Trim.ToUpper
        If String.IsNullOrEmpty(txtNroSiniestro.Text) Then
            BE.nsiniestro = 0
        Else
            BE.nsiniestro = Convert.ToInt32(txtNroSiniestro.Text)
        End If
        BE.nprecio_base = Convert.ToDecimal(txtPrecio.Text)
        If String.IsNullOrEmpty(txtIndemnizacion.Text) Then
            BE.nindemnizacion = Nothing
        Else
            BE.nindemnizacion = txtIndemnizacion.Text
        End If
        If SinEnter_Tab_Blancos(txtDatosSiniestro.Text) = 0 Then
            BE.cdatos_siniestro = String.Empty
        Else
            BE.cdatos_siniestro = txtDatosSiniestro.Text.Trim
        End If
        If SinEnter_Tab_Blancos(txtDetalleSiniestro.Text) = 0 Then
            BE.cdet_siniestro = String.Empty
        Else
            BE.cdet_siniestro = txtDetalleSiniestro.Text.Trim
        End If
        BE.crutimage1 = Ctl_Foto1.RutaImagen
        BE.crutimage2 = Ctl_Foto2.RutaImagen
        BE.crutimage3 = Ctl_Foto3.RutaImagen

        BE.nestado = Convert.ToInt32(ddlEstado.SelectedValue.ToString)

        Dim Tx As ResultadoTransaccion = Nothing

        If Request.QueryString("ItemSubmit").Equals(MyConfig.getParamGetUpdate) Then
            Tx = objBLT_Articulo.Modificar(BE, GlobalEntity.Instance.Usuario.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                Util.RegisterScript(upSubTipo, "__ExitoModificar__", String.Format("Registrar('{0}')", Resources.SSA_Mensajes.msgArticuloModificarExito))
            Else
                If Tx.ObjException.GetType() Is Type.GetType("System.Data.SqlClient.SqlException, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089") Then
                    If DirectCast(Tx.ObjException, System.Data.SqlClient.SqlException).Number = 2627 Then
                        Util.RegisterAsyncAlert(upSubTipo, "__ErrorRegistrar__", Resources.SSA_Mensajes.msgArticuloErrorUnique)
                        Exit Sub
                    End If
                End If
                Util.RegisterAsyncAlert(upSubTipo, "__ErrorModificar__", Resources.SSA_Mensajes.msgArticuloModificarError)
            End If
        Else
            Tx = objBLT_Articulo.Insertar(BE, GlobalEntity.Instance.Usuario.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                Util.RegisterScript(upSubTipo, "__ExitoRegistrar__", String.Format("Registrar('{0}')", Resources.SSA_Mensajes.msgArticuloRegistroExito))
            Else
                If Tx.ObjException.GetType() Is Type.GetType("System.Data.SqlClient.SqlException, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089") Then
                    If DirectCast(Tx.ObjException, System.Data.SqlClient.SqlException).Number = 2627 Then
                        Util.RegisterAsyncAlert(upSubTipo, "__ErrorRegistrar__", Resources.SSA_Mensajes.msgArticuloErrorUnique)
                        Exit Sub
                    End If
                End If
                Util.RegisterAsyncAlert(upSubTipo, "__ErrorRegistrar__", Resources.SSA_Mensajes.msgArticuloRegistroError)
            End If
        End If

    End Sub

#End Region

#Region " Métodos "

    Private Sub SeleccionarDatos(ByVal BE As BE_Articulo)
        txtCodArticulo.Text = BE.ccodart.ToString
        ddlTipoArticulo.SelectedValue = BE.id_tipo_articulo
        Cargar_SubTipo()
        ddlSubTipoArticulo.SelectedValue = BE.id_subtipo_articulo
        txtMarca.Text = BE.cmarca
        txtModelo.Text = BE.cmodelo
        If BE.canio <> 0 Then txtAnno.Text = BE.canio
        txtDescripcion.Text = BE.cdescrip_breve
        txtDatosTecnicos.Text = BE.Cdattec
        If BE.nsiniestro <> 0 Then txtNroSiniestro.Text = BE.nsiniestro.ToString
        txtPrecio.Text = BE.nprecio_base.ToString
        If BE.nindemnizacion.HasValue Then txtIndemnizacion.Text = BE.nindemnizacion.Value
        txtDatosSiniestro.Text = BE.cdatos_siniestro
        txtDetalleSiniestro.Text = BE.cdet_siniestro
        ddlEstado.SelectedValue = BE.nestado
        Ctl_Foto1.MostrarImagen(BE.crutimage1, 0)
        Ctl_Foto2.MostrarImagen(BE.crutimage2, 0)
        Ctl_Foto3.MostrarImagen(BE.crutimage3, 0)
    End Sub

    Private Sub Cargar_SubTipo() 'Si muestra el estado eliminado
        Dim objBLT_SubTipoArticulo As New BLT_SubTipoArticulo
        Dim objBE_SubTipoArticulo As New BE_SubTipoArticulo
        objBE_SubTipoArticulo.IdTipoArticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue.ToString)
        objBE_SubTipoArticulo.nEstado = MyConfig.getSubTipoArticuloActivo
        Util.VinculaDropDownList(ddlSubTipoArticulo, objBLT_SubTipoArticulo.Listar(objBE_SubTipoArticulo), "IdSubTipoArticulo", "cDescripcion")
        Util.AddSelectItemToDDL(ddlSubTipoArticulo)
    End Sub
    Private Sub CargarCombos()
        'El combo subtipo de articulo se va llenar en el index del TIPO_ARTICULO
        Dim objBLT_TipoArticulo As New BLT_TipoArticulo
        Dim objBLT_Parametro As New BLT_Parametro

        Dim objBE_TipoArticulo As New BE_TipoArticulo
        objBE_TipoArticulo.nEstado = MyConfig.getTipoArticuloActivo
        Util.VinculaDropDownList(ddlTipoArticulo, objBLT_TipoArticulo.Listar(objBE_TipoArticulo), "IdTipoArticulo", "cDescripcion")
        Util.AddSelectItemToDDL(ddlTipoArticulo)

        Util.AddSelectItemToDDL(ddlSubTipoArticulo)

        'Agregamos todos los estados y al final quitamos el Estado : Eliminado
        Dim objBE_Parametro As New BE_Parametro
        objBE_Parametro.nclase = MyConfig.getGrupoEstadoArticulo
        'objBE_Parametro.nSubclase = MyConfig.getSubGrupoEstadoArticulo
        objBE_Parametro.nEstado = MyConfig.getParametroEstadoActivo
        Util.VinculaDropDownList(ddlEstado, objBLT_Parametro.Listar(objBE_Parametro), "IdParametro", "cDescripcion")
        ddlEstado.SelectedValue = MyConfig.getEstadoArticuloEliminado
        ddlEstado.Items.Remove(ddlEstado.SelectedItem)

    End Sub

    'retorna la longitud del campo sin ENTER, TAB ni Espacios 
    Private Function SinEnter_Tab_Blancos(ByVal texto As String) As Integer
        Return texto.Replace(Chr(10), "").Replace(Chr(13), "").Trim.Length
    End Function
#End Region

#Region " Scripts "
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
            sScript.AppendLine("function Registrar(msg)")
            sScript.AppendLine("{")
            sScript.AppendLine("    alert(msg);")
            sScript.AppendLine("    location.href = 'SSA_ListarArticulos.aspx'; ")
            sScript.AppendLine("}")
            sScript.AppendLine("function Cancelar()")
            sScript.AppendLine("{")
            sScript.AppendLine("    location.href = 'SSA_ListarArticulos.aspx' ")
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), True)
        End If
    End Sub
#End Region

    
    
End Class
