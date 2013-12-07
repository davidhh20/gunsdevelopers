Imports System.IO
Imports SSA.BusinessEntity
Imports SSA.BusinessLogic

Partial Class Reporte_SSA_Print_Rentabilidad
    Inherits QNET.Common.BasePage

#Region " Propiedades "

    Private Property SumRentabilidad() As Decimal
        Get
            Return Convert.ToDecimal(ViewState("__SumRentabilidad__").ToString)
        End Get
        Set(ByVal value As Decimal)
            ViewState("__SumRentabilidad__") = value
        End Set
    End Property

    Private Property CountRentabilidad() As Decimal
        Get
            Return Convert.ToDecimal(ViewState("__CountRentabilidad__").ToString)
        End Get
        Set(ByVal value As Decimal)
            ViewState("__CountRentabilidad__") = value
        End Set
    End Property

#End Region

#Region " Página "

    Protected MyMasterPage As MasterPage_SSA_Reportes
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MyMasterPage = DirectCast(Master, MasterPage_SSA_Reportes)
        MyMasterPage.GridView = grvLista
        If Not Page.IsPostBack Then
            MyMasterPage.setTitulo = Resources.SSA_Texto.ReporteRentabilidad
            MyMasterPage.SetTitle = Resources.SSA_Texto.ReporteRentabilidad
            Dim idSubasta As Integer = 0
            If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                idSubasta = Convert.ToInt32(Request.QueryString("id").ToString)
            End If

            If idSubasta = 0 Then Exit Sub

            SumRentabilidad = 0
            CountRentabilidad = 0
            Dim objBLT_Subasta As New BLT_Subasta
            Dim objBE_ListaOferta As New BE_ListaOferta
            objBE_ListaOferta.id_subasta = idSubasta
            objBE_ListaOferta.nestado = MyConfig.getEstadoDetalleSubastaEliminado
            objBE_ListaOferta.nestado2 = MyConfig.getEstadoOfertaEliminada
            objBE_ListaOferta.nestado3 = MyConfig.getEstadoAdjudicacionEliminado

            grvLista.DataSource = objBLT_Subasta.Rentabilidad(objBE_ListaOferta)
            grvLista.DataBind()

            If grvLista.Rows.Count = 0 Then
                Dim sScript As New StringBuilder
                sScript.AppendLine(String.Format("alert('{0}')", Resources.SSA_Mensajes.msgReporteSubastaSinRegistro))
                sScript.AppendLine("window.close()")
                ClientScript.RegisterStartupScript(Page.GetType, "__CerrarPopup__", sScript.ToString, True)
            Else
                lb01.Visible = True               
            End If
        End If
    End Sub

    Protected Sub grvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvLista.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_Rentabilidad = CType(e.Row.DataItem, BE_Rentabilidad)
            If BE.rentabilidad.HasValue Then
                e.Row.Cells(5).Text = String.Format("{0}%", BE.rentabilidad.Value)
                SumRentabilidad += BE.rentabilidad
                CountRentabilidad += 1
            Else
                e.Row.Cells(0).ForeColor = Drawing.Color.Red
                e.Row.Cells(1).ForeColor = Drawing.Color.Red
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
            End If
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).ColumnSpan = 3
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(2).Text = "Promedio de rentabilidad"
            If CountRentabilidad > 0 Then
                e.Row.Cells(5).Text = String.Format("{0}%", (Math.Round(SumRentabilidad / CountRentabilidad, 2)))
            Else
                e.Row.Cells(5).Text = "-"
            End If
        End If
    End Sub

#End Region

End Class

