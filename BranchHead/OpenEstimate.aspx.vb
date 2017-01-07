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

            divError.Visible = False
            divwarning.Visible = False
            'btnExcel.Visible = False
            MessageDiv.Visible = False
            trAppButtons.Visible = False
            trAppRemarks.Visible = False
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

                        hdnJobCardID.Value = capex.GetJobCardID(hdnBrief.Value)
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
                    Else
                        CallDivError()
                    End If
                Else
                    CallDivError()
                End If

            Else
                CallDivError()
            End If
            gdvEstimate.Enabled = False
        End If


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

        If minmumCCprofitpercentage() > percentage Then
            'divContent.Visible = False
            divwarning.Visible = True
            lblwarning.Text = "Your data has been saved but your profit percentage(" & percentage & ") is below the approved percentage(" & minmumCCprofitpercentage() & "). "
        End If

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
        UpdateRecord(d1, d2, d3, d4, d5, d6, d7, agencyfee.Text)

        gdvEstimate.EditIndex = -1
        BindEstimateGrid()

        'Rebind Gridview to reflect changes made
        'btnAdd_Click(sender, e)
        InsertFinalEstimate()
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

    Protected Sub btnFinal_Click(sender As Object, e As EventArgs) Handles btnFinal.Click
        If hdnJobCardID.Value <> "" Then
            ' If CalculateProfit() = True Then

            Dim sql As String = "Update APEX_JobCard set IsEstimated='Y' where jobcardID=" & hdnJobCardID.Value
            If ExecuteNonQuery(sql) > 0 Then
                Dim capex As New clsApex
                capex.UpdateStageLevel("5", hdnJobCardID.Value)
                If hdnNodificationID.Value <> "" Then
                    capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
                End If

                Response.Redirect("JobCardManager.aspx?jid=" & hdnJobCardID.Value & "&mode=&nid=" & "#jobcard")

            End If
            'Else
            '    MessageDiv.Visible = True
            '    lblMsg.Text = "Please view Pre P&L and Estimate"

            'End If
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
            Dim sql As String = "Delete from Apex_tempestimate where EstimateID =" & EstimateID.Value

            If ExecuteNonQuery(sql) Then
                BindEstimateGrid()
                InsertFinalEstimate()
                FillEstimate()
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

End Class
