Imports clsDatabaseHelper
Imports clsMain

Partial Class ChangePassword
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            divMsgAlert.Visible = False

        End If
    End Sub

    Protected Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        Try
            Dim LoggedUser As String = getLoggedUserID()
            Dim username As String = getLoggedUserName()
            Dim sql As String = "update APEX_Login set Password='" & Clean(txtConfirmPassword.Text) & "' where username='" & username & "'"
            If ExecuteNonQuery(sql) > 0 Then
                divMsgAlert.Visible = True
                divMsgAlert.InnerHtml = "Your password has been changed successfully"
                sendMail("Password Changed", "Dear " & getLoggedUserName() & ",<br /><br /> Your password has been changed successfully.Your new password is '" & Clean(txtConfirmPassword.Text) & "'.<br /><br />Regards, <br />www.kestoneapps.com", "", getLoggedUserEmail(), "awadheshkj@kestone.in")
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
