Imports System
Imports System.Collections.Generic
Imports System.Data.Common
Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Friend Class DAT_SubastaNotx

    Public Function ListarPaginado(ByVal ObjBE_Subasta As BE_Subasta, ByVal obj_pPaginador As Paginador, ByVal pDatabase As Database) As List(Of BE_Subasta)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_SUBASTA_LISTAR")

        pDatabase.AddInParameter(objDbCommand, "VI_CODSUBASTA", DbType.Int32, ObjBE_Subasta.id_subasta)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, ObjBE_Subasta.nestadsub)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADOELIMINADO", DbType.Int32, ObjBE_Subasta.nestado2)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROPAGINA", DbType.Int32, obj_pPaginador.NumeroPagina)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROREGISTROS", DbType.Int32, obj_pPaginador.NumeroRegistros)
        pDatabase.AddOutParameter(objDbCommand, "VO_TOTALREGISTROS", DbType.Int32, obj_pPaginador.TotalResgistros)

        Dim Collection As New List(Of BE_Subasta)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Subasta.Listar(dr))
            End While

        End Using
        obj_pPaginador.TotalResgistros = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VO_TOTALREGISTROS"))

        Return Collection
    End Function

    Public Function Seleccionar(ByVal id_subasta As Integer, ByVal nestado As Integer, ByVal pDatabase As Database) As BE_Subasta

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_SUBASTA_SELECCIONAR")

        pDatabase.AddInParameter(objDbCommand, "VI_IDSUBASTA", DbType.Int32, id_subasta)

        Dim BE As BE_Subasta
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Dim objDAT_DetalleSubastaNotx As New DAT_DetalleSubastaNotx
                BE = POP_Subasta.Selecionar(dr)
                BE.MiDetalle = objDAT_DetalleSubastaNotx.Seleccionar(id_subasta, 0, nestado, pDatabase)
            End While

        End Using

        Return BE
    End Function

End Class
