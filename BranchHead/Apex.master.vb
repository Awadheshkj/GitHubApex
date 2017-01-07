Imports clsDatabaseHelper
Imports clsMain
Imports System.Data

Partial Class Apex
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        Try
            If Not Page.IsPostBack Then
                Dim cookies As HttpCookie = Request.Cookies.Get("DASTC")
                If Len(Request.QueryString("jc")) > 0 Then
                    If Request.QueryString("jc") <> Nothing Then
                        Response.Cookies("jcID").Value = Request.QueryString("jc")
                    End If
                    If Request.QueryString("jno") <> Nothing Then
                        lblJobCard.Text = Request.QueryString("jno")
                    End If
                End If

                FillRepeater()

                Dim capex As New clsApex

                If isLoggedIn() Then
                    'cokies for pm 

                    'HiddenField1.Value = DivRemaining.InnerHtml
                    'end cokies for pm 

                    lblDesg.Text = getLoggedUserName()
                    lblDesg1.Text = lblDesg.Text
                    'Dim capex As New clsApex
                    Dim role As String
                    role = capex.GetRoleName(lblDesg.Text)
                    If role = "O" Then
                        'Leftmenu.Visible = False
                        Leftmenu.Attributes.CssStyle.Add("Display", "Block")
                        'Content.Attributes.CssStyle.Add("width", "86%")
                        mnuReports.Visible = False
                        If Len(Request.QueryString("jc")) > 0 Then
                            If Request.QueryString("jc") <> Nothing Then
                                'Response.Cookies("jcID").Value = Request.QueryString("jid")

                                'Jcnum
                                Dim jcnumber As String = capex.GetJobCardByjcID(Request.QueryString("jc"))
                                'Response.Cookies("jcID").Value = Request.QueryString("jc")
                                Response.Cookies("Jcnum").Value = jcnumber
                                lblJobCard.Text = Response.Cookies("Jcnum").Value

                            End If
                        End If

                    Else

                        'Leftmenu.Visible = False
                        Leftmenu.Attributes.CssStyle.Add("Display", "none")
                        'Content.Attributes.CssStyle.Add("width", "100%")

                        mnuReports.Visible = True
                    End If
                    If role = "A" Or role = "H" Then
                        mnuLeads.Visible = True
                        mnuJobs.Visible = True
                        'mnuDatabase.Visible = True
                        'mnuCollateral.Visible = True
                        mnuClients.Visible = True
                        mnuSettings.Visible = True
                        ''divuserguide.Visible = False
                        ''divuserguide.HRef = "Userguide/Feedback.pdf"
                    ElseIf role = "P" Then
                        mnuLeads.Visible = False
                        mnuJobs.Visible = True
                        'mnuDatabase.Visible = False
                        'mnuCollateral.Visible = True
                        mnuClients.Visible = False
                        mnuSettings.Visible = False
                        'divuserguide.HRef = "Userguide/PM.pdf"
                    ElseIf role = "O" Or role = "F" Or role = "C" Then
                        mnuLeads.Visible = False
                        mnuJobs.Visible = False
                        'mnuDatabase.Visible = False
                        'mnuCollateral.Visible = False
                        mnuClients.Visible = False
                        mnuSettings.Visible = False
                        'divuserguide.HRef = "Userguide/PM.pdf"
                    ElseIf role = "B" Then
                        mnuLeads.Visible = True
                        mnuJobs.Visible = True
                        'mnuDatabase.Visible = False
                        'mnuCollateral.Visible = True
                        mnuClients.Visible = False
                        mnuSettings.Visible = False
                        ''dropprepnlandpostpnl.Visible = False
                        'divuserguide.HRef = "Userguide/KAM.pdf"
                    ElseIf role = "R" Then
                        mnuLeads.Visible = False
                        mnuJobs.Visible = False
                        'divDatabase.Visible = False
                        'divCollateral.Visible = False
                        mnuClients.Visible = False
                        mnuSettings.Visible = False
                        'divEmployees.Visible = False
                        'divreports.Visible = False
                        'Notification1.Visible = False
                        'divuserguide.HRef = "Userguide/Feedback.pdf"
                    End If
                Else
                    If Not cookies Is Nothing Then
                        If cookies.Value <> "" Then
                            lblDesg.Text = getLoggedUserName()
                            lblDesg1.Text = lblDesg.Text
                            'Dim capex As New clsApex
                            Dim role As String
                            role = capex.GetRoleName(lblDesg.Text)
                            If role = "O" Then
                                Leftmenu.Attributes.CssStyle.Add("Display", "Block")
                                'Content.Attributes.CssStyle.Add("width", "86%")
                                mnuReports.Visible = False
                                If Len(Request.QueryString("jc")) > 0 Then
                                    If Request.QueryString("jc") <> Nothing Then
                                        Dim jcnumber As String = capex.GetJobCardByjcID(Request.QueryString("jc"))
                                        'Response.Cookies("jcID").Value = Request.QueryString("jc")
                                        Response.Cookies("Jcnum").Value = jcnumber
                                        lblJobCard.Text = Response.Cookies("Jcnum").Value

                                    End If
                                End If
                            Else
                                'Leftmenu.Visible = False
                                Leftmenu.Attributes.CssStyle.Add("Display", "none")
                                'Content.Attributes.CssStyle.Add("width", "100%")
                                mnuReports.Visible = True
                            End If
                            If role = "A" Or role = "H" Then
                                mnuLeads.Visible = True
                                mnuJobs.Visible = True
                                'divEmployees.Visible = True
                                'divDatabase.Visible = True
                                'divCollateral.Visible = True
                                mnuClients.Visible = True
                                mnuSettings.Visible = True
                                '       divuserguide.Visible = False
                            ElseIf role = "P" Then
                                mnuLeads.Visible = False
                                mnuJobs.Visible = True
                                'divEmployees.Visible = False
                                'divDatabase.Visible = False
                                'divCollateral.Visible = False
                                mnuClients.Visible = False
                                mnuSettings.Visible = False
                                '      divuserguide.HRef = "Userguide/PM.pdf"
                            ElseIf role = "O" Or role = "F" Or role = "C" Then
                                mnuLeads.Visible = False
                                mnuJobs.Visible = False
                                'divEmployees.Visible = False
                                'divDatabase.Visible = False
                                'divCollateral.Visible = False
                                mnuClients.Visible = False
                                mnuSettings.Visible = False
                                '     divuserguide.HRef = "Userguide/PM.pdf"
                            ElseIf role = "B" Then
                                mnuLeads.Visible = True
                                mnuJobs.Visible = True
                                'divDatabase.Visible = False
                                'divCollateral.Visible = True
                                mnuClients.Visible = False
                                mnuSettings.Visible = False
                                'divEmployees.Visible = False
                                'dropprepnlandpostpnl.Visible = False
                                '    divuserguide.HRef = "Userguide/KAM.pdf"
                            ElseIf role = "R" Then
                                mnuLeads.Visible = False
                                mnuJobs.Visible = False
                                'divDatabase.Visible = False
                                'divCollateral.Visible = False
                                mnuClients.Visible = False
                                mnuSettings.Visible = False
                                'divEmployees.Visible = False
                                'divreports.Visible = False
                                'Notification1.Visible = False
                                '   divuserguide.HRef = "Userguide/Feedback.pdf"
                            End If
                        Else
                            Response.Redirect("Default.aspx")
                        End If
                    Else
                        Response.Redirect("Default.aspx")
                    End If
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Function isLoggedIn() As Boolean
        Try
            If Len(Session("UserID")) > 0 Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            'SendExMail(ex.Message, ex.ToString())
        End Try
        Return False

    End Function

    Public Shared Function Dateformate(ByVal dt As String) As String
        ' Entering("FormatDate with parameter dt: " & dt)
        Dim retSTR As String = ""
        If Not IsDBNull(dt) Then
            If IsDate(dt) Then
                'retSTR = MonthName(Month(dt), True) & " " & Day(dt) & ", " & Year(dt)
                Dim d As DateTime = dt
                retSTR = d.ToString("dd-MM-yyyy") & "  " & FormatDateTime(dt, DateFormat.ShortTime)
            End If
        End If

        ' Leaving("FormatDate: Returning " & retSTR)
        Return retSTR
    End Function

    Private Sub FillRepeater()
        Dim sql As String = "Select top 5 NotificationTitle,NotificationMessage, "
        sql &= " Link = case when type = 4 then 'Estimate.aspx?mode=app&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) +'&Appro=Y'  "
        sql &= " when type = 6 then 'RembursementClaimApproval.aspx?jc=' + convert(varchar(50),(select top 1 jobcardno from Apex_claimMaster where claimmasterID=AssociateID)) + '&type=SubTask&nid=' + convert(varchar(10),NotificationID)+ '&aid=' + convert(varchar(10),AssociateID)  "
        sql &= " when type = 5 then 'RembursementClaimApproval.aspx?jc=' + convert(varchar(50),(select top 1 jobcardno from Apex_claimMaster where claimmasterID=AssociateID)) + '&type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID)  "
        sql &= " when type = 7 then 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 15 then 'Estimate.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)+'&Approved=Y'  "
        sql &= " when type = 8 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 9 then 'Estimate.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        sql &= " when type = 10 then 'rejectednotification.aspx?bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 11 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 13 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 14 then 'TaskAccount.aspx?jc=' + convert(varchar(10),AssociateID) + '&tid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 12 then 'Checklist.aspx?tid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 16 then 'RembursementClaimApprovalView.aspx?jc=' + convert(varchar(50),(select top 1 jobcardno from Apex_claimMaster where claimmasterID=AssociateID)) + '&type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID) + '&mode=app'"
        sql &= " when type = 17 then 'RembursementClaimApproval.aspx?jc=' + convert(varchar(50),(select top 1 jobcardno from Apex_claimMaster where claimmasterID=AssociateID)) + '&type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID) + '&mode=rej' "
        sql &= " when type = 18 then 'ExcPrePnlManager.aspx?mode=edit&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID) "
        sql &= " when type = 19 then 'RembursementClaimApproval.aspx?jc='+ convert(varchar(50),(select top 1 jobcardno from Apex_claimMaster where claimmasterID=AssociateID)) +'&type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID) "
        sql &= " when type = 20 then 'viewJobInvoice.aspx?jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 22 then 'PostPnLManager.aspx?mode=view&jid=' + convert(varchar(10),AssociateID) + '&jc=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 21 then 'PostPnLManager.aspx?jid=' + convert(varchar(10),AssociateID) + '&jc=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)"
        sql &= " when type = 23 then 'PrePnlApproval.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + ' &kid=y&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 24 then 'OpenPrePnLManager.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + ' &kid=y&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 25 then 'ListOfTask.aspx?jc=' + convert(varchar(10),AssociateID) + '&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 26 then 'Estimate_VS_Actuals.aspx?jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 27 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 28 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 29 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 31 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 30 then 'PMTasklist.aspx?jc=' + convert(varchar(10),AssociateID) + '&jid=' + convert(varchar(10),AssociateID) + '&mode=Claim_Detail&nid=' + convert(varchar(10),NotificationID) end"

        sql &= " ,rp.InsertedOn from APEX_Notifications as nt left outer join APEX_Recieptents as rp on nt.NotificationID = rp.RefNotificationID where rp.UserID=" & getLoggedUserID() & " and IsExecuted <> 'Y' and  IsSeen<>'Y'  Order By rp.InsertedOn desc"
        ' and IsExecuted <> 'Y' and  IsSeen<>'Y'
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            rptNotification.DataSource = ds
            rptNotification.DataBind()
        End If
    End Sub

    Protected Sub lnkLogout_Click(sender As Object, e As EventArgs) Handles lnkLogout.Click
        Try
            clsMain.LogOut()
            Response.Redirect("../Default.aspx?Mode=LOGOUT")
        Catch ex As Exception
            'SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class

