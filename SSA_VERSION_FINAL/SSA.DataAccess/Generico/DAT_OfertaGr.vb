Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common

Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class DAT_OfertaGr
    Private objDataBase As Database = Nothing

    Public Sub New()
        objDataBase = Conexion.getInstance(DAT_Conexiones.Co_BD_SSA)
    End Sub

    Public Function Listar(ByVal objBE_Oferta As BE_Oferta) As List(Of BE_Oferta)
        Dim objDAT_OfertaNoTx As New DAT_OfertaNoTx
        Return objDAT_OfertaNoTx.Listar(objBE_Oferta, objDataBase)
    End Function

    Public Function ListarPaginado(ByVal objBE_ListaOferta As BE_ListaOferta, ByVal objPaginador As Paginador) As List(Of BE_ListaOferta)
        Dim objDAT_OfertaNoTx As New DAT_OfertaNoTx
        Return objDAT_OfertaNoTx.ListarPaginado(objBE_ListaOferta, objPaginador, objDataBase)
    End Function

    Public Function ListarSubastaValida(ByVal objBE_ListaOferta As BE_ListaOferta) As List(Of BE_ListaOferta)
        Dim objDAT_OfertaNoTx As New DAT_OfertaNoTx
        Return objDAT_OfertaNoTx.ListarSubastasOfertadas(objBE_ListaOferta, objDataBase)
    End Function

    Public Function Seleccionar(ByVal objBE_ListaOferta As BE_ListaOferta) As BE_ListaOferta
        Dim objDAT_OfertaNoTx As New DAT_OfertaNoTx
        Return objDAT_OfertaNoTx.Seleccionar(objBE_ListaOferta, objDataBase)
    End Function

    Public Function Insertar(ByVal objBE_Oferta As BE_Oferta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_OfertaTx As New DAT_OfertaTx
                objDAT_OfertaTx.Insertar(objBE_Oferta, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxInsercion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblOferta
                objBE_Transaccion.IdReferencia = objBE_Oferta.id_oferta
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

    Public Function Eliminar(ByVal objBE_Oferta As BE_Oferta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_OfertaTx As New DAT_OfertaTx
                objDAT_OfertaTx.Eliminar(objBE_Oferta, objDataBase, objDbConnection, objDbTransaction)

                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxEliminacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblOferta
                objBE_Transaccion.IdReferencia = objBE_Oferta.id_oferta
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                objDbTransaction.Commit()

                Return New ResultadoTransaccion(TipoResultado.Exito, Nothing, 0)

            Catch ex As Exception

                If objDbTransaction IsNot Nothing Then objDbTransaction.Rollback()

                Return New ResultadoTransaccion(TipoResultado.ErrorBD, ex, 0)

            End Try
        End Using

    End Function

    Public Function ListarSubasta(ByVal objBE_ListaOferta As BE_ListaOferta, ByVal objPaginador As Paginador) As List(Of BE_ListaOferta)
        Dim objDAT_OfertaNoTx As New DAT_OfertaNoTx
        Return objDAT_OfertaNoTx.ListarSubastas(objBE_ListaOferta, objPaginador, objDataBase)
    End Function

End Class