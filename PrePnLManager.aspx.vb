﻿Imports System.Data
Imports clsMain
Imports clsDatabaseHelper
Imports clsApex
Imports System.Data.OleDb
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System
Imports System.Drawing
Imports System.Data.SqlClient

Partial Class PrePnLManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            'If Not mpLabel Is Nothing Then
            '    Label1.Text = "Master page label = " + mpLabel.Text
            'End If

            trmsg.Visible = False
            btnFinallize.Visible = False

            If Not Page.IsPostBack Then
                lblTotalCost.Text = 0
                lblTotalServiceTax.Text = 0
                lblPreEventTotal.Text = 0

                divError.Visible = False
                MessageDiv.Visible = False
                trAppButtons.Visible = False
                trAppRemarks.Visible = False
                If Len(Request.QueryString("bid")) > 0 Then
                    If Request.QueryString("bid") <> Nothing Then
                        If Request.QueryString("bid").ToString() <> "" Then
                            hdnBriefID.Value = Request.QueryString("bid")
                            BindBriefData(hdnBriefID.Value)
                            If Len(Request.QueryString("Isnotific")) > 0 Then
                                If Request.QueryString("Isnotific") = "N" Then
                                    Label1.Text = "Please fill Pre Event Quote to proceed Estimate generation"
                                    MessageDiv.Visible = True
                                End If
                            End If


                            Dim capex As New clsApex
                            hdnJobCardID.Value = capex.GetJobCardID(hdnBriefID.Value)
                            If hdnJobCardID.Value <> "" Then
                                'If capex.CheckStageLevel("3", hdnJobCardID.Value) = True Then
                                'TWContent.InnerHtml = capex.GetList("3", hdnJobCardID.Value)
                                GetPrePnLID()
                                FillDetails()
                                BindGrid()
                                If gvPrePnLCost.Rows.Count > 0 Then
                                    btnAdd.Visible = True
                                Else
                                    btnAdd.Visible = False
                                End If
                                gvDisplay.DataSource = Nothing
                                gvDisplay.DataBind()
                                lblClient.Visible = False
                                lblEventName.Visible = False
                                lblEventDate.Visible = False
                                lblEventVenue.Visible = False
                                lblApprovalNo.Visible = False
                                lblCreditPeriod.Visible = False
                                lblPreEventQuote.Visible = False
                                lblTCost.Visible = False
                                lblTSTax.Visible = False
                                lblPETotal.Visible = False
                                lblPreEPr.Visible = False
                                lblfulpl_Mail.Visible = False
                                FillClient()
                                selectClientID()
                                If Len(Request.QueryString("nid")) > 0 Then
                                    '  btnnotificationBack.Visible = True
                                    btnTblCancel.Visible = True
                                End If
                                If CheckJobcardCreated() = False Then

                                    'btnAdd.Visible = True
                                    btnFinallize.Visible = False
                                    FillTotalQouteAfter()
                                    If Len(Request.QueryString("mode")) > 0 Then
                                        If Request.QueryString("mode") <> Nothing Then
                                            If Len(Request.QueryString("nid")) > 0 Then
                                                If Request.QueryString("nid") <> Nothing Then
                                                    btnCancelKamHome.Visible = False
                                                    ' btnnotificationBack.Visible = True
                                                    hdnNodificationID.Value = Request.QueryString("nid")
                                                    capex.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())

                                                    'trAppRemarks.Visible = True
                                                    If Len(Request.QueryString("Appro")) > 0 Then
                                                        trAppButtons.Visible = True
                                                        btnFinallize.Visible = True
                                                        BindGridAfter()
                                                        ' FillTotalQoute()
                                                        btnAdd.Visible = False
                                                        btnFinallize.Visible = False
                                                        CloseAllControls()
                                                    End If

                                                End If
                                            End If
                                        End If
                                    End If


                                Else
                                    BindGridAfter()
                                    FillTotalQouteAfter()
                                    btnAdd.Visible = False
                                    btnFinallize.Visible = False
                                    CloseAllControls()

                                    If Len(Request.QueryString("mode")) > 0 Then
                                        If Request.QueryString("mode") <> Nothing Then
                                            If Request.QueryString("mode") = "app" Then
                                                If Len(Request.QueryString("nid")) > 0 Then
                                                    If Request.QueryString("nid") <> Nothing Then
                                                        'btnCancelKamHome.Visible = True
                                                        btnCancelKamHome.Visible = False
                                                        ' btnnotificationBack.Visible = True
                                                        btnTblCancel.Visible = True

                                                        hdnNodificationID.Value = Request.QueryString("nid")
                                                        capex.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())
                                                        trAppRemarks.Visible = True
                                                        trAppButtons.Visible = True
                                                    End If
                                                End If



                                            End If
                                        End If
                                    End If
                                End If
                                'Else
                                'CallDivError()
                                'End If
                            Else
                                CallDivError()
                            End If
                        Else
                            CallDivError()
                        End If
                    Else
                        CallDivError()
                    End If
                    If Len(Request.QueryString("kid")) > 0 Then
                        If Request.QueryString("kid").ToString() <> "" Then
                            If Request.QueryString("kid") = "y" Then
                                btnTblCancel.Visible = False
                                'btnCancelKamHome.Visible = True
                                btnCancelKamHome.Visible = False
                            End If
                        End If
                    End If

                    If CheckNotification() = True Then
                        Dim capex1 As New clsApex
                        capex1.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())
                    End If
                Else
                    CallDivError()
                End If
            End If
            If gvPrePnLCost.Rows.Count > 0 Then
                btnAdd.Visible = True
            Else
                btnAdd.Visible = False
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            Dim sql As String = ""

            'sql = "Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row, * from Apex_PrePnLCost where RefBriefID=" & hdnBriefID.Value & " "
            'sql &= "  union "
            'sql &= "  Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row,* from Apex_tempPrePnLCost where RefBriefID=" & hdnBriefID.Value & "  "

            sql &= "	Select  ROW_NUMBER() OVER(ORDER BY PrePnLCostID Asc) AS Row,PPNL.*,ST.STATE,CT.CITY,'Y' as prepnl from Apex_PrePnLCost PPNL"
            sql &= "	left JOIN APEX_STATE ST ON PPNL.REFSTATEid=ST.STATEid"
            sql &= "	left JOIN APEX_CITY CT ON PPNL.REFCITYID =CT.CITYID"
            sql &= ""
            sql &= ""
            sql &= " where PPNL.isdeleted='N' and PPNL.isactive='Y' and RefBriefID = " & hdnBriefID.Value
            sql &= "  union "
            sql &= "	Select  ROW_NUMBER() OVER(ORDER BY PrePnLCostID Asc) AS Row,PPNL.*,ST.STATE,CT.CITY,'N' as prepnl from Apex_tempPrePnLCost PPNL"
            sql &= "	left JOIN APEX_STATE ST ON PPNL.REFSTATEid=ST.STATEid"
            sql &= "	left JOIN APEX_CITY CT ON PPNL.REFCITYID =CT.CITYID"
            sql &= ""
            sql &= ""
            sql &= " where PPNL.isdeleted='N' and PPNL.isactive='Y' and RefBriefID = " & hdnBriefID.Value
            'order by PrePnLCostID Asc

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            gvPrePnLCost.DataSource = ds
            gvPrePnLCost.DataBind()
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("prepnl") = "Y" Then
                    btnAdd.Visible = False
                Else
                    btnAdd.Visible = True
                End If

            End If

            If gvPrePnLCost.Rows.Count = 0 And Not gvPrePnLCost.DataSource Is Nothing Then
                Dim dt As Object = Nothing
                If gvPrePnLCost.DataSource.GetType Is GetType(Data.DataSet) Then
                    dt = New System.Data.DataSet
                    dt = CType(gvPrePnLCost.DataSource, System.Data.DataSet).Tables(0).Clone()
                ElseIf gvPrePnLCost.DataSource.GetType Is GetType(Data.DataTable) Then
                    dt = New System.Data.DataTable
                    dt = CType(gvPrePnLCost.DataSource, System.Data.DataTable).Clone()
                ElseIf gvPrePnLCost.DataSource.GetType Is GetType(Data.DataView) Then
                    dt = New System.Data.DataView
                    dt = CType(gvPrePnLCost.DataSource, System.Data.DataView).Table.Clone
                End If

                dt.Rows.Add(dt.NewRow())
                gvPrePnLCost.DataSource = dt
                gvPrePnLCost.DataBind()
                gvPrePnLCost.Rows(0).Visible = False
                gvPrePnLCost.Rows(0).Controls.Clear()

            End If
            FillTotalQoute()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function IsPnLCreate() As Boolean

        Dim flag As Boolean = False
        Try
            Dim sql As String = ""
            sql = "Select IsPrePnl from dbo.APEX_JobCard where refBriefID=" & hdnBriefID.Value & " and IsActive='Y' "
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("IsPrePnl")) Then
                    If ds.Tables(0).Rows(0)("IsPrePnl").ToString() = "Y" Then
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

    Private Sub FillPrePnLCost()
        Try
            Dim sql As String = "Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row,* from APEX_tempPrePnLCost where RefBriefID=" & hdnBriefID.Value & "   order by PrePnLCostID Asc"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvPrePnLCost.DataSource = ds
                gvPrePnLCost.DataBind()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillTotalQoute()
        Try
            Dim str As String = ""
            Dim sql As String = ""
            If btnAdd.Visible = False Then
                sql = "Select Sum(PreEventCost) as SumPreEventCost,Sum(PreEventServiceTax) as PreEventServiceTax,Sum(PreEventTotal) as PreEventTotal  from APEX_PrePnLCost where RefBriefID=" & hdnBriefID.Value
            Else
                sql = "Select Sum(PreEventCost) as SumPreEventCost,Sum(PreEventServiceTax) as PreEventServiceTax,Sum(PreEventTotal) as PreEventTotal  from APEX_tempPrePnLCost where RefBriefID=" & hdnBriefID.Value
            End If

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                lblTotalCost.Text = ds.Tables(0).Rows(0)("SumPreEventCost").ToString()
                If lblTotalCost.Text = "" Then
                    lblTotalCost.Text = "0"
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub UpdateIsPrepnl()
        Try
            If hdnPnLID.Value <> "" Then
                If hdnBriefID.Value <> "" Then
                    Dim sql1 As String = "Update APEX_JobCard set IsPrePnL = 'Y',RefPrePnLID=" & hdnPnLID.Value & " where RefBriefID = " & hdnBriefID.Value
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
            Dim sql As String = "Update APEX_JobCard set IsPrePnL = 'Y',RefPrePnLID=" & hdnPnLID.Value & " where RefBriefID = " & hdnBriefID.Value
            If ExecuteNonQuery(sql) > 0 Then
                Dim sql1 As String = "Delete from APEX_tempPrePnLCost where RefPrePnLID=" & hdnPnLID.Value
                If ExecuteNonQuery(sql1) > 0 Then
                    If getProfitPercentage() > 30 Then
                        Dim capex As New clsApex
                        hdnJobCardID.Value = capex.GetJobCardID(hdnBriefID.Value)
                        capex.UpdateStageLevel("4", hdnJobCardID.Value)
                        FillEstimate()
                    Else
                        Label1.Text = "Data Submitted Successfully and to be approved by Branch Head."
                        MessageDiv.Visible = True
                        divContent.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function getProfitPercentage() As String
        Dim result As String = ""
        Try
            Dim sql As String = "Select PreProfitPercent from APEX_PrePnL where PrePnlID=" & hdnPnLID.Value
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
            Dim sql As String = "INSERT INTO APEX_PrePnLCost SELECT [RefPrePnLID],[RefBriefID]"
            sql &= ",[NatureOfExpenses]"
            sql &= ",[SupplierName]"
            sql &= ",[PreEventCost]"
            sql &= ",[PreEventServiceTax]"
            sql &= ",[PreEventTotal]"
            sql &= ",[IsActive]"
            sql &= ",[IsDeleted]"
            sql &= ",[InsertedOn]"
            sql &= ",[InsertedBy]"
            sql &= ",[ModifiedOn]"
            sql &= ",[ModifiedBy],[Category],RefstateID,RefcityID,StartDate,EndDate,Quantity,Rate,Days FROM APEX_tempPrePnLCost where isactive='Y' and isdeleted='N' and RefPrePnLID=" & hdnPnLID.Value


            If ExecuteNonQuery(sql) > 0 Then

            End If

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillEstimate()
        Try
            Dim sql As String = "Insert into APEX_Estimate(RefBriefID)  values(" & hdnBriefID.Value() & ")"
            If ExecuteNonQuery(sql) Then
                Dim sqlquery As String = ""
                sqlquery &= "	INSERT INTO [APEX_TempEstimate]([RefEstimateID]"
                sqlquery &= "           ,[RefBriefID],[Particulars],[Quantity]"
                sqlquery &= "           ,[Rate],[Days],[Estimate],[Remarks]"
                sqlquery &= "           ,[Category],[AgencyFee]"
                sqlquery &= "	)SELECT (select EstimateID from APEX_Estimate where RefBriefID=" & hdnBriefID.Value() & ") RefEstimateID,[RefBriefID]"
                sqlquery &= "	,[NatureOfExpenses],Quantity,Rate,Days,[PreEventTotal],''"
                sqlquery &= "            ,[Category],'0' FROM APEX_PrePnLCost where isactive='Y' and isdeleted='N' "
                sqlquery &= "			and RefBriefID=" & hdnBriefID.Value() & ""
                sqlquery &= ""
                ExecuteNonQuery(sqlquery)

            End If

            'If ExecuteNonQuery(sql) Then

            'End If

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim profitpercent As Double = vbNull
            Dim apex As New clsApex
            If hdnNodificationID.Value <> "" Then
                apex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            End If

            Dim PreEventProfit As Double = vbNull
            If lblPreEventTotal.Text.Length > 0 Then
                Dim sql As String = "Update APEX_PrePnL set "
                If txtPreEventQuote.Text <> "" Then
                    sql &= "PreEventQuote = " & Clean(txtPreEventQuote.Text)
                Else
                    txtPreEventQuote.Text = "0"
                    sql &= "PreEventQuote = 0"
                End If
                If txtPreEventQuote.Text <> "" And lblTotalCost.Text <> "" Then
                    PreEventProfit = (Convert.ToDouble(txtPreEventQuote.Text) - Convert.ToDouble(lblTotalCost.Text)).ToString()
                End If
                If PreEventProfit <> vbNull Then
                    sql &= ",PreProfit= " & PreEventProfit
                Else
                    sql &= ",PreProfit= NULL"
                End If

                If PreEventProfit <> vbNull Then
                    'If txtPreEventQuote.Text <> "0.00" Or txtPreEventQuote.Text <> "0" Then
                    '    profitpercent = Math.Round((((PreEventProfit) / Convert.ToDouble(txtPreEventQuote.Text)) * 100), 2)
                    'Else
                    '    profitpercent = 0
                    'End If
                    profitpercent = 0
                    sql &= ",PreProfitPercent= " & profitpercent
                Else
                    sql &= ",PreProfitPercent= NULL"
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
                    fupl_Mail.SaveAs(Server.MapPath("Collateral/PrePnL/" & Clean(encname) & "." & fext))
                    Path = "Collateral/PrePnL/" & Clean(encname) & "." & fext

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
                sql &= " where RefBriefID=" & hdnBriefID.Value & " and PrePnLID=" & hdnPnLID.Value


                If ExecuteNonQuery(sql) > 0 Then

                    TransferTableData()
                    Dim sql1 As String = "Delete from APEX_tempPrePnLCost where RefPrePnLID=" & hdnPnLID.Value
                    ExecuteNonQuery(sql1)
                    If Len(Request.QueryString("Isnotific")) > 0 Then
                        If Request.QueryString("Isnotific") = "N" Then
                            Response.Redirect("Estimate.aspx?mode=edit&bid=" & hdnBriefID.Value)
                        End If
                    Else
                        FillEstimate()
                        SendForApprovalBranchHead(clsApex.NotificationType.JSTMT, hdnBriefID.Value, 2, "Estimate")
                        UpdateIsPrepnl()
                        Dim capex As New clsApex
                        capex.UpdateStageLevel("4", hdnJobCardID.Value)
                        Label1.Text = lblnotifivationMsg.Text
                        MessageDiv.Visible = True
                        divContent.Visible = False
                    End If
                    If profitpercent Then
                        lblPreEventProfit.Text = profitpercent
                    Else
                        lblPreEventProfit.Text = "0"
                    End If
                    btnAdd.Visible = False
                End If

                'Response.Redirect(Request.UrlReferrer.AbsoluteUri)

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub CloseAllControls()
        Try
            ddlClient.Visible = False
            txtEventName.Visible = False
            txtEventDate.Visible = False
            txtEventVenue.Visible = False
            txtApprovalNo.Visible = False
            txtCreditPeriod.Visible = False
            gvPrePnLCost.Visible = False
            txtPreEventQuote.Visible = False
            lblTotalCost.Visible = False
            lblTotalServiceTax.Visible = False
            lblPreEventTotal.Visible = False
            lblPreEventProfit.Visible = False
            fupl_Mail.Visible = False

            lblClient.Visible = True
            lblEventName.Visible = True
            lblEventDate.Visible = True
            lblEventVenue.Visible = True
            lblApprovalNo.Visible = True
            lblCreditPeriod.Visible = True
            lblPreEventQuote.Visible = True
            lblPreEPr.Visible = True
            lblTCost.Visible = True
            lblTSTax.Visible = True
            lblPETotal.Visible = True

            If lblfulpl_Mail.NavigateUrl <> "" Then
                lblfulpl_Mail.Visible = True
            Else
                lblfulpl_Mail.Visible = False
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillDetails()
        Try
            Dim sql As String = ""

            sql = "Select convert(varchar(10),EventDate,105) as NEventDate,* from APEX_PrePnL where RefBriefID=" & hdnBriefID.Value & " and PrePnLID="
            If hdnPnLID.Value <> "" Then
                sql &= hdnPnLID.Value
            Else
                sql &= "NULL"
            End If
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)

            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If Not IsDBNull(ds.Tables(0).Rows(i)("PLApprovalMail")) Then
                        Dim epass As String = ds.Tables(0).Rows(i)("PLApprovalMail")
                        ds.Tables(0).Rows(i).Item("PLApprovalMail") = Clean(epass)
                    End If
                Next

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
                txtPreEventQuote.Text = ds.Tables(0).Rows(0)("PreEventQuote").ToString()

                lblClient.Text = ddlClient.SelectedItem.Text
                lblEventName.Text = ds.Tables(0).Rows(0)("EventName").ToString()
                lblEventDate.Text = ds.Tables(0).Rows(0)("NEventDate").ToString()
                lblEventVenue.Text = ds.Tables(0).Rows(0)("EventVenue").ToString()
                lblApprovalNo.Text = ds.Tables(0).Rows(0)("PnLApprovalNo").ToString()
                lblCreditPeriod.Text = ds.Tables(0).Rows(0)("CreditPeriod").ToString()
                lblPreEventQuote.Text = ds.Tables(0).Rows(0)("PreEventQuote").ToString()

                If ds.Tables(0).Rows(0)("PreProfitPercent").ToString() <> "" Then
                    lblPreEventProfit.Text = ds.Tables(0).Rows(0)("PreProfitPercent").ToString()

                    lblPreEPr.Text = ds.Tables(0).Rows(0)("PreProfitPercent").ToString()
                Else
                    Dim sql_db As String = ""
                    Dim ds_db As New DataSet

                End If
                If ds.Tables(0).Rows(0)("PLApprovalMail").ToString() <> "" Then
                    lblfulpl_Mail.NavigateUrl = ds.Tables(0).Rows(0)("PLApprovalMail").ToString()
                Else
                    lblfulpl_Mail.Visible = False
                End If


                FillPrePnLCost()
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

    Protected Sub gvPrePnLCost_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvPrePnLCost.RowCreated

    End Sub

    Protected Sub gvPrePnLCost_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPrePnLCost.RowDataBound
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

                'start state bind
                Dim ddlState As DropDownList = e.Row.FindControl("ddlstate")
                bindstate(ddlState)
                'end

            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.DataItem IsNot Nothing Then
                    If (e.Row.RowState And DataControlRowState.Edit) > 0 Then
                        Dim ddlFormat As DropDownList = e.Row.FindControl("gv_ddlCategory")

                        Dim hdnPrePnLCostID As HiddenField = e.Row.FindControl("hdnPrePnLCostID")

                        'Dim hdnstateID As HiddenField = e.Row.FindControl("hdnstate")
                        'Dim hdncityid As HiddenField = e.Row.FindControl("hdncity")

                        ' HdnIncharge.Value = ddlFormat.DataValueField
                        Dim sqlBindstring As String = ""
                        Dim sqlq As String = "select PrePnLCostID from APEX_PrePnLCost where PrePnLCostID=" & hdnPrePnLCostID.Value
                        If ExecuteSingleResult(sqlq) <> "" Then
                            sqlBindstring &= "Select Category from APEX_PrePnLCost where PrePnlCostID=" & hdnPrePnLCostID.Value

                        Else
                            sqlBindstring &= "Select Category from APEX_tempPrePnlCost where PrePnlCostID=" & hdnPrePnLCostID.Value

                        End If

                        'Dim sqlBindstring As String = "Select Category from APEX_tempPrePnlCost where PrePnlCostID=" & hdnPrePnLCostID.Value

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

    Protected Sub gvPrePnLCost_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvPrePnLCost.RowDeleting
        'Try
        '    Dim prepnlcostid As New HiddenField
        '    prepnlcostid = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("hdnPrePnLCostID"), HiddenField)
        '    ' prepnlcostid = gvPrePnLCost.DataKeys(e.RowIndex).Value

        '    Dim sql As String = ""
        '    Dim sqlq As String = "select PrePnLCostID from APEX_PrePnLCost where PrePnLCostID=" & prepnlcostid.Value
        '    If ExecuteSingleResult(sqlq) <> "" Then
        '        'sql &= "UPDATE [APEX_PrePnLCost]"
        '        sql &= "Delete from [APEX_PrePnLCost] "
        '        btnAdd.Visible = False
        '    Else
        '        'sql &= "UPDATE [APEX_tempPrePnLCost] set Isdeleted='Y'"
        '        sql &= "Delete from [APEX_tempPrePnLCost] "
        '    End If


        '    sql &= " WHERE PrePnLCostID=" & prepnlcostid.Value
        '    If ExecuteNonQuery(sql) Then
        '        BindGrid()
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub UpdateTempPLID()
        Try
            Dim sql As String = "Update APEX_tempPrePnLCost set RefPrePnLID=" & hdnPnLID.Value & " where RefBriefID=" & hdnBriefID.Value

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

    Private Sub GetPrePnLID()
        Try
            Dim sql As String = "Select PrePnLID from APEX_PrePnL where RefBriefID=" & hdnBriefID.Value
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
            Dim capex As New clsApex
            Dim jobcode As String = capex.GetJobCardNoByBriefID(hdnBriefID.Value)
            If Len(Request.QueryString("nid")) > 0 Then
                Response.Redirect("Home.aspx")
            End If
            If jobcode <> "" Then
                Response.Redirect("JobCard.aspx?mode=rj")
            Else
                Response.Redirect("JobCard.aspx?mode=pj")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindGridAfter()
        Try
            Dim sql As String = ""
            sql &= "	Select  ROW_NUMBER() OVER(ORDER BY PrePnLCostID Asc) AS Row,PPNL.*,ST.STATE,CT.CITY from Apex_PrePnLCost PPNL"
            sql &= "	left JOIN APEX_STATE ST ON PPNL.REFSTATEid=ST.STATEid"
            sql &= "	left JOIN APEX_CITY CT ON PPNL.REFCITYID =CT.CITYID"
            sql &= ""
            sql &= ""
            sql &= " where RefBriefID = " & hdnBriefID.Value

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            gvPrePnLCost.DataSource = ds
            gvPrePnLCost.DataBind()
            gvDisplay.DataSource = ds
            gvDisplay.DataBind()
            If gvPrePnLCost.Rows.Count = 0 And Not gvPrePnLCost.DataSource Is Nothing Then
                Dim dt As Object = Nothing
                If gvPrePnLCost.DataSource.GetType Is GetType(Data.DataSet) Then
                    dt = New System.Data.DataSet
                    dt = CType(gvPrePnLCost.DataSource, System.Data.DataSet).Tables(0).Clone()
                ElseIf gvPrePnLCost.DataSource.GetType Is GetType(Data.DataTable) Then
                    dt = New System.Data.DataTable
                    dt = CType(gvPrePnLCost.DataSource, System.Data.DataTable).Clone()
                ElseIf gvPrePnLCost.DataSource.GetType Is GetType(Data.DataView) Then
                    dt = New System.Data.DataView
                    dt = CType(gvPrePnLCost.DataSource, System.Data.DataView).Table.Clone
                End If
                dt.Rows.Add(dt.NewRow())
                gvPrePnLCost.DataSource = dt
                gvPrePnLCost.DataBind()
                gvPrePnLCost.Rows(0).Visible = False
                gvPrePnLCost.Rows(0).Controls.Clear()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillTotalQouteAfter()
        Try
            Dim sql As String = "Select Sum(PreEventCost) as SumPreEventCost,Sum(PreEventServiceTax) as PreEventServiceTax,Sum(PreEventTotal) as PreEventTotal  from APEX_PrePnLCost where isactive='Y' and isdeleted='N' and RefBriefID=" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                lblTotalCost.Text = ds.Tables(0).Rows(0)("SumPreEventCost").ToString()
                'lblTotalServiceTax.Text = ds.Tables(0).Rows(0)("PreEventServiceTax").ToString()
                lblPreEventTotal.Text = ds.Tables(0).Rows(0)("PreEventTotal").ToString()
                'lblServiceTaxAmount.Text = ds.Tables(0).Rows(0)("PreEventServiceTax").ToString()

                If lblTotalCost.Text = "" Then
                    lblTotalCost.Text = "0"
                End If

                If lblPreEventTotal.Text = "" Then
                    lblPreEventTotal.Text = "0"
                End If
                If lblTotalCost.Text = "" Then
                    lblTotalCost.Text = "0"
                End If

                lblTCost.Text = ds.Tables(0).Rows(0)("SumPreEventCost").ToString()

                lblPETotal.Text = ds.Tables(0).Rows(0)("PreEventTotal").ToString()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvPrePnLCost_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPrePnLCost.PageIndexChanging
        Try
            gvPrePnLCost.PageIndex = e.NewPageIndex
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

    Public Sub SendForApprovalBranchHead(ByVal type As String, ByVal bid As String, ByVal desig As String, Optional stg As String = "other")
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
            sql &= "('Prepare Estimate'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Prepare Estimate'"
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

    Private Sub InsertRecieptentDetails(ByVal type As String, ByRef bid As String, ByVal deg As String, Optional stag As String = "other")
        Try
            Dim notificationid As String = ""
            Dim sql As String = "Select NotificationID from APEX_Notifications where Type=" & type & " and AssociateID=" & bid
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                notificationid = ds.Tables(0).Rows(0)(0).ToString()
            End If

            Dim bheadid As String = ""
            Dim sql3 As String = " "
            If stag <> "other" Then
                Dim clsApex As New clsApex
                ' Dim leadid As String = clsApex.GetLeadIDByBriefID(hdnBriefID.Value)
                sql3 = "Select b.insertedBy from dbo.APEX_Brief  as b    where BriefID= " & hdnBriefID.Value
            Else
                sql3 = "Select UserDetailsID from APEX_UsersDetails where Designation=" & deg & ""
            End If

            Dim ds3 As New DataSet
            ds3 = ExecuteDataSet(sql3)
            If ds3.Tables(0).Rows.Count > 0 Then
                bheadid = ds3.Tables(0).Rows(0)(0).ToString()
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
            sql1 &= "," & bheadid
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",NULL"
            sql1 &= ",NULL"
            sql1 &= "," & getLoggedUserID() & ")"

            ExecuteNonQuery(sql1)
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click
        Try
            Dim capex As New clsApex
            capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            GetPrePnLID()
            Dim sql As String = "update APEX_Estimate set "
            sql &= " IsOperationHeadApproved = 'Y'"
            sql &= ",OperationHeadApprovedOn = getdate()"
            sql &= ",IsBranchHeadApproved = 'Y'"
            sql &= ",BranchHeadApprovedOn = getdate()"
            If txtRemarks.Text <> "" Then
                sql &= ",Remarks='" & Clean(txtRemarks.Text) & "'"
            End If

            sql &= " where PrePnlID=" & hdnPnLID.Value

            If ExecuteNonQuery(sql) > 0 Then
                hdnJobCardID.Value = capex.GetJobCardID(hdnBriefID.Value)
                capex.UpdateStageLevel("5", hdnJobCardID.Value)
                trAppButtons.Visible = False
                Label1.Text = "Estimate approved successfully."
                divError.Visible = False
                MessageDiv.Visible = True
                SendForApprovalBranchHead(clsApex.NotificationType.JSTMT, hdnBriefID.Value, 2, "Estimate")
                If ddlClient.SelectedValue <> "" Then
                    SendForApprovalPM()
                End If

                FillEstimate()
                UpdateIsPrepnl()
                ' Response.Redirect("JobCard.aspx?mode=pj")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Public Sub SendForApprovalPM()
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
            sql &= "," & NotificationType.PPNLApproved
            sql &= "," & hdnBriefID.Value
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails()


            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub


    Private Sub InsertRecieptentDetails()
        Try
            Dim notificationid As String = ""
            Dim sql As String = "Select NotificationID from APEX_Notifications where Type=" & NotificationType.PPNLApproved & " and AssociateID=" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                notificationid = ds.Tables(0).Rows(0)(0).ToString()
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
            sql1 &= "," & getprojectmanager()
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",NULL"
            sql1 &= ",NULL"
            sql1 &= "," & getLoggedUserID() & ")"

            ExecuteNonQuery(sql1)
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Function getprojectmanager() As String
        Dim pm As String = ""
        Try
            Dim apx As New clsApex
            Dim bid As String = hdnBriefID.Value
            If bid <> "" Then
                Dim jid As String = apx.GetJobCardIDByBriefID(bid)
                Dim sql As String = "Select ProjectmanagerID from dbo.APEX_JobCard where JobCardID=" & jid & ""
                Dim ds As DataSet = Nothing
                ds = ExecuteDataSet(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0)("ProjectmanagerID")) Then
                        pm = ds.Tables(0).Rows(0)("ProjectmanagerID")
                    End If

                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return pm
    End Function

    Protected Sub btnRejected_Click(sender As Object, e As EventArgs) Handles btnRejected.Click
        Try
            Dim capex As New clsApex
            capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            GetPrePnLID()
            Dim sql As String = "update APEX_Estimate set "
            sql &= " IsOperationHeadApproved = 'R'"
            sql &= ",OperationHeadApprovedOn = getdate()"
            sql &= ",IsBranchHeadApproved = 'R'"
            sql &= ",BranchHeadApprovedOn = getdate()"
            If txtRemarks.Text <> "" Then
                sql &= ",Remarks='" & Clean(txtRemarks.Text) & "'"
            End If

            sql &= " where PrePnlID=" & hdnPnLID.Value

            If ExecuteNonQuery(sql) > 0 Then
                trAppButtons.Visible = False
                Label1.Text = "Estimate is rejected successfully."
                divError.Visible = False
                MessageDiv.Visible = True
                SendForApprovalBranchHead(clsApex.NotificationType.reject, hdnBriefID.Value, 2, "Estimate")

            End If
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

    Protected Sub gvPrePnLCost_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvPrePnLCost.RowEditing
        Try
            gvPrePnLCost.EditIndex = e.NewEditIndex
            'Rebind the GridView to show the data in edit mode
            BindGrid()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub


    Protected Sub gvPrePnLCost_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvPrePnLCost.RowCancelingEdit
        Try
            ' switch back to edit default mode
            'Response.Redirect(Request.UrlReferrer.AbsoluteUri)
            gvPrePnLCost.EditIndex = -1
            'Rebind the GridView to show the data in edit mode
            BindGrid()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub UpdateRecord(ByVal prepnlcostid As String, ByVal Nature As String, ByVal Supplier As String, ByVal PreEventCost As String, ByVal PreEventServiceTax As String, ByVal PreEventTotal As String, ByVal Category As String, ByVal startdt As String, ByVal enddt As String, ByVal qty As String, ByVal rate As String, ByVal days As String)
        Try
            Dim sql As String = ""
            'Dim sqlq As String = "select PrePnLCostID from APEX_PrePnLCost where PrePnLCostID=" & prepnlcostid
            'If ExecuteSingleResult(sqlq) <> "" Then
            '    sql &= "UPDATE [APEX_PrePnLCost]"
            'Else
            '    sql &= "UPDATE [APEX_TempPrePnLCost]"
            'End If

            sql &= "UPDATE [APEX_TempPrePnLCost]"
            sql &= "  SET [RefPrePnLID] = " & hdnPnLID.Value & ""
            sql &= "  ,[RefBriefID] = " & hdnBriefID.Value & ""
            sql &= ",[NatureOfExpenses] = '" & Clean(Nature) & "'"
            PreEventCost = (Convert.ToDouble(qty) * Convert.ToDouble(rate) * Convert.ToDouble(days)).ToString()
            sql &= " ,[PreEventCost] = '" & Clean(PreEventCost) & "'"
            sql &= "  ,[Category] = '" & Category & "'"
            sql &= "  ,[Quantity] = '" & qty & "'"
            sql &= "  ,[Rate] = '" & rate & "'"
            sql &= "  ,[Days] = '" & days & "'"
            sql &= " WHERE PrePnLCostID=" & prepnlcostid

            If ExecuteNonQuery(sql) > 0 Then
                FillPrePnLCost()
                FillTotalQoute()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub


    Protected Sub gvPrePnLCost_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPrePnLCost.RowCommand
        Try
            If e.CommandName = "Add" Then
                If gvPrePnLCost.Rows.Count > 0 Then
                    btnAdd.Visible = True
                Else
                    btnAdd.Visible = False
                End If

                Dim totalquote As Double = 0

                Dim frow As GridViewRow = gvPrePnLCost.FooterRow
                Dim txtNature As New TextBox
                Dim txtSupplier As New TextBox
                Dim txtPreEventCost As New TextBox
                'Dim txtPreEventServiceTax As New TextBox
                'Dim txtPreEventTotal As New TextBox
                Dim ddlCategory As New DropDownList
                Dim ddlstate As New DropDownList
                Dim ddlcity As New DropDownList
                Dim txtstartdt As New TextBox
                Dim txtenddt As New TextBox
                Dim txtDays As New TextBox
                Dim txtquantity As New TextBox
                Dim txtRate As New TextBox



                txtNature = CType(frow.FindControl("txtNature"), TextBox)
                txtSupplier = CType(frow.FindControl("txtSupplier"), TextBox)
                txtPreEventCost = CType(frow.FindControl("txtPreEventCost"), TextBox)
                'txtPreEventServiceTax = CType(frow.FindControl("txtPreEventServiceTax"), TextBox)
                'txtPreEventTotal = CType(frow.FindControl("txtPreEventTotal"), TextBox)
                ddlCategory = CType(frow.FindControl("ddlCategory"), DropDownList)

                ddlstate = CType(frow.FindControl("ddlstate"), DropDownList)
                ddlcity = CType(frow.FindControl("ddlcity"), DropDownList)
                txtstartdt = CType(frow.FindControl("txtstartdate"), TextBox)
                txtenddt = CType(frow.FindControl("txtEnddate"), TextBox)


                txtDays = CType(frow.FindControl("txtDays"), TextBox)
                txtquantity = CType(frow.FindControl("txtquantity"), TextBox)
                txtRate = CType(frow.FindControl("txtRate"), TextBox)

                txtPreEventCost.Text = (Convert.ToDouble(txtDays.Text) * Convert.ToDouble(txtquantity.Text) * Convert.ToDouble(txtRate.Text)).ToString()

                Dim sql As String = ""
                If btnAdd.Visible = True Then
                    sql = "Insert into APEX_tempPrePnLCost (RefPrePnLID,RefBriefID,NatureOfExpenses,SupplierName,PreEventCost,PreEventServiceTax,PreEventTotal,Category,[Quantity],[Rate],[Days]) Values ("
                Else
                    sql = "Insert into APEX_PrePnLCost (RefPrePnLID,RefBriefID,NatureOfExpenses,SupplierName,PreEventCost,PreEventServiceTax,PreEventTotal,Category,[Quantity],[Rate],[Days]) Values ("
                End If

                ',RefstateID,RefcityID,StartDate,EndDate
                If hdnPnLID.Value <> "" Then
                    sql &= hdnPnLID.Value
                Else
                    sql &= "NULL"

                End If
                sql &= "," & hdnBriefID.Value
                sql &= ",'" & Clean(txtNature.Text) & "'"
                sql &= ",'" & Clean(txtSupplier.Text) & "'"
                sql &= "," & Clean(txtPreEventCost.Text)
                sql &= ",0"
                'If txtPreEventCost.Text <> "" And txtPreEventServiceTax.Text <> "" Then
                '    Dim tax As Double = (Convert.ToDouble(txtPreEventCost.Text) * Convert.ToDouble(txtPreEventServiceTax.Text)) / 100

                sql &= "," & Clean(Convert.ToDouble(txtPreEventCost.Text))

                'End If

                sql &= ",'" & ddlCategory.SelectedItem.Text & "'"

                'sql &= ",'" & ddlstate.SelectedValue & "'"
                'sql &= ",'" & ddlcity.SelectedValue & "'"
                'sql &= ",convert(datetime,'" & txtstartdt.Text & "',105)"
                'sql &= ",convert(datetime,'" & txtenddt.Text & "',105)"
                sql &= "," & Clean(txtquantity.Text) & ""
                sql &= "," & Clean(txtRate.Text)
                sql &= "," & Clean(txtDays.Text)


                sql &= ")"

                If ExecuteNonQuery(sql) > 0 Then
                    ' Response.Redirect(Request.UrlReferrer.AbsoluteUri)
                    BindGrid()
                    'FillPrePnLCost()
                    FillTotalQoute()
                End If
            End If
            If e.CommandName = "Delete" Then
                Dim hdnPrePnLCostID As New HiddenField

                Dim row As GridViewRow = CType(CType(e.CommandSource, Button).NamingContainer, GridViewRow)

                hdnPrePnLCostID = CType(row.FindControl("hdnPrePnLCostID"), HiddenField)

                Dim sql As String = "Delete from APEX_tempPrePnLCost where PrePnLCostID=" & hdnPrePnLCostID.Value
                If ExecuteNonQuery(sql) > 0 Then
                    BindGrid()
                    ' FillPrePnLCost()
                    FillTotalQoute()
                End If
            End If
            If e.CommandName = "Edit" Then
                Dim hdnPrePnLCostID As New HiddenField
            End If
            If e.CommandName = "Update" Then
                Try
                    Dim rowIndex As GridViewRow = CType(CType(e.CommandSource, Button).NamingContainer, GridViewRow)
                    Dim prepnlcostid As New HiddenField
                    prepnlcostid = DirectCast(rowIndex.FindControl("hdnPrePnLCostID"), HiddenField)
                    Dim Nature As New TextBox
                    Dim Supplier As New TextBox
                    Dim PreEventCost As New TextBox
                    Dim Category As New DropDownList

                    Dim txtstartdt As New TextBox
                    Dim txtenddt As New TextBox

                    Dim Quantity As New TextBox
                    Dim rate As New TextBox
                    Dim days As New TextBox


                    Nature = DirectCast(rowIndex.FindControl("gv_txtNatureOfExpenses"), TextBox)
                    Category = DirectCast(rowIndex.FindControl("gv_ddlCategory"), DropDownList)
                    Supplier = DirectCast(rowIndex.FindControl("gv_txtSupplierName"), TextBox)
                    PreEventCost = DirectCast(rowIndex.FindControl("gv_txtPreEventCost"), TextBox)
                    txtstartdt = DirectCast(rowIndex.FindControl("gv_txtstartdate"), TextBox)
                    txtenddt = DirectCast(rowIndex.FindControl("gv_txtEnddate"), TextBox)

                    Quantity = DirectCast(rowIndex.FindControl("gv_txtquantity"), TextBox)
                    rate = DirectCast(rowIndex.FindControl("gv_txtRate"), TextBox)
                    days = DirectCast(rowIndex.FindControl("gv_txtDays"), TextBox)


                    Dim d2, d3, d4, d5, d6, d7, state, city, startdt, enddt As String
                    Dim d1 As String = prepnlcostid.Value
                    If Nature.Text <> "" Then
                        d2 = Clean(Nature.Text)
                    End If
                    If Supplier.Text <> "" Then
                        d3 = Clean(Supplier.Text)
                    End If
                    If PreEventCost.Text <> "" Then
                        d4 = Clean(PreEventCost.Text)
                    End If
                    d5 = 0
                    d6 = d4
                    If Category.SelectedIndex > 0 Then
                        d7 = Category.SelectedItem.Text
                    End If
                    startdt = txtstartdt.Text
                    enddt = txtenddt.Text


                    'Call the function to update the GridView
                    UpdateRecord(d1, d2, d3, d4, d5, d6, d7, startdt, enddt, Quantity.Text, rate.Text, days.Text)

                    gvPrePnLCost.EditIndex = -1

                    'Response.Redirect(Request.UrlReferrer.AbsoluteUri)
                    'Rebind Gridview to reflect changes made
                    BindGrid()

                Catch ex As Exception
                    SendExMail(ex.Message, ex.ToString())
                End Try
            End If
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
                    lblbrcclient.Text = lblClient.Text
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

    Protected Sub btnCancelKamHome_Click(sender As Object, e As EventArgs) Handles btnCancelKamHome.Click
        Response.Redirect("Home.aspx?mode=KAMS")
    End Sub

    Public Function CheckNotification() As Boolean
        Dim result As Boolean = False
        Dim sql As String = "select * from APEX_Notifications "
        sql &= " inner join APEX_Recieptents on RefNotificationID=NotificationID"
        sql &= " where type=7 and AssociateID=" & hdnBriefID.Value & " and IsExecuted='N' and UserID=" & getLoggedUserID()
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnNodificationID.Value = ds.Tables(0).Rows(0)("NotificationID").ToString()
            result = True
        End If
        Return result
    End Function

    Protected Sub ddlstate_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        'Grabbing a reference to the ddl1 that raised the event
        Dim ddl1 As DropDownList = CType(sender, DropDownList)

        'Grabbing a reference to the GridViewRow that the ddl1 belongs to 
        'so that I can grab a reference to the ddl2 that belongs to that row
        Dim gridViewRowDdlBelongsTo As GridViewRow = CType(CType(sender, Control).NamingContainer, GridViewRow)
        Dim ddl2 As DropDownList = Nothing
        If (gridViewRowDdlBelongsTo.RowState And DataControlRowState.Edit) > 0 Then
            If gridViewRowDdlBelongsTo IsNot Nothing Then
                ddl2 = CType(gridViewRowDdlBelongsTo.FindControl("gv_ddlcity"), DropDownList)
            End If
        Else
            If gridViewRowDdlBelongsTo IsNot Nothing Then
                ddl2 = CType(gridViewRowDdlBelongsTo.FindControl("ddlcity"), DropDownList)
            End If
        End If
        'Grabbing a reference to ddl2 that is in the same row as ddl1



        'Setting the datasource for ddl2 in the row, and binding to it.
        Dim sqlstr As String = "select CityID, City, RefStateID from apex_city where isactive='Y' and Isdeleted='N' and refstateID=" & ddl1.SelectedValue
        Dim ds1 As New DataSet
        ds1 = ExecuteDataSet(sqlstr)
        'If ds1.Tables(0).Rows.Count > 0 Then
        ddl2.DataTextField = "City"
        ddl2.DataValueField = "CityID"
        ddl2.DataSource = ds1
        ddl2.DataBind()

        ddl2.Items.Insert(0, New ListItem("Select", "0"))
        'Else
        'ddl2.DataSource = ds1
        'ddl2.DataBind()
        'ddl2.Items.Insert(0, New ListItem("Select", "0"))
        'End If
        'If ddl2 IsNot Nothing Then
        '    ddl2.DataSource = ddl2Source
        '    ddl2.DataBind()
        'End If
    End Sub

    Private Sub bindstate(ddlState As DropDownList)
        Try
            Dim sqlstr As String = "select StateID, State from apex_state where isactive='Y' and Isdeleted='N'"
            Dim ds1 As New DataSet
            ds1 = ExecuteDataSet(sqlstr)
            If ds1.Tables(0).Rows.Count > 0 Then
                ddlState.DataTextField = "State"
                ddlState.DataValueField = "StateID"
                ddlState.DataSource = ds1
                ddlState.DataBind()

            End If
            ddlState.Items.Insert(0, New ListItem("Select", "0"))
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click

        Response.Clear()
        BindGridAfter()
        Response.AddHeader("content-disposition", "attachment; filename=prepnl" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls")
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

    Protected Sub PasteToGridView(sender As Object, e As EventArgs)
        Try
            Dim dt As New DataTable()
            dt.Columns.AddRange(New DataColumn(6) {New DataColumn("id", GetType(Integer)), New DataColumn("Category", GetType(String)), New DataColumn("natureofexpenses", GetType(String)), New DataColumn("Quantity", GetType(String)), New DataColumn("UnitPrice", GetType(String)), New DataColumn("City", GetType(String)), New DataColumn("City1", GetType(String))})

            Dim copiedContent As String = Request.Form(txtCopied.UniqueID)
            For Each row As String In copiedContent.Split(ControlChars.Lf)
                If Not String.IsNullOrEmpty(row) Then
                    dt.Rows.Add()
                    Dim i As Integer = 0
                    For Each cell As String In row.Split(ControlChars.Tab)
                        dt.Rows(dt.Rows.Count - 1)(i) = cell
                        i += 1
                    Next
                End If
            Next
            GridView1.DataSource = dt
            GridView1.DataBind()
            txtCopied.Text = ""
            divError.Visible = False
            lblError.Text = ""
            If dt.Rows.Count > 0 Then
                btnimport.Visible = True
                btncancelimport.Visible = True
            Else
                btnimport.Visible = False
                btncancelimport.Visible = False
            End If

        Catch ex As Exception
            divError.Visible = True
            lblError.Text = "You have Enterd some wrong information."
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(5).Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
            e.Row.Cells(5).Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            Dim ddlcategory As DropDownList = CType(e.Row.FindControl("ddlcategory"), DropDownList)
            Dim hdncategory As HiddenField = CType(e.Row.FindControl("hdncatigory"), HiddenField)
            Dim category As String()
            Dim sql As String = "select RefActivityTypeID from APEX_Brief where BriefID=" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                category = (ds.Tables(0).Rows(0)(0).ToString()).Split("|")
                ddlcategory.DataSource = category
                ddlcategory.DataBind()
            End If

            ddlcategory.Items.Insert(0, New ListItem("Select", "0"))

            If hdncategory.Value <> "" Then
                If Not IsNothing(ddlcategory.Items.FindByText(hdncategory.Value)) Then
                    ddlcategory.Items.FindByText(hdncategory.Value).Selected = True
                End If

            End If
        End If
    End Sub

    Protected Sub btnimport_Click(sender As Object, e As EventArgs) Handles btnimport.Click

        For Each row As GridViewRow In GridView1.Rows
            Dim id As Integer = Integer.Parse(row.Cells(0).Text)
            Dim Category As DropDownList = CType(row.FindControl("ddlCategory"), DropDownList)
            Dim NatureOfExpenses As String = row.Cells(2).Text
            Dim Quantity As String = row.Cells(3).Text
            Dim Unitprice As String = row.Cells(4).Text
            Dim City As String = row.Cells(5).Text
            Dim PreEventCost As Integer = Integer.Parse(Quantity * Unitprice * City)
            Dim sql As String = ""
            If btnAdd.Visible = True Then
                sql = "Insert into APEX_tempPrePnLCost (RefPrePnLID,RefBriefID,NatureOfExpenses,SupplierName,PreEventCost,PreEventServiceTax,PreEventTotal,Category,[Quantity],[Rate],[Days]) Values ("
            Else
                sql = "Insert into APEX_PrePnLCost (RefPrePnLID,RefBriefID,NatureOfExpenses,SupplierName,PreEventCost,PreEventServiceTax,PreEventTotal,Category,[Quantity],[Rate],[Days]) Values ("
            End If

            If hdnPnLID.Value <> "" Then
                sql &= hdnPnLID.Value
            Else
                sql &= "NULL"

            End If
            sql &= "," & hdnBriefID.Value
            sql &= ",'" & Clean(NatureOfExpenses) & "'"
            sql &= ",''"
            sql &= "," & Clean(PreEventCost)
            sql &= ",0"
            'If txtPreEventCost.Text <> "" And txtPreEventServiceTax.Text <> "" Then
            '    Dim tax As Double = (Convert.ToDouble(txtPreEventCost.Text) * Convert.ToDouble(txtPreEventServiceTax.Text)) / 100

            sql &= "," & Clean(Convert.ToDouble(PreEventCost))

            'End If

            sql &= ",'" & Category.SelectedItem.Text & "'"

            'sql &= ",'" & ddlstate.SelectedValue & "'"
            'sql &= ",'" & ddlcity.SelectedValue & "'"
            'sql &= ",convert(datetime,'" & txtstartdt.Text & "',105)"
            'sql &= ",convert(datetime,'" & txtenddt.Text & "',105)"
            sql &= "," & Clean(Quantity) & ""
            sql &= "," & Clean(Unitprice)
            sql &= "," & Clean(City)


            sql &= ")"

            If ExecuteNonQuery(sql) > 0 Then
                ' Response.Redirect(Request.UrlReferrer.AbsoluteUri)

            End If

        Next
        BindGrid()
        FillTotalQoute()
        GridView1.DataSource = Nothing
        GridView1.DataBind()
        btnimport.Visible = False
        btncancelimport.Visible = False

    End Sub
    Protected Sub btnviewBrief_Click(sender As Object, e As EventArgs) Handles btnviewBrief.Click
        Response.Redirect("BriefManager.aspx?mode=edit&bid=" & hdnBriefID.Value)
    End Sub

    Private Sub gvPrePnLCost_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvPrePnLCost.RowUpdating
        'Try
        '    Dim prepnlcostid As New HiddenField
        '    prepnlcostid = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("hdnPrePnLCostID"), HiddenField)
        '    Dim Nature As New TextBox
        '    Dim Supplier As New TextBox
        '    Dim PreEventCost As New TextBox
        '    Dim Category As New DropDownList

        '    Dim txtstartdt As New TextBox
        '    Dim txtenddt As New TextBox

        '    Dim Quantity As New TextBox
        '    Dim rate As New TextBox
        '    Dim days As New TextBox


        '    Nature = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtNatureOfExpenses"), TextBox)
        '    Category = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_ddlCategory"), DropDownList)
        '    Supplier = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtSupplierName"), TextBox)
        '    PreEventCost = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtPreEventCost"), TextBox)
        '    txtstartdt = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtstartdate"), TextBox)
        '    txtenddt = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtEnddate"), TextBox)

        '    Quantity = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtquantity"), TextBox)
        '    rate = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtRate"), TextBox)
        '    days = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtDays"), TextBox)


        '    Dim d2, d3, d4, d5, d6, d7, state, city, startdt, enddt As String
        '    Dim d1 As String = prepnlcostid.Value
        '    If Nature.Text <> "" Then
        '        d2 = Clean(Nature.Text)
        '    End If
        '    If Supplier.Text <> "" Then
        '        d3 = Clean(Supplier.Text)
        '    End If
        '    If PreEventCost.Text <> "" Then
        '        d4 = Clean(PreEventCost.Text)
        '    End If
        '    d5 = 0
        '    d6 = d4
        '    If Category.SelectedIndex > 0 Then
        '        d7 = Category.SelectedItem.Text
        '    End If
        '    startdt = txtstartdt.Text
        '    enddt = txtenddt.Text


        '    'Call the function to update the GridView
        '    UpdateRecord(d1, d2, d3, d4, d5, d6, d7, startdt, enddt, Quantity.Text, rate.Text, days.Text)

        '    gvPrePnLCost.EditIndex = -1

        '    'Response.Redirect(Request.UrlReferrer.AbsoluteUri)
        '    'Rebind Gridview to reflect changes made
        '    BindGrid()

        'Catch ex As Exception
        '    SendExMail(ex.Message, ex.ToString())
        'End Try
    End Sub
End Class