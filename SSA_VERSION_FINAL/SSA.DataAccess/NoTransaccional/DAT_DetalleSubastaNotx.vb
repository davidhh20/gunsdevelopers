Imports System
Imports System.Collections.Generic
Imports System.Data.Common
Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Friend Class DAT_DetalleSubastaNotx

    'ENVIA DOS ESTADOS : el 1ero para listar por estado y el 2do para que no liste los eliminados
    Public Function Seleccionar(ByVal id_subasta As Integer, ByVal nestado As Integer, ByVal nestado2 As Integer, ByVal pDatabase As Database) As List(Of BE_Detalle_Subasta)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_DETALLESUBASTA_SELECCIONAR")
        pDatabase.AddInParameter(objDbCommand, "VI_IDSUBASTA", DbType.Int32, id_subasta)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADODETALLE", DbType.Int32, nestado)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADOELIMINADO", DbType.Int32, nestado2)


        Dim Collection As New List(Of BE_Detalle_Subasta)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_DetalleSubasta.Seleccionar(dr))
            End While

        End Using

        Return Collection
    End Function

    Public Function ListarPaginado(ByVal ObjBE_ListaOferta As BE_ListaOferta, ByVal obj_pPaginador As Paginador, ByVal pDatabase As Database) As List(Of BE_Detalle_Subasta)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_DETALLESUBASTA_LISTAR")

        pDatabase.AddInParameter(objDbCommand, "VI_IDSUBASTA", DbType.Int32, ObjBE_ListaOferta.id_subasta)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADOSUBASTA", DbType.Int32, ObjBE_ListaOferta.nestado4)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_DETSUBASTA", DbType.Int32, ObjBE_ListaOferta.nestado)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_DETSUBASTA_ELIMINADO", DbType.Int32, ObjBE_ListaOferta.nestado2)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_SUBASTA_ELIMINADO", DbType.Int32, ObjBE_ListaOferta.nestado3)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROPAGINA", DbType.Int32, obj_pPaginador.NumeroPagina)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROREGISTROS", DbType.Int32, obj_pPaginador.NumeroRegistros)
        pDatabase.AddOutParameter(objDbCommand, "VO_TOTALREGISTROS", DbType.Int32, obj_pPaginador.TotalResgistros)

        Dim Collection As New List(Of BE_Detalle_Subasta)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_DetalleSubasta.Listar(dr))
            End While

        End Using
        obj_pPaginador.TotalResgistros = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VO_TOTALREGISTROS"))

        Return Collection
    End Function

    Public Function Rentabilidad(ByVal ObjBE_ListaOferta As BE_ListaOferta, ByVal pDatabase As Database) As List(Of BE_Rentabilidad)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_REPORTE_RENTABILIDAD")

        pDatabase.AddInParameter(objDbCommand, "VI_IDSUBASTA", DbType.Int32, ObjBE_ListaOferta.id_subasta)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_DETSUBASTAELIMINADA", DbType.Int32, ObjBE_ListaOferta.nestado)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_OFERTAELIMINADA", DbType.Int32, ObjBE_ListaOferta.nestado2)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_ADJUDICACIONELIMINADA", DbType.Int32, ObjBE_ListaOferta.nestado3)

        Dim Collection As New List(Of BE_Rentabilidad)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_DetalleSubasta.Rentabilidad(dr))
            End While

        End Using

        Return Collection
    End Function

End Class
