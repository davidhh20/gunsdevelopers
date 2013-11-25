Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common

Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class DAT_ArticuloGr
    Private objDataBase As Database = Nothing

    Public Sub New()
        objDataBase = Conexion.getInstance(DAT_Conexiones.Co_BD_SSA)
    End Sub

    Public Function Seleccionar(ByVal id_articulo As Integer) As BE_Articulo
        Dim objDAT_ArticuloNoTx As New DAT_ArticuloNoTx
        Return objDAT_ArticuloNoTx.Seleccionar(id_articulo, objDataBase)
    End Function

    Public Function ListarPaginado(ByVal objBE_Articulo As BE_Articulo, ByVal Codigos_Excluidos As String, ByVal objPaginador As Paginador) As List(Of BE_Articulo)
        Dim objDAT_ArticuloNoTx As New DAT_ArticuloNoTx
        Return objDAT_ArticuloNoTx.ListarPaginado(objBE_Articulo, Codigos_Excluidos, objPaginador, objDataBase)
    End Function

    Public Function ListarPaginado(ByVal objBE_Articulo As BE_Articulo, ByVal objPaginador As Paginador) As List(Of BE_Articulo)
        Dim objDAT_ArticuloNoTx As New DAT_ArticuloNoTx
        Return objDAT_ArticuloNoTx.ListarPaginado(objBE_Articulo, objPaginador, objDataBase)
    End Function

    Public Function Eliminar(ByVal objBE_Articulo As BE_Articulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_ArticuloTx As New DAT_ArticuloTx
                objDAT_ArticuloTx.CambiarEstado(objBE_Articulo, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxEliminacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblArticulo
                objBE_Transaccion.IdReferencia = objBE_Articulo.id_articulo
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

    Public Function Insertar(ByVal objBE_Articulo As BE_Articulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_ArticuloTx As New DAT_ArticuloTx
                objDAT_ArticuloTx.Insertar(objBE_Articulo, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxInsercion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblArticulo
                objBE_Transaccion.IdReferencia = objBE_Articulo.id_articulo
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

    Public Function Modificar(ByVal objBE_Articulo As BE_Articulo, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_ArticuloTx As New DAT_ArticuloTx
                objDAT_ArticuloTx.Modificar(objBE_Articulo, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxActualizacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblArticulo
                objBE_Transaccion.IdReferencia = objBE_Articulo.id_articulo
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