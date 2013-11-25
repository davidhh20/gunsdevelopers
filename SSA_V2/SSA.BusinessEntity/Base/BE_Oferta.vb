Public Class BE_Oferta

#Region "Campos"
    Private _id_detsubasta As Integer
    Private _id_usuario As Integer
    Private _id_oferta As Integer
    Private _nprecio_oferta As Decimal
    Private _nestado As Integer
    Private _dfecha As DateTime

    Private _BE_Usuario As BE_Usuario
    Private _BE_Adjudicacion As BE_Adjudicacion
#End Region

#Region "Propiedades"

    Public Property id_detsubasta() As Integer
        Get
            Return _id_detsubasta
        End Get
        Set(ByVal value As Integer)
            _id_detsubasta = value
        End Set
    End Property

    Public Property id_usuario() As Integer
        Get
            Return _id_usuario
        End Get
        Set(ByVal value As Integer)
            _id_usuario = value
        End Set
    End Property

    Public Property nprecio_oferta() As Decimal
        Get
            Return _nprecio_oferta
        End Get
        Set(ByVal value As Decimal)
            _nprecio_oferta = value
        End Set
    End Property

    Public Property id_oferta() As Integer
        Get
            Return _id_oferta
        End Get
        Set(ByVal value As Integer)
            _id_oferta = value
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

    Public Property BE_Usuario() As BE_Usuario
        Get
            Return _BE_Usuario
        End Get
        Set(ByVal value As BE_Usuario)
            _BE_Usuario = value
        End Set
    End Property

    Public Property BE_Adjudicacion() As BE_Adjudicacion
        Get
            Return _BE_Adjudicacion
        End Get
        Set(ByVal value As BE_Adjudicacion)
            _BE_Adjudicacion = value
        End Set
    End Property

    Public ReadOnly Property id_adjudicacion() As Integer
        Get
            Return _BE_Adjudicacion.id_adjudicacion
        End Get
    End Property

#End Region

#Region " Constructor "
    Public Sub New()
        _BE_Usuario = New BE_Usuario
        _BE_Adjudicacion = New BE_Adjudicacion
    End Sub
#End Region
End Class
