Imports System.Collections.Generic
Imports System.Configuration
Imports SSA.BusinessLogic
Imports SSA.BusinessEntity
Imports QNET.Common

Partial Class MasterPage_SSA_Principal
    Inherits QNET.Common.BaseMaster


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If GlobalEntity.EsNulo Then
            Response.Redirect("../Seguridad/SSA_Login.aspx")
        End If

        If Not Page.IsPostBack Then

            lblPiePagina.Text = Resources.SSA_Texto.PiePaginaPrincipal
            lblNombreUsuario.Text = GlobalEntity.Instance.Usuario.cusuario

            Dim idPerfil As String = GlobalEntity.Instance.Usuario.nIdPerfil
            Dim Be_OpcionColl As List(Of BE_Opcion) = Nothing

            If Session("__Menu__") IsNot Nothing Then
                Be_OpcionColl = DirectCast(Session("__Menu__"), List(Of BE_Opcion))
            Else
                Dim objBLT_OpcionPerfil As New BLT_OpcionPerfil
                Be_OpcionColl = objBLT_OpcionPerfil.Listar(idPerfil)
                Session("__Menu__") = Be_OpcionColl
            End If

            For Each objOpcion As BE_Opcion In Be_OpcionColl

                If Not objOpcion.IdOpcionPadre.HasValue Then

                    Dim DivFin As New QNET.Web.UI.WebControls.MultiMenuItem
                    DivFin.IsDivider = True
                    DivFin.Image = "i.p.divider.gif"
                    mnuPricipal.TopMenuGroup.MultiMenuItems.Add(DivFin)

                    Dim itemGroup As New QNET.Web.UI.WebControls.MultiMenuItem
                    itemGroup.Text = objOpcion.Nombre
                    itemGroup.Height = 25
                    itemGroup.PostBack = False
                    mnuPricipal.TopMenuGroup.MultiMenuItems.Add(itemGroup)

                    Dim group As QNET.Web.UI.WebControls.MultiMenuGroup = itemGroup.CreateMenuGroup()
                    group.ExpandDirection = QNET.Web.UI.WebControls.ExpandDirectionType.Left
                    group.VerticalOffset = -2
                    group.HorizontalOffset = 0
                    group.CssClass = "MenuGroup"

                    For Each objSubOpcion As BE_Opcion In Be_OpcionColl
                        If objSubOpcion.IdOpcionPadre.HasValue AndAlso objSubOpcion.IdOpcionPadre.Value = objOpcion.IdOpcion Then
                            Dim item As New QNET.Web.UI.WebControls.MultiMenuItem
                            item.Text = objSubOpcion.Nombre
                            item.PostBack = False
                            item.Url = String.Format("../{0}", objSubOpcion.Url)
                            item.Height = 25
                            item.Width = 250
                            item.CssClass = "MenuItem2"
                            group.MultiMenuItems.Add(item)
                        End If
                    Next
                End If

            Next

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

    Protected Sub lkbSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkbSalir.Click
        FormsAuthentication.SignOut()
        GlobalEntity.SignOut()
        Session.Clear()
        Response.Redirect("../Seguridad/SSA_Login.aspx")
    End Sub
End Class


