Imports System
Imports System.Collections.Generic
Imports System.Data.Common
Imports SSA.BusinessEntity
Imports QNET.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data


Friend Class DAT_UsuarioNoTx

    Public Function Seguridad_ObtenerUsuario(ByVal ObjBE_Usuario As BE_Usuario, ByVal pDatabase As Database) As BE_Usuario
        Dim _objBE_Usuario As BE_Usuario
        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_SEGURIDAD_OBTENER_USUARIO")

        pDatabase.AddInParameter(objDbCommand, "username", DbType.String, ObjBE_Usuario.cusuario)
        If String.IsNullOrEmpty(ObjBE_Usuario.cclave) Then
            pDatabase.AddInParameter(objDbCommand, "clave", DbType.String, DBNull.Value)
        Else
            pDatabase.AddInParameter(objDbCommand, "clave", DbType.String, ObjBE_Usuario.cclave)
        End If
        If String.IsNullOrEmpty(ObjBE_Usuario.ccorreo) Then
            pDatabase.AddInParameter(objDbCommand, "ccorreo", DbType.String, DBNull.Value)
        Else
            pDatabase.AddInParameter(objDbCommand, "ccorreo", DbType.String, ObjBE_Usuario.ccorreo)
        End If

        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                _objBE_Usuario = POP_Usuario.Seguridad_ObtenerUsuario(dr)
            End While

        End Using

        Return _objBE_Usuario
    End Function

    Public Function ListarPaginado(ByVal ObjBE_Usuario As BE_Usuario, ByVal obj_pPaginador As Paginador, ByVal pDatabase As Database) As List(Of BE_Usuario)

        Dim objDbCommand As DbCommand = pDatabase.GetStoredProcCommand("SSA_USUARIO_LISTAR")

        If String.IsNullOrEmpty(ObjBE_Usuario.ndni) Then
            pDatabase.AddInParameter(objDbCommand, "VI_DNI", DbType.String, DBNull.Value)
        Else
            pDatabase.AddInParameter(objDbCommand, "VI_DNI", DbType.String, ObjBE_Usuario.ndni)
        End If
        If String.IsNullOrEmpty(ObjBE_Usuario.nruc) Then
            pDatabase.AddInParameter(objDbCommand, "VI_RUC", DbType.String, DBNull.Value)
        Else
            pDatabase.AddInParameter(objDbCommand, "VI_RUC", DbType.String, ObjBE_Usuario.nruc)
        End If

        pDatabase.AddInParameter(objDbCommand, "VI_IDUSUARIO", DbType.Int32, ObjBE_Usuario.id_usuario)
        pDatabase.AddInParameter(objDbCommand, "VI_ESTADO", DbType.Int32, ObjBE_Usuario.nestado)
        pDatabase.AddInParameter(objDbCommand, "VI_IDPERFIL", DbType.Int32, ObjBE_Usuario.nIdPerfil)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROPAGINA", DbType.Int32, obj_pPaginador.NumeroPagina)
        pDatabase.AddInParameter(objDbCommand, "VI_NUMEROREGISTROS", DbType.Int32, obj_pPaginador.NumeroRegistros)
        pDatabase.AddOutParameter(objDbCommand, "VO_TOTALREGISTROS", DbType.Int32, obj_pPaginador.TotalResgistros)

        Dim Collection As New List(Of BE_Usuario)
        Using dr As IDataReader = pDatabase.ExecuteReader(objDbCommand)
            While dr.Read
                Collection.Add(POP_Usuario.Listar(dr))
            End While

        End Using
        obj_pPaginador.TotalResgistros = Convert.ToInt32(pDatabase.GetParameterValue(objDbCommand, "VO_TOTALREGISTROS"))

        Return Collection
    End Function
End Class


