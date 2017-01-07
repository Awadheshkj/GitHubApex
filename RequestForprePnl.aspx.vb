Imports clsApex
Imports clsDatabaseHelper
Imports clsMain
Imports System.Data
Partial Class RequestForprePnl
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            divError.Visible = False
            MessageDiv.Visible = False

            If Not Page.IsPostBack Then
                btnCancel.Visible = False
                If Len(Request.QueryString("bid")) > 0 Then
                    hdnBrefID.Value = Request.QueryString("bid")
                    Dim projectnamaget As String = checkProjectMang(hdnBrefID.Value)

                    If projectnamaget = "" Then
                        FillCampaignManager()
                        maindiv.Visible = True

                    Else
                        Label1.Text = "Request sent to " & projectnamaget & " to fill Pre P&L"
                        MessageDiv.Visible = True
                        maindiv.Visible = False
                        btnCancel.Visible = True
                    End If
                    lblmsg.Text = ""


                Else
                    CallDivError()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub CallDivError()
        Try
            divContent.Visible = False
            divError.Visible = True
            Dim capex As New clsApex
            lblError.Text = capex.SetErrorMessage()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillCampaignManager()
        Try
            Dim sql As String = "Select UserDetailsID,(isnull(firstName,'')+' '+isnull(LastName,'')) as Name from APEX_UsersDetails where IsActive='Y' and IsDeleted='N' and Role in('O') order by FirstName"
            'Role not in('A','K','F','H')
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then

                ddlProjectManager.DataSource = ds
                ddlProjectManager.DataTextField = "Name"
                ddlProjectManager.DataValueField = "UserDetailsID"
                ddlProjectManager.DataBind()
            End If
            ddlProjectManager.Items.Insert(0, New ListItem("Select", "0"))
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnSendRequert_Click(sender As Object, e As EventArgs) Handles btnSendRequert.Click
        Try
            Dim sql As String = ""
            AssianProjectManager(hdnBrefID.Value)
            SendForApprovalBranchHead()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Public Sub SendForApprovalBranchHead()
        Try
            Dim sql As String = "INSERT INTO APEX_Notifications"
            sql &= "(NotificationTitle"
            sql &= ",NotificationMessage"
            sql &= ",Severity"
            sql &= ",Type"
            sql &= ",AssociateID"
            sql &= ",StartDate"
            sql &= ",EndDate"
            sql &= ",SendSMS"
            sql &= ",SendEmail)"
            sql &= "VALUES"
            sql &= "('Request for Pre P&L'"
            sql &= ",'<b>Project Name: </b>" & Clean(getProjectName()) & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Request for Pre P&L'"
            sql &= ",'H'"
            sql &= "," & NotificationType.PPNLA
            sql &= "," & hdnBrefID.Value
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails()
                Dim projectnamaget As String = checkProjectMang(hdnBrefID.Value)
                Label1.Text = "Request sent to " & projectnamaget & " to fill Pre P&L"
                MessageDiv.Visible = True
                maindiv.Visible = False

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function IsnotificationSend() As Boolean
        Dim flag As Boolean = False
        Try
            Dim sql As String = ""
            sql = ""
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return flag
    End Function

    Private Sub InsertRecieptentDetails()
        Try
            Dim title As String = ""
            Dim message As String = ""
            Dim notificationid As String = ""
            Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & NotificationType.PPNLA & " and AssociateID=" & hdnBrefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationID")) Then
                    notificationid = ds.Tables(0).Rows(0)("NotificationID").ToString()
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationTitle")) Then
                    title = ds.Tables(0).Rows(0)("NotificationTitle").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationMessage")) Then
                    message = ds.Tables(0).Rows(0)("NotificationMessage").ToString()
                End If
            End If


            Dim sql1 As String = "INSERT INTO APEX_Recieptents"
            sql1 &= " (RefNotificationID"
            sql1 &= ",UserID"
            sql1 &= ",IsSeen"
            sql1 &= ",IsHidden"
            sql1 &= ",IsExecuted"
            sql1 &= ",SMSSentOn"
            sql1 &= ",EmailSentOn"
            sql1 &= ",InsertedBy)"
            sql1 &= "VALUES"
            sql1 &= "(" & notificationid
            sql1 &= "," & Clean(ddlProjectManager.SelectedValue)
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",NULL"
            sql1 &= ",NULL"
            sql1 &= "," & getLoggedUserID() & ")"

            If ExecuteNonQuery(sql1) > 0 Then
                Dim emailid As String = ""
                If ddlProjectManager.SelectedValue > 0 Then
                    emailid = GetEmailIDByUserID(ddlProjectManager.SelectedValue)
                End If

                If emailid <> "" Then
                    sendMail(title, message, "", emailid, "")
                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub AssianProjectManager(p1 As String)
        Try
            Dim sql As String = ""
            If p1 <> "" Then

                sql = "Update APEX_JobCard set ProjectManagerID = " & ddlProjectManager.SelectedValue & " where RefBriefID=" & p1
                ExecuteNonQuery(sql)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function checkProjectMang(ByVal bid As String) As String
        Dim sql As String = ""
        Dim Username As String = ""
        Try
            sql = "Select jc.ProjectmanagerID,(isnull(ud.firstname,'')+' '+isnull(ud.lastname,'')) as uname from APEX_JobCard as jc "
            sql &= " join dbo.APEX_UsersDetails as ud on jc.ProjectmanagerID=ud.UserDetailsID"
            sql &= " where jc.RefBriefID=" & bid & " and jc.IsActive='Y'"

            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("uname")) Then
                    Username = ds.Tables(0).Rows(0)("uname").ToString()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return Username
    End Function

    Private Function getProjectName() As String
        Dim result As String = ""
        Try
            Dim sql As String = "Select BriefName from APEX_Brief where BriefID=" & hdnBrefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = ds.Tables(0).Rows(0)("BriefName").ToString()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Private Function getKAMName() As String
        Dim result As String = ""
        Try
            Dim sql As String = "select isnull(FirstName,'') + isnull(LastName,'') as KAMName "
            sql &= " from APEX_UsersDetails as ud "
            sql &= " Inner join APEX_Brief as ab on ud.UserDetailsID = ab.InsertedBy "
            sql &= " where ab.BriefID = " & hdnBrefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = ds.Tables(0).Rows(0)("KAMName").ToString()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("Home.aspx")
    End Sub
End Class