Imports System.Configuration
Imports QNET.Common

Public Class MyConfig

#Region " Servidor "
    Public Shared getRutaWebServidor As String = Util.getParametroWebConfig("RutaWebServidor")
    Public Shared getRutaFisicaServidor As String = Util.getParametroWebConfig("RutaFisicaServidor")
    Public Shared getRutaPlantillasMail As String = Util.getParametroWebConfig("RutaPlantillasMail")
    Public Shared getRutaWebImagenes As String = String.Format("{0}{1}", getRutaWebServidor, Util.getParametroWebConfig("RutaImagenes"))
    Public Shared getRutaFisicaImagenes As String = String.Format("{0}{1}", getRutaFisicaServidor, Util.getParametroWebConfig("RutaImagenes"))
#End Region
#Region " tipos de Operación por GET "
    Public Shared getParamGetAdd As String = Util.getParametroWebConfig("ParamGetAdd")
    Public Shared getParamGetUpdate As String = Util.getParametroWebConfig("ParamGetUpdate")
#End Region
#Region " Tipo Persona "
    Public Shared getTipoPersonaJuridica As Integer = Convert.ToInt32(Util.getParametroWebConfig("TipoPersonaJuridica"))
    Public Shared getTipoPersonaNatural As Integer = Convert.ToInt32(Util.getParametroWebConfig("TipoPersonaNatural"))
#End Region
#Region " Tipo & SubTipo de Artículo & Parametro "
    Public Shared getTipoArticuloActivo As Integer = Convert.ToInt32(Util.getParametroWebConfig("TipoArticuloActivo"))
    Public Shared getTipoArticuloEliminado As Integer = Convert.ToInt32(Util.getParametroWebConfig("TipoArticuloEliminado"))
    Public Shared getSubTipoArticuloActivo As Integer = Convert.ToInt32(Util.getParametroWebConfig("SubTipoArticuloActivo"))
    Public Shared getSubTipoArticuloEliminado As Integer = Convert.ToInt32(Util.getParametroWebConfig("SubTipoArticuloEliminado"))
    Public Shared getParametroEstadoActivo As Integer = Convert.ToInt32(Util.getParametroWebConfig("ParametroActivo"))
    Public Shared getParametroEstadoEliminado As Integer = Convert.ToInt32(Util.getParametroWebConfig("ParametroEliminado"))
#End Region
#Region " Perfil "
    Public Shared getPerfilComprador As Byte = Convert.ToByte(Util.getParametroWebConfig("PerfilComprador"))
#End Region
#Region " Grupos y Subgrupos de Parámetros "
    Public Shared getGrupoEstadoArticulo As Byte = Convert.ToByte(Util.getParametroWebConfig("GrupoEstadoArticulo"))
    'Public Shared getSubGrupoEstadoArticulo As Byte = Convert.ToByte(Util.getParametroWebConfig("SubGrupoEstadoArticulo"))
    Public Shared getGrupoEstadoSubasta As Byte = Convert.ToByte(Util.getParametroWebConfig("GrupoEstadoSubasta"))
    'Public Shared getSubGrupoEstadoSubasta As Byte = Convert.ToByte(Util.getParametroWebConfig("SubGrupoEstadoSubasta"))
    Public Shared getGrupoEstadoDetalleSubasta As Byte = Convert.ToByte(Util.getParametroWebConfig("GrupoEstadoDetalleSubasta"))
    'Public Shared getSubGrupoEstadoDetalleSubasta As Byte = Convert.ToByte(Util.getParametroWebConfig("SubGrupoEstadoDetalleSubasta"))
#End Region
#Region " Usuario&Comprador "
    Public Shared getEstadoUsuarioActivo As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoUsuarioActivo"))
    Public Shared getEstadoUsuarioEliminado As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoUsuarioEliminado"))
    Public Shared getMarcaCompradorDudoso As Integer = Convert.ToInt32(Util.getParametroWebConfig("MarcaDudosa"))
    Public Shared getMarcaCompradorNormal As Integer = Convert.ToInt32(Util.getParametroWebConfig("MarcaNormal"))
#End Region
#Region " Expresiones Regulares "
    Public Shared getExpresionNombres As String = Util.getParametroWebConfig("revNombres")
    Public Shared getExpresionDecimales As String = Util.getParametroWebConfig("revDecimales")
#End Region
#Region " Parámetros, Plantilla & Asunto de Correo "
    Public Shared getCredencialCorreo As String = Convert.ToString(Util.getParametroWebConfig("CredencialCorreo"))
    Public Shared getCredencialNombre As String = Convert.ToString(Util.getParametroWebConfig("CredencialNombre"))
    Public Shared getAsuntoOlvidoClave As String = Convert.ToString(Util.getParametroWebConfig("AsuntoOlvidoClave"))
    Public Shared getPlantillaOlvidoClave As String = String.Format("{0}{1}{3}", getRutaFisicaServidor, getRutaPlantillasMail, Util.getParametroWebConfig("RutaPlantillasMail"), Util.getParametroWebConfig("PlantillaOlvidoClave"))
    Public Shared getPlantillaAdjudicacion As String = String.Format("{0}{1}{3}", getRutaFisicaServidor, getRutaPlantillasMail, Util.getParametroWebConfig("RutaPlantillasMail"), Util.getParametroWebConfig("PlantillaAdjudicacion"))
#End Region
#Region " Artículo "
    Public Shared getEstadoArticuloRegistrado As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoArticuloRegistrado"))
    Public Shared getEstadoArticuloEliminado As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoArticuloEliminado"))
    Public Shared getEstadoArticuloEnSubasta As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoArticuloEnSubasta"))
#End Region
#Region " Subasta "
    Public Shared getEstadoSubastaActiva As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoSubastaActivo"))
    Public Shared getEstadoSubastaEliminada As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoSubastaEliminado"))
    Public Shared getEstadoSubastaPublicada As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoSubastaPublicado"))
    Public Shared getEstadoSubastaEnSubasta As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoSubastaEnSubasta"))
    Public Shared getEstadoSubastaCerrado As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoSubastaCerrado"))
#End Region
#Region " Detalle Subasta "
    Public Shared getEstadoDetalleSubastaActiva As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoDetalleSubastaActivo"))
    Public Shared getEstadoDetalleSubastaEliminado As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoDetalleSubastaEliminado"))
    Public Shared getEstadoDetalleSubastaRegistrado As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoDetalleSubastaRegistrado"))
    Public Shared getEstadoDetalleSubastaEnSubasta As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoDetalleSubastaEnSubasta"))
    Public Shared getEstadoDetalleSubastaCerrado As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoDetalleSubastaCerrado"))
    Public Shared getEstadoDetalleSubastaVendido As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoDetalleSubastaVendido"))
#End Region
#Region " Oferta "
    Public Shared getEstadoOfertaActiva As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoOfertaActivo"))
    Public Shared getEstadoOfertaEliminada As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoOfertaEliminado"))
#End Region
#Region " Adjudicacion "
    Public Shared getEstadoAdjudicacionActivo As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoAdjudicacionActivo"))
    Public Shared getEstadoAdjudicacionEliminado As Integer = Convert.ToInt32(Util.getParametroWebConfig("EstadoAdjudicacionEliminado"))
#End Region
#Region " Valores por Defecto "
    Public Shared getRutaSinFoto As String = String.Format("{0}{1}", getRutaWebImagenes, Util.getParametroWebConfig("RutaImagenSinFoto"))
#End Region

End Class
