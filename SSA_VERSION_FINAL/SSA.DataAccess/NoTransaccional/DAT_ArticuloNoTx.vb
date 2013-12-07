Imports System
Imports System.Collections.Generic
Imports System.Data.Common
Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data


Friend Class DAT_ArticuloNoTx

    Public Function ListarPaginado(ByVal ObjBE_Articulo As BE_Articulo, ByVal Codigos_Excluidos As String, ByVal obj_pPaginador As Paginador, ByVal pDatabase As Database) As List(Of BE_Articulo)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_ARTICULO_LISTAR_EXCLUIR_CODIGOS")

        pDatabase.AddInParameter(objDbCommand, "VI_CODARTICULO", DbType.Int32, ObjBE_Articulo.ccodart)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, ObjBE_Articulo.nestado)
        pDatabase.AddInParameter(objDbCommand, "VI_TIPO", DbType.Int32, ObjBE_Articulo.id_tipo_articulo)
        pDatabase.AddInParameter(objDbCommand, "VI_SUBTIPO", DbType.Int32, ObjBE_Articulo.id_subtipo_articulo)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADOELIMINADO", DbType.Int32, ObjBE_Articulo.nestado2)
        pDatabase.AddInParameter(objDbCommand, "VI_CODIGOS_EXCLUIDOS", DbType.String, Codigos_Excluidos)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROPAGINA", DbType.Int32, obj_pPaginador.NumeroPagina)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROREGISTROS", DbType.Int32, obj_pPaginador.NumeroRegistros)
        pDatabase.AddOutParameter(objDbCommand, "VO_TOTALREGISTROS", DbType.Int32, obj_pPaginador.TotalResgistros)

        Dim Collection As New List(Of BE_Articulo)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Articulo.Listar(dr))
            End While

        End Using
        obj_pPaginador.TotalResgistros = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VO_TOTALREGISTROS"))

        Return Collection
    End Function

    Public Function ListarPaginado(ByVal ObjBE_Articulo As BE_Articulo, ByVal obj_pPaginador As Paginador, ByVal pDatabase As Database) As List(Of BE_Articulo)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_ARTICULO_LISTAR")

        pDatabase.AddInParameter(objDbCommand, "VI_CODARTICULO", DbType.Int32, ObjBE_Articulo.ccodart)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, ObjBE_Articulo.nestado)
        pDatabase.AddInParameter(objDbCommand, "VI_TIPO", DbType.Int32, ObjBE_Articulo.id_tipo_articulo)
        pDatabase.AddInParameter(objDbCommand, "VI_SUBTIPO", DbType.Int32, ObjBE_Articulo.id_subtipo_articulo)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADOELIMINADO", DbType.Int32, ObjBE_Articulo.nestado2)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROPAGINA", DbType.Int32, obj_pPaginador.NumeroPagina)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROREGISTROS", DbType.Int32, obj_pPaginador.NumeroRegistros)
        pDatabase.AddOutParameter(objDbCommand, "VO_TOTALREGISTROS", DbType.Int32, obj_pPaginador.TotalResgistros)

        Dim Collection As New List(Of BE_Articulo)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Articulo.Listar(dr))
            End While

        End Using
        obj_pPaginador.TotalResgistros = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VO_TOTALREGISTROS"))

        Return Collection
    End Function

    Public Function Seleccionar(ByVal id_articulo As Integer, ByVal pDatabase As Database) As BE_Articulo

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_ARTICULO_SELECCIONAR")

        pDatabase.AddInParameter(objDbCommand, "VI_IDARTICULO", DbType.Int32, id_articulo)

        Dim objBE_Articulo As BE_Articulo
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                objBE_Articulo = POP_Articulo.Seleccionar(dr)
            End While

        End Using

        Return objBE_Articulo
    End Function

End Class


