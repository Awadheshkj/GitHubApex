Imports clsDatabaseHelper
Imports clsMain
Imports clsApex
Imports System.Data

Partial Class RptOverallProject
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Dim capex As New clsApex

            Dim cookies As HttpCookie = Request.Cookies.Get("DASTC")
            If clsMain.LoggedIn() Then
                Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
                ' role = "PM"

                Dim mode As String = ""
                If Len(Request.QueryString("mode")) > 0 Then
                    mode = Request.QueryString("mode")
                End If

                If role = "F" Then
                    PnlPM.Visible = False
                    PnlKAM.Visible = False
                ElseIf role = "PM" Then
                ElseIf role = "K" Or role = "A" Or role = "H" Then
                    pnlFM.Visible = False
                    PnlPM.Visible = False
                    PnlKAM.Visible = True
                    'bindtasks1()
                ElseIf role = "C" Then
                    pnlFM.Visible = False
                    PnlPM.Visible = False
                    PnlKAM.Visible = False
                ElseIf role = "R" Then
                    pnlFM.Visible = False
                    PnlPM.Visible = False
                    PnlKAM.Visible = False
                Else
                    Response.Cookies("userName").Value = getLoggedUserName()
                    Response.Redirect("HighchartsWeb/Default.aspx")
                    pnlFM.Visible = False
                    PnlPM.Visible = True
                    PnlKAM.Visible = False
                    If mode <> "" Then
                        If mode = "team" Then
                        ElseIf mode = "task" Then
                        ElseIf mode = "PostPM" Then
                        Else
                        End If
                    Else
                    End If
                End If

            Else
                Response.Redirect("Default.aspx")
            End If
        End If
    End Sub



End Class
