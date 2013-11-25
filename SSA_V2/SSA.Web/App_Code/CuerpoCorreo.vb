
Imports System
Imports System.Text
Imports System.IO
Imports QNET.Common

Public Class CuerpoCorreo
    Private Shared lockCorreoOlvidoClave As New Object()
    Public Shared Function CuerpoOlvidoClave(ByVal clave As String) As StringBuilder
        SyncLock lockCorreoOlvidoClave
            Dim builder As New StringBuilder()

            Using lectorTexto As New System.IO.StreamReader(MyConfig.getPlantillaOlvidoClave)
                Dim linea As String = lectorTexto.ReadLine()
                While linea IsNot Nothing
                    If linea.Contains("{clave}") Then
                        builder.Append(linea.Replace("{clave}", clave))
                    Else
                        builder.Append(linea)
                    End If
                    linea = lectorTexto.ReadLine()
                End While
            End Using

            Return builder
        End SyncLock
    End Function

    Private Shared lockCorreoAdjudicacion As New Object()
    Public Shared Function CuerpoAdjudicacion(ByVal usuario As String, ByVal cuerpo As String) As StringBuilder
        SyncLock lockCorreoAdjudicacion
            Dim builder As New StringBuilder()

            Using lectorTexto As New System.IO.StreamReader(MyConfig.getPlantillaAdjudicacion)
                Dim linea As String = lectorTexto.ReadLine()
                While linea IsNot Nothing
                    If linea.Contains("{usuario}") Then
                        builder.Append(linea.Replace("{usuario}", usuario))
                    ElseIf linea.Contains("{cuerpo}") Then
                        builder.Append(linea.Replace("{cuerpo}", cuerpo))
                    Else
                        builder.Append(linea)
                    End If
                    linea = lectorTexto.ReadLine()
                End While
            End Using

            Return builder
        End SyncLock
    End Function
End Class
