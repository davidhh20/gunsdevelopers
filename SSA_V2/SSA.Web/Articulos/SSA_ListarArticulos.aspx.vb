Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common
Imports QNET.Web.UI.Controls

Partial Class Articulos_SSA_ListarArticulos
    Inherits QNET.Common.BasePage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ScriptCliente()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dnvListado.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridLista)

        If Not Page.IsPostBack Then
            CType(Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.ArticuloLista
            btnNuevo.OnClientClick = "return NuevoRegistro()"
            hlkFoto1.NavigateUrl = "JavaScript:ColocarFoto(1)"
            hlkFoto2.NavigateUrl = "JavaScript:ColocarFoto(2)"
            hlkFoto3.NavigateUrl = "JavaScript:ColocarFoto(3)"
            grvLista.EmptyDataText = Resources.SSA_Texto.FilasVacias

            'El combo subtipo de articulo se va llenar en el index del TIPO_ARTICULO
            Dim objBLT_TipoArticulo As New BLT_TipoArticulo
            Dim objBLT_Parametro As New BLT_Parametro

            Dim objBE_TipoArticulo As New BE_TipoArticulo
            objBE_TipoArticulo.nEstado = MyConfig.getTipoArticuloActivo
            Util.VinculaDropDownList(ddlTipoArticulo, objBLT_TipoArticulo.Listar(objBE_TipoArticulo), "IdTipoArticulo", "cDescripcion")
            Util.AddAllItemToDDL(ddlTipoArticulo)

            'agregamos todos los SubTipos de articulo que no dependa del Tipo
            Dim objBLT_SubTipoArticulo As New BLT_SubTipoArticulo
            Dim objBE_SubTipoArticulo As New BE_SubTipoArticulo
            objBE_SubTipoArticulo.IdTipoArticulo = 0
            objBE_SubTipoArticulo.nEstado = MyConfig.getSubTipoArticuloActivo
            Util.VinculaDropDownList(ddlSubTipoArticulo, objBLT_SubTipoArticulo.Listar(objBE_SubTipoArticulo), "IdSubTipoArticulo", "cDescripcion")
            Util.AddAllItemToDDL(ddlSubTipoArticulo)

            'Agregamos todos los estados y al final quitamos el Estado : Eliminado
            Dim objBE_Parametro As New BE_Parametro
            objBE_Parametro.nclase = MyConfig.getGrupoEstadoArticulo
            'objBE_Parametro.nSubclase = MyConfig.getSubGrupoEstadoArticulo
            objBE_Parametro.nEstado = MyConfig.getParametroEstadoActivo
            Util.VinculaDropDownList(ddlEstado, objBLT_Parametro.Listar(objBE_Parametro), "IdParametro", "cDescripcion")
            ddlEstado.SelectedValue = MyConfig.getEstadoArticuloEliminado
            ddlEstado.Items.Remove(ddlEstado.SelectedItem)
            Util.AddAllItemToDDL(ddlEstado)

        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        dnvListado.InvokeBind()
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim objBLT_Articulo As New BLT_Articulo
        Dim objBE_Articulo As New BE_Articulo
        objBE_Articulo.id_articulo = Convert.ToInt32(hidIdArticulo.Value)
        objBE_Articulo.nestado = MyConfig.getEstadoArticuloEliminado

        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_Articulo.Eliminar(objBE_Articulo, GlobalEntity.Instance.Usuario.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            Util.RegisterAsyncAlert(upLista, "__ExitoEliminar__", Resources.SSA_Mensajes.msgArticuloExitoEliminar)
            dnvListado.InvokeBindKeepPage()
        Else
            Util.RegisterAsyncAlert(upLista, "__ErrorEliminar__", Resources.SSA_Mensajes.msgArticuloErrorEliminar)
        End If
    End Sub

    Protected Sub grvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_Articulo = CType(e.Row.DataItem, BE_Articulo)
            Dim hlkFotos As HyperLink = DirectCast(e.Row.FindControl("hlkFotos"), HyperLink)
            Dim hlkModificar As HyperLink = DirectCast(e.Row.FindControl("hlkModificar"), HyperLink)
            Dim hlkElimina As HyperLink = DirectCast(e.Row.FindControl("hlkElimina"), HyperLink)
            'setear los hyperlink
            If String.IsNullOrEmpty(BE.crutimage1) And String.IsNullOrEmpty(BE.crutimage2) And String.IsNullOrEmpty(BE.crutimage3) Then
                hlkFotos.NavigateUrl = "JavaScript:MensajeSinFotos()"
            Else
                Dim foto1 As String = MyConfig.getRutaSinFoto
                Dim foto2 As String = MyConfig.getRutaSinFoto
                Dim foto3 As String = MyConfig.getRutaSinFoto
                If Not String.IsNullOrEmpty(BE.crutimage1) Then
                    foto1 = String.Format("{0}{1}", MyConfig.getRutaWebImagenes, BE.crutimage1)
                End If
                If Not String.IsNullOrEmpty(BE.crutimage2) Then
                    foto2 = String.Format("{0}{1}", MyConfig.getRutaWebImagenes, BE.crutimage2)
                End If
                If Not String.IsNullOrEmpty(BE.crutimage3) Then
                    foto3 = String.Format("{0}{1}", MyConfig.getRutaWebImagenes, BE.crutimage3)
                End If
                hlkFotos.NavigateUrl = String.Format("JavaScript:VerFotos('{0}','{1}','{2}')", foto1, foto2, foto3)
            End If
            hlkModificar.NavigateUrl = String.Format("SSA_NuevoArticulo.aspx?ItemSubmit={0}&idIttem={1}", MyConfig.getParamGetUpdate, BE.id_articulo)
            hlkElimina.NavigateUrl = String.Format("JavaScript:EliminarItem('{0}','{1}')", BE.id_articulo, BE.ccodart)
            'seteamos celdas
            If BE.canio = 0 Then e.Row.Cells(6).Text = ""
        End If
    End Sub

#Region " Metodo "

    Private Function BindGridLista(ByVal sender As Object, ByVal e As EventArgs) As DataNavigatorParams
        Dim objBE_Usuario As New BE_Articulo
        Dim objBLT_Articulo As New BLT_Articulo
        Dim objPaginador As New Paginador

        objPaginador.NumeroRegistros = grvLista.PageSize
        objPaginador.NumeroPagina = dnvListado.CurrentPage

        If txtCodart.Text.Length = 0 Then
            objBE_Usuario.ccodart = -1
        Else
            objBE_Usuario.ccodart = Convert.ToInt32(txtCodart.Text)
        End If
        objBE_Usuario.nestado = Convert.ToInt32(ddlEstado.SelectedValue.ToString)
        objBE_Usuario.id_tipo_articulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue.ToString)
        objBE_Usuario.id_subtipo_articulo = Convert.ToInt32(ddlSubTipoArticulo.SelectedValue.ToString)
        objBE_Usuario.nestado2 = MyConfig.getEstadoArticuloEliminado

        Dim lista As IList = objBLT_Articulo.ListarPaginado(objBE_Usuario, objPaginador)

        dnvListado.Visible = (objPaginador.TotalResgistros > objPaginador.NumeroRegistros)
        Return New DataNavigatorParams(lista, objPaginador.TotalResgistros)

    End Function

#End Region

#Region " Scripts "
    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function ColocarFoto(n)")
            sScript.AppendLine("{")
            sScript.AppendLine("    ruta = '';")
            sScript.AppendLine("    switch(n){")
            sScript.AppendLine("        case 1:")
            sScript.AppendLineFormat("      ruta = document.getElementById('{0}').value", hidRutaImagen1.ClientID)
            sScript.AppendLine("            break;")
            sScript.AppendLine("        case 2:")
            sScript.AppendLineFormat("      ruta = document.getElementById('{0}').value", hidRutaImagen2.ClientID)
            sScript.AppendLine("            break;")
            sScript.AppendLine("        case 3:")
            sScript.AppendLineFormat("      ruta = document.getElementById('{0}').value", hidRutaImagen3.ClientID)
            sScript.AppendLine("            break;")
            sScript.AppendLine("    }")
            sScript.AppendLineFormat("      document.getElementById('{0}').src = ruta;", imgFoto.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("function NuevoRegistro(){")
            sScript.AppendLineFormat("location = 'SSA_NuevoArticulo.aspx?ItemSubmit={0}'", MyConfig.getParamGetAdd)
            sScript.AppendLine("      return false;")
            sScript.AppendLine("}")
            sScript.AppendLine("function MensajeSinFotos(){")
            sScript.AppendLineFormat("alert('{0}')", Resources.SSA_Mensajes.msgArticuloNoHayFotos)
            sScript.AppendLine("}")
            sScript.AppendLine("function VerFotos(img1, img2, img3){")
            sScript.AppendLineFormat("document.getElementById('{0}').value=img1", hidRutaImagen1.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=img2", hidRutaImagen2.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=img3", hidRutaImagen3.ClientID)
            sScript.AppendLine("      ColocarFoto(1);")
            sScript.AppendLineFormat("document.getElementById('{0}').click()", btnOpenFotos.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("function EliminarItem(idArticulo, codarticulo){")
            sScript.AppendLineFormat("msg =  '{0}'", Resources.SSA_Mensajes.msgArticuloConfirmarEliminar)
            sScript.AppendLine("      msg =  msg.replace('{0}',codarticulo)")
            sScript.AppendLine("    if(confirm(msg))")
            sScript.AppendLine("        {")
            sScript.AppendLineFormat("      document.getElementById('{0}').value=idArticulo", hidIdArticulo.ClientID)
            sScript.AppendLineFormat("      document.getElementById('{0}').click()", btnEliminar.ClientID)
            sScript.AppendLine("        }")
            sScript.AppendLine("}")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), True)
        End If
    End Sub
#End Region
 
End Class
