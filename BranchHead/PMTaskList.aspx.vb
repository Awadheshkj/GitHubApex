Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class PMTaskList
    Inherits System.Web.UI.Page
    Public message As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("mode") = "Claim_Detail" Then
                ' FillClientContactPerson()
                If Len(Request.QueryString("jc")) > 0 Then
                    hdnJobCardID.Value = Request.QueryString("jc")
                    If getapprovalmail() Then
                        divapproval.Visible = False
                        divgrid.Visible = True
                    Else
                        divapproval.Visible = True
                        divgrid.Visible = False
                    End If
                End If
                
            Else
                divapproval.Visible = False
                divgrid.Visible = True
            End If
        End If
    End Sub

    Function getapprovalmail() As Boolean

        Dim sql As String = ""
        sql &= "select IsClientApproval,ClientApprovedDatetime,ApprovedBy,IsClientApproval,ISnull(ApprovalMail,'N')ApprovalMail,ISclientapprovalverified from Apex_jobcard  where jobcardID=" & Request.QueryString("jc") & ""
        sql &= ""
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)("ISclientapprovalverified") = "Y" Then
                Return True
            Else
                Return False
            End If
        End If
    End Function


    Protected Sub btnverify_Click(sender As Object, e As EventArgs) Handles btnverify.Click

        SendForJobCodeApprovedToKAM(clsApex.NotificationType.clientapprovalpendinfromPMToKAM)

        divapproval.Visible = True
        divgrid.Visible = False
        divapproval.InnerHtml = "Notification sent successfully."
    End Sub

    Public Sub SendForJobCodeApprovedToKAM(ByVal type As String)
        Try
            Dim kam As String = ""
            If hdnBriefID.Value = "" Then
                Dim capex As New clsApex
                hdnBriefID.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)
            End If
            Dim sql_bid As String = "select InsertedBy from APEX_Brief  where BriefID=" & hdnBriefID.Value
            Dim ds_bid As New DataSet
            ds_bid = ExecuteDataSet(sql_bid)
            If ds_bid.Tables(0).Rows.Count > 0 Then
                kam = ds_bid.Tables(0).Rows(0)(0).ToString()
            End If
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
            sql &= "('Client Approval Pending'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Client Approval Pending for " & getProjectName() & "'"
            sql &= ",'H'"
            sql &= "," & type
            sql &= "," & hdnJobCardID.Value()
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then

                InsertRecieptentDetailsKAM(type, kam)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub InsertRecieptentDetailsKAM(ByVal type As String, ByRef kam As String)
        Try
            Dim notificationid As String = ""
            Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & type & " and AssociateID=" & hdnJobCardID.Value
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


            Dim bheadid As String = ""
            Dim sql3 As String = " "
            Dim leadid As String = ""


            Dim clsApex As New clsApex
            hdnBriefID.Value = clsApex.GetBriefIDByJobCardID(hdnJobCardID.Value)
            leadid = clsApex.GetLeadIDByBriefID(hdnBriefID.Value)


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
            sql1 &= "," & kam
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",NULL"
            sql1 &= ",NULL"
            sql1 &= "," & getLoggedUserID() & ")"

            If ExecuteNonQuery(sql1) > 0 Then
                Dim emailid As String = ""
                If kam <> "" Then
                    Dim uid As Integer = Convert.ToInt32(kam)
                    emailid = GetEmailIDByUserID(uid)
                End If

                If emailid <> "" Then
                    sendMail(title, message, "", emailid, "")
                End If


            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try

    End Sub


    Private Function getProjectName() As String
        Dim bid As String = ""
        Dim result As String = ""
        Try
            Dim capex As New clsApex
            bid = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)

            Dim sql As String = "Select BriefName from APEX_Brief where BriefID=" & bid
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
        Dim bid As String = ""
        Dim result As String = ""
        Try
            Dim capex As New clsApex
            bid = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)

            Dim sql As String = "select isnull(FirstName,'') + ' ' +isnull(LastName,'') as KAMName "
            sql &= " from APEX_UsersDetails as ud "
            sql &= " Inner join APEX_Brief as ab on ud.UserDetailsID = ab.InsertedBy "
            sql &= " where ab.BriefID = " & bid
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

End Class
