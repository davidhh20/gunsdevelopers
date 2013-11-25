Imports System.Collections.Generic
Imports System.Configuration
Imports SSA.BusinessLogic
Imports SSA.BusinessEntity
Imports QNET.Common

Partial Class MasterPage_SSA_Publica
    Inherits QNET.Common.BaseMaster


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sbSccript As MyStringBuilder = New MyStringBuilder()
        If GlobalEntity.EsNulo Then
            sbSccript.AppendLine("document.getElementById('fila1').style.display = 'inline';")
            sbSccript.AppendLine("document.getElementById('fila2').style.display = 'none';")
        Else
            lblNombreUsuario.Text = GlobalEntity.Instance.Usuario.cusuario
            'sbSccript.AppendLineFormat("document.getElementById('{0}').disabled = 'disabled';", txtNombreUsuario.ClientID)
            'sbSccript.AppendLineFormat("document.getElementById('{0}').value = '{1}';", txtNombreUsuario.ClientID, GlobalEntity.Instance.Usuario.cusuario)
            sbSccript.AppendLine("document.getElementById('fila1').style.display = 'none';")
            sbSccript.AppendLine("document.getElementById('fila2').style.display = 'inline';")
        End If
        Util.RegisterScript(upnProxy, "__Retorna__", sbSccript.ToString())
        If Not Page.IsPostBack Then
            lblPiePagina.Text = Resources.SSA_Texto.PiePaginaPrincipal
        End If



    End Sub

    Public Overrides Sub AddScriptDespuesScriptManager(ByVal str_pScript As String)

        Dim stb_Script As New MyStringBuilder
        stb_Script.Append(str_pScript)
        stb_Script.AppendLine()
        stb_Script.Append(lit_script.Text)
        lit_script.Text = stb_Script.ToString()

    End Sub

    Public WriteOnly Property setTitulo() As String
        Set(ByVal value As String)
            lblTitulo.Text = value.ToUpper
        End Set
    End Property

    Public WriteOnly Property setTitulo2() As String
        Set(ByVal value As String)
            lblTitulo2.Text = value.ToUpper
        End Set
    End Property

    Public WriteOnly Property setUsuario() As String
        Set(ByVal value As String)
            lblNombreUsuario.Text = value
        End Set
    End Property

    Protected Sub lkbSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkbSalir.Click
        FormsAuthentication.SignOut()
        GlobalEntity.SignOut()
        Session.Clear()
        Response.Redirect("../Seguridad/SSA_Login.aspx")
    End Sub

 
End Class


