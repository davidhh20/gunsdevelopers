Imports QNET.Common
Imports System.IO

Partial Class MasterPage_SSA_Reportes
    Inherits QNET.Common.BaseMaster

#Region " Propiedades "

    Public WriteOnly Property setTitulo() As String
        Set(ByVal value As String)
            lblTitulo.Text = value.ToUpper
        End Set
    End Property

    Public WriteOnly Property GridView() As GridView
        Set(ByVal value As GridView)
            _GridView = value
        End Set
    End Property

    Public WriteOnly Property SetTitle() As String
        Set(ByVal value As String)
            hidTitle.Value = value
        End Set
    End Property

    Public Property RenderGrid()
        Get
            If Not String.IsNullOrEmpty(ViewState("__RenderGrid__")) Then
                Return ViewState("__RenderGrid__").ToString
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value)
            ViewState("__RenderGrid__") = value
        End Set
    End Property

#End Region

    Protected _GridView As GridView
    Protected _MiTitle As String
    Protected _FileName As String

    Private Const CabeceraPagina As String = _
              "<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN\' ttp://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd>" + _
              "<html xmlns='http://www.w3.org/1999/xhtml><head><title></title>" + _
              "<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />" + _
              "</head><body>"
    Private Const ContenedorGrid As String = "<table><tr><td>{0}</td></tr></table>"
    Private Const PiePagina As String = "</body></html>"

    Protected Sub btnExportar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.ServerClick
        Dim constructorCuerpo As New StringBuilder()
        Dim strWrPage As New StringWriter(constructorCuerpo)
        Dim txtWrPage As New HtmlTextWriter(strWrPage)
        Dim sourceExport As New Page
        Dim Frm As New HtmlForm

        If _GridView IsNot Nothing Then       

            _GridView.EnableViewState = False

            sourceExport.EnableEventValidation = False
            sourceExport.DesignerInitialize()

            sourceExport.Controls.Add(Frm)
            Frm.Controls.Add(_GridView)

            txtWrPage.Write(String.Format("<h3>{0,15}</h3>", hidTitle.Value))
            'txtWrPage.Write(Me.RenderGrid)
            sourceExport.RenderControl(txtWrPage)

        Else
            txtWrPage.Write(CabeceraPagina)
            txtWrPage.Write(String.Format("<h3>{0,15}</h3>", hidTitle.Value))
            txtWrPage.Write(Me.RenderGrid)
            txtWrPage.Write(PiePagina)

            sourceExport.DesignerInitialize()

            'sourceExport.EnableEventValidation = False
            sourceExport.Controls.Add(Frm)

            sourceExport.RenderControl(txtWrPage)
            sourceExport.Dispose()
            sourceExport = Nothing

        End If

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=data.xls")
        HttpContext.Current.Response.Charset = "UTF-8"
        HttpContext.Current.Response.ContentEncoding = Encoding.Default
        HttpContext.Current.Response.Write(constructorCuerpo.ToString())
        HttpContext.Current.Response.End()
    End Sub


    'Response.Clear()
    'Response.Buffer = True
    'Response.ContentType = "application/vnd.ms-excel"
    'Response.AddHeader("Content-Disposition", "attachment;filename=data.xls")
    'Response.Charset = "UTF-8"
    'Response.ContentEncoding = Encoding.Default
    'Response.Write(constructorCuerpo.ToString())
    'Response.End()

    'HttpContext.Current.Response.ContentType = "application/ms-excel"
    'HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=dataarchivo.xls")
    'HttpContext.Current.Response.Write(constructorCuerpo.ToString)
    'HttpContext.Current.Response.End()

End Class

