Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity

Friend Class DAT_ParametroTx

    Public Sub Insertar(ByVal objBE_Parametro As BE_Parametro, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_PARAMETRO_INSERTAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddOutParameter(objDbCommand, "VI_IDPARAMETRO", DbType.Int32, 4)
            pDatabase.AddInParameter(objDbCommand, "VI_CLASE", DbType.Int32, objBE_Parametro.nclase)
            pDatabase.AddInParameter(objDbCommand, "VI_DESCRIPCION", DbType.String, objBE_Parametro.cDescripcion)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Parametro.nEstado)

            objDbCommand.ExecuteNonQuery()

            objBE_Parametro.IdParametro = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VI_IDPARAMETRO"))

        End Using

    End Sub

    Public Sub Modificar(ByVal objBE_Parametro As BE_Parametro, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_PARAMETRO_MODIFICAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDPARAMETRO", DbType.Int32, objBE_Parametro.IdParametro)
            pDatabase.AddInParameter(objDbCommand, "VI_DESCRIPCION", DbType.String, objBE_Parametro.cDescripcion)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

    Public Sub Eliminar(ByVal objBE_Parametro As BE_Parametro, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_PARAMETRO_ELIMINAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDPARAMETRO", DbType.Int32, objBE_Parametro.IdParametro)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Parametro.nEstado)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

End Class
