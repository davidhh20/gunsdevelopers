Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common
Imports QNET.Web.UI.Controls

Partial Class Mantenimiento_SSA_TipoArticulo
    Inherits QNET.Common.BasePage

#Region " Página "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        GridEffects.RegisterGridForEffects(grvLista)
        ScriptCliente()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dnvListado.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridLista)
        If Not Page.IsPostBack Then
            DirectCast(Me.Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.TipoArticulo
            dnvListado.InvokeBind()
            btnNuevo.OnClientClick = "return ExecNuevo();"
            btnGrabar.OnClientClick = "return ExecValida();"
            grvLista.EmptyDataText = Resources.SSA_Texto.FilasVacias
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        dnvListado.InvokeBind()
    End Sub


    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim objBLT_TipoArticulo As New BLT_TipoArticulo
        Dim objBE_TipoArticulo As New BE_TipoArticulo

        objBE_TipoArticulo.IdTipoArticulo = Convert.ToInt32(txtId.Text)
        objBE_TipoArticulo.nEstado = MyConfig.getTipoArticuloEliminado

        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_TipoArticulo.Eliminar(objBE_TipoArticulo, GlobalEntity.Instance.Usuario.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            dnvListado.InvokeBind()
            Util.RegisterAsyncAlert(upLista, "__msgExitoElimina__", Resources.SSA_Mensajes.msgTipoArticuloEliminarExito)
        Else
            Util.RegisterAsyncAlert(upLista, "__msgErrorElimina__", Resources.SSA_Mensajes.msgTipoArticuloEliminarError)
        End If
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim objBLT_TipoArticulo As New BLT_TipoArticulo
        Dim objBE_TipoArticulo As New BE_TipoArticulo

        Dim idTipoArticulo As Integer = 0
        If Not String.IsNullOrEmpty(txtId.Text) Then
            idTipoArticulo = Convert.ToInt32(txtId.Text)
        End If

        objBE_TipoArticulo.IdTipoArticulo = idTipoArticulo
        objBE_TipoArticulo.cDescripcion = txtDescripcion.Text.Trim.ToUpper
        objBE_TipoArticulo.nEstado = MyConfig.getTipoArticuloActivo
        Dim Tx As ResultadoTransaccion = Nothing

        If idTipoArticulo = 0 Then
            Tx = objBLT_TipoArticulo.Insertar(objBE_TipoArticulo, GlobalEntity.Instance.Usuario.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                dnvListado.InvokeBind()
                Util.RegisterAsyncAlert(upLista, "__msgExitoRegistra__", Resources.SSA_Mensajes.msgTipoArticuloRegistrarExito)
            Else
                Util.RegisterAsyncAlert(upLista, "__msgErrorRegistra__", Resources.SSA_Mensajes.msgTipoArticuloRegistrarError)
            End If
        Else
            Tx = objBLT_TipoArticulo.Modificar(objBE_TipoArticulo, GlobalEntity.Instance.Usuario.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                dnvListado.InvokeBind()
                Util.RegisterAsyncAlert(upLista, "__msgExitoModifica__", Resources.SSA_Mensajes.msgTipoArticuloModificarExito)
            Else
                Util.RegisterAsyncAlert(upLista, "__msgErrorModifica__", Resources.SSA_Mensajes.msgTipoArticuloModificarError)
            End If
        End If
        Util.RegisterAsyncPressButton(upLista, "__CierraDetalle__", btnCerrarDetalle.ClientID)
    End Sub

    Protected Sub grvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_TipoArticulo = CType(e.Row.DataItem, BE_TipoArticulo)
            Dim hid As HiddenField = DirectCast(e.Row.FindControl("hidNombre"), HiddenField)
            Dim hlkEliminar As HyperLink = DirectCast(e.Row.FindControl("hlkEliminar"), HyperLink)
            Dim hlkModificar As HyperLink = DirectCast(e.Row.FindControl("hlkModificar"), HyperLink)

            hid.Value = BE.cDescripcion

            If BE.bReferencia Then
                hlkEliminar.NavigateUrl = String.Format("JavaScript:alert('{0}')", Resources.SSA_Mensajes.msgIntegridadReferencial)
            Else
                hlkEliminar.NavigateUrl = String.Format("JavaScript:ExecEliminar('{0}')", BE.IdTipoArticulo)
            End If

            hlkModificar.NavigateUrl = String.Format("JavaScript:ExecModificar('{0}','{1}')", BE.IdTipoArticulo, hid.ClientID)
        End If
    End Sub

#End Region

#Region " Métodos "

    Private Function BindGridLista(ByVal sender As Object, ByVal e As EventArgs) As DataNavigatorParams
        Dim objBE_TipoArticulo As New BE_TipoArticulo
        Dim objBLT_TipoArticulo As New BLT_TipoArticulo
        Dim objPaginador As New Paginador

        objPaginador.NumeroRegistros = grvLista.PageSize
        objPaginador.NumeroPagina = dnvListado.CurrentPage

        objBE_TipoArticulo.IdTipoArticulo = 0
        objBE_TipoArticulo.cDescripcion = txtTexto.Text.Trim
        objBE_TipoArticulo.nEstado = MyConfig.getTipoArticuloEliminado
        objBE_TipoArticulo.nEstado2 = MyConfig.getSubTipoArticuloEliminado
        objBE_TipoArticulo.nEstado3 = MyConfig.getEstadoArticuloEliminado

        Dim lista As IList = objBLT_TipoArticulo.ListarPaginado(objBE_TipoArticulo, objPaginador)

        dnvListado.Visible = (objPaginador.TotalResgistros > objPaginador.NumeroRegistros)
        Return New DataNavigatorParams(lista, objPaginador.TotalResgistros)

    End Function

#End Region

#Region " Scripts " '

    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function ExecEliminar(idTipoArticulo)")
            sScript.AppendLine("{")
            sScript.AppendLineFormat("if(confirm('{0}'))", Resources.SSA_Mensajes.msgTipoArticuloEliminarConfirmar)
            sScript.AppendLine("    {")
            sScript.AppendLineFormat("document.getElementById('{0}').value=idTipoArticulo;", txtId.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnEliminar.ClientID)
            sScript.AppendLine("    }")
            sScript.AppendLine("}")

            sScript.AppendLine("function ExecModificar(idTipoArticulo, hidNombre){")
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML='{1}'", lblTitulo.ClientID, Resources.SSA_Texto.TipoArticuloModificar)
            sScript.AppendLineFormat("document.getElementById('{0}').value=idTipoArticulo", txtId.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=document.getElementById(hidNombre).value", txtDescripcion.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnOpenDetalle.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').focus();", txtDescripcion.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("")

            sScript.AppendLine("function ExecNuevo(){")
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML='{1}'", lblTitulo.ClientID, Resources.SSA_Texto.TipoArticuloNuevo)
            sScript.AppendLineFormat("document.getElementById('{0}').value=''", txtId.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=''", txtDescripcion.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnOpenDetalle.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').focus();", txtDescripcion.ClientID)
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")

            sScript.AppendLine("function ExecValida(){")
            sScript.AppendLineFormat("ID = document.getElementById('{0}').value", txtId.ClientID)
            sScript.AppendLine("    var msg=''")
            sScript.AppendLine("    if(ID.length==0){")
            sScript.AppendLineFormat("msg='{0}';", Resources.SSA_Mensajes.msgTipoArticuloRegistrarConfirmar)
            sScript.AppendLine("    }else{")
            sScript.AppendLineFormat("msg='{0}';", Resources.SSA_Mensajes.msgTipoArticuloModificarConfirmar)
            sScript.AppendLine("    }")
            sScript.AppendLine("    if(Page_ClientValidate('Grabar')){")
            sScript.AppendLine("        return confirm(msg);")
            sScript.AppendLine("    }")
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), True)
        End If
    End Sub


#End Region

End Class
