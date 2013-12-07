Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.Common
Imports System.Transactions

Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity
Imports QNET.Common

Public Class DAT_OpcionPerfilGr

    Private db As Database = Nothing

    Public Sub New()
        db = Conexion.getInstance(DAT_Conexiones.Co_BD_SSA)
    End Sub

    Public Function Listar(ByVal idPerfil As Integer) As List(Of BE_Opcion)

        Dim objDAT_OpcionPerfilNoTx As New DAT_OpcionPerfilNoTx
        Return objDAT_OpcionPerfilNoTx.Listar(idPerfil, db)

    End Function

End Class