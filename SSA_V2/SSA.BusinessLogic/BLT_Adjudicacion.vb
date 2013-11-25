Imports QNET.Common
Imports SSA.BusinessEntity
Imports SSA.DataAccess


Public Class BLT_Adjudicacion

    Public Function Insertar(ByVal miLista As List(Of BE_Adjudicacion), ByVal idUsuarioSistema As Integer) As ResultadoTransaccion
        Dim ObjDAT_AdjudicacionGr As New DAT_AdjudicacionGr
        Return ObjDAT_AdjudicacionGr.Insertar(miLista, idUsuarioSistema)
    End Function

End Class