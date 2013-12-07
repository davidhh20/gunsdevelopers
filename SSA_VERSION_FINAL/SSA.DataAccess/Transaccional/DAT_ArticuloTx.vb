Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports SSA.BusinessEntity

Friend Class DAT_ArticuloTx

    Public Sub CambiarEstado(ByVal objBE_Articulo As BE_Articulo, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_ARTICULO_CAMBIAR_ESTADO"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "VI_IDARTICULO", DbType.Int32, objBE_Articulo.id_articulo)
            pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, objBE_Articulo.nestado)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

    Public Sub Insertar(ByVal objBE_Articulo As BE_Articulo, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_ARTICULO_INSERTAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddOutParameter(objDbCommand, "id_articulo", DbType.Int32, 4)
            pDatabase.AddInParameter(objDbCommand, "id_tipo_articulo", DbType.Int32, objBE_Articulo.id_tipo_articulo)
            pDatabase.AddInParameter(objDbCommand, "id_subtipo_articulo", DbType.Int32, objBE_Articulo.id_subtipo_articulo)
            pDatabase.AddInParameter(objDbCommand, "ccodart", DbType.Int32, objBE_Articulo.ccodart)
            If objBE_Articulo.canio = 0 Then
                pDatabase.AddInParameter(objDbCommand, "canio", DbType.Int32, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "canio", DbType.Int32, objBE_Articulo.canio)
            End If
            pDatabase.AddInParameter(objDbCommand, "cdescrip_breve", DbType.String, objBE_Articulo.cdescrip_breve)
            If String.IsNullOrEmpty(objBE_Articulo.Cdattec) Then
                pDatabase.AddInParameter(objDbCommand, "Cdattec", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "Cdattec", DbType.String, objBE_Articulo.Cdattec)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.cmarca) Then
                pDatabase.AddInParameter(objDbCommand, "cmarca", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "cmarca", DbType.String, objBE_Articulo.cmarca)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.cmodelo) Then
                pDatabase.AddInParameter(objDbCommand, "cmodelo", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "cmodelo", DbType.String, objBE_Articulo.cmodelo)
            End If
            If objBE_Articulo.nsiniestro = 0 Then
                pDatabase.AddInParameter(objDbCommand, "nsiniestro", DbType.Int32, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "nsiniestro", DbType.Int32, objBE_Articulo.nsiniestro)
            End If
            pDatabase.AddInParameter(objDbCommand, "nprecio_base", DbType.Decimal, objBE_Articulo.nprecio_base)
            If objBE_Articulo.nindemnizacion.HasValue Then
                pDatabase.AddInParameter(objDbCommand, "nindemnizacion", DbType.Decimal, objBE_Articulo.nindemnizacion)
            Else
                pDatabase.AddInParameter(objDbCommand, "nindemnizacion", DbType.Decimal, DBNull.Value)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.cdatos_siniestro) Then
                pDatabase.AddInParameter(objDbCommand, "cdatos_siniestro", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "cdatos_siniestro", DbType.String, objBE_Articulo.cdatos_siniestro)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.cdet_siniestro) Then
                pDatabase.AddInParameter(objDbCommand, "cdet_siniestro", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "cdet_siniestro", DbType.String, objBE_Articulo.cdet_siniestro)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.crutimage1) Then
                pDatabase.AddInParameter(objDbCommand, "crutimage1", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "crutimage1", DbType.String, objBE_Articulo.crutimage1)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.crutimage2) Then
                pDatabase.AddInParameter(objDbCommand, "crutimage2", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "crutimage2", DbType.String, objBE_Articulo.crutimage2)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.crutimage3) Then
                pDatabase.AddInParameter(objDbCommand, "crutimage3", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "crutimage3", DbType.String, objBE_Articulo.crutimage3)
            End If
            pDatabase.AddInParameter(objDbCommand, "nestado", DbType.Int32, objBE_Articulo.nestado)

            objDbCommand.ExecuteNonQuery()

            objBE_Articulo.id_articulo = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "id_articulo"))

        End Using

    End Sub

    Public Sub Modificar(ByVal objBE_Articulo As BE_Articulo, ByVal pDatabase As Database, ByVal pDbConnection As DbConnection, ByVal pDbTransaction As DbTransaction)

        Using objDbCommand As DbCommand = pDatabase.DbProviderFactory.CreateCommand()

            objDbCommand.CommandType = CommandType.StoredProcedure
            objDbCommand.CommandText = "SSA_ARTICULO_MODIFICAR"
            objDbCommand.Connection = pDbConnection

            If pDbTransaction IsNot Nothing Then
                objDbCommand.Transaction = pDbTransaction
            End If

            pDatabase.AddInParameter(objDbCommand, "id_articulo", DbType.Int32, objBE_Articulo.id_articulo)
            pDatabase.AddInParameter(objDbCommand, "id_tipo_articulo", DbType.Int32, objBE_Articulo.id_tipo_articulo)
            pDatabase.AddInParameter(objDbCommand, "id_subtipo_articulo", DbType.Int32, objBE_Articulo.id_subtipo_articulo)
            pDatabase.AddInParameter(objDbCommand, "ccodart", DbType.Int32, objBE_Articulo.ccodart)
            If objBE_Articulo.canio = 0 Then
                pDatabase.AddInParameter(objDbCommand, "canio", DbType.Int32, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "canio", DbType.Int32, objBE_Articulo.canio)
            End If
            pDatabase.AddInParameter(objDbCommand, "cdescrip_breve", DbType.String, objBE_Articulo.cdescrip_breve)
            If String.IsNullOrEmpty(objBE_Articulo.Cdattec) Then
                pDatabase.AddInParameter(objDbCommand, "Cdattec", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "Cdattec", DbType.String, objBE_Articulo.Cdattec)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.cmarca) Then
                pDatabase.AddInParameter(objDbCommand, "cmarca", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "cmarca", DbType.String, objBE_Articulo.cmarca)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.cmodelo) Then
                pDatabase.AddInParameter(objDbCommand, "cmodelo", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "cmodelo", DbType.String, objBE_Articulo.cmodelo)
            End If
            If objBE_Articulo.nsiniestro = 0 Then
                pDatabase.AddInParameter(objDbCommand, "nsiniestro", DbType.Int32, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "nsiniestro", DbType.Int32, objBE_Articulo.nsiniestro)
            End If
            pDatabase.AddInParameter(objDbCommand, "nprecio_base", DbType.Decimal, objBE_Articulo.nprecio_base)
            If objBE_Articulo.nindemnizacion.HasValue Then
                pDatabase.AddInParameter(objDbCommand, "nindemnizacion", DbType.Decimal, objBE_Articulo.nindemnizacion)
            Else
                pDatabase.AddInParameter(objDbCommand, "nindemnizacion", DbType.Decimal, DBNull.Value)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.cdatos_siniestro) Then
                pDatabase.AddInParameter(objDbCommand, "cdatos_siniestro", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "cdatos_siniestro", DbType.String, objBE_Articulo.cdatos_siniestro)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.cdet_siniestro) Then
                pDatabase.AddInParameter(objDbCommand, "cdet_siniestro", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "cdet_siniestro", DbType.String, objBE_Articulo.cdet_siniestro)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.crutimage1) Then
                pDatabase.AddInParameter(objDbCommand, "crutimage1", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "crutimage1", DbType.String, objBE_Articulo.crutimage1)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.crutimage2) Then
                pDatabase.AddInParameter(objDbCommand, "crutimage2", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "crutimage2", DbType.String, objBE_Articulo.crutimage2)
            End If
            If String.IsNullOrEmpty(objBE_Articulo.crutimage3) Then
                pDatabase.AddInParameter(objDbCommand, "crutimage3", DbType.String, DBNull.Value)
            Else
                pDatabase.AddInParameter(objDbCommand, "crutimage3", DbType.String, objBE_Articulo.crutimage3)
            End If
            pDatabase.AddInParameter(objDbCommand, "nestado", DbType.Int32, objBE_Articulo.nestado)

            objDbCommand.ExecuteNonQuery()

        End Using

    End Sub

   End Class
