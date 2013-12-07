Imports System
Imports System.Collections.Generic
Imports System.Data.Common
Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Friend Class DAT_SubTipoArticuloNoTx

    Public Function Listar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal pDatabase As Database) As List(Of BE_SubTipoArticulo)
        Dim Collection As New List(Of BE_SubTipoArticulo)
        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_SUBTIPOARTICULO_LISTAR")

        pDatabase.AddInParameter(objDbCommand, "VI_TIPOARTICULO", DbType.Int32, objBE_SubTipoArticulo.IdTipoArticulo)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_SubTipoArticulo.nEstado)

        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_SubTipoArticulo.Listar(dr))
            End While

        End Using

        Return Collection
    End Function


    Public Function ListarPaginado(ByVal ObjBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal obj_pPaginador As Paginador, ByVal pDatabase As Database) As List(Of BE_SubTipoArticulo)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_SUBTIPOARTICULO_LISTARPAGINADO")

        pDatabase.AddInParameter(objDbCommand, "VI_IDSUBTIPOARTICULO", DbType.Int32, ObjBE_SubTipoArticulo.IdSubTipoArticulo)
        pDatabase.AddInParameter(objDbCommand, "VI_IDTIPOARTICULO", DbType.Int32, ObjBE_SubTipoArticulo.IdTipoArticulo)
        pDatabase.AddInParameter(objDbCommand, "VI_DESCRIPCION", DbType.String, ObjBE_SubTipoArticulo.cDescripcion)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_ELIMINADO", DbType.Int32, ObjBE_SubTipoArticulo.nEstado)
        pDatabase.AddInParameter(objDbCommand, "ELIMINADO_ARTICUL0", DbType.Int32, ObjBE_SubTipoArticulo.nEstado2)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROPAGINA", DbType.Int32, obj_pPaginador.NumeroPagina)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROREGISTROS", DbType.Int32, obj_pPaginador.NumeroRegistros)
        pDatabase.AddOutParameter(objDbCommand, "VO_TOTALREGISTROS", DbType.Int32, obj_pPaginador.TotalResgistros)

        Dim Collection As New List(Of BE_SubTipoArticulo)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_SubTipoArticulo.ListarPaginado(dr))
            End While
        End Using
        obj_pPaginador.TotalResgistros = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VO_TOTALREGISTROS"))

        Return Collection
    End Function

End Class
