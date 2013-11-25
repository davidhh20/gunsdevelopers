
Public Class BE_Parametro

#Region "Campos"

    Private _IdParametro As Integer
    Private _nclase As Integer
    Private _nSubclase As Byte
    Private _cDescripcion As String
    Private _nEstado As Integer
    Private _beliminado As Boolean
    Private _cDescripcionGrupo As String
    Private _bmodificar As Boolean
#End Region

#Region "Propiedades"

    Public Property IdParametro() As Integer
        Get
            Return _IdParametro
        End Get
        Set(ByVal value As Integer)
            _IdParametro = value
        End Set
    End Property

    Public Property nclase() As Integer
        Get
            Return _nclase
        End Get
        Set(ByVal value As Integer)
            _nclase = value
        End Set
    End Property

    Public Property nSubclase() As Byte
        Get
            Return _nSubclase
        End Get
        Set(ByVal value As Byte)
            _nSubclase = value
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

    Public Property beliminado() As Boolean
        Get
            Return _beliminado
        End Get
        Set(ByVal value As Boolean)
            _beliminado = value
        End Set
    End Property

    Public Property bmodificar() As Boolean
        Get
            Return _bmodificar
        End Get
        Set(ByVal value As Boolean)
            _bmodificar = value
        End Set
    End Property

    Public Property cDescripcionGrupo() As String
        Get
            Return _cDescripcionGrupo
        End Get
        Set(ByVal value As String)
            _cDescripcionGrupo = value
        End Set
    End Property
#End Region

End Class
