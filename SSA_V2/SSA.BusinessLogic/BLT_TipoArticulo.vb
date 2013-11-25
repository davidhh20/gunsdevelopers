Imports QNET.Common
Imports SSA.BusinessEntity
Imports SSA.DataAccess


Public Class BLT_TipoArticulo

    Public Function Listar(ByVal ObjBE_TipoArticulo As BE_TipoArticulo) As List(Of BE_TipoArticulo)
        Dim ObjDAT_TipoArticuloGr As New DAT_TipoArticuloGr
        Return ObjDAT_TipoArticuloGr.Listar(ObjBE_TipoArticulo)
    End Function

    Public Function ListarPaginado(ByVal ObjBE_TipoArticulo As BE_TipoArticulo, ByVal objPaginador As Paginador) As List(Of BE_TipoArticulo)
        Dim ObjDAT_TipoArticuloGr As New DAT_TipoArticuloGr
        Return ObjDAT_TipoArticuloGr.ListarPaginado(ObjBE_TipoArticulo, objPaginador)
    End Function

    Public Function Insertar(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_TipoArticuloGr As New DAT_TipoArticuloGr
        Return ObjDAT_TipoArticuloGr.Insertar(objBE_TipoArticulo, idUsuarioSistema)
    End Function

    Public Function Eliminar(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_TipoArticuloGr As New DAT_TipoArticuloGr
        Return ObjDAT_TipoArticuloGr.Eliminar(objBE_TipoArticulo, idUsuarioSistema)
    End Function

    Public Function Modificar(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_TipoArticuloGr As New DAT_TipoArticuloGr
        Return ObjDAT_TipoArticuloGr.Modificar(objBE_TipoArticulo, idUsuarioSistema)
    End Function

End Class