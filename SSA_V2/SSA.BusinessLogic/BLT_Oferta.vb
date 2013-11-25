Imports QNET.Common
Imports SSA.BusinessEntity
Imports SSA.DataAccess


Public Class BLT_Oferta

    Public Function Listar(ByVal objBE_Oferta As BE_Oferta) As List(Of BE_Oferta)
        Dim ObjDAT_OfertaGr As New DAT_OfertaGr
        Return ObjDAT_OfertaGr.Listar(objBE_Oferta)
    End Function

    Public Function ListarPaginado(ByVal objBE_ListaOferta As BE_ListaOferta, ByVal objPaginador As Paginador) As List(Of BE_ListaOferta)
        Dim ObjDAT_OfertaGr As New DAT_OfertaGr
        Return ObjDAT_OfertaGr.ListarPaginado(objBE_ListaOferta, objPaginador)
    End Function

    Public Function ListarSubasta(ByVal objBE_ListaOferta As BE_ListaOferta, ByVal objPaginador As Paginador) As List(Of BE_ListaOferta)
        Dim ObjDAT_OfertaGr As New DAT_OfertaGr
        Return ObjDAT_OfertaGr.ListarSubasta(objBE_ListaOferta, objPaginador)
    End Function


    Public Function ListarSubastaValida(ByVal objBE_ListaOferta As BE_ListaOferta) As List(Of BE_ListaOferta)
        Dim ObjDAT_OfertaGr As New DAT_OfertaGr
        Return ObjDAT_OfertaGr.ListarSubastaValida(objBE_ListaOferta)
    End Function

    Public Function Seleccionar(ByVal objBE_ListaOferta As BE_ListaOferta) As BE_ListaOferta
        Dim ObjDAT_OfertaGr As New DAT_OfertaGr
        Return ObjDAT_OfertaGr.Seleccionar(objBE_ListaOferta)
    End Function

    Public Function Insertar(ByVal objBE_Oferta As BE_Oferta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_OfertaGr As New DAT_OfertaGr
        Return ObjDAT_OfertaGr.Insertar(objBE_Oferta, idUsuarioSistema)
    End Function

    Public Function Eliminar(ByVal objBE_Oferta As BE_Oferta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_OfertaGr As New DAT_OfertaGr
        Return ObjDAT_OfertaGr.Eliminar(objBE_Oferta, idUsuarioSistema)
    End Function

End Class