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
Partial Class OpenEstimate
    Inherits System.Web.UI.Page
    Dim total As Double = 0


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            'divError.Visible = False
            divwarning.Visible = False
            'btnExcel.Visible = False
            MessageDiv.Visible = False
            trAppButtons.Visible = False
            trAppRemarks.Visible = False
            Messagebranchhead.Visible = False
            ' lblServiceTaxPer.Text = ConfigurationManager.AppSettings("Servicetax").ToString()

            If Len(Request.QueryString("bid")) > 0 Then
                If Request.QueryString("bid") <> Nothing Then
                    If Request.QueryString("bid").ToString() <> "" Then
                        hdnBrief.Value = Request.QueryString("bid").ToString()
                        BindBriefData(hdnBrief.Value)
                        getservicetax(hdnBrief.Value)

                        FillPrePnlDetails()
                        Dim capex As New clsApex
                        If ceckestimatevsactual() Then
                            gdvEstimate.Enabled = False
                        Else
                            gdvEstimate.Enabled = True
                        End If
                        If Len(Request.QueryString("nid")) > 0 Then
                            hdnNodificationID.Value = Request.QueryString("nid")
                            capex.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())
                        End If

                        hdnJobCardID.Value = capex.GetJobCardID(hdnBrief.Value)
                        hdnjcAmount.Value = capex.GetJobCardAmount(hdnBrief.Value)
                        lbljcno.Text = capex.GetJobCardNoByBriefID(hdnBrief.Value)
                        'Dim jcnum As String
                        'jcnum = capex.GetJobCardNoByBriefID(hdnBrief.Value)
                        'If jcnum = "" Then

                        'lblMangnmtFees.Visible = False
                        'lblServiceTaxPer.Visible = False
                        lblMFeePer.Visible = False
                        bindprepnldetail(hdnBrief.Value)
                        GetEstimateID()

                        'FillOtherDetails()
                        BindEstimateGrid()
                        FillEstimate()

                        If IsjobCardCompleted(hdnBrief.Value) = False Then
                            gdvEstimate.Visible = True
                            gvDisplay.Visible = False
                        Else

                            'gvDisplay.Visible = True
                            'gdvEstimate.Visible = False
                            gvDisplay.Visible = False
                            gdvEstimate.Visible = True
                            'CLoseAllControls()
                        End If
                        If Len(Request.QueryString("mode")) > 0 Then
                            If Request.QueryString("mode") <> Nothing Then
                                If Request.QueryString("mode").ToString() <> "" Then
                                    If Request.QueryString("mode").ToString() = "Pending" Then
                                        btnFinal.Visible = True
                                        gvDisplay.Visible = True
                                        'gdvEstimate.Visible = False
                                    Else
                                        btnFinal.Visible = False
                                        gvDisplay.Visible = False
                                        'gdvEstimate.Visible = False
                                    End If
                                End If
                            End If
                        End If

                        Dim role As String = ""
                        role = capex.GetRoleNameByUserID(getLoggedUserID())

                        Dim ds As DataSet = Nothing
                        Dim sql As String = "Select top 1 * from [dbo].[Apex_EstimateVSprepnlHistory] where refjobcardID=" & hdnJobCardID.Value & " order By ID desc   "
                        ds = ExecuteDataSet(sql)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If minmumCCprofitpercentage() > ds.Tables(0).Rows(0)("profitperc") Then
                                If role = "K" Then
                                    If ds.Tables(0).Rows(0)("Isapproved").ToString() = " " Then
                                        MessageDiv.Visible = True
                                        lblMsg.Text = "Please wait for branch head approval."
                                        divContent.Visible = False
                                        Exit Sub
                                    End If
                                End If
                            ElseIf role = "H" Then
                                btnApproval.Visible = False
                                btnRejected.Visible = False
                            End If
                        ElseIf role = "H" Then
                            btnApproval.Visible = False
                            btnRejected.Visible = False

                        End If

                        If Len(Request.QueryString("Appro")) > 0 Then

                            If role = "H" Then
                                trAppButtons.Visible = True
                                'divkamdata.Disabled = True
                                gdvEstimate.Enabled = False
                                btnAdd.Visible = False


                            End If
                            getEstimatevsprepnlHistory()

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
        'Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "", "javasript:GVcalculation();", True)
    End Sub

    Private Sub bindprepnldetail(ByVal bid As String)
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
                lblClient.Text = ds.Tables(0).Rows(0)("Client")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("PEQ")) Then
                lblPEQ.Text = ds.Tables(0).Rows(0)("PEQ")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("ProjectmanagerID")) Then
                hdnClientID.Value = ds.Tables(0).Rows(0)("ProjectmanagerID")
            End If
            If TotalQoute() <> vbNull Then
                lblPreEventtotal.Text = TotalQoute()
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

    Private Sub InsertFinalEstimate()
        Dim capex As New clsApex
        If hdnNodificationID.Value <> "" Then
            capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
        End If


        Dim sqlFEstring As String = ""
        Dim total As Double = 0
        Dim servicetaxs As Double = 0
        'total = Convert.ToDouble(txtMangnmtFees.Text) + Convert.ToDouble(lblSubTotal.Text)
        Dim grandtotal As Double = 0

        'servicetaxs = (total * txtServiceTax.Text) / 100
        'grandtotal = total + servicetaxs
        'lblServiceTax.Text = servicetaxs
        total = (Convert.ToDecimal(hdnSubTotal.Value) + Convert.ToDecimal(hdnMangnmtFees.Value))
        Dim percentage As Double = vbNull
        If hdnSubTotal.Value <> "" And TotalQoute() <> vbNull Then
            Dim totalquote As Double = TotalQoute()
            Dim preeventprofit As Double = (Convert.ToDouble(total) - Convert.ToDouble(totalquote))
            percentage = Math.Round((((preeventprofit) / Convert.ToDouble(total)) * 100), 2)
        End If

        'If minmumCCprofitpercentage() > percentage Then
        '    'divContent.Visible = False
        '    divwarning.Visible = True
        '    'lblwarning.Text = "Your data has been saved but your profit percentage(" & percentage & ") is below the approved percentage(" & minmumCCprofitpercentage() & "). "
        '    lblwarning.Text = "Your data has not been saved because your profit percentage(" & percentage & ") is below the approved percentage(" & minmumCCprofitpercentage() & "). "
        '    BindEstimateGrid()
        '    FillEstimate()
        '    Exit Sub
        'Else
        '    divwarning.Visible = False
        'End If

        InsertPrePnlVSEstimateHistory(CInt(hdnJobCardID.Value), Convert.ToDecimal(txtEstimate.Text), Convert.ToDecimal(txtPrePnL.Text), Convert.ToDecimal(txtActual.Text))

        sqlFEstring &= "Update APEX_Estimate set EstimatedSubtotal=" & Clean(hdnSubTotal.Value)
        sqlFEstring &= " ,EstimatedManagentFees=" & Clean(hdnMangnmtFees.Value)
        sqlFEstring &= "           ,Estimatedtotal=" & Clean(total.ToString())
        sqlFEstring &= "           ,EstimatedServiceTax=" & Clean((total.ToString() * ddlservicetax.SelectedValue) / 100)
        sqlFEstring &= "           ,EstimatedGrandTotal=" & Clean(total.ToString() + ((total.ToString() * ddlservicetax.SelectedValue) / 100))

        'btnAdd.Visible = False

        sqlFEstring &= "       where RefBriefID=" & hdnBrief.Value

        If ExecuteNonQuery(sqlFEstring) > 0 Then
            btnCancelHome.Visible = True
        End If

        GetEstimateID()
        lblTotal.Text = Math.Round(total, 2)
        lblGrandTotal.Text = Math.Round(grandtotal, 2)
    End Sub

    'Public Function InsertFinalEstimate() As Boolean
    '    Try
    '        Dim capex As New clsApex
    '        If hdnNodificationID.Value <> "" Then
    '            capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
    '        End If


    '        Dim sqlFEstring As String = ""
    '        Dim total As Double = 0
    '        Dim servicetaxs As Double = 0
    '        'total = Convert.ToDouble(txtMangnmtFees.Text) + Convert.ToDouble(lblSubTotal.Text)
    '        total = hdnTotal.Value
    '        Dim grandtotal As Double = 0

    '        'servicetaxs = (total * txtServiceTax.Text) / 100
    '        'grandtotal = total + servicetaxs
    '        'lblServiceTax.Text = servicetaxs

    '        Dim percentage As Double = vbNull
    '        If hdnSubTotal.Value <> "" And TotalQoute() <> 0 Then
    '            Dim totalquote As Double = TotalQoute()
    '            Dim preeventprofit As Double = (Convert.ToDouble(total) - Convert.ToDouble(totalquote))
    '            percentage = Math.Round((((preeventprofit) / Convert.ToDouble(total)) * 100), 2)
    '        End If

    '        sqlFEstring &= "Update APEX_Estimate set EstimatedSubTotal=" & Clean(hdnSubTotal.Value)
    '        sqlFEstring &= " ,EstimatedManagentFees=" & Clean(hdnMangnmtFees.Value)
    '        sqlFEstring &= "           ,EstimatedTotal=" & Clean(total.ToString())
    '        sqlFEstring &= "           ,EstimatedServiceTax=" & Clean(hdnServiceTax.Value)
    '        sqlFEstring &= "           ,EstimatedGrandTotal=" & Clean(hdnGrand.Value)
    '        'If percentage > 30 Then
    '        If Convert.ToDouble(percentage) >= Convert.ToDouble(lblprimaryactivityprofitpr.Text) Then
    '            updateprofitperc(Convert.ToDouble(percentage), Convert.ToDouble(lblprimaryactivityprofitpr.Text))
    '            sqlFEstring &= ",IsOperationHeadApproved= 'Y'"
    '            sqlFEstring &= ",OperationHeadApprovedOn = getdate()"
    '            sqlFEstring &= ",IsBranchHeadApproved = 'Y'"
    '            sqlFEstring &= ",BranchHeadApprovedOn = getdate()"
    '            btnFinal.Visible = True
    '            btnAdd.Visible = False
    '        Else
    '            SendForApprovalToBranchHead(clsApex.NotificationType.PLBA, hdnBrief.Value, 6)
    '            'SendForApprovalBranchHead(clsApex.NotificationType.PLBA, hdnBrief.Value, 4)
    '            sqlFEstring &= ",IsOperationHeadApproved = 'N'"
    '            sqlFEstring &= ",OperationHeadApprovedOn = NULL"
    '            sqlFEstring &= ",IsBranchHeadApproved = 'N'"
    '            sqlFEstring &= ",BranchHeadApprovedOn = NULL"
    '            sqlFEstring &= " ,Reason='" & Clean(txtreason.Text) & "'"

    '            lblMsg.Text = "Your Estimate details has been sent for approval to branch head"
    '            MessageDiv.Visible = True
    '            divContent.Visible = False
    '            btnAdd.Visible = False
    '            btnFinal.Visible = False
    '        End If
    '        sqlFEstring &= "           where RefBriefID=" & hdnBrief.Value

    '        If ExecuteNonQuery(sqlFEstring) > 0 Then
    '            If InsertPrePnlVSEstimateHistory(CInt(hdnJobCardID.Value), Convert.ToDecimal(total), Convert.ToDecimal(txtPrePnL.Text), Convert.ToDecimal(percentage)) Then
    '                Return True

    '            End If
    '            Return True

    '        End If
    '        GetEstimateID()
    '        BindEstimateGrid()

    '        lblTotal.Text = Math.Round(total, 2)
    '        lblGrandTotal.Text = Math.Round(grandtotal, 2)
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Private Sub updateprofitperc(Currentprofit As Double, minimumccprofit As Double)
        Try
            'Dim sql As String = " update Apex_jobcard  set MinimumCCProfit='" & minimumccprofit & "',Currentprofit='" & Currentprofit & "' where jobcardID='" & hdnJobCardID.Value & "'"
            'ExecuteNonQuery(sql)

            Dim sql As String = ""
            sql &= "IF EXISTS(SELECT MinimumCCProfit FROM  Apex_jobcard where jobcardID='" & hdnJobCardID.Value & "' and  " & minimumccprofit & " < MinimumCCProfit)"
            sql &= " Begin"
            sql &= " update Apex_jobcard  set MinimumCCProfit='" & minimumccprofit & "',Currentprofit='" & Currentprofit & "' where jobcardID='" & hdnJobCardID.Value & "'"
            sql &= " End"
            ExecuteNonQuery(sql)

        Catch ex As Exception

        End Try
    End Sub

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

    Private Sub InsertEstimateCost()
        Dim sqlCostString As String = ""

        'sqlCostString &= "Insert Into APEX_EstimateCost Select RefEstimateID,RefBriefID,Particulars,Quantity,Rate,Days,Estimate,Remarks,Category "
        'sqlCostString &= "from APEX_TempEstimate where RefBriefID=" & hdnBrief.Value

        'ExecuteNonQuery(sqlCostString)
    End Sub

    Private Sub DeleteFromTemp()
        Dim sqlDeleteTempString As String = ""

        sqlDeleteTempString = "Delete From APEX_TempEstimate where RefBriefID=" & hdnBrief.Value
        ExecuteNonQuery(sqlDeleteTempString)
    End Sub

    Private Sub BindEstimateGrid()
        Dim sqlGridString As String = ""
        Dim ds As New DataSet
        sqlGridString = "Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,* from  "
        sqlGridString &= " APEX_TempEstimate where RefBriefID=" & hdnBrief.Value & " "
        'sqlGridString &= "  union"
        'sqlGridString &= " Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,*"
        'sqlGridString &= " from APEX_EstimateCost where RefBriefID=" & hdnBrief.Value & " "
        sqlGridString &= " order by refEstimateID Asc"
        ds = ExecuteDataSet(sqlGridString)

        gdvEstimate.DataSource = ds
        gdvEstimate.DataBind()

        gvDisplay.DataSource = ds
        gvDisplay.DataBind()

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

    Protected Sub gdvEstimate_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdvEstimate.RowCreated

    End Sub


    Protected Sub gdvEstimate_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdvEstimate.RowDataBound

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim category As String()
            Dim ddlCategory As DropDownList = e.Row.FindControl("ddlCategory")

            Dim sql As String = "select RefActivityTypeID from APEX_Brief where BriefID=" & hdnBrief.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                category = (ds.Tables(0).Rows(0)(0).ToString()).Split("|")
                ddlCategory.DataSource = category
                ddlCategory.DataBind()
                ddlCategory.Items.Insert(0, New ListItem("Select", "0"))
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.DataItem IsNot Nothing Then
                If (e.Row.RowState And DataControlRowState.Edit) > 0 Then
                    Dim ddlFormat As DropDownList = e.Row.FindControl("gv_ddlCategory")
                    Dim hdnEstimateID As HiddenField = e.Row.FindControl("hdnEstimateID")

                    ' HdnIncharge.Value = ddlFormat.DataValueField
                    Dim sqlBindstring As String = "Select Category from APEX_tempEstimate where EstimateID=" & hdnEstimateID.Value
                    Dim sql As String = "select RefActivityTypeID from APEX_Brief where BriefID=" & hdnBrief.Value
                    ddlFormat.Items.Clear()
                    Dim ds1 As New DataSet
                    ds1 = ExecuteDataSet(sql)
                    Dim ds As New DataSet
                    ddlFormat.DataValueField = ""
                    ds = ExecuteDataSet(sqlBindstring)
                    ddlFormat.DataSource = (ds1.Tables(0).Rows(0)(0).ToString()).Split("|")
                    ddlFormat.DataBind()
                    'ddlFormat.Items.Insert(0, New ListItem("Select", "0"))
                    ddlFormat.Items.Insert(0, New ListItem("Select", "0"))
                    ddlFormat.SelectedIndex = ddlFormat.Items.IndexOf(ddlFormat.Items.FindByText(ds.Tables(0).Rows(0)(0).ToString()))
                Else
                    If e.Row.RowIndex = 0 Then
                        hdnSubTotal.Value = 0
                        hdnMangnmtFees.Value = 0
                        hdnTotal.Value = 0
                    End If
                    Dim txtEstimate1 As TextBox = e.Row.FindControl("txtEstimate1")
                    Dim txt_agencyfees As TextBox = e.Row.FindControl("txt_agencyfees")
                    If hdnSubTotal.Value.Replace("NaN", "") = "" Then
                        hdnSubTotal.Value = 0
                    End If
                    If hdnMangnmtFees.Value.Replace("NaN", "") = "" Then
                        hdnMangnmtFees.Value = 0
                    End If
                    If hdnTotal.Value.Replace("NaN", "") = "" Then
                        hdnTotal.Value = 0
                    End If
                    If txtEstimate1.Text <> "" Then
                        hdnSubTotal.Value += Convert.ToDecimal(txtEstimate1.Text)
                        hdnMangnmtFees.Value += ((Convert.ToDecimal(txtEstimate1.Text) * Convert.ToDecimal(txt_agencyfees.Text)) / 100)
                        hdnTotal.Value += (Convert.ToDecimal(hdnSubTotal.Value) + Convert.ToDecimal(hdnMangnmtFees.Value))
                    End If



                    'hdnServiceTax.Value = ""
                End If

            End If
        End If
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        InsertFinalEstimate()
        FillEstimate()
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



    Private Sub UpdateEstimate()
        Dim sql As String = "update APEX_JobCard set IsEstimated='Y' where RefBriefID = " & hdnBrief.Value
        ExecuteNonQuery(sql)
    End Sub

    Private Sub CLoseAllControls()


        gdvEstimate.Enabled = False
        txtMangnmtFees.Visible = False
        btnAdd.Visible = False
        '  btnFinal.Visible = False

        ' txtServiceTax.Visible = False

        lblMangnmtFees.Visible = True
        'btnExcel.Visible = False
        txtMFeePer.Visible = False

        '  lblServiceTaxPer.Visible = True
        lblMFeePer.Visible = True
    End Sub

    Private Sub BindFinalEstimateGrid()
        Dim sqlGridString As String = ""
        Dim ds As New DataSet

        sqlGridString = "Select ROW_NUMBER() OVER(ORDER BY EstimateCostID DESC) AS Row,RefEstimateID as EstimateID,EstimateParticulars as Particulars,EstimateQty as Quantity,EstimateRate as Rate,EstimateDays as Days,EstimateAmount as Estimate,EstimateRemarks as Remarks  from APEX_EstimateCost where RefBriefID=" & hdnBrief.Value
        ds = ExecuteDataSet(sqlGridString)

        gdvEstimate.DataSource = ds
        gdvEstimate.DataBind()

        gvDisplay.DataSource = ds
        gvDisplay.DataBind()
        ' FillOtherDetails()
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


    Protected Sub gdvEstimate_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gdvEstimate.RowEditing



        gdvEstimate.EditIndex = e.NewEditIndex
        'Rebind the GridView to show the data in edit mode
        BindEstimateGrid()

        'ClientScript.RegisterStartupScript(Me.GetType(), "UpdateTime", "javasript:Calc_Grid2(this);", True)

    End Sub

    Protected Sub gdvEstimate_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gdvEstimate.RowUpdating
        'Accessing Edited values from the GridView

        Dim hdnEstimateid As New HiddenField
        hdnEstimateid = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("hdnEstimateID"), HiddenField)
        Dim particular As New TextBox
        Dim Quantity As New TextBox
        Dim rate As New TextBox
        Dim days As New TextBox
        Dim Estimate As New Label
        Dim Category As New DropDownList
        Dim agencyfee As New TextBox

        particular = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("gv_txtParticulars"), TextBox)
        Quantity = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("gv_txtquantity"), TextBox)
        rate = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("gv_txtRate"), TextBox)
        days = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("gv_txtDays"), TextBox)
        Estimate = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("gv_txtEstimate"), Label)
        Category = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("gv_ddlCategory"), DropDownList)
        agencyfee = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("gv__agencyfee"), TextBox)

        Dim d2, d3, d4, d5, d6, d7 As String
        Dim d1 As String = hdnEstimateid.Value

        If particular.Text <> "" Then
            d2 = Clean(particular.Text)
        End If
        If Quantity.Text <> "" Then
            d3 = Quantity.Text
        End If
        If rate.Text <> "" Then
            d4 = rate.Text
        End If
        If days.Text <> "" Then
            d5 = days.Text
        End If
        If Estimate.Text <> "" Then
            If d3 <> "" And d4 <> "" And d5 <> "" Then
                d6 = Convert.ToDouble(d3) * Convert.ToDouble(d4) * Convert.ToDouble(d5)
            End If
        End If

        If Category.SelectedIndex > 0 Then
            d7 = Category.SelectedItem.Text
        End If
        'Call the function to update the GridView

        total = (Convert.ToDecimal(hdnSubTotal.Value) + Convert.ToDecimal(hdnMangnmtFees.Value))
        Dim percentage As Double = vbNull
        If hdnSubTotal.Value <> "" And TotalQoute() <> vbNull Then
            Dim totalquote As Double = TotalQoute()
            Dim preeventprofit As Double = (Convert.ToDouble(total) - Convert.ToDouble(totalquote))
            percentage = Math.Round((((preeventprofit) / Convert.ToDouble(total)) * 100), 2)
        End If


        If minmumCCprofitpercentage() <= hdnactual.Value Then

            UpdateRecord(d1, d2, d3, d4, d5, d6, d7, agencyfee.Text)
            btnAdd_Click(sender, e)
            divwarning.Visible = False
        Else
            UpdateRecordBelowMinimumPercentage(d1, d2, d3, d4, d5, d6, d7, agencyfee.Text)

            divwarning.Visible = True
            Messagebranchhead.Visible = True
            lblwarning.Text = "Your data has been saved in backend because your profit percentage(" & hdnactual.Value & ") is below the approved percentage(" & minmumCCprofitpercentage() & "). If you want to change in Actual values Please send to branch head for approval."
            'gdvEstimate.EditIndex = -1
            'BindEstimateGrid()
            'Exit Sub
        End If


        gdvEstimate.EditIndex = -1
        BindEstimateGrid()

        'Rebind Gridview to reflect changes made
        'btnAdd_Click(sender, e)
        'InsertFinalEstimate()
        FillEstimate()
        'Response.Redirect(Request.RawUrl)
    End Sub
    Protected Sub gdvEstimate_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gdvEstimate.RowCancelingEdit
        ' switch back to edit default mode
        gdvEstimate.EditIndex = -1
        'Rebind the GridView to show the data in edit mode
        BindEstimateGrid()


    End Sub

    Private Sub UpdateRecord(ByVal prepnlcostid As String, ByVal Prticulars As String, ByVal Quantity As String, ByVal Rate As String, ByVal Day As String, ByVal Estimate As String, ByVal Category As String, ByVal agencyfee As String)



        Dim sql As String = ""
        sql &= "UPDATE [APEX_TempEstimate]"
        sql &= "  SET [RefEstimateID] = " & hdnEstimate.Value & ""
        sql &= "  ,[RefBriefID] = " & hdnBrief.Value & ""
        sql &= ",[Particulars] = '" & Clean(Prticulars) & "'"

        sql &= "  ,[Quantity] = '" & Clean(Quantity) & "'"
        sql &= " ,[Rate] = '" & Clean(Rate) & "'"
        sql &= "  ,[Days] = '" & Clean(Day) & "'"
        sql &= "  ,[Estimate] = '" & Clean(Estimate) & "'"
        sql &= "  ,[Category] = '" & Category & "'"
        sql &= "  ,[AgencyFee] = '" & agencyfee & "'"

        sql &= " WHERE EstimateID=" & prepnlcostid

        'ExecuteNonQuery(sql)

        If ExecuteNonQuery(sql) > 0 Then
            BindEstimateGrid()
            'FillOtherDetails()
            btnAdd.Visible = True

        Else

            sql = "UPDATE [APEX_EstimateCost]"
            sql &= "  SET [RefEstimateID] = " & hdnEstimate.Value & ""
            sql &= "  ,[RefBriefID] = " & hdnBrief.Value & ""
            sql &= ",[EstimateParticulars] = '" & Clean(Prticulars) & "'"
            sql &= "  ,[EstimateQty] = '" & Clean(Quantity) & "'"
            sql &= " ,[EstimateRate] = '" & Clean(Rate) & "'"
            sql &= "  ,[EstimateDays] = '" & Clean(Day) & "'"
            sql &= "  ,[EstimateAmount] = '" & Clean(Estimate) & "'"
            sql &= "  ,[Category] = '" & Category & "'"
            sql &= "  ,[AgencyFee] = '" & agencyfee & "'"

            sql &= " WHERE EstimateCostID=" & prepnlcostid


            ExecuteNonQuery(sql)

            If ExecuteNonQuery(sql) > 0 Then
                gdvEstimate.EditIndex = -1
                BindEstimateGrid()
                'FillOtherDetails()
                btnAdd.Visible = True

            Else

            End If

        End If

    End Sub

    Private Sub UpdateRecordBelowMinimumPercentage(ByVal prepnlcostid As String, ByVal Prticulars As String, ByVal Quantity As String, ByVal Rate As String, ByVal Day As String, ByVal Estimate As String, ByVal Category As String, ByVal agencyfee As String)

        hdntempestimateTotal.Value = hdntempestimateTotal.Value + Estimate

        Dim sql As String = ""
        sql &= "UPDATE [APEX_TempEstimate]"
        sql &= "  SET [RefEstimateID] = " & hdnEstimate.Value & ""
        sql &= "  ,[RefBriefID] = " & hdnBrief.Value & ""
        sql &= ",[Particulars] = '" & Clean(Prticulars) & "'"

        sql &= "  ,[TempQty] = '" & Clean(Quantity) & "'"
        sql &= " ,[TempRate] = '" & Clean(Rate) & "'"
        sql &= "  ,[Tempdays] = '" & Clean(Day) & "'"
        ' sql &= "  ,[Estimate] = '" & Clean(Estimate) & "'"
        sql &= "  ,[TempEstimate] = '" & Estimate & "'"
        sql &= "  ,[TempAgencyFee] = '" & agencyfee & "'"

        sql &= " WHERE EstimateID=" & prepnlcostid

        'ExecuteNonQuery(sql)

        If ExecuteNonQuery(sql) > 0 Then
            BindEstimateGrid()
            'FillOtherDetails()
            btnAdd.Visible = True

        Else

            sql = "UPDATE [APEX_EstimateCost]"
            sql &= "  SET [RefEstimateID] = " & hdnEstimate.Value & ""
            sql &= "  ,[RefBriefID] = " & hdnBrief.Value & ""
            sql &= ",[EstimateParticulars] = '" & Clean(Prticulars) & "'"
            sql &= "  ,[EstimateQty] = '" & Clean(Quantity) & "'"
            sql &= " ,[EstimateRate] = '" & Clean(Rate) & "'"
            sql &= "  ,[EstimateDays] = '" & Clean(Day) & "'"
            sql &= "  ,[EstimateAmount] = '" & Clean(Estimate) & "'"
            sql &= "  ,[Category] = '" & Category & "'"
            sql &= "  ,[AgencyFee] = '" & agencyfee & "'"

            sql &= " WHERE EstimateCostID=" & prepnlcostid


            ExecuteNonQuery(sql)

            If ExecuteNonQuery(sql) > 0 Then
                BindEstimateGrid()
                'FillOtherDetails()
                btnAdd.Visible = True

            Else

            End If
        End If

    End Sub

    Protected Sub gdvEstimate_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvEstimate.RowCommand
        If e.CommandName = "add" Then
            Dim totalquote As Double = 0

            Dim frow As GridViewRow = gdvEstimate.FooterRow
            Dim txtDays As New TextBox
            Dim txtEstimate As New TextBox
            Dim txtquantity As New TextBox
            Dim txtRate As New TextBox
            Dim txtParticulars As New TextBox
            Dim txtRemarks As New TextBox
            Dim ddlCategory As New DropDownList
            Dim agencyfee As New TextBox

            txtDays = CType(frow.FindControl("txtDays"), TextBox)
            txtEstimate = CType(frow.FindControl("txtEstimate"), TextBox)
            txtquantity = CType(frow.FindControl("txtquantity"), TextBox)
            txtRate = CType(frow.FindControl("txtRate"), TextBox)
            txtParticulars = CType(frow.FindControl("txtParticulars"), TextBox)
            txtRemarks = CType(frow.FindControl("txtRemarks"), TextBox)

            txtEstimate.Text = (Convert.ToDouble(txtDays.Text) * Convert.ToDouble(txtquantity.Text) * Convert.ToDouble(txtRate.Text)).ToString()
            ddlCategory = CType(frow.FindControl("ddlCategory"), DropDownList)
            agencyfee = DirectCast(frow.FindControl("txt_agencyfee"), TextBox)

            Dim GrandTotal As String = lblTotal.Text

            Dim sqlEstimateString As String = ""

            sqlEstimateString &= "INSERT INTO [APEX_TempEstimate]"
            sqlEstimateString &= "           ([RefEstimateID]"
            sqlEstimateString &= "           ,[RefBriefID]"
            sqlEstimateString &= "           ,[Particulars]"
            sqlEstimateString &= "           ,[Quantity]"
            sqlEstimateString &= "           ,[Rate]"
            sqlEstimateString &= "           ,[Days]"
            sqlEstimateString &= "           ,[Estimate]"
            sqlEstimateString &= "           ,[Remarks]"
            sqlEstimateString &= "           ,[Category]"
            sqlEstimateString &= "           ,[agencyfee]"
            sqlEstimateString &= "           )"
            sqlEstimateString &= "     VALUES"
            sqlEstimateString &= "           ("
            sqlEstimateString &= hdnEstimate.Value & ", "
            sqlEstimateString &= hdnBrief.Value & ", "
            sqlEstimateString &= "  '" & Clean(txtParticulars.Text) & "', "
            sqlEstimateString &= Clean(txtquantity.Text)
            sqlEstimateString &= "," & Clean(txtRate.Text)
            sqlEstimateString &= "," & Clean(txtDays.Text)
            sqlEstimateString &= "," & Clean(txtEstimate.Text)
            If txtRemarks.Text <> "" Then
                sqlEstimateString &= ",'" & Clean(txtRemarks.Text) & "'"
            Else
                sqlEstimateString &= ",NULL"
            End If
            sqlEstimateString &= ",'" & ddlCategory.SelectedItem.Text & "'"
            sqlEstimateString &= ",'" & agencyfee.Text & "'"
            sqlEstimateString &= "     )   "

            If ExecuteNonQuery(sqlEstimateString) > 0 Then
                txtParticulars.Text = ""
                txtquantity.Text = ""
                txtRate.Text = ""
                txtDays.Text = ""
                txtEstimate.Text = ""
                txtRemarks.Text = ""
                ddlCategory.SelectedIndex = 0
            End If
            InsertEstimateCost()

            'BindEstimateGrid()

            CalculateSubTotal()
            'FillOtherDetails()
            btnAdd.Visible = True
            'InsertFinalEstimate()
            BindEstimateGrid()
            InsertFinalEstimate()
            FillEstimate()
        End If
        If e.CommandName = "delete" Then

            'Dim sqlDeleteString As String = ""

            'Dim hdnEstimateID As New HiddenField
            'Dim row As GridViewRow = CType(CType(e.CommandSource, ImageButton).NamingContainer, GridViewRow)

            'hdnEstimateID = CType(row.FindControl("hdnEstimateID"), HiddenField)

            'sqlDeleteString = "Delete From APEX_TempEstimate Where EstimateID =" & hdnEstimateID.Value
            'ExecuteNonQuery(sqlDeleteString)
            'BindEstimateGrid()
            'FillOtherDetails()
            'btnAdd.Visible = True

        End If
        If e.CommandName = "Edit" Then
            Dim hdnPrePnLCostID As New HiddenField


        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Function prepnlcodefilled() As Boolean
        Dim flag As Boolean = False
        If hdnBrief.Value <> "" Then
            Dim sql As String = "Select  PreEventQuote from APEX_PrePnL  where refBriefId=" & hdnBrief.Value
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("PreEventQuote")) Then
                    flag = True
                End If
            End If
        End If
        Return flag

    End Function

    Private Sub BindBriefData(ByVal BriefID As Integer)
        Dim activityType() As String
        Dim sqlBriefData As String = ""
        Dim ds As New DataSet

        sqlBriefData = "Select convert(varchar(10),ActivityDate,105) as NActivityDate,*,(Select Allowprofit from APEX_ActivityType where ProjectTypeID=APEX_Brief.PrimaryActivityID) as Allowprofit from APEX_Brief Where IsActive='Y' and IsDeleted='N' and BriefID=" & BriefID & ""
        ds = ExecuteDataSet(sqlBriefData)

        If ds.Tables(0).Rows.Count > 0 Then

            lblprimaryactivityprofitpr.Text = ds.Tables(0).Rows(0)("Allowprofit").ToString()
            'lblminimumprofit.Text = lblprimaryactivityprofitpr.Text

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
    End Sub
    Private Sub BindContactPersonDropdownEdit()
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
    End Sub

    Private Function primaryActivity(ByVal p1 As String) As String
        Dim activity As String = ""

        Dim sql As String = "Select ProjectType from dbo.APEX_ActivityType where projectTypeID=" & p1 & " and isActive='Y'"
        Dim ds As DataSet = Nothing

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("ProjectType")) Then
                activity = ds.Tables(0).Rows(0)("ProjectType").ToString()
            End If
        End If

        Return activity
    End Function

    Private Function returnClientname(ByVal p1 As String) As String
        Dim client As String = ""

        Dim sql As String = "Select Client from dbo.APEX_Clients where ClientID=" & p1 & " and isActive='Y'"

        Dim ds As DataSet = Nothing

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("Client")) Then
                client = ds.Tables(0).Rows(0)("Client").ToString()
            End If
        End If

        Return client
    End Function
    Private Sub FillPrePnlDetails()
        lblfulpl_Mail.Visible = False
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
            'lblplPreEventProfit.Text = ds.Tables(0).Rows(0)("PreProfitPercent").ToString()

            ' lblPreEPr.Text = ds.Tables(0).Rows(0)("PreProfitPercent").ToString()

            If ds.Tables(0).Rows(0)("PLApprovalMail").ToString() <> "" Then
                lblfulpl_Mail.NavigateUrl = ds.Tables(0).Rows(0)("PLApprovalMail").ToString()
                lblfulpl_Mail.Visible = True
            Else
                lblfulpl_Mail.Visible = False
            End If


            FillPrePnLCost()
            FillTotalQouteAfter()
            'If CheckJobcardCreated() = True Then
            '    FillTotalQoute()
            'Else
            '    FillTotalQouteAfter()
            'End If

        End If
    End Sub
    Private Sub FillTotalQouteAfter()

        Dim sql As String = "Select Sum(PreEventCost) as SumPreEventCost,Sum(PreEventServiceTax) as PreEventServiceTax,Sum(PreEventTotal) as PreEventTotal  from APEX_PrePnLCost where RefBriefID=" & hdnBrief.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            lblplTCost.Text = ds.Tables(0).Rows(0)("SumPreEventCost").ToString()
            'lblTotalServiceTax.Text = ds.Tables(0).Rows(0)("PreEventServiceTax").ToString()
            lblPreEventtotal.Text = ds.Tables(0).Rows(0)("PreEventTotal").ToString()
            'lblServiceTaxAmount.Text = ds.Tables(0).Rows(0)("PreEventServiceTax").ToString()


            If lblPreEventtotal.Text <> "" And lblplTCost.Text <> "" Then
                lblplTSTax.Text = Math.Round(Convert.ToDouble(lblPreEventtotal.Text) - Convert.ToDouble(lblplTCost.Text), 2)
            End If

            lblplPETotal.Text = lblPreEventtotal.Text

        End If
    End Sub
    Private Sub FillPrePnLCost()
        Dim sql As String = "Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row,* from APEX_PrePnLCost where RefBriefID=" & hdnBrief.Value & "   order by PrePnLCostID Asc"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gv_prepnl.DataSource = ds
            gv_prepnl.DataBind()
        End If
    End Sub

    Protected Sub btnCancelHome_Click(sender As Object, e As EventArgs) Handles btnCancelHome.Click
        If Len(Request.QueryString("ur")) > 0 Then
            Dim ur As String = Request.QueryString("ur").ToString()
            If ur = "hm1" Then
                Response.Redirect("Home.aspx?mode=KAM2")
            Else
                Response.Redirect("Home.aspx?mode=KAM1")
            End If
        Else
            Response.Redirect("Home.aspx")
        End If

    End Sub



    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        Response.Clear()
        'BindEstimateGrid()
        BindFinalEstimateGrid()
        Response.AddHeader("content-disposition", "attachment; filename=Estimate" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"
        Dim stringWriter As New StringWriter()
        Dim htmlWriter As New HtmlTextWriter(stringWriter)
        gdvEstimate.RenderControl(htmlWriter)
        Response.Write(stringWriter.ToString())
        Response.End()


    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered

    End Sub

    Protected Sub gdvEstimate_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gdvEstimate.RowDeleting
        Try
            Dim EstimateID As New HiddenField
            EstimateID = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("hdnEstimateID"), HiddenField)
            Dim txt_agencyfees As New TextBox
            txt_agencyfees = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("txt_agencyfees"), TextBox)
            Dim txtEstimate1 As TextBox
            txtEstimate1 = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("txtEstimate1"), TextBox)


            Dim sql As String = "Delete from Apex_tempestimate where EstimateID =" & EstimateID.Value

            total = (Convert.ToDecimal(hdnSubTotal.Value) + Convert.ToDecimal(hdnMangnmtFees.Value)) - (Convert.ToDecimal(txtEstimate1.Text) + (Convert.ToDecimal(txtEstimate1.Text) * Convert.ToDecimal(txt_agencyfees.Text) / 100))
            Dim percentage As Double = vbNull
            Dim preeventprofit As Double = vbNull
            Dim totalquote As Double = vbNull
            If hdnSubTotal.Value <> "" And TotalQoute() <> vbNull Then
                totalquote = TotalQoute()
                preeventprofit = (Convert.ToDouble(total) - Convert.ToDouble(totalquote))
                percentage = Math.Round((((preeventprofit) / Convert.ToDouble(total)) * 100), 2)
            End If
            If minmumCCprofitpercentage() > percentage Then

                divwarning.Visible = True

                lblwarning.Text = "Your data has not been Deleted because your profit percentage(" & percentage & ") is below the approved percentage(" & minmumCCprofitpercentage() & "). "
                BindEstimateGrid()
                FillEstimate()
                Exit Sub
            Else
                divwarning.Visible = False
                InsertPrePnlVSEstimateHistory(CInt(hdnJobCardID.Value), Convert.ToDecimal(total), Convert.ToDecimal(totalquote), Convert.ToDecimal(percentage))

                If ExecuteNonQuery(sql) Then
                    BindEstimateGrid()
                    InsertFinalEstimate()
                    FillEstimate()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Function ceckestimatevsactual() As Boolean
        Dim isestimate As Boolean = False
        Try
            Dim sqlGridString As String = ""
            Dim ds As New DataSet

            sqlGridString = "select isnull(IsEstimateVsActuals,'N')IsEstimateVsActuals from Apex_jobcard where RefBriefID=" & hdnBrief.Value
            ds = ExecuteDataSet(sqlGridString)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("IsEstimateVsActuals") = "Y" Then
                    isestimate = True
                Else
                    isestimate = False
                End If
            Else
                isestimate = False
            End If

        Catch ex As Exception

        End Try
        Return isestimate
    End Function

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
                        'txtEstimate.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal")
                        'txtEstimate.Text = lblTotal.Text
                        txtEstimate.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal")

                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0)("ppl")) Then
                        txtPrePnL.Text = ds.Tables(0).Rows(0)("ppl")
                    End If
                    txtActual.Text = Math.Round(((txtEstimate.Text - txtPrePnL.Text) / txtEstimate.Text) * 100, 2)

                    'If Not IsDBNull(ds.Tables(0).Rows(0)("profitper")) Then
                    '    per = ds.Tables(0).Rows(0)("profitper")
                    '    'txtActual.Text = Math.Round(per, 2)
                    '    txtActual.Text = Math.Round(((txtEstimate.Text - txtPrePnL.Text) / txtEstimate.Text) * 100, 2)
                    'Else
                    '    txtActual.Text = "0"
                    'End If
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub getservicetax(BriefID As String)
        Try
            Dim stax As String = ""
            Dim sql As String = "select cast(round((EstimatedServiceTax/EstimatedTotal)*100,2) as Numeric(18,2))servicetax from [dbo].[APEX_Estimate] where refbriefID='" & BriefID & "'"
            stax = ExecuteSingleResult(sql, _DataType.AlphaNumeric)
            'ddlservicetax.SelectedValue = stax
            ' ddlservicetax.Enabled = False
            ddlservicetax.SelectedValue = "14.50"
        Catch ex As Exception

        End Try
    End Sub

    Private Function minmumCCprofitpercentage() As Double
        Dim perc As Double
        Dim sql As String = "select minimumCCprofit from apex_jobcard where jobcardID=" & hdnJobCardID.Value
        perc = ExecuteSingleResult(sql, _DataType.AlphaNumeric)
        Return perc
    End Function

    'Add from Estimate
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
        sql &= "('Estimate for your approval.'"
        'sql &= ",'Approval for the Estimate'"

        sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Estimate for your approval with profit percentage " & percentage & "%, which is below the " & lblbriefPrimaryActivity.Text & " category approved %age(" & minmumCCprofitpercentage() & ")'"
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
        If type = 40 Then
            sql &= "('Estimate Rejected'"
            'sql &= ",'Your Estimate Rejected'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Your Estimate Rejected'"
        Else
            sql &= "('Estimate Approval'"
            'sql &= ",'Approval for the Estimate'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Your Estimate Approved'"
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
    Protected Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click
        Dim capex As New clsApex
        capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
        ' GetPrePnLID()
        Dim sql As String = ""

        sql &= "update [dbo].[Apex_EstimateVSprepnlHistory] set IsApproved='Y',approvedOn=getdate() where "
        sql &= "refjobcardID='" & hdnJobCardID.Value & "' and IsApproved=' ';"
        sql &= ""
        sql &= "update [dbo].[APEX_TempEstimate] set quantity=Tempqty,Rate=TempRate,Days=Tempdays,"
        sql &= "Estimate=tempEstimate,AgencyFee=tempAgencyFee"
        sql &= ",Tempqty=0,TempRate=0,Tempdays=0,tempEstimate=0,tempAgencyFee=0"
        sql &= " where refbriefID='" & hdnBrief.Value & "' and tempestimate<>0;"
        sql &= ""

        sql &= ""


        If ExecuteNonQuery(sql) > 0 Then
            'hdnJobCardID.Value = capex.GetJobCardID(hdnBrief.Value)
            BindEstimateGrid()
            InsertFinalEstimate()
            If hdnSubTotal.Value <> "" And TotalQoute() <> vbNull Then
                Dim totalquote As Double = TotalQoute()
                total = hdnTotal.Value
                Dim preeventprofit As Double = (Convert.ToDouble(total) - Convert.ToDouble(totalquote))
                Dim sqlda As String = "Select top 1 profitperc from [dbo].[Apex_EstimateVSprepnlHistory] where refjobcardID=" & hdnJobCardID.Value & " order By ID desc   "
                Dim profitperc As Decimal = ExecuteSingleResult(sqlda, _DataType.Numeric)

                updateprofitperc(profitperc, profitperc)

            End If

            capex.UpdateStageLevel("5", hdnJobCardID.Value)
            trAppButtons.Visible = False

            lblMsg.Text = "Estimate approved successfully."
            divError.Visible = False
            MessageDiv.Visible = True
            SendForApprovalBranchHead(clsApex.NotificationType.estimateapprovalfromBHtoKAM_AfterJC, hdnBrief.Value, 2, "Estimate")

        End If
    End Sub
    Public Sub SendForApprovalPM()
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
        sql &= "('Estimate for your Approval'"
        '  sql &= ",'Approval for the Estimate'"
        sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Create JobCard '"
        sql &= ",'H'"
        sql &= "," & NotificationType.PPNLApproved
        sql &= "," & hdnBrief.Value
        sql &= ",getdate()"
        sql &= ",NULL"
        sql &= ",'N'"
        sql &= ",'N')"

        If ExecuteNonQuery(sql) > 0 Then
            InsertRecieptentDetails()

        End If
    End Sub


    Private Sub InsertRecieptentDetails()
        Dim message As String = ""
        Dim notificationid As String = ""
        Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & NotificationType.PPNLApproved & " and AssociateID=" & hdnBrief.Value
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
        Dim bid As String = hdnBrief.Value
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

    Protected Sub btnRejected_Click(sender As Object, e As EventArgs) Handles btnRejected.Click
        Dim capex As New clsApex
        capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
        ' GetPrePnLID()
        Dim sql As String = ""
        sql &= "update [dbo].[APEX_TempEstimate] set "
        sql &= " Tempqty=0,TempRate=0,Tempdays=0,tempEstimate=0,tempAgencyFee=0"
        sql &= " where refbriefID='" & hdnBrief.Value & "' and tempestimate<>0;"
        sql &= ""
        sql &= "update [dbo].[Apex_EstimateVSprepnlHistory] set IsApproved='R',approvedOn=getdate() where "
        sql &= " refjobcardID='" & hdnJobCardID.Value & "' and IsApproved=' ';"
        sql &= ""


        If ExecuteNonQuery(sql) > 0 Then
            trAppButtons.Visible = False
            lblMsg.Text = "Estimate is rejected."
            divError.Visible = False
            MessageDiv.Visible = True
            SendForApprovalBranchHead(clsApex.NotificationType.estimateRejectfromBHtoKAM_AfterJC, hdnBrief.Value, 10, "Estimate")

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

    Protected Sub btncncl_Click(sender As Object, e As EventArgs) Handles btncncl.Click
        Messagebranchhead.Visible = False
        divContent.Visible = True
        Dim sql As String = ""
        sql &= "update [dbo].[APEX_TempEstimate] set "
        sql &= " Tempqty=0,TempRate=0,Tempdays=0,tempEstimate=0,tempAgencyFee=0"
        sql &= " where refbriefID='" & hdnBrief.Value & "' and tempestimate<>0;"


        ExecuteNonQuery(sql)
        Messagebranchhead.Visible = False
        MessageDiv.Visible = False
        divwarning.Visible = False
        lblMsg.Text = ""
    End Sub

    Private Sub btnsendrequesttobranchhead_Click(sender As Object, e As EventArgs) Handles btnsendrequesttobranchhead.Click
        Dim percentage As Double = vbNull
        Dim preeventprofit As Double = vbNull
        Dim totalquote As Double = vbNull
        Dim getestimatevalueforrequest As Double = vbNull

        getestimatevalueforrequest = ExecuteSingleResult("Select convert(Numeric(18,2),Sum((case when tempestimate=0 then estimate else tempestimate end +(case when tempestimate =0 then estimate else tempestimate end * case when TempAgencyfee =0 then Agencyfee else TempAgencyfee end)/100)))tempestimate from APEX_TempEstimate where refbriefID=" & hdnBrief.Value, _DataType.Numeric)

        totalquote = TotalQoute()
        preeventprofit = (Convert.ToDouble(getestimatevalueforrequest) - Convert.ToDouble(totalquote))
        percentage = Math.Round((((preeventprofit) / Convert.ToDouble(getestimatevalueforrequest)) * 100), 2)


        If InsertPrePnlVSEstimateHistory(hdnJobCardID.Value, getestimatevalueforrequest, TotalQoute(), percentage, Clean(txtreason.Text)) Then

            SendForApprovalToBranchHead(clsApex.NotificationType.estimateapprovalfromkamtoPM_AfterJC, hdnBrief.Value, 6, percentage)
            Messagebranchhead.Visible = False
            divContent.Visible = False
            MessageDiv.Visible = True
            lblMsg.Text = "Your Estimate details has been sent For approval To branch head"
        End If
    End Sub

    Private Sub getEstimatevsprepnlHistory()
        Dim sql As String = " Select RefjobcardID,EstimateTotal,PrepnlTotal,ProfitPerc,InsertedOn,ReasonFromKam,ReasonFromBH,IsApproved,ApprovedOn from Apex_EstimateVSprepnlHistory where refjobcardID=" & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        gridfinalesimate.DataSource = ds
        gridfinalesimate.DataBind()
    End Sub

    Private Sub btnFinal_Click(sender As Object, e As EventArgs) Handles btnFinal.Click

    End Sub
End Class
