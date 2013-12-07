Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity

Friend Class DAT_OfertaTx

    Public Sub Eliminar(ByVal objBE_Oferta As BE_Oferta, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_OFERTA_CAMBIAR_ESTADO"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDOFERTA", DbType.Int32, objBE_Oferta.id_oferta)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Oferta.nestado)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

    Public Sub Insertar(ByVal objBE_Oferta As BE_Oferta, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_OFERTA_INSERTAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddOutParameter(objDbCommand, "VI_IDOFERTA", DbType.Int32, 4)
            pDatabase.AddInParameter(objDbCommand, "VI_IDUSUARIO", DbType.Int32, objBE_Oferta.id_usuario)
            pDatabase.AddInParameter(objDbCommand, "VI_IDDETSUBASTA", DbType.Int32, objBE_Oferta.id_detsubasta)
            pDatabase.AddInParameter(objDbCommand, "VI_PRECIO", DbType.Decimal, objBE_Oferta.nprecio_oferta)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Oferta.nestado)

            objDbCommand.ExecuteNonQuery()

            objBE_Oferta.id_oferta = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VI_IDOFERTA"))

        End Using

    End Sub



End Class
