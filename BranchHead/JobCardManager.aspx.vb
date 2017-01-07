Imports System.Data
Imports clsMain
Imports clsDatabaseHelper
Imports clsApex
Imports System.Globalization
Imports System
Imports System.IO
Imports System.String

Partial Class JobCardManager
    Inherits System.Web.UI.Page
    Public title As String = ""
    Public message As String = ""
    Public messageshow As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            ddlProjectManager.Enabled = False
            If Not Page.IsPostBack() Then
                btnCancelHome.Visible = False
                MessageDiv.Visible = False
                btnJobCardClosure.Visible = False
                divError.Visible = False
                If Len(Request.QueryString("jid")) > 0 Then
                    If Request.QueryString("jid") <> Nothing Then
                        If Request.QueryString("jid").ToString() <> "" Then

                            hdnJobCardID.Value = Request.QueryString("jid")
                            Dim capex As New clsApex
                            If Len(Request.QueryString("nid")) > 0 Then
                                If Request.QueryString("nid") <> Nothing Then
                                    hdnNodificationID.Value = Request.QueryString("nid")
                                    capex.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())
                                End If
                            End If
                            hdnBriefID.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)
                            chkApproval.Checked = True
                            btnJobCardClosure.Visible = False

                            FillPrePnlDetails()
                            bindprepnldetail(hdnBriefID.Value)
                            BindFinalEstimateGrid()
                            BindBriefData(hdnBriefID.Value)

                            FillActivityType()
                            FillCampaignManager()
                            FillClient()

                            FillDetails()
                            'ddlClientName_SelectedIndexChanged(sender, e)
                            Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

                            If role = "K" Then
                                'btnJobCardClosure.Visible = True
                                btnverify.Visible = False
                                btnreject.Visible = False
                            End If

                            Dim jno As String = capex.GetJobCardNoByJobCardID(hdnJobCardID.Value)
                            If jno <> "" Then

                                Dim sql As String = "Select ProjectManagerID from dbo.APEX_JobCard where IsActive='Y' and  jobCardID=" & hdnJobCardID.Value
                                Dim userid As String = ""
                                Dim ds As DataSet = Nothing
                                ds = ExecuteDataSet(sql)
                                If ds.Tables(0).Rows.Count > 0 Then
                                    userid = ds.Tables(0).Rows(0)("ProjectManagerID").ToString()
                                End If
                                'Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

                                If role = "K" Then
                                    btnJobCardClosure.Visible = True
                                    btnverify.Visible = False
                                    btnreject.Visible = False
                                ElseIf role = "H" Then
                                    btnJobCardClosure.Visible = True
                                Else
                                    btnJobCardClosure.Visible = False

                                End If


                            Else

                            End If
                            If Len(Request.QueryString("nid")) > 0 Then
                                hdnNodificationID.Value = Request.QueryString("nid")
                                btnCancel.Visible = True
                            End If

                            If checkforAccountdepartment() = True Then
                                btnAdd.Visible = False
                                btnCancel.Visible = False
                                btnJobCardClosure.Visible = False

                                txtProjectName.Enabled = False
                                txtProjectStartDate.Enabled = False
                                txtProjectEndDate.Enabled = False
                                ddlPrimary.Enabled = False
                                chklActivityType.Enabled = False
                                ddlClientBudgetOwner.Enabled = False
                                ddlContactDetails.Enabled = False
                                txtApprovalDate.Enabled = False
                                ddlApprovedBy.Enabled = False
                                chkApproval.Enabled = False
                                fupl_Mail.Enabled = False
                                'RequiredFieldValidator9.Enabled = False
                                txtJobCode.Enabled = True
                                RfvJobCode.Enabled = True
                                If Len(Request.QueryString("mode")) > 0 Then
                                    If Request.QueryString("mode") <> Nothing Then
                                        If Len(Request.QueryString("nid")) > 0 Then
                                            If Request.QueryString("nid") <> Nothing Then

                                                hdnNodificationID.Value = Request.QueryString("nid")
                                                capex.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())

                                            End If
                                        End If
                                    End If
                                End If
                            Else
                                txtJobCode.Enabled = False
                                RfvJobCode.Enabled = False
                                txtProjectName.Enabled = True
                                txtProjectStartDate.Enabled = True
                                txtProjectEndDate.Enabled = True
                                ddlPrimary.Enabled = True
                                chklActivityType.Enabled = True
                                ddlClientBudgetOwner.Enabled = True
                                ddlContactDetails.Enabled = True
                                txtApprovalDate.Enabled = True
                                ddlApprovedBy.Enabled = True
                                chkApproval.Enabled = True
                                fupl_Mail.Enabled = True
                                If getapprovalmail() Then
                                    btnJobCardClosure.Visible = True
                                Else
                                    btnJobCardClosure.Visible = False
                                End If
                                'RequiredFieldValidator9.Enabled = True
                            End If


                            lblJobCode.Visible = False
                            lblProjectName.Visible = False
                            lblPrimary.Visible = False
                            lblProjectStartDate.Visible = False
                            lblProjectEndDate.Visible = False

                            lblClientName.Visible = False
                            lblClientBudgetOwner.Visible = False
                            lblContactDetails.Visible = False
                            lblApproval.Visible = False
                            lblApprovalDate.Visible = False
                            lblApprovedBy.Visible = False
                            lblActivityType.Visible = False
                            tblContactBudget.Visible = False

                            tblNewClient.Visible = False
                            lblProjectManager.Visible = False


                            FillClientContactPerson()
                            SelectClientContact()
                            FillEstimate()
                            If CheckJobCard() = True Then

                                btnJobCard.Visible = False
                                tblNewClient.Visible = False

                                tblContactBudget.Visible = False

                            Else

                                btnJobCard.Visible = False
                                btnAdd.Visible = False
                                tblContactBudget.Visible = False
                                lblContactDetails.Visible = False
                                If checkforAccountdepartment() = False Then

                                    'CloseAllControls()
                                    btnAdd.Visible = True
                                Else
                                    If Len(Request.QueryString("mode")) > 0 Then

                                        Dim mode As String = Request.QueryString("mode")
                                        If mode = "update" Then
                                            btnAdd.Visible = True
                                            txtJobCode.Enabled = True

                                        Else
                                            CloseAllControls()
                                        End If
                                    End If


                                End If


                            End If
                            If Len(Request.QueryString("ur")) > 0 Then
                                If Request.QueryString("ur") <> Nothing Then
                                    If Request.QueryString("ur").ToString() <> "" Then
                                        btnCancelHome.Visible = True
                                        btnCancel.Visible = False
                                    End If
                                End If
                            End If
                            If postpnlFilled() = True Then
                                btnJobCardClosure.Visible = False
                            End If

                        Else

                            CallDivError()
                        End If
                    Else
                        CallDivError()
                    End If
                    If CheckNotification() = True Then
                        Dim capex1 As New clsApex
                        capex1.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())
                    End If

                Else
                    CallDivError()
                End If

            End If

            If lblfulpl_Mail.PostBackUrl <> "" Then
                ' RequiredFieldValidator9.Visible = False
            Else
                'RequiredFieldValidator9.Visible = True
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Function getapprovalmail() As Boolean

        Dim sql As String = ""
        sql &= "select IsClientApproval,ClientApprovedDatetime,ApprovedBy,IsClientApproval,ISnull(ApprovalMail,'N')ApprovalMail,ISclientapprovalverified from Apex_jobcard  where jobcardID=" & hdnJobCardID.Value & ""
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

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Dim culture As IFormatProvider = New CultureInfo("en-GB", True)

            If CheckJobCardExists() = False Then
                Dim activity As String = ""

                For i As Integer = 0 To chklActivityType.Items.Count - 1
                    If chklActivityType.Items(i).Selected = True Then
                        activity &= chklActivityType.Items(i).Text & "|"
                    End If

                Next

                If activity.Length = 0 Then
                    activity = "NULL"
                Else
                    activity = activity.Substring(0, activity.Length - 1)
                End If

              

                If Convert.ToDateTime(txtProjectStartDate.Text, culture) < Convert.ToDateTime(txtProjectEndDate.Text, culture) Then
                    Dim sql As String = "UPDATE APEX_JobCard"
                    sql &= " SET JobCardName = '" & Clean(txtProjectName.Text) & "'"
                    If txtJobCode.Text <> "" Then
                        sql &= " ,JobCardNo = '" & Clean(txtJobCode.Text) & "'"
                    End If

                    sql &= ",PrimaryActivityID =" & ddlPrimary.SelectedValue
                    If activity <> "" Then
                        sql &= ",RefActivityID = '" & activity & "'"
                    Else
                        sql &= ",RefActivityID=NULL"
                    End If

                    sql &= ",ProjectManagerID = " & ddlProjectManager.SelectedValue

                    sql &= ",ActivityStartDate = convert(datetime,'" & txtProjectStartDate.Text & "',105)"
                    sql &= ",ActivityEndDate = convert(datetime,'" & txtProjectEndDate.Text & "',105)"
                    '  sql &= ",KAMID = " & ddlKAMName.SelectedValue
                    sql &= ",RefClientID = " & ddlClientName.SelectedValue
                    sql &= ",ClientBudgetOwner = " & ddlClientBudgetOwner.SelectedValue
                    sql &= ",ClientContactPerson = " & ddlContactDetails.SelectedValue
                    If chkApproval.Checked Then
                        sql &= ",IsClientApproval = 'Y'"
                    Else
                        sql &= ",IsClientApproval = 'N'"
                    End If
                    If txtApprovalDate.Text <> "" Then
                        sql &= ",ClientApprovedDatetime = convert(datetime,'" & txtApprovalDate.Text & "',105)"
                    Else
                        sql &= ",ClientApprovedDatetime = NULL"
                    End If
                    If ddlApprovedBy.SelectedIndex > 0 Then
                        sql &= ",ApprovedBy = " & ddlApprovedBy.SelectedValue
                    Else
                        sql &= ",ApprovedBy = NULL"
                    End If

                    If fupl_Mail.HasFile = True Then
                        Dim filename As String = fupl_Mail.FileName
                        Dim fname As String = filename.Substring(0, filename.LastIndexOf("."))
                        Dim fext As String = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - (filename.LastIndexOf(".") + 1))
                        Dim encname As String = ""
                        'txtUploads.Text = fname
                        Dim Path As String
                        encname = fname & DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date.Day & DateTime.Now.Date.Hour & DateTime.Now.Date.Minute & DateTime.Now.Date.Second
                        fupl_Mail.SaveAs(Server.MapPath("Collateral/Jobcard/" & Clean(encname.ToString().Replace("&", "")) & "." & fext))
                        Path = "Collateral/Jobcard/" & Clean(encname.ToString().Replace("&", "")) & "." & fext
                        sql &= ",ApprovalMail='" & Path & "'"
                        SendForclientApprovedToFM(clsApex.NotificationType.clientapprovalpendinfromKAMToFM)

                    End If
                    sql &= ",ModifiedOn = getdate()"
                    sql &= ",ModifiedBy = " & getLoggedUserID()
                    sql &= "WHERE JobCardID=" & hdnJobCardID.Value

                    If ExecuteNonQuery(sql) > 0 Then
                        'InsertTask()
                        Dim capex As New clsApex

                        If txtJobCode.Text <> "" Then
                            If Len(Request.QueryString("mode")) > 0 Then

                                If Request.QueryString("mode") <> "update" Then
                                    SendForJobCodeApprovedToKAM(clsApex.NotificationType.viewJCKM)
                                    SendForJobCodeApprovedToPM(clsApex.NotificationType.viewJKPM)
                                    capex.UpdateStageLevel("6", hdnJobCardID.Value)
                                    If hdnNodificationID.Value <> "" Then
                                        capex.UpdateFMRecieptentExecuted(hdnNodificationID.Value)
                                        updateJobCardFM(getLoggedUserID())
                                    End If
                                End If

                            End If
                        Else
                            If NotificationSent(clsApex.NotificationType.JCInserted, hdnJobCardID.Value) = False Then
                                SendForJobCodeApprovedToFM(clsApex.NotificationType.JCInserted)
                            End If
                        End If
                        'lblError.Text = "Job Code requested to finance Team"
                        Label1.Text = "Job Code requested to finance team"

                        FillDetails()
                        MessageDiv.Visible = True
                        divContent.Visible = False
                        Dim jobcode As String = ""
                        Dim cpex As New clsApex
                        If hdnJobCardID.Value <> "" Then
                            jobcode = capex.GetJobCardNoByJobCardID(hdnJobCardID.Value)
                            If jobcode <> "" Then
                                'lblError.Text = "Job Code assigned successfully"
                                Label1.Text = "Job Code assigned successfully"
                                MessageDiv.Visible = True
                                lblJobCode.Visible = True
                                divContent.Visible = False
                            End If
                        End If

                        divError.Visible = False
                        btnAdd.Visible = False
                        CloseAllControls()
                    End If
                Else
                    lblError.Text = "Project End Date should be greater than Project Start Date"
                    divError.Visible = True
                End If
            Else
                Dim capex As New clsApex
                Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

                If role = "F" Then
                    lblError.Text = "JobCode already exists"
                    divError.Visible = True
                    MessageDiv.Visible = False
                    messageshow = "hide"
                    Exit Sub
                Else
                    messageshow = "Show"
                End If
                

                Dim activity As String = ""

                For i As Integer = 0 To chklActivityType.Items.Count - 1
                    If chklActivityType.Items(i).Selected = True Then
                        activity &= chklActivityType.Items(i).Text & "|"
                    End If

                Next

                If activity.Length = 0 Then
                    activity = "NULL"
                Else
                    activity = activity.Substring(0, activity.Length - 1)
                End If
                If Convert.ToDateTime(txtProjectStartDate.Text, culture) < Convert.ToDateTime(txtProjectEndDate.Text, culture) Then
                    Dim sql As String = "UPDATE APEX_JobCard"
                    sql &= " SET JobCardName = '" & Clean(txtProjectName.Text) & "'"
                    sql &= ",PrimaryActivityID =" & ddlPrimary.SelectedValue
                    If activity <> "" Then
                        sql &= ",RefActivityID = '" & activity & "'"
                    Else
                        sql &= ",RefActivityID=NULL"
                    End If

                    sql &= ",ProjectManagerID = " & ddlProjectManager.SelectedValue

                    sql &= ",ActivityStartDate = convert(datetime,'" & txtProjectStartDate.Text & "',105)"
                    sql &= ",ActivityEndDate = convert(datetime,'" & txtProjectEndDate.Text & "',105)"
                    '  sql &= ",KAMID = " & ddlKAMName.SelectedValue
                    sql &= ",RefClientID = " & ddlClientName.SelectedValue
                    sql &= ",ClientBudgetOwner = " & ddlClientBudgetOwner.SelectedValue
                    sql &= ",ClientContactPerson = " & ddlContactDetails.SelectedValue
                    If chkApproval.Checked Then
                        sql &= ",IsClientApproval = 'Y'"
                    Else
                        sql &= ",IsClientApproval = 'N'"
                    End If
                    If txtApprovalDate.Text <> "" Then
                        sql &= ",ClientApprovedDatetime = convert(datetime,'" & txtApprovalDate.Text & "',105)"
                    Else
                        sql &= ",ClientApprovedDatetime = NULL"
                    End If
                    If ddlApprovedBy.SelectedIndex > 0 Then
                        sql &= ",ApprovedBy = " & ddlApprovedBy.SelectedValue
                    Else
                        sql &= ",ApprovedBy = NULL"
                    End If

                    If fupl_Mail.HasFile = True Then
                        Dim filename As String = fupl_Mail.FileName
                        Dim fname As String = filename.Substring(0, filename.LastIndexOf("."))
                        Dim fext As String = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - (filename.LastIndexOf(".") + 1))
                        Dim encname As String = ""
                        'txtUploads.Text = fname
                        Dim Path As String
                        encname = fname & DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date.Day & DateTime.Now.Date.Hour & DateTime.Now.Date.Minute & DateTime.Now.Date.Second
                        fupl_Mail.SaveAs(Server.MapPath("Collateral/Jobcard/" & Clean(encname.ToString().Replace("&", "")) & "." & fext))
                        Path = "Collateral/Jobcard/" & Clean(encname.ToString().Replace("&", "")) & "." & fext

                        sql &= ",ApprovalMail='" & Path & "'"
                        SendForclientApprovedToFM(clsApex.NotificationType.clientapprovalpendinfromKAMToFM)
                        Label1.Text = "Client approval verification request has been sent to finance team."
                    Else
                        Label1.Text = "Job Code Updated successfully"
                    End If
                    sql &= ",ModifiedOn = getdate()"
                    sql &= ",ModifiedBy = " & getLoggedUserID()
                    sql &= " WHERE JobCardID=" & hdnJobCardID.Value

                    If ExecuteNonQuery(sql) > 0 Then

                        MessageDiv.Visible = True
                        lblJobCode.Visible = True
                        'divContent.Visible = False
                        FillDetails()

                    End If
                Else
                    lblError.Text = "Project End Date should be greater than Project Start Date"
                    divError.Visible = True
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Public Sub SendForApprovalBranchHead(ByVal type As String, ByVal bid As String, ByVal desig As String, Optional ByVal stg As String = "other")
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
            sql &= "('Pre P&L Approval'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Approval for the Pre P&L Account'"
            sql &= ",'H'"
            sql &= "," & type
            sql &= "," & bid
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                If stg = "other" Then
                    InsertRecieptentDetails(type, bid, desig)
                Else
                    Dim Estimate As String = "Estemate"
                    InsertRecieptentDetails(type, bid, desig, Estimate)
                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub InsertRecieptentDetails(ByVal type As String, ByRef bid As String, ByVal deg As String, Optional ByVal stag As String = "other")
        Try
            Dim notificationid As String = ""

            Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & type & " and AssociateID=" & bid
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

            Dim bheadid() As String = {0}
            Dim sql3 As String = " "
            Dim leadid As String = ""


            Dim clsApex As New clsApex
            hdnBriefID.Value = clsApex.GetBriefIDByJobCardID(hdnJobCardID.Value)
            leadid = clsApex.GetLeadIDByBriefID(hdnBriefID.Value)

            If leadid <> "" Then
                If deg = 2 Then


                    sql3 = "Select l.insertedBy from  dbo.APEX_Leads as l "
                    sql3 &= "  join dbo.APEX_UsersDetails as ud on l.insertedBy=ud.UserDetailsID "
                    sql3 &= "  where leadid=" & leadid & "  and Designation=" & deg & ""
                ElseIf deg = 3 Then
                    sql3 = "Select UserDetailsID from APEX_UsersDetails where Designation=" & deg
                Else

                    sql3 = "Select UserDetailsID from APEX_UsersDetails where Designation=" & deg
                End If

                Dim ds3 As New DataSet
                ds3 = ExecuteDataSet(sql3)
                If ds3.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        bheadid(i) = ds3.Tables(0).Rows(i)(0).ToString()
                    Next
                End If
                For Each Str As String In bheadid


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
                    sql1 &= "," & Str
                    sql1 &= ",'N'"
                    sql1 &= ",'N'"
                    sql1 &= ",'N'"
                    sql1 &= ",NULL"
                    sql1 &= ",NULL"
                    sql1 &= "," & getLoggedUserID() & ")"

                    If ExecuteNonQuery(sql1) > 0 Then
                        Dim emailid As String = ""
                        If Str <> "" Then
                            Dim uid As Integer = Convert.ToInt32(Str)
                            emailid = GetEmailIDByUserID(uid)
                        End If
                        If emailid <> "" Then
                            sendMail(title, message, "", emailid, "")
                        End If


                    End If
                Next
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillActivityType()
        Try
            Dim sql As String = "Select * from APEX_ActivityType where IsActive='Y' and IsDeleted='N' Order By ProjectType"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                chklActivityType.DataSource = ds
                chklActivityType.DataTextField = "ProjectType"
                chklActivityType.DataValueField = "ProjectTypeID"
                chklActivityType.DataBind()

                ddlPrimary.DataSource = ds
                ddlPrimary.DataTextField = "ProjectType"
                ddlPrimary.DataValueField = "ProjectTypeID"
                ddlPrimary.DataBind()
            End If
            ' ddlPrimary.SelectedItem.Text = lblbriefPrimaryActivity.Text
            ddlPrimary.Items.Insert(0, New ListItem("Select", "0"))

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillClientContactPerson()
        Try
            ddlClientBudgetOwner.Items.Clear()
            ddlContactDetails.Items.Clear()
            ddlApprovedBy.Items.Clear()

            Dim sql As String = "Select * from APEX_ClientContacts where IsActive='Y' and IsDeleted='N' and RefClientID='" & ddlClientName.SelectedItem.Value & "' Order By ContactName"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlClientBudgetOwner.DataSource = ds
                ddlClientBudgetOwner.DataTextField = "ContactName"
                ddlClientBudgetOwner.DataValueField = "ContactID"
                ddlClientBudgetOwner.DataBind()

                ddlContactDetails.DataSource = ds
                ddlContactDetails.DataTextField = "ContactName"
                ddlContactDetails.DataValueField = "ContactID"
                ddlContactDetails.DataBind()

                ddlApprovedBy.DataSource = ds
                ddlApprovedBy.DataTextField = "ContactName"
                ddlApprovedBy.DataValueField = "ContactID"
                ddlApprovedBy.DataBind()
            End If
            ddlClientBudgetOwner.Items.Insert(0, New ListItem("Select", "0"))
            ddlClientBudgetOwner.Items.Insert(ddlClientBudgetOwner.Items.Count, New ListItem("Add new", "Add new"))
            ddlContactDetails.Items.Insert(0, New ListItem("Select", "0"))
            ddlApprovedBy.Items.Insert(0, New ListItem("Select", "0"))
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillCampaignManager()
        Try
            Dim sql As String = "Select * from APEX_UsersDetails where IsActive='Y' and IsDeleted='N' order by FirstName"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then

                ddlProjectManager.DataSource = ds
                ddlProjectManager.DataTextField = "FirstName"
                ddlProjectManager.DataValueField = "UserDetailsID"
                ddlProjectManager.DataBind()
            End If
            ddlProjectManager.Items.Insert(0, New ListItem("Select", "0"))
            'ddlProjectManager.SelectedItem.Text = lblPM.Text
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillDetails()
        Try
            Dim activityType() As String
            Dim sql As String = "Select convert(varchar(10),ActivityStartDate,105) as NActivityStartDate,convert(varchar(10),ActivityEndDate,105) as NActivityEndDate, convert(varchar(10),ClientApprovedDatetime,105) as NClientApprovedDatetime ,*,isnull(Approvalmail,'')Approvalmail1,ISclientapprovalverified from APEX_JobCard where IsActive='Y' and IsDeleted='N' and JobCardID=" & hdnJobCardID.Value
            Dim ds As New DataSet

            ds = ExecuteDataSet(sql)

            If ds.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim epass As String = ds.Tables(0).Rows(i)("Approvalmail1")
                    ds.Tables(0).Rows(i).Item("Approvalmail1") = Clean(epass)
                Next
                activityType = ds.Tables(0).Rows(0)("RefActivityID").ToString().Split("|")
                lblActivityType.Text = ds.Tables(0).Rows(0)("RefActivityID").ToString()
                If activityType.Count = 1 Then
                    If activityType(0) <> "" Then
                        For i As Integer = 0 To activityType.Count - 1
                            chklActivityType.SelectedIndex = chklActivityType.Items.IndexOf(chklActivityType.Items.FindByText(activityType(i)))
                        Next i
                    End If
                Else
                    For i As Integer = 0 To activityType.Count - 1
                        If activityType(i) <> "" Then
                            chklActivityType.Items(chklActivityType.Items.IndexOf(chklActivityType.Items.FindByText(activityType(i)))).Selected = True
                        End If
                    Next i
                End If

                txtJobCode.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
                lblJobCode.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
                txtProjectName.Text = ds.Tables(0).Rows(0)("JobCardName").ToString()
                ddlPrimary.SelectedValue = ds.Tables(0).Rows(0)("PrimaryActivityID")
                txtProjectStartDate.Text = ds.Tables(0).Rows(0)("NActivityStartDate").ToString()
                txtProjectEndDate.Text = ds.Tables(0).Rows(0)("NActivityEndDate").ToString()
                Dim kamid As String = ""

                ddlProjectManager.SelectedValue = ds.Tables(0).Rows(0)("ProjectManagerID").ToString()
                ddlClientName.SelectedValue = ds.Tables(0).Rows(0)("RefClientID").ToString()

                FillClientContactPerson()
                If Not IsDBNull(ds.Tables(0).Rows(0)("ClientBudgetOwner")) Then
                    ddlClientBudgetOwner.SelectedValue = ds.Tables(0).Rows(0)("ClientBudgetOwner")
                End If

                Dim capex As New clsApex
                Dim res() As String
                res = capex.FillContactInfo(ddlClientBudgetOwner.SelectedItem.Value)
                lblCBEmail.Text = res(0)
                lblCBContact.Text = res(1)

                ddlContactDetails.SelectedIndex = ddlContactDetails.Items.IndexOf(ddlContactDetails.Items.FindByValue(ds.Tables(0).Rows(0)("ClientContactPerson").ToString()))

                lblProjectName.Text = ds.Tables(0).Rows(0)("JobCardName").ToString()
                lblPrimary.Text = ddlPrimary.SelectedItem.Text
                lblProjectStartDate.Text = ds.Tables(0).Rows(0)("NActivityStartDate").ToString()
                lblProjectEndDate.Text = ds.Tables(0).Rows(0)("NActivityEndDate").ToString()

                lblProjectManager.Text = ddlProjectManager.SelectedItem.Text
                lblClientName.Text = ddlClientName.SelectedItem.Text
                lblClientBudgetOwner.Text = ddlClientBudgetOwner.SelectedItem.Text
                lblContactDetails.Text = ddlContactDetails.SelectedItem.Text
                Dim res1() As String
                res1 = capex.FillContactInfo(ddlContactDetails.SelectedItem.Value)
                lblCEmail.Text = res1(0)
                lblCContact.Text = res1(1)

                If ds.Tables(0).Rows(0)("IsClientApproval").ToString() = "Y" Then
                    chkApproval.Checked = True
                    lblApproval.Text = "Y"
                Else
                    chkApproval.Checked = False
                End If
                If ds.Tables(0).Rows(0)("ApprovalMail").ToString() <> "" Then

                    'lblfulpl_Mail.NavigateUrl = ds.Tables(0).Rows(0)("Approvalmail1").ToString()
                    'lblfulpl_Mail.PostBackUrl = ds.Tables(0).Rows(0)("Approvalmail1").ToString()
                    hdnlblfulpl_Mail.Value = ds.Tables(0).Rows(0)("Approvalmail1").ToString()
                    lblfulpl_Mail.Visible = True
                
                Else
                    lblfulpl_Mail.Visible = False
                    btnverify.Visible = False
                    btnreject.Visible = False
                End If


                txtApprovalDate.Text = ds.Tables(0).Rows(0)("NClientApprovedDatetime").ToString()
                lblApprovalDate.Text = ds.Tables(0).Rows(0)("NClientApprovedDatetime").ToString()
                If Not IsDBNull(ds.Tables(0).Rows(0)("ApprovedBy")) Then
                    ddlApprovedBy.SelectedValue = ds.Tables(0).Rows(0)("ApprovedBy")
                End If

                lblApprovedBy.Text = ddlApprovedBy.SelectedItem.Text

                Dim res2() As String
                res2 = capex.FillContactInfo(ddlApprovedBy.SelectedItem.Value)
                lblCApproveEmail.Text = res2(0)
                lblCApproveContact.Text = res2(1)


            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnAddClient_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddClient.Click
        Try
            Dim sql As String = "Insert into APEX_Clients(Client,Industry,AnnualTurnover,InsertedBy) values ('" & txtClient.Text & "','" & txtIndustry.Text & "','" & txtAnnualTurnover.Text & "','" & getLoggedUserID() & "')"
            If ExecuteNonQuery(sql) > 0 Then
                tblNewClient.Visible = False
                ddlClientName.DataSource = Nothing
                ddlClientName.DataBind()

                FillClient()

                ddlClientName.SelectedIndex = ddlClientName.Items.IndexOf(ddlClientName.Items.FindByText(txtClient.Text))
                ClearNewClient()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillClient()
        Try
            ddlClientName.Items.Clear()

            Dim sql As String = "Select ClientID,Client from APEX_Clients where IsActive='Y' and IsDeleted='N'"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlClientName.DataSource = ds
                ddlClientName.DataTextField = "Client"
                ddlClientName.DataValueField = "ClientID"
                ddlClientName.DataBind()
            End If

            ddlClientName.Items.Insert(0, New ListItem("Select", "0"))
            ddlClientName.Items.Insert(ddlClientName.Items.Count, New ListItem("Non Existing (New Client)", "99999"))
            ' ddlClientName.SelectedItem.Text = lblbriefClient.Text

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub ClearNewClient()
        Try
            txtClient.Text = ""
            txtIndustry.Text = ""
            txtAnnualTurnover.Text = ""
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub ddlClientName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlClientName.SelectedIndexChanged
        Try
            ddlClientBudgetOwner.Items.Clear()
            ddlContactDetails.Items.Clear()

            If ddlClientName.SelectedItem.Value = "99999" Then
                ddlClientBudgetOwner.Items.Insert(0, New ListItem("Select", "0"))
                tblNewClient.Visible = True
                ClearNewClient()
            ElseIf ddlClientName.SelectedIndex > 0 And ddlClientName.SelectedItem.Value <> "99999" Then
                tblNewClient.Visible = False
                'FillClientContactPerson()

            Else
                tblNewClient.Visible = False
                ddlClientBudgetOwner.Items.Insert(0, New ListItem("Select", "0"))
                ddlContactDetails.Items.Insert(0, New ListItem("Select", "0"))
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    'Protected Sub ddlClientBudgetOwner_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlClientBudgetOwner.SelectedIndexChanged
    '    Try
    '        If ddlClientBudgetOwner.SelectedItem.Value = "99999" Then
    '            tblContactBudget.Visible = True
    '            ClearNewBudgetContactPerson()
    '            tdcontactinfo.Visible = False
    '        ElseIf ddlClientBudgetOwner.SelectedIndex > 0 Then
    '            tdcontactinfo.Visible = True
    '            tblContactBudget.Visible = False
    '            Dim capex As New clsApex
    '            Dim res() As String
    '            res = capex.FillContactInfo(ddlClientBudgetOwner.SelectedItem.Value)
    '            lblCBEmail.Text = res(0)
    '            lblCBContact.Text = res(1)
    '        Else
    '            tdcontactinfo.Visible = False
    '        End If
    '    Catch ex As Exception
    '        SendExMail(ex.Message, ex.ToString())
    '    End Try
    'End Sub

    'Protected Sub ddlContactDetails_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlContactDetails.SelectedIndexChanged
    '    Try
    '        If ddlClientBudgetOwner.SelectedItem.Value = "99999" Then
    '            tblContactDetails.Visible = True
    '            ClearNewContactPerson()
    '        Else
    '            tblContactDetails.Visible = False
    '            Dim capex As New clsApex
    '            Dim res() As String
    '            res = capex.FillContactInfo(ddlContactDetails.SelectedItem.Value)
    '            lblCEmail.Text = res(0)
    '            lblCContact.Text = res(1)
    '        End If
    '    Catch ex As Exception
    '        SendExMail(ex.Message, ex.ToString())
    '    End Try
    'End Sub

    Private Sub ClearNewBudgetContactPerson()
        txtBudgetContactPerson.Text = ""
        txtBudgetContactOfficialEmail.Text = ""
        txtBudgetContactMobile1.Text = ""
    End Sub

    Private Sub FillEstimate()
        Try
            If hdnJobCardID.Value <> "" Then
                Dim sql As String = "select   isnull(EstimatedTotal,'0') as EstimatedSubTotal,"
                sql &= "  (Select sum(PreEventcost) from APEX_PrePnLcost where RefBriefID= j.RefBriefID) as ppl,"
                sql &= " ((EstimatedTotal - (Select sum(PreEventcost) from  APEX_PrePnLcost where RefBriefID= j.RefBriefID))/EstimatedTotal * 100)"
                sql &= " as profitper  from APEX_JobCard as j "
                sql &= " Inner join APEX_Estimate as e on j.RefBriefID  = e.RefBriefID "
                sql &= "  where j.JobCardID= " & hdnJobCardID.Value
                Dim ds As New DataSet
                ds = ExecuteDataSet(sql)
                Dim per As Decimal = 0

                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0)("EstimatedSubTotal")) Then
                        txtEstimate.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal")
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0)("ppl")) Then
                        txtPrePnL.Text = ds.Tables(0).Rows(0)("ppl")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0)("profitper")) Then
                        per = ds.Tables(0).Rows(0)("profitper")
                        txtActual.Text = Math.Round(per, 2)
                    Else
                        txtActual.Text = "0"
                    End If
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub calculatePercentage()
        Try
            Dim apx As New clsApex
            Dim sql As String = ""
            If hdnJobCardID.Value <> "" Then
                sql = "Select preprofitPercent from APEX_PrePnL where RefBriefID=" & apx.GetBriefIDByJobCardID(hdnJobCardID.Value)
                Dim ds As DataSet = Nothing
                ds = ExecuteDataSet(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0)("preprofitPercent")) Then
                        txtActual.Text = ds.Tables(0).Rows(0)("preprofitPercent").ToString()
                    Else
                        txtActual.Text = ""
                    End If
                Else
                    txtActual.Text = ""

                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub CloseAllControls()
        Try
            txtProjectName.Visible = False
            ddlPrimary.Visible = False
            txtProjectStartDate.Visible = False
            txtProjectEndDate.Visible = False
            ddlClientName.Visible = False
            ddlClientBudgetOwner.Visible = False
            ddlContactDetails.Visible = False
            chkApproval.Visible = False
            txtApprovalDate.Visible = False
            ddlApprovedBy.Visible = False
            chklActivityType.Visible = False
            tblContactBudget.Visible = False

            tblNewClient.Visible = False
            ddlProjectManager.Visible = False
            fupl_Mail.Visible = False
            txtJobCode.Visible = False

            lblProjectName.Visible = True
            lblPrimary.Visible = True
            lblProjectStartDate.Visible = True
            lblProjectEndDate.Visible = True
            lblClientName.Visible = True
            lblClientBudgetOwner.Visible = True
            lblContactDetails.Visible = True
            lblApproval.Visible = True
            lblApprovalDate.Visible = True
            lblApprovedBy.Visible = True
            lblActivityType.Visible = True
            lblProjectManager.Visible = True
            lblJobCode.Visible = True

            'CheckJobTaskComplete() 
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            If Len(Request.QueryString("nid")) > 0 Then
                Response.Redirect("Home.aspx")
            ElseIf Len(Request.QueryString("user")) > 0 Then

                Response.Redirect("Home.aspx")
            Else


                If hdnJobCardID.Value <> "" Then
                    Dim sql As String = "Select JobCompleted from APEX_JobCard where JobCardID=" & hdnJobCardID.Value
                    Dim ds As New DataSet
                    ds = ExecuteDataSet(sql)
                    If ds.Tables(0).Rows.Count > 0 Then
                        If ds.Tables(0).Rows(0)(0).ToString() = "Y" Then
                            Response.Redirect("JobCard.aspx?mode=cj")
                        Else
                            Response.Redirect("JobCard.aspx?mode=rj")
                        End If
                    Else
                        Response.Redirect("JobCard.aspx?mode=rj")
                    End If
                Else
                    Response.Redirect("JobCard.aspx?mode=rj")
                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    'Protected Sub btnBudgetContactPerson_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBudgetContactPerson.Click
    '    Try
    '        If CheckDuplicateContactPerson(txtBudgetContactOfficialEmail.Text) = True Then
    '            Dim sql As String = "Insert into APEX_ClientContacts(ContactName,ContactOfficialEmailID,Mobile1,RefClientID,InsertedBy) "
    '            sql &= " values ('" & txtBudgetContactPerson.Text & "','" & txtBudgetContactOfficialEmail.Text & "','" & txtBudgetContactMobile1.Text & "'," & ddlClientName.SelectedItem.Value & "," & getLoggedUserID() & ")"
    '            If ExecuteNonQuery(sql) > 0 Then
    '                tblContactBudget.Visible = False

    '                ddlClientBudgetOwner.Items.Clear()

    '                FillClientContactPerson()
    '                ddlClientBudgetOwner.SelectedIndex = ddlClientBudgetOwner.Items.IndexOf(ddlClientBudgetOwner.Items.FindByText(txtContactPerson.Text))
    '            End If
    '        Else
    '            lblError.Text = "Contact Person Already Exists"
    '            divError.Visible = True
    '        End If
    '    Catch ex As Exception
    '        SendExMail(ex.Message, ex.ToString())
    '    End Try
    'End Sub

    'Protected Sub btnContactDetails_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnContactDetails.Click
    '    Try
    '        If CheckDuplicateContactPerson(txtContactOfficialEmail.Text) = True Then
    '            Dim sql As String = "Insert into APEX_ClientContacts(ContactName,ContactOfficialEmailID,Mobile1,RefClientID,InsertedBy) "
    '            sql &= " values ('" & txtContactPerson.Text & "','" & txtContactOfficialEmail.Text & "','" & txtContactMobile1.Text & "'," & ddlClientName.SelectedItem.Value & "," & getLoggedUserID() & ")"
    '            If ExecuteNonQuery(sql) > 0 Then
    '                tblContactDetails.Visible = False

    '                ddlContactDetails.Items.Clear()
    '                ClearNewContactPerson()
    '                FillClientContactPerson()
    '                ddlContactDetails.SelectedIndex = ddlContactDetails.Items.IndexOf(ddlContactDetails.Items.FindByText(txtContactPerson.Text))
    '            End If
    '        Else
    '            lblError.Text = "Contact Person Already Exists"
    '            divError.Visible = True
    '        End If
    '    Catch ex As Exception
    '        SendExMail(ex.Message, ex.ToString())
    '    End Try
    'End Sub

    Private Function CheckDuplicateContactPerson(ByVal ContactOfficialEmail As String) As Boolean
        Dim result As Boolean = False
        Try
            Dim sql As String = "Select * from APEX_ClientContacts where ContactOfficialEmailID='" & ContactOfficialEmail & "'"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = False
            Else
                lblError.Text = ""
                divError.Visible = False
                result = True
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Private Function CheckJobCard() As Boolean
        Dim result As Boolean = False
        Try
            Dim sql As String = "Select  isnull(JobCardNo,'N') as JobCard from APEX_JobCard where JobCardID=" & hdnJobCardID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)(0).ToString().Substring(0, 4) = "KIMS" Then
                    result = True
                Else
                    result = False
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Private Sub SelectClientContact()
        Try
            Dim sql As String = "Select * From APEX_JobCard where JobCardID=" & hdnJobCardID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlContactDetails.SelectedIndex = ddlContactDetails.Items.IndexOf(ddlContactDetails.Items.FindByValue(ds.Tables(0).Rows(0)("ClientContactPerson").ToString()))

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub


    Private Sub InsertTask()
        Try
            Dim sql As String = "INSERT INTO APEX_Task(RefActivityType,RefJobCardID)VALUES(" & ddlPrimary.SelectedItem.Value & "," & hdnJobCardID.Value & ")"
            ExecuteNonQuery(sql)
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

    Private Sub CheckJobTaskComplete()
        Dim result As String = "False"
        Try
            Dim sql As String = "Select JobCardNo from APEX_JobCard where JobCardID=" & hdnJobCardID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                btnJobCardClosure.Visible = True
            Else
                btnJobCardClosure.Visible = False
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        'Return result
    End Sub

    Private Function CheckJobSubTaskComplete() As String
        Dim result As String = "False"
        Try
            Dim i As Integer = 1
            Dim sql As String = "select st.SubTaskCompleted "
            sql &= " from APEX_JobCard as jc"
            sql &= " Inner Join APEX_SubTask as st on  jc.JobCardID = st.RefJobCardID where RefJobCardID=" & hdnJobCardID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)

            If ds.Tables(0).Rows.Count > 0 Then
                For t As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(t)(0).ToString() = "Y" Then
                        i *= 1
                    Else
                        i *= 0
                    End If
                Next
            Else
                result = "NA"
            End If
            If result <> "NA" Then
                If i = 1 Then
                    result = "True"
                Else
                    result = "False"
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Protected Sub btnJobCardClosure_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnJobCardClosure.Click
        Try
            Dim sql As String = "Update APEX_JobCard set JobCompleted = 'P' where JobCardID = " & hdnJobCardID.Value
            If ExecuteNonQuery(sql) > 0 Then
                Dim capex As New clsApex
                If hdnNodificationID.Value <> "" Then
                    capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
                End If
                hdnNodificationID.Value = ""

                'InsertPostPnLDetails()
                capex.UpdateStageLevel("8", hdnJobCardID.Value)
                SendNotification_PMPost()
                'Response.Redirect("JobCard.aspx")
                divContent.Visible = False
                Label1.Text = "Job close Post P&L Request sent to PM"
                MessageDiv.Visible = True
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function CheckJobCardExists() As Boolean
        Dim result As Boolean = True
        Try
            Dim sql As String = "Select * from APEX_JobCard where JobCardNo='" & Clean(txtJobCode.Text) & "'"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = True
                lblError.Text = ""
                divError.Visible = False
            Else
                result = False
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function
    Private Function jComplete() As Boolean
        Dim result As Boolean = False
        Try
            Dim sql As String = "Select JobCompleted from APEX_JobCard where JobCardID=" & hdnJobCardID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Private Function checkforAccountdepartment() As Boolean
        Dim flag As Boolean = False
        Try
            Dim apx As New clsApex
            Dim role As String
            role = apx.GetRoleName(getLoggedUserName())
            If role = "A" Or role = "F" Then
                flag = True
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return flag
    End Function

    Private Function getprojectmnager(ByVal bid As String) As String
        Dim result As String = ""
        Try
            Dim sql As String = ""
            If bid <> "" Then
                sql = "Select InsertedBy from dbo.APEX_Brief  where briefID=" & bid
                Dim ds As DataSet = Nothing
                ds = ExecuteDataSet(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0)("InsertedBy")) Then
                        result = ds.Tables(0).Rows(0)("InsertedBy")

                    End If
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

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
            sql &= "('Job Code Assigned'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Job Code Assigned'"
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

    Public Sub SendForJobCodeApprovedToPM(ByVal type As String)
        Try
            Dim pm As String = ""

            If hdnBriefID.Value = "" Then
                Dim capex As New clsApex
                hdnBriefID.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)
            End If

            Dim sql_bid As String = "select ProjectManagerID from APEX_Jobcard  where JobCardID=" & hdnJobCardID.Value
            Dim ds_bid As New DataSet
            ds_bid = ExecuteDataSet(sql_bid)
            If ds_bid.Tables(0).Rows.Count > 0 Then
                pm = ds_bid.Tables(0).Rows(0)(0).ToString()
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
            sql &= "('Job Code Assigned'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Job Code Assigned proceed with task creation'"
            sql &= ",'H'"
            sql &= "," & type
            sql &= "," & hdnJobCardID.Value
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then

                InsertRecieptentDetailsPM(type, pm)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub InsertRecieptentDetailsPM(ByVal type As String, ByRef pm As String)
        Try
            Dim notificationid As String = ""

            Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & type & " and AssociateID=" & hdnJobCardID.Value()
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
            sql1 &= "," & pm
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",NULL"
            sql1 &= ",NULL"
            sql1 &= "," & getLoggedUserID() & ")"

            If ExecuteNonQuery(sql1) > 0 Then
                Dim emailid As String = ""
                If pm <> "" Then
                    Dim uid As Integer = Convert.ToInt32(pm)
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

    Public Sub SendForJobCodeApprovedToFM(ByVal type As String)
        Try
            Dim fm As String = ""

            If hdnBriefID.Value = "" Then
                Dim capex As New clsApex
                hdnBriefID.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)
            End If

            Dim sql_bid As String = "select UserDetailsID from APEX_UsersDetails  where Role='F'"
            Dim ds_bid As New DataSet
            ds_bid = ExecuteDataSet(sql_bid)
            If ds_bid.Tables(0).Rows.Count > 0 Then
                fm = ds_bid.Tables(0).Rows(0)(0).ToString()
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
            sql &= "('Request for JobCode'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Request for JobCode'"
            sql &= ",'H'"
            sql &= "," & type
            sql &= "," & hdnJobCardID.Value()
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then

                InsertRecieptentDetailsFM(type, fm)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub InsertRecieptentDetailsFM(ByVal type As String, ByRef fm As String)
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
            sql3 = "select UserDetailsID from APEX_UsersDetails  where Role='F'"

            Dim clsApex As New clsApex
            hdnBriefID.Value = clsApex.GetBriefIDByJobCardID(hdnJobCardID.Value)
            leadid = clsApex.GetLeadIDByBriefID(hdnBriefID.Value)
            Dim ds3 As New DataSet
            ds3 = ExecuteDataSet(sql3)
            If ds3.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds3.Tables(0).Rows.Count - 1
                    If Not IsDBNull(ds3.Tables(0).Rows(0)("UserDetailsID")) Then
                        bheadid = ds3.Tables(0).Rows(i)(0).ToString()
                        If notificationid <> "" Then
                            sendnotification(bheadid, notificationid)
                        End If

                    End If

                Next
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try

    End Sub
    Private Sub sendnotification(ByVal bheadid As String, ByVal notification As String)
        Try
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
            sql1 &= "(" & notification
            sql1 &= "," & bheadid
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",NULL"
            sql1 &= ",NULL"
            sql1 &= "," & getLoggedUserID() & ")"

            If ExecuteNonQuery(sql1) > 0 Then
                Dim emailid As String = ""
                If bheadid <> "" Then
                    Dim uid As Integer = Convert.ToInt32(bheadid)
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
    Private Function primaryActivity(ByVal p1 As String) As String
        Dim activity As String = ""
        Try
            Dim sql As String = "Select ProjectType from dbo.APEX_ActivityType where projectTypeID=" & p1 & " and isActive='Y'"
            Dim ds As DataSet = Nothing

            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("ProjectType")) Then
                    activity = ds.Tables(0).Rows(0)("ProjectType").ToString()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return activity
    End Function
    Private Sub BindContactPersonDropdownEdit()
        Try
            ddlContactPerson.Items.Clear()

            Dim sqlddlCPstring As String = ""
            Dim ds As New DataSet

            sqlddlCPstring = "Select ContactID,ContactName from APEX_ClientContacts where IsActive='Y' and IsDeleted='N' Order By ContactName"
            ds = ExecuteDataSet(sqlddlCPstring)

            Dim LastValue As Integer = ds.Tables(0).Rows.Count + 1
            If ds.Tables(0).Rows.Count > 0 Then
                ddlContactPerson.DataSource = ds
                ddlContactPerson.DataTextField = "ContactName"
                ddlContactPerson.DataValueField = "ContactID"
                ddlContactPerson.DataBind()

            End If

            ddlContactPerson.Items.Insert(0, New ListItem("Select", "0"))
            ddlContactPerson.Items.Insert(LastValue, "Non Existing(New Person)")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindBriefData(ByVal BriefID As Integer)
        Try
            Dim activityType() As String
            Dim sqlBriefData As String = ""
            Dim ds As New DataSet

            sqlBriefData = "Select convert(varchar(10),ActivityDate,105) as NActivityDate,* from APEX_Brief Where IsActive='Y' and IsDeleted='N' and BriefID=" & BriefID & ""
            ds = ExecuteDataSet(sqlBriefData)

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("BriefName").ToString() <> "" Then
                    lblBrief.Text = ds.Tables(0).Rows(0)("BriefName").ToString()
                    lblbriefname.Text = ds.Tables(0).Rows(0)("BriefName").ToString()
                End If
                If ds.Tables(0).Rows(0)("PrimaryActivityID").ToString() <> "" Then
                    lblActivity.Text = primaryActivity(ds.Tables(0).Rows(0)("PrimaryActivityID").ToString())
                    lblbriefPrimaryActivity.Text = lblActivity.Text
                End If

                If ds.Tables(0).Rows(0)("RefActivityTypeID").ToString() <> "" Then
                    activityType = ds.Tables(0).Rows(0)("RefActivityTypeID").ToString().Split("|")

                    lblchklActivityType.Text = ds.Tables(0).Rows(0)("RefActivityTypeID").ToString()

                    If activityType.Count = 1 Then
                        If activityType(0) <> "" Then
                            For i As Integer = 0 To activityType.Count - 1
                                chklActivityType.SelectedIndex = chklActivityType.Items.IndexOf(chklActivityType.Items.FindByText(activityType(i)))
                            Next i
                        End If
                    ElseIf activityType.Count > 1 Then

                        For i As Integer = 0 To activityType.Count - 1
                            If activityType(i) <> "" Then
                                ' chklActivityType.Items(chklActivityType.Items.IndexOf(chklActivityType.Items.FindByText(activityType(i)))).Selected = True
                            End If
                        Next i

                    End If
                End If

                If ds.Tables(0).Rows(0)("NActivityDate").ToString() <> "" Then

                    lblActivityDate.Text = ds.Tables(0).Rows(0)("NActivityDate").ToString()

                End If

                If ds.Tables(0).Rows(0)("RefClientID").ToString() <> "" Then

                    lblClient.Text = returnClientname(ds.Tables(0).Rows(0)("RefClientID").ToString())
                    lblbriefClient.Text = lblClient.Text
                    lblbrClient.Text = lblClient.Text
                End If

                BindContactPersonDropdownEdit()
                ddlContactPerson.SelectedIndex = ddlContactPerson.Items.IndexOf(ddlContactPerson.Items.FindByValue(ds.Tables(0).Rows(0)("RefContactPersonID").ToString()))
                Dim capex As New clsApex
                Dim res() As String
                res = capex.FillContactInfo(ddlContactPerson.SelectedItem.Value)
                If res(0) <> "" Then
                    lblCEmail.Text = "Email: " & res(0)
                Else
                    lblCEmail.Text = ""
                End If
                If res(1) <> "" Then

                    lblCContact.Text = "Contact: " & res(1)
                Else
                    lblCContact.Text = ""
                End If


                lblContactPerson.Text = ddlContactPerson.SelectedItem.Text

                If ds.Tables(0).Rows(0)("ScopeOfwork").ToString() <> "" Then

                    lblScope.Text = ds.Tables(0).Rows(0)("ScopeOfwork").ToString()

                End If

                If ds.Tables(0).Rows(0)("TargetAudience").ToString() <> "" Then

                    lblTargetAudience.Text = ds.Tables(0).Rows(0)("TargetAudience").ToString()

                End If

                If ds.Tables(0).Rows(0)("MeasurementMatrix").ToString() <> "" Then

                    lblMeasurementMatrix.Text = ds.Tables(0).Rows(0)("MeasurementMatrix").ToString()

                End If

                If ds.Tables(0).Rows(0)("ActivityDetails").ToString() <> "" Then

                    lblActivityDetails.Text = ds.Tables(0).Rows(0)("ActivityDetails").ToString()

                End If

                If ds.Tables(0).Rows(0)("KeyChallangesForExecution").ToString() <> "" Then

                    lblKeyChallenges.Text = ds.Tables(0).Rows(0)("KeyChallangesForExecution").ToString()

                End If

                If ds.Tables(0).Rows(0)("TimelineForRevert").ToString() <> "" Then
                    lblTimeline.Text = ds.Tables(0).Rows(0)("TimelineForRevert").ToString()
                End If
                If ds.Tables(0).Rows(0)("Budget").ToString() <> "" Then
                    lblBudget.Text = ds.Tables(0).Rows(0)("Budget").ToString()
                    lblbriefBudget.Text = lblBudget.Text
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Function returnClientname(ByVal p1 As String) As String
        Dim client As String = ""
        Try
            Dim sql As String = "Select Client from dbo.APEX_Clients where ClientID=" & p1 & " and isActive='Y'"

            Dim ds As DataSet = Nothing

            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("Client")) Then
                    client = ds.Tables(0).Rows(0)("Client").ToString()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return client
    End Function
    Private Sub bindprepnldetail(ByVal bid As String)
        Try
            Dim sql As String = ""
            sql &= ""
            sql &= " Select (Select (isnull(Firstname,'')+' '+ isnull(Lastname,''))  from APEX_UsersDetails where UserDetailsID"
            sql &= " = jc.ProjectmanagerID) as PM, (Select Client from APEX_Clients where clientid = jc.RefClientID)  as Client,RefLeadID,"
            sql &= " (Select (isnull(Firstname,'')+' '+ isnull(Lastname,'')) from APEX_UsersDetails where UserDetailsID"
            sql &= " = b.insertedBy)  as KAM, b.refLeadID"
            sql &= "  ,(Select PreEventQuote  from APEX_PrePnL where RefBriefID=jc.REfBriefID) as PEQ"
            sql &= " ,jc.ProjectmanagerID from APEX_jobcard  as jc "
            sql &= "  join APEX_Brief as b on jc.RefBriefID=b.BriefID"
            sql &= "   left outer  join APEX_Leads as l on l.leadID=b.refLeadID"
            sql &= " where jc.RefBriefID=" & bid & ""

            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("PM")) Then
                    lblPM.Text = ds.Tables(0).Rows(0)("PM").ToString()
                    lblEstPM.Text = lblPM.Text
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("KAM")) Then
                    lblKAM.Text = ds.Tables(0).Rows(0)("KAM").ToString()
                    lblEstKAM.Text = lblKAM.Text
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("Client")) Then
                    lblClient.Text = ds.Tables(0).Rows(0)("Client")
                    lblEstClient.Text = lblClient.Text
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("PEQ")) Then
                    lblPEQ.Text = ds.Tables(0).Rows(0)("PEQ")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("ProjectmanagerID")) Then
                    ' hdnClientID.Value = ds.Tables(0).Rows(0)("ProjectmanagerID")
                End If
                If TotalQoute() <> vbNull Then
                    lblPreEventtotal.Text = TotalQoute()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Function TotalQoute() As Double
        Dim str As String = ""
        Dim result As Double = vbNull
        Try

            Dim sql As String = "Select Sum(PreEventCost) as SumPreEventCost  from APEX_PrePnLCost where RefBriefID=" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("SumPreEventCost")) Then
                    result = ds.Tables(0).Rows(0)("SumPreEventCost").ToString()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function
    Private Sub FillPrePnlDetails()
        Try
            hypPrePL.Visible = False
            Dim sql As String = ""
            Dim prepnlid As String = ""
            Dim apx As New clsApex
            prepnlid = apx.GetPLIDByBriefID(hdnBriefID.Value)
            sql = "Select convert(varchar(10),EventDate,105) as NEventDate,* from APEX_PrePnL where RefBriefID=" & hdnBriefID.Value & " and PrePnLID=" & prepnlid

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("RefClientID")) Then
                    lblplclient.Text = returnClientname(ds.Tables(0).Rows(0)("RefClientID").ToString())
                End If

                Dim eventdate As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0)("EventName")) Then
                    lblplActivity.Text = ds.Tables(0).Rows(0)("EventName")

                End If

                If Not IsDBNull(ds.Tables(0).Rows(0)("NEventDate")) Then
                    lblplEventDate.Text = ds.Tables(0).Rows(0)("NEventDate")
                End If

                lblplEventVenue.Text = ds.Tables(0).Rows(0)("EventVenue").ToString()
                lblplApprovalNo.Text = ds.Tables(0).Rows(0)("PnLApprovalNo").ToString()
                lblplCreditPeriod.Text = ds.Tables(0).Rows(0)("CreditPeriod").ToString()
                lblplPreEventQuote.Text = ds.Tables(0).Rows(0)("PreEventQuote").ToString()

                lblPreEPr.Text = ds.Tables(0).Rows(0)("PreProfitPercent").ToString()
                lblplremarls.Text = ds.Tables(0).Rows(0)("Remarks").ToString()

                If ds.Tables(0).Rows(0)("PLApprovalMail").ToString() <> "" Then
                    hypPrePL.NavigateUrl = ds.Tables(0).Rows(0)("PLApprovalMail").ToString()
                    hypPrePL.Visible = True
                Else
                    hypPrePL.Visible = False
                End If


                FillPrePnLCost()
                FillTotalQouteAfter()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub FillTotalQouteAfter()
        Try
            Dim sql As String = "Select Sum(PreEventCost) as SumPreEventCost,Sum(PreEventServiceTax) as PreEventServiceTax,Sum(PreEventTotal) as PreEventTotal  from APEX_PrePnLCost where RefBriefID=" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                lblplTCost.Text = ds.Tables(0).Rows(0)("SumPreEventCost").ToString()
                lblPreEventtotal.Text = ds.Tables(0).Rows(0)("PreEventTotal").ToString()

                If lblPreEventtotal.Text <> "" And lblplTCost.Text <> "" Then
                    lblplTSTax.Text = Math.Round(Convert.ToDouble(lblPreEventtotal.Text) - Convert.ToDouble(lblplTCost.Text), 2)
                End If

                lblplPETotal.Text = lblPreEventtotal.Text

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub FillPrePnLCost()
        Try
            Dim sql As String = "Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row,* from APEX_PrePnLCost where RefBriefID=" & hdnBriefID.Value & "   order by PrePnLCostID Asc"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gv_prepnl.DataSource = ds
                gv_prepnl.DataBind()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindFinalEstimateGrid()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            sql = "Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,* from  "
            sql &= " APEX_TempEstimate where RefBriefID= " & hdnBriefID.Value & " "
            'sql &= "  union  "
            'sql &= " Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,*"
            'sql &= " from APEX_EstimateCost where RefBriefID= " & hdnBriefID.Value & " "
            sql &= " order by refEstimateID Asc"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvDisplay.DataSource = ds
                gvDisplay.DataBind()
            End If

            FillOtherDetails()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub FillOtherDetails()
        Try
            Dim sql As String = "Select SUM(Estimate) as SubTotal,EstimatedSubTotal,EstimatedManagentFees,EstimatedTotal,EstimatedGrandTotal,EstimatedServiceTax "
            sql &= " from APEX_TempEstimate as te"
            sql &= "  Join APEX_Estimate as et on et.EstimateID = te.RefEstimateID"
            sql &= " where et.RefBriefID = " & hdnBriefID.Value
            sql &= " Group By EstimatedSubTotal,EstimatedManagentFees,EstimatedTotal,EstimatedGrandTotal,EstimatedServiceTax"

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)

            Dim mfee As Double = 0
            Dim stax As Double = 0

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("SubTotal").ToString().Trim() <> "" Then
                    lblSubTotal.Text = ds.Tables(0).Rows(0)("SubTotal").ToString().Trim()
                    lblEstSubtotal.Text = lblSubTotal.Text
                Else
                    lblSubTotal.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString()
                    lblEstSubtotal.Text = lblSubTotal.Text
                End If

                If ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString() <> "" And ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString() <> "" Then
                    txtMFeePer.Text = ((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString()) / Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString())) * 100).ToString()
                    mfee = Convert.ToDouble(txtMFeePer.Text)
                    lblMFeePer.Text = Math.Round(mfee, 2)
                End If

                If ds.Tables(0).Rows(0)("EstimatedGrandTotal").ToString() <> "" And ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString() <> "" Then
                    txtServiceTax.Text = ((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString()) / ds.Tables(0).Rows(0)("EstimatedTotal").ToString()) * 100).ToString()
                    stax = Convert.ToDouble(txtServiceTax.Text)
                    lblServiceTax.Text = Math.Round(stax, 2)
                    txtServiceTax.Text = lblServiceTax.Text
                End If

                If lblSubTotal.Text <> "" Then
                    txtMangnmtFees.Text = Math.Round((Convert.ToDouble(lblSubTotal.Text) * mfee) / 100, 2)
                End If

                If lblSubTotal.Text <> "" And txtMangnmtFees.Text <> "" Then
                    lblTotal.Text = Math.Round(Convert.ToDouble(lblSubTotal.Text) + Convert.ToDouble(txtMangnmtFees.Text), 2)
                End If

                If lblTotal.Text <> "" Then
                    lblServiceTax.Text = Math.Round((Convert.ToDouble(lblTotal.Text) * stax) / 100, 2)
                End If

                If lblTotal.Text <> "" And lblServiceTax.Text <> "" Then
                    lblGrandTotal.Text = Math.Round(Convert.ToDouble(lblTotal.Text) + Convert.ToDouble(lblServiceTax.Text), 2)
                End If

                lblMangnmtFees.Text = txtMangnmtFees.Text
                lblServiceTaxPer.Text = txtServiceTax.Text
                lblMFeePer.Text = txtMFeePer.Text
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancelHome_Click(sender As Object, e As EventArgs) Handles btnCancelHome.Click
        Try
            If Request.QueryString("ur").ToString() = "hm" Then
                Response.Redirect("Home.aspx?mode=KAM1")
            ElseIf Request.QueryString("ur").ToString() = "hm1" Then
                Response.Redirect("Home.aspx?mode=KAM2")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub updateJobCardFM(ByVal userid As String)
        Dim sql As String = "update APEX_JobCard set FinanceManagerID = " & userid & " where JobCardID=" & hdnJobCardID.Value
        ExecuteNonQuery(sql)
    End Sub

    Public Sub SendNotification_PMPost()
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
            sql &= "('Prepare Post P&L'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Prepare Post P&L'"
            sql &= ",'H'"
            sql &= "," & NotificationType.PMPostPnL
            sql &= "," & hdnJobCardID.Value
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails_PMPost()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub InsertRecieptentDetails_PMPost()
        Try
            Dim capex As New clsApex

            Dim notificationid As String = ""
            Dim sql As String = "Select Top(1) NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & NotificationType.PMPostPnL & " and AssociateID=" & hdnJobCardID.Value
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

            'sql3 = "Select b.insertedBy from dbo.APEX_Brief  as b    where BriefID= " & hdnBriefID.Value
            'Dim ds3 As New DataSet
            'ds3 = ExecuteDataSet(sql3)
            'If ds3.Tables(0).Rows.Count > 0 Then
            '    bheadid = ds3.Tables(0).Rows(0)(0).ToString()
            'End If
            bheadid = capex.GetProjectManagerOfJobCard(hdnJobCardID.Value)
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
            sql1 &= "," & bheadid
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",NULL"
            sql1 &= ",NULL"
            sql1 &= "," & getLoggedUserID() & ")"

            If ExecuteNonQuery(sql1) > 0 Then
                Dim emailid As String = ""
                If bheadid <> "" Then
                    Dim uid As Integer = Convert.ToInt32(bheadid)
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

    Private Sub InsertPostPnLDetails()
        Try
            Dim sql As String = "INSERT INTO [APEX_PostPnL]"
            sql &= " ([RefBriefID]"
            sql &= "  ,[RefClientID]"
            sql &= "  ,[RefJobCardID])"
            sql &= " VALUES"
            sql &= "  (" & hdnBriefID.Value
            sql &= "  ," & ddlClientName.SelectedItem.Value
            sql &= "," & hdnJobCardID.Value
            sql &= ")"

            If ExecuteNonQuery(sql) > 0 Then

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function NotificationSent(ByVal notificationType As String, ByVal jid As String) As Boolean
        Dim result As Boolean = True
        Dim sql As String = "select * from aPEX_notifications where type=" & notificationType & " and associateID=" & jid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = True
        Else
            result = False
        End If
        Return result
    End Function

    Private Function postpnlFilled() As Boolean
        Dim result As Boolean = False
        Dim sql As String = "Select JobCompleted from APEX_JobCard where JobCardID = " & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0).ToString() = "Y" Then
                result = True
            End If
        End If
        Return result
    End Function

    Public Function CheckNotification() As Boolean
        Dim result As Boolean = False
        Dim sql As String = "select * from APEX_Notifications "
        sql &= " inner join APEX_Recieptents on RefNotificationID=NotificationID"
        sql &= " where type in (8,11,13,14) and AssociateID=" & hdnJobCardID.Value & " and IsExecuted='N' and UserID=" & getLoggedUserID()
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnNodificationID.Value = ds.Tables(0).Rows(0)("NotificationID").ToString()
            result = True
        End If
        Return result
    End Function



    'Protected Sub lblfulpl_Mail_Click(sender As Object, e As EventArgs) Handles lblfulpl_Mail.Click
    '    Response.ContentType = "application/vnd.ms-outlook"
    '    Response.AppendHeader("Content-Disposition", "attachment; filename=Message.msg")

    '    Response.TransmitFile(Server.MapPath(lblfulpl_Mail.CommandArgument))
    '    Response.End()

    'End Sub

    Protected Sub lblfulpl_Mail_Click(sender As Object, e As EventArgs) Handles lblfulpl_Mail.Click
        Response.ContentType = "application/vnd.ms-outlook"

        Dim str As String = hdnlblfulpl_Mail.Value
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(str) & "")
        Response.TransmitFile("../" & str.ToString())
        'Response.TransmitFile("~/Collateral/Jobcard/65K Fund 2015422000.msg")
        Response.End()
    End Sub
    Public Shared Function GetFileName( _
    path As String _
) As String

    End Function


    Public Function CheckclientapprovalverifiyfromFM() As Boolean
        Dim result As Boolean = False
        Dim sql As String = "select ISclientapprovalverified from APEX_jobcard where jobcardID=" & hdnJobCardID.Value & ""
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)("ISclientapprovalverified").ToString() = "Y" Then
                result = True
            Else
                result = False
            End If

        End If
        Return result
    End Function

    Protected Sub btnverify_Click(sender As Object, e As EventArgs) Handles btnverify.Click
        If txtJobCode.Text.ToString.Substring(0, 4) = "KIMS" Then
            lblError.Text = "Please assign actual job code against temprary JC"
            divError.Visible = True
            txtJobCode.Focus()
            Exit Sub
        Else
            divError.Visible = False
            btnAdd_Click(sender, e)
            verifiyclientapproval(hdnJobCardID.Value, "Y")
            SendForclientApprovedFromFMToKAM(clsApex.NotificationType.clientapprovalpendinfromFMToKAM)
            SendForclientApprovedToPM(clsApex.NotificationType.clientapprovalpendinfromFMToPM)
            If messageshow = "hide" Then

            Else
                Label1.Text = "Client approval request verified."
                MessageDiv.Visible = True
            End If
            
        End If

    End Sub

    Protected Sub btnreject_Click(sender As Object, e As EventArgs) Handles btnreject.Click
        verifiyclientapproval(hdnJobCardID.Value, "N")
        SendForclientRejectFromFMToKAM(clsApex.NotificationType.clientrejectpendinfromFMToKAM)
        Label1.Text = "Client approval request reject."
        MessageDiv.Visible = True
    End Sub

    Public Sub SendForclientApprovedToFM(ByVal type As String)
        Try
            Dim fm As String = ""

            If hdnBriefID.Value = "" Then
                Dim capex As New clsApex
                hdnBriefID.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)
            End If

            Dim sql_bid As String = "select UserDetailsID from APEX_UsersDetails  where Role='F'"
            Dim ds_bid As New DataSet
            ds_bid = ExecuteDataSet(sql_bid)
            If ds_bid.Tables(0).Rows.Count > 0 Then
                fm = ds_bid.Tables(0).Rows(0)(0).ToString()
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
            sql &= "('Verification of Client Approval'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Verification of Client Approval for " & getProjectName() & "'"
            sql &= ",'H'"
            sql &= "," & type
            sql &= "," & hdnJobCardID.Value()
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetailsFM(type, fm)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Public Sub SendForclientApprovedFromFMToKAM(ByVal type As String)
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
            sql &= "('Client Approval verified'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Client Approval has been verified for " & getProjectName() & "'"
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

    Public Sub SendForclientApprovedToPM(ByVal type As String)
        Try
            Dim pm As String = ""

            If hdnBriefID.Value = "" Then
                Dim capex As New clsApex
                hdnBriefID.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)
            End If

            Dim sql_bid As String = "select ProjectManagerID from APEX_Jobcard  where JobCardID=" & hdnJobCardID.Value
            Dim ds_bid As New DataSet
            ds_bid = ExecuteDataSet(sql_bid)
            If ds_bid.Tables(0).Rows.Count > 0 Then
                pm = ds_bid.Tables(0).Rows(0)(0).ToString()
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
            sql &= "('Claim submission'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Claim submission has been activated.'"
            sql &= ",'H'"
            sql &= "," & type
            sql &= "," & hdnJobCardID.Value
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then

                InsertRecieptentDetailsPM(type, pm)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Public Sub SendForclientRejectFromFMToKAM(ByVal type As String)
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
            sql &= "('Client Approval reject'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Verification of client approval has been rejected for " & getProjectName() & "'"
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

    Private Sub verifiyclientapproval(jcID As String, ByVal ISclientapprovalverified As String)
        Dim sql As String = ""
        sql &= " update Apex_jobcard set ISclientapprovalverified='" & ISclientapprovalverified & "',fmverifyOn=getdate() where jobcardID='" & jcID & "'"
        ExecuteNonQuery(sql)

    End Sub

End Class