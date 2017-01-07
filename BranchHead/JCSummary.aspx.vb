Imports clsDatabaseHelper
Imports clsMain
Imports System.Data
Imports clsApex
Imports System.Data.OleDb
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System
Imports System.Drawing
Partial Class Estimate_VS_Actuals
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btnCancelHome.Visible = False
            divError.Visible = False
            btnExcel.Visible = False
            MessageDiv.Visible = False
            divwarning.Visible = False

            trAppRemarks.Visible = False
            btnFinal.Visible = False
            btnCancelApp.Visible = False
            If Len(Request.QueryString("jid")) > 0 Then
                If Request.QueryString("jid") <> Nothing Then
                    If Request.QueryString("jid").ToString() <> "" Then
                        Divback.Visible = False
                        hdnJobCardID.Value = Request.QueryString("jid").ToString()
                        Dim capex As New clsApex
                        hdnBrief.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)
                        lbljcno.Text = capex.GetJobCardByjcID(hdnJobCardID.Value)
                        getservicetax(hdnBrief.Value)
                        lblMangnmtFees.Visible = False
                        'lblServiceTaxPer.Visible = False
                        lblMFeePer.Visible = False
                        btnApproval.Visible = False

                        If hdnJobCardID.Value <> "" Then
                            If Len(Request.QueryString("nid")) > 0 Then
                                If Request.QueryString("nid") <> Nothing Then
                                    btnCancel.Visible = False
                                    btnCancelApp.Visible = True
                                    hdnNodificationID.Value = Request.QueryString("nid")
                                    capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
                                End If
                            End If

                            Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
                            If role = "B" Or role = "A" Or role = "H" Or role = "F" Then
                                GetEstimateID()
                                If role = "F" Then
                                    gdvEstimate.Enabled = False
                                    btnAdd.Visible = False
                                Else
                                    gdvEstimate.Enabled = True

                                    If getestimatedvsActual() = "P" Then
                                        If role <> "H" Then
                                            divwarning.Visible = True
                                            lblwarning.Text = "Your profit percentage is below the approved percentage. Waiting approval from branch head."
                                            btnAdd.Visible = False
                                            If getestimatedvsActual() = "Y" Then
                                                'Response.Redirect("Invoiceing.aspx?jid=" & hdnJobCardID.Value)
                                            End If
                                        End If
                                    Else
                                        btnAdd.Visible = True
                                        If role <> "H" Then
                                            divwarning.Visible = False
                                            If getestimatedvsActual() = "Y" Then
                                                'Response.Redirect("Invoiceing.aspx?jid=" & hdnJobCardID.Value)
                                            End If
                                        End If
                                    End If

                                End If
                                If role = "H" Then

                                    gdvEstimate.Enabled = False
                                    btnAdd.Visible = False


                                End If

                                If IsjobCardCompleted(hdnBrief.Value) = True Then
                                    FillPrePnlDetails()
                                    bindprepnldetail(hdnBrief.Value)
                                    BindFinalEstimateGrid1()
                                    BindBriefData(hdnBrief.Value)
                                    BindpostpnlGrid()

                                    'FillOtherDetails()
                                    BindEstimateGrid()

                                    gvDisplay.Visible = False
                                    gdvEstimate.Visible = True

                                    CLoseAllControls()
                                    MessageDiv.Visible = False

                                    divContent.Visible = True
                                    If role = "H" Then
                                        Dim percentage As Double = "0.00"
                                        Dim preeventprofit As Double = (Convert.ToDouble(hdnTotal.Value) - Convert.ToDouble(getpostpnltotal()))
                                        percentage = Math.Round((((preeventprofit) / Convert.ToDouble(hdnTotal.Value)) * 100), 2)
                                        'hdnTotal.Value 
                                        'getpostpnltotal()
                                        txtfinalEstimate.Text = hdnTotal.Value
                                        txtPostPnL.Text = getpostpnltotal()
                                        txtActual.Text = percentage

                                        If minmumCCprofitpercentage() > percentage Then
                                            btnApproval.Visible = True
                                        Else
                                            btnApproval.Visible = False
                                        End If
                                    End If

                                    lblestimate.Text = lblTotal1.Text
                                    lblprepnl.Text = lblPreEventtotal.Text
                                    lblestimateactual.Text = Math.Round((((lblestimate.Text - lblprepnl.Text) / lblestimate.Text) * 100), 2)
                                Else
                                    MessageDiv.Visible = True
                                    lblMsg.Text = "Job Card not Closed"
                                    divContent.Visible = False

                                    Divback.Visible = True
                                End If
                                Dim percentage1 As Double = "0.00"
                                If hdnTotal.Value = "" Then
                                    hdnTotal.Value = "0"
                                End If
                                Dim preeventprofit1 As Double = (Convert.ToDouble(hdnTotal.Value) - Convert.ToDouble(getpostpnltotal()))
                                percentage1 = Math.Round((((preeventprofit1) / Convert.ToDouble(hdnTotal.Value)) * 100), 2)
                                'hdnTotal.Value 
                                'getpostpnltotal()
                                txtfinalEstimate.Text = hdnTotal.Value
                                txtPostPnL.Text = getpostpnltotal()
                                txtActual.Text = percentage1

                            Else
                                CallDivError()
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

            Else
                CallDivError()
            End If
        End If
    End Sub
   
    Private Sub CalculateSubTotal()
        Dim sqlSubTotalString As String = ""
        Dim ds As New DataSet

        sqlSubTotalString = "Select SUM(Estimate) as SubTotal from APEX_TempEstimate where RefBriefID=" & hdnBrief.Value
        ds = ExecuteDataSet(sqlSubTotalString)

        If ds.Tables(0).Rows.Count > 0 Then
            lblSubTotal.Text = ds.Tables(0).Rows(0)("SubTotal").ToString()
        End If
    End Sub

   
    Private Sub InsertEstimateCost()
        Dim sqlCostString As String = ""

        sqlCostString &= "Insert Into APEX_EstimateCost Select RefEstimateID,RefBriefID,Particulars,Quantity,Rate,Days,Estimate,Remarks,Category "
        sqlCostString &= "from APEX_TempEstimate where RefBriefID=" & hdnBrief.Value

        ExecuteNonQuery(sqlCostString)
    End Sub

    Private Sub DeleteFromTemp()
        Dim sqlDeleteTempString As String = ""

        sqlDeleteTempString = "Delete From APEX_TempEstimate where RefBriefID=" & hdnBrief.Value
        ExecuteNonQuery(sqlDeleteTempString)
    End Sub
    Private Sub BindFinalEstimateGrid()
        Dim sqlGridString As String = ""
        Dim ds As New DataSet

        sqlGridString = "Select ROW_NUMBER() OVER(ORDER BY EstimateCostID DESC) AS Row,AgencyFee,RefEstimateID as EstimateID,EstimateParticulars as Particulars,EstimateQty as Quantity,EstimateRate as Rate,EstimateDays as Days,isnull(EstimateAmount,'0.00') as Estimate,EstimateRemarks as Remarks  from APEX_EstimateCost where RefBriefID=" & hdnBrief.Value
        ds = ExecuteDataSet(sqlGridString)


        gdvEstimate.DataSource = ds
        gdvEstimate.DataBind()

        gvDisplay.DataSource = ds
        gvDisplay.DataBind()
        FillOtherDetails()

    End Sub

    Private Sub BindEstimateGrid()
        Dim sqlGridString As String = ""
        Dim ds As New DataSet
        sqlGridString = "Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,*,isnull(actual,0)Actualestimate from  "
        sqlGridString &= " APEX_TempEstimate where RefBriefID=" & hdnBrief.Value & " "
        'sqlGridString &= "  union"
        'sqlGridString &= " Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,*"
        'sqlGridString &= " from APEX_EstimateCost where RefBriefID=" & hdnBrief.Value & " "
        'sqlGridString &= " order by refEstimateID Asc"
        ds = ExecuteDataSet(sqlGridString)
        hdnTotal.Value = ds.Tables(0).Rows(0)("total")
        gdvEstimate.DataSource = ds
        gdvEstimate.DataBind()

        If gdvEstimate.Rows.Count = 0 And Not gdvEstimate.DataSource Is Nothing Then
            Dim dt As Object = Nothing
            If gdvEstimate.DataSource.GetType Is GetType(Data.DataSet) Then
                dt = New System.Data.DataSet
                dt = CType(gdvEstimate.DataSource, System.Data.DataSet).Tables(0).Clone()
            ElseIf gdvEstimate.DataSource.GetType Is GetType(Data.DataTable) Then
                dt = New System.Data.DataTable
                dt = CType(gdvEstimate.DataSource, System.Data.DataTable).Clone()
            ElseIf gdvEstimate.DataSource.GetType Is GetType(Data.DataView) Then
                dt = New System.Data.DataView
                dt = CType(gdvEstimate.DataSource, System.Data.DataView).Table.Clone
            End If
            dt.Rows.Add(dt.NewRow())
            gdvEstimate.DataSource = dt
            gdvEstimate.DataBind()
            gdvEstimate.Rows(0).Visible = False
            gdvEstimate.Rows(0).Controls.Clear()
        End If
    End Sub
    Private Function CalculateProfit() As String
        Dim flag As Boolean = False
        Dim sql As String = "select  ((EstimatedGrandTotal - PreEventQuote)/EstimatedGrandTotal * 100) as profitper from APEX_JobCard as j Inner join APEX_Estimate as e on j.RefBriefID  = e.RefBriefID Inner Join APEX_PrePnL as p on j.RefBriefID = p.RefBriefID where j.JobCardID = " & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        Dim per As Decimal = 0
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)(0).ToString()) Then
                per = ds.Tables(0).Rows(0)(0).ToString()
            End If
        End If
        If per > 0 Then
            flag = True
        End If

        Return flag
    End Function
    Private Function TotalQoute() As Double
        Dim str As String = ""
        Dim result As Double = vbNull
        Dim sql As String = "Select Sum(PreEventCost) as SumPreEventCost  from APEX_PrePnLCost where RefBriefID=" & hdnBrief.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("SumPreEventCost")) Then
                result = ds.Tables(0).Rows(0)("SumPreEventCost").ToString()
            End If
        End If
        Return result
    End Function

    Private Sub Updatejobcard(ByVal status As String)
        Dim sql As String = "update APEX_JobCard set JobCompleted = 'Y',IsEstimateVsActuals='" & status & "' where JobcardID = " & hdnJobCardID.Value
        ExecuteNonQuery(sql)
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If gdvEstimate.Rows.count > 0 Then
            Dim sql As String
            For Each row As GridViewRow In gdvEstimate.Rows

                Dim Actualvalue As TextBox = row.FindControl("txt_actual")
                Dim Agencyfee As TextBox = row.FindControl("txt_agencyfee")
                Dim Estimateid As HiddenField = row.FindControl("hdnEstimateID")

                Dim value As String = Actualvalue.Text
                If value <> "" And Estimateid.value <> "" Then
                    sql = "update APEX_TempEstimate set Actual=" & value & " , AgencyFee=" & Agencyfee.Text & " ,Subtotal=" & hdnSubTotal.Value & ",Managementfee=" & hdnMangnmtFees.Value & " ,total=" & hdnTotal.Value & ",servicetax=" & hdnServiceTax.Value & ",GrandTotal=" & hdnGrand.Value & " where EstimateID=" & Estimateid.Value & ""
                    ExecuteNonQuery(Sql)

                End If
            Next
            If hdnJobCardID.Value <> "" Then

                Dim percentage As Double = "0.00"
                Dim preeventprofit As Double = (Convert.ToDouble(hdnTotal.Value) - Convert.ToDouble(getpostpnltotal()))
                percentage = Math.Round((((preeventprofit) / Convert.ToDouble(hdnTotal.Value)) * 100), 2)
                'hdnTotal.Value 
                'getpostpnltotal()
                If minmumCCprofitpercentage() > percentage Then
                    divwarning.Visible = True
                    lblwarning.Text = "Your profit percentage(" & percentage & ") is below the approved percentage(" & minmumCCprofitpercentage() & "). Waiting approval from branch head."
                    Updatejobcard("P")
                    SendForApprovalToBranchHead(clsApex.NotificationType.finalestimateapproval, hdnJobCardID.Value, 6)
                    btnAdd.Visible = False
                Else
                    Updatejobcard("Y")
                    Response.Redirect("Invoiceing.aspx?jid=" & hdnJobCardID.Value)
                End If
            End If
            Dim percentage1 As Double = "0.00"
            Dim preeventprofit1 As Double = (Convert.ToDouble(hdnTotal.Value) - Convert.ToDouble(getpostpnltotal()))
            percentage1 = Math.Round((((preeventprofit1) / Convert.ToDouble(hdnTotal.Value)) * 100), 2)
            txtfinalEstimate.Text = hdnTotal.Value
            txtPostPnL.Text = getpostpnltotal()
            txtActual.Text = percentage1
            End If

    End Sub

    Private Function minmumCCprofitpercentage() As Double
        Dim perc As Double
        Dim sql As String = "select minimumCCprofit from apex_jobcard where jobcardID=" & hdnJobCardID.Value
        perc = ExecuteSingleResult(sql, _DataType.AlphaNumeric)
        Return perc
    End Function
    Private Function getpostpnltotal() As Double
        Dim perc As Double
        Dim sql As String = "select sum(posteventcost)posteventtotal from Apex_tempPostPnLCost where refjobcardID=" & hdnJobCardID.Value
        perc = ExecuteSingleResult(sql, _DataType.AlphaNumeric)
        Return perc
    End Function

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
      
        Response.Redirect("JobCard.aspx")
    End Sub

    Private Function CheckEstimated() As Boolean
        Dim result As Boolean = True
        Dim sql As String = "Select IsEstimated from APEX_JobCard where RefBriefID=" & hdnBrief.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0).ToString() = "Y" Then
                result = True
            Else
                result = False
            End If
        End If
        Return result
    End Function

    Private Sub GetEstimateID()
        Dim sql As String = "Select EstimateID from APEX_Estimate where RefBriefID=" & hdnBrief.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnEstimate.Value = ds.Tables(0).Rows(0)(0).ToString()
        End If
    End Sub


    Private Sub CLoseAllControls()



        txtMangnmtFees.Visible = False

        '  btnFinal.Visible = False

        ' txtServiceTax.Visible = False

        lblMangnmtFees.Visible = True
        btnExcel.Visible = False
        txtMFeePer.Visible = False

        'lblServiceTaxPer.Visible = True
        lblMFeePer.Visible = True
    End Sub

    Private Sub FillOtherDetails()
        Dim sql As String = "Select SUM(Actual) as SubTotal,EstimatedSubTotal,EstimatedManagentFees,EstimatedTotal,EstimatedGrandTotal,EstimatedServiceTax "
        sql &= " from APEX_TempEstimate as te"
        sql &= "  Join APEX_Estimate as et on et.EstimateID = te.RefEstimateID"
        sql &= " where et.RefBriefID = " & hdnBrief.Value
        sql &= " Group By EstimatedSubTotal,EstimatedManagentFees,EstimatedTotal,EstimatedGrandTotal,EstimatedServiceTax"

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)

        Dim mfee As Double = 0
        Dim stax As Double = 0

        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)("SubTotal").ToString().Trim() <> "" Then
                lblSubTotal.Text = ds.Tables(0).Rows(0)("SubTotal").ToString().Trim()
            Else
                lblSubTotal.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString()
            End If

            If ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString() <> "" And ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString() <> "" Then
                txtMFeePer.Text = Math.Round(((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString()) / Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString())) * 100), 2).ToString()
                mfee = Convert.ToDouble(txtMFeePer.Text)
                lblMFeePer.Text = Math.Round(mfee, 2)
            End If

            If ds.Tables(0).Rows(0)("EstimatedGrandTotal").ToString() <> "" And ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString() <> "" Then
                'txtServiceTax.Text = ((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString()) / ds.Tables(0).Rows(0)("EstimatedTotal").ToString()) * 100).ToString()
                stax = Convert.ToDouble(ddlservicetax.SelectedValue)
                lblServiceTax.Text = Math.Round(stax, 2)
                ' txtServiceTax.Text = lblServiceTax.Text
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
            'lblServiceTaxPer.Text = txtServiceTax.Text
            lblMFeePer.Text = txtMFeePer.Text
        End If
    End Sub

    Protected Sub gdvEstimate_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gdvEstimate.PageIndexChanging
        gdvEstimate.PageIndex = e.NewPageIndex
        If CheckEstimated() = False Then
            BindEstimateGrid()
        Else
            BindFinalEstimateGrid()
        End If
    End Sub

    Protected Sub gvDisplay_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvDisplay.PageIndexChanging
        gvDisplay.PageIndex = e.NewPageIndex
        If CheckEstimated() = False Then
            BindEstimateGrid()
        Else
            BindFinalEstimateGrid()
        End If
    End Sub

    Private Sub CallDivError()
        divContent.Visible = False
        divError.Visible = True
        Dim capex As New clsApex
        lblError.Text = capex.SetErrorMessage()
    End Sub


    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered

    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        BindFinalEstimateGrid()

        Dim lblId As Label
        Dim lblEstimate As Label
        Dim lblParticulars As Label
        Dim lblQuantity As Label
        Dim lblRate As Label
        Dim lblDays As Label
        Dim lblRemarks As Label
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object
        Dim count As Integer = 1


        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add
        oSheet = oBook.Worksheets(1)
        oSheet.Range("A" & count).Value = gvDisplay.HeaderRow.Cells(0).Text
        oSheet.Range("B" & count).Value = gvDisplay.HeaderRow.Cells(1).Text
        oSheet.Range("C" & count).Value = gvDisplay.HeaderRow.Cells(2).Text

        oSheet.Range("D" & count).Value = gvDisplay.HeaderRow.Cells(3).Text
        oSheet.Range("E" & count).Value = gvDisplay.HeaderRow.Cells(4).Text

        oSheet.Range("F" & count).Value = gvDisplay.HeaderRow.Cells(5).Text

        'for coloring
        oSheet.Range("A1", "F1").Interior.ColorIndex = 1
        oSheet.Range("A1", "F1").Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
        Dim i As Integer
        Dim gRow As GridViewRow
        For i = 0 To gvDisplay.Rows.Count - 1
            count += 1
            gRow = gvDisplay.Rows(i)
            lblId = gRow.FindControl("lblId")
            lblParticulars = gRow.FindControl("lblParticulars")

            lblQuantity = gRow.FindControl("lblQuantity")
            lblRate = gRow.FindControl("lblRate")
            lblDays = gRow.FindControl("lblDays")
            lblEstimate = gRow.FindControl("lblEstimate")
            lblRemarks = gRow.FindControl("lblRemarks")




            oSheet.Range("A" & count).Value = lblId.Text
            oSheet.Range("B" & count).Value = lblParticulars.Text
            oSheet.Range("C" & count).Value = lblQuantity.Text

            oSheet.Range("D" & count).Value = lblRate.Text
            oSheet.Range("E" & count).Value = lblDays.Text

            oSheet.Range("F" & count).Value = lblEstimate.Text
            oSheet.Range("G" & count).Value = lblRemarks.Text
        Next

        count += 2

        oSheet.Range("A" & count).Value = "Sub Total:"
        oSheet.Range("A" & count).Font.Bold = True
        oSheet.Range("B" & count).Value = lblSubTotal.Text
        count += 1
        oSheet.Range("A" & count).Value = "Management Fees :"
        oSheet.Range("A" & count).Font.Bold = True
        oSheet.Range("B" & count).Value = lblMangnmtFees.Text

        count += 1
        oSheet.Range("A" & count).Value = "Total :"
        oSheet.Range("A" & count).Font.Bold = True
        oSheet.Range("B" & count).Value = lblTotal.Text

        count += 1
        oSheet.Range("A" & count).Value = "Service Tax : (12.36%)"
        oSheet.Range("A" & count).Font.Bold = True
        oSheet.Range("B" & count).Value = lblServiceTax.Text

        count += 1
        oSheet.Range("A" & count).Value = "Grand Total :"
        oSheet.Range("A" & count).Font.Bold = True
        oSheet.Range("B" & count).Value = lblGrandTotal.Text

        oBook.SaveAs("uploads/Estimate" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls")
        oSheet = Nothing
        oBook = Nothing
        oExcel.Quit()
        oExcel = Nothing
        GC.Collect()


    End Sub

    Protected Sub gdvEstimate_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gdvEstimate.RowCancelingEdit

    End Sub
    
   
    Protected Sub gdvEstimate_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdvEstimate.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim hdnTaskID As HiddenField = CType(e.Row.FindControl("hdnTaskID"), HiddenField)
            Dim lblstatus As Label = CType(e.Row.FindControl("lbltaskstatus"), Label)

        End If

    End Sub

    Private Sub FillPrePnlDetails()
        Try
            hypPrePL.Visible = False
            Dim sql As String = ""
            Dim prepnlid As String = ""
            Dim apx As New clsApex
            prepnlid = apx.GetPLIDByBriefID(hdnBrief.Value)
            sql = "Select convert(varchar(10),EventDate,105) as NEventDate,* from APEX_PrePnL where RefBriefID=" & hdnBrief.Value & " and PrePnLID=" & prepnlid

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
    Private Sub FillPrePnLCost()
        Try
            Dim sql As String = "Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row,* from APEX_PrePnLCost where RefBriefID=" & hdnBrief.Value & "   order by PrePnLCostID Asc"
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
    Private Sub FillTotalQouteAfter()
        Try
            Dim sql As String = "Select Sum(PreEventCost) as SumPreEventCost,Sum(PreEventServiceTax) as PreEventServiceTax,Sum(PreEventTotal) as PreEventTotal  from APEX_PrePnLCost where RefBriefID=" & hdnBrief.Value
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
    Private Sub bindprepnldetail(ByVal bid As String)
        Try
            Dim sql As String = ""
            sql &= ""
            sql &= " Select (Select (isnull(Firstname,'')+' '+ isnull(Lastname,''))  from APEX_UsersDetails where UserDetailsID"
            sql &= " = jc.ProjectmanagerID) as PM, (Select Client from APEX_Clients where clientid = jc.RefClientID)  as Client,RefLeadID,"
            sql &= " (Select (isnull(Firstname,'')+' '+ isnull(Lastname,'')) from APEX_UsersDetails where UserDetailsID"
            sql &= " = b.insertedBy)  as KAM, b.refLeadID"
            sql &= "  ,(Select PreEventQuote  from APEX_PrePnL where RefBriefID=jc.REfBriefID) as PEQ"
            sql &= " ,jc.ProjectmanagerID,isnull(Approvalmail,'')Approvalmail1 from APEX_jobcard  as jc "
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
                lblfulpl_Mail.NavigateUrl = "../" & ds.Tables(0).Rows(0)("Approvalmail1").ToString()
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

                    'If activityType.Count = 1 Then
                    '    If activityType(0) <> "" Then
                    '        For i As Integer = 0 To activityType.Count - 1
                    '            chklActivityType.SelectedIndex = chklActivityType.Items.IndexOf(chklActivityType.Items.FindByText(activityType(i)))
                    '        Next i
                    '    End If
                    'ElseIf activityType.Count > 1 Then

                    '    For i As Integer = 0 To activityType.Count - 1
                    '        If activityType(i) <> "" Then
                    '            ' chklActivityType.Items(chklActivityType.Items.IndexOf(chklActivityType.Items.FindByText(activityType(i)))).Selected = True
                    '        End If
                    '    Next i

                    'End If
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
                'Dim res() As String
                'res = capex.FillContactInfo(ddlContactPerson.SelectedItem.Value)
                'If res(0) <> "" Then
                '    lblCEmail.Text = "Email: " & res(0)
                'Else
                '    lblCEmail.Text = ""
                'End If
                'If res(1) <> "" Then

                '    lblCContact.Text = "Contact: " & res(1)
                'Else
                '    lblCContact.Text = ""
                'End If


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

    Private Sub BindFinalEstimateGrid1()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            sql = "Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,* from  "
            sql &= " APEX_TempEstimate where RefBriefID= " & hdnBrief.Value & " "
            'sql &= "  union  "
            'sql &= " Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,*"
            'sql &= " from APEX_EstimateCost where RefBriefID= " & hdnBrief.Value & " "
            'sql &= " order by refEstimateID Asc"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then

                GridView1.DataSource = ds
                GridView1.DataBind()
            End If

            FillOtherDetails1()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub FillOtherDetails1()
        Try
            Dim sql As String = "Select SUM(Estimate) as SubTotal,EstimatedSubTotal,EstimatedManagentFees,EstimatedTotal,EstimatedGrandTotal,EstimatedServiceTax "
            sql &= " from APEX_TempEstimate as te"
            sql &= "  Join APEX_Estimate as et on et.EstimateID = te.RefEstimateID"
            sql &= " where et.RefBriefID = " & hdnBrief.Value
            sql &= " Group By EstimatedSubTotal,EstimatedManagentFees,EstimatedTotal,EstimatedGrandTotal,EstimatedServiceTax"



            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)

            Dim mfee As Double = 0
            Dim stax As Double = 0

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("SubTotal").ToString().Trim() <> "" Then
                    lblSubTotal1.Text = ds.Tables(0).Rows(0)("SubTotal").ToString().Trim()
                    lblEstSubtotal.Text = lblSubTotal1.Text
                Else
                    lblSubTotal1.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString()
                    lblEstSubtotal.Text = lblSubTotal1.Text
                End If

                If ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString() <> "" And ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString() <> "" Then
                    txtMFeePer1.Text = ((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString()) / Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString())) * 100).ToString()
                    mfee = Convert.ToDouble(txtMFeePer1.Text)
                    lblMFeePer1.Text = Math.Round(mfee, 2)
                End If

                If ds.Tables(0).Rows(0)("EstimatedGrandTotal").ToString() <> "" And ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString() <> "" Then
                    txtServiceTax1.Text = ((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString()) / ds.Tables(0).Rows(0)("EstimatedTotal").ToString()) * 100).ToString()
                    stax = Convert.ToDouble(txtServiceTax1.Text)
                    lblServiceTax1.Text = Math.Round(stax, 2)
                    txtServiceTax1.Text = lblServiceTax1.Text
                End If

                If lblSubTotal1.Text <> "" Then
                    txtMangnmtFees1.Text = Math.Round((Convert.ToDouble(lblSubTotal1.Text) * mfee) / 100, 2)
                End If

                If lblSubTotal1.Text <> "" And txtMangnmtFees1.Text <> "" Then
                    lblTotal1.Text = Math.Round(Convert.ToDouble(lblSubTotal1.Text) + Convert.ToDouble(txtMangnmtFees1.Text), 2)
                End If

                If lblTotal1.Text <> "" Then
                    lblServiceTax1.Text = Math.Round((Convert.ToDouble(lblTotal1.Text) * stax) / 100, 2)
                End If

                If lblTotal1.Text <> "" And lblServiceTax1.Text <> "" Then
                    lblGrandTotal1.Text = Math.Round(Convert.ToDouble(lblTotal1.Text) + Convert.ToDouble(lblServiceTax1.Text), 2)
                End If

                lblMangnmtFees1.Text = txtMangnmtFees1.Text
                lblServiceTaxPer1.Text = txtServiceTax1.Text
                lblMFeePer1.Text = txtMFeePer1.Text
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindpostpnlGrid()
        Try
            Dim sql As String = ""

            sql = "Select ROW_NUMBER() OVER(ORDER BY PostPnLCostID DESC) AS Row, * from Apex_PostPnLCost where RefJobCardID=" & hdnJobCardID.Value & " "
            sql &= "  union "
            sql &= "  Select ROW_NUMBER() OVER(ORDER BY PostPnLCostID DESC) AS Row,* from Apex_tempPostPnLCost where RefJobCardID=" & hdnJobCardID.Value & "  order by PostPnLCostID Asc"

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            gvpostpnl.DataSource = ds
            gvpostpnl.DataBind()
            'gvDisplay.DataSource = ds
            'gvDisplay.DataBind()

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvpostpnl_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvpostpnl.RowDataBound

        If e.Row.RowType = DataControlRowType.Footer Then
         
        End If

    End Sub

    Private Sub getservicetax(BriefID As String)
        Try
            Dim stax As String = ""
            'Dim sql As String = "select cast(round((EstimatedServiceTax/EstimatedTotal)*100,2) as Numeric(18,2))servicetax from [dbo].[APEX_Estimate] where refbriefID='" & BriefID & "'"
            Dim sql As String = "select top 1 cast(round((ServiceTax/Total)*100,2) as Numeric(18,2))servicetax from [dbo].[APEX_TempEstimate] where refbriefID='" & BriefID & "'"
            stax = ExecuteSingleResult(sql, _DataType.AlphaNumeric)
            ddlservicetax.SelectedValue = stax
            'ddlservicetax.Enabled = False
            'ddlservicetax.SelectedValue = "14.00"
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SendForApprovalToBranchHead(ByVal type As String, ByVal bid As String, ByVal desig As String, Optional stg As String = "other")
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
        sql &= "('Final Estimate for your approval.'"
        sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Final Estimate for your approval.'"
        
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
                Dim Estimate As String = "Final Estemate"
                InsertRecieptentDetails(type, bid, desig, Estimate)
            End If

        End If
    End Sub

    Public Sub SendForApprovalBranchHead(ByVal type As String, ByVal bid As String, ByVal desig As String, Optional stg As String = "other")
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
        If type = 10 Then
            sql &= "('Estimate Rejected'"
            'sql &= ",'Your Estimate Rejected'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Your Estimate Rejected'"
        Else
            sql &= "('Final Estimate Approval'"
            'sql &= ",'Approval for the Estimate'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Your Final Estimate Approved'"
        End If



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

        Dim bheadid As String = ""
        Dim sql3 As String = " "
        If stag <> "other" Then
            Dim clsApex As New clsApex
            ' Dim leadid As String = clsApex.GetLeadIDByBriefID(hdnBriefID.Value)
            sql3 = "Select b.insertedBy from dbo.APEX_Brief  as b    where BriefID= " & hdnBrief.Value
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

    Private Function getProjectName() As String
        Dim result As String = ""
        If hdnBrief.Value <> "" Then
            Dim sql As String = "Select BriefName from APEX_Brief where BriefID=" & hdnBrief.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = ds.Tables(0).Rows(0)("BriefName").ToString()
            End If
        End If

        Return result
    End Function

    Private Function getKAMName() As String
        Dim result As String = ""
        If hdnBrief.Value <> "" Then
            Dim sql As String = "select isnull(FirstName,'') + ' ' +isnull(LastName,'') as KAMName "
            sql &= " from APEX_UsersDetails as ud "
            sql &= " Inner join APEX_Brief as ab on ud.UserDetailsID = ab.InsertedBy "
            sql &= " where ab.BriefID = " & hdnBrief.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = ds.Tables(0).Rows(0)("KAMName").ToString()
            End If
        End If

        Return result
    End Function

    Private Function getestimatedvsActual() As String
        Dim sql As String = "select isnull(IsEstimateVsActuals,'N')IsEstimateVsActuals from apex_jobcard where jobcardID='" & hdnJobCardID.Value & "'"
        Return ExecuteSingleResult(sql, _DataType.AlphaNumeric)
    End Function

    Protected Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click
        Dim capex As New clsApex
        If Len(Request.QueryString("nid")) > 0 Then
            If Request.QueryString("nid") <> Nothing Then
                btnCancel.Visible = False
                btnCancelApp.Visible = True
                hdnNodificationID.Value = Request.QueryString("nid")
           
                capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            End If
        End If
        Dim percentage As Double = "0.00"
        Dim preeventprofit As Double = (Convert.ToDouble(hdnTotal.Value) - Convert.ToDouble(getpostpnltotal()))
        percentage = Math.Round((((preeventprofit) / Convert.ToDouble(getpostpnltotal())) * 100), 2)
        updateprofitperc(percentage)
        Updatejobcard("Y")
        SendForApprovalBranchHead(clsApex.NotificationType.finalestimateapproval, hdnJobCardID.Value, 2, "Final Estimate approved")
        MessageDiv.Visible = True
        lblMsg.Text = "Final Estimate Approved Successfully."

    End Sub

    Private Sub updateprofitperc(minimumccprofit As Double)
        Try
            Dim sql As String = " update Apex_jobcard  set MinimumCCProfit='" & minimumccprofit & "' where jobcardID='" & hdnJobCardID.Value & "'"
            ExecuteNonQuery(sql)
        Catch ex As Exception

        End Try
    End Sub

End Class
