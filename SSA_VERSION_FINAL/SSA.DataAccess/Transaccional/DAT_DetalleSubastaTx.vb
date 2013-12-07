Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity

Friend Class DAT_DetalleSubastaTx

    Public Sub Insertar(ByVal objBE_Detalle_Subasta As BE_Detalle_Subasta, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_DETALLESUBASTA_INSERTAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddOutParameter(objDbCommand, "VI_IDDETSUBASTA", DbType.Int32, 4)
            pDatabase.AddInParameter(objDbCommand, "VI_IDSUBASTA", DbType.Int32, objBE_Detalle_Subasta.id_subasta)
            pDatabase.AddInParameter(objDbCommand, "VI_IDARTICULO", DbType.Int32, objBE_Detalle_Subasta.BE_Articulo.id_articulo)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADODETSUB", DbType.Int32, objBE_Detalle_Subasta.nestdetsub)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Detalle_Subasta.nestado)

            objDbCommand.ExecuteNonQuery()

            objBE_Detalle_Subasta.id_detsubasta = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VI_IDDETSUBASTA"))

        End Using

    End Sub

    Public Sub CambiarEstado(ByVal objBE_Detalle_Subasta As BE_Detalle_Subasta, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_DETALLESUBASTA_CAMBIAR_ESTADO"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDDETSUBASTA", DbType.Int32, objBE_Detalle_Subasta.id_detsubasta)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADODETSUB", DbType.Int32, objBE_Detalle_Subasta.nestdetsub)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

End Class
