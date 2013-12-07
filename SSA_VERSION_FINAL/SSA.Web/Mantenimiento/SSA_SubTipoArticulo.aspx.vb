Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common
Imports QNET.Web.UI.Controls

Partial Class Mantenimiento_SSA_SubTipoArticulo
    Inherits QNET.Common.BasePage

#Region " Página "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        GridEffects.RegisterGridForEffects(grvLista)
        ScriptCliente()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dnvListado.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridLista)
        If Not Page.IsPostBack Then
            DirectCast(Me.Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.SubTipoArticulo
            CargarComboTipo()
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
        Dim objBLT_SubTipoArticulo As New BLT_SubTipoArticulo
        Dim objBE_SubTipoArticulo As New BE_SubTipoArticulo

        objBE_SubTipoArticulo.IdSubTipoArticulo = Convert.ToInt32(txtId.Text)
        objBE_SubTipoArticulo.nEstado = MyConfig.getSubTipoArticuloEliminado

        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_SubTipoArticulo.Eliminar(objBE_SubTipoArticulo, GlobalEntity.Instance.Usuario.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            dnvListado.InvokeBind()
            Util.RegisterAsyncAlert(upLista, "__msgExitoElimina__", Resources.SSA_Mensajes.msgSubTipoArticuloEliminarExito)
        Else
            Util.RegisterAsyncAlert(upLista, "__msgErrorElimina__", Resources.SSA_Mensajes.msgSubTipoArticuloEliminarError)
        End If
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim objBLT_SubTipoArticulo As New BLT_SubTipoArticulo
        Dim objBE_SubTipoArticulo As New BE_SubTipoArticulo

        Dim idSubTipoArticulo As Integer = 0
        If Not String.IsNullOrEmpty(txtId.Text) Then
            idSubTipoArticulo = Convert.ToInt32(txtId.Text)
        End If

        objBE_SubTipoArticulo.IdSubTipoArticulo = idSubTipoArticulo
        objBE_SubTipoArticulo.IdTipoArticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue.ToString)
        objBE_SubTipoArticulo.cDescripcion = txtDescripcion.Text.Trim.ToUpper
        objBE_SubTipoArticulo.nEstado = MyConfig.getSubTipoArticuloActivo
        Dim Tx As ResultadoTransaccion = Nothing

        If idSubTipoArticulo = 0 Then
            Tx = objBLT_SubTipoArticulo.Insertar(objBE_SubTipoArticulo, GlobalEntity.Instance.Usuario.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                dnvListado.InvokeBind()
                Util.RegisterAsyncAlert(upLista, "__msgExitoRegistra__", Resources.SSA_Mensajes.msgSubTipoArticuloRegistrarExito)
            Else
                Util.RegisterAsyncAlert(upLista, "__msgErrorRegistra__", Resources.SSA_Mensajes.msgSubTipoArticuloRegistrarError)
            End If
        Else
            Tx = objBLT_SubTipoArticulo.Modificar(objBE_SubTipoArticulo, GlobalEntity.Instance.Usuario.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                dnvListado.InvokeBind()
                Util.RegisterAsyncAlert(upLista, "__msgExitoModifica__", Resources.SSA_Mensajes.msgSubTipoArticuloModificarExito)
            Else
                Util.RegisterAsyncAlert(upLista, "__msgErrorModifica__", Resources.SSA_Mensajes.msgSubTipoArticuloModificarError)
            End If
        End If
        Util.RegisterAsyncPressButton(upLista, "__CierraDetalle__", btnCerrarDetalle.ClientID)
    End Sub

    Protected Sub grvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_SubTipoArticulo = CType(e.Row.DataItem, BE_SubTipoArticulo)
            Dim hid As HiddenField = DirectCast(e.Row.FindControl("hidNombre"), HiddenField)
            Dim hlkEliminar As HyperLink = DirectCast(e.Row.FindControl("hlkEliminar"), HyperLink)
            Dim hlkModificar As HyperLink = DirectCast(e.Row.FindControl("hlkModificar"), HyperLink)

            hid.Value = BE.cDescripcion

            If BE.bReferencia Then
                hlkEliminar.NavigateUrl = String.Format("JavaScript:alert('{0}')", Resources.SSA_Mensajes.msgIntegridadReferencial)
            Else
                hlkEliminar.NavigateUrl = String.Format("JavaScript:ExecEliminar('{0}')", BE.IdSubTipoArticulo)
            End If

            hlkModificar.NavigateUrl = String.Format("JavaScript:ExecModificar('{0}','{1}','{2}')", BE.IdSubTipoArticulo, BE.IdTipoArticulo, hid.ClientID)
        End If
    End Sub

#End Region

#Region " Métodos "

    Private Function BindGridLista(ByVal sender As Object, ByVal e As EventArgs) As DataNavigatorParams
        Dim objBE_SubTipoArticulo As New BE_SubTipoArticulo
        Dim objBLT_SubTipoArticulo As New BLT_SubTipoArticulo
        Dim objPaginador As New Paginador

        objPaginador.NumeroRegistros = grvLista.PageSize
        objPaginador.NumeroPagina = dnvListado.CurrentPage

        objBE_SubTipoArticulo.IdSubTipoArticulo = 0
        objBE_SubTipoArticulo.IdTipoArticulo = Convert.ToInt32(ddlFiltroTipo.SelectedValue.ToString)
        objBE_SubTipoArticulo.cDescripcion = txtTexto.Text.Trim
        objBE_SubTipoArticulo.nEstado = MyConfig.getSubTipoArticuloEliminado
        objBE_SubTipoArticulo.nEstado2 = MyConfig.getEstadoArticuloEliminado

        Dim lista As IList = objBLT_SubTipoArticulo.ListarPaginado(objBE_SubTipoArticulo, objPaginador)

        dnvListado.Visible = (objPaginador.TotalResgistros > objPaginador.NumeroRegistros)
        Return New DataNavigatorParams(lista, objPaginador.TotalResgistros)

    End Function

    Private Sub CargarComboTipo()
        Dim objBLT_TipoArticulo As New BLT_TipoArticulo
        Dim objBE_TipoArticulo As New BE_TipoArticulo
        objBE_TipoArticulo.nEstado = MyConfig.getTipoArticuloActivo
        Dim Lista As IList = objBLT_TipoArticulo.Listar(objBE_TipoArticulo)
        Util.VinculaDropDownList(ddlFiltroTipo, Lista, "IdTipoArticulo", "cDescripcion")
        Util.VinculaDropDownList(ddlTipoArticulo, Lista, "IdTipoArticulo", "cDescripcion")
        Util.AddAllItemToDDL(ddlFiltroTipo)
        Util.AddSelectItemToDDL(ddlTipoArticulo)
    End Sub
#End Region

#Region " Scripts " '

    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function ExecEliminar(idSubTipoArticulo)")
            sScript.AppendLine("{")
            sScript.AppendLineFormat("if(confirm('{0}'))", Resources.SSA_Mensajes.msgSubTipoArticuloEliminarConfirmar)
            sScript.AppendLine("    {")
            sScript.AppendLineFormat("document.getElementById('{0}').value=idSubTipoArticulo;", txtId.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnEliminar.ClientID)
            sScript.AppendLine("    }")
            sScript.AppendLine("}")

            sScript.AppendLine("function ExecModificar(idSubTipoArticulo,idTipoArticulo, hidNombre){")
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML='{1}'", lblTitulo.ClientID, Resources.SSA_Texto.SubTipoArticuloModificar)
            sScript.AppendLineFormat("document.getElementById('{0}').value=idSubTipoArticulo", txtId.ClientID)
            sScript.AppendLineFormat("seleccionarCombo(document.getElementById('{0}') , idTipoArticulo);", ddlTipoArticulo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=document.getElementById(hidNombre).value", txtDescripcion.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnOpenDetalle.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').focus();", txtDescripcion.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("")

            sScript.AppendLine("function ExecNuevo(){")
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML='{1}'", lblTitulo.ClientID, Resources.SSA_Texto.SubTipoArticuloNuevo)
            sScript.AppendLineFormat("document.getElementById('{0}').value=''", txtId.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=''", txtDescripcion.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').selectedIndex=0", ddlTipoArticulo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnOpenDetalle.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').focus();", txtDescripcion.ClientID)
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")

            sScript.AppendLine("function ExecValida(){")
            sScript.AppendLineFormat("ID = document.getElementById('{0}').value", txtId.ClientID)
            sScript.AppendLine("    var msg=''")
            sScript.AppendLine("    if(ID.length==0){")
            sScript.AppendLineFormat("msg='{0}';", Resources.SSA_Mensajes.msgSubTipoArticuloRegistrarConfirmar)
            sScript.AppendLine("    }else{")
            sScript.AppendLineFormat("msg='{0}';", Resources.SSA_Mensajes.msgSubTipoArticuloModificarConfirmar)
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
