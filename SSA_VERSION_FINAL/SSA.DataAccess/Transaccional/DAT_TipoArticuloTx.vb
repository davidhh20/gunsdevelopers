Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity

Friend Class DAT_TipoArticuloTx

    Public Sub Insertar(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_TIPOARTICULO_INSERTAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddOutParameter(objDbCommand, "VI_IDTIPO", DbType.Int32, 4)
            pDatabase.AddInParameter(objDbCommand, "VI_DESCRIPCION", DbType.String, objBE_TipoArticulo.cDescripcion)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_TipoArticulo.nEstado)

            objDbCommand.ExecuteNonQuery()

            objBE_TipoArticulo.IdTipoArticulo = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VI_IDTIPO"))

        End Using

    End Sub


    Public Sub Modificar(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_TIPOARTICULO_MODIFICAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDTIPO", DbType.Int32, objBE_TipoArticulo.IdTipoArticulo)
            pDatabase.AddInParameter(objDbCommand, "VI_DESCRIPCION", DbType.String, objBE_TipoArticulo.cDescripcion)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

    Public Sub Eliminar(ByVal objBE_TipoArticulo As BE_TipoArticulo, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_TIPOARTICULO_ELIMINAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDTIPO", DbType.Int32, objBE_TipoArticulo.IdTipoArticulo)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_TipoArticulo.nEstado)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

End Class
