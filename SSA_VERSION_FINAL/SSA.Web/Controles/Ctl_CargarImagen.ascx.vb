Imports System
Imports System.Web
Imports System.IO
Imports QNET.Common

Partial Class Controles_Ctl_CargarImagen
    Inherits System.Web.UI.UserControl

#Region " Propiedades "

    Public Property RutaImagen() As String
        Get
            Return hidRutaArchivo.Value
        End Get
        Set(ByVal value As String)
            hidRutaArchivo.Value = value
        End Set
    End Property

    Public Sub ResetearPanel()
        hidRutaArchivo.Value = String.Empty
        lblPropiedades.Text = String.Empty
        imgFile.ImageUrl = String.Empty
        pnlResultado.Attributes.Add("style", "display:none")
        pnlCarga.Attributes.Add("style", "display:block")
    End Sub
    '1.- Si el nombre de archivo tiene longitud=0 entonces no hace nada
    Public Sub MostrarImagen(ByVal NombreArchivo As String, ByVal Tamanio As Double)
        If String.IsNullOrEmpty(NombreArchivo) Then Exit Sub
        Dim RutaCompleta As String = String.Format("{0}{1}", MyConfig.getRutaFisicaImagenes, NombreArchivo)
        If File.Exists(RutaCompleta) Then
            If Tamanio = 0 Then 'si es 0 se debe calcular el tamaño
                Dim miFile As New FileInfo(RutaCompleta)
                Tamanio = Decimal.Round(Convert.ToDecimal(miFile.Length / TamanioKB), 2, MidpointRounding.AwayFromZero)
            End If
            hidRutaArchivo.Value = NombreArchivo
            lblPropiedades.Text = String.Format("Tamaño<br>{0}KB", Tamanio)
            imgFile.ImageUrl = String.Format("{0}{1}", MyConfig.getRutaWebImagenes, NombreArchivo)
            pnlResultado.Attributes.Add("style", "display:block")
            pnlCarga.Attributes.Add("style", "display:none")
        Else
            ResetearPanel()
        End If
    End Sub
    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        ResetearPanel()
    End Sub
#End Region

#Region " Página "

    Dim TamanioKB As Double = 1024D

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ScripCliente()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FileUp.Attributes.Add("onchange", String.Format("SubirArchivo_{0}()", Me.UniqueID))
        If Not Page.IsPostBack Then
            hlkElimina.NavigateUrl = String.Format("JavaScript:EliminarRuta_{0}()", Me.UniqueID)
            ibtnAdjuntar.OnClientClick = String.Format("return SubirArchivo_{0}()", Me.UniqueID)
        End If
    End Sub

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        If FileUp.HasFile Then
            Dim NombreArchivo As String = "img_" & String.Format("{0:yyyyMMdd_hhmmss}_{1}", DateTime.Now, FileUp.FileName)
            Dim CarpetaImagenes As String = MyConfig.getRutaFisicaImagenes
            Dim Tamanio As Double = Decimal.Round(Convert.ToDecimal(FileUp.PostedFile.ContentLength / TamanioKB), 2, MidpointRounding.AwayFromZero)
            FileUp.SaveAs(String.Format("{0}{1}", CarpetaImagenes, NombreArchivo))
            MostrarImagen(NombreArchivo, Tamanio)
        End If
    End Sub


#End Region

#Region " Scripts "

    Private Sub ScripCliente()
        If Not Me.Page.ClientScript.IsClientScriptBlockRegistered(String.Format("__FuncionalidadArticulo_{0}__", Me.UniqueID)) Then

            Dim sbScript As New MyStringBuilder
            sbScript.AppendLine("")
            sbScript.AppendLineFormat("function SubirArchivo_{0}()", Me.UniqueID)
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat(" f = document.getElementById('{0}');", FileUp.ClientID)
            sbScript.AppendLine("       var fileName = f.value; ")
            sbScript.AppendLine("       fileName = fileName.slice(fileName.lastIndexOf('\\') + 1);")
            sbScript.AppendLine("       fileName = fileName.toLowerCase();")
            sbScript.AppendLine("       if ( fileName.length > 0 ){")
            sbScript.AppendLine("           var listado = new Array('.gif', '.jpg', '.png');")
            sbScript.AppendLine("           var ext = (fileName.substring(fileName.lastIndexOf('.'), fileName.length));")
            sbScript.AppendLine("           var ok = false;")
            sbScript.AppendLine("           for (var i = 0; i < listado.length; i++){")
            sbScript.AppendLine("               if (listado[i] == ext) {")
            sbScript.AppendLine("                   ok = true;")
            sbScript.AppendLine("                   break;")
            sbScript.AppendLine("               }")
            sbScript.AppendLine("           }")
            sbScript.AppendLine("           if(!ok){")
            sbScript.AppendLineFormat("         msg = '{0}'", Resources.SSA_Mensajes.msgAdjuntarSinExtension)
            sbScript.AppendLine("               msg = msg.replace('{0}',listado.join(' '))")
            sbScript.AppendLine("               alert(msg);")
            sbScript.AppendLineFormat("         ReseteaRuta_{0}()", Me.UniqueID)
            sbScript.AppendLine("               return false;")
            sbScript.AppendLine("           }")
            sbScript.AppendLine("       }else{")
            sbScript.AppendLineFormat("     alert('{0}')", Resources.SSA_Mensajes.msgAdjuntarNoHayArchivo)
            sbScript.AppendLine("           return false;")
            sbScript.AppendLine("       }")
            sbScript.AppendLineFormat("if(confirm('{0}'))", Resources.SSA_Mensajes.msgAdjuntarConfirmarCarga)
            sbScript.AppendLine("       {")
            sbScript.AppendLineFormat(" document.getElementById('{0}').click();", btnSubir.ClientID)
            sbScript.AppendLine("       }else{")
            sbScript.AppendLineFormat("     ReseteaRuta_{0}()", Me.UniqueID)
            sbScript.AppendLine("       }")
            sbScript.AppendLine("   return false;")
            sbScript.AppendLine("}")
            sbScript.AppendLineFormat("function ReseteaRuta_{0}()", Me.UniqueID)
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat(" f = document.getElementById('{0}');", FileUp.ClientID)
            sbScript.AppendLine("   nuevoFile = document.createElement('input');")
            sbScript.AppendLine("   nuevoFile.id = f.id;")
            sbScript.AppendLine("   nuevoFile.type = 'file';")
            sbScript.AppendLine("   nuevoFile.name = f.name;")
            sbScript.AppendLine("   nuevoFile.value = '';")
            sbScript.AppendLine("   nuevoFile.onchange = f.onchange;")
            sbScript.AppendLine("   nuevoFile.style.width = f.style.width;")
            sbScript.AppendLine("   nodoPadre = f.parentNode;")
            sbScript.AppendLine("   nodoSiguiente = f.nextSibling;")
            sbScript.AppendLine("   nodoPadre.removeChild(f);")
            sbScript.AppendLine("   (nodoSiguiente == null) ? nodoPadre.appendChild(nuevoFile):")
            sbScript.AppendLine("   nodoPadre.insertBefore(nuevoFile, nodoSiguiente);")
            sbScript.AppendLine("}")
            sbScript.AppendLineFormat("function EliminarRuta_{0}()", Me.UniqueID)
            sbScript.AppendLine("{")
            sbScript.AppendLineFormat("if(confirm('{0}'))", Resources.SSA_Mensajes.msgAdjuntarConfirmarEliminar)
            sbScript.AppendLine("      {")
            sbScript.AppendLineFormat("     document.getElementById('{0}').click();", btnEliminar.ClientID)
            sbScript.AppendLine("      }")
            sbScript.AppendLine("}")
            sbScript.AppendLine()

            Me.Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Format("__FuncionalidadArticulo_{0}__", Me.UniqueID), sbScript.ToString(), True)
        End If

    End Sub

#End Region

  
End Class
