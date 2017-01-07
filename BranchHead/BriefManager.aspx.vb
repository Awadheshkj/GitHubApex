Imports clsMain
Imports clsDatabaseHelper
Imports System.Data
Imports System.IO
Imports clsApex

Partial Class BriefManager
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                btnCancelHome.Visible = False
                divError.Visible = False


                BindClientDropdown()
                BindddlPrimaryActivity()
                ddlContactPerson.Items.Insert(0, New ListItem("Select", "0"))
                If Len(Request.QueryString("mode")) > 0 Then
                    If Request.QueryString("mode") <> Nothing Then
                        If Request.QueryString("mode") <> "" Then
                            If Request.QueryString("mode") = "edit" Then
                                If Len(Request.QueryString("bid")) > 0 Then
                                    If Request.QueryString("bid") <> Nothing Then
                                        If Request.QueryString("bid") <> "" Then
                                            Dim capex As New clsApex
                                            If Len(Request.QueryString("jid")) > 0 Then
                                                If Request.QueryString("jid") <> "" Then
                                                    hdnBriefID.Value = capex.GetBriefIDByJobCardID(Request.QueryString("jid"))
                                                Else
                                                    hdnBriefID.Value = Request.QueryString("bid")
                                                End If
                                            Else
                                                hdnBriefID.Value = Request.QueryString("bid")
                                            End If

                                            'hdnBriefID.Value = Request.QueryString("bid")
                                            BindBriefData(hdnBriefID.Value)
                                            If checkBriefed() = False Then
                                                closeAddlable()
                                            Else

                                                If IsjobCardCompleted(hdnBriefID.Value) = True Then

                                                    'CloseAllControls()
                                                    'btnEdit.Visible = False
                                                    'btnAdd.Visible = False
                                                    closeAddlable()

                                                Else
                                                    closeAddlable()
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
                                btnNext.Visible = True
                            ElseIf Request.QueryString("mode") = "add" Then

                                btnNext.Visible = False
                                btnEdit.Visible = False

                                lblBrief.Visible = False
                                lblActivity.Visible = False
                                lblActivity.Visible = False
                                lblActivityDate.Visible = False
                                lblClient.Visible = False
                                lblContactPerson.Visible = False
                                lblScope.Visible = False
                                lblTargetAudience.Visible = False
                                lblMeasurementMatrix.Visible = False
                                lblActivityDetails.Visible = False
                                lblKeyChallenges.Visible = False
                                lblTimeline.Visible = False
                                lblBudget.Visible = False
                            Else
                                CallDivError()
                            End If
                            If Len(Request.QueryString("ur")) > 0 Then
                                If Request.QueryString("ur") <> Nothing Then
                                    If Request.QueryString("ur").ToString() <> "" Then
                                        btnCancelHome.Visible = True
                                        btnCancel.Visible = False
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

    Private Sub closeAddlable()
        btnAdd.Visible = False
        btnNext.Visible = False
        lblBrief.Visible = False
        lblActivity.Visible = False
        lblActivity.Visible = False
        lblActivityDate.Visible = False
        lblClient.Visible = False
        lblContactPerson.Visible = False
        lblScope.Visible = False
        lblTargetAudience.Visible = False
        lblMeasurementMatrix.Visible = False
        lblActivityDetails.Visible = False
        lblKeyChallenges.Visible = False
        lblTimeline.Visible = False
        lblBudget.Visible = False
        lblchklActivityType.Visible = False
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            InsertBrief()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            UpdateBriefDetails()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try

    End Sub

    Private Sub InsertPrePnLInfo()
        Try
            Dim clientid As String = ""

            Dim sql1 As String = "Select RefClientID From APEX_Brief where BriefID=" & hdnBriefID.Value
            Dim ds1 As New DataSet
            ds1 = ExecuteDataSet(sql1)
            If ds1.Tables(0).Rows.Count > 0 Then
                clientid = ds1.Tables(0).Rows(0)(0).ToString()
            End If
            If checkBriefIDInPrepnl(hdnBriefID.Value) <> False Then
                Dim sql As String = "INSERT INTO APEX_PrePnL("
                sql &= "RefBriefID"
                sql &= ",RefClientID"
                sql &= ",InsertedBy)VALUES"
                sql &= "("
                sql &= hdnBriefID.Value
                sql &= "," & clientid
                sql &= "," & getLoggedUserID() & ")"
                ExecuteNonQuery(sql)
            End If

            Dim capex As New clsApex
            ' capex.UpdateStageLevel("4", hdnJobCardID.Value)
            ' Response.Redirect("PrePnlManager.aspx?bid=" & hdnBrief.Value)
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindClientDropdown()
        Try
            Dim SqlddlString As String = ""
            Dim ds As New DataSet

            SqlddlString = "Select ClientID,Client from APEX_Clients where IsActive='Y' and IsDeleted='N' Order By Client"
            ds = ExecuteDataSet(SqlddlString)
            Dim LastVal As Integer = ds.Tables(0).Rows.Count + 1

            If ds.Tables(0).Rows.Count > 0 Then
                ddlClient.DataSource = ds
                ddlClient.DataTextField = "Client"
                ddlClient.DataValueField = "ClientID"
                ddlClient.DataBind()
                ddlClient.Items.Insert(0, New ListItem("Select", "0"))
                ddlClient.Items.Insert(LastVal, New ListItem("Non Existing(New Client)", "9999999"))
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

            sqlddlCPstring = "Select ContactID,ContactName from APEX_ClientContacts where RefClientID=" & ddlClient.SelectedItem.Value & " and IsActive='Y' and IsDeleted='N' Order By ContactName"
            ds = ExecuteDataSet(sqlddlCPstring)

            Dim LastValue As Integer = ds.Tables(0).Rows.Count + 1
            If ds.Tables(0).Rows.Count > 0 Then
                ddlContactPerson.DataSource = ds
                ddlContactPerson.DataTextField = "ContactName"
                ddlContactPerson.DataValueField = "ContactID"
                ddlContactPerson.DataBind()

            End If

            ddlContactPerson.Items.Insert(0, New ListItem("Select", "0"))
            ddlContactPerson.Items.Insert(LastValue, New ListItem("Add new", "Add new"))
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindddlPrimaryActivity()
        Try
            Dim sqlddlPrimaryActivitystring As String = ""
            Dim ds As New DataSet

            sqlddlPrimaryActivitystring = "Select ProjectTypeID,ProjectType from APEX_ActivityType Where IsActive='Y' and IsDeleted='N' Order By ProjectType"
            ds = ExecuteDataSet(sqlddlPrimaryActivitystring)

            If ds.Tables(0).Rows.Count > 0 Then
                ddlActivity.DataSource = ds
                ddlActivity.DataTextField = "ProjectType"
                ddlActivity.DataValueField = "ProjectTypeID"
                ddlActivity.DataBind()
                ddlActivity.Items.Insert(0, New ListItem("Select", "0"))

                chklActivityType.DataSource = ds
                chklActivityType.DataTextField = "ProjectType"
                chklActivityType.DataValueField = "ProjectTypeID"
                chklActivityType.DataBind()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub InsertBrief()
        Try
            Dim sqlInsertString As String = ""
            Dim ActivityDate As String = ""

            Dim activity As String = ""


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

                ActivityDate = "Convert(datetime,'" & txtActivityDate.Text & "',105)"

                If checkDuplicateBriefName() = False Then

                    sqlInsertString &= "INSERT INTO [APEX_Brief]"
                    sqlInsertString &= "           ([BriefName]"
                    sqlInsertString &= "           ,[PrimaryActivityID]"
                    sqlInsertString &= "           ,[RefActivityTypeID]"
                    sqlInsertString &= "           ,[RefClientID]"
                    sqlInsertString &= "           ,[RefContactPersonID]"
                    sqlInsertString &= "           ,[ActivityDate]"
                    sqlInsertString &= "           ,[ScopeOfWork]"
                    sqlInsertString &= "           ,[MeasurementMatrix]"
                    sqlInsertString &= "           ,[TargetAudience]"
                    sqlInsertString &= "           ,[ActivityDetails]"
                    sqlInsertString &= "           ,[KeyChallangesForExecution]"
                    sqlInsertString &= "           ,[TimelineForRevert]"
                    sqlInsertString &= "           ,[Budget]"
                    sqlInsertString &= "           ,[InsertedBy]"
                    sqlInsertString &= "           )"
                    sqlInsertString &= "     VALUES"
                    sqlInsertString &= "           ("
                    sqlInsertString &= "      '" & Clean(txtBrief.Text) & "',     "
                    sqlInsertString &= " '" & ddlActivity.SelectedValue & "', "
                    If activity <> "" Then
                        activity = activity.Substring(0, activity.Length - 1)
                        sqlInsertString &= " '" & activity & "',"
                    Else
                        sqlInsertString &= "NULL,"
                    End If

                    sqlInsertString &= " '" & ddlClient.SelectedValue & "',  "
                    sqlInsertString &= " '" & hdnContactPersonID.Value & "',  "
                    sqlInsertString &= "  " & ActivityDate & ", "
                    sqlInsertString &= " '" & Clean(txtScopeOfWork.Text) & "',  "
                    sqlInsertString &= "  '" & Clean(txtMeasurementMatrix.Text) & "', "
                    sqlInsertString &= " '" & Clean(txtTargetAudience.Text) & "', "
                    sqlInsertString &= " '" & Clean(txtActivityDetails.Text) & "', "
                    sqlInsertString &= " '" & Clean(txtKeyChallenges.Text) & "',  "
                    sqlInsertString &= " '" & Clean(txtTimeline.Text) & "', "
                    sqlInsertString &= " '" & Clean(txtBudget.Text) & "' ,"
                    sqlInsertString &= " '" & getLoggedUserID() & "'"
                    sqlInsertString &= ")"

                    If ExecuteNonQuery(sqlInsertString) > 0 Then
                        GetBriefIDByBriefName()
                        Dim capex As New clsApex
                        Dim jcard As String = capex.GetJobCardIDByBriefID(hdnBriefID.Value)
                        InsertPrePnLInfo()
                        If jcard = "" Then
                            InsertJobCard()
                        Else
                            UpdateJobCard()

                            Response.Redirect("BriefCollateral.aspx?bid=" & hdnBriefID.Value)
                        End If
                    End If
                Else
                    lblError.Text = "Brief Name already exists."
                    divError.Visible = True

                End If
            Else
                Response.Write("<script>alert('Please select atleast one Sub Activity')</script>")
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
                    txtBrief.Text = ds.Tables(0).Rows(0)("BriefName").ToString()
                    lblBrief.Text = ds.Tables(0).Rows(0)("BriefName").ToString()
                Else
                    txtBrief.Text = ""
                End If
                If ds.Tables(0).Rows(0)("PrimaryActivityID").ToString() <> "" Then
                    ddlActivity.SelectedIndex = ddlActivity.Items.IndexOf(ddlActivity.Items.FindByValue(ds.Tables(0).Rows(0)("PrimaryActivityID").ToString()))
                    lblActivity.Text = ddlActivity.SelectedItem.Text
                Else
                    ddlActivity.SelectedIndex = 0
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
                                chklActivityType.Items(chklActivityType.Items.IndexOf(chklActivityType.Items.FindByText(activityType(i)))).Selected = True
                            End If
                        Next i

                    End If
                End If

                If ds.Tables(0).Rows(0)("NActivityDate").ToString() <> "" Then
                    txtActivityDate.Text = ds.Tables(0).Rows(0)("NActivityDate").ToString()
                    lblActivityDate.Text = ds.Tables(0).Rows(0)("NActivityDate").ToString()
                Else
                    txtActivityDate.Text = ""
                End If

                If ds.Tables(0).Rows(0)("RefClientID").ToString() <> "" Then
                    ddlClient.SelectedIndex = ddlClient.Items.IndexOf(ddlClient.Items.FindByValue(ds.Tables(0).Rows(0)("RefClientID").ToString()))
                    lblClient.Text = ddlClient.SelectedItem.Text
                Else
                    ddlClient.SelectedIndex = 0
                End If

                BindContactPersonDropdownEdit()
                ddlContactPerson.SelectedIndex = ddlContactPerson.Items.IndexOf(ddlContactPerson.Items.FindByValue(ds.Tables(0).Rows(0)("RefContactPersonID").ToString()))
                hdnContactPersonID.Value = ds.Tables(0).Rows(0)("RefContactPersonID").ToString()
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
                    txtScopeOfWork.Text = ds.Tables(0).Rows(0)("ScopeOfwork").ToString()
                    lblScope.Text = ds.Tables(0).Rows(0)("ScopeOfwork").ToString()
                Else
                    txtScopeOfWork.Text = ""
                End If

                If ds.Tables(0).Rows(0)("TargetAudience").ToString() <> "" Then
                    txtTargetAudience.Text = ds.Tables(0).Rows(0)("TargetAudience").ToString()
                    lblTargetAudience.Text = ds.Tables(0).Rows(0)("TargetAudience").ToString()
                Else
                    txtTargetAudience.Text = ""
                End If

                If ds.Tables(0).Rows(0)("MeasurementMatrix").ToString() <> "" Then
                    txtMeasurementMatrix.Text = ds.Tables(0).Rows(0)("MeasurementMatrix").ToString()
                    lblMeasurementMatrix.Text = ds.Tables(0).Rows(0)("MeasurementMatrix").ToString()
                Else
                    txtMeasurementMatrix.Text = ""
                End If

                If ds.Tables(0).Rows(0)("ActivityDetails").ToString() <> "" Then
                    txtActivityDetails.Text = ds.Tables(0).Rows(0)("ActivityDetails").ToString()
                    lblActivityDetails.Text = ds.Tables(0).Rows(0)("ActivityDetails").ToString()
                Else
                    txtActivityDetails.Text = ""
                End If

                If ds.Tables(0).Rows(0)("KeyChallangesForExecution").ToString() <> "" Then
                    txtKeyChallenges.Text = ds.Tables(0).Rows(0)("KeyChallangesForExecution").ToString()
                    lblKeyChallenges.Text = ds.Tables(0).Rows(0)("KeyChallangesForExecution").ToString()
                Else
                    txtKeyChallenges.Text = ""
                End If

                If ds.Tables(0).Rows(0)("TimelineForRevert").ToString() <> "" Then
                    txtTimeline.Text = ds.Tables(0).Rows(0)("TimelineForRevert").ToString()
                    lblTimeline.Text = ds.Tables(0).Rows(0)("TimelineForRevert").ToString()
                Else
                    txtTimeline.Text = ""
                End If

                If ds.Tables(0).Rows(0)("Budget").ToString() <> "" Then
                    txtBudget.Text = ds.Tables(0).Rows(0)("Budget").ToString()
                    lblBudget.Text = ds.Tables(0).Rows(0)("Budget").ToString()
                Else
                    txtBudget.Text = ""
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub UpdateBriefDetails()
        Try
            Dim activity As String = ""

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

            Dim sqlUpdateBriefString As String = ""
            Dim ActivityDate As String = "Convert(datetime,'" & txtActivityDate.Text & "',105)"
            If activity <> "" Then
                sqlUpdateBriefString &= "UPDATE [APEX_Brief]"
                sqlUpdateBriefString &= "   SET [BriefName] = '" & Clean(txtBrief.Text) & "'"
                sqlUpdateBriefString &= "      ,[PrimaryActivityID] = '" & ddlActivity.SelectedValue & "'    "
                If activity <> "" Then
                    sqlUpdateBriefString &= "      ,[RefActivityTypeID] = '" & activity.Substring(0, activity.Length - 1) & "'    "
                Else
                    sqlUpdateBriefString &= "      ,[RefActivityTypeID] = NULL"
                End If
                sqlUpdateBriefString &= "      ,[RefClientID]=" & ddlClient.SelectedValue
                sqlUpdateBriefString &= "      ,[RefContactPersonID] =" & hdnContactPersonID.Value
                sqlUpdateBriefString &= "      ,[ActivityDate] = " & ActivityDate
                sqlUpdateBriefString &= "      ,[ScopeOfWork] = '" & Clean(txtScopeOfWork.Text) & "'"
                sqlUpdateBriefString &= "      ,[MeasurementMatrix] = '" & Clean(txtMeasurementMatrix.Text) & "'"
                sqlUpdateBriefString &= "      ,[TargetAudience] = '" & Clean(txtTargetAudience.Text) & "'"
                sqlUpdateBriefString &= "      ,[ActivityDetails] = '" & Clean(txtActivityDetails.Text) & "'"
                sqlUpdateBriefString &= "      ,[KeyChallangesForExecution] = '" & Clean(txtKeyChallenges.Text) & "'"
                sqlUpdateBriefString &= "      ,[TimelineForRevert] = '" & Clean(txtTimeline.Text) & "'"
                sqlUpdateBriefString &= "      ,[Budget] =" & Clean(txtBudget.Text)
                sqlUpdateBriefString &= " WHERE BriefID=" & hdnBriefID.Value

                If ExecuteNonQuery(sqlUpdateBriefString) > 0 Then
                    InsertPrePnLInfo()
                    Dim sqlcheck As String = "Select * from APEX_JobCard where RefBriefID=" & hdnBriefID.Value
                    Dim ds As New DataSet
                    ds = ExecuteDataSet(sqlcheck)
                    If ds.Tables(0).Rows.Count > 0 Then
                        UpdateJobCard()
                        Response.Redirect("leads.aspx")
                    Else
                        InsertJobCard()
                        Response.Redirect("BriefCollateral.aspx?bid=" & hdnBriefID.Value)
                    End If


                End If
            Else
                Response.Write("<script>alert('Please select atleast one Sub Activity')</script>")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub CloseAllControls()
        Try
            txtBrief.Visible = False
            ddlActivity.Visible = False
            chklActivityType.Visible = False
            txtActivityDate.Visible = False
            ddlClient.Visible = False
            ddlContactPerson.Visible = False
            txtScopeOfWork.Visible = False
            txtTargetAudience.Visible = False
            txtMeasurementMatrix.Visible = False
            txtActivityDetails.Visible = False
            txtKeyChallenges.Visible = False
            txtTimeline.Visible = False
            txtBudget.Visible = False

            lblBrief.Visible = True
            lblActivity.Visible = True
            lblActivity.Visible = True
            lblActivityDate.Visible = True
            lblClient.Visible = True
            lblContactPerson.Visible = True
            lblScope.Visible = True
            lblTargetAudience.Visible = True
            lblMeasurementMatrix.Visible = True
            lblActivityDetails.Visible = True
            lblKeyChallenges.Visible = True
            lblTimeline.Visible = True
            lblBudget.Visible = True
            lblchklActivityType.Visible = True
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Dim capex As New clsApex
            If hdnBriefID.Value <> "" Then
                Dim leadid As String = capex.GetLeadIDByBriefID(hdnBriefID.Value)
                If leadid <> "" Then
                    Response.Redirect("Leads.aspx?mode=cl")
                Else
                    Response.Redirect("Leads.aspx?mode=db")
                End If
            Else
                Response.Redirect("Leads.aspx?mode=db")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub InsertJobCard()
        Try
            Dim activity As String = ""
            For i As Integer = 0 To chklActivityType.Items.Count - 1
                If chklActivityType.Items(i).Selected = True Then
                    activity &= chklActivityType.Items(i).Text & "|"
                End If

            Next
            ' hdnBriefID.Value = GetBriefID()
            Dim sql As String = "Insert into APEX_JobCard (JobCardName"
            sql &= ",RefBriefID"
            sql &= ",PrimaryActivityID"
            sql &= ",RefActivityID"
            sql &= ",RefClientID"
            sql &= ",ClientContactPerson "
            sql &= ",IsBriefed) "
            sql &= "values ('" & Clean(txtBrief.Text) & "'"
            sql &= "," & hdnBriefID.Value
            sql &= "," & ddlActivity.SelectedValue
            If activity <> "" Then
                activity = activity.Substring(0, activity.Length - 1)
                sql &= ",'" & activity & "'"
            Else
                sql &= ",NULL"
            End If
            sql &= "," & ddlClient.SelectedValue
            sql &= "," & hdnContactPersonID.Value & ""
            sql &= ",'Y')"
            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("BriefCollateral.aspx?bid=" & hdnBriefID.Value)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub UpdateJobCard()
        Try
            Dim activity As String = ""
            For i As Integer = 0 To chklActivityType.Items.Count - 1
                If chklActivityType.Items(i).Selected = True Then
                    activity &= chklActivityType.Items(i).Text & "|"
                End If

            Next

            Dim sql As String = "Update APEX_JobCard "
            sql &= " set JobCardName='" & Clean(txtBrief.Text) & "'"
            sql &= ",PrimaryActivityID=" & ddlActivity.SelectedValue
            If activity <> "" Then
                sql &= ",RefActivityID= '" & activity & "'"
            Else
                sql &= ",RefActivityID=NULL"
            End If
            sql &= ",IsBriefed='Y'"
            sql &= ",RefClientID=" & ddlClient.SelectedValue
            sql &= ",ClientContactPerson=" & hdnContactPersonID.Value
            sql &= " where RefBriefID=" & hdnBriefID.Value
            If ExecuteNonQuery(sql) > 0 Then
                'Response.Redirect("BriefCollateral.aspx?bid=" & hdnBriefID.Value)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function GetBriefID() As String
        Dim result As String = ""
        Try
            Dim sql As String = "select BriefID from APEX_Brief where BriefName='" & Clean(txtBrief.Text) & "'"
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

    Private Function checkDuplicateBriefName() As Boolean
        Dim result As Boolean = False
        Try
            Dim sql As String = "Select * from Apex_Brief where BriefName='" & Clean(txtBrief.Text) & "'"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = True
            Else
                result = False
                divError.Visible = False
                lblError.Text = ""
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Private Function checkBriefed() As Boolean
        Dim result As Boolean = True
        Try
            Dim sql As String = "Select IsBriefed from APEX_JobCard where RefBriefID=" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)(0).ToString() = "Y" Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            Response.Redirect("BriefCollateral.aspx?bid=" & hdnBriefID.Value)
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

    Private Sub GetBriefIDByBriefName()
        Try
            Dim sql As String = "select * from APEX_Brief where BriefName='" & Clean(txtBrief.Text) & "'"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                hdnBriefID.Value = ds.Tables(0).Rows(0)("BriefID").ToString()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function checkBriefIDInPrepnl(ByVal bid As String) As Boolean
        Dim flag As Boolean = True
        Try
            Dim sql As String = "Select count(*) from dbo.APEX_PrePnL where RefBriefid= " & bid
            Dim count As Integer = ExecuteSingleResult(sql, _DataType.Numeric)
            If count > 0 Then
                flag = False
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return flag
    End Function

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
                        Else

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
            If Request.QueryString("ur").ToString() = "hm" Then
                Response.Redirect("Home.aspx?mode=KAM1")
            ElseIf Request.QueryString("ur").ToString() = "hm1" Then
                Response.Redirect("Home.aspx?mode=KAM2")
            End If

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

End Class
