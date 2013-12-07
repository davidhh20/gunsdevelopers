Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common

Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class DAT_TipoArticuloGr
    Private objDataBase As Database = Nothing

    Public Sub New()
        objDataBase = Conexion.getInstance(DAT_Conexiones.Co_BD_SSA)
    End Sub

    Public Function Listar(ByVal ObjBE_TipoArticulo As BE_TipoArticulo) As List(Of BE_TipoArticulo)
        Dim ObjDAT_TipoArticuloNoTx As New DAT_TipoArticuloNoTx
        Return ObjDAT_TipoArticuloNoTx.Listar(ObjBE_TipoArticulo, objDataBase)
    End Function

    Public Function ListarPaginado(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal objPaginador As Paginador) As List(Of BE_TipoArticulo)
        Dim ObjDAT_TipoArticuloNoTx As New DAT_TipoArticuloNoTx
        Return ObjDAT_TipoArticuloNoTx.ListarPaginado(objBE_TipoArticulo, objPaginador, objDataBase)
    End Function


    Public Function Insertar(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_TipoArticuloTx As New DAT_TipoArticuloTx
                objDAT_TipoArticuloTx.Insertar(objBE_TipoArticulo, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxInsercion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblTipoArticulo
                objBE_Transaccion.IdReferencia = objBE_TipoArticulo.IdTipoArticulo
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

    Public Function Modificar(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_TipoArticuloTx As New DAT_TipoArticuloTx
                objDAT_TipoArticuloTx.Modificar(objBE_TipoArticulo, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxActualizacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblTipoArticulo
                objBE_Transaccion.IdReferencia = objBE_TipoArticulo.IdTipoArticulo
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

    Public Function Eliminar(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_TipoArticuloTx As New DAT_TipoArticuloTx
                objDAT_TipoArticuloTx.Eliminar(objBE_TipoArticulo, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxEliminacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblTipoArticulo
                objBE_Transaccion.IdReferencia = objBE_TipoArticulo.IdTipoArticulo
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

End Class
