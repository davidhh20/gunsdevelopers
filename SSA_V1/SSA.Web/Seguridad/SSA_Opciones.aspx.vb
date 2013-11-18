Imports System.Resources

Partial Class SSA_opciones
    Inherits QNET.Common.BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CType(Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.TituloPrincipal
    End Sub

End Class
