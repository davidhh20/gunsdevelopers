Public Class BE_Opcion

#Region "Campos"

    Private _IdOpcion As Integer
    Private _Nombre As String
    Private _Url As String
    Private _IdOpcionPadre As Nullable(Of Integer)
    Private _nIdOpcionSubPadre As Nullable(Of Integer)
#End Region

#Region "Propiedades"

    Public Property nIdOpcionSubPadre() As System.Nullable(Of Integer)
        Get
            Return _nIdOpcionSubPadre
        End Get
        Set(ByVal value As System.Nullable(Of Integer))
            _nIdOpcionSubPadre = value
        End Set
    End Property

    Public Property IdOpcion() As Integer
        Get
            Return _IdOpcion
        End Get
        Set(ByVal value As Integer)
            _IdOpcion = value
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

    Public Property Url() As String
        Get
            Return _Url
        End Get
        Set(ByVal value As String)
            _Url = value
        End Set
    End Property

    Public Property IdOpcionPadre() As Nullable(Of Integer)
        Get
            Return _IdOpcionPadre
        End Get
        Set(ByVal value As Nullable(Of Integer))
            _IdOpcionPadre = value
        End Set
    End Property

#End Region
End Class
