Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common

Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class DAT_SubTipoArticuloGr
    Private objDataBase As Database = Nothing

    Public Sub New()
        objDataBase = Conexion.getInstance(DAT_Conexiones.Co_BD_SSA)
    End Sub

    Public Function Listar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo) As List(Of BE_SubTipoArticulo)
        Dim ObjDAT_SubTipoArticuloNoTx As New DAT_SubTipoArticuloNoTx
        Return ObjDAT_SubTipoArticuloNoTx.Listar(objBE_SubTipoArticulo, objDataBase)
    End Function

    Public Function ListarPaginado(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal objPaginador As Paginador) As List(Of BE_SubTipoArticulo)
        Dim ObjDAT_SubTipoArticuloNoTx As New DAT_SubTipoArticuloNoTx
        Return ObjDAT_SubTipoArticuloNoTx.ListarPaginado(objBE_SubTipoArticulo, objPaginador, objDataBase)
    End Function


    Public Function Insertar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_SubTipoArticuloTx As New DAT_SubTipoArticuloTx
                objDAT_SubTipoArticuloTx.Insertar(objBE_SubTipoArticulo, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxInsercion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblSubTipoArticulo
                objBE_Transaccion.IdReferencia = objBE_SubTipoArticulo.IdSubTipoArticulo
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

    Public Function Modificar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_SubTipoArticuloTx As New DAT_SubTipoArticuloTx
                objDAT_SubTipoArticuloTx.Modificar(objBE_SubTipoArticulo, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxActualizacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblSubTipoArticulo
                objBE_Transaccion.IdReferencia = objBE_SubTipoArticulo.IdSubTipoArticulo
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

    Public Function Eliminar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_SubTipoArticuloTx As New DAT_SubTipoArticuloTx
                objDAT_SubTipoArticuloTx.Eliminar(objBE_SubTipoArticulo, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxEliminacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblTipoArticulo
                objBE_Transaccion.IdReferencia = objBE_SubTipoArticulo.IdSubTipoArticulo
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
