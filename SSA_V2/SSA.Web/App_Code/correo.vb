Imports System.Net.Mail
Imports System.IO
Imports System.Collections.Generic

Public NotInheritable Class correo

#Region " Parámetros de Correo "
    Private _Body As StringBuilder
    Public Shared ReadOnly Property CredencialCorreo() As String
        Get
            Return MyConfig.getCredencialCorreo
        End Get
    End Property
    Public Shared ReadOnly Property CredencialNombre() As String
        Get
            Return MyConfig.getCredencialNombre
        End Get
    End Property
    Public Property Body() As StringBuilder
        Get
            Return _Body
        End Get
        Set(ByVal value As StringBuilder)
            _Body = value
        End Set
    End Property
#End Region

#Region " Enviar Correo"
    Public Sub Enviar(ByVal Para As String, ByVal ConCopia As String, ByVal Asunto As String)
        Dim Mail As New System.Net.Mail.MailMessage() ' MailMessage
        Dim Smtp As New System.Net.Mail.SmtpClient() ' SmtpClient

        Mail.From = New System.Net.Mail.MailAddress(CredencialCorreo, CredencialNombre)
        Mail.To.Add(Para)

        If Not String.IsNullOrEmpty(ConCopia) Then
            Mail.CC.Add(ConCopia)
        End If

        Mail.Subject = Asunto
        Mail.Body = _Body.ToString
        Mail.BodyEncoding = Encoding.GetEncoding("iso-8859-1")
        Mail.IsBodyHtml = True
        Mail.Priority = MailPriority.High

        'If Adjuntos IsNot Nothing Then
        '    If Adjuntos.Count > 0 Then
        '        For Each Adjunto As String In Adjuntos
        '            If File.Exists(Adjunto) Then
        '                Mail.Attachments.Add(New Attachment(Adjunto))
        '            End If
        '        Next
        '    End If
        'End If
        Smtp.Send(Mail)

    End Sub
#End Region

End Class
