Imports System
Imports System.Collections.Generic
Imports System.Data.Common
Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Friend Class DAT_ParametroNoTx

    Public Function ListarPaginado(ByVal ObjBE_Parametro As BE_Parametro, ByVal obj_pPaginador As Paginador, ByVal pDatabase As Database) As List(Of BE_Parametro)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_PARAMETRO_LISTARPAGINADO")

        pDatabase.AddInParameter(objDbCommand, "VI_IDGRUPO", DbType.Int32, ObjBE_Parametro.nclase)
        pDatabase.AddInParameter(objDbCommand, "VI_DESCRIPCION", DbType.String, ObjBE_Parametro.cDescripcion)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO_ELIMINADO", DbType.Int32, ObjBE_Parametro.nEstado)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROPAGINA", DbType.Int32, obj_pPaginador.NumeroPagina)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROREGISTROS", DbType.Int32, obj_pPaginador.NumeroRegistros)
        pDatabase.AddOutParameter(objDbCommand, "VO_TOTALREGISTROS", DbType.Int32, obj_pPaginador.TotalResgistros)

        Dim Collection As New List(Of BE_Parametro)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Parametro.ListarPaginado(dr))
            End While
        End Using
        obj_pPaginador.TotalResgistros = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VO_TOTALREGISTROS"))

        Return Collection
    End Function

    Public Function Listar(ByVal objBE_Parametro As BE_Parametro, ByVal pDatabase As Database) As List(Of BE_Parametro)
        Dim Collection As New List(Of BE_Parametro)
        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_PARAMETRO_LISTAR")

        pDatabase.AddInParameter(objDbCommand, "VI_CLASE", DbType.Byte, objBE_Parametro.nclase)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Parametro.nEstado)

        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Parametro.Listar(dr))
            End While

        End Using

        Return Collection
    End Function

    Public Function ListarGrupos(ByVal pDatabase As Database) As List(Of BE_Parametro)
        Dim Collection As New List(Of BE_Parametro)
        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_GRUPOS_LISTAR")

        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Parametro.ListarGrupo(dr))
            End While

        End Using

        Return Collection
    End Function

End Class
