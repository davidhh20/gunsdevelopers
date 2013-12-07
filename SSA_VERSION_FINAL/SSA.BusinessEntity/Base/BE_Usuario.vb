Public Class BE_Usuario

#Region "Campos"
    Private _id_usuario As Integer
    Private _capellidos As String
    Private _cnombres As String
    Private _ntipo_persona As String
    Private _ndni As String
    Private _nruc As String
    Private _ccorreo As String
    Private _ccorreoalt As String
    Private _ctelefono As String
    Private _cusuario As String
    Private _cclave As String
    Private _nmarca As Integer
    Private _nestado As Integer
    Private _id_perfil As Byte

    Private _des_TipoPersona As String
#End Region

#Region "Propiedades"

    Public Property id_usuario() As Integer
        Get
            Return _id_usuario
        End Get
        Set(ByVal value As Integer)
            _id_usuario = value
        End Set
    End Property

    Public Property capellidos() As String
        Get
            Return _capellidos
        End Get
        Set(ByVal value As String)
            _capellidos = value
        End Set
    End Property

    Public Property cnombres() As String
        Get
            Return _cnombres
        End Get
        Set(ByVal value As String)
            _cnombres = value
        End Set
    End Property

    Public Property ntipo_persona() As String
        Get
            Return _ntipo_persona
        End Get
        Set(ByVal value As String)
            _ntipo_persona = value
        End Set
    End Property

    Public Property nestado() As String
        Get
            Return _nestado
        End Get
        Set(ByVal value As String)
            _nestado = value
        End Set
    End Property

    Public Property ndni() As String
        Get
            Return _ndni
        End Get
        Set(ByVal value As String)
            _ndni = value
        End Set
    End Property


    Public Property nruc() As String
        Get
            Return _nruc
        End Get
        Set(ByVal value As String)
            _nruc = value
        End Set
    End Property

    Public Property ccorreo() As String
        Get
            Return _ccorreo
        End Get
        Set(ByVal value As String)
            _ccorreo = value
        End Set
    End Property

    Public Property ccorreoalt() As String
        Get
            Return _ccorreoalt
        End Get
        Set(ByVal value As String)
            _ccorreoalt = value
        End Set
    End Property

    Public Property ctelefono() As String
        Get
            Return _ctelefono
        End Get
        Set(ByVal value As String)
            _ctelefono = value
        End Set
    End Property

    Public Property cusuario() As String
        Get
            Return _cusuario
        End Get
        Set(ByVal value As String)
            _cusuario = value
        End Set
    End Property

    Public Property cclave() As String
        Get
            Return _cclave
        End Get
        Set(ByVal value As String)
            _cclave = value
        End Set
    End Property

    Public Property nmarca() As String
        Get
            Return _nmarca
        End Get
        Set(ByVal value As String)
            _nmarca = value
        End Set
    End Property

    Public Property nIdPerfil() As Byte
        Get
            Return _id_perfil
        End Get
        Set(ByVal value As Byte)
            _id_perfil = value
        End Set
    End Property



#End Region

#Region " Propiedades Solo Lectura"
    Public ReadOnly Property NombreApellido() As String
        Get
            Return String.Format("{0} {1}", _cnombres, _capellidos)
        End Get
    End Property
#End Region

#Region " Propiedades Auxiliares "
    Public Property des_TipoPersona() As String
        Get
            Return _des_TipoPersona
        End Get
        Set(ByVal value As String)
            _des_TipoPersona = value
        End Set
    End Property
#End Region
End Class
