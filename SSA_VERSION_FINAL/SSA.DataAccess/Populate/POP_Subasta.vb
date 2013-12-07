Imports SSA.BusinessEntity

Friend Class POP_Subasta

    Public Shared Function Listar(ByVal dr As IDataRecord) As BE_Subasta

        Dim objBE_Subasta As New BE_Subasta

        If dr("ID_SUBASTA") IsNot DBNull.Value Then
            objBE_Subasta.id_subasta = Convert.ToInt32(dr("ID_SUBASTA"))
        End If
        If dr("FECHA_PUBLICACION") IsNot DBNull.Value Then
            objBE_Subasta.dfpublicacion = Convert.ToDateTime(dr("FECHA_PUBLICACION"))
        End If
        If dr("FECHA_INICIO") IsNot DBNull.Value Then
            objBE_Subasta.dfinicio = Convert.ToDateTime(dr("FECHA_INICIO"))
        End If
        If dr("FECHA_FIN") IsNot DBNull.Value Then
            objBE_Subasta.dfinal = Convert.ToDateTime(dr("FECHA_FIN"))
        End If
        If dr("NESTADOSUB") IsNot DBNull.Value Then
            objBE_Subasta.nestadsub = Convert.ToInt32(dr("NESTADOSUB"))
        End If
        If dr("NESTADO") IsNot DBNull.Value Then
            objBE_Subasta.nestado = Convert.ToInt32(dr("NESTADO"))
        End If
        If dr("FECHAREG") IsNot DBNull.Value Then
            objBE_Subasta.dfecha = Convert.ToDateTime(dr("FECHAREG"))
        End If
        If dr("ESTADO") IsNot DBNull.Value Then
            objBE_Subasta.des_estado = Convert.ToString(dr("ESTADO"))
        End If

        Return objBE_Subasta
    End Function

    Public Shared Function Selecionar(ByVal dr As IDataRecord) As BE_Subasta

        Dim objBE_Subasta As New BE_Subasta

        If dr("id_subasta") IsNot DBNull.Value Then
            objBE_Subasta.id_subasta = Convert.ToInt32(dr("id_subasta"))
        End If
        If dr("dfpublicacion") IsNot DBNull.Value Then
            objBE_Subasta.dfpublicacion = Convert.ToDateTime(dr("dfpublicacion"))
        End If
        If dr("dfinicio") IsNot DBNull.Value Then
            objBE_Subasta.dfinicio = Convert.ToDateTime(dr("dfinicio"))
        End If
        If dr("dfinal") IsNot DBNull.Value Then
            objBE_Subasta.dfinal = Convert.ToDateTime(dr("dfinal"))
        End If
        If dr("nestadsub") IsNot DBNull.Value Then
            objBE_Subasta.nestadsub = Convert.ToInt32(dr("nestadsub"))
        End If
        If dr("nestado") IsNot DBNull.Value Then
            objBE_Subasta.nestado = Convert.ToInt32(dr("nestado"))
        End If
        If dr("dfecha") IsNot DBNull.Value Then
            objBE_Subasta.dfecha = Convert.ToDateTime(dr("dfecha"))
        End If

        Return objBE_Subasta
    End Function

End Class
