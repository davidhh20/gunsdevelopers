Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common

Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class DAT_AdjudicacionGr
    Private objDataBase As Database = Nothing

    Public Sub New()
        objDataBase = Conexion.getInstance(DAT_Conexiones.Co_BD_SSA)
    End Sub

    Public Function Insertar(ByVal ListaAdjudicacion As List(Of BE_Adjudicacion), ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()
                Dim objDAT_AdjudicacionTx As New DAT_AdjudicacionTx
                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional

                For Each item As BE_Adjudicacion In ListaAdjudicacion
                    objBE_Transaccion = New BE_Transaccional
                    If item.id_adjudicacion = 0 Then
                        objDAT_AdjudicacionTx.Insertar(item, objDataBase, objDbConnection, objDbTransaction)
                        objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxInsercion
                    Else
                        objDAT_AdjudicacionTx.Modificar(item, objDataBase, objDbConnection, objDbTransaction)
                        objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxActualizacion
                    End If
                    objBE_Transaccion.IdUsuario = idUsuarioSistema
                    objBE_Transaccion.CTabla = DAT_Constantes.Co_TblAdjudicacion
                    objBE_Transaccion.IdReferencia = item.id_adjudicacion
                    objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)
                Next

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

End Class