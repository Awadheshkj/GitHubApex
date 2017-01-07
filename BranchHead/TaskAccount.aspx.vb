Imports clsDatabaseHelper
Imports clsMain
Imports clsApex
Imports System.Data
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System
Imports System.Data.SqlClient

Partial Class TaskAccount
    Inherits System.Web.UI.Page
    Public message As String = ""
    Dim total As Integer = 0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        btnNext.Visible = False
        If Not Page.IsPostBack Then
            divError.Visible = False
            divcopypasteerror.Visible = False
            MessageDiv.Visible = False
            'btnExcel.Visible = False
            'If Request.QueryString("jid") <> Nothing Then
            '    If Request.QueryString("jid").ToString() <> "" Then
            '        hdnJobCardID.Value = Request.QueryString("jid").ToString()
            '        Dim capex As New clsApex
            '        hdnBriefID.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)
            '        'BindDDLCategory()
            '        'FillJobCardDetails()
            '    End If

            'End If

            If Len(Request.QueryString("tid")) > 0 Then
                If Request.QueryString("tid") <> Nothing Then
                    If Request.QueryString("tid").ToString() <> "" Then
                        ' hdnJobCardID.Value = Request.QueryString("tid").ToString()
                        'If checktaskinsertedby(hdnTaskID.Value) = getLoggedUserID() Then
                        '    divContent.Visible = True

                        'Else
                        '    divContent.Visible = False
                        '    divError.Visible = True
                        '    lblError.Text = "You are not authorised to create task regarding this #JC"
                        'End If
                        'gvPrePnLCost_RowEditing(sender, e)

                      
                        hdnJobCardID.Value = Request.QueryString("tid").ToString()

                        'gvDisplay.Visible = False
                        Dim capex As New clsApex
                        hdnBriefID.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)

                        'hdnjobid.Value = capex.GetJobCardByTaskID(hdnTaskID.Value)
                        ' BindBalance()
                        Getprepnlwithcategory()
                        gvDisplay.Attributes.Add("style", "display:none")
                        gdvAccount.Visible = True
                        'bindcategory()
                        BindgdvAccount()
                        CalculateBalance()

                        If Len(Request.QueryString("nid")) > 0 Then
                            If Request.QueryString("nid") <> Nothing Then

                                hdnNodificationID.Value = Request.QueryString("nid")
                                capex.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())

                                'trAppRemarks.Visible = True
                                btnCancel.Visible = False

                            End If

                        End If
                        ' TWContent.InnerHtml = capex.GetList("6", jobcard)
                        If TaskTeamMember() = True Then
                            btnClaims.Visible = True

                            divClaims.Visible = True
                        Else
                            btnClaims.Visible = False
                            divClaims.Visible = False
                        End If
                        If capex.checkTaskCompleted(hdnJobCardID.Value) = "Y" Then
                            'CloseAllControls()
                            If TaskTeamMember() = True Then
                                btnClaims.Visible = True
                                divClaims.Visible = True
                            Else
                                btnClaims.Visible = False
                                divClaims.Visible = False
                            End If
                        End If
                    Else
                        CallDivError()
                    End If
                Else
                    CallDivError()
                End If
                'For i As Integer = 0 To i < gdvAccount.Rows.Count - 1
                '    gdvAccount.EditIndex = i + 1
                '    gdvAccount.Rows(i + 1).RowState = DataControlRowState.Edit
                'Next
                'gdvAccount.EditIndex = 0

            Else
                CallDivError()
            End If
        End If
    End Sub

    Private Sub BindgdvAccount()
        Dim sqlacString As String = ""
        Dim ds As New DataSet

        sqlacString &= "Select ROW_NUMBER() over(order by AccountID desc) as ID,Category,"
        sqlacString &= " AccountID, Particulars, Quantity, Amount, Total,ta.Description,Ta.TaskName,Ta.City"
        sqlacString &= " ,ta.StartDate,ta.EndDate"
        'sqlacString &= ",convert(varchar(20),ta.StartDate,106) + ' ' + LTRIM(RIGHT(CONVERT(VARCHAR(20), ta.StartDate, 100), 7))StartDate,convert(varchar(20),ta.EndDate ,106) + ' ' + LTRIM(RIGHT(CONVERT(VARCHAR(20), ta.EndDate , 100), 7))EndDate "
        sqlacString &= " ,  Name = case when (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) <>'' then"
        sqlacString &= "  (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) else 'N/A' end"
        sqlacString &= ",  isnull((SELECT Sum(CT.amount) FROM APEX_ClaimMaster CM "
        sqlacString &= "	Left join APEX_ClaimTransaction CT on CM.claimmasterID =CT.refClaimID"
        sqlacString &= "	where CM.refTaskID=TA.accountID and isapproved='Y'),0)Claimed"
        sqlacString &= " from APEX_TaskAccount as ta  "
        sqlacString &= " left outer join APEX_TaskTeam as tt on ta.AccountID=tt.RefTaskID"
        sqlacString &= " left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        sqlacString &= "  Where ta.IsActive='Y' and ta.IsDeleted='N' and "
        sqlacString &= " ta.RefJobcardID= " & hdnJobCardID.Value & "  order by AccountID Asc "
        ds = ExecuteDataSet(sqlacString)

        'If ds.Tables(0).Rows.Count > 0 Then
        '    hdnAccountID.Value = ds.Tables(0).Rows(0)("AccountID")
        'End If

        gdvAccount.DataSource = ds
        gdvAccount.DataBind()
        gvDisplay.DataSource = ds
        gvDisplay.DataBind()


        If gdvAccount.Rows.Count = 0 And Not gdvAccount.DataSource Is Nothing Then
            Dim dt As Object = Nothing
            If gdvAccount.DataSource.GetType Is GetType(Data.DataSet) Then
                dt = New System.Data.DataSet
                dt = CType(gdvAccount.DataSource, System.Data.DataSet).Tables(0).Clone()
            ElseIf gdvAccount.DataSource.GetType Is GetType(Data.DataTable) Then
                dt = New System.Data.DataTable
                dt = CType(gdvAccount.DataSource, System.Data.DataTable).Clone()
            ElseIf gdvAccount.DataSource.GetType Is GetType(Data.DataView) Then
                dt = New System.Data.DataView
                dt = CType(gdvAccount.DataSource, System.Data.DataView).Table.Clone
            End If
            dt.Rows.Add(dt.NewRow())
            gdvAccount.DataSource = dt
            gdvAccount.DataBind()
            gdvAccount.Rows(0).Visible = False
            gdvAccount.Rows(0).Controls.Clear()
            If ds.Tables(0).Rows.Count > 0 Then

            End If
            'Else
            '    Dim apx As New clsApex
            '    Dim pmid As String = apx.GetProjectManagerOfJobCard(hdnjobid.Value)
            '    If pmid <> "" Then
            '        If pmid = getLoggedUserID() Then
            '            btnNext.Visible = True

            '        End If
            '    End If
        End If

    End Sub

    'Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
    '    If gdvAccount.Rows.Count > 0 Then
    '        'Dim apx As New clsApex
    '        'If hdnNodificationID.Value <> "" Then
    '        '    apx.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
    '        'End If

    '        If hdnTaskID.Value <> "" Then
    '            Response.Redirect("Checklist.aspx?tid=" & hdnTaskID.Value)
    '        End If


    '    Else
    '        lblError.Text = "Enter Accounts Details"
    '        divError.Visible = True
    '    End If
    'End Sub

    'Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
    '    Dim capx As New clsApex

    '    Response.Redirect("Task.aspx?jid=" & hdnjobid.Value & "&mode=edit&tid=" & hdnTaskID.Value)
    'End Sub

    Private Sub CloseAllControls()
        gdvAccount.Visible = False
        gvDisplay.Visible = True
        btnExcel.Visible = True
    End Sub

    Protected Sub gdvAccount_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gdvAccount.PageIndexChanging
        gdvAccount.PageIndex = e.NewPageIndex
        BindgdvAccount()
    End Sub

    Protected Sub gvDisplay_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvDisplay.PageIndexChanging
        gvDisplay.PageIndex = e.NewPageIndex
        BindgdvAccount()
    End Sub

    Private Sub CallDivError()
        divContent.Visible = False
        divError.Visible = True
        Dim capex As New clsApex
        lblError.Text = capex.SetErrorMessage()
    End Sub

    Private Sub CalculateBalance()
        Dim capex As New clsApex
        Dim jobcardid As String
        Dim result As Boolean = False
        Dim tot As Double = 0
        Dim PreEventcot As Double = 0

        'jobcardid = capex.GetJobCardByTaskID(hdnTaskID.Value)
        jobcardid = hdnJobCardID.Value
        If dhncategoryname.Value <> "" Then
            Dim sql1 As String = "select SUM(total) as sumtotal from APEX_TaskAccount as ta "
            ' sql1 &= " inner join APEX_Task as tk on ta.RefTaskID = tk.TaskID "
            ' sql1 &= " inner join APEX_JobCard as jc on tk.RefJobCardID = jc.JobCardID "
            sql1 &= " where RefjobcardID=" & jobcardid & " and  ta.category='" & dhncategoryname.Value & "'"
            Dim ds1 As New DataSet
            ds1 = ExecuteDataSet(sql1)
            If ds1.Tables(0).Rows.Count > 0 Then
                If ds1.Tables(0).Rows(0)("sumtotal").ToString() <> "" Then
                    tot = Convert.ToDouble(ds1.Tables(0).Rows(0)("sumtotal").ToString())
                    lbltotalcost.Text = tot.ToString()

                End If

            End If

            'Dim sql As String = "select PreEventQuote from APEX_PrePnL where RefBriefID=" & capex.GetBriefIDByJobCardID(jobcardid)

            Dim bid As String = capex.GetBriefIDByJobCardID(jobcardid)
            If bid <> "" Then
                Dim sql As String = "select sum(PreEventCost) as PreEventQuote from APEX_PrePnLcost where category='" & dhncategoryname.Value & "' and  RefBriefID=" & bid
                Dim ds As New DataSet
                ds = ExecuteDataSet(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0)("PreEventQuote")) Then
                        lblPreEventQuote.Text = ds.Tables(0).Rows(0)("PreEventQuote").ToString()
                        PreEventcot = ds.Tables(0).Rows(0)("PreEventQuote")
                        'If lblremain.Text = "0" Then
                        '    lblremain.Text = lblPreEventQuote.Text
                        'End If

                        If tot = "0" Then
                            lblremain.Text = lblPreEventQuote.Text
                            lbltotalcost.Text = tot
                        End If

                    End If

                End If
                If tot > 0 And PreEventcot > 0 Then
                    lblremain.Text = (PreEventcot - tot).ToString()
                End If
            End If

        End If


    End Sub

    Private Function CheckTotalAmount(ByVal total As Double) As Boolean
        Dim capex As New clsApex
        Dim jobcardid As String
        Dim result As Boolean = False
        Dim tot As Double = 0
        'jobcardid = capex.GetJobCardByTaskID(hdnTaskID.Value)
        jobcardid = hdnJobCardID.Value
        If dhncategoryname.Value <> "" Then
            Dim sql1 As String = "select SUM(total) as sumtotal from APEX_TaskAccount as ta "
            'sql1 &= " inner join APEX_Task as tk on ta.RefTaskID = tk.TaskID "
            'sql1 &= " inner join APEX_JobCard as jc on tk.RefJobCardID = jc.JobCardID "
            sql1 &= " where RefjobcardID=" & jobcardid & " and  ta.category='" & dhncategoryname.Value & "'"
            Dim ds1 As New DataSet
            ds1 = ExecuteDataSet(sql1)
            If ds1.Tables(0).Rows.Count > 0 Then
                If ds1.Tables(0).Rows(0)(0).ToString() <> "" Then
                    tot = Convert.ToDouble(ds1.Tables(0).Rows(0)(0).ToString())
                End If
            End If
            lbltotalcost.Text = tot
            tot += total


            '  Dim sql As String = "select PreEventQuote from APEX_PrePnL where RefBriefID=" & capex.GetBriefIDByJobCardID(jobcardid)
            Dim sql As String = "select isnull(sum(PreEventcost),0) as PreEventQuote from APEX_PrePnLcost where category='" & dhncategoryname.Value & "' and  RefBriefID=" & capex.GetBriefIDByJobCardID(jobcardid)
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("PreEventQuote").ToString() <> "" Then
                    If IsNumeric(Convert.ToDouble(ds.Tables(0).Rows(0)("PreEventQuote").ToString())) Then
                        If tot <= Convert.ToDouble(ds.Tables(0).Rows(0)("PreEventQuote").ToString()) Then
                            result = True
                            lblError.Text = ""
                            divError.Visible = False
                        Else
                            result = False

                        End If
                    Else
                        result = False
                    End If
                Else
                    result = False
                End If
            Else
                result = False
            End If

        End If


        Return result
    End Function

    'Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
    '    Response.Clear()
    '    BindgdvAccount()
    '    Response.AddHeader("content-disposition", "attachment; filename=TaskAccountReport" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls")
    '    Response.Charset = ""

    '    Response.ContentType = "application/vnd.xls"

    '    Dim stringWriter As New StringWriter()
    '    Dim htmlWriter As New HtmlTextWriter(stringWriter)

    '    gvDisplay.RenderControl(htmlWriter)
    '    Response.Write(stringWriter.ToString())

    '    Response.End()
    'End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered

    End Sub

    'Protected Sub btnClaims_Click(sender As Object, e As EventArgs) Handles btnClaims.Click
    '    Dim capex As New clsApex
    '    Dim jobcardid As String
    '    jobcardid = capex.GetJobCardByTaskID(hdnTaskID.Value)
    '    Response.Redirect("RembursementClaim.aspx?jid=" & jobcardid & "&tid=" & hdnTaskID.Value)
    'End Sub

    Private Function TaskTeamMember() As Boolean
        Dim sql As String = "select RefLeadID,RefMemberID from APEX_TaskTeam where ReftaskID = " & hdnJobCardID.Value
        Dim result As Boolean = False
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If Not IsDBNull(ds.Tables(0).Rows(i)("RefLeadID")) Then
                    If ds.Tables(0).Rows(0)("RefLeadID").ToString() = getLoggedUserID() Then
                        result = True
                    End If
                End If

                If Not IsDBNull(ds.Tables(0).Rows(i)("RefMemberID")) Then
                    If ds.Tables(0).Rows(i)("RefMemberID").ToString() = getLoggedUserID() Then
                        result = True
                    End If
                End If
            Next

        Else
            result = False
        End If

        Return result

    End Function

    'Private Sub BindBalance()
    '    Dim sql As String = "select Balance from APEX_TaskAccount where AccountID=" & hdnTaskID.Value
    '    Dim ds As New DataSet
    '    ds = ExecuteDataSet(sql)
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        lblClaim.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End If
    'End Sub

    Protected Sub gvPrePnLCost_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gdvAccount.RowEditing
        gdvAccount.EditIndex = e.NewEditIndex
        'Rebind the GridView to show the data in edit mode
        Dim index As Integer = e.NewEditIndex
        'Dim ddlLead As DropDownList = CType(gdvAccount.Rows(index).FindControl("gv_ddlLead"), DropDownList)
        'Dim sql As String = "Select UserDetailsID,(isnull(firstName,'')+' '+isnull(LastName,'')) as Name from APEX_UsersDetails where IsActive='Y' and IsDeleted='N' and Role not in('A','K','F','H') order by LTRIM(FirstName)"
        'Dim ds As New DataSet
        'ds = ExecuteDataSet(sql)
        'If ds.Tables(0).Rows.Count > 0 Then
        '    ddlLead.DataSource = ds.Tables(0)
        '    ddlLead.DataTextField = "Name"
        '    ddlLead.DataValueField = "UserDetailsID"
        '    ddlLead.DataBind()
        '    ddlLead.Items.Insert(0, New ListItem("Select", "0"))
        'End If
        'Dim ddlcategory As DropDownList = CType(gdvAccount.Rows(index).FindControl("gv_ddlCategory"), DropDownList)
        'Dim sqlAct As String = "select category from [dbo].[APEX_PrePnLCost] where refbriefID=" & hdnBriefID.Value & " group by category"
        'Dim dsAct As New DataSet
        'dsAct = ExecuteDataSet(sqlAct)
        'If dsAct.Tables(0).Rows.Count > 0 Then
        '    ddlcategory.DataSource = dsAct
        '    ddlcategory.DataTextField = "category"
        '    ddlcategory.DataValueField = "category"
        '    ddlcategory.DataBind()
        '    ddlcategory.Items.Insert(0, New ListItem("Select", "0"))
        'End If


        BindgdvAccount()
        Dim hdnTaskAccountID As HiddenField = CType(gdvAccount.Rows(index).FindControl("hdnTaskAccountID"), HiddenField)
        Dim ddlLead As DropDownList = CType(gdvAccount.Rows(index).FindControl("gv_ddlLead"), DropDownList)
        Dim sql As String = "Select UserDetailsID,(isnull(firstName,'')+' '+isnull(LastName,'')) as Name from APEX_UsersDetails where IsActive='Y' and IsDeleted='N' and Role not in('A','K','F','H') order by LTRIM(FirstName)"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            ddlLead.DataSource = ds.Tables(0)
            ddlLead.DataTextField = "Name"
            ddlLead.DataValueField = "UserDetailsID"
            ddlLead.DataBind()
            ddlLead.Items.Insert(0, New ListItem("Select", "0"))
        End If
        Dim ddlcategory As DropDownList = CType(gdvAccount.Rows(index).FindControl("gv_ddlCategory"), DropDownList)
        Dim ddlcategory1 As DropDownList = CType(gdvAccount.FooterRow.FindControl("ddlCategory"), DropDownList)
        Dim sqlAct As String = "select category from [dbo].[APEX_PrePnLCost] where refbriefID=" & hdnBriefID.Value & " group by category"
        Dim dsAct As New DataSet
        dsAct = ExecuteDataSet(sqlAct)
        If dsAct.Tables(0).Rows.Count > 0 Then
            ddlcategory.DataSource = dsAct
            ddlcategory.DataTextField = "category"
            ddlcategory.DataValueField = "category"
            ddlcategory.DataBind()
            ddlcategory.Items.Insert(0, New ListItem("Select", "0"))

        End If

        Dim sqlta As String = " select category,TL from APEX_TaskAccount where AccountID =" & hdnTaskAccountID.Value & ""
        Dim dsta As New DataSet
        dsta = ExecuteDataSet(sqlta)
        If dsta.Tables(0).Rows.Count > 0 Then
            ddlcategory.SelectedValue = dsta.Tables(0).Rows(0)("category")
            ddlLead.SelectedValue = dsta.Tables(0).Rows(0)("TL")
        End If
        ddlcategory1.Enabled = False
        dhncategoryname.Value = ddlcategory.SelectedItem.Value
        CalculateBalance()
        'CalculateBalance()
    End Sub

    Protected Sub gdvAccount_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gdvAccount.RowUpdating
        'Accessing Edited values from the GridView

        Dim TaskAccountID As New HiddenField
        TaskAccountID = DirectCast(gdvAccount.Rows(e.RowIndex).FindControl("hdnTaskAccountID"), HiddenField)
        Dim Particulars As New TextBox
        Dim Quantity As New TextBox
        Dim txtAmount As New TextBox
        Dim txtTotal As New Label
        Dim startdate As New TextBox
        Dim Enddate As New TextBox
        Dim Description As New TextBox
        Dim category As New DropDownList
        Dim TL As New DropDownList
        Dim txttaskname As New TextBox
        Dim txtcity As New TextBox



        Particulars = DirectCast(gdvAccount.Rows(e.RowIndex).FindControl("gv_txtParticulars"), TextBox)
        Quantity = DirectCast(gdvAccount.Rows(e.RowIndex).FindControl("gv_txtQuantity"), TextBox)
        txtAmount = DirectCast(gdvAccount.Rows(e.RowIndex).FindControl("gv_txtAmount"), TextBox)
        txtTotal = DirectCast(gdvAccount.Rows(e.RowIndex).FindControl("gv_txtTotal"), Label)

        startdate = CType(gdvAccount.Rows(e.RowIndex).FindControl("gv_txtstartdate"), TextBox)
        Enddate = CType(gdvAccount.Rows(e.RowIndex).FindControl("gv_txtEnddate"), TextBox)
        Description = CType(gdvAccount.Rows(e.RowIndex).FindControl("gv_txtDescription"), TextBox)

        category = CType(gdvAccount.Rows(e.RowIndex).FindControl("gv_ddlCategory"), DropDownList)
        TL = CType(gdvAccount.Rows(e.RowIndex).FindControl("gv_ddlLead"), DropDownList)

        txtcity = CType(gdvAccount.Rows(e.RowIndex).FindControl("gv_txtcity"), TextBox)
        txttaskname = CType(gdvAccount.Rows(e.RowIndex).FindControl("gv_txttaskname"), TextBox)


        Dim d2, d3, d4, d5 As String
        Dim d1 As String = TaskAccountID.Value
        If Particulars.Text <> "" Then
            d2 = Clean(Particulars.Text)
        End If
        If Quantity.Text <> "" Then
            d3 = Quantity.Text
        End If
        If txtAmount.Text <> "" Then
            d4 = txtAmount.Text
        End If
        If d3 <> "" And d4 <> "" Then
            d5 = Convert.ToDouble(d3) * Convert.ToDouble(d4)
        End If
        Dim preotal As Double = Convert.ToDouble(txtTotal.Text)
        Dim amount As Double = Double.Parse(txtAmount.Text)
        Dim quantity1 As Double = Double.Parse(Quantity.Text)

        Dim total As Double = amount * quantity1 - preotal
        'Call the function to update the GridView
        If CheckTotalAmount(total) = True Then
            UpdateRecord(d1, d2, d3, d4, d5, startdate.Text, Enddate.Text, Description.Text, category.SelectedValue, TL.SelectedValue, Clean(txtcity.Text), Clean(txttaskname.Text))
            '    BindBalance()
            CalculateBalance()
            Getprepnlwithcategory()
        Else
            lblError.Text = "Amount exceeded the job budget"
            divError.Visible = True
        End If
        gdvAccount.EditIndex = -1
        'Rebind Gridview to reflect changes made
        BindgdvAccount()
    End Sub

    Protected Sub gdvAccount_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gdvAccount.RowCancelingEdit
        ' switch back to edit default mode
        gdvAccount.EditIndex = -1
        'Rebind the GridView to show the data in edit mode
        BindgdvAccount()
    End Sub

    Private Sub UpdateRecord(ByVal TaskAccountID As String, ByVal Prticulars As String, ByVal Quantity As String, ByVal Amount As String, ByVal total As String, ByVal Startdate As String, ByVal Enddate As String, ByVal Description As String, ByVal Category As String, ByVal TL As String, ByVal city As String, ByVal taskname As String)

        Dim sql As String = ""
        sql &= "UPDATE [APEX_TaskAccount]"
        sql &= "  SET [RefjobcardID] = " & hdnJobCardID.Value & ""
        sql &= ",[Particulars] = '" & Clean(Prticulars) & "'"
        sql &= "  ,[Quantity] = '" & Clean(Quantity) & "'"
        sql &= " ,[Amount] = '" & Clean(Amount) & "'"
        sql &= "  ,[Total] = '" & Clean(total) & "'"
        sql &= " ,[Balance] = '" & Clean(total) & "'"
        'sql &= " ,[startdate] = '" & Clean(Startdate) & "'"
        'sql &= " ,[Enddate] = '" & Clean(Enddate) & "'"
        sql &= " ,[startdate] = convert(datetime,'" & Clean(Startdate) & "',105)"
        sql &= " ,[Enddate] = convert(datetime,'" & Clean(Enddate) & "',105)"
        sql &= " ,[Description] = '" & Clean(Description) & "'"
        sql &= " ,Category='" & Clean(Category) & "'"
        sql &= " ,TL='" & Clean(TL) & "'"
        sql &= " ,Taskname='" & Clean(taskname) & "'"
        sql &= " ,City='" & Clean(city) & "'"
        sql &= " WHERE AccountID=" & TaskAccountID


        ExecuteNonQuery(sql)

        If ExecuteNonQuery(sql) > 0 Then
            BindgdvAccount()
            CalculateBalance()
        End If

    End Sub

    Protected Sub gdvAccount_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvAccount.RowCommand
        If e.CommandName = "add" Then

            Dim frow As GridViewRow = gdvAccount.FooterRow
            Dim txtParticulars As New TextBox
            Dim txtQuantity As New TextBox
            Dim txtAmount As New TextBox
            Dim txtTotal As New TextBox
            Dim ddlLead As New DropDownList
            Dim startdate As New TextBox
            Dim Enddate As New TextBox
            Dim Description As New TextBox
            Dim txtcity As New TextBox
            Dim txttaskname As New TextBox


            txtParticulars = CType(frow.FindControl("txtParticulars"), TextBox)
            txtQuantity = CType(frow.FindControl("txtQuantity"), TextBox)
            txtAmount = CType(frow.FindControl("txtAmount"), TextBox)
            txtTotal = CType(frow.FindControl("txtTotal"), TextBox)
            ddlLead = CType(frow.FindControl("ddlLead"), DropDownList)
            startdate = CType(frow.FindControl("txtstartdate"), TextBox)
            Enddate = CType(frow.FindControl("txtEnddate"), TextBox)
            Description = CType(frow.FindControl("txtDescription"), TextBox)
            txtcity = CType(frow.FindControl("txtCity"), TextBox)
            txttaskname = CType(frow.FindControl("txtTaskName"), TextBox)

            Dim sqlString As String = ""
            Dim amount As Double = Double.Parse(txtAmount.Text)
            Dim quantity As Double = Double.Parse(txtQuantity.Text)
            Dim total As Double = amount * quantity

            If CheckTotalAmount(total) = True Then

                Dim refTaskId As String = 1

                sqlString &= "INSERT INTO [APEX_TaskAccount]"
                sqlString &= "           ([RefjobcardID]"
                sqlString &= "           ,[Particulars]"
                sqlString &= "           ,[Quantity]"
                sqlString &= "           ,[Amount]"
                If dhncategoryname.Value <> "" Then
                    sqlString &= "           ,[Category]"
                End If
                sqlString &= "           ,[Balance]"
                sqlString &= "           ,[Total],startdate,Enddate,Description,InsertedBy,City,TaskName)"
                sqlString &= "     VALUES"
                sqlString &= "           (" & hdnJobCardID.Value
                sqlString &= "           ,'" & Clean(txtParticulars.Text) & "'"
                sqlString &= "           ," & Clean(txtQuantity.Text)
                sqlString &= "           ," & txtAmount.Text
                If dhncategoryname.Value <> "" Then
                    sqlString &= "           , '" & dhncategoryname.Value & "'  "
                End If
                sqlString &= "           ," & total
                sqlString &= "           ," & total
                sqlString &= "           ,convert(datetime,'" & Clean(startdate.Text) & "',105)"
                sqlString &= "           ,convert(datetime,'" & Clean(Enddate.Text) & "',105)"
                'sqlString &= "           ,'" & Clean(startdate.Text) & "'"
                'sqlString &= "           ,'" & Clean(Enddate.Text) & "'"
                sqlString &= "           ,'" & Clean(Description.Text) & "'"
                sqlString &= "           ,'" & getLoggedUserID() & "'"
                sqlString &= "           ,'" & Clean(txtcity.Text) & "'"
                sqlString &= "           ,'" & Clean(txttaskname.Text) & "'"
                sqlString &= "           )"

                If ExecuteNonQuery(sqlString) > 0 Then
                    'If 1 > 0 Then
                    'Send Notification

                    SendNotification_PMTask(ddlLead.SelectedItem.Text, hdnJobCardID.Value, ddlLead.SelectedValue)
                    'end send notification

                    InsertTeamLead(ddlLead)
                    BindgdvAccount()
                    lblError.Text = ""
                    divError.Visible = False
                    'Dim capex As New clsApex
                    'capex.AddTaskBalance(hdnTaskID.Value, total)
                    'BindBalance()

                    CalculateBalance()
                    Getprepnlwithcategory()
                End If
            Else
                lblError.Text = "Amount exceeded the job budget"
                divError.Visible = True
            End If
        End If
        If e.CommandName = "Delete" Then
            Dim hdnTaskAccountID As New HiddenField
            Dim hdnAmount As New HiddenField

            Dim row As GridViewRow = CType(CType(e.CommandSource, Button).NamingContainer, GridViewRow)

            hdnTaskAccountID = CType(row.FindControl("hdnTaskAccountID"), HiddenField)
            hdnAmount = CType(row.FindControl("hdnAmount"), HiddenField)

            Dim sql As String = "Delete from APEX_TaskAccount where AccountID=" & hdnTaskAccountID.Value
            If ExecuteNonQuery(sql) > 0 Then
                BindgdvAccount()
                'Dim capex As New clsApex
                'capex.DeductTaskBalance(hdnTaskID.Value, hdnAmount.Value)
                'BindBalance()
                CalculateBalance()
                Getprepnlwithcategory()
            End If
        End If
        If e.CommandName = "Edit" Then
            Dim hdnPrePnLCostID As New HiddenField

        End If
    End Sub

    Public Sub SendNotification_PMTask(ByVal Pmname As String, ByVal jcid As String, ByVal pmid As String)
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
            sql &= "('Assigned task'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>Project Manager: </b>" & getPMName() & "<br /><hr  /><b>Message: </b>You have been assigned a task'"
            sql &= ",'H'"
            sql &= "," & NotificationType.PMtaskrequest
            sql &= "," & jcid
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails_PMPost(jcid, pmid)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub InsertRecieptentDetails_PMPost(ByVal jcid As String, ByVal pmid As String)
        Try
            Dim capex As New clsApex
            Dim notificationid As String = ""
            Dim sql As String = "Select Top(1) NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & NotificationType.PMtaskrequest & " and AssociateID=" & jcid & " order by NotificationID desc"
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

            Dim bheadid As String = ""
            Dim sql3 As String = " "
            'bheadid = capex.GetProjectManagerOfJobCard(jcid)
            bheadid = pmid
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
                    sendMail(ds.Tables(0).Rows(0)("NotificationTitle").ToString() & " JC #" & capex.GetJobCardNoByJobCardID(jcid), message, "", emailid, "")
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


    Private Function getPMName() As String
        Dim bid As String = ""
        Dim result As String = ""
        Try
            Dim capex As New clsApex
            Dim sql As String = "select isnull(FirstName,'') + ' ' +isnull(LastName,'') as KAMName "
            sql &= " from APEX_UsersDetails as ud "
            'sql &= " Inner join APEX_Brief as ab on ud.UserDetailsID = ab.InsertedBy "
            sql &= " where UserDetailsID = " & getLoggedUserID()
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



    'Private Sub bindcategory()
    '    If hdnTaskID.Value <> "" Then
    '        Dim sql As String = "Select Categorytask from dbo.APEX_Task where IsActive='Y' and  TaskID= " & hdnTaskID.Value
    '        Dim ds As DataSet = Nothing
    '        ds = ExecuteDataSet(sql)
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            If Not IsDBNull(ds.Tables(0).Rows(0)("Categorytask")) Then
    '                dhncategoryname.Value = ds.Tables(0).Rows(0)("Categorytask")
    '            End If
    '        End If
    '    End If

    'End Sub

    Protected Sub gdvAccount_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdvAccount.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hdnTaskAccountID As HiddenField = CType(e.Row.FindControl("hdnTaskAccountID"), HiddenField)
            'e.Row.Cells(5).Attributes("onclick") = "javascript:call(" & hdnTaskAccountID.Value & ");"

            e.Row.Cells(5).Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
            e.Row.Cells(5).Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim ddlLead As DropDownList = CType(e.Row.FindControl("ddlLead"), DropDownList)
            Dim sql As String = "Select UserDetailsID,(isnull(firstName,'')+' '+isnull(LastName,'')) as Name from APEX_UsersDetails where IsActive='Y' and IsDeleted='N' and Role not in('A','F','H') order by LTRIM(FirstName)"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlLead.DataSource = ds.Tables(0)
                ddlLead.DataTextField = "Name"
                ddlLead.DataValueField = "UserDetailsID"
                ddlLead.DataBind()
                ddlLead.Items.Insert(0, New ListItem("Select", "0"))
            End If
            Dim ddlcategory As DropDownList = CType(e.Row.FindControl("ddlcategory"), DropDownList)
            Dim sqlAct As String = "select category from [dbo].[APEX_PrePnLCost] where refbriefID=" & hdnBriefID.Value & " group by category"
            Dim dsAct As New DataSet
            dsAct = ExecuteDataSet(sqlAct)
            If dsAct.Tables(0).Rows.Count > 0 Then
                ddlcategory.DataSource = dsAct
                ddlcategory.DataTextField = "category"
                ddlcategory.DataValueField = "category"
                ddlcategory.DataBind()
                ddlcategory.Items.Insert(0, New ListItem("Select", "0"))
            End If

        End If

        If e.Row.RowState = DataControlRowState.Edit Then
            Dim hdnTaskAccountID As HiddenField = CType(e.Row.FindControl("hdnTaskAccountID"), HiddenField)
            Dim ddlLead As DropDownList = CType(e.Row.FindControl("gv_ddlLead"), DropDownList)
            Dim sql As String = "Select UserDetailsID,(isnull(firstName,'')+' '+isnull(LastName,'')) as Name from APEX_UsersDetails where IsActive='Y' and IsDeleted='N' and Role not in('A','K','F','H') order by LTRIM(FirstName)"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlLead.DataSource = ds.Tables(0)
                ddlLead.DataTextField = "Name"
                ddlLead.DataValueField = "UserDetailsID"
                ddlLead.DataBind()
                ddlLead.Items.Insert(0, New ListItem("Select", "0"))
            End If
            Dim ddlcategory As DropDownList = CType(e.Row.FindControl("gv_ddlCategory"), DropDownList)
            Dim sqlAct As String = "select category from [dbo].[APEX_PrePnLCost] where refbriefID=" & hdnBriefID.Value & " group by category"
            Dim dsAct As New DataSet
            dsAct = ExecuteDataSet(sqlAct)
            If dsAct.Tables(0).Rows.Count > 0 Then
                ddlcategory.DataSource = dsAct
                ddlcategory.DataTextField = "category"
                ddlcategory.DataValueField = "category"
                ddlcategory.DataBind()
                ddlcategory.Items.Insert(0, New ListItem("Select", "0"))

            End If

            Dim sqlta As String = " select category,TL from APEX_TaskAccount where AccountID =" & hdnTaskAccountID.Value & ""
            Dim dsta As New DataSet
            dsta = ExecuteDataSet(sqlta)
            If dsta.Tables(0).Rows.Count > 0 Then
                ddlcategory.SelectedValue = dsta.Tables(0).Rows(0)("category")
                ddlLead.SelectedValue = dsta.Tables(0).Rows(0)("TL")
            End If
        End If
    End Sub
    Private Sub InsertTeamLead(ByVal ddlLead As DropDownList)


        Dim sqlTeamString As String = ""
        sqlTeamString &= "Update APEX_TaskAccount set TL=" & ddlLead.SelectedValue & " where AccountID=(select Max(AccountID) from APEX_TaskAccount) "

        ExecuteNonQuery(sqlTeamString)

    End Sub

    Private Sub BindDDLCategory()
        Dim sqlDDLstring As String = ""
        Dim ds As New DataSet

        Dim sqlAct As String = "select category from [dbo].[APEX_PrePnLCost] where refbriefID=" & hdnBriefID.Value & " group by category"
        Dim dsAct As New DataSet
        dsAct = ExecuteDataSet(sqlAct)

        'ddlCategory.DataSource = dsAct
        'ddlCategory.DataTextField = "category"
        'ddlCategory.DataValueField = "category"
        'ddlCategory.DataBind()

        'ddlCategory.Items.Insert(0, New ListItem("Select", "0"))

    End Sub
    Private Sub FillJobCardDetails()
        Dim sql As String = "Select JobCardNo from APEX_JobCard where JobCardID=" & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            'lblJobCode.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
        End If
    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlcategory As DropDownList = DirectCast(sender, DropDownList)
        dhncategoryname.Value = ddlcategory.SelectedItem.Value
        CalculateBalance()
    End Sub

    Private Function checktaskinsertedby(ByVal reftaskID As Integer) As Integer
        Dim taskInsertedby As Integer = 0
        Try
            Dim sql As String = " select top 1 InsertedBy from APEX_TaskAccount where RefTaskID=" & reftaskID & ""
            taskInsertedby = ExecuteSingleResult(sql, _DataType.Numeric)
        Catch ex As Exception

        End Try
        Return taskInsertedby

    End Function

    Protected Sub PasteToGridView(sender As Object, e As EventArgs)
        Try

            Dim dt As New DataTable()
            dt.Columns.AddRange(New DataColumn(11) {New DataColumn("id", GetType(Integer)), New DataColumn("Category", GetType(String)), New DataColumn("TaskName", GetType(String)), New DataColumn("City", GetType(String)), New DataColumn("Particulars", GetType(String)), New DataColumn("Quantity", GetType(String)), New DataColumn("UnitPrice", GetType(String)), New DataColumn("Total", GetType(String)), New DataColumn("TL", GetType(String)), New DataColumn("Startdate", GetType(String)), New DataColumn("Enddate", GetType(String)), New DataColumn("Description", GetType(String))})
            'dt.Columns.AddRange(New DataColumn(9) {New DataColumn("id", GetType(Integer)), New DataColumn("Category", GetType(String)), New DataColumn("Particulars", GetType(String)), New DataColumn("Quantity", GetType(String)), New DataColumn("UnitPrice", GetType(String)), New DataColumn("Total", GetType(String)) = New DataColumn("Quantity", GetType(String)) * New DataColumn("UnitPrice", GetType(String)), New DataColumn("TL", GetType(String)), New DataColumn("Startdate", GetType(String)), New DataColumn("Enddate", GetType(String)), New DataColumn("Description", GetType(String))})

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
            divcopypasteerror.Visible = True
            lblcopypasteerror.Text = "You have Entered some wrong information."
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Dim hdnTaskAccountID As HiddenField = CType(e.Row.FindControl("hdnTaskAccountID"), HiddenField)
            'e.Row.Cells(5).Attributes("onclick") = "javascript:call(" & hdnTaskAccountID.Value & ");"

            e.Row.Cells(5).Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
            e.Row.Cells(5).Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            Dim ddlLead As DropDownList = CType(e.Row.FindControl("ddlLead"), DropDownList)
            Dim hdnteamleader As HiddenField = CType(e.Row.FindControl("hdnteamleader"), HiddenField)

            Dim sql As String = "Select UserDetailsID,(isnull(firstName,'')+' '+isnull(LastName,'')) as Name from APEX_UsersDetails where IsActive='Y' and IsDeleted='N' and Role not in('A','K','F','H') order by LTRIM(FirstName)"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlLead.DataSource = ds.Tables(0)
                ddlLead.DataTextField = "Name"
                ddlLead.DataValueField = "UserDetailsID"
                ddlLead.DataBind()
                ddlLead.Items.Insert(0, New ListItem("Select", "0"))
            End If
            If hdnteamleader.Value <> "" Then
                'ddlLead.Text = hdnteamleader.Value
                If Not IsNothing(ddlLead.Items.FindByText(hdnteamleader.Value)) Then
                    ddlLead.Items.FindByText(hdnteamleader.Value).Selected = True
                End If

            End If

            Dim ddlcategory As DropDownList = CType(e.Row.FindControl("ddlcategory"), DropDownList)
            Dim hdncategory As HiddenField = CType(e.Row.FindControl("hdncatigory"), HiddenField)
            Dim sqlAct As String = "select category from [dbo].[APEX_PrePnLCost] where refbriefID=" & hdnBriefID.Value & " group by category"
            Dim dsAct As New DataSet
            dsAct = ExecuteDataSet(sqlAct)
            If dsAct.Tables(0).Rows.Count > 0 Then
                ddlcategory.DataSource = dsAct
                ddlcategory.DataTextField = "category"
                ddlcategory.DataValueField = "category"
                ddlcategory.DataBind()
                ddlcategory.Items.Insert(0, New ListItem("Select", "0"))
            End If
            If hdncategory.Value <> "" Then
                'ddlcategory.SelectedItem.Text = hdncategory.Value
                If Not IsNothing(ddlcategory.Items.FindByText(hdncategory.Value)) Then
                    ddlcategory.Items.FindByText(hdncategory.Value).Selected = True
                End If

            End If
        End If
    End Sub

    Protected Sub btncancelimport_Click(sender As Object, e As EventArgs) Handles btncancelimport.Click
        GridView1.DataSource = Nothing
        GridView1.DataBind()
        btnimport.Visible = False
        btncancelimport.Visible = False
    End Sub

    Protected Sub btnimport_Click(sender As Object, e As EventArgs) Handles btnimport.Click
        'select category from [dbo].[APEX_PrePnLCost] where refbriefID=" & hdnBriefID.Value & " group by category
        Dim iserror As Boolean = False
        Dim errorcategory As String = ""
        Dim sqlAct As String = ""
        sqlAct &= "	select A.Category,a.PreEventQuote,(isnull(a.PreEventQuote,0)-isnull(B.balance,0)) balance  from (select category,isnull(sum(PreEventcost),0) as PreEventQuote "
        sqlAct &= "	from [dbo].[APEX_PrePnLCost] A"
        sqlAct &= "	where refbriefID=" & hdnBriefID.Value & " group by category)A"
        sqlAct &= "	Left join"
        sqlAct &= "	(select category, Sum(Amount)balance from APEX_TaskAccount "
        sqlAct &= "	where RefjobcardID=(select jobcardID from APEX_JobCard where refbriefID=" & hdnBriefID.Value & ")"
        sqlAct &= "	group by category)B "
        sqlAct &= "	 on A.Category =B.category "
        sqlAct &= ""
        sqlAct &= ""
        sqlAct &= ""
        Dim dsAct As New DataSet
        dsAct = ExecuteDataSet(sqlAct)
        If dsAct.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To dsAct.Tables(0).Rows.Count - 1
                Dim _category As String = dsAct.Tables(0).Rows(i)("Category")
                Dim Balance As Decimal = CType(dsAct.Tables(0).Rows(i)("balance"), Decimal)
                Dim gridsum As Decimal = 0

                For Each row As GridViewRow In GridView1.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim category As DropDownList = TryCast(row.Cells(1).FindControl("ddlCategory"), DropDownList)
                        Dim cattotal As Label = TryCast(row.Cells(7).FindControl("lblcattotal"), Label)

                        If category.SelectedItem.Text = _category Then
                            gridsum += CType(cattotal.Text, Decimal)

                        End If
                    End If
                Next
                If gridsum > Balance Then
                    If errorcategory = "" Then
                        errorcategory = _category
                    Else
                        errorcategory &= " | " & _category
                    End If

                    'lblError.Text = "Amount exceeded the job budget for '" & _category & "' category."
                    'divError.Visible = True
                    'GridView1.DataSource = Nothing
                    'GridView1.DataBind()
                    iserror = True
                    ''Exit Sub
                Else
                    'lblError.Text = ""
                    'divError.Visible = False
                End If

            Next
            If iserror = False Then
                GridviewToDataTable(GridView1, "")
                GridView1.DataSource = Nothing
                GridView1.DataBind()
                lblError.Text = ""
                divError.Visible = False
                lblMsg.Text = "Data Inserted Successfully."
                MessageDiv.Visible = True
            Else
                lblError.Text = "Amount exceeded the job budget for '" & errorcategory & "' category."
                divError.Visible = True
            End If

        End If
        'For Each row As GridViewRow In GridView1.Rows
        '    If row.RowType = DataControlRowType.DataRow Then
        '        'Dim bf As CheckBox = TryCast(row.Cells(3).FindControl("chkDetails"), CheckBox)
        '        'Dim id As String = TryCast(row.Cells(0).FindControl("lblUserid"), Label).Text
        '        Dim category As DropDownList = TryCast(row.Cells(1).FindControl("ddlCategory"), DropDownList)

        '    End If
        'Next

    End Sub
    Public Function GridviewToDataTable(ByVal PassedGridView As GridView, ByRef Error_Message As String) As DataTable
        '-----------------------------------------------
        'Dim Tbl_StackSheets = New Data.DataTable

        'Tbl_StackSheets = ReportsCommonClass.GridviewToDataTable(StackSheetsGridView)

        '-----------------------------------------------
        Dim dtPM As New DataTable
        ' dtPM.Columns.AddRange(New DataColumn(1) {New DataColumn("Pmname", GetType(String)), New DataColumn("cnt", GetType(Integer))})

        Dim dt As New DataTable
        Dim ColInd As Integer = 0
        Dim ValOffset As Integer

        Try

            For Each col As DataControlField In PassedGridView.Columns
                dt.Columns.Add(col.HeaderText)
            Next


            If (PassedGridView.AutoGenerateDeleteButton Or PassedGridView.AutoGenerateEditButton Or PassedGridView.AutoGenerateSelectButton) Then
                ValOffset = 1
            Else
                ValOffset = 0
            End If


            For Each row As GridViewRow In PassedGridView.Rows

                Dim NewDataRow As DataRow = dt.NewRow

                ColInd = 0
                For Each col As DataControlField In PassedGridView.Columns
                    If ColInd = 1 Then
                        Dim category As DropDownList = TryCast(row.Cells(1).FindControl("ddlCategory"), DropDownList)
                        NewDataRow(ColInd) = category.SelectedItem.Text
                    ElseIf ColInd = 7 Then
                        Dim cattotal As Label = TryCast(row.Cells(7).FindControl("lblcattotal"), Label)
                        NewDataRow(ColInd) = cattotal.Text
                    ElseIf ColInd = 8 Then
                        Dim teaml As DropDownList = TryCast(row.Cells(8).FindControl("ddlLead"), DropDownList)
                        NewDataRow(ColInd) = teaml.SelectedValue
                    Else
                        NewDataRow(ColInd) = row.Cells(ColInd + ValOffset).Text.Replace("&nbsp;", "")
                    End If
                    ColInd += 1
                Next

                dt.Rows.Add(NewDataRow)
                insertdataintofinal(dt, row.RowIndex)


            Next

            dtPM = GroupBy("TeamLeader", "TeamLeader", dt)
            If dtPM.Rows.Count > 0 Then
                For i As Integer = 0 To dtPM.Rows.Count() - 1
                    SendNotification_PMTask(dtPM.Rows(i)(0), hdnJobCardID.Value, dtPM.Rows(i)(0), dtPM.Rows(i)(1))
                Next

                BindgdvAccount()
                lblError.Text = ""
                divError.Visible = False
                Getprepnlwithcategory()

            End If

            Error_Message = Nothing

        Catch ex As Exception
            Error_Message = "GridviewToDataTable: " & ex.Message
        End Try


        Return dt


    End Function

    Public Function GroupBy(i_sGroupByColumn As String, i_sAggregateColumn As String, i_dSourceTable As DataTable) As DataTable

        Dim dv As New DataView(i_dSourceTable)

        'getting distinct values for group column
        Dim dtGroup As DataTable = dv.ToTable(True, New String() {i_sGroupByColumn})

        'adding column for the row count
        dtGroup.Columns.Add("Count", GetType(Integer))

        'looping thru distinct values for the group, counting
        For Each dr As DataRow In dtGroup.Rows
            dr("Count") = i_dSourceTable.Compute("Count(" & i_sAggregateColumn & ")", (i_sGroupByColumn & " = '") + dr(i_sGroupByColumn) & "'")
        Next

        'returning grouped/counted result
        Return dtGroup
    End Function

    Private Sub insertdataintofinal(dt As DataTable, ByVal rowno As Integer)
        Try
            Dim sqlString As String = ""

            sqlString &= "INSERT INTO [APEX_TaskAccount]"
            sqlString &= "           ([RefjobcardID]"
            sqlString &= "           ,[Category],TaskName,City"
            sqlString &= "           ,[Particulars],[Quantity]"
            sqlString &= "           ,[Amount]"
            sqlString &= "           ,[Total]"
            sqlString &= "           ,[Balance],TL"
            sqlString &= "           ,startdate,Enddate,Description,InsertedBy)"
            sqlString &= "     VALUES"
            sqlString &= "           (" & hdnJobCardID.Value
            sqlString &= "           ,'" & Clean(dt.Rows(rowno)(1)) & "'"
            sqlString &= "           ,'" & Clean(dt.Rows(rowno)(2)) & "'"
            sqlString &= "           ,'" & dt.Rows(rowno)(3) & "'"
            sqlString &= "           , '" & dt.Rows(rowno)(4) & "'  "
            sqlString &= "           ," & dt.Rows(rowno)(5)
            sqlString &= "           ," & dt.Rows(rowno)(6)
            sqlString &= "           ,'" & Clean(dt.Rows(rowno)(7)) & "'"
            sqlString &= "           ,'" & Clean(dt.Rows(rowno)(7)) & "'"
            sqlString &= "           ,'" & dt.Rows(rowno)(8) & "'"
            sqlString &= "           ,'" & dt.Rows(rowno)(9) & "'"
            sqlString &= "           ,'" & Clean(dt.Rows(rowno)(10)) & "'"
            sqlString &= "           ,'" & Clean(dt.Rows(rowno)(11)) & "'"
            sqlString &= "            ,'" & getLoggedUserID() & "'"
            sqlString &= "           )"

            If ExecuteNonQuery(sqlString) > 0 Then

               

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Getprepnlwithcategory()
        Try
            Dim sqlAct As String = ""
            sqlAct &= "	select A.Category,a.PreEventQuote,(isnull(a.PreEventQuote,0)-isnull(B.balance,0)) balance,isnull(B.balance,0)used  from (select category,isnull(sum(PreEventcost),0) as PreEventQuote "
            sqlAct &= "	from [dbo].[APEX_PrePnLCost] A"
            sqlAct &= "	where refbriefID=" & hdnBriefID.Value & " group by category)A"
            sqlAct &= "	Left join"
            sqlAct &= "	(select category, Sum(total)balance from APEX_TaskAccount "
            sqlAct &= "	where RefjobcardID=(select jobcardID from APEX_JobCard where refbriefID=" & hdnBriefID.Value & ")"
            sqlAct &= "	group by category)B "
            sqlAct &= "	 on A.Category =B.category "
            sqlAct &= ""
            sqlAct &= ""
            sqlAct &= ""
            Dim dsAct As New DataSet
            dsAct = ExecuteDataSet(sqlAct)
            gridprepnlcat.DataSource = dsAct
            gridprepnlcat.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub gridprepnlcat_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridprepnlcat.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim lblqy As Label = DirectCast(e.Row.FindControl("lblqty"), Label)
    '        Dim qty As Integer = Int32.Parse(lblqy.Text)
    '        total = total + qty
    '    End If
    '    If e.Row.RowType = DataControlRowType.Footer Then
    '        Dim lblTotalqty As Label = DirectCast(e.Row.FindControl("lblTotalqty"), Label)
    '        lblTotalqty.Text = total.ToString()
    '    End If
    'End Sub


    Protected Sub gdvAccount_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gdvAccount.RowDeleting

    End Sub

    Public Sub SendNotification_PMTask(ByVal Pmname As String, ByVal jcid As String, ByVal pmid As String, ByVal message As String)
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
            sql &= "('Assigned task'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>Project Manager: </b>" & getPMName() & "<br /><hr  /><b>Message: </b> You have been assigned " & message & " task'"
            sql &= ",'H'"
            sql &= "," & NotificationType.PMtaskrequest
            sql &= "," & jcid
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails_PMPost(jcid, pmid)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Public Function ProcessMyDataItem(ByVal myValue As Object) As Integer

        If IsDBNull(myValue) Then
            Return 0
        End If

        Return myValue

    End Function


End Class