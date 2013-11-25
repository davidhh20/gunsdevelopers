Public Class BE_ListaOferta

#Region " Campos "
    Private _id_detsubasta As Integer
    Private _id_subasta As Integer
    Private _id_articulo As Integer
    Private _ccodart As Integer
    Private _cdescrip_breve As String
    Private _id_oferta As System.Nullable(Of Integer)
    Private _nprecio_oferta As System.Nullable(Of Decimal)
    Private _dfecha As System.Nullable(Of DateTime)
    Private _crutimage1 As String
    Private _crutimage2 As String
    Private _crutimage3 As String

    Private _id_usuario As Integer
    Private _id_tipo_articulo As Integer
    Private _id_subtipo_articulo As Integer
    Private _nestado As Integer
    Private _nestado2 As Integer
    Private _nestado3 As Integer
    Private _nestado4 As Integer
    Private _nestado5 As Integer
    Private _BE_Articulo As BE_Articulo

    Private _dfechaInicio As DateTime
    Private _dfechaFin As DateTime

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

    Public Property id_subasta() As Integer
        Get
            Return _id_subasta
        End Get
        Set(ByVal value As Integer)
            _id_subasta = value
        End Set
    End Property

    Public Property id_articulo() As Integer
        Get
            Return _id_articulo
        End Get
        Set(ByVal value As Integer)
            _id_articulo = value
        End Set
    End Property

    Public Property id_oferta() As System.Nullable(Of Integer)
        Get
            Return _id_oferta
        End Get
        Set(ByVal value As System.Nullable(Of Integer))
            _id_oferta = value
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

    Public Property dfecha() As System.Nullable(Of DateTime)
        Get
            Return _dfecha
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _dfecha = value
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

    Public Property crutimage1() As String
        Get
            Return _crutimage1
        End Get
        Set(ByVal value As String)
            _crutimage1 = value
        End Set
    End Property

    Public Property crutimage2() As String
        Get
            Return _crutimage2
        End Get
        Set(ByVal value As String)
            _crutimage2 = value
        End Set
    End Property

    Public Property crutimage3() As String
        Get
            Return _crutimage3
        End Get
        Set(ByVal value As String)
            _crutimage3 = value
        End Set
    End Property

    Public Property BE_Articulo() As BE_Articulo
        Get
            Return _BE_Articulo
        End Get
        Set(ByVal value As BE_Articulo)
            _BE_Articulo = value
        End Set
    End Property

#End Region

#Region " Propiedades de Solo Lectura"

    Public ReadOnly Property s_dfecha() As String
        Get
            If _dfecha.HasValue Then
                Return _dfecha.Value.ToString("dd/MM/yyyy")
            Else
                Return String.Empty
            End If
        End Get
    End Property

#End Region

#Region " Propiedades de Ayuda "

    Public Property id_usuario() As Integer
        Get
            Return _id_usuario
        End Get
        Set(ByVal value As Integer)
            _id_usuario = value
        End Set
    End Property

    Public Property id_tipo_articulo() As Integer
        Get
            Return _id_tipo_articulo
        End Get
        Set(ByVal value As Integer)
            _id_tipo_articulo = value
        End Set
    End Property

    Public Property id_subtipo_articulo() As Integer
        Get
            Return _id_subtipo_articulo
        End Get
        Set(ByVal value As Integer)
            _id_subtipo_articulo = value
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

    Public Property nestado2() As Integer
        Get
            Return _nestado2
        End Get
        Set(ByVal value As Integer)
            _nestado2 = value
        End Set
    End Property

    Public Property nestado3() As Integer
        Get
            Return _nestado3
        End Get
        Set(ByVal value As Integer)
            _nestado3 = value
        End Set
    End Property

    Public Property nestado4() As Integer
        Get
            Return _nestado4
        End Get
        Set(ByVal value As Integer)
            _nestado4 = value
        End Set
    End Property

    Public Property nestado5() As Integer
        Get
            Return _nestado5
        End Get
        Set(ByVal value As Integer)
            _nestado5 = value
        End Set
    End Property

    Public Property dfechaInicio() As DateTime
        Get
            Return _dfechaInicio
        End Get
        Set(ByVal value As DateTime)
            _dfechaInicio = value
        End Set
    End Property

    Public Property dfechaFin() As DateTime
        Get
            Return _dfechaFin
        End Get
        Set(ByVal value As DateTime)
            _dfechaFin = value
        End Set
    End Property

#End Region

End Class