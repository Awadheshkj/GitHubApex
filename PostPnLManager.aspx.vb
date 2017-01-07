Imports System.Data
Imports clsMain
Imports clsDatabaseHelper
Imports clsApex

Imports System.Data.OleDb
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System
Imports System.Drawing

Partial Class PostPnLManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            trmsg.Visible = False
            'If Not Page.IsPostBack Then
            If Not IsPostBack Then
                'GetRoleName(userbane

                Dim capex12 As New clsApex
                Dim role As String
                role = capex12.GetRoleName(getLoggedUserName())
                If role = "O" Then
                    creditperiod.Visible = False
                    POnumber.Visible = False
                    btnAdd.Visible = True
                Else
                    creditperiod.Visible = True
                    POnumber.Visible = True
                    btnAdd.Visible = False
                End If

                btnFinallize.Visible = False
                btnHomeCancel.Visible = False
                lblTotalCost.Text = 0
                lblTotalServiceTax.Text = 0
                lblPostEventTotal.Text = 0

                divError.Visible = False
                MessageDiv.Visible = False
                trAppButtons.Visible = False
                trAppRemarks.Visible = False
                If Len(Request.QueryString("jid")) > 0 Then
                    If Request.QueryString("jid") <> Nothing Then
                        If Request.QueryString("jid").ToString() <> "" Then
                            hdnJobCardID.Value = Request.QueryString("jid")

                            Dim capex As New clsApex
                            hdnBriefID.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)
                            Dim jno As String = capex.GetJobCardNoByJobCardID(hdnJobCardID.Value)
                            If jno <> "" Then
                                If hdnJobCardID.Value <> "" Then
                                    If isDatatransfer() = False Then
                                        transferData_prepnl_To_postpnl()
                                    End If
                                    Dim postpnl As Integer = 0
                                    Dim sql1 As String = "Select jobcompleted  From APEX_JobCard where IsPostPnL is not null and JobCardID=" & hdnJobCardID.Value
                                    Dim ds1 As New DataSet
                                    ds1 = ExecuteDataSet(sql1)
                                    If ds1.Tables(0).Rows.Count > 0 Then
                                        If ds1.Tables(0).Rows(0)("jobcompleted").ToString() = "Y" Then
                                            postpnl = 1
                                        Else
                                            postpnl = 0
                                        End If
                                    Else
                                        postpnl = 0
                                    End If

                                    FillPrePnlDetails()
                                    bindprepnldetail(hdnBriefID.Value)
                                    GetPostPnLID()
                                    FillDetails()
                                    BindGrid()
                                    gvDisplay.DataSource = Nothing
                                    gvDisplay.DataBind()
                                    lblClient.Visible = False
                                    lblEventName.Visible = False
                                    lblEventDate.Visible = False
                                    lblEventVenue.Visible = False
                                    lblApprovalNo.Visible = False
                                    lblCreditPeriod.Visible = False
                                    lblPostEventQuote.Visible = False
                                    lblTCost.Visible = False
                                    lblTSTax.Visible = False
                                    lblPETotal.Visible = False
                                    lblPostEPr.Visible = False
                                    lblfulpl_Mail.Visible = False
                                    FillClient()
                                    selectClientID()
                                    If postpnl = 1 Then
                                        BindGrid()
                                        CloseAllControls()
                                        gvPostPnLCost.Visible = True
                                        gvPostPnLCost.Enabled = False
                                        gvDisplay.Visible = True
                                        btnReset.Visible = False
                                        btnAdd.Visible = False
                                        btnHomeCancel.Visible = True
                                        btnTblCancel.Visible = False
                                        'gvPostPnLCost.Enabled = False
                                        'btnFinallize.Visible = True
                                    End If
                                    If Len(Request.QueryString("nid")) > 0 Then
                                        If Request.QueryString("nid") <> Nothing Then

                                            btnTblCancel.Visible = True
                                            hdnNodificationID.Value = Request.QueryString("nid")
                                            capex.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())

                                            If IsPnLCreate() = True Then
                                                trAppButtons.Visible = False
                                                BindGridAfter()
                                                btnAdd.Visible = False
                                                btnFinallize.Visible = False
                                                CloseAllControls()
                                                btnReset.Visible = False
                                            Else

                                                btnAdd.Visible = True
                                                btnFinallize.Visible = False
                                                btnReset.Visible = True
                                            End If
                                        End If
                                    End If
                                    If Len(Request.QueryString("mode")) > 0 Then
                                        If Request.QueryString("mode") <> "" Then
                                            btnHomeCancel.Visible = True
                                            btnTblCancel.Visible = False

                                        End If
                                    End If

                                    If CheckNotification() = True Then
                                        Dim capex1 As New clsApex
                                        capex1.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())
                                    End If

                                    If capex.GetRoleNameByUserID(getLoggedUserID) = "K" Then
                                        CloseAllControls()
                                        ' btnAdd.Visible = False
                                        btnReset.Visible = False
                                    Else
                                        btnTblCancel.Visible = False
                                        btnHomeCancel.Visible = False

                                    End If
                                Else
                                    Response.Redirect("JobCardManager.aspx?jid=" & hdnJobCardID.Value)
                                End If
                            Else
                                Response.Redirect("JobCardManager.aspx?jid=" & hdnJobCardID.Value)
                            End If
                        Else
                            CallDivError()
                        End If
                    Else
                        CallDivError()
                    End If
                Else
                    CallDivError()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            Dim sql As String = ""

            sql = "Select ROW_NUMBER() OVER(ORDER BY PostPnLCostID DESC) AS Row, * from Apex_PostPnLCost where RefJobCardID=" & hdnJobCardID.Value & " "
            sql &= "  union "
            sql &= "  Select ROW_NUMBER() OVER(ORDER BY PostPnLCostID DESC) AS Row,* from Apex_tempPostPnLCost where RefJobCardID=" & hdnJobCardID.Value & "  order by PostPnLCostID Asc"

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            gvPostPnLCost.DataSource = ds
            gvPostPnLCost.DataBind()
            'gvDisplay.DataSource = ds
            'gvDisplay.DataBind()

            If gvPostPnLCost.Rows.Count = 0 And Not gvPostPnLCost.DataSource Is Nothing Then
                Dim dt As Object = Nothing
                If gvPostPnLCost.DataSource.GetType Is GetType(Data.DataSet) Then
                    dt = New System.Data.DataSet
                    dt = CType(gvPostPnLCost.DataSource, System.Data.DataSet).Tables(0).Clone()
                ElseIf gvPostPnLCost.DataSource.GetType Is GetType(Data.DataTable) Then
                    dt = New System.Data.DataTable
                    dt = CType(gvPostPnLCost.DataSource, System.Data.DataTable).Clone()
                ElseIf gvPostPnLCost.DataSource.GetType Is GetType(Data.DataView) Then
                    dt = New System.Data.DataView
                    dt = CType(gvPostPnLCost.DataSource, System.Data.DataView).Table.Clone
                End If
                dt.Rows.Add(dt.NewRow())
                gvPostPnLCost.DataSource = dt
                gvPostPnLCost.DataBind()
                gvPostPnLCost.Rows(0).Visible = False
                gvPostPnLCost.Rows(0).Controls.Clear()
            Else

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Function IsPnLCreate() As Boolean

        Dim flag As Boolean = False
        Try
            Dim sql As String = ""
            sql = "Select IsPostPnl from APEX_JobCard where refJobCardID=" & hdnJobCardID.Value & " and IsActive='Y' "
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("IsPostPnl")) Then
                    If ds.Tables(0).Rows(0)("IsPostPnl").ToString() = "Y" Then
                        flag = True
                    End If
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return flag
    End Function
    Private Sub FillClient()
        Try
            ddlClient.DataSource = Nothing
            ddlClient.DataBind()

            Dim sql As String = "Select ClientID,Client from APEX_Clients where IsActive='Y' and IsDeleted='N' Order By Client"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlClient.DataSource = ds
                ddlClient.DataTextField = "Client"
                ddlClient.DataValueField = "ClientID"
                ddlClient.DataBind()
            End If
            ddlClient.Items.Insert(0, New ListItem("Select", "0"))
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub ResetAllControls()
        Try
            txtPostEventQuote.Text = ""
            lblTotalCost.Text = ""
            lblTotalServiceTax.Text = ""
            lblPostEventTotal.Text = ""
            txtEventName.Text = ""
            txtEventDate.Text = ""
            txtEventVenue.Text = ""
            txtApprovalNo.Text = ""
            txtCreditPeriod.Text = ""
            divError.Visible = False
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillPostPnLCost()
        Try
            Dim sql As String = "Select ROW_NUMBER() OVER(ORDER BY PostPnLCostID DESC) AS Row,* from APEX_tempPostPnLCost where RefBriefID=" & hdnBriefID.Value & "   order by PostPnLCostID Asc"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvPostPnLCost.DataSource = ds
                gvPostPnLCost.DataBind()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillTotalQoute()
        Try
            Dim str As String = ""
            Dim sql As String = "Select Sum(PostEventCost) as SumPostEventCost,Sum(PostEventServiceTax) as PostEventServiceTax,Sum(PostEventTotal) as PostEventTotal  from APEX_tempPostPnLCost where RefBriefID=" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                lblTotalCost.Text = ds.Tables(0).Rows(0)("SumPostEventCost").ToString()
                If lblTotalCost.Text = "" Then
                    lblTotalCost.Text = "0"
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub UpdateIsPostpnl()
        Try
            If hdnPnLID.Value <> "" Then
                If hdnBriefID.Value <> "" Then
                    Dim sql1 As String = "Update APEX_JobCard set IsPostPnL = 'Y', RefPostPnLID=" & hdnPnLID.Value & " where JobCardID = " & hdnJobCardID.Value
                    ExecuteNonQuery(sql1)

                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnFinallize_Click(sender As Object, e As EventArgs) Handles btnFinallize.Click
        Try
            Dim clsapex As New clsApex
            clsapex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            CloseAllControls()
            TransferTableData()
            Dim sql As String = "Update APEX_JobCard set IsPostPnL = 'Y',RefPostPnLID=" & hdnPnLID.Value & " where RefJobCardID = " & hdnJobCardID.Value
            If ExecuteNonQuery(sql) > 0 Then
                Dim sql1 As String = "Delete from APEX_tempPostPnLCost where RefPostPnLID=" & hdnPnLID.Value
                If ExecuteNonQuery(sql1) > 0 Then
                    Dim capex As New clsApex
                    capex.UpdateStageLevel("9", hdnJobCardID.Value)
                    capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
                    SendNotification_FMInv()
                    Response.Redirect("JobCard.aspx?mode=cj")

                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Public Sub SendNotification_FMInv()
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
            sql &= "('View Job Invoice'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>View Job Invoice'"
            sql &= ",'H'"
            sql &= "," & NotificationType.FMCreateInvoice
            sql &= "," & hdnJobCardID.Value
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails_FMInv()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub InsertRecieptentDetails_FMInv()
        Try
            Dim capex As New clsApex
            Dim title As String = ""
            Dim message As String = ""
            Dim notificationid As String = ""
            Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & NotificationType.FMCreateInvoice & " and AssociateID=" & hdnJobCardID.Value
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

            'sql3 = "Select insertedBy from  APEX_Brief  where BriefID= " & hdnBriefID.Value
            'Dim ds3 As New DataSet
            'ds3 = ExecuteDataSet(sql3)
            'If ds3.Tables(0).Rows.Count > 0 Then
            '    bheadid = ds3.Tables(0).Rows(0)(0).ToString()
            'End If
            bheadid = capex.GetFinanceManagerOfJobCard(hdnJobCardID.Value)
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
                Dim uid As Integer = Convert.ToInt32(bheadid)
                emailid = GetEmailIDByUserID(uid)
                If emailid <> "" Then
                    sendMail(title, message, "", emailid, "")
                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function getProfitPercentage() As String
        Dim result As String = ""
        Try
            Dim sql As String = "Select PostProfitPercent from APEX_PostPnL where PostPnLID=" & hdnPnLID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = ds.Tables(0).Rows(0)(0).ToString()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function
    Private Sub TransferTableData()
        Try
            Dim sql As String = "INSERT INTO APEX_PostPnLCost SELECT [RefPostPnLID],[RefBriefID]"
            sql &= ",[NatureOfExpenses]"
            sql &= ",[SupplierName]"
            sql &= ",[PostEventCost]"
            sql &= ",[PostEventServiceTax]"
            sql &= ",[PostEventTotal]"
            sql &= ",[IsActive]"
            sql &= ",[IsDeleted]"
            sql &= ",[InsertedOn]"
            sql &= ",[InsertedBy]"
            sql &= ",[ModifiedOn]"
            sql &= ",[ModifiedBy],[Category] FROM APEX_tempPostPnLCost where RefPostPnLID=" & hdnPnLID.Value
            If ExecuteNonQuery(sql) > 0 Then

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub transferData_prepnl_To_postpnl()

        Dim sql As String = "insert into APEX_postpnl(refBriefID,RefClientID,EventName,Eventdate,Eventvenue,pnlApprovalNo,CreditPeriod,PostEventQuote,postProfit,postProfitPercent"
        sql &= " ,IsOperationHeadApproved,OperationHeadApprovedOn,IsBranchHeadApproved,BranchHeadApprovedOn,IsActive)"
        sql &= " Select refBriefID,RefClientID,EventName,Eventdate,Eventvenue,pnlApprovalNo,CreditPeriod,PreEventQuote,PreProfit,PreProfitPercent"
        sql &= " ,IsOperationHeadApproved,OperationHeadApprovedOn,IsBranchHeadApproved,BranchHeadApprovedOn,IsActive from APEX_Prepnl"
        sql &= " where refbriefID=" & hdnBriefID.Value

        If ExecuteNonQuery(sql) > 0 Then
            sql = ""
            sql = "update Apex_PostPnL set  RefjobcardID=" & hdnJobCardID.Value & " where REfBriefID=" & hdnBriefID.Value
            Dim apx As New clsApex
            ExecuteNonQuery(sql)
            Dim postpnlid As String = apx.GetPostLIDByBriefID(hdnBriefID.Value)
            sql = ""
            sql = "insert into APEX_tempPostpnlcost(refpostpnlID,REfBriefID,NatureofExpenses,SupplierName,PostEventCost,POstEventServiceTax,PostEventTotal,Category,Quantity,Rate,Days)"
            sql &= " Select '" & postpnlid & "',refBriefID,NatureofExpenses,SupplierName,PreEventCost,PreEventServiceTax,PreEventTotal,Category,Quantity,Rate,Days "
            sql &= " from APEX_Prepnlcost"
            sql &= " where refbriefID = " & hdnBriefID.Value
            If ExecuteNonQuery(sql) > 0 Then
                sql = ""
                sql = "update Apex_tempPostPnLCost set  RefjobcardID=" & hdnJobCardID.Value & " where REfBriefID=" & hdnBriefID.Value
                ExecuteNonQuery(sql)
            End If
        End If

    End Sub
    Private Function isDatatransfer() As String
        Dim flag As Boolean = False
        If hdnJobCardID.Value <> "" Then
            Dim sql As String = "Select count(*)cnt from APEX_PostPnL where RefJobCardID=" & hdnJobCardID.Value
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("cnt") > 0 Then
                    flag = True
                End If
            End If
        End If

        Return flag
    End Function
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            GetPostPnLID()
            Dim profitpercent As Double = vbNull
            Dim apex As New clsApex
            If hdnNodificationID.Value <> "" Then
                apex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            End If

            Dim PostEventProfit As Double = vbNull
            lblPostEventTotal.Text = lblTotalCost.Text
            If lblPostEventTotal.Text.Length > 0 Then
                Dim sql As String = "Update APEX_PostPnL set "
                If txtPostEventQuote.Text = "0.00" Then
                    txtPostEventQuote.Text = "0"
                End If
                If txtPostEventQuote.Text <> "" Then

                    sql &= "PostEventQuote = " & Clean(txtPostEventQuote.Text)
                Else
                    txtPostEventQuote.Text = "0"
                    sql &= "PostEventQuote = 0"
                End If
                If txtPostEventQuote.Text <> "" And lblTotalCost.Text <> "" Then

                    PostEventProfit = (Convert.ToDouble(txtPostEventQuote.Text) - Convert.ToDouble(lblTotalCost.Text)).ToString()
                End If
                If PostEventProfit <> vbNull Then

                    sql &= ",PostProfit= " & PostEventProfit


                Else
                    sql &= ",PostProfit= NULL"
                End If

                If PostEventProfit <> vbNull Then
                    If txtPostEventQuote.Text <> "0" Then
                        profitpercent = Math.Round((((PostEventProfit) / Convert.ToDouble(txtPostEventQuote.Text)) * 100), 2)
                    Else
                        profitpercent = 0
                    End If

                    sql &= ",PostProfitPercent= " & profitpercent
                Else

                    sql &= ",PostProfitPercent= NULL"
                End If

                sql &= ",EventName="

                If txtEventName.Text <> "" Then
                    sql &= "'" & Clean(txtEventName.Text) & "'"
                Else
                    sql &= "NULL"
                End If

                sql &= ",EventDate="
                If txtEventDate.Text <> "" Then
                    sql &= "convert(datetime,'" & txtEventDate.Text & "',105)"
                Else
                    sql &= "NULL"
                End If

                sql &= ",EventVenue="
                If txtEventVenue.Text <> "" Then
                    sql &= "'" & Clean(txtEventVenue.Text) & "'"
                Else
                    sql &= "NULL"
                End If
                If txtApprovalNo.Text <> "" Then
                    sql &= ",PnLApprovalNo='" & Clean(txtApprovalNo.Text) & "'"
                Else
                    sql &= ",PnLApprovalNo=NULL"
                End If

                If fupl_Mail.HasFile = True Then
                    Dim filename As String = fupl_Mail.FileName
                    Dim fname As String = filename.Substring(0, filename.LastIndexOf("."))
                    Dim fext As String = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - (filename.LastIndexOf(".") + 1))
                    Dim encname As String = ""
                    'txtUploads.Text = fname
                    Dim Path As String
                    encname = fname & DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date.Day & DateTime.Now.Date.Hour & DateTime.Now.Date.Minute & DateTime.Now.Date.Second
                    fupl_Mail.SaveAs(Server.MapPath("Collateral/PrePnL/" & encname & "." & fext))
                    Path = "Collateral/PrePnL/" & encname & "." & fext

                    sql &= ",PLApprovalMail='" & Path & "'"
                Else
                    sql &= ",PLApprovalMail=NULL"
                End If
                If txtCreditPeriod.Text <> "" Then
                    sql &= ",CreditPeriod=" & Clean(txtCreditPeriod.Text)
                Else
                    sql &= ",CreditPeriod=NULL"
                End If

                sql &= ",IsOperationHeadApproved= 'Y'"
                sql &= ",OperationHeadApprovedOn = getdate()"
                sql &= ",IsBranchHeadApproved = 'Y'"
                sql &= ",BranchHeadApprovedOn = getdate()"
                sql &= " where RefJobCardID=" & hdnJobCardID.Value & " and PostPnLID=" & hdnPnLID.Value

                If checkpostpnllines() Then
                    If checkprepnlwithclaim(hdnJobCardID.Value, lblTotalCost.Text) Then

                    Else
                        lblError.Text = "Your cannot create a Post PnL with amount less than Claimed Amount."
                        divError.Visible = True
                        Exit Sub
                    End If


                    If ExecuteNonQuery(sql) > 0 Then
                            If Len(Request.QueryString("mode")) > 0 Then
                                Dim capex12 As New clsApex
                                Dim role As String
                                role = capex12.GetRoleName(getLoggedUserName())
                            If role = "K" Or role = "H" Then
                                If Request.QueryString("mode").ToString() = "view" Then
                                    UpdateIsPostpnl()
                                    Response.Redirect("Estimate_VS_Actuals.aspx?jid=" & hdnJobCardID.Value)
                                    'SendNotification_KAmforInv()
                                End If
                                btnFinallize.Visible = True
                            Else
                                SendNotification_KAmforInv()
                                    divContent.Visible = False
                                    Label1.Text = "Post P&L Generated Request Sent To KAM"
                                    MessageDiv.Visible = True
                                End If

                            Else
                                SendNotification_KAmforInv()
                                divContent.Visible = False
                                Label1.Text = "Post P&L Generated Request Sent To KAM"
                                MessageDiv.Visible = True

                            End If

                        End If
                    Else
                        divError.Visible = True
                    lblError.Text = "Please add postpnl items before save postpnl."
                    Exit Sub
                End If
                FillDetails()
                FillPostEventDetails()
                BindGrid()
                CloseAllControls()
                btnReset.Visible = False
                btnAdd.Visible = False
                btnFinallize.Visible = True
                gvDisplay.Visible = True
                gvPostPnLCost.Enabled = False


                'Response.Redirect("Home.aspx")
            End If

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Public Function checkprepnlwithclaim(ByVal jobcardID As Integer, ByVal postpnlamount As Decimal) As Boolean
        Dim retval As Boolean = False
        Dim totalclaim As Decimal = Nothing
        Dim sql As String = " select isnull(Sum(amount),0) from [dbo].[APEX_ClaimTransaction] CT"
        sql &= " inner join APEX_ClaimMaster CM on CM.claimMasterID=CT.RefClaimID"
        sql &= " where jobcardNo=" & jobcardID & " and isapproved='Y'"
        totalclaim = ExecuteSingleResult(sql, _DataType.AlphaNumeric)

        If CDbl(postpnlamount) < totalclaim Then
            retval = False
        Else
            retval = True
        End If

        Return retval
    End Function


    Private Sub CloseAllControls()
        Try
            ddlClient.Visible = False
            txtEventName.Visible = False
            txtEventDate.Visible = False
            txtEventVenue.Visible = False
            txtApprovalNo.Visible = False
            txtCreditPeriod.Visible = False
            txtPostEventQuote.Visible = False
            lblTotalCost.Visible = False
            lblTotalServiceTax.Visible = False
            lblPostEventTotal.Visible = False
            lblPostEventProfit.Visible = False
            fupl_Mail.Visible = False

            lblClient.Visible = True
            lblEventName.Visible = True
            lblEventDate.Visible = True
            lblEventVenue.Visible = True
            lblApprovalNo.Visible = True
            lblCreditPeriod.Visible = True
            lblPostEventQuote.Visible = True
            lblPostEPr.Visible = True
            lblTCost.Visible = True
            lblTSTax.Visible = True
            lblPETotal.Visible = True

            If lblfulpl_Mail.NavigateUrl <> "" Then
                lblfulpl_Mail.Visible = True
            Else
                lblfulpl_Mail.Visible = False
            End If
            gvPostPnLCost.Enabled = False
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            ResetAllControls()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillDetails()
        Try
            Dim sql As String = ""

            sql = "Select convert(varchar(10),EventDate,105) as NEventDate,* from APEX_PostPnL where RefJobCardID=" & hdnJobCardID.Value & " and PostPnLID="
            If hdnPnLID.Value <> "" Then
                sql &= hdnPnLID.Value
            Else
                sql &= "NULL"
            End If
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)

            If ds.Tables(0).Rows.Count > 0 Then
                FillClient()
                ddlClient.SelectedIndex = ddlClient.Items.IndexOf(ddlClient.Items.FindByValue(ds.Tables(0).Rows(0)("RefClientID").ToString()))

                Dim eventdate As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0)("EventName")) Then
                    txtEventName.Text = ds.Tables(0).Rows(0)("EventName").ToString()

                Else
                    txtEventName.Text = getActivityName(hdnBriefID.Value)

                End If

                If Not IsDBNull(ds.Tables(0).Rows(0)("NEventDate")) Then
                    txtEventDate.Text = ds.Tables(0).Rows(0)("NEventDate").ToString()
                Else
                    txtEventDate.Text = getEventdate(hdnBriefID.Value)
                End If


                txtEventVenue.Text = ds.Tables(0).Rows(0)("EventVenue").ToString()
                txtApprovalNo.Text = ds.Tables(0).Rows(0)("PnLApprovalNo").ToString()
                txtCreditPeriod.Text = ds.Tables(0).Rows(0)("CreditPeriod").ToString()
                txtPostEventQuote.Text = ds.Tables(0).Rows(0)("PostEventQuote").ToString()

                lblClient.Text = ddlClient.SelectedItem.Text
                lblEventName.Text = ds.Tables(0).Rows(0)("EventName").ToString()
                lblEventDate.Text = ds.Tables(0).Rows(0)("NEventDate").ToString()
                lblEventVenue.Text = ds.Tables(0).Rows(0)("EventVenue").ToString()
                lblApprovalNo.Text = ds.Tables(0).Rows(0)("PnLApprovalNo").ToString()
                lblCreditPeriod.Text = ds.Tables(0).Rows(0)("CreditPeriod").ToString()
                lblPostEventQuote.Text = ds.Tables(0).Rows(0)("PostEventQuote").ToString()

                If ds.Tables(0).Rows(0)("PostProfitPercent").ToString() <> "" Then
                    lblPostEventProfit.Text = ds.Tables(0).Rows(0)("PostProfitPercent").ToString()

                    lblPostEPr.Text = ds.Tables(0).Rows(0)("PostProfitPercent").ToString()
                Else
                    Dim sql_db As String = ""
                    Dim ds_db As New DataSet

                End If
                If ds.Tables(0).Rows(0)("PLApprovalMail").ToString() <> "" Then
                    lblfulpl_Mail.NavigateUrl = ds.Tables(0).Rows(0)("PLApprovalMail").ToString()
                Else
                    lblfulpl_Mail.Visible = False
                End If


                FillPostPnLCost()
                If CheckJobcardCreated() = True Then
                    FillTotalQoute()
                Else
                    FillTotalQouteAfter()
                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub selectClientID()
        Try
            Dim sql As String = "Select RefClientID from APEX_Brief where BriefID=" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlClient.SelectedIndex = ddlClient.Items.IndexOf(ddlClient.Items.FindByValue(ds.Tables(0).Rows(0)(0).ToString()))
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvPostPnLCost_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvPostPnLCost.RowCreated

    End Sub

    Protected Sub gvPostPnLCost_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPostPnLCost.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim category As String()
                Dim ddlCategory As DropDownList = e.Row.FindControl("ddlCategory")
                Dim sql As String = "select RefActivityTypeID from APEX_Brief where BriefID=" & hdnBriefID.Value
                Dim ds As New DataSet
                ds = ExecuteDataSet(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    category = (ds.Tables(0).Rows(0)(0).ToString()).Split("|")
                    ddlCategory.DataSource = category
                    ddlCategory.DataBind()

                End If
                ddlCategory.Items.Insert(0, New ListItem("Select", "0"))
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.DataItem IsNot Nothing Then
                    If (e.Row.RowState And DataControlRowState.Edit) > 0 Then
                        Dim ddlFormat As DropDownList = e.Row.FindControl("gv_ddlCategory")
                        Dim hdnPostPnLCostID As HiddenField = e.Row.FindControl("hdnPostPnLCostID")
                        ' HdnIncharge.Value = ddlFormat.DataValueField
                        Dim sqlBindstring As String = "Select Category from APEX_tempPostPnLCost where PostPnLCostID=" & hdnPostPnLCostID.Value
                        Dim sql As String = "select RefActivityTypeID from APEX_Brief where BriefID=" & hdnBriefID.Value
                        ddlFormat.Items.Clear()
                        Dim ds1 As New DataSet
                        ds1 = ExecuteDataSet(sql)
                        Dim ds As New DataSet
                        ddlFormat.DataValueField = ""
                        ds = ExecuteDataSet(sqlBindstring)
                        ddlFormat.DataSource = (ds1.Tables(0).Rows(0)(0).ToString()).Split("|")
                        ddlFormat.DataBind()
                        ddlFormat.Items.Insert(0, New ListItem("Select", "0"))
                        ddlFormat.SelectedIndex = ddlFormat.Items.IndexOf(ddlFormat.Items.FindByText(ds.Tables(0).Rows(0)(0).ToString()))
                    End If
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvPostPnLCost_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvPostPnLCost.RowDeleting

    End Sub

    Private Sub UpdateTempPLID()
        Try
            Dim sql As String = "Update APEX_tempPostPnLCost set RefPostPnLID=" & hdnPnLID.Value & " where RefJobCardID=" & hdnJobCardID.Value

            ExecuteNonQuery(sql)
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function CheckJobcardCreated() As Boolean
        Dim result As Boolean = False
        Try
            Dim sql As String = "Select jobcardNo from APEX_JobCard where RefBriefID=" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("jobcardNo")) Then
                    result = True
                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Private Sub GetPostPnLID()
        Try
            Dim sql As String = "Select PostPnLID from APEX_PostPnL where RefJobCardID=" & hdnJobCardID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)

            If ds.Tables(0).Rows.Count > 0 Then
                hdnPnLID.Value = ds.Tables(0).Rows(0)(0).ToString()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnTblCancel_Click(sender As Object, e As EventArgs) Handles btnTblCancel.Click
        Try
            Response.Redirect("JobCard.aspx?mode=rj")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindGridAfter()
        Try
            Dim sql As String = "Select  ROW_NUMBER() OVER(ORDER BY PostPnLCostID ) AS Row,* from APEX_tempPostPnLCost where RefJobCardID=" & hdnJobCardID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            gvPostPnLCost.DataSource = ds
            gvPostPnLCost.DataBind()
            gvDisplay.DataSource = ds
            gvDisplay.DataBind()
            If gvPostPnLCost.Rows.Count = 0 And Not gvPostPnLCost.DataSource Is Nothing Then
                Dim dt As Object = Nothing
                If gvPostPnLCost.DataSource.GetType Is GetType(Data.DataSet) Then
                    dt = New System.Data.DataSet
                    dt = CType(gvPostPnLCost.DataSource, System.Data.DataSet).Tables(0).Clone()
                ElseIf gvPostPnLCost.DataSource.GetType Is GetType(Data.DataTable) Then
                    dt = New System.Data.DataTable
                    dt = CType(gvPostPnLCost.DataSource, System.Data.DataTable).Clone()
                ElseIf gvPostPnLCost.DataSource.GetType Is GetType(Data.DataView) Then
                    dt = New System.Data.DataView
                    dt = CType(gvPostPnLCost.DataSource, System.Data.DataView).Table.Clone
                End If
                dt.Rows.Add(dt.NewRow())
                gvPostPnLCost.DataSource = dt
                gvPostPnLCost.DataBind()
                gvPostPnLCost.Rows(0).Visible = False
                gvPostPnLCost.Rows(0).Controls.Clear()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillTotalQouteAfter()
        Try
            Dim sql As String = "Select Sum(PostEventCost) as SumPostEventCost,Sum(PostEventServiceTax) as PostEventServiceTax,Sum(PostEventTotal) as PostEventTotal  from APEX_tempPostPnLCost where RefJobCardID=" & hdnJobCardID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                lblTotalCost.Text = ds.Tables(0).Rows(0)("SumPostEventCost").ToString()
                'lblTotalServiceTax.Text = ds.Tables(0).Rows(0)("PostEventServiceTax").ToString()
                lblPostEventTotal.Text = ds.Tables(0).Rows(0)("PostEventTotal").ToString()
                'lblServiceTaxAmount.Text = ds.Tables(0).Rows(0)("PostEventServiceTax").ToString()

                If lblTotalCost.Text = "" Then
                    lblTotalCost.Text = "0"
                End If

                If lblPostEventTotal.Text = "" Then
                    lblPostEventTotal.Text = "0"
                End If
                If lblTotalCost.Text = "" Then
                    lblTotalCost.Text = "0"
                End If

                lblTCost.Text = ds.Tables(0).Rows(0)("SumPostEventCost").ToString()

                lblPETotal.Text = ds.Tables(0).Rows(0)("PostEventTotal").ToString()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvPostPnLCost_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPostPnLCost.PageIndexChanging
        Try
            gvPostPnLCost.PageIndex = e.NewPageIndex
            BindGrid()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvDisplay_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvDisplay.PageIndexChanging
        Try
            gvDisplay.PageIndex = e.NewPageIndex
            BindGrid()
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
    Private Function getActivityName(ByVal bid As String) As String

        Dim Activityname As String = ""
        Try
            Dim sql As String = "Select BriefName from APEX_Brief where BriefID=" & bid & ""
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("BriefName")) Then
                    Activityname = ds.Tables(0).Rows(0)("BriefName").ToString()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return Activityname
    End Function


    Private Function getEventdate(ByVal Bid As String) As String
        Dim EventDate As String = ""
        Try
            Dim sql As String = "Select convert(varchar(10),Activitydate,105) as Adate from APEX_Brief where BriefID=" & Bid & ""
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("Adate")) Then
                    EventDate = ds.Tables(0).Rows(0)("Adate").ToString()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return EventDate
    End Function

    Protected Sub gvPostPnLCost_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvPostPnLCost.RowEditing
        Try
            gvPostPnLCost.EditIndex = e.NewEditIndex
            'Rebind the GridView to show the data in edit mode
            BindGrid()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvPostPnLCost_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvPostPnLCost.RowUpdating
        'Accessing Edited values from the GridView
        Try
            Dim PostPnLCostID As New HiddenField
            PostPnLCostID = DirectCast(gvPostPnLCost.Rows(e.RowIndex).FindControl("hdnPostPnLCostID"), HiddenField)
            Dim Nature As New TextBox
            Dim Supplier As New TextBox
            Dim PostEventCost As New TextBox
            Dim Category As New DropDownList

            Dim Quantity As New TextBox
            Dim rate As New TextBox
            Dim days As New TextBox


            Nature = DirectCast(gvPostPnLCost.Rows(e.RowIndex).FindControl("gv_txtNatureOfExpenses"), TextBox)
            Category = DirectCast(gvPostPnLCost.Rows(e.RowIndex).FindControl("gv_ddlCategory"), DropDownList)
            Supplier = DirectCast(gvPostPnLCost.Rows(e.RowIndex).FindControl("gv_txtSupplierName"), TextBox)
            PostEventCost = DirectCast(gvPostPnLCost.Rows(e.RowIndex).FindControl("gv_txtPostEventCost"), TextBox)

            Quantity = DirectCast(gvPostPnLCost.Rows(e.RowIndex).FindControl("gv_txtquantity"), TextBox)
            rate = DirectCast(gvPostPnLCost.Rows(e.RowIndex).FindControl("gv_txtRate"), TextBox)
            days = DirectCast(gvPostPnLCost.Rows(e.RowIndex).FindControl("gv_txtDays"), TextBox)

            Dim d2, d3, d4, d5, d6, d7 As String
            Dim d1 As String = PostPnLCostID.Value
            If Nature.Text <> "" Then
                d2 = Clean(Nature.Text)
            End If
            If Supplier.Text <> "" Then
                d3 = Clean(Supplier.Text)
            End If
            If PostEventCost.Text <> "" Then
                d4 = Clean(PostEventCost.Text)
            End If
            d5 = 0
            d6 = d4
            If Category.SelectedIndex > 0 Then
                d7 = Category.SelectedItem.Text
            End If


            'Call the function to update the GridView
            UpdateRecord(d1, d2, d3, d4, d5, d6, d7, Quantity.Text, rate.Text, days.Text)

            gvPostPnLCost.EditIndex = -1

            'Rebind Gridview to reflect changes made
            BindGrid()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Protected Sub gvPostPnLCost_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvPostPnLCost.RowCancelingEdit
        Try
            ' switch back to edit default mode
            gvPostPnLCost.EditIndex = -1
            'Rebind the GridView to show the data in edit mode
            BindGrid()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub UpdateRecord(ByVal PostPnLCostID As String, ByVal Nature As String, ByVal Supplier As String, ByVal PostEventCost As String, ByVal PostEventServiceTax As String, ByVal PostEventTotal As String, ByVal Category As String, ByVal qty As String, ByVal rate As String, ByVal days As String)
        Try
            Dim sql As String = ""
            sql &= "UPDATE [APEX_tempPostPnLCost]"
            sql &= "  SET [RefPostPnLID] = " & hdnPnLID.Value & ""
            sql &= ",[NatureOfExpenses] = '" & Clean(Nature) & "'"
            'sql &= "  ,[SupplierName] = '" & Clean(Supplier) & "'"
            PostEventCost = (Convert.ToDouble(qty) * Convert.ToDouble(rate) * Convert.ToDouble(days)).ToString()
            sql &= " ,[PostEventCost] = '" & Clean(PostEventCost) & "'"
            'sql &= "  ,[PostEventServiceTax] = '" & Clean(PostEventServiceTax) & "'"
            'sql &= "  ,[PostEventTotal] = '" & Clean(PostEventTotal) & "'"
            sql &= "  ,[Category] = '" & Category & "'"


            sql &= "  ,[Quantity] = '" & qty & "'"
            sql &= "  ,[Rate] = '" & rate & "'"
            sql &= "  ,[Days] = '" & days & "'"

            sql &= " WHERE PostPnLCostID=" & PostPnLCostID


            ExecuteNonQuery(sql)

            If ExecuteNonQuery(sql) > 0 Then
                FillPostPnLCost()
                FillTotalQoute()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try

    End Sub


    Protected Sub gvPostPnLCost_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPostPnLCost.RowCommand
        Try
            If e.CommandName = "Add" Then
                gvDisplay.Visible = False
                Dim totalquote As Double = 0

                Dim frow As GridViewRow = gvPostPnLCost.FooterRow
                Dim txtNature As New TextBox
                Dim txtSupplier As New TextBox
                Dim txtPostEventCost As New TextBox
                'Dim txtPostEventServiceTax As New TextBox
                'Dim txtPostEventTotal As New TextBox
                Dim ddlCategory As New DropDownList
                Dim txtDays As New TextBox
                Dim txtquantity As New TextBox
                Dim txtRate As New TextBox

                txtNature = CType(frow.FindControl("txtNature"), TextBox)
                txtSupplier = CType(frow.FindControl("txtSupplier"), TextBox)
                txtPostEventCost = CType(frow.FindControl("txtPostEventCost"), TextBox)
                'txtPostEventServiceTax = CType(frow.FindControl("txtPostEventServiceTax"), TextBox)
                'txtPostEventTotal = CType(frow.FindControl("txtPostEventTotal"), TextBox)
                ddlCategory = CType(frow.FindControl("ddlCategory"), DropDownList)


                txtDays = CType(frow.FindControl("txtDays"), TextBox)
                txtquantity = CType(frow.FindControl("txtquantity"), TextBox)
                txtRate = CType(frow.FindControl("txtRate"), TextBox)

                txtPostEventCost.Text = (Convert.ToDouble(txtDays.Text) * Convert.ToDouble(txtquantity.Text) * Convert.ToDouble(txtRate.Text)).ToString()

                Dim sql As String = "Insert into APEX_tempPostPnLCost (RefPostPnLID,RefBriefID,NatureOfExpenses,SupplierName,PostEventCost,PostEventServiceTax,PostEventTotal,Category,RefJobCardID,[Quantity],[Rate],[Days]) Values ("
                If hdnPnLID.Value <> "" Then
                    sql &= hdnPnLID.Value
                Else
                    sql &= "NULL"

                End If
                sql &= "," & hdnBriefID.Value
                sql &= ",'" & Clean(txtNature.Text) & "'"
                sql &= ",'" & Clean(txtSupplier.Text) & "'"
                sql &= "," & Clean(txtPostEventCost.Text)
                sql &= ",0"
                'If txtPostEventCost.Text <> "" And txtPostEventServiceTax.Text <> "" Then
                '    Dim tax As Double = (Convert.ToDouble(txtPostEventCost.Text) * Convert.ToDouble(txtPostEventServiceTax.Text)) / 100

                sql &= "," & Clean(Convert.ToDouble(txtPostEventCost.Text))

                'End If

                sql &= ",'" & ddlCategory.SelectedItem.Text & "'"
                sql &= "," & hdnJobCardID.Value

                sql &= "," & Clean(txtquantity.Text) & ""
                sql &= "," & Clean(txtRate.Text)
                sql &= "," & Clean(txtDays.Text)

                sql &= ")"

                If ExecuteNonQuery(sql) > 0 Then
                    BindGrid()
                    'FillPrePnLCost()
                    FillTotalQoute()
                End If
            End If
            If e.CommandName = "Delete" Then
                Dim hdnPostPnLCostID As New HiddenField

                Dim row As GridViewRow = CType(CType(e.CommandSource, Button).NamingContainer, GridViewRow)

                hdnPostPnLCostID = CType(row.FindControl("hdnPostPnLCostID"), HiddenField)

                Dim sql As String = "Delete from APEX_tempPostPnLCost where PostPnLCostID=" & hdnPostPnLCostID.Value
                If ExecuteNonQuery(sql) > 0 Then
                    BindGrid()
                    ' FillPrePnLCost()
                    FillTotalQoute()
                End If
            End If
            If e.CommandName = "Edit" Then
                Dim hdnPostPnLCostID As New HiddenField
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function primaryActivity(ByVal p1 As String) As String
        Dim activity As String = ""
        Try
            Dim sql As String = "Select ProjectType from APEX_ActivityType where projectTypeID=" & p1 & " and isActive='Y'"
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

    Private Function returnClientname(ByVal p1 As String) As String
        Dim client As String = ""
        Try
            Dim sql As String = "Select Client from  APEX_Clients where ClientID=" & p1 & " and isActive='Y'"

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

    Private Function getProjectName() As String
        Dim result As String = ""
        Try
            Dim sql As String = "Select BriefName from APEX_Brief where BriefID=" & hdnBriefID.Value
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
            sql &= " where ab.BriefID = " & hdnBriefID.Value
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



    Public Sub SendNotification_KAmforInv()
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
            sql &= "('Post P&L Generated Please Review'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>View Job Invoice'"
            sql &= ",'H'"
            sql &= "," & NotificationType.viewpostpnl
            sql &= "," & hdnJobCardID.Value
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails_KAMInv()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub


    Private Sub InsertRecieptentDetails_KAMInv()
        Try
            Dim capex As New clsApex
            Dim title As String = ""
            Dim message As String = ""
            Dim notificationid As String = ""
            Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & NotificationType.viewpostpnl & " and AssociateID=" & hdnJobCardID.Value
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

            sql3 = "Select insertedBy from  APEX_Brief  where BriefID= " & hdnBriefID.Value
            Dim ds3 As New DataSet
            ds3 = ExecuteDataSet(sql3)
            If ds3.Tables(0).Rows.Count > 0 Then
                bheadid = ds3.Tables(0).Rows(0)(0).ToString()
            End If
            'bheadid = capex.GetFinanceManagerOfJobCard(hdnJobCardID.Value)
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
                Dim uid As Integer = Convert.ToInt32(bheadid)
                emailid = GetEmailIDByUserID(uid)
                If emailid <> "" Then
                    sendMail(title, message, "", emailid, "")
                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillPostEventDetails()
        Dim sql As String = "select PostProfit from APEX_PostPnl where PostPnlID=" & hdnPnLID.Value

        Dim ds As New DataSet

    End Sub

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

                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("KAM")) Then
                    lblKAM.Text = ds.Tables(0).Rows(0)("KAM").ToString()

                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("Client")) Then
                    lblclient1.Text = ds.Tables(0).Rows(0)("Client")

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

    Protected Sub btnHomeCancel_Click(sender As Object, e As EventArgs) Handles btnHomeCancel.Click
        If Request.QueryString("mode") = "KAMJ" Then
            Response.Redirect("Home.aspx?mode=KAMJ")
        ElseIf Request.QueryString("mode") = "PostPM" Then
            Response.Redirect("Home.aspx?mode=PostPM")
        End If
    End Sub

    Public Function CheckNotification() As Boolean
        Dim result As Boolean = False
        Dim sql As String = "select * from APEX_Notifications "
        sql &= " inner join APEX_Recieptents on RefNotificationID=NotificationID"
        sql &= " where type in (21,22) and AssociateID=" & hdnJobCardID.Value & " and IsExecuted='N' and UserID=" & getLoggedUserID()
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnNodificationID.Value = ds.Tables(0).Rows(0)("NotificationID").ToString()
            result = True
        End If
        Return result
    End Function

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click

        Response.Clear()
        BindGridAfter()
        Response.AddHeader("content-disposition", "attachment; filename=Postpnl" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"
        Dim stringWriter As New StringWriter()
        Dim htmlWriter As New HtmlTextWriter(stringWriter)
        gvDisplay.RenderControl(htmlWriter)
        Response.Write(stringWriter.ToString())
        Response.End()

    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered

    End Sub

    Public Function checkpostpnllines() As Boolean
        Dim sql As String = "select count(1) from APEX_tempPostPnLCost where refbriefID='" & hdnBriefID.Value & "'"
        Dim cnt As Integer = 0
        cnt = ExecuteSingleResult(sql, _DataType.Numeric)
        If cnt > 0 Then
            Return True
        Else
            Return False
        End If
        'APEX_tempPostPnLCost
    End Function
End Class