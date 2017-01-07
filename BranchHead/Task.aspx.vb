Imports clsDatabaseHelper
Imports clsMain
Imports System.Data
Imports clsApex

Partial Class Task
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ddlSubCategory.Visible = False
        lblSubCategory.Visible = False
        Label2.Visible = False
        If Not Page.IsPostBack Then
            'tblVendor.Visible = False
            ' tblVendorContacts.Visible = False

            divError.Visible = False

            If Request.QueryString("jc") <> Nothing Then
                If Request.QueryString("jc").ToString() <> "" Then

                    hdnJobCardID.Value = Request.QueryString("jc").ToString()

                    If checkpostpnlstatus(hdnJobCardID.Value) = True Then
                        MessageDiv.Visible = True
                        lblmsg.Text = "You Can not create Task for this jobcard. Because job is Closed."
                        divContent.Visible = False
                        Exit Sub
                    Else
                        MessageDiv.Visible = False
                        lblmsg.Text = ""
                        divContent.Visible = True
                    End If

                    Dim capex As New clsApex
                    hdnBriefID.Value = capex.GetBriefIDByJobCardID(hdnJobCardID.Value)

                    If capex.CheckStageLevel("6", hdnJobCardID.Value) = True Then
                        If Request.QueryString("mode") <> Nothing Then
                            If Request.QueryString("mode").ToString() <> "" Then
                                ddlContactPersonName.Items.Clear()
                                'TWContent.InnerHtml = capex.GetList("6", hdnJobCardID.Value)
                                ddlContactPersonName.Items.Insert(0, New ListItem("Select", "0"))
                                BindDDLActivity()
                                BindDDLCategory()
                                BindDDLVendor()
                                'BindDDLClient()

                                btnSubTask.Visible = False


                                If Request.QueryString("mode") = "edit" Then
                                    If Request.QueryString("tid") <> Nothing Then
                                        If Request.QueryString("tid").ToString() <> "" Then
                                            hdnTaskID.Value = Request.QueryString("tid").ToString()
                                            btnEditNext.Visible = True
                                            'btnNext.Visible = False
                                            btnn.Visible = False
                                            FillDetails()
                                            lblActivity.Visible = False
                                            lblCategory.Visible = False

                                            lblTitle.Visible = False
                                            lblJobCode.Visible = False
                                            lblDescription.Visible = False
                                            lblStartDate.Visible = False
                                            lblEndDate.Visible = False
                                            lblRemarks.Visible = False
                                            lblVendor.Visible = False
                                            lblContactPerson.Visible = False
                                            If capex.checkTaskCompleted(hdnTaskID.Value) = "Y" Then
                                                'CloseAllControls()
                                                If GetTeamLeaderID(hdnTaskID.Value) = getLoggedUserID() Then
                                                    If capex.getjobStatus(hdnJobCardID.Value) = "Y" Then
                                                        btnSubTask.Visible = False
                                                    Else
                                                        btnSubTask.Visible = True
                                                    End If


                                                End If

                                            End If
                                        End If
                                    End If
                                ElseIf Request.QueryString("mode") = "add" Then
                                    FillJobCardDetails()
                                    btnEditNext.Visible = False
                                    btnn.Visible = True
                                    FillJobtital()
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
            Else
                CallDivError()
            End If
        End If
    End Sub

    Private Function GetTeamLeaderID(ByVal tid As String) As Integer
        Dim leaderid As Integer = 0

        Dim sql As String = ""
        sql &= "Select distinct refLeadID from dbo.APEX_taskTEam where reftaskid=" & tid & " and isActive='Y'"
        Dim ds As DataSet = Nothing
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("refLeadID")) Then
                leaderid = ds.Tables(0).Rows(0)("refLeadID")
            End If
        End If
        Return leaderid
    End Function

    Private Sub BindDDLActivity()
        Dim sqlDDLstring As String = ""
        Dim ds As New DataSet

        sqlDDLstring = "Select ProjectTypeID,ProjectType from APEX_ActivityType where IsActive='Y' and IsDeleted='N' Order By ProjectType"
        ds = ExecuteDataSet(sqlDDLstring)

        If ds.Tables(0).Rows.Count > 0 Then
            ddlActivity.DataSource = ds
            ddlActivity.DataTextField = "ProjectType"
            ddlActivity.DataValueField = "ProjectTypeID"
            ddlActivity.DataBind()

        End If
        'ddlActivity.Items.Insert(0, New ListItem("Select", "0"))

    End Sub

    Private Sub BindDDLCategory()
        Dim sqlDDLstring As String = ""
        Dim ds As New DataSet


        sqlDDLstring = "Select CategoryTypeID,CategoryType from Apex_Category where IsActive='Y' and IsDeleted='N' Order By CategoryType"
        ds = ExecuteDataSet(sqlDDLstring)

        If ds.Tables(0).Rows.Count > 0 Then
            'ddlCategory.DataSource = ds
            'ddlCategory.DataTextField = "CategoryType"
            'ddlCategory.DataValueField = "CategoryTypeID"
            'ddlCategory.DataBind()

            ddlVendorCategory.DataSource = ds
            ddlVendorCategory.DataTextField = "CategoryType"
            ddlVendorCategory.DataValueField = "CategoryTypeID"
            ddlVendorCategory.DataBind()
        End If

        'Dim sqlAct As String = "select RefActivityID from APEX_JobCard where JobCardID=" & hdnJobCardID.Value
        Dim sqlAct As String = "select category from [dbo].[APEX_PrePnLCost] where refbriefID=" & hdnBriefID.Value & " group by category"
        'hdnBriefID.Value
        Dim dsAct As New DataSet
        Dim arrIns As String()

        dsAct = ExecuteDataSet(sqlAct)
        'If dsAct.Tables(0).Rows.Count > 0 Then
        '    arrIns = (dsAct.Tables(0).Rows(0)(0).ToString()).Split("|")
        '    ddlCategory.DataSource = arrIns
        '    ddlCategory.DataBind()
        'End If
        ddlCategory.DataSource = dsAct
        ddlCategory.DataTextField = "category"
        ddlCategory.DataValueField = "category"
        ddlCategory.DataBind()

        ddlCategory.Items.Insert(0, New ListItem("Select", "0"))
        ddlVendorCategory.Items.Insert(0, New ListItem("Select", "0"))
    End Sub


    'Private Sub BindDDLSubCategory()
    '    Dim sqlDDLstring As String = ""
    '    Dim ds As New DataSet

    '    sqlDDLstring = "Select SubCategoryTypeID,SubCategoryType from Apex_SubCategory where RefCategoryTypeID=" & ddlCategory.SelectedItem.Value & " and IsActive='Y' and IsDeleted='N' Order By SubCategoryType"
    '    ds = ExecuteDataSet(sqlDDLstring)

    '    If ds.Tables(0).Rows.Count > 0 Then
    '        ddlSubCategory.DataSource = ds
    '        ddlSubCategory.DataTextField = "SubCategoryType"
    '        ddlSubCategory.DataValueField = "SubCategoryTypeID"
    '        ddlSubCategory.DataBind()

    '    End If
    '    ddlSubCategory.Items.Insert(0, New ListItem("Select", "0"))
    'End Sub

    Private Sub BindDDLVendor()
        ddlVendor.Items.Clear()
        Dim sqlDDLstring As String = ""
        Dim ds As New DataSet

        sqlDDLstring = "Select VendorID,VendorName from APEX_Vendor where IsActive='Y' and IsDeleted='N' Order By VendorName"
        ds = ExecuteDataSet(sqlDDLstring)

        Dim LastVal As Integer = ds.Tables(0).Rows.Count + 1

        If ds.Tables(0).Rows.Count > 0 Then
            ddlVendor.DataSource = ds
            ddlVendor.DataTextField = "VendorName"
            ddlVendor.DataValueField = "VendorID"
            ddlVendor.DataBind()

        End If
        ddlVendor.Items.Insert(0, New ListItem("Select", "0"))
        ddlVendor.Items.Insert(LastVal, "Non Existing(New Vendor)")
    End Sub

    Private Sub BindDDLClient()
        ddlContactPersonName.Items.Clear()
        Dim sqlDDLstring As String = ""
        Dim ds As New DataSet

        sqlDDLstring = "Select VendorContactID,ContactName from APEX_VendorContacts where RefVendorID=" & ddlVendor.SelectedItem.Value & " and IsActive='Y' and IsDeleted='N' Order By ContactName"

        ds = ExecuteDataSet(sqlDDLstring)
        Dim LastValue As Integer = ds.Tables(0).Rows.Count + 1

        If ds.Tables(0).Rows.Count > 0 Then
            ddlContactPersonName.DataSource = ds
            ddlContactPersonName.DataTextField = "ContactName"
            ddlContactPersonName.DataValueField = "VendorContactID"
            ddlContactPersonName.DataBind()

        End If
        ddlContactPersonName.Items.Insert(0, New ListItem("Select", "0"))
        ddlContactPersonName.Items.Insert(LastValue, "Non Existing(New Person)")
    End Sub

    Private Sub InsertQuery()
        If CheckDuplicateTask() = False Then
            Dim sqlInsertString As String = ""
            Dim StartDate As String = "Convert(datetime,'" & txtStartDate.Text & "',105)"
            Dim EndDate As String = "Convert(datetime,'" & txtEndDate.Text & "',105)"


            sqlInsertString &= "INSERT INTO [APEX_Task] ( "
            sqlInsertString &= "           [RefActivityType]"
            'sqlInsertString &= "           ,[RefCategory]"
            sqlInsertString &= "           ,[RefSubCategory]"
            sqlInsertString &= "           ,[Title]"
            sqlInsertString &= "           ,[RefJobCardID]"
            sqlInsertString &= "           ,[StartDate]"
            sqlInsertString &= "           ,[EndDate]"
            sqlInsertString &= "           ,[Description]"
            sqlInsertString &= "           ,[RefVendor]"
            sqlInsertString &= "           ,[RefVendorContactPerson]"
            sqlInsertString &= "           ,[Remarks]"
            sqlInsertString &= "           ,[CategoryTask]"
            sqlInsertString &= ",InsertedBy)"
            sqlInsertString &= "     VALUES"
            sqlInsertString &= "           ("
            sqlInsertString &= ddlActivity.SelectedValue & ","
            'sqlInsertString &= ddlCategory.SelectedValue & ","
            sqlInsertString &= "NULL,"
            sqlInsertString &= "    '" & Clean(txtTitle.Text) & "',    "
            sqlInsertString &= hdnJobCardID.Value & ","
            sqlInsertString &= StartDate & ","
            sqlInsertString &= EndDate & ","
            sqlInsertString &= "    '" & Clean(txtDescription.Text) & "',    "

            If ddlVendor.SelectedIndex > 0 Then
                sqlInsertString &= ddlVendor.SelectedValue & ","
            Else
                sqlInsertString &= "NULL,"
            End If

            If ddlContactPersonName.SelectedIndex > 0 Then
                sqlInsertString &= ddlContactPersonName.SelectedValue & ","
            Else
                sqlInsertString &= "NULL,"
            End If

            If txtRemarks.Text <> "" Then
                sqlInsertString &= "'" & Clean(txtRemarks.Text) & "',"
            Else
                sqlInsertString &= "NULL,"
            End If

            sqlInsertString &= "'" & ddlCategory.SelectedItem.Text & "',"
            sqlInsertString &= "" & getLoggedUserID() & ")"

            If ExecuteNonQuery(sqlInsertString) > 0 Then
                GetTaskID()
                'hdncategory.Value = ddlCategory.SelectedItem.Text
                If hdnTaskID.Value <> "" Then
                    Response.Redirect("TaskAccount.aspx?tid=" & hdnTaskID.Value)
                    'lblmsg.Text = "Task Generated successfully"
                    'MessageDiv.Visible = True
                    'divContent.Visible = False
                End If

            End If
        Else
            lblError.Text = "Task title already exists for this JobCode"
            divError.Visible = True
        End If
    End Sub

    'Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
    '    InsertQuery()
    'End Sub

    Private Sub FillJobCardDetails()
        Dim sql As String = "Select * from APEX_JobCard where JobCardID=" & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            lblJobCode.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
            ddlActivity.SelectedIndex = ddlActivity.Items.IndexOf(ddlActivity.Items.FindByValue(ds.Tables(0).Rows(0)("PrimaryActivityID").ToString()))
        End If
    End Sub

    'Protected Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendor.SelectedIndexChanged
    '    Dim LastValue As Integer = 0
    '    If ddlVendor.SelectedValue = "Non Existing(New Vendor)" Then

    '        tblVendor.Visible = True
    '    Else
    '        ClearNewVendor()
    '        tblVendor.Visible = False
    '        If ddlVendor.SelectedIndex > 0 Then
    '            Dim sql As String = "Select * From APEX_VendorContacts where RefVendorID=" & ddlVendor.SelectedItem.Value & " Order By ContactName"
    '            Dim ds As New DataSet
    '            ds = ExecuteDataSet(sql)
    '            LastValue = ds.Tables(0).Rows.Count + 1


    '            ddlContactPersonName.Items.Clear()

    '            If ds.Tables(0).Rows.Count > 0 Then
    '                ddlContactPersonName.Items.Clear()
    '                ddlContactPersonName.DataSource = ds
    '                ddlContactPersonName.DataTextField = "ContactName"
    '                ddlContactPersonName.DataValueField = "VendorContactID"
    '                ddlContactPersonName.DataBind()

    '                ddlContactPersonName.Items.Insert(0, New ListItem("Select", "0"))
    '                ddlContactPersonName.Items.Insert(LastValue, "Non Existing(New Person)")
    '            Else
    '                ddlContactPersonName.Items.Insert(0, New ListItem("Select", "0"))
    '                ddlContactPersonName.Items.Insert(LastValue, "Non Existing(New Person)")
    '            End If
    '        Else
    '            ddlContactPersonName.Items.Clear()
    '            ddlContactPersonName.Items.Insert(0, New ListItem("Select", "0"))
    '        End If
    '    End If



    'End Sub
    Private Sub GetTaskID()
        Dim sql As String = "Select TaskID from APEX_Task where RefJobCardID=" & hdnJobCardID.Value & " and Title='" & Clean(txtTitle.Text) & "'"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnTaskID.Value = ds.Tables(0).Rows(0)("TaskID").ToString()

        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("ListofTask.aspx?jid=" & hdnJobCardID.Value)
    End Sub

    Private Sub FillDetails()
        Dim sql As String = "select [RefActivityType]"
        sql &= ",[RefCategory]"
        sql &= ",[RefSubCategory]"
        sql &= ",[Title]"
        sql &= ",JobCardNo"
        sql &= ",convert(varchar(10),StartDate,105) as NStartDate"
        sql &= " ,convert(varchar(10),EndDate,105) as NEndDate"
        sql &= ",[Description]"
        sql &= ",[RefVendor]"
        sql &= ",[RefVendorContactPerson]"
        sql &= ",[Remarks]"
        sql &= ",[CategoryTask]"
        sql &= " From APEX_Task as t "
        sql &= " Inner join APEX_JobCard as j on t.RefJobCardID = j.JobCardID"
        sql &= " where TaskID = " & hdnTaskID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            ddlActivity.SelectedIndex = ddlActivity.Items.IndexOf(ddlActivity.Items.FindByValue(ds.Tables(0).Rows(0)("RefActivityType").ToString()))

            ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(ds.Tables(0).Rows(0)("RefCategory").ToString()))
            '  BindDDLSubCategory()

            '  ddlSubCategory.SelectedIndex = ddlSubCategory.Items.IndexOf(ddlSubCategory.Items.FindByValue(ds.Tables(0).Rows(0)("RefSubCategory").ToString()))
            txtTitle.Text = ds.Tables(0).Rows(0)("Title").ToString()
            lblJobCode.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
            txtStartDate.Text = ds.Tables(0).Rows(0)("NStartDate").ToString()
            txtEndDate.Text = ds.Tables(0).Rows(0)("NEndDate").ToString()
            txtDescription.Text = ds.Tables(0).Rows(0)("Description").ToString()
            ddlVendor.SelectedIndex = ddlVendor.Items.IndexOf(ddlVendor.Items.FindByValue(ds.Tables(0).Rows(0)("RefVendor").ToString()))
            BindDDLClient()
            ddlContactPersonName.SelectedIndex = ddlContactPersonName.Items.IndexOf(ddlContactPersonName.Items.FindByValue(ds.Tables(0).Rows(0)("RefVendorContactPerson").ToString()))
            txtRemarks.Text = ds.Tables(0).Rows(0)("Remarks").ToString()

            lblActivity.Text = ddlActivity.SelectedItem.Text
            lblCategory.Text = ddlCategory.SelectedItem.Text
            '  lblSubCategory.Text = ddlSubCategory.SelectedItem.Text
            lblTitle.Text = ds.Tables(0).Rows(0)("Title").ToString()
            lblJobCode.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
            lblStartDate.Text = ds.Tables(0).Rows(0)("NStartDate").ToString()
            lblEndDate.Text = ds.Tables(0).Rows(0)("NEndDate").ToString()
            lblDescription.Text = ds.Tables(0).Rows(0)("Description").ToString()
            If ddlVendor.SelectedIndex > 0 Then
                lblVendor.Text = ddlVendor.SelectedItem.Text
            Else
                lblVendor.Text = ""
            End If
            If ddlContactPersonName.SelectedIndex > 0 Then
                lblContactPerson.Text = ddlContactPersonName.SelectedItem.Text
            Else
                lblContactPerson.Text = ""
            End If
            lblRemarks.Text = ds.Tables(0).Rows(0)("Remarks").ToString()
            ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(ds.Tables(0).Rows(0)("CategoryTask").ToString()))
        End If
    End Sub

    Protected Sub btnEditNext_Click(sender As Object, e As EventArgs) Handles btnEditNext.Click
        Dim sqlInsertString As String = ""
        Dim StartDate As String = "Convert(datetime,'" & txtStartDate.Text & "',105)"
        Dim EndDate As String = "Convert(datetime,'" & txtEndDate.Text & "',105)"

        Try
            sqlInsertString &= "Update APEX_Task Set RefActivityType=" & ddlActivity.SelectedValue
            sqlInsertString &= "           ,RefCategory=NULL"
            sqlInsertString &= "           ,RefSubCategory=NULL"
            sqlInsertString &= "           ,Title='" & Clean(txtTitle.Text) & "'"
            sqlInsertString &= "           ,RefJobCardID=" & hdnJobCardID.Value
            sqlInsertString &= "           ,StartDate=" & StartDate
            sqlInsertString &= "           ,EndDate=" & EndDate
            sqlInsertString &= "           ,Description='" & Clean(txtDescription.Text) & "'"


            If ddlVendor.SelectedIndex <> 0 And ddlVendor.SelectedValue <> "Select" Then
                sqlInsertString &= "           ,RefVendor=" & ddlVendor.SelectedValue
            Else
                sqlInsertString &= "           ,RefVendor=NULL"
            End If

            If ddlVendor.SelectedIndex <> 0 And ddlVendor.SelectedValue <> "Select" Then
                sqlInsertString &= "           ,RefVendorContactPerson=" & ddlVendor.SelectedValue
            Else
                sqlInsertString &= "           ,RefVendorContactPerson=NULL"
            End If

            If txtRemarks.Text <> "" Then
                sqlInsertString &= "           ,Remarks='" & Clean(txtRemarks.Text) & "'"
            Else
                sqlInsertString &= "           ,Remarks=NULL"
            End If
            sqlInsertString &= "           ,CategoryTask='" & ddlCategory.SelectedItem.Text & "'"
            sqlInsertString &= ",ModifiedBy=" & getLoggedUserID()
            sqlInsertString &= ",ModifiedOn=getdate()"
            sqlInsertString &= " where TaskID=" & hdnTaskID.Value
            If ExecuteNonQuery(sqlInsertString) > 0 Then
                hdncategory.Value = ddlCategory.SelectedItem.Text
                'Response.Redirect("TaskAccount.aspx?tid=" & hdnTaskID.Value & "&cat=" & hdncategory.Value)
                Response.Redirect("ListofTask.aspx?jid=" & hdnJobCardID.Value)
            End If
        Catch ex As Exception
            Response.Write(ex)
        End Try
    End Sub

    Private Sub CloseAllControls()
        ddlActivity.Visible = False
        ddlCategory.Visible = False
        ddlSubCategory.Visible = False
        txtTitle.Visible = False
        '  txtJobCode.Visible = False
        txtDescription.Visible = False
        txtStartDate.Visible = False
        txtEndDate.Visible = False
        txtRemarks.Visible = False
        ddlVendor.Visible = False
        ddlContactPersonName.Visible = False

        lblActivity.Visible = True
        lblCategory.Visible = True
        '  lblSubCategory.Visible = True
        lblTitle.Visible = True
        lblJobCode.Visible = True
        lblDescription.Visible = True
        lblStartDate.Visible = True
        lblEndDate.Visible = True
        lblRemarks.Visible = True
        lblVendor.Visible = True
        lblContactPerson.Visible = True
    End Sub

    Protected Sub btnSubTask_Click(sender As Object, e As EventArgs) Handles btnSubTask.Click
        Response.Redirect("CreateSubTask.aspx?jid=" & hdnJobCardID.Value & "&tid=" & hdnTaskID.Value)
    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        'ddlSubCategory.Items.Clear()
        'If ddlCategory.SelectedIndex > 0 Then
        '    Dim sql As String = "Select * From APEX_SubCategory where RefCategoryTypeID=" & ddlCategory.SelectedItem.Value & " Order By SubCategoryType"
        '    Dim ds As New DataSet
        '    ds = ExecuteDataSet(sql)
        '    If ds.Tables(0).Rows.Count > 0 Then
        '        ddlSubCategory.DataSource = ds
        '        ddlSubCategory.DataTextField = "SubCategoryType"
        '        ddlSubCategory.DataValueField = "SubCategoryTypeID"
        '        ddlSubCategory.DataBind()
        '    End If
        'Else
        '    ddlSubCategory.Items.Clear()
        'End If

        'ddlSubCategory.Items.Insert(0, New ListItem("Select", "0"))
    End Sub

    Private Function CheckDuplicateTask() As Boolean
        Dim result As Boolean = True
        Dim sql As String = "Select * from APEX_Task where Title='" & Clean(txtTitle.Text) & "' and RefJobCardID=" & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = True
        Else
            result = False
            lblError.Text = ""
            divError.Visible = False
        End If
        Return result
    End Function

    Private Sub CallDivError()
        divContent.Visible = False
        divError.Visible = True
        Dim capex As New clsApex

        lblError.Text = capex.SetErrorMessage()
    End Sub

    Private Sub FillJobtital()
        Dim sql As String = ""
        Dim ds As DataSet = Nothing
        If hdnJobCardID.Value <> "" Then
            sql = "Select jobCardname from Apex_jobcard where jobcardid=" & hdnJobCardID.Value & " and isActive='Y' and IsDeleted='N'"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("jobCardname")) Then
                    ' txtTitle.Text = ds.Tables(0).Rows(0)("jobCardname").ToString()
                End If

            End If

        End If

    End Sub

    Private Sub ClearNewVendor()
        txtVendorName.Text = ""
        ddlVendorCategory.SelectedIndex = 0
    End Sub

    'Protected Sub ddlContactPersonName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContactPersonName.SelectedIndexChanged
    '    If ddlContactPersonName.SelectedValue = "Non Existing(New Person)" Then
    '        tblVendorContacts.Visible = True
    '    Else
    '        tblVendorContacts.Visible = False
    '    End If
    'End Sub

    Protected Sub btnAddClient_Click(sender As Object, e As EventArgs) Handles btnAddClient.Click
        Dim sql As String = ""

        sql = "insert into APEX_Vendor(VendorName,RefCategoryID)"
        sql &= "values('" & Clean(txtVendorName.Text.Trim()) & "'," & ddlVendorCategory.SelectedValue & ")"

        If ExecuteNonQuery(sql) > 0 Then
            ClearNewVendor()
            'tblVendor.Visible = False
            BindDDLVendor()

            ddlVendor.SelectedIndex = ddlVendor.Items.IndexOf(ddlVendor.Items.FindByText(txtVendorName.Text))
        End If

    End Sub


    Protected Sub btnContactPerson_Click(sender As Object, e As EventArgs) Handles btnContactPerson.Click
        If txtContactPerson.Text.Trim() = "" Then
            txtContactPerson.Focus()
        End If

        Dim sql As String = "Insert into APEX_VendorContacts(RefVendorID,ContactName,ContactOfficialEmailID,Mobile1) "
        sql &= " values (" & ddlVendor.SelectedValue & ",'" & Clean(txtContactPerson.Text) & "','" & Clean(txtContactOfficialEmail.Text) & "','" & Clean(txtContactMobile1.Text) & "')"
        If ExecuteNonQuery(sql) > 0 Then
            ' tblVendorContacts.Visible = False

            ddlContactPersonName.Items.Clear()


            Dim sql01 As String = "Select * From APEX_VendorContacts where RefVendorID=" & ddlVendor.SelectedItem.Value & " Order By ContactName"
            Dim ds01 As New DataSet
            ds01 = ExecuteDataSet(sql01)
            Dim LastValue As Integer = ds01.Tables(0).Rows.Count
            If ds01.Tables(0).Rows.Count > 0 Then
                ddlContactPersonName.DataSource = ds01
                ddlContactPersonName.DataTextField = "ContactName"
                ddlContactPersonName.DataValueField = "VendorContactID"
                ddlContactPersonName.DataBind()

                ddlContactPersonName.Items.Insert(LastValue, "Non Existing(New Person)")
            End If





            ddlContactPersonName.SelectedIndex = ddlContactPersonName.Items.IndexOf(ddlContactPersonName.Items.FindByText(txtContactPerson.Text))
            ClearNewContactPerson()
        End If
    End Sub

    Private Sub ClearNewContactPerson()
        txtContactPerson.Text = ""
        txtContactOfficialEmail.Text = ""
        txtContactMobile1.Text = ""
    End Sub

    'Protected Sub bntcancle_Click(sender As Object, e As EventArgs) Handles bntcancle.Click
    '    'tblVendor.Visible = False
    '    ddlVendor.SelectedIndex = 0
    '    'ddlContactPersonName.DataSource = Nothing
    '    'ddlContactPersonName.DataBind()
    'End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    'tblVendorContacts.Visible = False
    '    ddlContactPersonName.SelectedIndex = 0
    'End Sub

  
    Protected Sub btnn_Click(sender As Object, e As EventArgs) Handles btnn.Click
        InsertQuery()
    End Sub
End Class
