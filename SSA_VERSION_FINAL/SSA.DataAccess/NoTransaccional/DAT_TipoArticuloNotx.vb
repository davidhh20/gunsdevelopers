Imports System
Imports System.Collections.Generic
Imports System.Data.Common
Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Friend Class DAT_TipoArticuloNoTx
    Public Function Listar(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal pDatabase As Database) As List(Of BE_TipoArticulo)
        Dim Collection As New List(Of BE_TipoArticulo)
        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_TIPOARTICULO_LISTAR")

        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_TipoArticulo.nEstado)

        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_TipoArticulo.Listar(dr))
            End While

        End Using

        Return Collection
    End Function

    Public Function ListarPaginado(ByVal ObjBE_TipoArticulo As BE_TipoArticulo, ByVal obj_pPaginador As Paginador, ByVal pDatabase As Database) As List(Of BE_TipoArticulo)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_TIPOARTICULO_LISTARPAGINADO")

        pDatabase.AddInParameter(objDbCommand, "VI_IDTIPOARTICULO", DbType.Int32, ObjBE_TipoArticulo.IdTipoArticulo)
        pDatabase.AddInParameter(objDbCommand, "VI_DESCRIPCION", DbType.String, ObjBE_TipoArticulo.cDescripcion)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_ELIMINADO", DbType.Int32, ObjBE_TipoArticulo.nEstado)
        pDatabase.AddInParameter(objDbCommand, "ELIMINADO_SUBTIPO", DbType.Int32, ObjBE_TipoArticulo.nEstado2)
        pDatabase.AddInParameter(objDbCommand, "ELIMINADO_ARTICUL0", DbType.Int32, ObjBE_TipoArticulo.nEstado3)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROPAGINA", DbType.Int32, obj_pPaginador.NumeroPagina)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROREGISTROS", DbType.Int32, obj_pPaginador.NumeroRegistros)
        pDatabase.AddOutParameter(objDbCommand, "VO_TOTALREGISTROS", DbType.Int32, obj_pPaginador.TotalResgistros)

        Dim Collection As New List(Of BE_TipoArticulo)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_TipoArticulo.ListarPaginado(dr))
            End While

        End Using
        obj_pPaginador.TotalResgistros = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VO_TOTALREGISTROS"))

        Return Collection
    End Function

End Class
