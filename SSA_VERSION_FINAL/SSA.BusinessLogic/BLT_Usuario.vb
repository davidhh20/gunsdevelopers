Imports QNET.Common
Imports SSA.BusinessEntity
Imports SSA.DataAccess


Public Class BLT_Usuario

    Public Function ListarPaginado(ByVal ObjBE_Usuario As BE_Usuario, ByVal objPaginador As Paginador) As List(Of BE_Usuario)
        Dim ObjDA_UsuarioGr As New DAT_UsuarioGr
        Return ObjDA_UsuarioGr.ListarPaginado(ObjBE_Usuario, objPaginador)
    End Function

    Public Function Seguridad_ObtenerUsuario(ByVal ObjBE_Usuario As BE_Usuario) As BE_Usuario
        Dim ObjDA_UsuarioGr As New DAT_UsuarioGr
        Return ObjDA_UsuarioGr.Seguridad_ObtenerUsuario(ObjBE_Usuario)
    End Function

    Public Function Insertar(ByVal ObjBE_Usuario As BE_Usuario) As ResultadoTransaccion
        Dim ObjDA_UsuarioGr As New DAT_UsuarioGr
        Return ObjDA_UsuarioGr.Insertar(ObjBE_Usuario)
    End Function

    Public Function Marcar(ByVal objBE_Usuario As BE_Usuario, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDA_UsuarioGr As New DAT_UsuarioGr
        Return ObjDA_UsuarioGr.Marcar(objBE_Usuario, idUsuarioSistema)
    End Function

    Public Function Modificar(ByVal objBE_Usuario As BE_Usuario, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDA_UsuarioGr As New DAT_UsuarioGr
        Return ObjDA_UsuarioGr.Modificar(objBE_Usuario, idUsuarioSistema)
    End Function

End Class