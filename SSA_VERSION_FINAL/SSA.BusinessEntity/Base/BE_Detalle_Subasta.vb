Public Class BE_Detalle_Subasta

#Region "Campos"

    Private _id_detsubasta As Integer
    Private _id_subasta As Integer
    Private _nestdetsub As Integer
    Private _nestado As Integer

    Private _BE_Articulo As BE_Articulo
    Private _des_estado As String
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

    Public Property id_subasta() As Integer
        Get
            Return _id_subasta
        End Get
        Set(ByVal value As Integer)
            _id_subasta = value
        End Set
    End Property

    Public Property nestdetsub() As Integer
        Get
            Return _nestdetsub
        End Get
        Set(ByVal value As Integer)
            _nestdetsub = value
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

    Public Property BE_Articulo() As BE_Articulo
        Get
            Return _BE_Articulo
        End Get
        Set(ByVal value As BE_Articulo)
            _BE_Articulo = value
        End Set
    End Property

#End Region

#Region " Propiedades de Ayuda "

    Public Property des_estado() As String
        Get
            Return _des_estado
        End Get
        Set(ByVal value As String)
            _des_estado = value
        End Set
    End Property

#End Region

#Region " Propiedades Solo Lectura "

    Public ReadOnly Property id_articulo() As Integer
        Get
            Return _BE_Articulo.id_articulo
        End Get
    End Property

#End Region
#Region " Constructor "
    Public Sub New()
        _BE_Articulo = New BE_Articulo
    End Sub
#End Region

End Class
