Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity

Friend Class DAT_SubTipoArticuloTx

    Public Sub Insertar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_SUBTIPOARTICULO_INSERTAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddOutParameter(objDbCommand, "VI_IDSUBTIPO", DbType.Int32, 4)
            pDatabase.AddInParameter(objDbCommand, "VI_IDTIPO", DbType.Int32, objBE_SubTipoArticulo.IdTipoArticulo)
            pDatabase.AddInParameter(objDbCommand, "VI_DESCRIPCION", DbType.String, objBE_SubTipoArticulo.cDescripcion)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_SubTipoArticulo.nEstado)

            objDbCommand.ExecuteNonQuery()

            objBE_SubTipoArticulo.IdTipoArticulo = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VI_IDSUBTIPO"))

        End Using

    End Sub


    Public Sub Modificar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_SUBTIPOARTICULO_MODIFICAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDSUBTIPO", DbType.Int32, objBE_SubTipoArticulo.IdSubTipoArticulo)
            pDatabase.AddInParameter(objDbCommand, "VI_IDTIPO", DbType.Int32, objBE_SubTipoArticulo.IdTipoArticulo)
            pDatabase.AddInParameter(objDbCommand, "VI_DESCRIPCION", DbType.String, objBE_SubTipoArticulo.cDescripcion)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

    Public Sub Eliminar(ByVal objBE_SubTipoArticulo As BE_SubTipoArticulo, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_SUBTIPOARTICULO_ELIMINAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDSUBTIPO", DbType.Int32, objBE_SubTipoArticulo.IdSubTipoArticulo)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_SubTipoArticulo.nEstado)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

End Class
