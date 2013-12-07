Imports SSA.BusinessEntity
Imports SSA.BusinessLogic

Partial Class Reporte_SSA_Print_OfertaComprador
    Inherits QNET.Common.BasePage


    Protected MyMasterPage As MasterPage_SSA_Reportes
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MyMasterPage = DirectCast(Master, MasterPage_SSA_Reportes)
        MyMasterPage.GridView = Nothing
        'MyMasterPage.RenderGrid = "<h3>del RENDERRRR</h3>"
        If Not Page.IsPostBack Then

            MyMasterPage.setTitulo = Resources.SSA_Texto.ReporteOfertaSubasta
            MyMasterPage.SetTitle = Resources.SSA_Texto.ReporteOfertaSubasta

            Dim objBLT_Subasta As New BLT_Subasta
            Dim objBLT_Oferta As New BLT_Oferta
            Dim objBE_Oferta As BE_Oferta
            Dim idSubasta As Integer = 0

            If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                idSubasta = Convert.ToInt32(Request.QueryString("id").ToString)
            End If

            If idSubasta = 0 Then Exit Sub

            Dim objBE_Subasta As BE_Subasta = objBLT_Subasta.Seleccionar(idSubasta, MyConfig.getEstadoDetalleSubastaEliminado)
            For Each item As BE_Detalle_Subasta In objBE_Subasta.MiDetalle
                Dim UC_L As Controles_Ctl_ListaOfertas = CType(LoadControl("../Controles/Ctl_ListaOfertas.ascx"), Controles_Ctl_ListaOfertas)
                UC_L.CodigoArticulo = item.BE_Articulo.id_articulo
                UC_L.DescripcionArticulo = item.BE_Articulo.cdescrip_breve
                UC_L.PrecioArticulo = item.BE_Articulo.nprecio_base


                objBE_Oferta = New BE_Oferta
                objBE_Oferta.id_detsubasta = item.id_detsubasta
                objBE_Oferta.nestado = MyConfig.getEstadoOfertaEliminada
                objBE_Oferta.BE_Adjudicacion.nestado = MyConfig.getEstadoAdjudicacionEliminado
                UC_L.ListaOfertas = objBLT_Oferta.Listar(objBE_Oferta)
                pnl.Controls.Add(UC_L)

            Next

            Dim getHTML As New StringBuilder
            For Each UC_L As Control In pnl.Controls
                If TypeOf UC_L Is Controles_Ctl_ListaOfertas Then
                    getHTML.AppendLine(CType(UC_L, Controles_Ctl_ListaOfertas).RenderHTML)
                End If
            Next
            MyMasterPage.RenderGrid = getHTML.ToString

        End If
    End Sub
End Class
