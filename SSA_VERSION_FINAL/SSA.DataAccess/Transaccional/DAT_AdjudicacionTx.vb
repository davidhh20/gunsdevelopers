Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity

Friend Class DAT_AdjudicacionTx

    Public Sub Insertar(ByVal objBE_Adjudicacion As BE_Adjudicacion, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_ADJUDICACION_INSERTAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then objDbCommand.Transaction = pDbTransaction

            pDatabase.AddOutParameter(objDbCommand, "VI_IDADJUDICACION", DbType.Int32, 4)
            pDatabase.AddInParameter(objDbCommand, "VI_IDOFERTA", DbType.Int32, objBE_Adjudicacion.id_oferta)
            pDatabase.AddInParameter(objDbCommand, "VI_FECHANOTIFICACION", DbType.DateTime, objBE_Adjudicacion.dfnotificacion)
            pDatabase.AddInParameter(objDbCommand, "VI_VENDIDO", DbType.Boolean, objBE_Adjudicacion.fvendido)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Adjudicacion.nestado)
            If String.IsNullOrEmpty(objBE_Adjudicacion.cComentario) Then
                pDatabase.AddInParameter(objDbCommand, "VI_COMENTARIO", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "VI_COMENTARIO", DbType.String, objBE_Adjudicacion.cComentario)
            End If

            objDbCommand.ExecuteNonQuery()

            objBE_Adjudicacion.id_adjudicacion = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VI_IDADJUDICACION"))

        End Using

    End Sub

    Public Sub Modificar(ByVal objBE_Adjudicacion As BE_Adjudicacion, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_ADJUDICACION_MODIFICAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then objDbCommand.Transaction = pDbTransaction

            pDatabase.AddInParameter(objDbCommand, "VI_IDADJUDICACION", DbType.Int32, objBE_Adjudicacion.id_adjudicacion)
            If Not objBE_Adjudicacion.dfnotificacion.HasValue Then
                pDatabase.AddInParameter(objDbCommand, "VI_FECHANOTIFICACION", DbType.DateTime, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "VI_FECHANOTIFICACION", DbType.DateTime, objBE_Adjudicacion.dfnotificacion)
            End If
            pDatabase.AddInParameter(objDbCommand, "VI_VENDIDO", DbType.Boolean, objBE_Adjudicacion.fvendido)
            If String.IsNullOrEmpty(objBE_Adjudicacion.cComentario) Then
                pDatabase.AddInParameter(objDbCommand, "VI_COMENTARIO", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "VI_COMENTARIO", DbType.String, objBE_Adjudicacion.cComentario)
            End If

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

End Class
