Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Imports QNET.Common

Imports ssa.DataAccess
Imports ssa.BusinessEntity

Public Class BLT_OpcionPerfil

    Public Function Listar(ByVal idPerfil As Integer) As List(Of BE_Opcion)

        Dim objDAT_OpcionPerfilGr As New DAT_OpcionPerfilGr
        Return objDAT_OpcionPerfilGr.Listar(idPerfil)

    End Function

End Class
