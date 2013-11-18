Imports System
Public Class BE_Transaccional
#Region "Campos"
    Private _NIdTransaccion As Integer
    Private _IdUsuario As Integer
    Private _DFecha As DateTime
    Private _CTabla As Integer
    Private _CMovimiento As Integer
    Private _IdReferencia As Integer
#End Region


#Region "Propiedades"
    Public Property NIdTransaccion() As Integer
        Get
            Return _NIdTransaccion
        End Get
        Set(ByVal value As Integer)
            _NIdTransaccion = value
        End Set
    End Property

    Public Property IdUsuario() As Integer
        Get
            Return _IdUsuario
        End Get
        Set(ByVal value As Integer)
            _IdUsuario = value
        End Set
    End Property

    Public Property DFecha() As DateTime
        Get
            Return _DFecha
        End Get
        Set(ByVal value As DateTime)
            _DFecha = value
        End Set
    End Property

    Public Property CTabla() As Integer
        Get
            Return _CTabla
        End Get
        Set(ByVal value As Integer)
            _CTabla = value
        End Set
    End Property

    Public Property CMovimiento() As Integer
        Get
            Return _CMovimiento
        End Get
        Set(ByVal value As Integer)
            _CMovimiento = value
        End Set
    End Property

    Public Property IdReferencia() As Integer
        Get
            Return _IdReferencia
        End Get
        Set(ByVal value As Integer)
            _IdReferencia = value
        End Set
    End Property
#End Region
End Class
