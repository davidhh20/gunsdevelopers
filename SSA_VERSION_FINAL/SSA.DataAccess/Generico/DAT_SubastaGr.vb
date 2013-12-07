Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common

Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class DAT_SubastaGr
    Private objDataBase As Database = Nothing

    Public Sub New()
        objDataBase = Conexion.getInstance(DAT_Conexiones.Co_BD_SSA)
    End Sub

    Public Function Rentabilidad(ByVal objBE_ListaOferta As BE_ListaOferta) As List(Of BE_Rentabilidad)
        Dim objDAT_DetalleSubastaNotx As New DAT_DetalleSubastaNotx
        Return objDAT_DetalleSubastaNotx.Rentabilidad(objBE_ListaOferta, objDataBase)
    End Function

    Public Function MiDetalle(ByVal id_subasta As Integer, ByVal nestado As Integer, ByVal nestado2 As Integer) As List(Of BE_Detalle_Subasta)
        Dim objDAT_DetalleSubastaNotx As New DAT_DetalleSubastaNotx
        Return objDAT_DetalleSubastaNotx.Seleccionar(id_subasta, nestado, nestado2, objDataBase)
    End Function

    Public Function Seleccionar(ByVal id_subasta As Integer, ByVal nestado As Integer) As BE_Subasta
        Dim objDAT_SubastaNotx As New DAT_SubastaNotx
        Return objDAT_SubastaNotx.Seleccionar(id_subasta, nestado, objDataBase)
    End Function

    Public Function ListarPaginado(ByVal objBE_ListaOferta As BE_ListaOferta, ByVal objPaginador As Paginador) As List(Of BE_Detalle_Subasta)
        Dim objDAT_DetalleSubastaNotx As New DAT_DetalleSubastaNotx
        Return objDAT_DetalleSubastaNotx.ListarPaginado(objBE_ListaOferta, objPaginador, objDataBase)
    End Function

    Public Function ListarPaginado(ByVal objBE_Subasta As BE_Subasta, ByVal objPaginador As Paginador) As List(Of BE_Subasta)
        Dim objDAT_SubastaNotx As New DAT_SubastaNotx
        Return objDAT_SubastaNotx.ListarPaginado(objBE_Subasta, objPaginador, objDataBase)
    End Function

    ''' <summary>
    ''' Esta funcion va eliminar la subasta pero va poner en estado REGISTRADO  a los articulos asociados
    ''' </summary>
    Public Function Eliminar(ByVal objBE_Subasta As BE_Subasta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_SubastaTx As New DAT_SubastaTx
                Dim objDAT_ArticuloTx As DAT_ArticuloTx
                Dim objDAT_TransaccionTx As DAT_TransaccionTx
                Dim objBE_Transaccion As BE_Transaccional

                objDAT_SubastaTx.Eliminar(objBE_Subasta, objDataBase, objDbConnection, objDbTransaction)
                objDAT_TransaccionTx = New DAT_TransaccionTx
                objBE_Transaccion = New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxEliminacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblSubasta
                objBE_Transaccion.IdReferencia = objBE_Subasta.id_subasta
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                'Convertimos los articulos asociados a estado registrado
                Dim objBE_Articulo As BE_Articulo
                For Each IdArticulo As String In objBE_Subasta.des_estado.Split(","c)
                    If IdArticulo.Trim.Length = 0 Then Continue For
                    objBE_Articulo = New BE_Articulo
                    objDAT_ArticuloTx = New DAT_ArticuloTx

                    objBE_Articulo.id_articulo = IdArticulo
                    objBE_Articulo.nestado = objBE_Subasta.nestado2
                    objDAT_ArticuloTx.CambiarEstado(objBE_Articulo, objDataBase, objDbConnection, objDbTransaction)
                    objDAT_TransaccionTx = New DAT_TransaccionTx
                    objBE_Transaccion = New BE_Transaccional
                    objBE_Transaccion.IdUsuario = idUsuarioSistema
                    objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxActualizacion
                    objBE_Transaccion.CTabla = DAT_Constantes.Co_TblArticulo
                    objBE_Transaccion.IdReferencia = IdArticulo
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

    '1.- Insertamos la cabecera de la subasta
    '2.- Insertamos cada detalle
    '3.- Actualizamos el estado del artículo a EN SUBASTA
    Public Function Insertar(ByVal objBE_Subasta As BE_Subasta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_SubastaTx As New DAT_SubastaTx
                Dim objDAT_TransaccionTx As DAT_TransaccionTx
                Dim objBE_Transaccion As BE_Transaccional
                Dim objDAT_DetalleSubastaTx As DAT_DetalleSubastaTx
                Dim objDAT_ArticuloTx As DAT_ArticuloTx

                objDAT_SubastaTx.Insertar(objBE_Subasta, objDataBase, objDbConnection, objDbTransaction)

                objDAT_TransaccionTx = New DAT_TransaccionTx
                objBE_Transaccion = New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxInsercion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblSubasta
                objBE_Transaccion.IdReferencia = objBE_Subasta.id_subasta
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                For Each item As BE_Detalle_Subasta In objBE_Subasta.MiDetalle
                    objDAT_DetalleSubastaTx = New DAT_DetalleSubastaTx
                    item.id_subasta = objBE_Subasta.id_subasta
                    objDAT_DetalleSubastaTx.Insertar(item, objDataBase, objDbConnection, objDbTransaction)
                    objDAT_TransaccionTx = New DAT_TransaccionTx
                    objBE_Transaccion = New BE_Transaccional
                    objBE_Transaccion.IdUsuario = idUsuarioSistema
                    objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxInsercion
                    objBE_Transaccion.CTabla = DAT_Constantes.Co_TblDetalleSubasta
                    objBE_Transaccion.IdReferencia = item.id_detsubasta
                    objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                    objDAT_ArticuloTx = New DAT_ArticuloTx
                    objDAT_ArticuloTx.CambiarEstado(item.BE_Articulo, objDataBase, objDbConnection, objDbTransaction)
                    objDAT_TransaccionTx = New DAT_TransaccionTx
                    objBE_Transaccion = New BE_Transaccional
                    objBE_Transaccion.IdUsuario = idUsuarioSistema
                    objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxActualizacion
                    objBE_Transaccion.CTabla = DAT_Constantes.Co_TblArticulo
                    objBE_Transaccion.IdReferencia = item.BE_Articulo.id_articulo
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

    'Actualizamos el detalle de la subasta
    Public Function CambiarEstado(ByVal objBE_Subasta As BE_Subasta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_SubastaTx As New DAT_SubastaTx
                Dim objDAT_DetalleSubastaTx As New DAT_DetalleSubastaTx
                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional

                objDAT_SubastaTx.CambiarEstado(objBE_Subasta, objDataBase, objDbConnection, objDbTransaction)
                objBE_Transaccion = New BE_Transaccional
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxActualizacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblSubasta
                objBE_Transaccion.IdReferencia = objBE_Subasta.id_subasta
                objDAT_TransaccionTx.Transaccion_Insertar(objBE_Transaccion, objDataBase, objDbConnection, objDbTransaction)

                For Each item As BE_Detalle_Subasta In objBE_Subasta.MiDetalle
                    objDAT_DetalleSubastaTx.CambiarEstado(item, objDataBase, objDbConnection, objDbTransaction)
                    objBE_Transaccion = New BE_Transaccional
                    objBE_Transaccion.IdUsuario = idUsuarioSistema
                    objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxActualizacion
                    objBE_Transaccion.CTabla = DAT_Constantes.Co_TblDetalleSubasta
                    objBE_Transaccion.IdReferencia = item.id_detsubasta
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

    'Actualizamos solo el detalle de la subasta
    Public Function CambiarEstado_Detalle(ByVal objBE_Detalle_Subasta As BE_Detalle_Subasta, ByVal idUsuarioSistema As Integer) As ResultadoTransaccion

        Using objDbConnection As DbConnection = objDataBase.CreateConnection

            Dim objDbTransaction As DbTransaction = Nothing

            Try
                objDbConnection.Open()
                objDbTransaction = objDbConnection.BeginTransaction()

                Dim objDAT_DetalleSubastaTx As New DAT_DetalleSubastaTx
                Dim objDAT_TransaccionTx As New DAT_TransaccionTx
                Dim objBE_Transaccion As New BE_Transaccional

                objDAT_DetalleSubastaTx.CambiarEstado(objBE_Detalle_Subasta, objDataBase, objDbConnection, objDbTransaction)            
                objBE_Transaccion.IdUsuario = idUsuarioSistema
                objBE_Transaccion.CMovimiento = DAT_Constantes.Co_TxActualizacion
                objBE_Transaccion.CTabla = DAT_Constantes.Co_TblDetalleSubasta
                objBE_Transaccion.IdReferencia = objBE_Detalle_Subasta.id_detsubasta
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