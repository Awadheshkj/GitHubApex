Imports System.Data
Imports clsDatabaseHelper
Imports clsMain
Imports clsApex

Partial Class LeadManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                divError.Visible = False
                MessageDiv.Visible = False
                'If Len(Request.QueryString("call")) > 0 Then
                '    If Request.QueryString("call") <> Nothing Then
                '        FillClient1(Request.QueryString("call"))
                '    End If
                'End If

                If Len(Request.QueryString("mode")) > 0 Then


                    If Request.QueryString("mode") <> Nothing Then
                        If Request.QueryString("mode").ToString() <> "" Then
                            btnCancelHome.Visible = False
                            ' btnCancel.Visible = True
                            Dim mode As String = Request.QueryString("mode")
                            hdnMode.Value = mode
                            ' FillClient()
                            ddlContactPerson.Items.Insert(0, New ListItem("Select", "0"))
                            Dim apex As New clsApex
                            lblContact.Text = apex.GetKeyStoneContacts(getLoggedUserID())
                            FillActivityType()
                            FillLeadsStatus()
                            lblClient.Visible = False
                            lblContactPerson.Visible = False
                            lblLeadName.Visible = False
                            lblActivity.Visible = False
                            lblActivityType.Visible = False
                            lblLeadBrief.Visible = False
                            lblBudget.Visible = False
                            lblExecution.Visible = False
                            lblClosureDateTime.Visible = False
                            lblClosureProbability.Visible = False
                            lblLeadStatus.Visible = False
                            lblContact.Visible = True
                            lblRemarks.Visible = False

                            Dim rights As String = "R" 'CheckforBusinessRights()

                            If hdnMode.Value = "edit" Then

                                Naction.Enabled = True
                                If Len(Request.QueryString("lid")) > 0 Then
                                    If Request.QueryString("lid") <> Nothing Then
                                        If Request.QueryString("lid").ToString() <> "" Then
                                            Dim leadid As String = Request.QueryString("lid")
                                            Dim capex As New clsApex
                                            Dim jobcard As String = capex.GetJobCardIDByLeadID(leadid)
                                            If jobcard <> "" Then
                                            End If
                                            hdnLeadsID.Value = leadid
                                            btnEdit.Visible = True
                                            btnAdd.Visible = False
                                            btnBrief.Visible = True
                                            FillDetails()
                                            If rights <> "R" Then

                                                ddlClient.Enabled = False
                                                ddlContactPerson.Enabled = False
                                                txtLeadName.Enabled = False
                                                ddlActivity.Enabled = False
                                                chklActivityType.Enabled = False
                                                txtLeadBrief.Enabled = False
                                                txtBudget.Enabled = False
                                                txtExecution.Enabled = False
                                                txtClosureDateTime.Enabled = False
                                                txtClosureProbability.Enabled = False
                                                ddlLeadStatus.Enabled = False
                                                txtRemarks.Enabled = False

                                                btnAdd.Enabled = False
                                                btnBrief.Enabled = False

                                                btnEdit.Enabled = False



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
                                getleadhistory()
                            ElseIf hdnMode.Value = "add" Then
                                Naction.Enabled = False
                                If rights = "R" Then
                                    btnAdd.Visible = True
                                    btnEdit.Visible = False
                                    btnBrief.Visible = False
                                    FillCurrentDate()
                                    txtClosureDateTime.Visible = True
                                Else

                                    CallDivError()
                                End If

                            Else
                                CallDivError()
                            End If
                            If Len(Request.QueryString("ur")) > 0 Then
                                If Request.QueryString("ur") <> Nothing Then
                                    If Request.QueryString("ur").ToString() <> "" Then
                                        btnCancelHome.Visible = True
                                        ' btnCancel.Visible = False
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
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

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
            ddlClient.Items.Insert(ddlClient.Items.Count, New ListItem("Non Existing (New Client)", "9999999"))
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Function FillClient1(ByVal name As String) As String
        Dim jsonstring As String = ""
        Try
            ddlClient.DataSource = Nothing
            ddlClient.DataBind()
            Dim sql As String = "Select ClientID,Client from APEX_Clients where IsActive='Y' and IsDeleted='N' Order By Client"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                jsonstring = GetJSONString(ds.Tables(0))
                Return jsonstring
            End If

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Function

    Private Function CheckforBusinessRights() As String
        Dim result As String = ""
        Try
            Dim sql As String = ""
            sql = "Select role from dbo.APEX_UsersDetails where UserDetailsID=" & getLoggedUserID()
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("role")) Then
                    result = ds.Tables(0).Rows(0)("role").ToString()
                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function
    Private Sub FillContactPerson()
        Try
            Dim sql As String = "Select * from APEX_ClientContacts where IsActive='Y' and IsDeleted='N' and RefClientID='" & ddlClient.SelectedItem.Value & "' Order By ContactName"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlContactPerson.DataSource = ds
                ddlContactPerson.DataTextField = "ContactName"
                ddlContactPerson.DataValueField = "ContactID"
                ddlContactPerson.DataBind()
            End If

            ddlContactPerson.Items.Insert(0, New ListItem("Select", "0"))
            'ddlContactPerson.Items.Insert(ddlContactPerson.Items.Count, New ListItem("Non Existing (New Contact Person)", "99999"))
            ddlContactPerson.Items.Insert(ddlContactPerson.Items.Count, New ListItem("Add new", "Add new"))


        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillActivityType()
        Try
            Dim sql As String = "Select * from APEX_ActivityType where IsActive='Y' and IsDeleted='N' Order By ProjectType"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                chklActivityType.DataSource = ds
                chklActivityType.DataTextField = "ProjectType"
                chklActivityType.DataValueField = "ProjectTypeID"
                chklActivityType.DataBind()

                'chklActivityType.SelectedItem.Attributes.Add("disabled", "true")

                ddlActivity.DataSource = ds
                ddlActivity.DataTextField = "ProjectType"
                ddlActivity.DataValueField = "ProjectTypeID"
                ddlActivity.DataBind()





            End If


            ddlActivity.Items.Insert(0, New ListItem("Select", "0"))
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillLeadsStatus()
        Try
            ddlLeadStatus.Items.Clear()

            Dim itemValues As Array = System.Enum.GetValues(GetType(LeadsStatus))
            Dim itemNames As Array = System.Enum.GetNames(GetType(LeadsStatus))

            For i As Integer = 0 To itemNames.Length - 1
                Dim item As New ListItem(itemNames(i), itemValues(i))
                ddlLeadStatus.Items.Add(item)
            Next

            ddlLeadStatus.Items.Insert(0, New ListItem("Select", "0"))
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            'If CheckBriefCompleted() = False Then
            '    Response.Redirect("Leads.aspx?mode=pl")
            'Else
            '    Response.Redirect("Leads.aspx?mode=cl")
            'End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillDetails()
        Try
            FillClient()
            Dim clientType As Integer
            Dim activityType() As String
            Dim leadStatus As String

            Dim sql As String = "Select convert(varchar(10),l.ClosureByDateTime,105) as ClosureByDateTime,l.*,a.ProjectType "
            sql &= " from APEX_Leads as l"
            sql &= " left outer join APEX_ActivityType as a on l.PrimaryActivityID=a.ProjectTypeID "
            sql &= "  where LeadID= " & hdnLeadsID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)


            If ds.Tables(0).Rows.Count > 0 Then
                ddlClient.SelectedIndex = ddlClient.Items.IndexOf(ddlClient.Items.FindByValue(ds.Tables(0).Rows(0)("RefClientID").ToString()))
                'ddlClient.SelectedValue = ds.Tables(0).Rows(0)("RefClientID")
                clientid.Value = ddlClient.SelectedValue
                lblClient.Text = ddlClient.SelectedItem.Text
                If ds.Tables(0).Rows(0)("ClientType").ToString() = "O" Then
                    clientType = 1
                Else
                    clientType = 2
                End If
                FillContactPerson()
                ddlContactPerson.SelectedIndex = ddlContactPerson.Items.IndexOf(ddlContactPerson.Items.FindByValue(ds.Tables(0).Rows(0)("RefClientContactID").ToString()))
                hdnContactPersonID.Value = ds.Tables(0).Rows(0)("RefClientContactID").ToString()
                'ddlContactPerson.SelectedItem.Text = ds.Tables(0).Rows(0)("RefClientContactID").ToString()
                Dim capex As New clsApex
                Dim res() As String
                res = capex.FillContactInfo(ddlContactPerson.SelectedItem.Value)
                If res(0) <> "" Then
                    lblCEmail.Text = "Email: " & res(0)
                End If
                If res(1) <> "" Then
                    lblCContact.Text = "Contact: " & res(1)
                End If


                lblContactPerson.Text = ddlContactPerson.SelectedItem.Text
                lblActivity.Text = ds.Tables(0).Rows(0)("ProjectType").ToString()

                txtLeadName.Text = ds.Tables(0).Rows(0)("LeadName").ToString()
                lblLeadName.Text = ds.Tables(0).Rows(0)("LeadName").ToString()

                activityType = ds.Tables(0).Rows(0)("RefActivityTypeID").ToString().Split("|")


                lblActivityType.Text = ds.Tables(0).Rows(0)("RefActivityTypeID").ToString()
                hdnclientID.Value = ds.Tables(0).Rows(0)("RefClientID")
                If activityType.Count = 1 Then
                    If activityType(0) <> "" Then
                        For i As Integer = 0 To activityType.Count - 1
                            chklActivityType.SelectedIndex = chklActivityType.Items.IndexOf(chklActivityType.Items.FindByText(activityType(i)))
                        Next i
                    End If
                Else
                    For Each chk In activityType
                        chklActivityType.Items.FindByText(chk).Selected = True
                    Next
                End If

                txtLeadBrief.Text = ds.Tables(0).Rows(0)("LeadBrief").ToString()
                lblLeadBrief.Text = ds.Tables(0).Rows(0)("LeadBrief").ToString()

                txtBudget.Text = ds.Tables(0).Rows(0)("Budget").ToString()
                lblBudget.Text = ds.Tables(0).Rows(0)("Budget").ToString()

                txtExecution.Text = ds.Tables(0).Rows(0)("ExecutionMandays").ToString()
                lblExecution.Text = ds.Tables(0).Rows(0)("ExecutionMandays").ToString()

                If ds.Tables(0).Rows(0)("ClosureByDateTime").ToString() <> "" Then
                    txtClosureDateTime.Text = ds.Tables(0).Rows(0)("ClosureByDateTime").ToString()
                    lblClosureDateTime.Text = ds.Tables(0).Rows(0)("ClosureByDateTime").ToString()
                Else
                    txtClosureDateTime.Enabled = False
                    chkDateRequired.Checked = True
                    FillCurrentDate()
                End If

                lblClosureProbability.Text = ds.Tables(0).Rows(0)("ClosureProbability").ToString()
                leadStatus = ds.Tables(0).Rows(0)("LeadStatus").ToString()

                If Not IsDBNull(ds.Tables(0).Rows(0)("ClosureProbability")) Then
                    txtClosureProbability.Text = ds.Tables(0).Rows(0)("ClosureProbability").ToString()
                End If


                ddlLeadStatus.SelectedIndex = ddlLeadStatus.Items.IndexOf(ddlLeadStatus.Items.FindByText(leadStatus.ToString()))
                lblLeadStatus.Text = ddlLeadStatus.SelectedItem.Text

                ' ddlContact.SelectedIndex = ddlContact.Items.IndexOf(ddlContact.Items.FindByValue(ds.Tables(0).Rows(0)("RefUserDetailID").ToString()))
                Dim clsapx As New clsApex
                If Not IsDBNull(ds.Tables(0).Rows(0)("RefUserDetailID")) Then
                    lblContact.Text = clsapx.GetKeyStoneContacts(ds.Tables(0).Rows(0)("RefUserDetailID"))
                End If

                txtRemarks.Text = ds.Tables(0).Rows(0)("Remarks").ToString()
                lblRemarks.Text = ds.Tables(0).Rows(0)("Remarks").ToString()

                ddlActivity.SelectedIndex = ddlActivity.Items.IndexOf(ddlActivity.Items.FindByValue(ds.Tables(0).Rows(0)("PrimaryActivityID").ToString()))
                For i = 0 To chklActivityType.Items.Count - 1
                    If chklActivityType.Items(i).Selected = True Then
                        ddlActivity.Enabled = False
                        chklActivityType.Items(i).Enabled = False
                    Else
                        chklActivityType.Items(i).Enabled = True
                    End If

                Next i
                'If ds.Tables(0).Rows(0)("IsBriefed").ToString() = "Y" Then
                '    CloseAllControls()
                '    btnEdit.Visible = False

                '    btnBrief.Visible = False
                'Else
                btnBrief.Visible = False
                lblClient.Visible = False

                lblContactPerson.Visible = False
                lblLeadName.Visible = False
                lblActivity.Visible = False
                lblActivityType.Visible = False
                lblLeadBrief.Visible = False
                lblBudget.Visible = False
                lblExecution.Visible = False
                lblClosureDateTime.Visible = False
                lblClosureProbability.Visible = False
                lblLeadStatus.Visible = False
                lblContact.Visible = True
                lblRemarks.Visible = False
                'End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Dim activity As String = ""
            Dim LeadStatus As String = ""
            Dim ClosureDate As String = ""

            If ddlActivity.SelectedIndex > 0 Then
                activity = ddlActivity.SelectedItem.Text & "|"
            End If

            For i As Integer = 0 To chklActivityType.Items.Count - 1
                If chklActivityType.Items(i).Selected = True Then
                    If chklActivityType.Items(i).Text <> ddlActivity.SelectedItem.Text Then

                        activity &= chklActivityType.Items(i).Text & "|"
                    End If
                End If

            Next


            If activity <> "" Then

                If activity.Length = 0 Then
                    activity = "NULL"
                Else
                    activity = activity.Substring(0, activity.Length - 1)
                End If
                If chkDateRequired.Checked = False Then
                    ClosureDate = "convert(datetime,'" & txtClosureDateTime.Text & "',105)"
                Else
                    ClosureDate = ""
                End If

                Dim sql As String = "Update APEX_Leads set RefClientID = '" & Clean(hdnclientID.Value)
                If hdnContactPersonID.Value <> "" Then
                    sql &= "',RefClientContactID='" & Clean(hdnContactPersonID.Value) & "'"
                Else
                    sql &= "',RefClientContactID = NULL"
                End If
                sql &= ",LeadName='" & Clean(txtLeadName.Text)

                If activity <> "" Then
                    sql &= "',RefActivityTypeID='" & Clean(activity) & "'"
                Else
                    sql &= "',RefActivityTypeID = NULL"
                End If

                sql &= ",LeadBrief='" & Clean(txtLeadBrief.Text) & "'"

                If txtBudget.Text <> "" Then
                    sql &= ",Budget=" & Clean(txtBudget.Text)
                Else
                    sql &= ",Budget= NULL"
                End If

                If txtExecution.Text <> "" Then
                    sql &= ",ExecutionMandays='" & Clean(txtExecution.Text) & "'"
                Else
                    sql &= ",ExecutionMandays=NULL"
                End If

                If ClosureDate <> "" Then
                    sql &= ",ClosureByDateTime=" & ClosureDate
                Else
                    sql &= ",ClosureByDateTime= NULL"
                End If

                If txtClosureProbability.Text <> "" Then
                    sql &= ",ClosureProbability='" & Clean(txtClosureProbability.Text) & "'"
                Else
                    sql &= ",ClosureProbability= NULL"
                End If

                If ddlLeadStatus.SelectedIndex > 0 Then
                    sql &= ",LeadStatus='" & Clean(ddlLeadStatus.SelectedItem.Text) & "'"
                Else
                    sql &= ",LeadStatus= NULL"
                End If


                sql &= ",RefUserDetailID='" & Clean(getLoggedUserID()) & "'"


                sql &= ",Remarks = '" & Clean(txtRemarks.Text) & "'"

                If ddlActivity.SelectedIndex > 0 Then
                    sql &= ",PrimaryActivityID=" & Clean(ddlActivity.SelectedItem.Value)
                Else
                    sql &= ",PrimaryActivityID=NULL"
                End If

                sql &= ",ModifiedBy='" & getLoggedUserID()
                sql &= "',ModifiedOn=getdate()"
                sql &= "where Leadid=" & hdnLeadsID.Value

                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("Leads.aspx")
                End If
            Else
                Response.Write("<script>alert('Please select atleast one Sub Activity')</script>")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim activity As String = ""
            Dim LeadStatus As String = ""
            Dim ClosureDate As String = ""

            If ddlActivity.SelectedIndex > 0 Then
                activity = ddlActivity.SelectedItem.Text & "|"
            End If

            For i As Integer = 0 To chklActivityType.Items.Count - 1
                If chklActivityType.Items(i).Selected = True Then
                    If chklActivityType.Items(i).Text <> ddlActivity.SelectedItem.Text Then

                        activity &= chklActivityType.Items(i).Text & "|"
                    End If
                End If

            Next
            If activity <> "" Then


                If chkDateRequired.Checked = False Then
                    ClosureDate = "convert(datetime,'" & txtClosureDateTime.Text & "',105)"
                Else
                    ClosureDate = ""
                End If

                Dim sql As String = "Insert into APEX_Leads (RefClientID,RefClientContactID,LeadName,RefActivityTypeID,LeadBrief,Budget,ExecutionMandays,ClosureByDateTime,ClosureProbability,LeadStatus,RefUserDetailID,Remarks,InsertedBy,PrimaryActivityID)"
                sql &= " Values(" & Clean(hdnclientID.Value)
                If hdnContactPersonID.Value <> "" Then
                    sql &= "," & Clean(hdnContactPersonID.Value)
                Else
                    sql &= ",NULL"
                End If
                sql &= ",'" & Clean(txtLeadName.Text.ToString().Replace("'", " ")) & "'"
                If activity.Count = 0 Then
                    sql &= ",NULL"
                ElseIf activity.Count = 1 Then
                    If activity(0) = "" Then
                        sql = ",NULL"
                    Else
                        sql &= ",'" & activity.Substring(0, activity.Length - 1) & "'"
                    End If
                Else
                    sql &= ",'" & activity.Substring(0, activity.Length - 1) & "'"
                End If


                sql &= ",'" & Clean(txtLeadBrief.Text) & "',"
                If txtBudget.Text <> "" Then
                    sql &= "'" & Clean(txtBudget.Text) & "'"
                Else
                    sql &= "NULL"
                End If
                If txtExecution.Text <> "" Then
                    sql &= ",'" & Clean(txtExecution.Text) & "'"
                Else
                    sql &= ",NULL"
                End If
                If ClosureDate <> "" Then
                    sql &= "," & ClosureDate & ","
                Else
                    sql &= ",NULL,"
                End If
                If txtClosureProbability.Text <> "" Then
                    sql &= Clean(txtClosureProbability.Text)
                Else
                    sql &= "NULL"
                End If
                If ddlLeadStatus.SelectedIndex > 0 Then
                    sql &= ",'" & Clean(ddlLeadStatus.SelectedItem.Text) & "'"
                Else
                    sql &= ",NULL"
                End If

                sql &= "," & getLoggedUserID()

                If txtRemarks.Text <> "" Then
                    sql &= ",'" & Clean(txtRemarks.Text) & "'"
                Else
                    sql &= ",NULL"
                End If



                sql &= "," & getLoggedUserID()

                If ddlActivity.SelectedIndex > 0 Then
                    sql &= "," & Clean(ddlActivity.SelectedItem.Value)
                Else
                    sql &= ",NULL"
                End If
                sql &= ")"

                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("Leads.aspx")
                End If
            Else
                Response.Write("<script>alert('Please select atleast one Sub Activity')</script>")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub ClearActivityChecks()
        Try
            For i As Integer = 0 To chklActivityType.Items.Count - 1
                chklActivityType.Items(i).Selected = False
            Next
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub


    Private Sub FillCurrentDate()
        Try
            Dim day As String = ""
            Dim month As String = ""

            If DateTime.Now.Date.Day < 10 Then
                day = "0" & DateTime.Now.Date.Day
            Else
                day = DateTime.Now.Date.Day
            End If

            If DateTime.Now.Month < 10 Then
                month = "0" & Date.Now.Month
            Else
                month = Date.Now.Month
            End If
            txtClosureDateTime.Text = day & "-" & month & "-" & DateTime.Now.Year
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnBrief_Click(sender As Object, e As EventArgs) Handles btnBrief.Click
        Try
            Dim RefLeadID As String = ""
            Dim PrimaryActivityID As String = ""
            Dim ActivityTypeID As String = ""
            Dim RefClientID As String = ""
            Dim Budget As String = ""
            Dim Lead As String = ""
            Dim RefContactPersonID = ""
            Dim leadSql As String = "Select * from APEX_Leads where LeadId=" & hdnLeadsID.Value
            Dim ds As New DataSet

            ds = ExecuteDataSet(leadSql)
            If ds.Tables(0).Rows.Count > 0 Then
                RefLeadID = hdnLeadsID.Value
                PrimaryActivityID = ds.Tables(0).Rows(0)("PrimaryActivityID").ToString()
                ActivityTypeID = ds.Tables(0).Rows(0)("RefActivityTypeID").ToString()
                RefClientID = ds.Tables(0).Rows(0)("RefClientID").ToString()
                Budget = ds.Tables(0).Rows(0)("Budget").ToString()
                Lead = ds.Tables(0).Rows(0)("LeadName").ToString()
                RefContactPersonID = ds.Tables(0).Rows(0)("RefClientContactID").ToString()
            End If


            Dim sql As String = "insert into APEX_Brief(RefLeadID,PrimaryActivityID,RefActivityTypeID,RefClientID,Budget,BriefName,RefContactPersonID,insertedBy) Values ("
            sql &= RefLeadID

            If PrimaryActivityID <> "" Then
                sql &= "," & Clean(PrimaryActivityID)
            Else
                sql &= ",NULL"
            End If

            If ActivityTypeID <> "" Then
                sql &= ",'" & Clean(ActivityTypeID) & "'"
            Else
                sql &= ",NULL"
            End If

            If RefClientID <> "" Then
                sql &= "," & Clean(RefClientID)
            Else
                sql &= ",NULL"
            End If

            If Budget <> "" Then
                sql &= "," & Clean(Budget)
            Else
                sql &= ",NULL"
            End If

            sql &= ",'" & Clean(Lead) & "'"

            If RefContactPersonID <> "" Then
                sql &= "," & RefContactPersonID
            Else
                sql &= ",NULL"
            End If
            sql &= "," & getLoggedUserID()
            sql &= ") Update APEX_Leads set IsBriefed='Y' where LeadID=" & RefLeadID
            If ExecuteNonQuery(sql) > 0 Then
                CloseAllControls()
                Dim capex As New clsApex
                Dim briefid As String = capex.GetBriefIDByLeadID(hdnLeadsID.Value)
                Response.Redirect("BriefManager.aspx?mode=edit&bid=" & briefid)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub CloseAllControls()
        Try
            ddlClient.Visible = False
            ddlContactPerson.Visible = False
            txtLeadName.Visible = False
            ddlActivity.Visible = False
            chklActivityType.Visible = False
            txtLeadBrief.Visible = False
            txtBudget.Visible = False
            txtExecution.Visible = False
            txtClosureDateTime.Visible = False
            chkDateRequired.Visible = False
            txtClosureProbability.Visible = False
            ddlLeadStatus.Visible = False
            ' ddlContact.Visible = False
            txtRemarks.Visible = False

            lblClient.Visible = True
            lblContactPerson.Visible = True
            lblLeadName.Visible = True
            lblActivity.Visible = True
            lblActivityType.Visible = True
            lblLeadBrief.Visible = True
            lblBudget.Visible = True
            lblExecution.Visible = True
            lblClosureDateTime.Visible = True
            lblClosureProbability.Visible = True
            lblLeadStatus.Visible = True
            lblContact.Visible = True
            lblRemarks.Visible = True
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Function CheckBriefCompleted() As Boolean
        Dim result As Boolean = True
        Try
            If hdnLeadsID.Value <> "" Then
                Dim sql As String = "Select * from APEX_Brief where RefLeadID=" & hdnLeadsID.Value

                Dim ds As New DataSet
                ds = ExecuteDataSet(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    result = True
                Else
                    result = False
                End If
            Else
                result = False
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

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
    Protected Sub ddlActivity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlActivity.SelectedIndexChanged
        Try
            If ddlActivity.SelectedIndex > 0 Then
                For i = 0 To chklActivityType.Items.Count - 1
                    chklActivityType.Items(i).Selected = False
                Next
                For i As Integer = 0 To chklActivityType.Items.Count - 1
                    If chklActivityType.Items(i).Selected = False Then
                        If chklActivityType.Items(i).Text = ddlActivity.SelectedItem.Text Then
                            chklActivityType.Items(i).Selected = True
                        End If
                        ' chklActivityType.SelectedValue = ddlActivity.SelectedValue
                    Else
                        chklActivityType.Items(i).Selected = True
                    End If
                Next
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancelHome_Click(sender As Object, e As EventArgs) Handles btnCancelHome.Click
        Try
            Response.Redirect("Home.aspx")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnaddactionstep_Click(sender As Object, e As EventArgs) Handles btnaddactionstep.Click
        Dim sql As String = ""

        sql &= "INSERT INTO [dbo].[Apex_LeadHistory]"
        sql &= "       ([RefLeadID]"
        sql &= "       ,[NextActionStep]"
        sql &= "       ,[ActionDate],Insertedby"
        sql &= "       )"
        sql &= " VALUES"
        sql &= "       (" & hdnLeadsID.Value & ","
        sql &= "       '" & txtnextactionStep.Text & "', "
        'sql &= "        '" & txtactiondate.Text & "'          )"
        sql &= "convert(datetime,'" & txtactiondate.Text & "',105),'" & getLoggedUserID() & "' )"
        sql &= ""
        sql &= ""

        If ExecuteNonQuery(sql) Then
            MessageDiv.Visible = True
            lblMsg.Text = "Your data has been saved"
            txtnextactionStep.Text = ""
            txtactiondate.Text = ""
            getleadhistory()
        End If

    End Sub

    Private Sub getleadhistory()

        Dim sql As String = "select NextActionStep,Convert(varchar(20),ActionDate,106)ActionDate from Apex_LeadHistory where RefLeadID=" & hdnLeadsID.Value & ""

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        gdvLeadHistory.DataSource = ds
        gdvLeadHistory.DataBind()

    End Sub

End Class