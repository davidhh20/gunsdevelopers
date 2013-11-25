Imports SSA.BusinessEntity

Friend Class POP_Articulo

    Public Shared Function Listar(ByVal dr As IDataRecord) As BE_Articulo

        Dim objBE_Articulo As New BE_Articulo

        If dr("ID_ARTICULO") IsNot DBNull.Value Then
            objBE_Articulo.id_articulo = Convert.ToInt32(dr("ID_ARTICULO"))
        End If
        If dr("COD_ARTICULO") IsNot DBNull.Value Then
            objBE_Articulo.ccodart = Convert.ToInt32(dr("COD_ARTICULO"))
        End If
        If dr("DES_ARTICULO") IsNot DBNull.Value Then
            objBE_Articulo.cdescrip_breve = Convert.ToString(dr("DES_ARTICULO"))
        End If
        If dr("TIPO") IsNot DBNull.Value Then
            objBE_Articulo.des_tipo = Convert.ToString(dr("TIPO"))
        End If
        If dr("SUBTIPO") IsNot DBNull.Value Then
            objBE_Articulo.subdes_tipo = Convert.ToString(dr("SUBTIPO"))
        End If
        If dr("MARCA") IsNot DBNull.Value Then
            objBE_Articulo.cmarca = Convert.ToString(dr("MARCA"))
        End If
        If dr("MODELO") IsNot DBNull.Value Then
            objBE_Articulo.cmodelo = Convert.ToString(dr("MODELO"))
        End If
        If dr("ANIO") IsNot DBNull.Value Then
            objBE_Articulo.canio = Convert.ToInt32(dr("ANIO"))
        End If
        If dr("IDESTADO") IsNot DBNull.Value Then
            objBE_Articulo.nestado = Convert.ToInt32(dr("IDESTADO"))
        End If
        If dr("ESTADO") IsNot DBNull.Value Then
            objBE_Articulo.des_estado = Convert.ToString(dr("ESTADO"))
        End If
        If dr("IMAGEN1") IsNot DBNull.Value Then
            objBE_Articulo.crutimage1 = Convert.ToString(dr("IMAGEN1"))
        End If
        If dr("IMAGEN2") IsNot DBNull.Value Then
            objBE_Articulo.crutimage2 = Convert.ToString(dr("IMAGEN2"))
        End If
        If dr("IMAGEN3") IsNot DBNull.Value Then
            objBE_Articulo.crutimage3 = Convert.ToString(dr("IMAGEN3"))
        End If

        Return objBE_Articulo
    End Function

    Public Shared Function Seleccionar(ByVal dr As IDataRecord) As BE_Articulo

        Dim objBE_Articulo As New BE_Articulo

        If dr("id_articulo") IsNot DBNull.Value Then
            objBE_Articulo.id_articulo = Convert.ToInt32(dr("id_articulo"))
        End If
        If dr("id_tipo_articulo") IsNot DBNull.Value Then
            objBE_Articulo.id_tipo_articulo = Convert.ToInt32(dr("id_tipo_articulo"))
        End If
        If dr("id_subtipo_articulo") IsNot DBNull.Value Then
            objBE_Articulo.id_subtipo_articulo = Convert.ToInt32(dr("id_subtipo_articulo"))
        End If
        If dr("ccodart") IsNot DBNull.Value Then
            objBE_Articulo.ccodart = Convert.ToInt32(dr("ccodart"))
        End If
        If dr("canio") IsNot DBNull.Value Then
            objBE_Articulo.canio = Convert.ToInt32(dr("canio"))
        End If
        If dr("cdescrip_breve") IsNot DBNull.Value Then
            objBE_Articulo.cdescrip_breve = Convert.ToString(dr("cdescrip_breve"))
        End If
        If dr("Cdattec") IsNot DBNull.Value Then
            objBE_Articulo.Cdattec = Convert.ToString(dr("Cdattec"))
        End If
        If dr("cmarca") IsNot DBNull.Value Then
            objBE_Articulo.cmarca = Convert.ToString(dr("cmarca"))
        End If
        If dr("cmodelo") IsNot DBNull.Value Then
            objBE_Articulo.cmodelo = Convert.ToString(dr("cmodelo"))
        End If
        If dr("nsiniestro") IsNot DBNull.Value Then
            objBE_Articulo.nsiniestro = Convert.ToInt32(dr("nsiniestro"))
        End If
        If dr("nprecio_base") IsNot DBNull.Value Then
            objBE_Articulo.nprecio_base = Convert.ToDecimal(dr("nprecio_base"))
        End If
        If dr("nindemnizacion") IsNot DBNull.Value Then
            objBE_Articulo.nindemnizacion = Convert.ToDecimal(dr("nindemnizacion"))
        End If
        If dr("cdatos_siniestro") IsNot DBNull.Value Then
            objBE_Articulo.cdatos_siniestro = Convert.ToString(dr("cdatos_siniestro"))
        End If
        If dr("cdet_siniestro") IsNot DBNull.Value Then
            objBE_Articulo.cdet_siniestro = Convert.ToString(dr("cdet_siniestro"))
        End If
        If dr("crutimage1") IsNot DBNull.Value Then
            objBE_Articulo.crutimage1 = Convert.ToString(dr("crutimage1"))
        End If
        If dr("crutimage2") IsNot DBNull.Value Then
            objBE_Articulo.crutimage2 = Convert.ToString(dr("crutimage2"))
        End If
        If dr("crutimage3") IsNot DBNull.Value Then
            objBE_Articulo.crutimage3 = Convert.ToString(dr("crutimage3"))
        End If
        If dr("nestado") IsNot DBNull.Value Then
            objBE_Articulo.nestado = Convert.ToInt32(dr("nestado"))
        End If

        Return objBE_Articulo
    End Function

End Class
