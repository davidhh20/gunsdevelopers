Imports SSA.BusinessEntity

Friend Class POP_DetalleSubasta

    Public Shared Function Seleccionar(ByVal dr As IDataRecord) As BE_Detalle_Subasta

        Dim objBE_DetSubasta As New BE_Detalle_Subasta

        If dr("id_detsubasta") IsNot DBNull.Value Then
            objBE_DetSubasta.id_detsubasta = Convert.ToInt32(dr("id_detsubasta"))
        End If
        If dr("id_subasta") IsNot DBNull.Value Then
            objBE_DetSubasta.id_subasta = Convert.ToInt32(dr("id_subasta"))
        End If
        If dr("id_articulo") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.id_articulo = Convert.ToInt32(dr("id_articulo"))
        End If
        If dr("nestdetsub") IsNot DBNull.Value Then
            objBE_DetSubasta.nestdetsub = Convert.ToInt32(dr("nestdetsub"))
        End If
        If dr("cestado") IsNot DBNull.Value Then
            objBE_DetSubasta.nestado = Convert.ToInt32(dr("cestado"))
        End If
        If dr("ccodart") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.ccodart = Convert.ToInt32(dr("ccodart"))
        End If
        If dr("cmarca") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.cmarca = Convert.ToString(dr("cmarca"))
        End If
        If dr("cmodelo") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.cmodelo = Convert.ToString(dr("cmodelo"))
        End If
        If dr("canio") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.canio = Convert.ToInt32(dr("canio"))
        End If
        If dr("cdescrip_breve") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.cdescrip_breve = Convert.ToString(dr("cdescrip_breve"))
        End If
        If dr("cdescripcion") IsNot DBNull.Value Then
            objBE_DetSubasta.des_estado = Convert.ToString(dr("cdescripcion"))
        End If
        If dr("nprecio_base") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.nprecio_base = Convert.ToDecimal(dr("nprecio_base"))
        End If

        Return objBE_DetSubasta
    End Function

    Public Shared Function Listar(ByVal dr As IDataRecord) As BE_Detalle_Subasta
        Dim objBE_DetSubasta As New BE_Detalle_Subasta
        If dr("IDDETSUBASTA") IsNot DBNull.Value Then
            objBE_DetSubasta.id_detsubasta = Convert.ToInt32(dr("IDDETSUBASTA"))
        End If
        If dr("IDSUBASTA") IsNot DBNull.Value Then
            objBE_DetSubasta.id_subasta = Convert.ToInt32(dr("IDSUBASTA"))
        End If
        If dr("IDARTICULO") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.id_articulo = Convert.ToInt32(dr("IDARTICULO"))
        End If
        If dr("ESTADODETSUBASTA") IsNot DBNull.Value Then
            objBE_DetSubasta.nestdetsub = Convert.ToInt32(dr("ESTADODETSUBASTA"))
        End If
        If dr("CODARTICULO") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.ccodart = Convert.ToInt32(dr("CODARTICULO"))
        End If
        If dr("MARCA") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.cmarca = Convert.ToString(dr("MARCA"))
        End If
        If dr("MODELO") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.cmodelo = Convert.ToString(dr("MODELO"))
        End If
        If dr("ANIO") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.canio = Convert.ToInt32(dr("ANIO"))
        End If
        If dr("DESCRIPCION") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.cdescrip_breve = Convert.ToString(dr("DESCRIPCION"))
        End If
        If dr("DESESTADO") IsNot DBNull.Value Then
            objBE_DetSubasta.des_estado = Convert.ToString(dr("DESESTADO"))
        End If
        If dr("PRECIO") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.nprecio_base = Convert.ToDecimal(dr("PRECIO"))
        End If
        If dr("NSINIESTRO") IsNot DBNull.Value Then
            objBE_DetSubasta.BE_Articulo.nsiniestro = Convert.ToInt32(dr("NSINIESTRO"))
        End If

        Return objBE_DetSubasta
    End Function

    Public Shared Function Rentabilidad(ByVal dr As IDataRecord) As BE_Rentabilidad
        Dim objBE_Rentabilidad As New BE_Rentabilidad
        If dr("IDDETSUBASTA") IsNot DBNull.Value Then
            objBE_Rentabilidad.id_detsubasta = Convert.ToInt32(dr("IDDETSUBASTA"))
        End If
        If dr("CODARTICULO") IsNot DBNull.Value Then
            objBE_Rentabilidad.ccodart = Convert.ToInt32(dr("CODARTICULO"))
        End If
        If dr("DESARTICULO") IsNot DBNull.Value Then
            objBE_Rentabilidad.cdescrip_breve = Convert.ToString(dr("DESARTICULO"))
        End If
        If dr("PRECIOBASE") IsNot DBNull.Value Then
            objBE_Rentabilidad.nprecio_base = Convert.ToDecimal(dr("PRECIOBASE"))
        End If
        If dr("PRECIOOFERTADO") IsNot DBNull.Value Then
            objBE_Rentabilidad.nprecio_oferta = Convert.ToDecimal(dr("PRECIOOFERTADO"))
        End If
        If dr("MARCA") IsNot DBNull.Value Then
            objBE_Rentabilidad.BE_Usuario.nmarca = Convert.ToInt32(dr("MARCA"))
        End If
        If dr("USUARIONOM") IsNot DBNull.Value Then
            objBE_Rentabilidad.BE_Usuario.cnombres = Convert.ToString(dr("USUARIONOM"))
        End If
        If dr("USUARIOAPE") IsNot DBNull.Value Then
            objBE_Rentabilidad.BE_Usuario.capellidos = Convert.ToString(dr("USUARIOAPE"))
        End If
        If dr("COMENTARIO") IsNot DBNull.Value Then
            objBE_Rentabilidad.cComentario = Convert.ToString(dr("COMENTARIO"))
        End If
        If dr("RENTABILIDAD") IsNot DBNull.Value Then
            objBE_Rentabilidad.rentabilidad = Convert.ToDecimal(dr("RENTABILIDAD"))
        End If
        Return objBE_Rentabilidad
    End Function

End Class
