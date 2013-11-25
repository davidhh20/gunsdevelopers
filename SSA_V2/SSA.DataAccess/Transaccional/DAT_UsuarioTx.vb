Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity

Friend Class DAT_UsuarioTx

    Public Sub Marcar(ByVal objBE_Usuario As BE_Usuario, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_USUARIO_MARCAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDUSUARIO", DbType.Int32, objBE_Usuario.id_usuario)
            pDatabase.AddInParameter(objDbCommand, "VI_NMARCA", DbType.Int32, objBE_Usuario.nmarca)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

    Public Sub Insertar(ByVal objBE_Usuario As BE_Usuario, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_USUARIO_INSERTAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddOutParameter(objDbCommand, "id_usuario", DbType.Int32, 4)
            If String.IsNullOrEmpty(objBE_Usuario.capellidos) Then
                pDatabase.AddInParameter(objDbCommand, "capellidos", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "capellidos", DbType.String, objBE_Usuario.capellidos)
            End If
            pDatabase.AddInParameter(objDbCommand, "cnombres", DbType.String, objBE_Usuario.cnombres)
            pDatabase.AddInParameter(objDbCommand, "ntipo_persona", DbType.Int32, objBE_Usuario.ntipo_persona)
            If String.IsNullOrEmpty(objBE_Usuario.ndni) Then
                pDatabase.AddInParameter(objDbCommand, "ndni", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "ndni", DbType.String, objBE_Usuario.ndni)
            End If
            If String.IsNullOrEmpty(objBE_Usuario.nruc) Then
                pDatabase.AddInParameter(objDbCommand, "nruc", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "nruc", DbType.String, objBE_Usuario.nruc)
            End If
            pDatabase.AddInParameter(objDbCommand, "ccorreo", DbType.String, objBE_Usuario.ccorreo)
            If String.IsNullOrEmpty(objBE_Usuario.ccorreoalt) Then
                pDatabase.AddInParameter(objDbCommand, "ccorreoalt", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "ccorreoalt", DbType.String, objBE_Usuario.ccorreoalt)
            End If
            pDatabase.AddInParameter(objDbCommand, "ctelefono", DbType.String, objBE_Usuario.ctelefono)
            pDatabase.AddInParameter(objDbCommand, "cusuario", DbType.String, objBE_Usuario.cusuario)
            pDatabase.AddInParameter(objDbCommand, "cclave", DbType.String, objBE_Usuario.cclave)
            pDatabase.AddInParameter(objDbCommand, "nmarca", DbType.Int32, objBE_Usuario.nmarca)
            pDatabase.AddInParameter(objDbCommand, "nestado", DbType.Int32, objBE_Usuario.nestado)
            pDatabase.AddInParameter(objDbCommand, "nIdPerfil", DbType.Byte, objBE_Usuario.nIdPerfil)

            objDbCommand.ExecuteNonQuery()

            objBE_Usuario.id_usuario = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "id_usuario"))

        End Using

    End Sub

    Public Sub Modificar(ByVal objBE_Usuario As BE_Usuario, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_USUARIO_MODIFICAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "id_usuario", DbType.Int32, objBE_Usuario.id_usuario)
            If String.IsNullOrEmpty(objBE_Usuario.capellidos) Then
                pDatabase.AddInParameter(objDbCommand, "capellidos", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "capellidos", DbType.String, objBE_Usuario.capellidos)
            End If
            pDatabase.AddInParameter(objDbCommand, "cnombres", DbType.String, objBE_Usuario.cnombres)
            pDatabase.AddInParameter(objDbCommand, "ntipo_persona", DbType.Int32, objBE_Usuario.ntipo_persona)
            If String.IsNullOrEmpty(objBE_Usuario.ndni) Then
                pDatabase.AddInParameter(objDbCommand, "ndni", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "ndni", DbType.String, objBE_Usuario.ndni)
            End If
            If String.IsNullOrEmpty(objBE_Usuario.nruc) Then
                pDatabase.AddInParameter(objDbCommand, "nruc", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "nruc", DbType.String, objBE_Usuario.nruc)
            End If
            pDatabase.AddInParameter(objDbCommand, "ccorreo", DbType.String, objBE_Usuario.ccorreo)
            If String.IsNullOrEmpty(objBE_Usuario.ccorreoalt) Then
                pDatabase.AddInParameter(objDbCommand, "ccorreoalt", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "ccorreoalt", DbType.String, objBE_Usuario.ccorreoalt)
            End If
            pDatabase.AddInParameter(objDbCommand, "ctelefono", DbType.String, objBE_Usuario.ctelefono)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

End Class
