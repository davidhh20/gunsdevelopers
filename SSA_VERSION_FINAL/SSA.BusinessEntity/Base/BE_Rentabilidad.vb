Public Class BE_Rentabilidad

#Region " Campos "

    Private _id_detsubasta As Integer
    Private _ccodart As Integer
    Private _cdescrip_breve As String
    Private _nprecio_base As Decimal
    Private _nprecio_oferta As System.Nullable(Of Decimal)
    Private _cComentario As String
    Private _BE_Usuario As BE_Usuario
    Private _rentabilidad As System.Nullable(Of Decimal)
#End Region

#Region " Propiedades "

    Public Property id_detsubasta() As Integer
        Get
            Return _id_detsubasta
        End Get
        Set(ByVal value As Integer)
            _id_detsubasta = value
        End Set
    End Property

    Public Property nprecio_oferta() As System.Nullable(Of Decimal)
        Get
            Return _nprecio_oferta
        End Get
        Set(ByVal value As System.Nullable(Of Decimal))
            _nprecio_oferta = value
        End Set
    End Property

    Public Property nprecio_base() As Decimal
        Get
            Return _nprecio_base
        End Get
        Set(ByVal value As Decimal)
            _nprecio_base = value
        End Set
    End Property

    Public Property cdescrip_breve() As String
        Get
            Return _cdescrip_breve
        End Get
        Set(ByVal value As String)
            _cdescrip_breve = value
        End Set
    End Property

    Public Property ccodart() As Integer
        Get
            Return _ccodart
        End Get
        Set(ByVal value As Integer)
            _ccodart = value
        End Set
    End Property

    Public Property cComentario() As String
        Get
            Return _cComentario
        End Get
        Set(ByVal value As String)
            _cComentario = value
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

    Public Property rentabilidad() As System.Nullable(Of Decimal)
        Get
            Return _rentabilidad
        End Get
        Set(ByVal value As System.Nullable(Of Decimal))
            _rentabilidad = value
        End Set
    End Property

#End Region

#Region " Constructor "
    Public Sub New()
        _BE_Usuario = New BE_Usuario
    End Sub
#End Region
End Class
