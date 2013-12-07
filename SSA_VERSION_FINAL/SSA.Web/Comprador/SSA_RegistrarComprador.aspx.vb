Imports QNET.Common
Imports SSA.BusinessEntity
Imports SSA.BusinessLogic

Partial Class Comprador_SSA_RegistrarComprador
    Inherits System.Web.UI.Page

    
#Region " Página "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ScriptCustomValidator()
        ScriptCliente()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            DirectCast(Me.Master, MasterPage_SSA_Publica).setTitulo = Resources.SSA_Texto.CompradorRegistroUsuario
            rbtNatural.Attributes.Add("onclick", "EjecutarSegunPersonaBoton()")
            rbtJuridica.Attributes.Add("onclick", "EjecutarSegunPersonaBoton()")
            txtUsuario.Attributes.Add("onkeypress", "SoloUsuario()")
            txtNombre.Attributes.Add("onkeypress", "SoloNombres(0,this,0)")
            txtApellido.Attributes.Add("onkeypress", "SoloNombres(0,this,0)")
            txtDNI.Attributes.Add("onkeypress", "SoloEnteros()")
            txtRUC.Attributes.Add("onkeypress", "SoloEnteros()")
            txtTelefono.Attributes.Add("onkeypress", "SoloTelefono(this)")
            revApellido.ValidationExpression = MyConfig.getExpresionNombres
            btnCancelar.OnClientClick = String.Format("if(confirm('{0}')) location='../Seguridad/SSA_Login.aspx';  return false;", Resources.SSA_Mensajes.msgRegistroUsuarioCancelar)
            btnBeforeRegistro.OnClientClick = "return Registrar(this)"
            btnEjecuta.OnClientClick = "return EjecutarSegunPersona()"
        End If
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim BE As New BE_Usuario

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
        BE.ccorreoalt = IIf(txtCorreo2.Text.Length = 0, String.Empty, txtCorreo2.Text.Trim.ToLower)
        BE.ctelefono = txtTelefono.Text
        BE.cusuario = txtUsuario.Text.Trim.ToLower
        BE.cclave = txtClave.Text
        BE.nmarca = MyConfig.getMarcaCompradorNormal
        BE.nestado = MyConfig.getEstadoUsuarioActivo
        BE.nIdPerfil = MyConfig.getPerfilComprador

        Dim objBLT_Usuario As New BLT_Usuario
        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_Usuario.Insertar(BE)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            Dim strScript As New MyStringBuilder
            strScript.AppendFormat("alert('{0}');", Resources.SSA_Mensajes.msgRegistroUsuarioExito)
            strScript.Append("location='../Seguridad/SSA_Login.aspx'")
            Util.RegisterScript(upProxy, "__RegistroUsuarioGoto__", strScript.ToString)
        Else
            If Tx.ObjException.GetType() Is Type.GetType("System.Data.SqlClient.SqlException, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089") Then
                Dim msgError As String = ""
                Select Case DirectCast(Tx.ObjException, System.Data.SqlClient.SqlException).Number
                    Case 3609
                        msgError = Resources.SSA_Mensajes.msgRegistroUsuarioDuplicado
                    Case 2627
                        msgError = Resources.SSA_Mensajes.msgRegistroUsuarioDuplicado2
                    Case Else
                End Select
                If msgError.Length > 0 Then
                    Util.RegisterAsyncAlert(upProxy, "__MensajeErrorUniqueDocumento__", msgError)
                    Util.RegisterScript(upProxy, "__DesHabilitarBoton01__", String.Format("document.getElementById('{0}').disabled=''", btnBeforeRegistro.ClientID))
                    Exit Sub
                End If
            End If
            Util.RegisterAsyncAlert(upProxy, "__ErrorRegistro__", Resources.SSA_Mensajes.msgRegistroUsuarioError)
        End If

        Util.RegisterScript(upProxy, "__DesHabilitarBoton02__", String.Format("document.getElementById('{0}').disabled=''", btnBeforeRegistro.ClientID))

    End Sub

#End Region

#Region " Scripts "
    Private Sub ScriptCustomValidator()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCustomValidator__") Then

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
            sbScript.AppendLine("function fConfirmarClave(source, arguments)")
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat("txt1 = document.getElementById('{0}') ", txtClave.ClientID)
            sbScript.AppendLineFormat("txt2 = document.getElementById('{0}') ", txtConfirmar.ClientID)
            sbScript.AppendLine("   arguments.IsValid = (txt1.value==txt2.value)")
            sbScript.AppendLine("}")
            sbScript.AppendLine("")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCustomValidator__", sbScript.ToString(), True)

        End If
    End Sub

    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then

            Dim sbScript As New MyStringBuilder
            sbScript.AppendLine("")
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
            sbScript.AppendLineFormat("if(confirm('{0}'))", Resources.SSA_Mensajes.msgRegistroUsuarioConfirmar)
            sbScript.AppendLine("      {")
            sbScript.AppendLineFormat("   btn.disabled='disabled'; ")
            sbScript.AppendLineFormat("   document.getElementById('{0}').click();", btnAceptar.ClientID)
            sbScript.AppendLine("      }")
            sbScript.AppendLine("   }")
            sbScript.AppendLine("   return false;")
            sbScript.AppendLine("}")
            sbScript.AppendLine("")
            sbScript.AppendLine("function fConfirmarClave(source, arguments)")
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat("txt1 = document.getElementById('{0}') ", txtClave.ClientID)
            sbScript.AppendLineFormat("txt2 = document.getElementById('{0}') ", txtConfirmar.ClientID)
            sbScript.AppendLine("   arguments.IsValid = (txt1.value==txt2.value)")
            sbScript.AppendLine("}")
            sbScript.AppendLine("")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sbScript.ToString(), True)

        End If
    End Sub
#End Region

End Class
