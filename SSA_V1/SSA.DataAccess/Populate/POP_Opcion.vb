Imports SSA.BusinessEntity

Friend Class POP_Opcion

    Public Shared Function Listar(ByVal dr As IDataRecord) As BE_Opcion

        Dim objBE_Opcion As New BE_Opcion

        If dr("nIdOpcion") IsNot DBNull.Value Then
            objBE_Opcion.IdOpcion = Convert.ToInt32(dr("nIdOpcion"))
        End If
        If dr("cNombre") IsNot DBNull.Value Then
            objBE_Opcion.Nombre = Convert.ToString(dr("cNombre"))
        End If
        If dr("cUrlPagina") IsNot DBNull.Value Then
            objBE_Opcion.Url = Convert.ToString(dr("cUrlPagina"))
        End If
        If dr("nIdOpcionPadre") IsNot DBNull.Value Then
            objBE_Opcion.IdOpcionPadre = Convert.ToInt32(dr("nIdOpcionPadre"))
        End If

        Return objBE_Opcion

    End Function

End Class
