Imports System
Imports QNET.Common

Public Class DAT_Constantes

#Region "TipoTransaccion"
    Public Shared Co_TxInsercion As Integer = Convert.ToInt32(Util.getParametroWebConfig("Insercion"))
    Public Shared Co_TxActualizacion As Integer = Convert.ToInt32(Util.getParametroWebConfig("Actualizacion"))
    Public Shared Co_TxEliminacion As Integer = Convert.ToInt32(Util.getParametroWebConfig("Eliminacion"))
#End Region

#Region " Tablas "
    Public Shared Co_TblUsuario As Integer = Convert.ToInt32(Util.getParametroWebConfig("TblUsuario"))
    Public Shared Co_TblParametro As Integer = Convert.ToInt32(Util.getParametroWebConfig("TblParametro"))
    Public Shared Co_TblArticulo As Integer = Convert.ToInt32(Util.getParametroWebConfig("TblArticulo"))
    Public Shared Co_TblAdjudicacion As Integer = Convert.ToInt32(Util.getParametroWebConfig("TblAdjudicacion"))
    Public Shared Co_TblOferta As Integer = Convert.ToInt32(Util.getParametroWebConfig("TblOferta"))
    Public Shared Co_TblDetalleSubasta As Integer = Convert.ToInt32(Util.getParametroWebConfig("TblDetalleSubasta"))
    Public Shared Co_TblSubasta As Integer = Convert.ToInt32(Util.getParametroWebConfig("TblSubasta"))
    Public Shared Co_TblTipoArticulo As Integer = Convert.ToInt32(Util.getParametroWebConfig("TblTipoArticulo"))
    Public Shared Co_TblSubTipoArticulo As Integer = Convert.ToInt32(Util.getParametroWebConfig("TblSubTipoArticulo"))
    Public Shared Co_TblTransacciones As Integer = Convert.ToInt32(Util.getParametroWebConfig("TblTransacciones"))
#End Region
End Class