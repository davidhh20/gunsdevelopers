Imports SSA.BusinessEntity

Friend Class POP_TipoArticulo

    Public Shared Function Listar(ByVal dr As IDataRecord) As BE_TipoArticulo

        Dim objBE_TipoArticulo As New BE_TipoArticulo

        If dr("id_tipo_articulo") IsNot DBNull.Value Then
            objBE_TipoArticulo.IdTipoArticulo = Convert.ToInt32(dr("id_tipo_articulo"))
        End If
        If dr("descripcion") IsNot DBNull.Value Then
            objBE_TipoArticulo.cDescripcion = Convert.ToString(dr("descripcion"))
        End If

        Return objBE_TipoArticulo
    End Function

    Public Shared Function ListarPaginado(ByVal dr As IDataRecord) As BE_TipoArticulo

        Dim objBE_TipoArticulo As New BE_TipoArticulo

        If dr("id_tipo_articulo") IsNot DBNull.Value Then
            objBE_TipoArticulo.IdTipoArticulo = Convert.ToInt32(dr("id_tipo_articulo"))
        End If
        If dr("descripcion") IsNot DBNull.Value Then
            objBE_TipoArticulo.cDescripcion = Convert.ToString(dr("descripcion"))
        End If
        If dr("referencia") IsNot DBNull.Value Then
            objBE_TipoArticulo.bReferencia = Convert.ToBoolean(dr("referencia"))
        End If
        Return objBE_TipoArticulo
    End Function

End Class
