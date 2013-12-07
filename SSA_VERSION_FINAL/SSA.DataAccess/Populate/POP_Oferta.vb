Imports SSA.BusinessEntity

Friend Class POP_Oferta

    Public Shared Function ListarPaginado(ByVal dr As IDataRecord) As BE_ListaOferta

        Dim objBE_ListaOferta As New BE_ListaOferta

        If dr("ID_DETSUBASTA") IsNot DBNull.Value Then
            objBE_ListaOferta.id_detsubasta = Convert.ToInt32(dr("ID_DETSUBASTA"))
        End If
        If dr("ID_SUBASTA") IsNot DBNull.Value Then
            objBE_ListaOferta.id_subasta = Convert.ToInt32(dr("ID_SUBASTA"))
        End If
        If dr("ID_ARTICULO") IsNot DBNull.Value Then
            objBE_ListaOferta.id_articulo = Convert.ToInt32(dr("ID_ARTICULO"))
        End If
        If dr("CODART") IsNot DBNull.Value Then
            objBE_ListaOferta.ccodart = Convert.ToInt32(dr("CODART"))
        End If
        If dr("DESCRIPCION") IsNot DBNull.Value Then
            objBE_ListaOferta.cdescrip_breve = Convert.ToString(dr("DESCRIPCION"))
        End If
        If dr("ID_OFERTA") IsNot DBNull.Value Then
            objBE_ListaOferta.id_oferta = Convert.ToInt32(dr("ID_OFERTA"))
        End If
        If dr("PRECIO") IsNot DBNull.Value Then
            objBE_ListaOferta.nprecio_oferta = Convert.ToDecimal(dr("PRECIO"))
        End If
        If dr("FECHA") IsNot DBNull.Value Then
            objBE_ListaOferta.dfecha = Convert.ToDateTime(dr("FECHA"))
        End If
        If dr("IMAGEN1") IsNot DBNull.Value Then
            objBE_ListaOferta.crutimage1 = Convert.ToString(dr("IMAGEN1"))
        End If
        If dr("IMAGEN2") IsNot DBNull.Value Then
            objBE_ListaOferta.crutimage2 = Convert.ToString(dr("IMAGEN2"))
        End If
        If dr("IMAGEN3") IsNot DBNull.Value Then
            objBE_ListaOferta.crutimage3 = Convert.ToString(dr("IMAGEN3"))
        End If
        If dr("FECHAINICIO") IsNot DBNull.Value Then
            objBE_ListaOferta.dfechaInicio = Convert.ToDateTime(dr("FECHAINICIO"))
        End If
        If dr("FECHAFIN") IsNot DBNull.Value Then
            objBE_ListaOferta.dfechaFin = Convert.ToDateTime(dr("FECHAFIN"))
        End If
        If dr("ESTADOSUBASTA") IsNot DBNull.Value Then
            objBE_ListaOferta.nestado4 = Convert.ToInt32(dr("ESTADOSUBASTA"))
        End If

        Return objBE_ListaOferta

    End Function

    Public Shared Function Seleccionar(ByVal dr As IDataRecord) As BE_ListaOferta

        Dim objBE_ListaOferta As New BE_ListaOferta

        If dr("MAXIMO") IsNot DBNull.Value Then
            objBE_ListaOferta.nprecio_oferta = Convert.ToDecimal(dr("MAXIMO"))
        End If

        Return objBE_ListaOferta

    End Function

    Public Shared Function Listar(ByVal dr As IDataRecord) As BE_Oferta
        Dim BE As New BE_Oferta
        If dr("id_oferta") IsNot DBNull.Value Then
            BE.id_oferta = Convert.ToInt32(dr("id_oferta"))
        End If
        If dr("nprecio_oferta") IsNot DBNull.Value Then
            BE.nprecio_oferta = Convert.ToDecimal(dr("nprecio_oferta"))
        End If
        If dr("dfecha") IsNot DBNull.Value Then
            BE.dfecha = Convert.ToDateTime(dr("dfecha"))
        End If
        If dr("id_usuario") IsNot DBNull.Value Then
            BE.id_usuario = Convert.ToInt32(dr("id_usuario"))
        End If
        If dr("capellidos") IsNot DBNull.Value Then
            BE.BE_Usuario.capellidos = Convert.ToString(dr("capellidos"))
        End If
        If dr("cnombres") IsNot DBNull.Value Then
            BE.BE_Usuario.cnombres = Convert.ToString(dr("cnombres"))
        End If
        If dr("nmarca") IsNot DBNull.Value Then
            BE.BE_Usuario.nmarca = Convert.ToString(dr("nmarca"))
        End If
        If dr("ccorreo") IsNot DBNull.Value Then
            BE.BE_Usuario.ccorreo = Convert.ToString(dr("ccorreo"))
        End If
        If dr("ccorreoalt") IsNot DBNull.Value Then
            BE.BE_Usuario.ccorreoalt = Convert.ToString(dr("ccorreoalt"))
        End If

        If dr("id_adjudicacion") IsNot DBNull.Value Then
            BE.BE_Adjudicacion.id_adjudicacion = Convert.ToInt32(dr("id_adjudicacion"))
        End If
        If dr("dfnotificacion") IsNot DBNull.Value Then
            BE.BE_Adjudicacion.dfnotificacion = Convert.ToDateTime(dr("dfnotificacion"))
        End If
        If dr("fvendido") IsNot DBNull.Value Then
            BE.BE_Adjudicacion.fvendido = Convert.ToBoolean(dr("fvendido"))
        End If
        If dr("cComentario") IsNot DBNull.Value Then
            BE.BE_Adjudicacion.cComentario = Convert.ToString(dr("cComentario"))
        End If

        Return BE
    End Function

    Public Shared Function ListarSubasta(ByVal dr As IDataRecord) As BE_ListaOferta

        Dim objBE_ListaOferta As New BE_ListaOferta

        If dr("ID_DETSUBASTA") IsNot DBNull.Value Then
            objBE_ListaOferta.id_detsubasta = Convert.ToInt32(dr("ID_DETSUBASTA"))
        End If
        If dr("ID_SUBASTA") IsNot DBNull.Value Then
            objBE_ListaOferta.id_subasta = Convert.ToInt32(dr("ID_SUBASTA"))
        End If
        If dr("ID_ARTICULO") IsNot DBNull.Value Then
            objBE_ListaOferta.id_articulo = Convert.ToInt32(dr("ID_ARTICULO"))
        End If
        If dr("CODART") IsNot DBNull.Value Then
            objBE_ListaOferta.ccodart = Convert.ToInt32(dr("CODART"))
        End If
        If dr("DESCRIPCION") IsNot DBNull.Value Then
            objBE_ListaOferta.cdescrip_breve = Convert.ToString(dr("DESCRIPCION"))
        End If
        If dr("IMAGEN1") IsNot DBNull.Value Then
            objBE_ListaOferta.crutimage1 = Convert.ToString(dr("IMAGEN1"))
        End If
        If dr("IMAGEN2") IsNot DBNull.Value Then
            objBE_ListaOferta.crutimage2 = Convert.ToString(dr("IMAGEN2"))
        End If
        If dr("IMAGEN3") IsNot DBNull.Value Then
            objBE_ListaOferta.crutimage3 = Convert.ToString(dr("IMAGEN3"))
        End If
        If dr("FECHAINICIO") IsNot DBNull.Value Then
            objBE_ListaOferta.dfechaInicio = Convert.ToDateTime(dr("FECHAINICIO"))
        End If
        If dr("FECHAFIN") IsNot DBNull.Value Then
            objBE_ListaOferta.dfechaFin = Convert.ToDateTime(dr("FECHAFIN"))
        End If
        If dr("ESTADOSUBASTA") IsNot DBNull.Value Then
            objBE_ListaOferta.nestado4 = Convert.ToInt32(dr("ESTADOSUBASTA"))
        End If

        Return objBE_ListaOferta

    End Function

    Public Shared Function ListarSubastaValidad(ByVal dr As IDataRecord) As BE_ListaOferta

        Dim objBE_ListaOferta As New BE_ListaOferta

        If dr("ID_DETSUBASTA") IsNot DBNull.Value Then
            objBE_ListaOferta.id_detsubasta = Convert.ToInt32(dr("ID_DETSUBASTA"))
        End If
        If dr("ID_SUBASTA") IsNot DBNull.Value Then
            objBE_ListaOferta.id_subasta = Convert.ToInt32(dr("ID_SUBASTA"))
        End If
        If dr("ID_ARTICULO") IsNot DBNull.Value Then
            objBE_ListaOferta.id_articulo = Convert.ToInt32(dr("ID_ARTICULO"))
        End If
        If dr("ID_OFERTA") IsNot DBNull.Value Then
            objBE_ListaOferta.id_oferta = Convert.ToInt32(dr("ID_OFERTA"))
        End If
        If dr("nprecio_oferta") IsNot DBNull.Value Then
            objBE_ListaOferta.nprecio_oferta = Convert.ToDecimal(dr("nprecio_oferta"))
        End If
        If dr("dfecha") IsNot DBNull.Value Then
            objBE_ListaOferta.dfecha = Convert.ToDateTime(dr("dfecha"))
        End If
        If dr("dfinicio") IsNot DBNull.Value Then
            objBE_ListaOferta.dfechaInicio = Convert.ToDateTime(dr("dfinicio"))
        End If
        If dr("dfinal") IsNot DBNull.Value Then
            objBE_ListaOferta.dfechaFin = Convert.ToDateTime(dr("dfinal"))
        End If
        If dr("nestadsub") IsNot DBNull.Value Then
            objBE_ListaOferta.nestado4 = Convert.ToInt32(dr("nestadsub"))
        End If

        Return objBE_ListaOferta

    End Function

End Class
