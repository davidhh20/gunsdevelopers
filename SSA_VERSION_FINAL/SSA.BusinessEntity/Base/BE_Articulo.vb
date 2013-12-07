Public Class BE_Articulo
#Region "Campos"

    Private _id_articulo As Integer
    Private _id_tipo_articulo As Integer
    Private _id_subtipo_articulo As Integer
    Private _ccodart As Integer

    Private _canio As Integer
    Private _cdescrip_breve As String
    Private _Cdattec As String
    Private _cmarca As String
    Private _cmodelo As String

    Private _nsiniestro As Integer
    Private _nprecio_base As Decimal
    Private _nindemnizacion As System.Nullable(Of Decimal)
    Private _cdatos_siniestro As String
    Private _cdet_siniestro As String

    Private _crutimage1 As String
    Private _crutimage2 As String
    Private _crutimage3 As String

    Private _nestado As Integer ' ya esta

    Private _des_tipo As String
    Private _subdes_tipo As String
    Private _des_estado As String
    Private _nestado2 As Integer

#End Region

#Region "Propiedades"

    Public Property id_articulo() As Integer
        Get
            Return _id_articulo
        End Get
        Set(ByVal value As Integer)
            _id_articulo = value
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

    Public Property ccodart() As Integer
        Get
            Return _ccodart
        End Get
        Set(ByVal value As Integer)
            _ccodart = value
        End Set
    End Property

    Public Property canio() As String
        Get
            Return _canio
        End Get
        Set(ByVal value As String)
            _canio = value
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

    Public Property nestado() As Integer
        Get
            Return _nestado
        End Get
        Set(ByVal value As Integer)
            _nestado = value
        End Set
    End Property

    Public Property Cdattec() As String
        Get
            Return _Cdattec
        End Get
        Set(ByVal value As String)
            _Cdattec = value
        End Set
    End Property

    Public Property cmarca() As String
        Get
            Return _cmarca
        End Get
        Set(ByVal value As String)
            _cmarca = value
        End Set
    End Property

    Public Property cmodelo() As String
        Get
            Return _cmodelo
        End Get
        Set(ByVal value As String)
            _cmodelo = value
        End Set

    End Property

    Public Property nsiniestro() As Integer
        Get
            Return _nsiniestro
        End Get
        Set(ByVal value As Integer)
            _nsiniestro = value
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


    Public Property nindemnizacion() As System.Nullable(Of Decimal)
        Get
            Return _nindemnizacion
        End Get
        Set(ByVal value As System.Nullable(Of Decimal))
            _nindemnizacion = value
        End Set

    End Property

    Public Property cdatos_siniestro() As String
        Get
            Return _cdatos_siniestro
        End Get
        Set(ByVal value As String)
            _cdatos_siniestro = value
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

    Public Property cdet_siniestro() As String
        Get
            Return _cdet_siniestro
        End Get
        Set(ByVal value As String)
            _cdet_siniestro = value
        End Set
    End Property
#End Region

#Region " Propiedades de Ayuda "
    Public Property des_tipo() As String
        Get
            Return _des_tipo
        End Get
        Set(ByVal value As String)
            _des_tipo = value
        End Set
    End Property
    Public Property subdes_tipo() As String
        Get
            Return _subdes_tipo
        End Get
        Set(ByVal value As String)
            _subdes_tipo = value
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
    Public Property nestado2() As Integer
        Get
            Return _nestado2
        End Get
        Set(ByVal value As Integer)
            _nestado2 = value
        End Set
    End Property
#End Region
End Class
