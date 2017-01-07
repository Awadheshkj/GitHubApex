Imports clsMain
Imports clsApex
Imports clsDatabaseHelper


Partial Class _Default
    Inherits System.Web.UI.Page
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Authenticate()
    End Sub

    Private Sub Authenticate()
        Trace.Warn("Authenticating")
        Try
            Dim capex As New clsApex
            If Trim(txtUsername.Text) = "" And Trim(txtPassword.Text) = "" Then
                'lblMsg.Text = "Enter Username/Password"
            Else
                Dim UNM, PWD As String
                UNM = txtUsername.Text
                PWD = txtPassword.Text
                Select Case requestAccess(UNM, PWD)
                    Case _ReturnCodes.AccessDenied
                        lblMsg.Text = "Access Denied"
                    Case _ReturnCodes.AccountInActive
                        lblMsg.Text = "Account InActive"
                    Case _ReturnCodes.LoginNotAllowed
                        lblMsg.Text = "Login Now Allowed"
                    Case _ReturnCodes.InValidPassword
                        lblMsg.Text = "Invalid Credentials"
                    Case _ReturnCodes.InValidUsername
                        lblMsg.Text = "Invalid Credentials"
                    Case _ReturnCodes.AccessGranted
                        Response.Redirect("Dashboard.aspx")
                        
                End Select
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
            LogError(ex)

        End Try
    End Sub
End Class
