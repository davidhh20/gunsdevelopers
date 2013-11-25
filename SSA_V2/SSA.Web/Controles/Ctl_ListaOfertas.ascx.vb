Imports System.IO
Imports System.Collections.Generic
Imports SSA.BusinessEntity

Partial Class Controles_Ctl_ListaOfertas
    Inherits System.Web.UI.UserControl

#Region " Propiedades "

    Public ReadOnly Property RenderHTML() As String
        Get
            Dim constructorCuerpo As New StringBuilder()
            Dim strWrPage As New StringWriter(constructorCuerpo)
            Dim txtWrPage As New HtmlTextWriter(strWrPage)
            Dim sourceExport As New Page
            Dim Frm As New HtmlForm

            Dim grv As GridView = grvCopy

            grv.EnableViewState = False

            sourceExport.EnableEventValidation = False
            sourceExport.DesignerInitialize()

            sourceExport.Controls.Add(Frm)
            Frm.Controls.Add(grv) '''''''

            txtWrPage.Write("<table>")
            txtWrPage.Write("</tr><td colspan='6'>&nbsp;</td></tr>")
            txtWrPage.Write("</tr><td colspan='6'>&nbsp;</td></tr>")
            txtWrPage.Write("<tr><td><b>Código: </b>{0}</td><td colspan='3'><b>Descripción: </b>{1}</td><td colspan='2'><b>Precio Base: </b>{2}</td></tr>", lblCodigo.Text, lblDescripcion.Text, lblPrecioBase.Text)
            txtWrPage.Write("</table>")
            sourceExport.RenderControl(txtWrPage)

            Return constructorCuerpo.ToString

        End Get
    End Property

    Public WriteOnly Property CodigoArticulo() As String
        Set(ByVal value As String)
            lblCodigo.Text = value
        End Set
    End Property

    Public WriteOnly Property DescripcionArticulo() As String
        Set(ByVal value As String)
            lblDescripcion.Text = value
        End Set
    End Property

    Public WriteOnly Property PrecioArticulo() As String
        Set(ByVal value As String)
            lblPrecioBase.Text = value
        End Set
    End Property

    Public WriteOnly Property ListaOfertas() As List(Of BE_Oferta)
        Set(ByVal value As List(Of BE_Oferta))
            grvListaOferta.DataSource = value
            grvListaOferta.DataBind()
            grvCopy.DataSource = value
            grvCopy.DataBind()
        End Set
    End Property

#End Region
    
#Region " Página "

    Protected Sub grvListaOferta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvListaOferta.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_Oferta = CType(e.Row.DataItem, BE_Oferta)
            Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
            If BE.BE_Usuario.nmarca = MyConfig.getMarcaCompradorDudoso Then
                e.Row.Cells(3).Text = "X"
            End If
            'setear lo adjudicado si existiera: Setear el comentario, si esta vendido o no
            If BE.id_adjudicacion > 0 Then
                If BE.BE_Adjudicacion.fvendido Then
                    e.Row.Cells(4).Text = "SI"
                    chk.Checked = True
                Else
                    e.Row.Cells(4).Text = "NO"
                End If
            End If
        End If
    End Sub

#End Region

End Class
