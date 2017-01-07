Imports clsDatabaseHelper
Imports clsMain
Imports System.Data

Partial Class forgotpassword
    Inherits System.Web.UI.Page

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim sql As String = ""
        Dim email As String = ""
        Dim pass As String = ""
        Dim epass As String = ""
        Dim epass1 As String = ""
        Dim ds As DataSet
        Dim memID As String = ""

        email = txtUsername.Text
        sql &= "Select username,password from APEX_Login where EmailID='" & email & "'"
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            epass = ds.Tables(0).Rows(0)("password")
            memID = ds.Tables(0).Rows(0)("username")
            epass1 = epass.Remove(0, 1)
            pass = Decrypt(epass1)
            SendEmail(pass, memID, email)

            lblMsg.Text = "Your Password has been successfully send to your Email Address."
            txtUsername.Text = ""
        Else
            lblMsg.Text = "Invalid Email!"
            txtUsername.Text = ""
        End If
        'Else
        'lblPassword.Text = "Your username is incorrect!"
        'End If
    End Sub

    Private Sub SendEmail(ByVal pass As String, ByVal Memid As String, ByVal MemEmail As String)

        Dim Subject As String = "Forgot Password"
        Dim mailBody As String = ""

        mailBody &= "Dear " & Memid & ",<br /><p>Your Login details is given below:</p><p>Username: " & Memid & "<br />Password : " & pass & "<br /></p><br /><br />Regards, <br />www.kestoneapps.com"

        Dim PlainText As String = ""
        Dim ToAddress As String = MemEmail
        Dim CCAddress As String = ""
        sendMail(Subject, mailBody, PlainText, ToAddress, CCAddress)
    End Sub
End Class
