Imports QNET.Common
Imports SSA.BusinessEntity
Imports SSA.DataAccess


Public Class BLT_Articulo

    Public Function Seleccionar(ByVal id_articulo As Integer) As BE_Articulo
        Dim ObjDAT_ArticuloGr As New DAT_ArticuloGr
        Return ObjDAT_ArticuloGr.Seleccionar(id_articulo)
    End Function

    Public Function ListarPaginado(ByVal objBE_Articulo As BE_Articulo, ByVal Codigos_Excluidos As String, ByVal objPaginador As Paginador) As List(Of BE_Articulo)
        Dim ObjDAT_ArticuloGr As New DAT_ArticuloGr
        Return ObjDAT_ArticuloGr.ListarPaginado(objBE_Articulo, Codigos_Excluidos, objPaginador)
    End Function

    Public Function ListarPaginado(ByVal objBE_Articulo As BE_Articulo, ByVal objPaginador As Paginador) As List(Of BE_Articulo)
        Dim ObjDAT_ArticuloGr As New DAT_ArticuloGr
        Return ObjDAT_ArticuloGr.ListarPaginado(objBE_Articulo, objPaginador)
    End Function

    Public Function Eliminar(ByVal objBE_Articulo As BE_Articulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_ArticuloGr As New DAT_ArticuloGr
        Return ObjDAT_ArticuloGr.Eliminar(objBE_Articulo, idUsuarioSistema)
    End Function

    Public Function Insertar(ByVal objBE_Articulo As BE_Articulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_ArticuloGr As New DAT_ArticuloGr
        Return ObjDAT_ArticuloGr.Insertar(objBE_Articulo, idUsuarioSistema)
    End Function

    Public Function Modificar(ByVal objBE_Articulo As BE_Articulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_ArticuloGr As New DAT_ArticuloGr
        Return ObjDAT_ArticuloGr.Modificar(objBE_Articulo, idUsuarioSistema)
    End Function

End Class