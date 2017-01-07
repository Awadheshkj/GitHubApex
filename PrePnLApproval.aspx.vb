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

Partial Class OpenPrePnLManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            trmsg.Visible = False

            If Not Page.IsPostBack Then
                divwarning.Visible = False
                lblTotalCost.Text = 0
                lblTotalServiceTax.Text = 0
                lblPreEventTotal.Text = 0
                divError.Visible = False
                MessageDiv.Visible = False
                Label2.Text = ""
                divMsgAlert.Visible = False
                If Len(Request.QueryString("bid")) > 0 Then
                    If Request.QueryString("bid") <> Nothing Then
                        If Request.QueryString("bid").ToString() <> "" Then
                            hdnBriefID.Value = Request.QueryString("bid")
                            BindBriefData(hdnBriefID.Value)
                            Dim capex As New clsApex
                            If Len(Request.QueryString("nid") > 0) Then
                                If Request.QueryString("nid") <> "" Then
                                    hdnNodificationID.Value = Request.QueryString("nid")
                                    capex.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())

                                End If
                            End If

                            hdnJobCardID.Value = capex.GetJobCardID(hdnBriefID.Value)
                            If hdnJobCardID.Value <> "" Then
                                'If capex.CheckStageLevel("3", hdnJobCardID.Value) = True Then
                                'TWContent.InnerHtml = capex.GetList("3", hdnJobCardID.Value)
                                BindGrid()

                                GetPrePnLID()
                                FillDetails()
                                FillEstimate()
                                getEstimatevsprepnlHistory()

                                If minmumCCprofitpercentage() > txtActual.Text Then
                                    'divContent.Visible = False

                                    divwarning.Visible = True
                                        lblwarning.Text = "With this request your estimated profit percentage will be " & txtActual.Text & "% which is below the approved percentage(" & minmumCCprofitpercentage() & "). To proceed either you need to Reject this request or you need to increase the estimate amount or you need to send an approval request to the branch head with a valid reason.</br><a href='openestimate.aspx?bid=" & hdnBriefID.Value & "&mode=Genereted'>Click here</a> to go to estimate page or enter the reason below and send the request to branch head."
                                        btnsendapproval.Text = "Reject"
                                        Messagebranchhead.Visible = True




                                Else
                                    If gvPrePnLCost.Rows.Count > 0 Then

                                    Else
                                        divwarning.Visible = True
                                        lblwarning.Text = "The prepnl increase request has been accepted by branch head and has automatically been approved for PM at your end."
                                        btnsendapproval.Text = "Reject"
                                        Messagebranchhead.Visible = True
                                        btnsendapproval.Visible = False

                                    End If
                                    Messagebranchhead.Visible = False
                                End If

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
                                If CheckJobcardCreated() = False Then
                                    FillTotalQouteAfter()
                                Else
                                    'BindGridAfter()
                                    FillTotalQouteAfter()
                                End If
                                'Else
                                'CallDivError()
                                'End If
                            Else
                                CallDivError()
                            End If
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
                            btnCancelKamHome.Visible = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            Dim sql As String = ""

            sql = "Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row, *,isnull((select REquestAmt from APEX_PrePnlCostTrans where"
            sql &= "  prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N'),0)REquestAmt,(select prvEventTotalPRV from APEX_PrePnlCostTrans"
            sql &= " where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N')prevEventTotalCUR"
            sql &= "	,isnull((select Quantity from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N'),0)Qty, "
            sql &= "	isnull((select Rate from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N'),0)Rt, "
            sql &= "	isnull((select [Days] from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N'),0)Dy "
            sql &= " ,isnull((select Reason from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N'),0)Reason"
            sql &= " from Apex_PrePnLCost where RefBriefID=" & hdnBriefID.Value & " "
            sql &= "    and (select prvEventTotalPRV from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N') is not null"
            sql &= "  union "
            sql &= "  Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row,*,0,0,0,0,0,'' from Apex_tempPrePnLCost where RefBriefID=" & hdnBriefID.Value & "  order by PrePnLCostID Asc"

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            gvPrePnLCost.DataSource = ds
            gvPrePnLCost.DataBind()
            If ds.Tables(0).Rows.Count > 0 Then
                lblreson.Text = ds.Tables(0).Rows(0)("Reason")
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
            Dim sql As String = "Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row,*,isnull((select REquestAmt from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N'),0)REquestAmt,(select prevEventTotalCUR from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N')prevEventTotalCUR  from APEX_PrePnLCost where RefBriefID=" & hdnBriefID.Value & "   order by PrePnLCostID Asc"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                'gvPrePnLCost.DataSource = ds
                'gvPrePnLCost.DataBind()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillTotalQoute()
        Try
            Dim str As String = ""
            Dim sql As String = "Select Sum(PreEventCost) as SumPreEventCost,Sum(PreEventServiceTax) as PreEventServiceTax,Sum(PreEventTotal) as PreEventTotal  from APEX_PrePnLCost where RefBriefID=" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                lblTotalCost.Text = ds.Tables(0).Rows(0)("SumPreEventCost").ToString()
                If lblTotalCost.Text = "" Then
                    lblTotalCost.Text = "0"
                    If txtPreEventQuote.Text = "" Then
                        txtPreEventQuote.Text = "0"
                    End If

                    lblPreEventProfit.Text = Math.Round(((Convert.ToDouble(txtPreEventQuote.Text) - Convert.ToDouble(lblTotalCost.Text)) / Convert.ToDouble(txtPreEventQuote.Text)) * 100, 2).ToString()
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
            sql &= ",[ModifiedBy],[Category] FROM APEX_PrePnLCost where RefPrePnLID=" & hdnPnLID.Value
            If ExecuteNonQuery(sql) > 0 Then

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


                ' FillPrePnLCost()
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
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.DataItem IsNot Nothing Then

                    Dim imgbtnEdit As Button = e.Row.FindControl("imgbtnEdit")
                    Dim ddlStatus As DropDownList = e.Row.FindControl("ddlStatus")
                    Dim txtapprovedAmount As TextBox = e.Row.FindControl("txtamt")
                    lblcost.Text = CInt(lblcost.Text) + CInt(txtapprovedAmount.Text)
                    'If imgbtnEdit.Text = "N/A" Then
                    '    ddlStatus.Visible = False
                    '    btnsendapproval.Visible = False
                    'Else
                    '    ddlStatus.Visible = True
                    '    btnsendapproval.Visible = True
                    'End If
                    btnsendapproval.Visible = True

                    If (e.Row.RowState And DataControlRowState.Edit) > 0 Then
                        Dim ddlFormat As DropDownList = e.Row.FindControl("gv_ddlCategory")
                        Dim hdnPrePnLCostID As HiddenField = e.Row.FindControl("hdnPrePnLCostID")
                        ' HdnIncharge.Value = ddlFormat.DataValueField
                        Dim sqlBindstring As String = "Select Category from APEX_PrePnlCost where PrePnlCostID=" & hdnPrePnLCostID.Value
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

    End Sub

    Private Sub UpdateTempPLID()
        Try
            Dim sql As String = "Update APEX_PrePnLCost set RefPrePnLID=" & hdnPnLID.Value & " where RefBriefID=" & hdnBriefID.Value

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

    Private Sub BindGridAfter()
        Try
            Dim sql As String = "Select  ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row,*,"
            sql &= " isnull((select REquestAmt from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N'),0)REquestAmt,"
            sql &= " (select prvEventTotalPRV from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N')prevEventTotalCUR "
            sql &= " ,isnull((select Quantity from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N'),0)Qty, "
            sql &= "  isnull((select Rate from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N'),0)Rt, "
            sql &= "  isnull((select [Days] from APEX_PrePnlCostTrans where prepnlcostID=Apex_PrePnLCost.prepnlcostID and Approvedstatus='N'),0)Dy "
            sql &= " from Apex_PrePnLCost where RefBriefID=" & hdnBriefID.Value
            sql &= " "

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            gvPrePnLCost.DataSource = ds
            gvPrePnLCost.DataBind()
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
            Dim sql As String = "Select Sum(PreEventCost) as SumPreEventCost,Sum(PreEventServiceTax) as PreEventServiceTax,Sum(PreEventTotal) as PreEventTotal  from APEX_PrePnLCost where RefBriefID=" & hdnBriefID.Value
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

    Public Sub SendForApprovalBranchHead(ByVal type As String, ByVal bid As String, ByVal desig As String, ByVal approvalstr As String, Optional stg As String = "PrepnlApproval")
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
            sql &= "('" & approvalstr & "'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Approve Pre P&L Amount Increase'"
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

    Protected Sub gvPrePnLCost_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvPrePnLCost.RowUpdating
        Try
            Dim prepnlcostid As New HiddenField
            prepnlcostid = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("hdnPrePnLCostID"), HiddenField)
            Dim Nature As New TextBox
            Dim Supplier As New TextBox
            Dim PreEventCost As New TextBox
            Dim Category As New DropDownList
            Dim hdnpreeventcost As New HiddenField
            hdnpreeventcost = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("hdnpreeventcost"), HiddenField)

            Nature = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtNatureOfExpenses"), TextBox)
            Category = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_ddlCategory"), DropDownList)
            Supplier = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtSupplierName"), TextBox)
            PreEventCost = DirectCast(gvPrePnLCost.Rows(e.RowIndex).FindControl("gv_txtPreEventCost"), TextBox)
            Dim d2, d3, d4, d5, d6, d7 As String
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


            'Call the function to update the GridView
            UpdateRecord(d1, d2, d3, d4, d5, d6, d7, hdnpreeventcost.Value)

            gvPrePnLCost.EditIndex = -1

            'Rebind Gridview to reflect changes made

            BindGrid()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvPrePnLCost_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvPrePnLCost.RowCancelingEdit
        Try
            ' switch back to edit default mode
            gvPrePnLCost.EditIndex = -1
            'Rebind the GridView to show the data in edit mode
            BindGrid()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub UpdateRecord(ByVal prepnlcostid As String, ByVal Nature As String, ByVal Supplier As String, ByVal PreEventCost As String, ByVal PreEventServiceTax As String, ByVal PreEventTotal As String, ByVal Category As String, ByVal hdnpreeventcost As String)
        Try
            If Clean(PreEventCost) > Clean(hdnpreeventcost) Then
                Dim REquestAmt As String = ""
                REquestAmt = Clean(PreEventCost - hdnpreeventcost)

                'SendForApprovalBranchHead(clsApex.NotificationType.approveprepnlAmt, prepnlcostid, 2)

                Dim sql As String = ""
                sql &= " insert into APEX_PrePnlCostTrans(PrepnlcostID,PrvEventTotalPRV,PrevEventTotalCUR,REquestAmt,RequestDT,ApprovedBy,ApprovedStatus)"
                sql &= " values(" & prepnlcostid & "," & hdnpreeventcost & "," & PreEventCost & "," & REquestAmt & ",getdate(),'0','N')"
                sql &= ""
                sql &= ""
                sql &= ""

                'Dim sql As String = ""
                'sql &= "UPDATE [APEX_PrePnLCost]"
                'sql &= "  SET [RefPrePnLID] = " & hdnPnLID.Value & ""
                'sql &= "  ,[RefBriefID] = " & hdnBriefID.Value & ""
                'sql &= ",[NatureOfExpenses] = '" & Clean(Nature) & "'"
                'sql &= "  ,[SupplierName] = '" & Clean(Supplier) & "'"
                'sql &= " ,[PreEventCost] = '" & Clean(PreEventCost) & "'"
                ''sql &= "  ,[PreEventServiceTax] = '" & Clean(PreEventServiceTax) & "'"
                ''sql &= "  ,[PreEventTotal] = '" & Clean(PreEventTotal) & "'"
                'sql &= "  ,[Category] = '" & Category & "'"
                'sql &= " WHERE PrePnLCostID=" & prepnlcostid

                If ExecuteNonQuery(sql) > 0 Then
                    FillPrePnLCost()
                    FillTotalQoute()
                    divMsgAlert.Visible = True
                    divContent.Visible = False
                    Label2.Text = "Your Rs. " & REquestAmt & " send requested to KAM for approval"
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try

    End Sub

    Protected Sub gvPrePnLCost_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPrePnLCost.RowCommand
        Try
            If e.CommandName = "Add" Then
                Dim totalquote As Double = 0

                Dim frow As GridViewRow = gvPrePnLCost.FooterRow
                Dim txtNature As New TextBox
                Dim txtSupplier As New TextBox
                Dim txtPreEventCost As New TextBox
                Dim ddlCategory As New DropDownList

                txtNature = CType(frow.FindControl("txtNature"), TextBox)
                txtSupplier = CType(frow.FindControl("txtSupplier"), TextBox)
                txtPreEventCost = CType(frow.FindControl("txtPreEventCost"), TextBox)
                ddlCategory = CType(frow.FindControl("ddlCategory"), DropDownList)

                Dim sql As String = "Insert into APEX_PrePnLCost (RefPrePnLID,RefBriefID,NatureOfExpenses,SupplierName,PreEventCost,PreEventServiceTax,PreEventTotal,Category) Values ("
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

                sql &= "," & Clean(Convert.ToDouble(txtPreEventCost.Text))


                sql &= ",'" & ddlCategory.SelectedItem.Text & "'"
                sql &= ")"

                If ExecuteNonQuery(sql) > 0 Then
                    BindGrid()
                    FillTotalQoute()
                End If
            End If
            If e.CommandName = "Delete" Then
                Dim hdnPrePnLCostID As New HiddenField

                Dim row As GridViewRow = CType(CType(e.CommandSource, ImageButton).NamingContainer, GridViewRow)

                hdnPrePnLCostID = CType(row.FindControl("hdnPrePnLCostID"), HiddenField)

                Dim sql As String = "Delete from APEX_PrePnLCost where PrePnLCostID=" & hdnPrePnLCostID.Value
                If ExecuteNonQuery(sql) > 0 Then
                    BindGrid()
                    FillTotalQoute()
                End If
            End If
            If e.CommandName = "Edit" Then
                Dim hdnPrePnLCostID As New HiddenField
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

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Dim profitpercent As Double = vbNull
            Dim apex As New clsApex

            Dim PreEventProfit As Double = vbNull
            If lblPreEventTotal.Text.Length > 0 Then
                Dim sql As String = "Update APEX_PrePnL set "
                If txtPreEventQuote.Text <> "" Then
                    sql &= "PreEventQuote = " & Clean(txtPreEventQuote.Text)
                    If txtPreEventQuote.Text = "0.00" Then
                        txtPreEventQuote.Text = "0"
                    End If
                Else
                    txtPreEventQuote.Text = "0"
                    sql &= "PreEventQuote = NULL"
                End If
                If txtPreEventQuote.Text <> "" And lblTotalCost.Text <> "" Then
                    If txtPreEventQuote.Text > "0" Then
                        PreEventProfit = (Convert.ToDouble(txtPreEventQuote.Text) - Convert.ToDouble(lblTotalCost.Text)).ToString()
                    Else
                        PreEventProfit = "0"
                    End If

                End If
                If PreEventProfit <> vbNull Then
                    sql &= ",PreProfit= " & PreEventProfit
                Else
                    sql &= ",PreProfit= NULL"
                End If

                If PreEventProfit <> vbNull Then
                    If txtPreEventQuote.Text > "0" Then
                        profitpercent = Math.Round((((PreEventProfit) / Convert.ToDouble(txtPreEventQuote.Text)) * 100), 2)
                    Else
                        profitpercent = "0"
                    End If

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

                sql &= " where RefBriefID=" & hdnBriefID.Value & " and PrePnLID=" & hdnPnLID.Value


                If ExecuteNonQuery(sql) > 0 Then

                    'TransferTableData()
                    'Dim sql1 As String = "Delete from APEX_PrePnLCost where RefPrePnLID=" & hdnPnLID.Value
                    'ExecuteNonQuery(sql1)
                    If profitpercent Then
                        lblPreEventProfit.Text = profitpercent
                    Else
                        lblPreEventProfit.Text = "0"
                    End If
                    Label2.Text = "Your Pre P&L Send Successfully For Estimate"
                    divMsgAlert.Visible = True
                    divContent.Visible = False
                    'Response.Redirect("Home.aspx")
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnsendapproval_Click(sender As Object, e As EventArgs) Handles btnsendapproval.Click

        Dim strmsg As String = ""
        Dim strrej As String = ""
        For i As Integer = 0 To gvPrePnLCost.Rows.Count - 1
            Dim PreEventTotal As New HiddenField
            PreEventTotal = DirectCast(gvPrePnLCost.Rows(i).FindControl("hdnapprovalaMt"), HiddenField)
            Dim prepnlcostid As New HiddenField
            prepnlcostid = DirectCast(gvPrePnLCost.Rows(i).FindControl("hdnPrePnLCostID"), HiddenField)
            Dim approval As New DropDownList
            approval = DirectCast(gvPrePnLCost.Rows(i).FindControl("ddlStatus"), DropDownList)
            Dim hdnpreeventcost As New HiddenField
            hdnpreeventcost = DirectCast(gvPrePnLCost.Rows(i).FindControl("hdnpreeventcost"), HiddenField)
            Dim sql1 As New StringBuilder
            Dim approveamt As New TextBox


            Dim Quantity As New TextBox
            Dim rate As New TextBox
            Dim days As New TextBox

            Quantity = DirectCast(gvPrePnLCost.Rows(i).FindControl("gv_txtquantity"), TextBox)
            rate = DirectCast(gvPrePnLCost.Rows(i).FindControl("gv_txtRate"), TextBox)
            days = DirectCast(gvPrePnLCost.Rows(i).FindControl("gv_txtDays"), TextBox)

            'approveamt = DirectCast(gvPrePnLCost.Rows(i).FindControl("txtamt"), TextBox)
            approveamt.Text = (Convert.ToDouble(Quantity.Text) * Convert.ToDouble(rate.Text) * Convert.ToDouble(days.Text)).ToString()
            'approval.Text = (Quantity.Text * rate.Text * days.Text).ToString()
            If approveamt.Text = "" Then
                approveamt.Text = "0.00"
            End If
            If btnsendapproval.Text = "Reject" Then

                sql1.Append("update APEX_PrePnlCostTrans set approvedstatus='R'")
                sql1.Append(" where prepnlcostid='" & prepnlcostid.Value & "' and approvedstatus='N'")
                sql1.Append("")
                ExecuteNonQuery(sql1.ToString)

            Else
                If approval.SelectedItem.Value = "Y" Then
                    'sql1.Append("update APEX_PrePnlCostTrans set approvalAmt=requestAmt,approvalDT=getdate(),approvedBy='" & getLoggedUserID() & "',approvedstatus='" & approval.SelectedItem.Value & "'")
                    sql1.Append("update APEX_PrePnlCostTrans set approvalAmt='" & approveamt.Text & "',approvalDT=getdate(),approvedBy='" & getLoggedUserID() & "',approvedstatus='" & approval.SelectedItem.Value & "'")
                    sql1.Append(" where prepnlcostid='" & prepnlcostid.Value & "' and approvedstatus='N'")
                    sql1.Append("")
                    If ExecuteNonQuery(sql1.ToString()) Then

                        strmsg = "Approved &"

                        Dim sql As String = ""
                        sql &= "UPDATE [APEX_PrePnLCost]"
                        sql &= "  SET [RefPrePnLID] = " & hdnPnLID.Value & ""
                        'sql &= "  ,[PreEventcost] = '" & Clean(PreEventTotal.Value + Math.Round(CType(approveamt.Text, Decimal), 2)) & "'"
                        sql &= "  ,[PreEventcost] = '" & Clean(Math.Round(CType(approveamt.Text, Decimal), 2)) & "'"

                        sql &= "  ,[Quantity] = '" & Quantity.Text & "'"
                        sql &= "  ,[Rate] = '" & rate.Text & "'"
                        sql &= "  ,[Days] = '" & days.Text & "'"

                        sql &= " WHERE PrePnLCostID=" & prepnlcostid.Value
                        If ExecuteNonQuery(sql) Then
                            FillEstimate()
                            InsertPrePnlVSEstimateHistory(CInt(hdnJobCardID.Value), Convert.ToDecimal(txtEstimate.Text), Convert.ToDecimal(txtPrePnL.Text), Convert.ToDecimal(txtActual.Text))


                            If minmumCCprofitpercentage() > txtActual.Text Then
                                'divContent.Visible = False
                                divwarning.Visible = True
                                lblwarning.Text = "Your data has been approved but your profit percentage(" & txtActual.Text & ") is below the approved percentage(" & minmumCCprofitpercentage() & "). "
                            End If
                        End If
                    End If
                ElseIf approval.SelectedItem.Value = "R" Then
                    strrej = "Reject"
                    sql1.Append("update APEX_PrePnlCostTrans set approvedstatus='" & approval.SelectedItem.Value & "'")
                    sql1.Append(" where prepnlcostid='" & prepnlcostid.Value & "' and approvedstatus='N'")
                    sql1.Append("")
                    ExecuteNonQuery(sql1.ToString)

                End If
            End If




        Next
        Dim capex As New clsApex
        capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
        'SendForApprovalBranchHead(clsApex.NotificationType.approveprepnlAmtsuccess, hdnBriefID.Value, 3, "Prepnl additional Amount " & strmsg & " " & strrej & " From KAM")
        SendForApprovalBranchHeadPrepnl(clsApex.NotificationType.approveprepnlAmtsuccess, hdnBriefID.Value, 3, "KAM Updates On Additional cost Request")
        divMsgAlert.Visible = True
        divContent.Visible = False
        Label2.Text = "Updated successfully"
        'DirectCast(gvPrePnLCost.Rows(0).FindControl("MyHiddenField"), HiddenField).Value()

    End Sub

    Private Sub FillEstimate()
        Try
            If hdnJobCardID.Value <> "" Then
                Dim sql As String = "select   isnull(EstimatedTotal,'0') as EstimatedSubTotal,"
                sql &= "  (Select sum(PreEventcost) from APEX_PrePnLcost where RefBriefID= j.RefBriefID) as ppl,"
                sql &= " ((EstimatedTotal - (Select sum(PreEventcost) from  APEX_PrePnLcost where RefBriefID= j.RefBriefID))/EstimatedTotal * 100)"
                sql &= " as profitper  from APEX_JobCard as j "
                sql &= " Inner join APEX_Estimate as e on j.RefBriefID  = e.RefBriefID "
                sql &= " where j.JobCardID= " & hdnJobCardID.Value
                sql &= "  "

                Dim ds As New DataSet
                ds = ExecuteDataSet(sql)
                Dim per As Decimal = 0
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0)("EstimatedSubTotal")) Then
                        txtEstimate.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal")
                    End If
                    'If Not IsDBNull(ds.Tables(0).Rows(0)("ppl")) Then
                    '    txtPrePnL.Text = ds.Tables(0).Rows(0)("ppl")
                    'End If
                    txtPrePnL.Text = CInt(lblcost.Text) + CInt(lblTotalCost.Text)
                    txtActual.Text = Math.Round(((txtEstimate.Text - txtPrePnL.Text) / txtEstimate.Text) * 100, 2)
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function minmumCCprofitpercentage() As Double
        Dim perc As Double
        Dim sql As String = "select minimumCCprofit from apex_jobcard where jobcardID=" & hdnJobCardID.Value
        perc = ExecuteSingleResult(sql, _DataType.AlphaNumeric)
        Return perc
    End Function

    Private Sub getEstimatevsprepnlHistory()
        Dim sql As String = " select RefjobcardID,EstimateTotal,PrepnlTotal,ProfitPerc,InsertedOn from Apex_EstimateVSprepnlHistory where refjobcardID=" & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        gridfinalesimate.DataSource = ds
        gridfinalesimate.DataBind()
    End Sub

    Private Sub btnsendrequesttobranchhead_Click(sender As Object, e As EventArgs) Handles btnsendrequesttobranchhead.Click
        Dim capex As New clsApex

        Dim percentage As Double = vbNull
        Dim preeventprofit As Double = vbNull
        Dim totalquote As Double = vbNull
        Dim getestimatevalueforrequest As Double = vbNull

        getestimatevalueforrequest = ExecuteSingleResult("Select convert(Numeric(18,2),Sum((case when tempestimate=0 then estimate else tempestimate end +(case when tempestimate =0 then estimate else tempestimate end * case when TempAgencyfee =0 then Agencyfee else TempAgencyfee end)/100)))tempestimate from APEX_TempEstimate where refbriefID=" & hdnBriefID.Value, _DataType.Numeric)

        totalquote = txtPrePnL.Text
        preeventprofit = (Convert.ToDouble(getestimatevalueforrequest) - Convert.ToDouble(totalquote))
        percentage = Math.Round((((preeventprofit) / Convert.ToDouble(getestimatevalueforrequest)) * 100), 2)


        If InsertPrePnlVSEstimateHistory(hdnJobCardID.Value, getestimatevalueforrequest, totalquote, txtActual.Text, " Pre PnL : " & Clean(txtreason.Text)) Then


            SendForApprovalToBranchHead(clsApex.NotificationType.PrepnlapprovalfromkamtoPM_AfterJC, hdnBriefID.Value, 6, percentage)
            Messagebranchhead.Visible = False
            divContent.Visible = False
            divMsgAlert.Visible = True
            Label2.Text = "Your Prepnl details has been sent For approval To branch head"
            capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
        Else
            divContent.Visible = False
            divMsgAlert.Visible = True
            Label2.Text = "Your Prepnl details has already been sent For approval To branch head"
        End If
    End Sub

    'prepnl
    Public Sub SendForApprovalBranchHeadPrepnl(ByVal type As String, ByVal bid As String, ByVal desig As String, ByVal approvalstr As String, Optional stg As String = "PrepnlApproval")
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
            sql &= "('" & approvalstr & "'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Approve Pre P&L Amount Increase'"
            sql &= ",'H'"
            sql &= "," & type
            sql &= "," & bid
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                If stg = "other" Then
                    InsertRecieptentDetailsPrepnl(type, bid, desig)
                Else
                    Dim Estimate As String = "Estemate"
                    InsertRecieptentDetailsPrepnl(type, bid, desig, Estimate)
                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub InsertRecieptentDetailsPrepnl(ByVal type As String, ByRef bid As String, ByVal deg As String, Optional stag As String = "other")
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
                'sql3 = "Select b.insertedBy from dbo.APEX_Brief  as b    where BriefID= " & hdnBriefID.Value
                sql3 = "select projectManagerID as insertedBy from APEX_JobCard where refBriefID= " & hdnBriefID.Value
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

    'end prepnl

    Public Sub SendForApprovalToBranchHead(ByVal type As String, ByVal bid As String, ByVal desig As String, ByVal percentage As Decimal, Optional stg As String = "other")
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
        'If type = 10 Then
        '    sql &= "('Estimate for your approval.'"
        '    'sql &= ",'Your Estimate Rejected'"
        '    sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Your Estimate Rejected'"
        'Else
        sql &= "('Profit percentage approval.'"
        'sql &= ",'Approval for the Estimate'"

        sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Pre PnL approval with profit percentage " & percentage & "%, which is below the " & lblbriefPrimaryActivity.Text & " category approved %age(" & minmumCCprofitpercentage() & ")'"
        'End If



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
    End Sub


    Private Sub InsertRecieptentDetails(ByVal type As String, ByRef bid As String, ByVal deg As String, Optional stag As String = "other")
        Dim notificationid As String = ""
        Dim title As String = ""
        Dim message As String = ""
        Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & type & " and AssociateID=" & bid & " order by NotificationID desc"
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
    End Sub

    Private Sub InsertRecieptentDetails()
        Dim message As String = ""
        Dim notificationid As String = ""
        Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & NotificationType.PPNLApproved & " and AssociateID=" & hdnBriefID.Value & " order by NotificationID desc"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationID")) Then
                notificationid = ds.Tables(0).Rows(0)("NotificationID").ToString()
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationTitle")) Then
                Title = ds.Tables(0).Rows(0)("NotificationTitle").ToString()
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
        sql1 &= "," & getprojectmanager()
        sql1 &= ",'N'"
        sql1 &= ",'N'"
        sql1 &= ",'N'"
        sql1 &= ",NULL"
        sql1 &= ",NULL"
        sql1 &= "," & getLoggedUserID() & ")"

        If ExecuteNonQuery(sql1) > 0 Then
            Dim emailid As String = ""
            Dim projectmanager As String = getprojectmanager()
            If projectmanager <> "" Then
                Dim uid As Integer = Convert.ToInt32(getprojectmanager())
                emailid = GetEmailIDByUserID(uid)
            End If
            If emailid <> "" Then
                sendMail(Title, message, "", emailid, "")
            End If

        End If
    End Sub
    Private Function getprojectmanager() As String
        Dim pm As String = ""
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


        Return pm
    End Function
End Class