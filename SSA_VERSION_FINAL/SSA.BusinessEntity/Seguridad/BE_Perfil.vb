Public Class BE_Perfil

#Region "Campos"

    Private _IdPerfil As String
    Private _Nombre As String

#End Region

#Region "Propiedades"

    Public Property IdPerfil() As String
        Get
            Return _IdPerfil
        End Get
        Set(ByVal value As String)
            _IdPerfil = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property

#End Region

End Class
