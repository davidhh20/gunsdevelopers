Imports System.Collections.Generic
Imports SSA.BusinessEntity
Imports SSA.BusinessLogic

Imports QNET.Common
Imports QNET.Web.UI.Controls

Partial Class Subasta_SSA_PublicarSubasta
    Inherits QNET.Common.BasePage

#Region " Page "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        GridEffects.RegisterGridForEffects(grvLista)
        ScriptCliente()
        GridEffects.RegisterGridForEffects(grvListaDetalle)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dnvListado.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridLista)

        If Not Page.IsPostBack Then
            If String.IsNullOrEmpty(Request.QueryString("op")) Then 'esta llamando de publicar sino de Cerrar
                CType(Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.SubastaPublicar
                grvLista.Columns(9).Visible = False
            Else
                CType(Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.SubastaCerrar
                grvLista.Columns(7).Visible = False
                grvLista.Columns(8).Visible = False
                btnNuevo.Visible = False
            End If

            CargarEstado()
            btnNuevo.OnClientClick = "return NuevoRegistro()"
            btnGrabarCerrarSubasta.OnClientClick = String.Format("return confirm('{0}')", Resources.SSA_Mensajes.msgSubastaCerrarConfirmar)
            txtCodigoSubasta.Attributes.Add("onkeypress", "SoloEnteros()")
            grvLista.EmptyDataText = Resources.SSA_Texto.FilasVacias
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        dnvListado.InvokeBind()
    End Sub

    'Aqui vamos eliminar la subasta pero se va a cambiar a estado REGISTRADO los articulos asociados
    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim objBE_Subasta As New BE_Subasta
        Dim objBLT_Subasta As New BLT_Subasta
        objBE_Subasta.id_subasta = Convert.ToInt32(hidIdSubasta.Value)
        objBE_Subasta.nestado = MyConfig.getEstadoSubastaEliminada
        objBE_Subasta.nestado2 = MyConfig.getEstadoArticuloRegistrado
        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_Subasta.Eliminar(objBE_Subasta, GlobalEntity.Instance.Usuario.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            Util.RegisterAsyncAlert(upLista, "__ExitoEliminar__", Resources.SSA_Mensajes.msgSubastaExitoEliminar)
            dnvListado.InvokeBindKeepPage()
        Else
            Util.RegisterAsyncAlert(upLista, "__ErrorEliminar__", Resources.SSA_Mensajes.msgSubastaErrorEliminar)
        End If
    End Sub

    Protected Sub grvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BE As BE_Subasta = CType(e.Row.DataItem, BE_Subasta)
            Dim hlkEliminar As HyperLink = DirectCast(e.Row.FindControl("hlkEliminar"), HyperLink)
            Dim hlkModificar As HyperLink = DirectCast(e.Row.FindControl("hlkModificar"), HyperLink)
            Dim hlkCerrar As HyperLink = DirectCast(e.Row.FindControl("hlkCerrar"), HyperLink)

            hlkEliminar.NavigateUrl = String.Format("JavaScript:EliminarItem('{0}')", BE.id_subasta)
            hlkModificar.NavigateUrl = String.Format("SSA_NuevaSubasta.aspx?ItemSubmit={0}&idIttem={1}", MyConfig.getParamGetUpdate, BE.id_subasta)

            Dim FechaInicio As DateTime = BE.dfinicio.Date
            Dim FechaFin As DateTime = BE.dfinal.Date
            Dim FechaEntre As DateTime = DateTime.Now.Date
            Dim JS As String

            If BE.nestadsub = MyConfig.getEstadoSubastaCerrado Then
                JS = String.Format("JavaScript:NoSePuedeCerrar('{0}');", Resources.SSA_Mensajes.msgSubastaCerrarNo02)
            Else
                If FechaEntre < FechaInicio Then
                    JS = String.Format("JavaScript:NoSePuedeCerrar('{0}');", Resources.SSA_Mensajes.msgSubastaCerrarNo03)
                ElseIf FechaEntre <= FechaFin Then
                    JS = String.Format("JavaScript:NoSePuedeCerrar('{0}');", Resources.SSA_Mensajes.msgSubastaCerrarNo01)
                Else
                    JS = String.Format("JavaScript:AbrirDetalle('{0}')", BE.id_subasta)
                End If
            End If
            hlkCerrar.NavigateUrl = JS
        End If
    End Sub

    Protected Sub btnListarDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarDetalle.Click
        'vamos a listar los detalles e la subasta que esten en estado EN SUBASTA
        If String.IsNullOrEmpty(hidIdSubasta.Value) Then hidIdSubasta.Value = "0"
        Dim objBLT_Subasta As New BLT_Subasta
        grvListaDetalle.DataSource = objBLT_Subasta.MiDetalle(Convert.ToInt32(hidIdSubasta.Value), MyConfig.getEstadoDetalleSubastaEnSubasta, MyConfig.getEstadoDetalleSubastaEliminado)
        grvListaDetalle.DataBind()
        Util.RegisterAsyncPressButton(upDetalle, "__OpenCerrarSubasta__", btnOpenDetalle.ClientID)
    End Sub

    Protected Sub btnGrabarCerrarSubasta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabarCerrarSubasta.Click
        'Aqui seteamos los valores para asignar el estado cerrado a la subasta y sus edtalles
        Dim objBE_Subasta As New BE_Subasta
        Dim objBLT_Subasta As New BLT_Subasta
        objBE_Subasta.id_subasta = Convert.ToInt32(hidIdSubasta.Value)
        objBE_Subasta.nestadsub = MyConfig.getEstadoSubastaCerrado

        Dim MiDetalle As New List(Of BE_Detalle_Subasta)
        Dim BE As BE_Detalle_Subasta
        For Each gvr As GridViewRow In grvListaDetalle.Rows
            BE = New BE_Detalle_Subasta
            BE.id_detsubasta = Convert.ToInt32(grvListaDetalle.DataKeys(gvr.RowIndex).Item("id_detsubasta").ToString)
            BE.nestdetsub = MyConfig.getEstadoDetalleSubastaCerrado
            MiDetalle.Add(BE)
        Next
        objBE_Subasta.MiDetalle = MiDetalle

        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_Subasta.CambiarEstado(objBE_Subasta, GlobalEntity.Instance.Usuario.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            Util.RegisterAsyncAlert(upLista, "__ExitoCerrarSubasta__", Resources.SSA_Mensajes.msgSubastaCerrarExito)
            dnvListado.InvokeBindKeepPage()
            Util.RegisterAsyncPressButton(upDetalle, "__CloseCerrarSubasta__", btnCerrarDetalle.ClientID)
        Else
            Util.RegisterAsyncAlert(upLista, "__ErrorCerrarSubasta__", Resources.SSA_Mensajes.msgSubastaCerrarError)
        End If
    End Sub

#End Region

#Region " Métodos "

    Private Function BindGridLista(ByVal sender As Object, ByVal e As EventArgs) As DataNavigatorParams
        Dim objBE_Subasta As New BE_Subasta
        Dim objBLT_Subasta As New BLT_Subasta
        Dim objPaginador As New Paginador

        objPaginador.NumeroRegistros = grvLista.PageSize
        objPaginador.NumeroPagina = dnvListado.CurrentPage

        If String.IsNullOrEmpty(txtCodigoSubasta.Text) Then
            objBE_Subasta.id_subasta = -1
        Else
            objBE_Subasta.id_subasta = Convert.ToInt32(txtCodigoSubasta.Text)
        End If
        objBE_Subasta.nestadsub = Convert.ToInt32(ddlEstadoSubasta.SelectedValue.ToString)
        objBE_Subasta.nestado2 = MyConfig.getEstadoSubastaEliminada

        Dim lista As IList = objBLT_Subasta.ListarPaginado(objBE_Subasta, objPaginador)

        dnvListado.Visible = (objPaginador.TotalResgistros > objPaginador.NumeroRegistros)
        Return New DataNavigatorParams(lista, objPaginador.TotalResgistros)

    End Function

    Private Sub CargarEstado()
        Dim objBE_Parametro As New BE_Parametro
        Dim objBLT_Parametro As New BLT_Parametro

        objBE_Parametro.nclase = MyConfig.getGrupoEstadoSubasta
        'objBE_Parametro.nSubclase = MyConfig.getSubGrupoEstadoSubasta
        objBE_Parametro.nEstado = MyConfig.getParametroEstadoActivo
        Util.VinculaDropDownList(ddlEstadoSubasta, objBLT_Parametro.Listar(objBE_Parametro), "IdParametro", "cDescripcion")
        Util.AddAllItemToDDL(ddlEstadoSubasta)
    End Sub

#End Region

#Region " Scripts "

    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then
            Dim MyScript As New MyStringBuilder
            MyScript.AppendLine("")
            MyScript.AppendLine("function NuevoRegistro(){")
            MyScript.AppendLineFormat("location = 'SSA_NuevaSubasta.aspx?ItemSubmit={0}'", MyConfig.getParamGetAdd)
            MyScript.AppendLine("      return false;")
            MyScript.AppendLine("}")
            MyScript.AppendLine("function EliminarItem(idSubasta){")
            MyScript.AppendLineFormat("msg = '{0}';", Resources.SSA_Mensajes.msgSubastaConfirmarEliminar)
            MyScript.AppendLine("   msg = msg.replace('{0}',idSubasta)")
            MyScript.AppendLine("   if(confirm(msg))")
            MyScript.AppendLine("       {")
            MyScript.AppendLineFormat("     document.getElementById('{0}').value=idSubasta;", hidIdSubasta.ClientID)
            MyScript.AppendLineFormat("     document.getElementById('{0}').click();", btnEliminar.ClientID)
            MyScript.AppendLine("       }")
            MyScript.AppendLine("}")
            'Scripts de Cerrar Subasta
            MyScript.AppendLine("function AbrirDetalle(idSubasta){")
            MyScript.AppendLineFormat("     document.getElementById('{0}').value=idSubasta;", hidIdSubasta.ClientID)
            MyScript.AppendLineFormat("     document.getElementById('{0}').innerHTML=idSubasta;", lblIdSubasta.ClientID)
            MyScript.AppendLineFormat("     document.getElementById('{0}').click();", btnListarDetalle.ClientID)
            MyScript.AppendLine("}")
            MyScript.AppendLine("function NoSePuedeCerrar(msg){")
            MyScript.AppendLine("alert(msg);")
            MyScript.AppendLine("}")
            ClientScript.RegisterStartupScript(Page.GetType, "__ScriptCliente__", MyScript.ToString, True)
        End If
    End Sub

#End Region

  
 

End Class
