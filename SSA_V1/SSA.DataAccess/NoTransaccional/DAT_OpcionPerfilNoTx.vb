Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data.Common
Imports System.Data

Imports SSA.BusinessEntity

Imports Microsoft.Practices.EnterpriseLibrary.Data

Imports QNET.Common

Friend Class DAT_OpcionPerfilNoTx

    Public Function Listar(ByVal idPerfil As Integer, ByVal pDatabase As Database) As List(Of BE_Opcion)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_SEGURIDAD_OPCIONES_LISTAR ")
        pDatabase.AddInParameter(objDbCommand, "VI_PERFIL", DbType.String, idPerfil)

        Dim BE_OpcionCollection As New List(Of BE_Opcion)

        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                BE_OpcionCollection.Add(POP_Opcion.Listar(dr))
            End While
        End Using

        Return BE_OpcionCollection

    End Function

End Class