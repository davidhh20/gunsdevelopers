Imports QNET.Common
Imports SSA.BusinessEntity
Imports SSA.DataAccess


Public Class BLT_SubTipoArticulo

    Public Function Listar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo) As List(Of BE_SubTipoArticulo)
        Dim ObjDAT_SubTipoArticuloGr As New DAT_SubTipoArticuloGr
        Return ObjDAT_SubTipoArticuloGr.Listar(objBE_SubTipoArticulo)
    End Function

    Public Function ListarPaginado(ByVal ObjBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal objPaginador As Paginador) As List(Of BE_SubTipoArticulo)
        Dim ObjDAT_SubTipoArticuloGr As New DAT_SubTipoArticuloGr
        Return ObjDAT_SubTipoArticuloGr.ListarPaginado(ObjBE_SubTipoArticulo, objPaginador)
    End Function

    Public Function Insertar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_SubTipoArticuloGr As New DAT_SubTipoArticuloGr
        Return ObjDAT_SubTipoArticuloGr.Insertar(objBE_SubTipoArticulo, idUsuarioSistema)
    End Function

    Public Function Eliminar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_SubTipoArticuloGr As New DAT_SubTipoArticuloGr
        Return ObjDAT_SubTipoArticuloGr.Eliminar(objBE_SubTipoArticulo, idUsuarioSistema)
    End Function

    Public Function Modificar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_SubTipoArticuloGr As New DAT_SubTipoArticuloGr
        Return ObjDAT_SubTipoArticuloGr.Modificar(objBE_SubTipoArticulo, idUsuarioSistema)
    End Function

End Class