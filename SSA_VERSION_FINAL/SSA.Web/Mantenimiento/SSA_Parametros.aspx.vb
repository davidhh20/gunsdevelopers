Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common
Imports QNET.Web.UI.Controls

Partial Class Mantenimiento_SSA_Parametros
    Inherits QNET.Common.BasePage

#Region " Página "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        GridEffects.RegisterGridForEffects(grvLista)
        ScriptCliente()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dnvListado.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridLista)
        If Not Page.IsPostBack Then
            DirectCast(Me.Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.Parametro
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
        Dim objBLT_Parametro As New BLT_Parametro
        Dim objBE_Parametro As New BE_Parametro

        objBE_Parametro.IdParametro = Convert.ToInt32(txtId.Text)
        objBE_Parametro.nEstado = MyConfig.getParametroEstadoEliminado

        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_Parametro.Eliminar(objBE_Parametro, GlobalEntity.Instance.Usuario.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            dnvListado.InvokeBind()
            Util.RegisterAsyncAlert(upLista, "__msgExitoElimina__", Resources.SSA_Mensajes.msgParametroEliminarExito)
        Else
            Util.RegisterAsyncAlert(upLista, "__msgErrorElimina__", Resources.SSA_Mensajes.msgParametroEliminarError)
        End If
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim objBLT_Parametro As New BLT_Parametro
        Dim objBE_Parametro As New BE_Parametro

        Dim idParametro As Integer = 0
        If Not String.IsNullOrEmpty(txtId.Text) Then
            idParametro = Convert.ToInt32(txtId.Text)
        End If

        objBE_Parametro.IdParametro = idParametro
        objBE_Parametro.nclase = Convert.ToInt32(ddlGrupo.SelectedValue.ToString)
        objBE_Parametro.cDescripcion = txtDescripcion.Text.Trim.ToUpper
        objBE_Parametro.nEstado = MyConfig.getParametroEstadoActivo
        Dim Tx As ResultadoTransaccion = Nothing

        If idParametro = 0 Then
            Tx = objBLT_Parametro.Insertar(objBE_Parametro, GlobalEntity.Instance.Usuario.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                dnvListado.InvokeBind()
                Util.RegisterAsyncAlert(upLista, "__msgExitoRegistra__", Resources.SSA_Mensajes.msgParametroRegistrarExito)
            Else
                Util.RegisterAsyncAlert(upLista, "__msgErrorRegistra__", Resources.SSA_Mensajes.msgParametroRegistrarError)
            End If
        Else
            Tx = objBLT_Parametro.Modificar(objBE_Parametro, GlobalEntity.Instance.Usuario.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                dnvListado.InvokeBind()
                Util.RegisterAsyncAlert(upLista, "__msgExitoModifica__", Resources.SSA_Mensajes.msgParametroModificarExito)
            Else
                Util.RegisterAsyncAlert(upLista, "__msgErrorModifica__", Resources.SSA_Mensajes.msgParametroModificarError)
            End If
        End If
        Util.RegisterAsyncPressButton(upLista, "__CierraDetalle__", btnCerrarDetalle.ClientID)
    End Sub

    Protected Sub grvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_Parametro = CType(e.Row.DataItem, BE_Parametro)
            Dim hid As HiddenField = DirectCast(e.Row.FindControl("hidNombre"), HiddenField)
            Dim hlkEliminar As HyperLink = DirectCast(e.Row.FindControl("hlkEliminar"), HyperLink)
            Dim hlkModificar As HyperLink = DirectCast(e.Row.FindControl("hlkModificar"), HyperLink)

            hid.Value = BE.cDescripcion

            If BE.beliminado Then
                hlkEliminar.NavigateUrl = String.Format("JavaScript:ExecEliminar('{0}')", BE.IdParametro)
            Else
                hlkEliminar.NavigateUrl = String.Format("JavaScript:alert('{0}')", Resources.SSA_Mensajes.msgParametroEliminarNoSePuede)
            End If
            If BE.bmodificar Then
                hlkModificar.NavigateUrl = String.Format("JavaScript:ExecModificar('{0}','{1}','{2}')", BE.IdParametro, BE.nclase, hid.ClientID)
            Else
                hlkModificar.NavigateUrl = String.Format("JavaScript:alert('{0}')", Resources.SSA_Mensajes.msgParametroModificarNoSePuede)
            End If


        End If
    End Sub

#End Region

#Region " Métodos "

    Private Function BindGridLista(ByVal sender As Object, ByVal e As EventArgs) As DataNavigatorParams
        Dim objBE_Parametro As New BE_Parametro
        Dim objBLT_Parametro As New BLT_Parametro
        Dim objPaginador As New Paginador

        objPaginador.NumeroRegistros = grvLista.PageSize
        objPaginador.NumeroPagina = dnvListado.CurrentPage

        objBE_Parametro.nEstado = MyConfig.getParametroEstadoEliminado
        objBE_Parametro.nclase = Convert.ToInt32(ddlFiltroGrupo.SelectedValue.ToString)
        objBE_Parametro.cDescripcion = txtTexto.Text

        Dim lista As IList = objBLT_Parametro.ListarPaginado(objBE_Parametro, objPaginador)

        dnvListado.Visible = (objPaginador.TotalResgistros > objPaginador.NumeroRegistros)
        Return New DataNavigatorParams(lista, objPaginador.TotalResgistros)

    End Function

    Private Sub CargarComboTipo()
        Dim objBLT_Parametro As New BLT_Parametro
        Util.VinculaDropDownList(ddlFiltroGrupo, objBLT_Parametro.ListarGrupos(), "IdParametro", "cDescripcion")
        Util.VinculaDropDownList(ddlGrupo, objBLT_Parametro.ListarGrupos(), "IdParametro", "cDescripcion")
        Util.AddAllItemToDDL(ddlFiltroGrupo)
        Util.AddSelectItemToDDL(ddlGrupo)
    End Sub

#End Region

#Region " Scripts " '

    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function ExecEliminar(idParametro)")
            sScript.AppendLine("{")
            sScript.AppendLineFormat("if(confirm('{0}'))", Resources.SSA_Mensajes.msgParametroEliminarConfirmar)
            sScript.AppendLine("    {")
            sScript.AppendLineFormat("document.getElementById('{0}').value=idParametro;", txtId.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnEliminar.ClientID)
            sScript.AppendLine("    }")
            sScript.AppendLine("}")

            sScript.AppendLine("function ExecModificar(idParametro,idGrupo, hidNombre){")
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML='{1}'", lblTitulo.ClientID, Resources.SSA_Texto.SubTipoArticuloModificar)
            sScript.AppendLineFormat("document.getElementById('{0}').value=idParametro", txtId.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').disabled='disabled'", ddlGrupo.ClientID)
            sScript.AppendLineFormat("seleccionarCombo(document.getElementById('{0}') , idGrupo);", ddlGrupo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=document.getElementById(hidNombre).value", txtDescripcion.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnOpenDetalle.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').focus();", txtDescripcion.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("")

            sScript.AppendLine("function ExecNuevo(){")
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML='{1}'", lblTitulo.ClientID, Resources.SSA_Texto.SubTipoArticuloNuevo)
            sScript.AppendLineFormat("document.getElementById('{0}').value=''", txtId.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=''", txtDescripcion.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').selectedIndex=0", ddlGrupo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').disabled=''", ddlGrupo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnOpenDetalle.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').focus();", txtDescripcion.ClientID)
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")

            sScript.AppendLine("function ExecValida(){")
            sScript.AppendLineFormat("ID = document.getElementById('{0}').value", txtId.ClientID)
            sScript.AppendLine("    var msg=''")
            sScript.AppendLine("    if(ID.length==0){")
            sScript.AppendLineFormat("msg='{0}';", Resources.SSA_Mensajes.msgParametroRegistrarConfirmar)
            sScript.AppendLine("    }else{")
            sScript.AppendLineFormat("msg='{0}';", Resources.SSA_Mensajes.msgParametroModificarConfirmar)
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
