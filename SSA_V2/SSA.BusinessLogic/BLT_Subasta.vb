Imports QNET.Common
Imports SSA.BusinessEntity
Imports SSA.DataAccess


Public Class BLT_Subasta

    Public Function Rentabilidad(ByVal objBE_ListaOferta As BE_ListaOferta) As List(Of BE_Rentabilidad)
        Dim ObjDAT_SubastaGr As New DAT_SubastaGr
        Return ObjDAT_SubastaGr.Rentabilidad(objBE_ListaOferta)
    End Function

    Public Function MiDetalle(ByVal id_subasta As Integer, ByVal nestado As Integer, ByVal nestado2 As Integer) As List(Of BE_Detalle_Subasta)
        Dim ObjDAT_SubastaGr As New DAT_SubastaGr
        Return ObjDAT_SubastaGr.MiDetalle(id_subasta, nestado, nestado2)
    End Function

    Public Function Seleccionar(ByVal id_subasta As Integer, ByVal nestado As Integer) As BE_Subasta
        Dim ObjDAT_SubastaGr As New DAT_SubastaGr
        Return ObjDAT_SubastaGr.Seleccionar(id_subasta, nestado)
    End Function

    Public Function ListarPaginado(ByVal objBE_ListaOferta As BE_ListaOferta, ByVal objPaginador As Paginador) As List(Of BE_Detalle_Subasta)
        Dim ObjDAT_SubastaGr As New DAT_SubastaGr
        Return ObjDAT_SubastaGr.ListarPaginado(objBE_ListaOferta, objPaginador)
    End Function

    Public Function ListarPaginado(ByVal objBE_Subasta As BE_Subasta, ByVal objPaginador As Paginador) As List(Of BE_Subasta)
        Dim ObjDAT_SubastaGr As New DAT_SubastaGr
        Return ObjDAT_SubastaGr.ListarPaginado(objBE_Subasta, objPaginador)
    End Function

    Public Function CambiarEstado(ByVal objBE_Subasta As BE_Subasta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_SubastaGr As New DAT_SubastaGr
        Return ObjDAT_SubastaGr.CambiarEstado(objBE_Subasta, idUsuarioSistema)
    End Function

    Public Function Eliminar(ByVal objBE_Subasta As BE_Subasta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_SubastaGr As New DAT_SubastaGr
        Return ObjDAT_SubastaGr.Eliminar(objBE_Subasta, idUsuarioSistema)
    End Function

    Public Function Insertar(ByVal objBE_Subasta As BE_Subasta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_SubastaGr As New DAT_SubastaGr
        Return ObjDAT_SubastaGr.Insertar(objBE_Subasta, idUsuarioSistema)
    End Function

    'Public Function Modificar(ByVal objBE_Articulo As BE_Articulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
    '    Dim ObjDAT_SubastaGr As New DAT_SubastaGr
    '    Return ObjDAT_ArticuloGr.Modificar(objBE_Articulo, idUsuarioSistema)
    'End Function
    Public Function CambiarEstado_Detalle(ByVal objBE_Detalle_Subasta As BE_Detalle_Subasta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_SubastaGr As New DAT_SubastaGr
        Return ObjDAT_SubastaGr.CambiarEstado_Detalle(objBE_Detalle_Subasta, idUsuarioSistema)
    End Function

End Class