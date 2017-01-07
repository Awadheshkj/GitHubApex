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
                        getservicetax(hdnBrief.Value)
                        lblMangnmtFees.Visible = False
                        'lblServiceTaxPer.Visible = False
                        lblMFeePer.Visible = False

                        If hdnJobCardID.Value <> "" Then
                            Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
                            If role = "K" Or role = "A" Or role = "H" Then
                                GetEstimateID()

                                If IsjobCardCompleted(hdnBrief.Value) = True Then

                                    ' FillPrePnlDetails()
                                    'bindprepnldetail(hdnBrief.Value)
                                    InvoicingGrid()
                                    BindFinalEstimateGrid1()
                                    'BindBriefData(hdnBrief.Value)
                                    'BindpostpnlGrid()

                                    'FillOtherDetails()
                                    BindEstimateGrid()

                                    gvDisplay.Visible = False
                                    gdvEstimate.Visible = True

                                    CLoseAllControls()
                                    MessageDiv.Visible = False

                                    divContent.Visible = True

                                Else
                                    MessageDiv.Visible = True
                                    lblMsg.Text = "Job Card not Closed"
                                    divContent.Visible = False

                                    Divback.Visible = True
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

        sqlGridString = "Select ROW_NUMBER() OVER(ORDER BY EstimateCostID DESC) AS Row,AgencyFee,RefEstimateID as EstimateID,EstimateParticulars as Particulars"
        sqlGridString &= ",EstimateQty as Quantity,EstimateRate as Rate,EstimateDays as Days,EstimateAmount as Estimate,EstimateRemarks as Remarks  from APEX_EstimateCost where RefBriefID=" & hdnBrief.Value

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
        'sqlGridString = "Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,* from  "
        'sqlGridString &= " APEX_TempEstimate where RefBriefID=" & hdnBrief.Value & " "

        'sqlGridString &= "	Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,APEX_TempEstimate.EstimateID,APEX_TempEstimate.Category,"
        'sqlGridString &= "	APEX_TempEstimate.Particulars,APEX_TempEstimate.Quantity,APEX_TempEstimate.Rate,APEX_TempEstimate.Days,"
        'sqlGridString &= "	APEX_TempEstimate.Actual,isnull(SUM(Inv.Actual),0.00)INVOICED,(APEX_TempEstimate.Actual-isnull(SUM(Inv.Actual),0.00)) BAL"
        'sqlGridString &= "	,APEX_TempEstimate.REMARKS,APEX_TempEstimate.AgencyFee from APEX_TempEstimate"
        'sqlGridString &= "	Left join Apex_Estimate_for_invoicing Inv on APEX_TempEstimate.EstimateID=INV.ReftempestimateID"
        'sqlGridString &= "	 where APEX_TempEstimate.refbriefID=" & hdnBrief.Value & " "
        'sqlGridString &= "	 GROUP BY APEX_TempEstimate.EstimateID,APEX_TempEstimate.Category,"
        'sqlGridString &= "	APEX_TempEstimate.Particulars,APEX_TempEstimate.Quantity,APEX_TempEstimate.Rate,APEX_TempEstimate.Days,"
        'sqlGridString &= "	APEX_TempEstimate.Actual,refEstimateID,APEX_TempEstimate.REMARKS,APEX_TempEstimate.AgencyFee"

        sqlGridString &= "	select  AE.EstimateID as ID,Row,T1.EstimateID,T1.Category	,T1.Particulars	,T1.Quantity,T1.Rate,T1.Days,T1.Actual"
        sqlGridString &= "	,AE.Actual as INVOICED,(T1.INVOICED)BAL,T1.REMARKS,T1.AgencyFee,T1.Isinvoiced,AE.Isinvoiced  from "
        sqlGridString &= "	(Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,APEX_TempEstimate.EstimateID,APEX_TempEstimate.Category,	"
        sqlGridString &= "	APEX_TempEstimate.Particulars,APEX_TempEstimate.Quantity,APEX_TempEstimate.Rate,APEX_TempEstimate.Days,	"
        sqlGridString &= "	APEX_TempEstimate.Actual"
        sqlGridString &= "	,isnull(Sum(Inv.Actual),0.00)INVOICED,(APEX_TempEstimate.Actual-isnull(sum(Inv.Actual),0.00)) BAL	"
        sqlGridString &= "	,APEX_TempEstimate.REMARKS,APEX_TempEstimate.AgencyFee,Inv.Isinvoiced  from APEX_TempEstimate	"
        sqlGridString &= "	Left join Apex_Estimate_for_invoicing Inv on APEX_TempEstimate.EstimateID=INV.ReftempestimateID	 "
        sqlGridString &= "	where APEX_TempEstimate.refbriefID=" & hdnBrief.Value
        sqlGridString &= "	and Inv.Isinvoiced='N'"
        sqlGridString &= "	GROUP BY APEX_TempEstimate.EstimateID,APEX_TempEstimate.Category,	"
        sqlGridString &= "	APEX_TempEstimate.Particulars,APEX_TempEstimate.Quantity,APEX_TempEstimate.Rate,APEX_TempEstimate.Days,	"
        sqlGridString &= "	APEX_TempEstimate.Actual,refEstimateID,APEX_TempEstimate.REMARKS,APEX_TempEstimate.AgencyFee,Inv.Isinvoiced"
        sqlGridString &= "	 )T1 "
        sqlGridString &= "	 inner join Apex_Estimate_for_invoicing AE on T1.EstimateID = AE.RefTempEstimateID "
        sqlGridString &= "	 where AE.Isinvoiced='Y'"
        sqlGridString &= ""
        sqlGridString &= ""
        sqlGridString &= ""
        sqlGridString &= ""
        sqlGridString &= ""

        'sqlGridString &= "  union"
        'sqlGridString &= " Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,*"
        'sqlGridString &= " from APEX_EstimateCost where RefBriefID=" & hdnBrief.Value & " "
        'sqlGridString &= " order by refEstimateID Asc"
        ds = ExecuteDataSet(sqlGridString)
        If ds.Tables(0).Rows.Count > 0 Then

            hdnisalreadyregistered.Value = "Yes"
        Else
            hdnisalreadyregistered.Value = "No"
            ds = Nothing
            sqlGridString = " delete from Apex_Estimate_for_invoicing where refbriefID='" & hdnBrief.Value & "' and Isinvoiced='N'"
            sqlGridString &= ""
            sqlGridString &= "	Select ID=0,ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,APEX_TempEstimate.EstimateID,APEX_TempEstimate.Category,"
            sqlGridString &= "	APEX_TempEstimate.Particulars,APEX_TempEstimate.Quantity,APEX_TempEstimate.Rate,APEX_TempEstimate.Days,"
            sqlGridString &= "	APEX_TempEstimate.Actual,isnull(SUM(Inv.Actual),0.00)INVOICED,(APEX_TempEstimate.Actual-isnull(SUM(Inv.Actual),0.00)) BAL"
            'sqlGridString &= "	APEX_TempEstimate.Actual,isnull(SUM(0),0.00)INVOICED,(isnull(SUM(Inv.Actual),0.00)) BAL"
            sqlGridString &= "	,APEX_TempEstimate.REMARKS,APEX_TempEstimate.AgencyFee from APEX_TempEstimate"
            sqlGridString &= "	Left join Apex_Estimate_for_invoicing Inv on APEX_TempEstimate.EstimateID=INV.ReftempestimateID"
            sqlGridString &= "	 where APEX_TempEstimate.refbriefID=" & hdnBrief.Value & " "
            sqlGridString &= "	 GROUP BY APEX_TempEstimate.EstimateID,APEX_TempEstimate.Category,"
            sqlGridString &= "	APEX_TempEstimate.Particulars,APEX_TempEstimate.Quantity,APEX_TempEstimate.Rate,APEX_TempEstimate.Days,"
            sqlGridString &= "	APEX_TempEstimate.Actual,refEstimateID,APEX_TempEstimate.REMARKS,APEX_TempEstimate.AgencyFee"
            ds = ExecuteDataSet(sqlGridString)

        End If

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

    Private Sub Updatejobcard()
        Dim sql As String = "update APEX_JobCard set JobCompleted = 'Y',  IsEstimateVsActuals='Y' where JobcardID = " & hdnJobCardID.Value
        ExecuteNonQuery(sql)
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If gdvEstimate.Rows.Count > 0 Then
            If hdnSubTotal.Value = "0.00" Then
                Response.Write("<script>alert('Please Enter Your Invoice Amount');</script>")
                Exit Sub
            End If
            Dim hdnEstimateID As New HiddenField
            For Each row As GridViewRow In gdvEstimate.Rows
                Dim sql As String = ""

                Dim Actualvalue As TextBox = row.FindControl("txt_actual")
                Dim Agencyfee As TextBox = row.FindControl("txt_agencyfee")
                Dim Estimateid As HiddenField = row.FindControl("hdnEstimateID")
                Dim invoicedamt As Label = row.FindControl("lblinvoicedamt")
                Dim lblEstimate As TextBox = row.FindControl("lblEstimate")
                Dim ID As HiddenField = row.FindControl("hdnID")

                'lblEstimate
                Dim actuamt As Decimal = "0.00"
                If Actualvalue.Text = "" Then
                    Actualvalue.Text = "0.00"
                End If
                If lblEstimate.Text = "" Then
                    lblEstimate.Text = "0.00"
                End If
                actuamt = CType(Actualvalue.Text, Decimal) + CType(invoicedamt.Text, Decimal)
                If actuamt > lblEstimate.Text Then
                    'Invoice amount can not greater than Estimated amount.
                    Response.Write("<script>alert('Invoice amount can not greater than Estimated amount.');</script>")
                    Exit Sub
                End If

                hdnEstimateID.Value = Estimateid.Value

                Dim value As String = Actualvalue.Text
                If value <> "" And Estimateid.Value <> "" Then
                    If hdnisalreadyregistered.Value = "Yes" Then
                        sql = "update Apex_Estimate_for_invoicing set Actual=" & value & ",AgencyFee=" & Agencyfee.Text & ",Subtotal=" & hdnSubTotal.Value & ",Managementfee=" & hdnMangnmtFees.Value & ",total=" & hdnTotal.Value & ",servicetax=" & hdnServiceTax.Value & ",GrandTotal=" & hdnGrand.Value & " where EstimateID=" & ID.Value & ""
                    Else
                        sql = ""
                        sql += "insert into Apex_Estimate_for_invoicing  (Actual,AgencyFee,Subtotal,Managementfee,total,servicetax,GrandTotal,RefTempEstimateID,RefBriefID)values"
                        sql += "(" & value & " ," & Agencyfee.Text & "," & hdnSubTotal.Value & "," & hdnMangnmtFees.Value & "," & hdnTotal.Value & "," & hdnServiceTax.Value & "," & hdnGrand.Value & "," & Estimateid.Value & "," & hdnBrief.Value & ")"
                        sql += ""
                        sql += ""
                    End If

                    ExecuteNonQuery(sql)

                End If
            Next
            If hdnJobCardID.Value <> "" Then
                Updatejobcard()

                Response.Redirect("viewJobInvoice.aspx?jid=" & hdnJobCardID.Value & "&EID=" & hdnEstimateID.Value)
            End If

        End If

    End Sub

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

        'txtServiceTax.Visible = False

        lblMangnmtFees.Visible = True
        btnExcel.Visible = False
        txtMFeePer.Visible = False

        ' lblServiceTaxPer.Visible = True
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
                '  txtServiceTax.Text = ((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString()) / ds.Tables(0).Rows(0)("EstimatedTotal").ToString()) * 100).ToString()
                stax = Convert.ToDouble(ddlservicetax.SelectedValue)
                lblServiceTax.Text = Math.Round(stax, 2)
                'txtServiceTax.Text = lblServiceTax.Text
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

    Private Sub InvoicingGrid()
        Dim sqlGridString As String = ""
        Dim ds As New DataSet
        sqlGridString = ""
        sqlGridString &= "  select top 1 Subtotal,Managementfee,total,servicetax,GrandTotal,'Total Estimated Amount' as Detail from Apex_tempestimate"
        sqlGridString &= "   where RefBriefID =" & hdnBrief.Value & " "
        sqlGridString &= "  UNION ALL"
        sqlGridString &= "	select top 1 Sum(Subtotal)Subtotal,Sum(Managementfee)Managementfee,Sum(total)total,Sum(servicetax)servicetax,"
        sqlGridString &= "	Sum(GrandTotal)GrandTotal,'Total Invoiced Amount' as Detail from Apex_Estimate_for_invoicing where RefBriefID =" & hdnBrief.Value & ""
        sqlGridString &= "	Group by RefTempEstimateID"
        sqlGridString &= ""
        sqlGridString &= ""
        sqlGridString &= ""
        'sqlGridString &= " APEX_TempEstimate where RefBriefID=" & hdnBrief.Value & " "
        ds = ExecuteDataSet(sqlGridString)

        GvInvoicedetail.DataSource = ds
        GvInvoicedetail.DataBind()

    End Sub
    Private Sub getservicetax(BriefID As String)
        Try
            Dim stax As String = ""
            Dim sql As String = "select cast(round((EstimatedServiceTax/EstimatedTotal)*100,2) as Numeric(18,2))servicetax from [dbo].[APEX_Estimate] where refbriefID='" & BriefID & "'"
            stax = ExecuteSingleResult(Sql, _DataType.AlphaNumeric)
            ddlservicetax.SelectedValue = stax
            ddlservicetax.Enabled = False
            'ddlservicetax.SelectedValue = "14.50"
        Catch ex As Exception

        End Try
    End Sub
End Class
