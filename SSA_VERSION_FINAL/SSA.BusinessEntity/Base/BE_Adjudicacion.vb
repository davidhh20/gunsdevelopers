Public Class BE_Adjudicacion

#Region "Campos"

    Private _id_adjudicacion As Integer
    Private _id_oferta As Integer
    Private _dfnotificacion As System.Nullable(Of DateTime)
    Private _fvendido As Boolean
    Private _nestado As Integer
    Private _cComentario As String

#End Region

#Region "Propiedades"

    Public Property id_adjudicacion() As Integer
        Get
            Return _id_adjudicacion
        End Get
        Set(ByVal value As Integer)
            _id_adjudicacion = value
        End Set
    End Property

    Public Property id_oferta() As String
        Get
            Return _id_oferta
        End Get
        Set(ByVal value As String)
            _id_oferta = value
        End Set
    End Property

    Public Property dfnotificacion() As System.Nullable(Of DateTime)
        Get
            Return _dfnotificacion
        End Get
        Set(ByVal value As System.Nullable(Of DateTime))
            _dfnotificacion = value
        End Set
    End Property

    Public Property fvendido() As Boolean
        Get
            Return _fvendido
        End Get
        Set(ByVal value As Boolean)
            _fvendido = value
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

    Public Property cComentario() As String
        Get
            Return _cComentario
        End Get
        Set(ByVal value As String)
            _cComentario = value
        End Set
    End Property

#End Region

End Class


