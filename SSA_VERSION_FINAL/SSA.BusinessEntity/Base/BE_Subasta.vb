Public Class BE_Subasta

#Region "Campos"

    Private _id_subasta As Integer
    Private _dfpublicacion As DateTime
    Private _dfinicio As DateTime
    Private _dfinal As DateTime
    Private _nestadsub As Integer
    Private _nestado As Integer
    Private _dfecha As DateTime

    Private _MiDetalle As List(Of BE_Detalle_Subasta)
    Private _nestado2 As Integer
    Private _des_estado As String
#End Region

#Region " Propiedades "

    Public Property id_subasta() As Integer
        Get
            Return _id_subasta
        End Get
        Set(ByVal value As Integer)
            _id_subasta = value
        End Set
    End Property

    Public Property dfpublicacion() As DateTime
        Get
            Return _dfpublicacion
        End Get
        Set(ByVal value As DateTime)
            _dfpublicacion = value
        End Set
    End Property

    Public Property dfinicio() As DateTime
        Get
            Return _dfinicio
        End Get
        Set(ByVal value As DateTime)
            _dfinicio = value
        End Set
    End Property

    Public Property dfinal() As DateTime
        Get
            Return _dfinal
        End Get
        Set(ByVal value As DateTime)
            _dfinal = value
        End Set
    End Property

    Public Property nestadsub() As Integer
        Get
            Return _nestadsub
        End Get
        Set(ByVal value As Integer)
            _nestadsub = value
        End Set
    End Property

    Public Property nestado() As Integer
        Get
            Return _nestado
        End Get
        Set(ByVal value As Integer)
            _nestado = value
        End Set
    End Property

    Public Property dfecha() As DateTime
        Get
            Return _dfecha
        End Get
        Set(ByVal value As DateTime)
            _dfecha = value
        End Set
    End Property

#End Region

#Region " Propiedades de Ayuda "

    Public Property MiDetalle() As List(Of BE_Detalle_Subasta)
        Get
            Return _MiDetalle
        End Get
        Set(ByVal value As List(Of BE_Detalle_Subasta))
            _MiDetalle = value
        End Set
    End Property

    Public Property nestado2() As Integer
        Get
            Return _nestado2
        End Get
        Set(ByVal value As Integer)
            _nestado2 = value
        End Set
    End Property

    Public Property des_estado() As String
        Get
            Return _des_estado
        End Get
        Set(ByVal value As String)
            _des_estado = value
        End Set
    End Property

#End Region

#Region " Propiedades ReadOnly "
    Public ReadOnly Property s_dfpublicacion() As String
        Get
            Return _dfpublicacion.ToString("dd/MM/yyyy")
        End Get
    End Property

    Public ReadOnly Property s_dfinicio() As String
        Get
            Return _dfinicio.ToString("dd/MM/yyyy")
        End Get
    End Property

    Public ReadOnly Property s_dfinal() As String
        Get
            Return _dfinal.ToString("dd/MM/yyyy")
        End Get
    End Property

    Public ReadOnly Property s_dfecha() As String
        Get
            Return _dfecha.ToString("dd/MM/yyyy")
        End Get
    End Property

#End Region
End Class
