Public Class BE_SubTipoArticulo

#Region "Campos"

    Private _IdSubTipoArticulo As Integer
    Private _IdTipoArticulo As Integer
    Private _cDescripcion As String
    Private _nEstado As Integer

    Private _bReferencia As Boolean
    Private _nEstado2 As Integer
    Private _destipo As String
#End Region

#Region "Propiedades"

    Public Property IdSubTipoArticulo() As Integer
        Get
            Return _IdSubTipoArticulo
        End Get
        Set(ByVal value As Integer)
            _IdSubTipoArticulo = value
        End Set
    End Property

    Public Property IdTipoArticulo() As Integer
        Get
            Return _IdTipoArticulo
        End Get
        Set(ByVal value As Integer)
            _IdTipoArticulo = value
        End Set
    End Property

    Public Property cDescripcion() As String
        Get
            Return _cDescripcion
        End Get
        Set(ByVal value As String)
            _cDescripcion = value
        End Set
    End Property

    Public Property nEstado() As Integer
        Get
            Return _nEstado
        End Get
        Set(ByVal value As Integer)
            _nEstado = value
        End Set
    End Property

#End Region

#Region " Propiedades de Ayuda "

    Public Property bReferencia() As Boolean
        Get
            Return _bReferencia
        End Get
        Set(ByVal value As Boolean)
            _bReferencia = value
        End Set
    End Property

    Public Property nEstado2() As Integer
        Get
            Return _nEstado2
        End Get
        Set(ByVal value As Integer)
            _nEstado2 = value
        End Set
    End Property

    Public Property destipo() As String
        Get
            Return _destipo
        End Get
        Set(ByVal value As String)
            _destipo = value
        End Set
    End Property

#End Region

End Class
