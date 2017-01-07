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

Partial Class Estimate
    Inherits System.Web.UI.Page
    Dim total As Double = 0
    Private isEditMode As Boolean = False

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            btnCancelHome.Visible = False
            divError.Visible = False
            ' btnExcel.Visible = False
            MessageDiv.Visible = False
            trAppButtons.Visible = False
            trAppRemarks.Visible = False
            btnFinal.Visible = False
            btnCancelApp.Visible = False
            Messagebranchhead.Visible = False
            If Len(Request.QueryString("bid")) > 0 Then
                If Request.QueryString("bid") <> Nothing Then
                    If Request.QueryString("bid").ToString() <> "" Then
                        hdnBrief.Value = Request.QueryString("bid").ToString()
                       
                        getservicetax(hdnBrief.Value)
                        BindBriefData(hdnBrief.Value)
                        FillPrePnlDetails()
                        Dim capex As New clsApex
                        hdnJobCardID.Value = capex.GetJobCardID(hdnBrief.Value)
                        bindprepnldetail(hdnBrief.Value)
                        If Len(Request.QueryString("nid")) > 0 Then
                            If Request.QueryString("nid") <> Nothing Then
                                btnCancel.Visible = False
                                btnCancelApp.Visible = True
                                hdnNodificationID.Value = Request.QueryString("nid")
                                capex.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())
                                If Len(Request.QueryString("Appro")) > 0 Then
                                    trAppButtons.Visible = True
                                    BindFinalEstimateGrid()
                                    gdvEstimate.Visible = False
                                    gvDisplay.Visible = True
                                    btnAdd.Visible = False
                                    CLoseAllControls()
                                    FillOtherDetails()
                                End If
                                If Len(Request.QueryString("Approved")) > 0 Then
                                    hdnNodificationID.Value = Request.QueryString("nid")
                                    btnAdd.Visible = False
                                    btnFinal.Visible = True
                                    BindFinalEstimateGrid()
                                    gdvEstimate.Visible = False
                                    gvDisplay.Visible = True

                                    Dim sql_btn As String = "select role from APEX_UsersDetails where userdetailsid=" & getLoggedUserID()
                                    Dim ds_btn As New DataSet
                                    ds_btn = ExecuteDataSet(sql_btn)

                                    If ds_btn.Tables(0).Rows.Count > 0 Then
                                        Dim role As String = ds_btn.Tables(0).Rows(0)("role").ToString()

                                        If role = "K" Or role = "H" Or role = "A" Then
                                            btnFinal.Visible = True
                                        Else
                                            btnFinal.Visible = False
                                        End If
                                       
                                    Else
                                        btnFinal.Visible = False
                                    End If
                                    CLoseAllControls()
                                    FillOtherDetails()
                                End If
                                Dim RL As String
                                RL = capex.GetRoleNameByUserID(getLoggedUserID())
                                If RL = "K" Then
                                    If isbranchheadapproval(hdnBrief.Value) = "N" Then
                                        lblMsg.Text = "Your Estimate details has been sent for approval to branch head"
                                        MessageDiv.Visible = True
                                        divContent.Visible = False
                                        btnAdd.Visible = False
                                        btnFinal.Visible = False
                                    End If
                                End If
                            End If
                           
                        End If
                        If hdnJobCardID.Value <> "" Then
                            'If capex.CheckStageLevel("4", hdnJobCardID.Value) = True Then
                            'TWContent.InnerHtml = capex.GetList("4", hdnJobCardID.Value)
                            GetEstimateID()
                            Dim clsapx As New clsApex
                            FillOtherDetails()
                            IsInEditMode = True
                            BindEstimateGrid()

                            If clsapx.IsjobCardcreated(hdnBrief.Value) = False Then
                                ' btnFinal.Visible = False
                                gdvEstimate.Visible = True
                                gvDisplay.Visible = False

                            Else

                                ' btnFinal.Visible = True
                                BindFinalEstimateGrid()

                                gvDisplay.Visible = True
                                gdvEstimate.Visible = False

                                CLoseAllControls()
                            End If
                            If Len(Request.QueryString("ur")) > 0 Then
                                If Request.QueryString("ur") <> Nothing Then
                                    If Request.QueryString("ur").ToString() <> "" Then
                                        btnCancelHome.Visible = True
                                        btnCancel.Visible = False
                                    End If
                                End If
                            End If
                            'Else
                            'CallDivError()
                            'End If

                            If CheckNotification() = True Then
                                Dim capex1 As New clsApex
                                capex1.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())
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
            FillEstimate()
            If Len(Request.QueryString("call")) > 0 Then
                If Request.QueryString("call") <> Nothing Then
                    UpdateButton_Click(sender, e)
                End If
            End If
        End If
        'If Convert.ToDouble("140.00") > Convert.ToDouble(lblprimaryactivityprofitpr.Text) Then
        '    lblprimaryactivityprofitpr.Text = "10.00"
        'End If
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

    Public Function InsertFinalEstimate() As Boolean
        Try
            Dim capex As New clsApex
            If hdnNodificationID.Value <> "" Then
                capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            End If


            Dim sqlFEstring As String = ""
            Dim total As Double = 0
            Dim servicetaxs As Double = 0
            'total = Convert.ToDouble(txtMangnmtFees.Text) + Convert.ToDouble(lblSubTotal.Text)
            total = hdnTotal.Value
            Dim grandtotal As Double = 0

            'servicetaxs = (total * txtServiceTax.Text) / 100
            'grandtotal = total + servicetaxs
            'lblServiceTax.Text = servicetaxs

            Dim percentage As Double = vbNull
            If hdnSubTotal.Value <> "" And TotalQoute() <> 0 Then
                Dim totalquote As Double = TotalQoute()
                Dim preeventprofit As Double = (Convert.ToDouble(total) - Convert.ToDouble(totalquote))
                percentage = Math.Round((((preeventprofit) / Convert.ToDouble(total)) * 100), 2)
            End If

            sqlFEstring &= "Update APEX_Estimate set EstimatedSubTotal=" & Clean(hdnSubTotal.Value)
            sqlFEstring &= " ,EstimatedManagentFees=" & Clean(hdnMangnmtFees.Value)
            sqlFEstring &= "           ,EstimatedTotal=" & Clean(total.ToString())
            sqlFEstring &= "           ,EstimatedServiceTax=" & Clean(hdnServiceTax.Value)
            sqlFEstring &= "           ,EstimatedGrandTotal=" & Clean(hdnGrand.Value)
            'If percentage > 30 Then
            If Convert.ToDouble(percentage) >= Convert.ToDouble(lblprimaryactivityprofitpr.Text) Then
                updateprofitperc(Convert.ToDouble(percentage), Convert.ToDouble(lblprimaryactivityprofitpr.Text))
                sqlFEstring &= ",IsOperationHeadApproved= 'Y'"
                sqlFEstring &= ",OperationHeadApprovedOn = getdate()"
                sqlFEstring &= ",IsBranchHeadApproved = 'Y'"
                sqlFEstring &= ",BranchHeadApprovedOn = getdate()"
                btnFinal.Visible = True
                btnAdd.Visible = False
            Else
                SendForApprovalToBranchHead(clsApex.NotificationType.PLBA, hdnBrief.Value, 6)
                'SendForApprovalBranchHead(clsApex.NotificationType.PLBA, hdnBrief.Value, 4)
                sqlFEstring &= ",IsOperationHeadApproved = 'N'"
                sqlFEstring &= ",OperationHeadApprovedOn = NULL"
                sqlFEstring &= ",IsBranchHeadApproved = 'N'"
                sqlFEstring &= ",BranchHeadApprovedOn = NULL"
                lblMsg.Text = "Your Estimate details has been sent for approval to branch head"
                MessageDiv.Visible = True
                divContent.Visible = False
                btnAdd.Visible = False
                btnFinal.Visible = False
            End If
            sqlFEstring &= "           where RefBriefID=" & hdnBrief.Value

            If ExecuteNonQuery(sqlFEstring) > 0 Then
                Return True

            End If
            GetEstimateID()
            lblTotal.Text = Math.Round(total, 2)
            lblGrandTotal.Text = Math.Round(grandtotal, 2)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function TotalQoute() As Double
        Dim str As String = ""
        Dim result As Double = 0
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

        sqlCostString &= "Insert Into APEX_EstimateCost Select RefEstimateID,RefBriefID,Particulars,Quantity,Rate,Days,Estimate,Remarks,Category "
        sqlCostString &= "from APEX_TempEstimate where RefBriefID=" & hdnBrief.Value

        ExecuteNonQuery(sqlCostString)
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
        sqlGridString &= " APEX_TempEstimate where RefBriefID=" & hdnBrief.Value & " order by estimateID asc"
        'sqlGridString &= "  union"
        'sqlGridString &= " Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,*"
        'sqlGridString &= " from APEX_EstimateCost where RefBriefID=" & hdnBrief.Value & " "
        'sqlGridString &= " order by refEstimateID Asc"
        ds = ExecuteDataSet(sqlGridString)

        IsInEditMode = True
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
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("EstimatedSubTotal")) Then
                    txtEstimate.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal")
                End If
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

    Protected Sub btnFinal_Click(sender As Object, e As EventArgs) Handles btnFinal.Click
        If hdnJobCardID.Value <> "" Then
            ' If CalculateProfit() = True Then
            Dim tempno As String = gettempjc(hdnJobCardID.Value)

            Dim sql As String = "Update APEX_JobCard set IsEstimated='Y' ,tempjobcardNo='" & tempno & "',jobcardNo='" & tempno & "',TempJCInsertedon=getdate() where jobcardID=" & hdnJobCardID.Value
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

    Protected Sub gdvEstimate_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdvEstimate.RowCreated

    End Sub


    Protected Sub gdvEstimate_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdvEstimate.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim category As String()
            Dim ddlCategoryedit As DropDownList = e.Row.FindControl("gv_ddlCategoryedit")
            Dim lblcat As Label = e.Row.FindControl("lblcat")

            Dim sql As String = "select RefActivityTypeID from APEX_Brief where BriefID=" & hdnBrief.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                category = (ds.Tables(0).Rows(0)(0).ToString()).Split("|")
                Dim dt As New DataTable
                dt.Columns.Add(New DataColumn("key"))
                dt.Columns.Add(New DataColumn("value"))
                Dim dr As DataRow
                For c As Integer = 0 To category.Length - 1
                    dr = dt.NewRow()
                    dr("key") = category(c)
                    dr("value") = category(c)

                    dt.Rows.Add(dr)
                Next
                ddlCategoryedit.DataSource = dt
                ddlCategoryedit.DataTextField = "key"
                ddlCategoryedit.DataValueField = "value"
                ddlCategoryedit.DataBind()
                ddlCategoryedit.Items.Insert(0, New ListItem("Select", "0"))
                ddlCategoryedit.SelectedValue = lblcat.Text
            End If
        End If

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
                End If
            End If
        End If
    End Sub

    Protected Sub gdvEstimate_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gdvEstimate.RowDeleting
        Try
            'Dim EstimateID As New HiddenField
            'EstimateID = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("hdnEstimateID"), HiddenField)
            'Dim sql As String = "Delete from Apex_tempestimate where EstimateID =" & EstimateID.Value
            'If ExecuteNonQuery(sql) Then
            '    InsertFinalEstimate()
            '    FillEstimate()
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'If InsertFinalEstimate() Then
        '    btnFinal_Click(sender, e)
        'End If
        If hdnMangnmtFees.Value <= 0 Then
            ScriptManager.RegisterStartupScript(Me, [GetType](), "showalert", "alert('Please add agency fee in each item before saving estimate.');", True)
            Exit Sub
        End If
        If hdnServiceTax.Value > 0 Then


            Dim sql As String = "select count(1) from APEX_TempEstimate where refbriefID='" & hdnBrief.Value & "'"
            Dim cnt As Integer = 0
            cnt = ExecuteSingleResult(sql, _DataType.Numeric)
            If cnt > 0 Then
                total = hdnTotal.Value
                Dim percentage As Double = vbNull
                If hdnSubTotal.Value <> "" And TotalQoute() <> 0 Then
                    Dim totalquote As Double = TotalQoute()
                    Dim preeventprofit As Double = (Convert.ToDouble(total) - Convert.ToDouble(totalquote))
                    percentage = Math.Round((((preeventprofit) / Convert.ToDouble(total)) * 100), 2)
                End If

                If Convert.ToDouble(percentage) >= Convert.ToDouble(lblprimaryactivityprofitpr.Text) Then
                    Messagebranchhead.Visible = False
                    InsertFinalEstimate()
                Else
                    If btnsendrequesttobranchhead.Visible = True Then
                        Messagebranchhead.Visible = False
                        InsertFinalEstimate()
                    Else
                        Messagebranchhead.Visible = True
                        divContent.Visible = False
                    End If
                   
                End If



            Else
                ScriptManager.RegisterStartupScript(Me, [GetType](), "showalert", "alert('Please add estimate items before save estimate.');", True)
            End If

        End If

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'Dim capex As New clsApex
        'Dim leadid As String = capex.GetLeadIDByBriefID(hdnBrief.Value)
        'If leadid <> "" Then
        '    Response.Redirect("Leads.aspx?mode=cl")
        'Else
        '    Response.Redirect("Leads.aspx?mode=db")
        'End If
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
        '  btnExcel.Visible = False
        txtMFeePer.Visible = False

        'lblServiceTaxPer.Visible = True
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
        FillOtherDetails()
    End Sub

    Private Sub FillOtherDetails()
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
                lblSubTotal.Text = ds.Tables(0).Rows(0)("SubTotal").ToString().Trim()
            Else
                lblSubTotal.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString()
            End If
            lblTotal.Text = Math.Round((Convert.ToDouble(lblSubTotal.Text) * mfee))

            If ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString() <> "" And ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString() <> "" Then
                'txtMFeePer.Text = Math.Round(((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString()) / Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString())) * 100), 2).ToString()
                'mfee = Convert.ToDouble(txtMFeePer.Text)
                'lblMFeePer.Text = Math.Round(mfee, 2)
            End If

            If ds.Tables(0).Rows(0)("EstimatedGrandTotal").ToString() <> "" And ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString() <> "" Then
                'txtServiceTax.Text = ((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString()) / ds.Tables(0).Rows(0)("EstimatedTotal").ToString()) * 100).ToString()
                ' txtServiceTax.Text = ConfigurationManager.AppSettings("Servicetax").ToString()
                stax = Convert.ToDouble(ddlservicetax.SelectedValue)
                lblServiceTax.Text = Math.Round(stax, 2)
                'txtServiceTax.Text = lblServiceTax.Text
            End If

            'If lblSubTotal.Text <> "" Then
            '    txtMangnmtFees.Text = Math.Round((Convert.ToDouble(lblSubTotal.Text) * mfee) / 100, 2)
            'End If

            'If lblSubTotal.Text <> "" And txtMangnmtFees.Text <> "" Then
            '    lblTotal.Text = Math.Round(Convert.ToDouble(lblSubTotal.Text) + Convert.ToDouble(txtMangnmtFees.Text), 2)
            'End If

            'If lblTotal.Text <> "" Then
            '    lblServiceTax.Text = Math.Round((Convert.ToDouble(lblTotal.Text) * stax) / 100, 2)
            'End If

            'If lblTotal.Text <> "" And lblServiceTax.Text <> "" Then
            '    lblGrandTotal.Text = Math.Round(Convert.ToDouble(lblTotal.Text) + Convert.ToDouble(lblServiceTax.Text), 2)
            'End If

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
        'Response.AppendHeader("content-disposition", "attachment;filename=FileEName.xls")
        'Response.Charset = ""
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Response.ContentType = "application/vnd.ms-excel"
        'EnableViewState = False
        'Response.Write(divContent.InnerHtml)
        'Response.End()
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

    Protected Sub gdvEstimate_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gdvEstimate.RowEditing
        gdvEstimate.EditIndex = e.NewEditIndex
        'Rebind the GridView to show the data in edit mode
        BindEstimateGrid()

    End Sub

    Protected Sub gdvEstimate_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gdvEstimate.RowUpdating
        'Accessing Edited values from the GridView

        Dim hdnEstimateid As New HiddenField
        If Len(Request.QueryString("ID")) > 0 Then
            If Request.QueryString("ID") <> Nothing Then
                hdnEstimateid.Value = Request.QueryString("ID")
            Else
                hdnEstimateid = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("hdnEstimateID"), HiddenField)
            End If
        Else
            hdnEstimateid = DirectCast(gdvEstimate.Rows(e.RowIndex).FindControl("hdnEstimateID"), HiddenField)
        End If

        Dim particular As New TextBox
        Dim Quantity As New TextBox
        Dim rate As New TextBox
        Dim days As New TextBox
        Dim Estimate As New Label
        Dim Category As New DropDownList
        Dim agencyfee As New TextBox
        'Request.QueryString("rowID")

        particular = DirectCast(gdvEstimate.Rows(Request.QueryString("rowID")).FindControl("gv_txtParticulars"), TextBox)
        Quantity = DirectCast(gdvEstimate.Rows(Request.QueryString("rowID")).FindControl("gv_txtquantity"), TextBox)
        rate = DirectCast(gdvEstimate.Rows(Request.QueryString("rowID")).FindControl("gv_txtRate"), TextBox)
        days = DirectCast(gdvEstimate.Rows(Request.QueryString("rowID")).FindControl("gv_txtDays"), TextBox)
        Estimate = DirectCast(gdvEstimate.Rows(Request.QueryString("rowID")).FindControl("gv_txtEstimate"), Label)
        Category = DirectCast(gdvEstimate.Rows(Request.QueryString("rowID")).FindControl("gv_ddlCategory"), DropDownList)
        agencyfee = DirectCast(gdvEstimate.Rows(Request.QueryString("rowID")).FindControl("gv__agencyfee"), TextBox)


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

        'Rebind Gridview to reflect changes made
        BindEstimateGrid()
        FillEstimate()
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
            FillOtherDetails()
            btnAdd.Visible = True
            btnFinal.Visible = False
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

            sql &= " WHERE EstimateCostID=" & prepnlcostid


            ExecuteNonQuery(sql)

            If ExecuteNonQuery(sql) > 0 Then
                BindEstimateGrid()
                FillOtherDetails()
                btnAdd.Visible = True
                btnFinal.Visible = False
            Else

            End If
        End If

    End Sub

    Private Sub UpdateRecordall(ByVal prepnlcostid As String, ByVal Prticulars As String, ByVal Quantity As String, ByVal Rate As String, ByVal Day As String, ByVal Estimate As String, ByVal Category As String, ByVal agencyfee As String, ByVal Remarks As String)

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
        sql &= "  ,[Remarks] = '" & Remarks & "'"

        sql &= " WHERE EstimateID=" & prepnlcostid

        'ExecuteNonQuery(sql)

        If ExecuteNonQuery(sql) > 0 Then
            BindEstimateGrid()
            FillOtherDetails()
            btnAdd.Visible = True
            btnFinal.Visible = False
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
            sql &= "  ,[Remarks] = '" & Remarks & "'"

            sql &= " WHERE EstimateCostID=" & prepnlcostid


            ExecuteNonQuery(sql)

            If ExecuteNonQuery(sql) > 0 Then
                BindEstimateGrid()
                FillOtherDetails()
                btnAdd.Visible = True
                btnFinal.Visible = False
            Else

            End If
        End If

    End Sub

    Protected Sub gdvEstimate_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvEstimate.RowCommand
        Try


            If e.CommandName = "add" Then
                Dim totalquote As Double = 0

                Dim frow As GridViewRow = gdvEstimate.FooterRow

                Dim txtEstimate As New TextBox
                Dim txtDays As New TextBox
                Dim txtquantity As New TextBox
                Dim txtRate As New TextBox
                Dim txtParticulars As New TextBox
                Dim txtRemarks As New TextBox
                Dim ddlCategory As New DropDownList
                Dim txtagencyfee As New TextBox


                txtEstimate = CType(frow.FindControl("txtEstimate"), TextBox)
                txtDays = CType(frow.FindControl("txtDays"), TextBox)
                txtquantity = CType(frow.FindControl("txtquantity"), TextBox)
                txtRate = CType(frow.FindControl("txtRate"), TextBox)
                txtParticulars = CType(frow.FindControl("txtParticulars"), TextBox)
                txtRemarks = CType(frow.FindControl("txtRemarks"), TextBox)
                txtagencyfee = CType(frow.FindControl("txt_agencyfee"), TextBox)

                txtEstimate.Text = (Convert.ToDouble(txtDays.Text) * Convert.ToDouble(txtquantity.Text) * Convert.ToDouble(txtRate.Text)).ToString()
                ddlCategory = CType(frow.FindControl("ddlCategory"), DropDownList)

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
                sqlEstimateString &= "           ,[AgencyFee]"

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

                sqlEstimateString &= ",'" & txtagencyfee.Text & "'     )   "

                If ExecuteNonQuery(sqlEstimateString) > 0 Then
                    txtParticulars.Text = ""
                    txtquantity.Text = ""
                    txtRate.Text = ""
                    txtDays.Text = ""
                    txtEstimate.Text = ""
                    txtRemarks.Text = ""
                    ddlCategory.SelectedIndex = 0
                    ' btnAdd_Click(sender, e)
                End If


                BindEstimateGrid()

                CalculateSubTotal()
                FillOtherDetails()
                btnAdd.Visible = True
                btnFinal.Visible = False
            End If
            If e.CommandName = "delete" Then

                Dim sqlDeleteString As String = ""

                Dim hdnEstimateID As New HiddenField
                Dim row As GridViewRow = CType(CType(e.CommandSource, Button).NamingContainer, GridViewRow)

                hdnEstimateID = CType(row.FindControl("hdnEstimateID"), HiddenField)

                sqlDeleteString = "Delete From APEX_TempEstimate Where EstimateID =" & hdnEstimateID.Value
                ExecuteNonQuery(sqlDeleteString)
                BindEstimateGrid()
                FillOtherDetails()
                btnAdd.Visible = True
                btnFinal.Visible = False
            End If
            If e.CommandName = "Edit" Then
                Dim hdnPrePnLCostID As New HiddenField


            End If
            FillEstimate()
        Catch ex As Exception

        End Try
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
        'If type = 10 Then
        '    sql &= "('Estimate for your approval.'"
        '    'sql &= ",'Your Estimate Rejected'"
        '    sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Your Estimate Rejected'"
        'Else
        sql &= "('Estimate for your approval.'"
        'sql &= ",'Approval for the Estimate'"
        sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Estimate for your approval.'"
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
        If type = 10 Then
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
        Dim sql As String = "update APEX_Estimate set "
        sql &= " IsOperationHeadApproved = 'Y'"
        sql &= ",OperationHeadApprovedOn = getdate()"
        sql &= ",IsBranchHeadApproved = 'Y'"
        sql &= ",BranchHeadApprovedOn = getdate()"
        If txtRemarks.Text <> "" Then
            sql &= ",Remarks='" & Clean(txtRemarks.Text) & "'"
        End If

        sql &= " where EstimateID=" & hdnEstimate.Value

        If ExecuteNonQuery(sql) > 0 Then
            hdnJobCardID.Value = capex.GetJobCardID(hdnBrief.Value)

            If hdnSubTotal.Value <> "" And TotalQoute() <> vbNull Then
                Dim totalquote As Double = TotalQoute()
                total = hdnTotal.Value
                Dim preeventprofit As Double = (Convert.ToDouble(total) - Convert.ToDouble(totalquote))
                updateprofitperc(Math.Round((((preeventprofit) / Convert.ToDouble(total)) * 100), 2), Math.Round((((preeventprofit) / Convert.ToDouble(total)) * 100), 2))
            End If

            capex.UpdateStageLevel("5", hdnJobCardID.Value)
            trAppButtons.Visible = False
            lblMsg.Text = "Estimate approved successfully."
            divError.Visible = False
            MessageDiv.Visible = True
            SendForApprovalBranchHead(clsApex.NotificationType.PPNLApproved, hdnBrief.Value, 2, "Estimate")
            'If hdnClientID.Value <> "" Then
            '    SendForApprovalPM()
            'End If

            ' FillEstimate()
            ' UpdateIsPrepnl()
            ' Response.Redirect("JobCard.aspx?mode=pj")
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
        Dim sql As String = "update APEX_Estimate set "
        sql &= " IsOperationHeadApproved = 'R'"
        sql &= ",OperationHeadApprovedOn = getdate()"
        sql &= ",IsBranchHeadApproved = 'R'"
        sql &= ",BranchHeadApprovedOn = getdate()"
        If txtRemarks.Text <> "" Then
            sql &= ",Remarks='" & Clean(txtRemarks.Text) & "'"
        End If

        sql &= " where EstimateID=" & hdnEstimate.Value

        If ExecuteNonQuery(sql) > 0 Then
            trAppButtons.Visible = False
            lblMsg.Text = "Estimate is rejected."
            divError.Visible = False
            MessageDiv.Visible = True
            SendForApprovalBranchHead(clsApex.NotificationType.reject, hdnBrief.Value, 2, "Estimate")

        End If
    End Sub

    Protected Sub btnCancelApp_Click(sender As Object, e As EventArgs) Handles btnCancelApp.Click
        If Len(Request.QueryString("nid")) > 0 Then
            Dim capex As New clsApex
            If hdnNodificationID.Value <> "" Then
                capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            End If
            Response.Redirect("Home.aspx")

        End If


    End Sub
    Private Sub BindBriefData(ByVal BriefID As Integer)
        Dim activityType() As String
        Dim sqlBriefData As String = ""
        Dim ds As New DataSet

        sqlBriefData = "Select convert(varchar(10),ActivityDate,105) as NActivityDate,*,(Select Allowprofit from APEX_ActivityType where ProjectTypeID=APEX_Brief.PrimaryActivityID) as Allowprofit from APEX_Brief Where IsActive='Y' and IsDeleted='N' and BriefID=" & BriefID & ""
        ds = ExecuteDataSet(sqlBriefData)

        If ds.Tables(0).Rows.Count > 0 Then
            lblprimaryactivityprofitpr.Text = ds.Tables(0).Rows(0)("Allowprofit").ToString()
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
        sql = "Select convert(varchar(10),EventDate,105) as NEventDate,isnull(PLApprovalMail,'')PLApprovalMail1,* from APEX_PrePnL where RefBriefID=" & hdnBrief.Value & " and PrePnLID=" & prepnlid

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)

        If ds.Tables(0).Rows.Count > 0 Then

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim epass As String = ds.Tables(0).Rows(i)("PLApprovalMail1")
                ds.Tables(0).Rows(i).Item("PLApprovalMail1") = Clean(epass)
            Next
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

            If ds.Tables(0).Rows(0)("PLApprovalMail1").ToString() <> "" Then
                lblfulpl_Mail.NavigateUrl = ds.Tables(0).Rows(0)("PLApprovalMail1").ToString()
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

    Protected Sub btnCancelHome_Click(sender As Object, e As EventArgs) Handles btnCancelHome.Click
        If Request.QueryString("ur").ToString() = "hm" Then
            Response.Redirect("Home.aspx?mode=KAM1")
        ElseIf Request.QueryString("ur").ToString() = "hm1" Then
            Response.Redirect("Home.aspx?mode=KAM2")
        End If
    End Sub

    Public Function CheckNotification() As Boolean
        Dim result As Boolean = False
        Dim sql As String = "select * from APEX_Notifications "
        sql &= " inner join APEX_Recieptents on RefNotificationID=NotificationID"
        sql &= " where type in (15,9) and AssociateID=" & hdnBrief.Value & " and IsExecuted='N' and UserID=" & getLoggedUserID()
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnNodificationID.Value = ds.Tables(0).Rows(0)("NotificationID").ToString()
            result = True
        End If
        Return result
    End Function

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
                        'txtEstimate.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal")
                        'txtEstimate.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal")
                        txtEstimate.Text = hdnTotal.Value
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
            'ddlservicetax.Enabled = False
            ddlservicetax.SelectedValue = "14.50"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub updateprofitperc(Currentprofit As Double, minimumccprofit As Double)
        Try
            Dim sql As String = " update Apex_jobcard  set MinimumCCProfit='" & minimumccprofit & "',Currentprofit='" & Currentprofit & "' where jobcardID='" & hdnJobCardID.Value & "'"
            ExecuteNonQuery(sql)
        Catch ex As Exception

        End Try
    End Sub

    Protected Property IsInEditMode() As Boolean
        Get
            Return Me.isEditMode
        End Get
        Set(ByVal Value As Boolean)
            Me.isEditMode = Value
        End Set
    End Property

    Protected Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click

        Dim str As String()
        str = (Request.QueryString("str").ToString()).Split("|")

        Dim Category = str(0)
        Dim particular = str(1)
        Dim Quantity = str(2)
        Dim rate = str(3)
        Dim days = str(4)
        Dim Estimate = str(5)
        Dim agencyfee = str(6)
        Dim Remarks = str(7)
        'Request.QueryString("rowID")


        Dim d2, d3, d4, d5, d6, d7 As String
        Dim d1 As String = Request.QueryString("ID")

        If particular <> "" Then
            d2 = Clean(particular)
        End If
        If Quantity <> "" Then
            d3 = Quantity
        End If
        If rate <> "" Then
            d4 = rate
        End If
        If days <> "" Then
            d5 = days
        End If
        If Estimate <> "" Then
            If d3 <> "" And d4 <> "" And d5 <> "" Then
                d6 = Convert.ToDouble(d3) * Convert.ToDouble(d4) * Convert.ToDouble(d5)
            End If
        End If

        If Category <> "" Then
            d7 = Category
        End If
        'Call the function to update the GridView
        UpdateRecordall(d1, d2, d3, d4, d5, d6, d7, agencyfee, Remarks)

        gdvEstimate.EditIndex = -1

        'Rebind Gridview to reflect changes made
        BindEstimateGrid()
        FillEstimate()


    End Sub

    Private Function gettempjc(p1 As String) As String
        Try
            Dim sql As String = " select 'KIMS' + convert(varchar(50),convert(numeric(7,0),rand() * 8999999) + 1000000)"
            Dim num As String = ExecuteSingleResult(sql, _DataType.AlphaNumeric)
            sql = ""
            sql = " Select Count(jobcardID) from Apex_jobcard where TempJobcardNo='" & num & "'"
            If ExecuteSingleResult(sql, _DataType.Numeric) = 0 Then
                Return num
            Else
                gettempjc(p1)
            End If
        Catch ex As Exception

        End Try

    End Function

    Private Function isbranchheadapproval(p1 As String) As String
        Dim sql As String = ""
        sql = "select case when  isnull(EstimatedSubtotal,0)>0 then IsBranchHeadApproved  else 'Y' end IsBranchHeadApproved from [dbo].[APEX_Estimate] where refbriefID=" & p1
        Return ExecuteSingleResult(sql, _DataType.AlphaNumeric)
    End Function

    
    Protected Sub btncncl_Click(sender As Object, e As EventArgs) Handles btncncl.Click
        Messagebranchhead.Visible = False
        divContent.Visible = True
    End Sub

    'Protected Sub btnsendrequesttobranchhead_Click(sender As Object, e As EventArgs) Handles btnsendrequesttobranchhead.Click
    '    Messagebranchhead.Visible = False
    'End Sub
End Class
