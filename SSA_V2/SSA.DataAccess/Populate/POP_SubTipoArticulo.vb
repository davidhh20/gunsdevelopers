Imports SSA.BusinessEntity

Friend Class POP_SubTipoArticulo

    Public Shared Function Listar(ByVal dr As IDataRecord) As BE_SubTipoArticulo

        Dim objBE_SubTipoArticulo As New BE_SubTipoArticulo

        If dr("id_subtipo_articulo") IsNot DBNull.Value Then
            objBE_SubTipoArticulo.IdSubTipoArticulo = Convert.ToInt32(dr("id_subtipo_articulo"))
        End If
        If dr("cdescripcion") IsNot DBNull.Value Then
            objBE_SubTipoArticulo.cDescripcion = Convert.ToString(dr("cdescripcion"))
        End If

        Return objBE_SubTipoArticulo
    End Function

    Public Shared Function ListarPaginado(ByVal dr As IDataRecord) As BE_SubTipoArticulo

        Dim objBE_SubTipoArticulo As New BE_SubTipoArticulo

        If dr("id_subtipo_articulo") IsNot DBNull.Value Then
            objBE_SubTipoArticulo.IdSubTipoArticulo = Convert.ToInt32(dr("id_subtipo_articulo"))
        End If
        If dr("id_tipo_articulo") IsNot DBNull.Value Then
            objBE_SubTipoArticulo.IdTipoArticulo = Convert.ToInt32(dr("id_tipo_articulo"))
        End If
        If dr("descripcion") IsNot DBNull.Value Then
            objBE_SubTipoArticulo.cDescripcion = Convert.ToString(dr("descripcion"))
        End If
        If dr("destipoarticulo") IsNot DBNull.Value Then
            objBE_SubTipoArticulo.destipo = Convert.ToString(dr("destipoarticulo"))
        End If
        If dr("referencia") IsNot DBNull.Value Then
            objBE_SubTipoArticulo.bReferencia = Convert.ToBoolean(dr("referencia"))
        End If
        Return objBE_SubTipoArticulo
    End Function

End Class
