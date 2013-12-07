Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity

Friend Class DAT_SubastaTx

    Public Sub Eliminar(ByVal objBE_Subasta As BE_Subasta, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_SUBASTA_ELIMINAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDSUBASTA", DbType.Int32, objBE_Subasta.id_subasta)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Subasta.nestado)
            pDatabase.AddOutParameter(objDbCommand, "VO_ARTICULOS", DbType.String, 1000)

            objDbCommand.ExecuteNonQuery()

            objBE_Subasta.des_estado = pDatabase.GetParameterValue(objDbCommand, "VO_ARTICULOS")
        End Using

    End Sub

    Public Sub CambiarEstado(ByVal objBE_Subasta As BE_Subasta, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_SUBASTA_CAMBIAR_ESTADO"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDSUBASTA", DbType.Int32, objBE_Subasta.id_subasta)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Subasta.nestadsub)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

    Public Sub Insertar(ByVal objBE_Subasta As BE_Subasta, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_SUBASTA_INSERTAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddOutParameter(objDbCommand, "VI_IDSUBASTA", DbType.Int32, 4)
            pDatabase.AddInParameter(objDbCommand, "VI_FECHAPUBLICACION", DbType.DateTime, objBE_Subasta.dfpublicacion)
            pDatabase.AddInParameter(objDbCommand, "VI_FECHAINI", DbType.DateTime, objBE_Subasta.dfinicio)
            pDatabase.AddInParameter(objDbCommand, "VI_FECHAFIN", DbType.DateTime, objBE_Subasta.dfinal)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADOSUB", DbType.Int32, objBE_Subasta.nestadsub)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Subasta.nestado)

            objDbCommand.ExecuteNonQuery()

            objBE_Subasta.id_subasta = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VI_IDSUBASTA"))

        End Using

    End Sub

    Public Sub Modificar(ByVal objBE_Subasta As BE_Subasta, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_SUBASTA_MODIFICAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDSUBASTA", DbType.Int32, objBE_Subasta.id_subasta)
            pDatabase.AddInParameter(objDbCommand, "VI_FECHAPUBLICACION", DbType.Int32, objBE_Subasta.dfpublicacion)
            pDatabase.AddInParameter(objDbCommand, "VI_FECHAINI", DbType.Int32, objBE_Subasta.dfinicio)
            pDatabase.AddInParameter(objDbCommand, "VI_FECHAFIN", DbType.Int32, objBE_Subasta.dfinal)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADOSUB", DbType.Int32, objBE_Subasta.nestadsub)

            objDbCommand.ExecuteNonQuery()


        End Using

    End Sub


End Class
