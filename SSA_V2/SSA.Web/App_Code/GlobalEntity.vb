Imports SSA.BusinessEntity

Public Class GlobalEntity

    'Private Sub New()

    '    'Borrar toda esta inicializacion
    '    _BE_Usuario = New BE_Usuario
    '    _BE_Usuario.Username = "etorre"
    '    _BE_Usuario.IdUsuario = 1
    '    _BE_Usuario.BE_Agencia.Id_agencia = 4

    'End Sub

    Private _BE_Usuario As BE_Usuario

    Public Property Usuario() As BE_Usuario
        Get
            Return _BE_Usuario
        End Get
        Set(ByVal value As BE_Usuario)
            _BE_Usuario = value
        End Set
    End Property

    Public Shared ReadOnly Property Instance() As GlobalEntity
        Get
            If HttpContext.Current.Session("__Global__") Is Nothing Then
                HttpContext.Current.Session("__Global__") = New GlobalEntity
            End If
            Dim obj_Global As GlobalEntity = DirectCast(HttpContext.Current.Session("__Global__"), GlobalEntity)
            Return obj_Global
        End Get
    End Property

    Public Shared Sub SignOut()
        HttpContext.Current.Session("__Global__") = Nothing
    End Sub

    Public Shared ReadOnly Property EsNulo() As Boolean
        Get
            If HttpContext.Current.Session("__Global__") Is Nothing Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

End Class
