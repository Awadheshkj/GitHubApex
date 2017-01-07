Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports System.Net

Partial Class Testmail
    Inherits System.Web.UI.Page


    Private Sub Testmail_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' sendMail("sub", "test", "d", "chander@kestone.in", "")
            ' Main()
        End If

    End Sub


    Public Shared Sub Main()

        For i As Integer = 1 To 8
            'Dim remoteImageURLAddress As String = "http://www.panjabdigilib.org/images?ID=15325&page=" & i & "&CategoryID=1&pagetype=null&Searched=W3GX"
            'Dim remoteImageURLAddress As String = "http://www.panjabdigilib.org/images?ID=15439&page=" & i & "&CategoryID=1&pagetype=null&Searched=W3GX"
            Dim remoteImageURLAddress As String = "http://www.panjabdigilib.org/images?ID=15446&page=" & i & "&CategoryID=1&pagetype=null&Searched=W3GX"
            'Dim localFileName As String = "C:\Users\Chander\Desktop\srinarainhariUpdeah\" & i & ".png"
            'Dim localFileName As String = "C:\Users\Chander\Desktop\sirhand_saka\" & i & ".png"
            Dim localFileName As String = "C:\Users\Chander\Desktop\Books\mata_sulakhani\" & i & ".png"
            Using webClient As New WebClient()
                webClient.DownloadFile(remoteImageURLAddress, localFileName)
            End Using
            'ResponseElement..WriteLine("copied image from remote location - " & i)
            'Console.ReadLine()
        Next

    End Sub

    Public Shared Function sendMail(ByVal Subject As String, ByVal mailBody As String, ByVal PlainText As String, ByVal ToAddress As String, ByVal CCAddress As String, Optional ByVal FromAddress As String = "aj@majestikplus.com", Optional ByVal FromName As String = "majestikplus") As Boolean
        Try
            Dim mailOB As New MailMessage

            mailOB.IsBodyHtml = True
            mailOB.From = New MailAddress(FromAddress, FromName)
            mailOB.To.Add(New MailAddress(ToAddress))
            If CCAddress.Trim <> "" Then
                mailOB.CC.Add(New MailAddress(CCAddress))
            End If

            mailOB.Subject = Subject



            'first we create the Plain Text part
            Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString(PlainText, Nothing, "text/plain")

            'then we create the Html part
            'to embed images, we need to use the prefix 'cid' in the img src value
            'the cid value will map to the Content-Id of a Linked resource.
            'thus <img src='cid:companylogo'> will map to a LinkedResource with a ContentId of 'companylogo'
            Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(mailBody, Nothing, "text/html")


            'add the views
            mailOB.AlternateViews.Add(plainView)
            mailOB.AlternateViews.Add(htmlView)

            Dim smtpOB As New System.Net.Mail.SmtpClient
            'smtpOB.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
            'smtpOB.Host = "localhost"        
            smtpOB.Host = "smtp.gmail.com"
            smtpOB.EnableSsl = True
            Dim mailAuthentication As System.Net.NetworkCredential = New System.Net.NetworkCredential("majestikplus1@gmail.com", "AJ@majestik123")
            smtpOB.Credentials = mailAuthentication
            smtpOB.Send(mailOB)
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function
End Class
