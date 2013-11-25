Public Class BE_TipoArticulo

#Region "Campos"

    Private _IdTipoArticulo As Integer
    Private _cDescripcion As String
    Private _nEstado As Integer
    Private _bReferencia As Boolean
    Private _nEstado2 As Integer
    Private _nEstado3 As Integer
#End Region

#Region "Propiedades"

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

    Public Property nEstado3() As Integer
        Get
            Return _nEstado3
        End Get
        Set(ByVal value As Integer)
            _nEstado3 = value
        End Set
    End Property

#End Region

End Class
