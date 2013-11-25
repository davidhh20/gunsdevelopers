Imports System.Collections.Generic
Imports SSA.BusinessEntity
Imports SSA.BusinessLogic
Imports QNET.Common
Imports QNET.Web.UI.Controls


Partial Class Subasta_SSA_Adjudicar
    Inherits QNET.Common.BasePage

#Region " Pagina "

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ScriptCliente()
        GridEffects.RegisterGridForEffects(grvLista)
        GridEffects.RegisterGridForEffects(grvListaOferta)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dnvListado.BindGridView = New QNET.Web.UI.Controls.DataNavigator.BindGridViewDelegate(AddressOf BindGridLista)
        If Not Page.IsPostBack Then
            DirectCast(Me.Master, MasterPage_SSA_Principal).setTitulo = Resources.SSA_Texto.SubastaAdjudicar
            grvLista.EmptyDataText = Resources.SSA_Texto.AdjudicacionFilasVacias
            dnvListado.InvokeBind()
            btnEnviarCorreo.OnClientClick = "return GrabarEnvioCorreo();"
            btnVendido.OnClientClick = "return GrabarVendido();"
            btnGrabarAdjudicacion.OnClientClick = String.Format("return confirm('{0}')", Resources.SSA_Mensajes.msgAdjudicarConfirmar)
        End If
    End Sub

    Protected Sub btnListarOfertas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarOfertas.Click
        Dim objBLT_Oferta As New BLT_Oferta
        Dim objBE_Oferta As New BE_Oferta
        objBE_Oferta.id_detsubasta = Convert.ToInt32(hidIdDetalleSubasta.Value)
        objBE_Oferta.nestado = MyConfig.getEstadoOfertaEliminada
        objBE_Oferta.BE_Adjudicacion.nestado = MyConfig.getEstadoAdjudicacionEliminado
        grvListaOferta.DataSource = objBLT_Oferta.Listar(objBE_Oferta)
        grvListaOferta.DataBind()
        Util.RegisterAsyncPressButton(upListaOferta, "__AbrirListaOfertas__", btnOpenListaOfertas.ClientID)
    End Sub

    Protected Sub grvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hlk As HyperLink = DirectCast(e.Row.FindControl("hlkOfertas"), HyperLink)
            Dim BE As BE_Detalle_Subasta = CType(e.Row.DataItem, BE_Detalle_Subasta) 'Descripcion,Codigo,Marca,Modelo,Anio
            hlk.Visible = (BE.nestdetsub = MyConfig.getEstadoDetalleSubastaCerrado)
            hlk.NavigateUrl = String.Format("JavaScript:AbrirListaOfertas('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", _
                                            BE.id_detsubasta, BE.BE_Articulo.ccodart, BE.BE_Articulo.nsiniestro, BE.BE_Articulo.nprecio_base, BE.des_estado, _
                                            BE.BE_Articulo.cdescrip_breve, BE.BE_Articulo.cmarca, BE.BE_Articulo.cmodelo, BE.BE_Articulo.canio)
        End If
    End Sub

#End Region

#Region "Adjudicar Articulo "

    Protected Sub grvListaOferta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hlk As HyperLink = DirectCast(e.Row.FindControl("hlkCorreo"), HyperLink)
            Dim chk As CheckBox = DirectCast(e.Row.FindControl("chk"), CheckBox)
            Dim txt As TextBox = DirectCast(e.Row.FindControl("txtComentario"), TextBox)
            Dim BE As BE_Oferta = CType(e.Row.DataItem, BE_Oferta)
            hlk.NavigateUrl = String.Format("JavaScript:EnviarCorreo('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("hh:mm"), _
                                                BE.BE_Usuario.NombreApellido, BE.BE_Usuario.ccorreo, BE.BE_Usuario.ccorreoalt, chk.ClientID, txt.ClientID)
            chk.Attributes.Add("onclick", String.Format("CheckeaCheck(this,'{0}')", txt.ClientID))

            If BE.BE_Usuario.nmarca = MyConfig.getMarcaCompradorDudoso Then
                e.Row.Cells(4).Text = "X"
            End If

            'setear lo adjudicado si existiera: Setear el comentario, si esta vendido o no
            If BE.id_adjudicacion > 0 Then
                txt.Text = BE.BE_Adjudicacion.cComentario
                If BE.BE_Adjudicacion.fvendido Then
                    e.Row.Cells(6).Text = "SI"
                    chk.Checked = True
                    txt.Enabled = True
                Else
                    e.Row.Cells(6).Text = "NO"
                End If
            End If

        End If
    End Sub

    Protected Sub btnEnviarCorreo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviarCorreo.Click
        Dim miCorreo As New correo
        miCorreo.Body = CuerpoCorreo.CuerpoAdjudicacion(lblPostor.Text, txtCuerpoCorreo.Text)

        Dim Correo1 As String = hidCorreo1.Value
        Dim Correo2 As String = hidCorreo2.Value
        If String.IsNullOrEmpty(Correo1) AndAlso String.IsNullOrEmpty(Correo2) Then
            Util.RegisterAsyncAlert(upProxy, "__NoHayEnvioCorreo__", Resources.SSA_Mensajes.msgAdjudicarNoHayCorreo)
        Else
            Try
                miCorreo.Enviar(Correo1, Correo2, txtAsuntoCorreo.Text)
                Util.RegisterScript(upProxy, "__ExitoEnvioCorreo__", "EnvioCorreo()")
            Catch ex As Exception
                Util.RegisterAsyncAlert(upProxy, "__ErrorEnvioCorreo__", Resources.SSA_Mensajes.msgAdjudicarErrorEnvioCorreo)
            End Try
        End If

    End Sub

#End Region

#Region " Metodos "

    Private Function BindGridLista(ByVal sender As Object, ByVal e As EventArgs) As DataNavigatorParams

        Dim objBE_ListaOferta As New BE_ListaOferta
        Dim objBLT_Subasta As New BLT_Subasta
        Dim objPaginador As New Paginador

        objPaginador.NumeroRegistros = Int32.MaxValue 'grvLista.PageSize
        objPaginador.NumeroPagina = dnvListado.CurrentPage

        objBE_ListaOferta.id_subasta = 0
        objBE_ListaOferta.nestado = 0
        objBE_ListaOferta.nestado2 = MyConfig.getEstadoDetalleSubastaEliminado
        objBE_ListaOferta.nestado3 = MyConfig.getEstadoSubastaEliminada
        objBE_ListaOferta.nestado4 = MyConfig.getEstadoSubastaCerrado

        Dim lista As IList = objBLT_Subasta.ListarPaginado(objBE_ListaOferta, objPaginador)

        dnvListado.Visible = (objPaginador.TotalResgistros > objPaginador.NumeroRegistros)
        Return New DataNavigatorParams(lista, objPaginador.TotalResgistros)

    End Function

    Private Function Adjudicar() As Boolean
        Dim objBLT_Adjudicacion As New BLT_Adjudicacion
        Dim miLista As New List(Of BE_Adjudicacion)
        Dim BE As BE_Adjudicacion
        Dim idAdjudicacion As Integer

        For Each grv As GridViewRow In grvListaOferta.Rows
            idAdjudicacion = Convert.ToInt32(grvListaOferta.DataKeys(grv.RowIndex).Item("id_adjudicacion").ToString)
            Dim chk As CheckBox = DirectCast(grv.FindControl("chk"), CheckBox)

            If idAdjudicacion = 0 Then
                If chk.Checked Then
                    BE = New BE_Adjudicacion
                    BE.id_adjudicacion = 0
                    BE.id_oferta = Convert.ToInt32(grvListaOferta.DataKeys(grv.RowIndex).Item("id_oferta").ToString)
                    BE.dfnotificacion = DateTime.Now
                    BE.fvendido = True
                    BE.nestado = MyConfig.getEstadoAdjudicacionActivo
                    BE.cComentario = DirectCast(grv.FindControl("txtComentario"), TextBox).Text
                    miLista.Add(BE)
                End If
            Else
                BE = New BE_Adjudicacion
                BE.id_adjudicacion = idAdjudicacion
                BE.dfnotificacion = IIf(chk.Checked, DateTime.Now, Nothing)
                BE.fvendido = chk.Checked
                BE.cComentario = DirectCast(grv.FindControl("txtComentario"), TextBox).Text
                miLista.Add(BE)
            End If
        Next

        Dim Tx As ResultadoTransaccion = Nothing
        Tx = objBLT_Adjudicacion.Insertar(miLista, GlobalEntity.Instance.Usuario.id_usuario)
        If Tx.EnuTipoResultado = TipoResultado.Exito Then
            Util.RegisterAsyncAlert(upLista, "__AdjudicacionExito__", Resources.SSA_Mensajes.msgAdjudicarExitoRegistro)
            Return True
        Else
            Util.RegisterAsyncAlert(upLista, "__AdjudicacionError__", Resources.SSA_Mensajes.msgAdjudicarErrorRegistro)
            Return False
        End If
    End Function

#End Region

#Region " Scripts "

    Private Sub ScriptCliente()
        If Not ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__") Then
            Dim sScript As New MyStringBuilder
            sScript.AppendLine("")
            sScript.AppendLine("function EnviarCorreo(Fecha, Hora, Usuario, Correo1, Correo2, chkID, txtID){")
            sScript.AppendLine("chk = document.getElementById(chkID);")
            sScript.AppendLine("if(VerificarRepetido(chk)) return ;")
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Fecha;", lblFecha.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Hora;", lblHora.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Usuario;", lblPostor.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Correo1;", lblCorreo1.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=Correo1;", hidCorreo1.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Correo2;", lblCorreo2.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=Correo2;", hidCorreo2.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=chkID;", hidIdChk.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value='';", txtAsuntoCorreo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value='';", txtCuerpoCorreo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').value=txtID;", hidIdTxtComentario.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnOpenEnvioCorreo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').focus();", txtAsuntoCorreo.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("function AbrirListaOfertas(IdDetSubasta, Codigo, Siniestro, PrecioBase, Estado, Descripcion,Marca,Modelo,Anio){")
            sScript.AppendLineFormat("document.getElementById('{0}').value=IdDetSubasta;", hidIdDetalleSubasta.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Codigo;", lblCodigo.ClientID)
            sScript.AppendLine("if(Siniestro==0){")
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML='';", lblNroSiniestro.ClientID)
            sScript.AppendLine("}else{")
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Siniestro;", lblNroSiniestro.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=PrecioBase;", lblPrecioBase.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Estado;", lblEstado.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Descripcion;", lblDescripcionArticuloEnvioCorreo.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Codigo;", lblCodigoEnvio.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Marca;", lblMarcaEnvio.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Modelo;", lblModeloEnvio.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').innerHTML=Anio;", lblAnioEnvio.ClientID)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnListarOfertas.ClientID)
            sScript.AppendLine("}")
            'envio de correo
            sScript.AppendLine("function EnvioCorreo()")
            sScript.AppendLine("{")
            sScript.AppendLineFormat("chkID = document.getElementById('{0}').value;", hidIdChk.ClientID)
            sScript.AppendLineFormat("txtID = document.getElementById('{0}').value;", hidIdTxtComentario.ClientID)
            sScript.AppendLine("      document.getElementById(chkID).checked=true;")
            sScript.AppendLine("      document.getElementById(txtID).disabled='';")
            sScript.AppendLineFormat("alert('{0}');", Resources.SSA_Mensajes.msgAdjudicarExitoEnvioCorreo)
            sScript.AppendLineFormat("document.getElementById('{0}').click();", btnCerrarEnvioCorreo.ClientID)
            sScript.AppendLine("}")
            sScript.AppendLine("function GrabarEnvioCorreo()")
            sScript.AppendLine("{")
            sScript.AppendLine("    if(Page_ClientValidate('Correo')){")
            sScript.AppendLineFormat("  return confirm('{0}');", Resources.SSA_Mensajes.msgAdjudicarConfirmar)
            sScript.AppendLine("    }")
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")
            'Adjudicacion de articulo GrabarVendido
            sScript.AppendLine("function GrabarVendido(){")
            sScript.AppendLine("    i=false;")
            sScript.AppendLine("    f = document.forms[0].elements;")
            sScript.AppendLine("    for(x=0; x<f.length; x++){")
            sScript.AppendLine("        obj = f[x];")
            sScript.AppendLine("        if(obj.type=='checkbox'){")
            sScript.AppendLineFormat("      lnIDcomp = obj.id.replace('{0}','').length;", grvListaOferta.ClientID)
            sScript.AppendLine("            if(obj.id.length!=lnIDcomp){")
            sScript.AppendLine("                if(obj.checked){")
            sScript.AppendLine("                    i=true; break;")
            sScript.AppendLine("                }")
            sScript.AppendLine("            }")
            sScript.AppendLine("        }")
            sScript.AppendLine("    }")
            sScript.AppendLine("    if(i){")
            sScript.AppendLineFormat("  return confirm('{0}')", Resources.SSA_Mensajes.msgAdjudicarVendidoConfirmar)
            sScript.AppendLine("    }")
            sScript.AppendLineFormat("alert('{0}');", Resources.SSA_Mensajes.msgAdjudicarVendidoNoHay)
            sScript.AppendLine("    return false;")
            sScript.AppendLine("}")
            sScript.AppendLine("function VerificarRepetido(chk){")
            sScript.AppendLine("    i=false;")
            sScript.AppendLine("    f = document.forms[0].elements;")
            sScript.AppendLine("    for(x=0; x<f.length; x++){")
            sScript.AppendLine("        obj = f[x];")
            sScript.AppendLine("        if(obj.type=='checkbox'){")
            sScript.AppendLineFormat("      lnIDcomp = obj.id.replace('{0}','').length;", grvListaOferta.ClientID)
            sScript.AppendLine("            if(obj.id.length!=lnIDcomp && obj.id!=chk.id){")
            sScript.AppendLine("                if(obj.checked){")
            sScript.AppendLine("                    i=true; break;")
            sScript.AppendLine("                }")
            sScript.AppendLine("            }")
            sScript.AppendLine("        }")
            sScript.AppendLine("    }")
            sScript.AppendLine("    if(i){")
            sScript.AppendLineFormat("  alert('{0}');", Resources.SSA_Mensajes.msgAdjudicarDuplicado)
            sScript.AppendLine("        chk.checked=false;")
            sScript.AppendLine("    }")
            sScript.AppendLine("    return i;")
            sScript.AppendLine("}")
            sScript.AppendLine("function CheckeaCheck(chk, txtID){")
            sScript.AppendLine("    if(!VerificarRepetido(chk)){")
            sScript.AppendLine("        if(chk.checked){")
            sScript.AppendLine("            document.getElementById(txtID).disabled='';")
            sScript.AppendLine("        }else{")
            sScript.AppendLine("            document.getElementById(txtID).disabled='disabled';")
            sScript.AppendLine("        }")
            sScript.AppendLine("    }")
            sScript.AppendLine("}")
            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), True)
        End If
    End Sub

#End Region


    
    Protected Sub btnGrabarAdjudicacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabarAdjudicacion.Click
        If Adjudicar() Then
            dnvListado.InvokeBind()
            Util.RegisterAsyncPressButton(upLista, "__CerrarDetalleAdjudicacion__", btnCerrarListaOfertas.ClientID)
        End If
    End Sub

    Protected Sub btnVendido_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVendido.Click
        If Adjudicar() Then
            Dim objBLT_Subasta As New BLT_Subasta
            Dim objBE_Detalle_Subasta As New BE_Detalle_Subasta
            Dim Tx As ResultadoTransaccion = Nothing
            objBE_Detalle_Subasta.id_detsubasta = Convert.ToInt32(hidIdDetalleSubasta.Value)
            objBE_Detalle_Subasta.nestdetsub = MyConfig.getEstadoDetalleSubastaVendido
            Tx = objBLT_Subasta.CambiarEstado_Detalle(objBE_Detalle_Subasta, GlobalEntity.Instance.Usuario.id_usuario)
            If Tx.EnuTipoResultado = TipoResultado.Exito Then
                dnvListado.InvokeBind()
                Util.RegisterAsyncAlert(upLista, "__VendidoExito__", Resources.SSA_Mensajes.msgAdjudicarVendidoExito)
                Util.RegisterAsyncPressButton(upLista, "__CerrarDetalleAdjudicacion__", btnCerrarListaOfertas.ClientID)
            Else
                Util.RegisterAsyncAlert(upLista, "__ErrorExito__", Resources.SSA_Mensajes.msgAdjudicarVendidoError)
            End If

        End If
    End Sub

End Class

