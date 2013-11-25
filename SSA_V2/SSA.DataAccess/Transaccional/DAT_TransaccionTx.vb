Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity

Friend Class DAT_TransaccionTx

    Public Sub Transaccion_Insertar(ByVal obj_pBE_Transaccion As BE_Transaccional, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_TRANSACCION_INSERT"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then objDbCommand.Transaction = pDbTransaction

            pDatabase.AddInParameter(objDbCommand, "VI_IDUSUARIO", DbType.Int32, obj_pBE_Transaccion.IdUsuario)
            pDatabase.AddInParameter(objDbCommand, "VI_CTABLA", DbType.Int32, obj_pBE_Transaccion.CTabla)
            pDatabase.AddInParameter(objDbCommand, "VI_CMOVIMIENTO", DbType.Int32, obj_pBE_Transaccion.CMovimiento)
            pDatabase.AddInParameter(objDbCommand, "VI_IDREFERENCIA", DbType.Int32, obj_pBE_Transaccion.IdReferencia)

            objDbCommand.ExecuteNonQuery()


        End Using

    End Sub

End Class
