Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common

Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class DAT_ParametroGr
    Private objDataBase As Database = Nothing

    Public Sub New()
        objDataBase = Conexion.getInstance(DAT_Conexiones.Co_BD_SSA)
    End Sub

    Public Function Listar(ByVal objBE_Parametro As BE_Parametro) As List(Of BE_Parametro)
        Dim ObjDAT_ParametroNoTx As New DAT_ParametroNoTx
        Return ObjDAT_ParametroNoTx.Listar(objBE_Parametro, objDataBase)
    End Function

    Public Function ListarGrupos() As List(Of BE_Parametro)
        Dim ObjDAT_ParametroNoTx As New DAT_ParametroNoTx
        Return ObjDAT_ParametroNoTx.ListarGrupos(objDataBase)
    End Function

    Public Function ListarPaginado(ByVal objBE_Parametro As BE_Parametro, ByVal objPaginador As Paginador) As List(Of BE_Parametro)
        Dim ObjDAT_ParametroNoTx As New DAT_ParametroNoTx
        Return ObjDAT_ParametroNoTx.ListarPaginado(objBE_Parametro, objPaginador, objDataBase)
    End Function

    Public Function Insertar(ByVal objBE_Parametro As BE_Parametro, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_ParametroTx As New DAT_ParametroTx
                objDAT_ParametroTx.Insertar(objBE_Parametro, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxInsercion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblParametro
                objBE_Transaccion.IdReferencia = objBE_Parametro.IdParametro
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

    Public Function Modificar(ByVal objBE_Parametro As BE_Parametro, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_ParametroTx As New DAT_ParametroTx
                objDAT_ParametroTx.Modificar(objBE_Parametro, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxActualizacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblParametro
                objBE_Transaccion.IdReferencia = objBE_Parametro.IdParametro
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

    Public Function Eliminar(ByVal objBE_Parametro As BE_Parametro, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_ParametroTx As New DAT_ParametroTx
                objDAT_ParametroTx.Eliminar(objBE_Parametro, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxEliminacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblParametro
                objBE_Transaccion.IdReferencia = objBE_Parametro.IdParametro
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
