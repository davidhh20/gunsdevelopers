Imports SSA.BusinessEntity

Friend Class POP_Usuario

    Public Shared Function Seguridad_ObtenerUsuario(ByVal dr As IDataRecord) As BE_Usuario

        Dim ojbBE_Usuario As New BE_Usuario

        If dr("id_usuario") IsNot DBNull.Value Then
            ojbBE_Usuario.id_usuario = Convert.ToInt32(dr("id_usuario"))
        End If
        If dr("capellidos") IsNot DBNull.Value Then
            ojbBE_Usuario.capellidos = Convert.ToString(dr("capellidos"))
        End If
        If dr("cnombres") IsNot DBNull.Value Then
            ojbBE_Usuario.cnombres = Convert.ToString(dr("cnombres"))
        End If
        If dr("ntipo_persona") IsNot DBNull.Value Then
            ojbBE_Usuario.ntipo_persona = Convert.ToInt32(dr("ntipo_persona"))
        End If
        If dr("ccorreo") IsNot DBNull.Value Then
            ojbBE_Usuario.ccorreo = Convert.ToString(dr("ccorreo"))
        End If
        If dr("cclave") IsNot DBNull.Value Then
            ojbBE_Usuario.cclave = Convert.ToString(dr("cclave"))
        End If
        If dr("ccorreoalt") IsNot DBNull.Value Then
            ojbBE_Usuario.ccorreoalt = Convert.ToString(dr("ccorreoalt"))
        End If
        If dr("nIdPerfil") IsNot DBNull.Value Then
            ojbBE_Usuario.nIdPerfil = Convert.ToByte(dr("nIdPerfil"))
        End If

        Return ojbBE_Usuario
    End Function

    Public Shared Function Listar(ByVal dr As IDataRecord) As BE_Usuario

        Dim ojbBE_Usuario As New BE_Usuario

        If dr("ID_USUARIO") IsNot DBNull.Value Then
            ojbBE_Usuario.id_usuario = Convert.ToInt32(dr("ID_USUARIO"))
        End If
        If dr("APELLIDOS") IsNot DBNull.Value Then
            ojbBE_Usuario.capellidos = Convert.ToString(dr("APELLIDOS"))
        End If
        If dr("NOMBRES") IsNot DBNull.Value Then
            ojbBE_Usuario.cnombres = Convert.ToString(dr("NOMBRES"))
        End If
        If dr("RUC") IsNot DBNull.Value Then
            ojbBE_Usuario.nruc = Convert.ToString(dr("RUC"))
        End If
        If dr("DNI") IsNot DBNull.Value Then
            ojbBE_Usuario.ndni = Convert.ToString(dr("DNI"))
        End If
        If dr("NMARCA") IsNot DBNull.Value Then
            ojbBE_Usuario.nmarca = Convert.ToInt32(dr("NMARCA"))
        End If
        If dr("NESTADO") IsNot DBNull.Value Then
            ojbBE_Usuario.nestado = Convert.ToInt32(dr("NESTADO"))
        End If
        If dr("CORREO") IsNot DBNull.Value Then
            ojbBE_Usuario.ccorreo = Convert.ToString(dr("CORREO"))
        End If
        If dr("TIPOPERSONA") IsNot DBNull.Value Then
            ojbBE_Usuario.des_TipoPersona = Convert.ToString(dr("TIPOPERSONA"))
        End If

        If dr("IDTIPOPERSONA") IsNot DBNull.Value Then
            ojbBE_Usuario.ntipo_persona = Convert.ToString(dr("IDTIPOPERSONA"))
        End If
        If dr("CORREO2") IsNot DBNull.Value Then
            ojbBE_Usuario.ccorreoalt = Convert.ToString(dr("CORREO2"))
        End If
        If dr("TELEFONO") IsNot DBNull.Value Then
            ojbBE_Usuario.ctelefono = Convert.ToString(dr("TELEFONO"))
        End If

        Return ojbBE_Usuario
    End Function

End Class
