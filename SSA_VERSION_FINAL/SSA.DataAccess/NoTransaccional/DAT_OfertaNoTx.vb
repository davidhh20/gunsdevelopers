Imports System
Imports System.Collections.Generic
Imports System.Data.Common
Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data


Friend Class DAT_OfertaNoTx

    Public Function ListarPaginado(ByVal ObjBE_Oferta As BE_ListaOferta, ByVal obj_pPaginador As Paginador, ByVal pDatabase As Database) As List(Of BE_ListaOferta)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_OFERTA_LISTAR")

        pDatabase.AddInParameter(objDbCommand, "VI_IDTIPOARTICULO", DbType.Int32, ObjBE_Oferta.id_tipo_articulo)
        pDatabase.AddInParameter(objDbCommand, "VI_IDSUBTIPOARTICULO", DbType.Int32, ObjBE_Oferta.id_subtipo_articulo)
        pDatabase.AddInParameter(objDbCommand, "VI_IDUSUARIO", DbType.Int32, ObjBE_Oferta.id_usuario)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_SUBASTA", DbType.Int32, ObjBE_Oferta.nestado5)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_DETALLE", DbType.Int32, ObjBE_Oferta.nestado3)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_OFERTAELIMINADA", DbType.Int32, ObjBE_Oferta.nestado)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_DETALLE_ELIMINADA", DbType.Int32, ObjBE_Oferta.nestado2)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_SUBASTA_ELIMINADA", DbType.Int32, ObjBE_Oferta.nestado4)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROPAGINA", DbType.Int32, obj_pPaginador.NumeroPagina)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROREGISTROS", DbType.Int32, obj_pPaginador.NumeroRegistros)
        pDatabase.AddOutParameter(objDbCommand, "VO_TOTALREGISTROS", DbType.Int32, obj_pPaginador.TotalResgistros)

        Dim Collection As New List(Of BE_ListaOferta)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Oferta.ListarPaginado(dr))
            End While

        End Using
        obj_pPaginador.TotalResgistros = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VO_TOTALREGISTROS"))

        Return Collection

    End Function

    Public Function Seleccionar(ByVal ObjBE_Oferta As BE_ListaOferta, ByVal pDatabase As Database) As BE_ListaOferta

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_OFERTA_MAXIMOVALOR")

        pDatabase.AddInParameter(objDbCommand, "VI_IDDETSUBASTA", DbType.Int32, ObjBE_Oferta.id_detsubasta)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_OFERTAELIMINADA", DbType.Int32, ObjBE_Oferta.nestado)

        Dim BE As BE_ListaOferta
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                BE = POP_Oferta.Seleccionar(dr)
                Dim objArticulo As New BE_Articulo
                Dim objDatArticuloNoTx As New DAT_ArticuloNoTx
                objArticulo = objDatArticuloNoTx.Seleccionar(ObjBE_Oferta.id_articulo, pDatabase)
                BE.BE_Articulo = objArticulo
            End While

        End Using

        Return BE

    End Function

    ''' <summary>
    ''' Lista las ofertas activas segun un ID det_subasta
    ''' </summary>
    Public Function Listar(ByVal ObjBE_Oferta As BE_Oferta, ByVal pDatabase As Database) As List(Of BE_Oferta)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_OFERTA_LISTAR_SUBASTA")

        pDatabase.AddInParameter(objDbCommand, "VI_IDDETSUBASTA", DbType.Int32, ObjBE_Oferta.id_detsubasta)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_OFERTAELIMINADA", DbType.Int32, ObjBE_Oferta.nestado)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_ADJUDICACIONELIMINADA", DbType.Int32, ObjBE_Oferta.BE_Adjudicacion.nestado)

        Dim Collection As New List(Of BE_Oferta)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Oferta.Listar(dr))
            End While

        End Using

        Return Collection

    End Function


    Public Function ListarSubastas(ByVal ObjBE_Oferta As BE_ListaOferta, ByVal obj_pPaginador As Paginador, ByVal pDatabase As Database) As List(Of BE_ListaOferta)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_OFERTA_LISTAR_PRINCIPAL")

        pDatabase.AddInParameter(objDbCommand, "VI_IDTIPOARTICULO", DbType.Int32, ObjBE_Oferta.id_tipo_articulo)
        pDatabase.AddInParameter(objDbCommand, "VI_IDSUBTIPOARTICULO", DbType.Int32, ObjBE_Oferta.id_subtipo_articulo)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_SUBASTA", DbType.Int32, ObjBE_Oferta.nestado5)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_DETALLE", DbType.Int32, ObjBE_Oferta.nestado3)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_DETALLE_ELIMINADA", DbType.Int32, ObjBE_Oferta.nestado2)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_SUBASTA_ELIMINADA", DbType.Int32, ObjBE_Oferta.nestado4)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROPAGINA", DbType.Int32, obj_pPaginador.NumeroPagina)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROREGISTROS", DbType.Int32, obj_pPaginador.NumeroRegistros)
        pDatabase.AddOutParameter(objDbCommand, "VO_TOTALREGISTROS", DbType.Int32, obj_pPaginador.TotalResgistros)

        Dim Collection As New List(Of BE_ListaOferta)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Oferta.ListarSubasta(dr))
            End While

        End Using
        obj_pPaginador.TotalResgistros = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VO_TOTALREGISTROS"))

        Return Collection

    End Function

    Public Function ListarSubastasOfertadas(ByVal ObjBE_Oferta As BE_ListaOferta, ByVal pDatabase As Database) As List(Of BE_ListaOferta)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_VALIDARSUBASTA")
        pDatabase.AddInParameter(objDbCommand, "VI_IDUSUARIO", DbType.Int32, ObjBE_Oferta.id_usuario)
        pDatabase.AddInParameter(objDbCommand, "VI_IDDETALLESUBASTA", DbType.Int32, ObjBE_Oferta.id_detsubasta)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_SUBASTA", DbType.Int32, ObjBE_Oferta.nestado5)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_DETALLE", DbType.Int32, ObjBE_Oferta.nestado3)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_DETALLE_ELIMINADA", DbType.Int32, ObjBE_Oferta.nestado2)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_SUBASTA_ELIMINADA", DbType.Int32, ObjBE_Oferta.nestado4)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_OFERTAELIMINADA", DbType.Int32, ObjBE_Oferta.nestado)
        Dim Collection As New List(Of BE_ListaOferta)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Oferta.ListarSubastaValidad(dr))
            End While

        End Using

        Return Collection

    End Function

End Class


