Imports QNET.Common
Imports SSA.BusinessEntity
Imports SSA.DataAccess


Public Class BLT_Parametro

    Public Function Listar(ByVal ObjBE_Parametro As BE_Parametro) As List(Of BE_Parametro)
        Dim ObjDAT_ParametroGr As New DAT_ParametroGr
        Return ObjDAT_ParametroGr.Listar(ObjBE_Parametro)
    End Function

    Public Function ListarGrupos() As List(Of BE_Parametro)
        Dim ObjDAT_ParametroGr As New DAT_ParametroGr
        Return ObjDAT_ParametroGr.ListarGrupos()
    End Function

    Public Function ListarPaginado(ByVal objBE_Parametro As BE_Parametro, ByVal objPaginador As Paginador) As List(Of BE_Parametro)
        Dim ObjDAT_ParametroGr As New DAT_ParametroGr
        Return ObjDAT_ParametroGr.ListarPaginado(objBE_Parametro, objPaginador)
    End Function

    Public Function Eliminar(ByVal objBE_Parametro As BE_Parametro, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_ParametroGr As New DAT_ParametroGr
        Return ObjDAT_ParametroGr.Eliminar(objBE_Parametro, idUsuarioSistema)
    End Function

    Public Function Modificar(ByVal objBE_Parametro As BE_Parametro, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_ParametroGr As New DAT_ParametroGr
        Return ObjDAT_ParametroGr.Modificar(objBE_Parametro, idUsuarioSistema)
    End Function

    Public Function Insertar(ByVal objBE_Parametro As BE_Parametro, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_ParametroGr As New DAT_ParametroGr
        Return ObjDAT_ParametroGr.Insertar(objBE_Parametro, idUsuarioSistema)
    End Function

End Class