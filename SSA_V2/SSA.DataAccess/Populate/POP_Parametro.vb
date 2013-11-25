Imports SSA.BusinessEntity

Friend Class POP_Parametro

    Public Shared Function ListarPaginado(ByVal dr As IDataRecord) As BE_Parametro

        Dim objBE_Parametro As New BE_Parametro

        If dr("IDPARAMETRO") IsNot DBNull.Value Then
            objBE_Parametro.IdParametro = Convert.ToInt32(dr("IDPARAMETRO"))
        End If
        If dr("IDGRUPO") IsNot DBNull.Value Then
            objBE_Parametro.nclase = Convert.ToInt32(dr("IDGRUPO"))
        End If
        If dr("DESPARAMETRO") IsNot DBNull.Value Then
            objBE_Parametro.cDescripcion = Convert.ToString(dr("DESPARAMETRO"))
        End If
        If dr("DESGRUPO") IsNot DBNull.Value Then
            objBE_Parametro.cDescripcionGrupo = Convert.ToString(dr("DESGRUPO"))
        End If
        If dr("ELIMINADO") IsNot DBNull.Value Then
            objBE_Parametro.beliminado = Convert.ToBoolean(dr("ELIMINADO"))
        End If
        If dr("MODIFICAR") IsNot DBNull.Value Then
            objBE_Parametro.bmodificar = Convert.ToBoolean(dr("MODIFICAR"))
        End If

        Return objBE_Parametro
    End Function

    Public Shared Function Listar(ByVal dr As IDataRecord) As BE_Parametro

        Dim objBE_Parametro As New BE_Parametro

        If dr("id_parametro") IsNot DBNull.Value Then
            objBE_Parametro.IdParametro = Convert.ToInt32(dr("id_parametro"))
        End If
        If dr("cdescripcion") IsNot DBNull.Value Then
            objBE_Parametro.cDescripcion = Convert.ToString(dr("cdescripcion"))
        End If

        Return objBE_Parametro
    End Function


    Public Shared Function ListarGrupo(ByVal dr As IDataRecord) As BE_Parametro

        Dim objBE_Parametro As New BE_Parametro

        If dr("idgrupo") IsNot DBNull.Value Then
            objBE_Parametro.IdParametro = Convert.ToInt32(dr("idgrupo"))
        End If
        If dr("descripcion") IsNot DBNull.Value Then
            objBE_Parametro.cDescripcion = Convert.ToString(dr("descripcion"))
        End If

        Return objBE_Parametro
    End Function

End Class
