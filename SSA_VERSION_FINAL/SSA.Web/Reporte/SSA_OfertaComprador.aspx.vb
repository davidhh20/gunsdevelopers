Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common
Imports QNET.Web.UI.Controls

Partial Class Reporte_SSA_OfertaComprador
    Inherits QNET.Common.BasePage

#Region " Pagina "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        GridEffects.RegisterGridForEffects(grvLista)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dnvListado.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridLista)
        If Not Page.IsPostBack Then
            If String.IsNullOrEmpty(Request.QueryString("op")) Then 'esta llamando de Ofertas x Subasta 
                DirectCast(Me.Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.ReporteOfertaSubasta
                hidOperacion.Value = "1"
            Else
                DirectCast(Me.Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.ReporteRentabilidad
                hidOperacion.Value = "2"
            End If
            dnvListado.InvokeBind()
        End If
    End Sub

    Protected Sub grvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvLista.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hlk01 As HyperLink = DirectCast(e.Row.FindControl("hlk01"), HyperLink)
            Dim BE As BE_Subasta = CType(e.Row.DataItem, BE_Subasta)
            If hidOperacion.Value = "1" Then
                hlk01.NavigateUrl = String.Format("JavaScript:AbrirVentana01('{0}')", BE.id_subasta)
            Else
                hlk01.NavigateUrl = String.Format("JavaScript:AbrirVentana02('{0}')", BE.id_subasta)
            End If
        End If
    End Sub

#End Region

#Region " Metodos "

    Private Function BindGridLista(ByVal sender As Object, ByVal e As EventArgs) As DataNavigatorParams
        Dim objBE_Subasta As New BE_Subasta
        Dim objBLT_Subasta As New BLT_Subasta
        Dim objPaginador As New Paginador

        objPaginador.NumeroRegistros = Int32.MaxValue
        objPaginador.NumeroPagina = 1

        objBE_Subasta.id_subasta = -1
        'aqui falta definir por que estado se filtrara; el de rentabilidad se filtra solo subastas cerradas
        If hidOperacion.Value = "1" Then
            objBE_Subasta.nestadsub = -1
        Else
            objBE_Subasta.nestadsub = MyConfig.getEstadoSubastaCerrado
        End If
        objBE_Subasta.nestado2 = MyConfig.getEstadoSubastaEliminada

        Dim lista As IList = objBLT_Subasta.ListarPaginado(objBE_Subasta, objPaginador)
        dnvListado.Visible = (objPaginador.TotalResgistros > objPaginador.NumeroRegistros)
        Return New DataNavigatorParams(lista, objPaginador.TotalResgistros)

    End Function

#End Region

End Class
