Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common

Partial Class Comprador_SSA_OlvidoClave
    Inherits System.Web.UI.Page

#Region " Página "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ScriptCliente()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btnCancelar.OnClientClick = "return Salir()"
            btnBeforeRegistro.OnClientClick = "return Aceptar(this)"
            DirectCast(Me.Master, MasterPage_SSA_Publica).setTitulo = Resources.SSA_Texto.CompradorOlvidoClave
        End If
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim objBE_Usuario As New BE_Usuario
        Dim objBLT_Usuario As New BLT_Usuario
        objBE_Usuario.cusuario = txtUsuario.Text
        objBE_Usuario.cclave = String.Empty 'Para que lo cree la propiedad del tipo de dato y no cree los punteros en memoria
        objBE_Usuario.ccorreo = txtCorreo.Text
        objBE_Usuario = objBLT_Usuario.Seguridad_ObtenerUsuario(objBE_Usuario)
        If objBE_Usuario IsNot Nothing Then
            Dim miCorreo As New correo
            miCorreo.Body = CuerpoCorreo.CuerpoOlvidoClave(objBE_Usuario.cclave)
            Dim msg As String = String.Empty
            Try
                miCorreo.Enviar(txtCorreo.Text.Trim, String.Empty, MyConfig.getAsuntoOlvidoClave)
                msg = Resources.SSA_Mensajes.msgOlvidoClaveExitoEnvio
                ScriptManager.RegisterClientScriptBlock(upProxy, upProxy.GetType, "__ExitoEnvioCorreo__", "EnvioCorreo()", True)
            Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(upProxy, upProxy.GetType, "__ErrorEnvioCorreo__", String.Format("alert('{0}')", Resources.SSA_Mensajes.msgOlvidoClaveErrorEnvio), True)
            End Try
        Else
            ScriptManager.RegisterClientScriptBlock(upProxy, upProxy.GetType, "__ErrorLogin__", String.Format("alert('{0}')", Resources.SSA_Mensajes.msgOlvidoClaveNoHayUsuario), True)
        End If
        Util.RegisterScript(upProxy, "__DesHabilitarBoton01__", String.Format("document.getElementById('{0}').disabled=''", btnBeforeRegistro.ClientID))
    End Sub

#End Region

#Region " ScriptCliente "
    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then
            Dim sbScript As New StringBuilder
            sbScript.AppendLine("")
            sbScript.AppendLine("function Aceptar(btn)")
            sbScript.AppendLine("{")
            sbScript.AppendLine("   if(Page_ClientValidate()){")
            sbScript.AppendLine("       btn.disabled='disabled';")
            sbScript.AppendLine(String.Format("document.getElementById('{0}').click()", btnAceptar.ClientID))
            sbScript.AppendLine("   }")
            sbScript.AppendLine("   return false;")
            sbScript.AppendLine("}")
            sbScript.AppendLine("function EnvioCorreo()")
            sbScript.AppendLine("{")
            sbScript.AppendLine(String.Format("alert('{0}')", Resources.SSA_Mensajes.msgOlvidoClaveExitoEnvio))
            sbScript.AppendLine("   location='../Seguridad/SSA_Login.aspx'")
            sbScript.AppendLine("}")
            sbScript.AppendLine("function Salir()")
            sbScript.AppendLine("{")
            sbScript.AppendLine("   location='../Seguridad/SSA_Login.aspx'")
            sbScript.AppendLine("   return false;")
            sbScript.AppendLine("}")
            sbScript.AppendLine("")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sbScript.ToString(), True)
        End If
    End Sub
#End Region

End Class
