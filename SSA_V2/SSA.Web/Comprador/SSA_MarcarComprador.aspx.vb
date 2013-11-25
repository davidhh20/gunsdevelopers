Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common
Imports QNET.Web
Imports QNET.Web.UI.Controls

Partial Class Administrador_SSA_MarcarComprador
    Inherits QNET.Common.BasePage

#Region " Página "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ScriptCustomValidator()
        ScriptCliente()
        GridEffects.RegisterGridForEffects(grvUsuario)
        ScriptClienteModificar()
        ScriptCustomValidatorModificar()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dnvListado.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridLista)
        If Not Page.IsPostBack Then

            If String.IsNullOrEmpty(Request.QueryString("op")) Then 'esta llamando de Marcar sino de Actualizar
                DirectCast(Me.Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.CompradorMarcar
                grvUsuario.Columns(7).Visible = False
            Else
                'SETEA VALORES CUANDO ES PARA MODIFICAR LOS DATOS DE UN PERSONAL
                DirectCast(Me.Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.CompradorActualizar
                grvUsuario.Columns(6).Visible = False
                rbtNatural.Attributes.Add("onclick", "EjecutarSegunPersonaBoton()")
                rbtJuridica.Attributes.Add("onclick", "EjecutarSegunPersonaBoton()")
                txtNombre.Attributes.Add("onkeypress", "SoloNombres(0,this,0)")
                txtApellido.Attributes.Add("onkeypress", "SoloNombres(0,this,0)")
                txtDNI.Attributes.Add("onkeypress", "SoloEnteros()")
                txtRUC.Attributes.Add("onkeypress", "SoloEnteros()")
                txtTelefono.Attributes.Add("onkeypress", "SoloTelefono(this)")
                revApellido.ValidationExpression = MyConfig.getExpresionNombres
                btnBeforeRegistro.OnClientClick = "return Registrar(this)"
                btnEjecuta.OnClientClick = "return EjecutarSegunPersona()"
            End If
            txtCodUsuario.Attributes.Add("onkeypress", "SoloEnteros()")
            txtDocumento.Attributes.Add("onkeypress", "SoloDocumento(this.value.length)")
            ddlTipoDocumento.Attributes.Add("onchange", "FormateaCampos(this.selectedIndex)")
            btnCancelar.OnClientClick = "JavaScript:return Cancelar();"
            grvUsuario.EmptyDataText = Resources.SSA_Texto.FilasVacias
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        dnvListado.InvokeBind()
    End Sub

    Protected Sub grvUsuario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_Usuario = CType(e.Row.DataItem, BE_Usuario)
            Dim chk As CheckBox = DirectCast(e.Row.FindControl("chkMarca"), CheckBox)
            Dim hlk As HyperLink = DirectCast(e.Row.FindControl("hlkModificar"), HyperLink)
            Dim hidNom As HiddenField = DirectCast(e.Row.FindControl("hidNom"), HiddenField)
            Dim hidApe As HiddenField = DirectCast(e.Row.FindControl("hidApe"), HiddenField)

            hidNom.Value = BE.cnombres
            hidApe.Value = BE.capellidos
            chk.Checked = (BE.nmarca = MyConfig.getMarcaCompradorDudoso)
            chk.Attributes.Add("onclick", String.Format("SetMarcaUsuario(this.checked,'{0}')", BE.id_usuario))
            hlk.NavigateUrl = String.Format("JavaScript:OpenDet('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", _
                                                BE.id_usuario, BE.ntipo_persona, hidNom.ClientID, hidApe.ClientID, _
                                                BE.ndni, BE.nruc, BE.ccorreo, BE.ccorreoalt, BE.ctelefono)
        End If
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim objBE_Usuario As New BE_Usuario
        Dim objBLT_Usuario As New BLT_Usuario
        objBE_Usuario.id_usuario = Convert.ToInt32(hidIdUsuario.Value)
        objBE_Usuario.nmarca = Convert.ToInt32(hidTipoMarca.Value)

        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_Usuario.Marcar(objBE_Usuario, GlobalEntity.Instance.Usuario.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            Util.RegisterAsyncAlert(upLista, "__ExitoMarca__", Resources.SSA_Mensajes.msgMarcarRegistroExito)
        Else
            Util.RegisterAsyncAlert(upLista, "__ErrorMarca__", Resources.SSA_Mensajes.msgMarcarRegistroError)
        End If
    End Sub

#End Region

#Region " Modificar Usuario "

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim BE As New BE_Usuario

        BE.id_usuario = Convert.ToInt32(hidIdUsuario.Value)
        BE.capellidos = txtApellido.Text.Trim.ToUpper
        BE.cnombres = txtNombre.Text.Trim.ToUpper
        If rbtNatural.Checked Then
            BE.ntipo_persona = MyConfig.getTipoPersonaNatural
        Else
            BE.ntipo_persona = MyConfig.getTipoPersonaJuridica
        End If
        BE.ndni = txtDNI.Text
        BE.nruc = txtRUC.Text
        BE.ccorreo = txtCorreo.Text.Trim.ToLower
        BE.ccorreoalt = txtCorreo2.Text.Trim.ToLower
        BE.ctelefono = txtTelefono.Text

        Dim objBLT_Usuario As New BLT_Usuario
        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_Usuario.Modificar(BE, GlobalEntity.Instance.Usuario.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            dnvListado.InvokeBind()
            Util.RegisterAsyncAlert(upLista, "__ModificaExito__", Resources.SSA_Mensajes.msgUsuarioModificarExito)
            Util.RegisterAsyncPressButton(upLista, "__CierraModificacion__", btnCerrarDetalle.ClientID)
        Else
            If Tx.ObjException.GetType() Is Type.GetType("System.Data.SqlClient.SqlException, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089") Then
                Dim msgError As String = String.Empty
                Select Case DirectCast(Tx.ObjException, System.Data.SqlClient.SqlException).Number
                    Case 3609
                        msgError = Resources.SSA_Mensajes.msgRegistroUsuarioDuplicado
                    Case 2627
                        msgError = Resources.SSA_Mensajes.msgRegistroUsuarioDuplicado2
                    Case Else
                End Select
                If msgError.Length > 0 Then
                    Util.RegisterAsyncAlert(upLista, "__MensajeErrorUniqueDocumento__", msgError)
                    Util.RegisterScript(upLista, "__DesHabilitarBoton01__", String.Format("document.getElementById('{0}').disabled=''", btnBeforeRegistro.ClientID))
                    Exit Sub
                End If
            End If
            Util.RegisterAsyncAlert(upLista, "__ModificaError__", Resources.SSA_Mensajes.msgUsuarioModificarError)
        End If

        Util.RegisterScript(upLista, "__DesHabilitarBoton02__", String.Format("document.getElementById('{0}').disabled=''", btnBeforeRegistro.ClientID))

    End Sub

#End Region

#Region " Metodos "

    Private Function BindGridLista(ByVal sender As Object, ByVal e As EventArgs) As DataNavigatorParams
        Dim objBE_Usuario As New BE_Usuario
        Dim objBLT_Usuario As New BLT_Usuario
        Dim objPaginador As New Paginador

        objPaginador.NumeroRegistros = grvUsuario.PageSize
        objPaginador.NumeroPagina = dnvListado.CurrentPage

        Select Case Convert.ToInt32(ddlTipoDocumento.SelectedValue.ToString)
            Case 0 'x DNI
                objBE_Usuario.ndni = txtDocumento.Text
                objBE_Usuario.nruc = String.Empty
            Case 1 'x RUC
                objBE_Usuario.ndni = String.Empty
                objBE_Usuario.nruc = txtDocumento.Text
            Case Else
                objBE_Usuario.ndni = String.Empty
                objBE_Usuario.nruc = String.Empty
        End Select

        If String.IsNullOrEmpty(txtCodUsuario.Text) Then
            objBE_Usuario.id_usuario = 0
        Else
            objBE_Usuario.id_usuario = Convert.ToInt32(txtCodUsuario.Text)
        End If

        objBE_Usuario.nestado = MyConfig.getEstadoUsuarioActivo
        objBE_Usuario.nIdPerfil = MyConfig.getPerfilComprador

        Dim lista As IList = objBLT_Usuario.ListarPaginado(objBE_Usuario, objPaginador)

        dnvListado.Visible = (objPaginador.TotalResgistros > objPaginador.NumeroRegistros)
        Return New DataNavigatorParams(lista, objPaginador.TotalResgistros)

    End Function

#End Region

#Region " Scripts "

    Private Sub ScriptCustomValidator()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCustomValidator__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function fDocumento(source, arguments)")
            sScript.AppendLine("{")
            sScript.AppendLineFormat("cbo = document.getElementById('{0}')", ddlTipoDocumento.ClientID)
            sScript.AppendLineFormat("documento = document.getElementById('{0}').value;", txtDocumento.ClientID)
            sScript.AppendLine("    indice = cbo.selectedIndex ;")
            sScript.AppendLineFormat("longitud = document.getElementById('{0}').value", hidLongDocumento.ClientID)
            sScript.AppendLine("    if(indice==0){")
            sScript.AppendLine("        arguments.IsValid=true;")
            sScript.AppendLine("        return true;")
            sScript.AppendLine("    }")
            sScript.AppendLine("    arguments.IsValid = (documento.length==longitud);")
            sScript.AppendLine("}")
            sScript.AppendLine("")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCustomValidator__", sScript.ToString(), True)
        End If
    End Sub

    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function SetMarcaUsuario(checkeado, idusuario){")
            sScript.AppendLineFormat("document.getElementById('{0}').value=idusuario", hidIdUsuario.ClientID)
            sScript.AppendLine("    if(checkeado){")
            sScript.AppendLineFormat("document.getElementById('{0}').value='{1}'", hidTipoMarca.ClientID, MyConfig.getMarcaCompradorDudoso)
            sScript.AppendLine("    }else{")
            sScript.AppendLineFormat("document.getElementById('{0}').value='{1}'", hidTipoMarca.ClientID, MyConfig.getMarcaCompradorNormal)
            sScript.AppendLine("    }")
            sScript.AppendLineFormat("document.getElementById('{0}').click()", btnGrabar.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("function FormateaCampos(Indice)")
            sScript.AppendLine("{")
            sScript.AppendLine("    switch(Indice){")
            sScript.AppendLine("        case 1:")
            sScript.AppendLine("            longTexto=8")
            sScript.AppendLine("            break;")
            sScript.AppendLine("        case 2:")
            sScript.AppendLine("            longTexto=11")
            sScript.AppendLine("            break;")
            sScript.AppendLine("        default:")
            sScript.AppendLine("            longTexto=0")
            sScript.AppendLine("    }")
            sScript.AppendLineFormat("document.getElementById('{0}').value=longTexto", hidLongDocumento.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=''", txtDocumento.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').focus()", txtDocumento.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("function SoloDocumento(longTexto)")
            sScript.AppendLine("{")
            sScript.AppendLineFormat("longMax = document.getElementById('{0}').value", hidLongDocumento.ClientID)
            sScript.AppendLine("    if(longTexto>=longMax){  ")
            sScript.AppendLine("        event.keyCode=0; ")
            sScript.AppendLine("        return")
            sScript.AppendLine("    }")
            sScript.AppendLine("    SoloEnteros()")
            sScript.AppendLine("}")
            sScript.AppendLine("function Cancelar()")
            sScript.AppendLine("{")
            sScript.AppendLine("    location.href = '../Seguridad/SSA_Opciones.aspx' ")
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), True)
        End If
    End Sub

#End Region

#Region " Scripts Modificar Datos "

    Private Sub ScriptCustomValidatorModificar()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCustomValidatorModificar__") Then

            Dim sbScript As New MyStringBuilder
            sbScript.AppendLine("") '
            sbScript.AppendLine("function fRUC(source, arguments)")
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat("rbtJ = document.getElementById('{0}');", rbtJuridica.ClientID)
            sbScript.AppendLineFormat("RUC = document.getElementById('{0}').value ", txtRUC.ClientID)
            sbScript.AppendLine("      arguments.IsValid = ((rbtJ.checked)?(RUC.length!=0):true);")
            sbScript.AppendLine("}")
            sbScript.AppendLine("function fDNI(source, arguments)")
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat("rbtN = document.getElementById('{0}');", rbtNatural.ClientID)
            sbScript.AppendLineFormat("DNI = document.getElementById('{0}').value ", txtDNI.ClientID)
            sbScript.AppendLine("      arguments.IsValid = ((rbtN.checked)?(DNI.length!=0):true);")
            sbScript.AppendLine("}")
            sbScript.AppendLine("function fApellido(source, arguments)")
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat("rbtN = document.getElementById('{0}');", rbtNatural.ClientID)
            sbScript.AppendLineFormat("apellido = document.getElementById('{0}').value ", txtApellido.ClientID)
            sbScript.AppendLine("      arguments.IsValid = ((rbtN.checked)?(apellido.length!=0):true);")
            sbScript.AppendLine("}")
            sbScript.AppendLine("function fTipoPersona(source, arguments)")
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat("rbt1 = document.getElementById('{0}');", rbtNatural.ClientID)
            sbScript.AppendLineFormat("rbt2 = document.getElementById('{0}');", rbtJuridica.ClientID)
            sbScript.AppendLine("   arguments.IsValid = (rbt1.checked || rbt2.checked)")
            sbScript.AppendLine("}")
            sbScript.AppendLine("")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCustomValidatorModificar__", sbScript.ToString(), True)

        End If
    End Sub

    Private Sub ScriptClienteModificar()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptClienteModificar__") Then

            Dim sbScript As New MyStringBuilder
            sbScript.AppendLine("function OpenDet(idUsr, tipoPer, txtNomId, txtApeId, DNI, RUC, correo, correo2, tlf){")
            sbScript.AppendLineFormat("document.getElementById('{0}').value=idUsr;", hidIdUsuario.ClientID)
            sbScript.AppendLineFormat("if(tipoPer=={0})", MyConfig.getTipoPersonaJuridica)
            sbScript.AppendLineFormat("     document.getElementById('{0}').checked=true;", rbtJuridica.ClientID)
            sbScript.AppendLineFormat("if(tipoPer=={0})", MyConfig.getTipoPersonaNatural)
            sbScript.AppendLineFormat("     document.getElementById('{0}').checked=true;", rbtNatural.ClientID)
            sbScript.AppendLine("      EjecutarSegunPersonaBoton();")
            sbScript.AppendLineFormat("document.getElementById('{0}').value=document.getElementById(txtNomId).value;", txtNombre.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').value=document.getElementById(txtApeId).value;", txtApellido.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').value=DNI;", txtDNI.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').value=RUC;", txtRUC.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').value=correo;", txtCorreo.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').value=correo2;", txtCorreo2.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').value=tlf;", txtTelefono.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').click()", btnOpenDetalle.ClientID)
            sbScript.AppendLine("}")

            sbScript.AppendLine("function EjecutarSegunPersonaBoton()")
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat("document.getElementById('{0}').click()", btnEjecuta.ClientID)
            sbScript.AppendLine("}")
            sbScript.AppendLine("function EjecutarSegunPersona()")
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat("rbt1 = document.getElementById('{0}');", rbtNatural.ClientID)
            sbScript.AppendLineFormat("rbt2 = document.getElementById('{0}');", rbtJuridica.ClientID)
            sbScript.AppendLine("   if(rbt1.checked){")
            sbScript.AppendLineFormat("document.getElementById('{0}').disabled=''", txtApellido.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').disabled=''", txtDNI.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').value=''", txtRUC.ClientID)
            sbScript.AppendLine("   }")
            sbScript.AppendLine("   if(rbt2.checked){")
            sbScript.AppendLineFormat("document.getElementById('{0}').value=''", txtApellido.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').value=''", txtDNI.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').disabled='disabled'", txtApellido.ClientID)
            sbScript.AppendLineFormat("document.getElementById('{0}').disabled='disabled'", txtDNI.ClientID)
            sbScript.AppendLine("   }")
            sbScript.AppendLine("   return false;")
            sbScript.AppendLine("}")
            sbScript.AppendLine("function SeleccionaJuridico(activo)")
            sbScript.AppendLine("{")
            sbScript.AppendLine("alert('MyJuridico')")
            sbScript.AppendLine("   if(activo){")
            sbScript.AppendLine("   }")
            sbScript.AppendLine("}")
            sbScript.AppendLine("function Registrar(btn)")
            sbScript.AppendLine("{")
            sbScript.AppendLine("   if(Page_ClientValidate('Registrar')){")
            sbScript.AppendLineFormat("if(confirm('{0}'))", Resources.SSA_Mensajes.msgUsuarioModificarConfirmar)
            sbScript.AppendLine("      {")
            sbScript.AppendLineFormat("   btn.disabled='disabled'; ")
            sbScript.AppendLineFormat("   document.getElementById('{0}').click();", btnAceptar.ClientID)
            sbScript.AppendLine("      }")
            sbScript.AppendLine("   }")
            sbScript.AppendLine("   return false;")
            sbScript.AppendLine("}")
            sbScript.AppendLine("")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptClienteModificar__", sbScript.ToString(), True)

        End If
    End Sub

#End Region
   
 
End Class
