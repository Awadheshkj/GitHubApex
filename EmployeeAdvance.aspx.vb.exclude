Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports System.Data.SqlClient

Partial Class EmployeeAdvance
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Page.IsPostBack Then
                divError.Visible = False

                If Len(Request.QueryString("taid")) > 0 Then
                    If Request.QueryString("taid").ToString() <> Nothing Then
                        hdnTaskID.Value = Request.QueryString("taid").ToString()
                        If Len(Request.QueryString("sid")) > 0 Then
                            If Request.QueryString("sid").ToString() <> Nothing Then
                                hdnSubTaskID.Value = Request.QueryString("sid").ToString()
                            End If
                        End If
                        'fillClaims(-1)
                        ' BindGrid()
                        fillDetails()
                        'fillClaims()
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

    Private Sub fillDetails()

        'Dim sql As String = "Select t.title,ta.particulars,ta.Amount from APEX_TAskAccount as ta "
        'sql &= " join APEX_Task as t on ta.refTaskID=t.taskID where ta.AccountID=" & hdnTaskID.Value
        Dim sql As String = ""
        sql &= " select City,category,ta.taskname title,ta.particulars,isnull(ta.total,0)Amount,(UD.FirstNAme + ' ' + UD.LastName)EMPName,EMPcode,JobCardName EventName,convert(varchar(20),ActivityDate,106)EventDate,CL.client,(select (FirstNAme + ' ' + LastName) from Apex_usersdetails where UserDetailsID = JC.ProjectManagerID) PMName from APEX_TAskAccount TA"
        sql &= " inner join Apex_usersdetails UD on TA.TL=UD.UserDetailsID "
        'sql &= " inner join APEX_Task as t on ta.refTaskID=t.taskID"
        sql &= " inner join apex_jobcard as JC on ta.refjobcardID=jc.jobcardID"
        ' sql &= " inner join apex_prepnl as PP on JC.RefBriefID=pp.refbriefID"
        sql &= " inner join APEX_Brief as B on jc.RefBriefID =B.BriefID "
        sql &= " inner join apex_clients as CL on B.RefClientID=CL.clientID "
        sql &= "  where ta.AccountID=" & hdnTaskID.Value
        sql &= ""

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            If Not IsDBNull(ds.Tables(0).Rows(0)("title")) Then
                lblTaskTitle.Text = ds.Tables(0).Rows(0)("title")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("particulars")) Then
                lblparticulars.Text = ds.Tables(0).Rows(0)("particulars")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Amount")) Then
                lblAmount.Text = ds.Tables(0).Rows(0)("Amount")
                txtbugetamountallocated.Text = ds.Tables(0).Rows(0)("Amount")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("PMName")) Then
                txtPmName.Text = ds.Tables(0).Rows(0)("PMName")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("EMPName")) Then
                txtEmployeeName.Text = ds.Tables(0).Rows(0)("EMPName")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("EMPcode")) Then
                txtempcode.Text = ds.Tables(0).Rows(0)("EMPcode")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("EventName")) Then
                txtEventName.Text = ds.Tables(0).Rows(0)("EventName")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("EventDate")) Then
                txteventDate.Text = ds.Tables(0).Rows(0)("EventDate")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("client")) Then
                txtclientName.Text = ds.Tables(0).Rows(0)("client")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("category")) Then
                lblexpensehead.Text = ds.Tables(0).Rows(0)("category")
            End If
            txteventCity.Text = ds.Tables(0).Rows(0)("City")
            txtKamName.Text = getKAMName()
        End If

        Dim jid As String = ""
        Dim capex As New clsApex
        Dim tid As String = getTaskIDbyAccountID(hdnTaskID.Value)
        'If tid <> "" Then
        '    jid = capex.GetJobCardByTaskID(tid)
        '    hdnJobCardID.Value = jid
        'End If
        'If hdnJobCardID.Value <> "" Then

        Dim sql2 As String = "Select JobCardNo  from APEX_JobCard where JobCardID = " & tid
        Dim ds2 As New DataSet
        ds2 = ExecuteDataSet(sql2)
        If ds2.Tables(0).Rows.Count > 0 Then
            lblJobCode.Text = ds2.Tables(0).Rows(0)(0).ToString()
            txtJCNo.Text = ds2.Tables(0).Rows(0)(0).ToString()
        End If
        'End If
    End Sub

    'Private Sub fillClaims(ByVal rows As Integer)

    '    Dim arr As New ArrayList
    '    For i As Integer = 0 To rows
    '        arr.Add(i)
    '    Next
    '    gvClaims.DataSource = arr
    '    gvClaims.DataBind()

    'End Sub

    'Protected Sub txtNOE_TextChanged(sender As Object, e As EventArgs) Handles txtNOE.TextChanged
    '    If IsNumeric(txtNOE.Text) Then
    '        fillClaims(txtNOE.Text - 1)
    '    Else

    '        fillClaims(-1)
    '    End If

    'End Sub

    Private Sub CallDivError()
        divContent.Visible = False
        divError.Visible = True
        Dim capex As New clsApex
        lblError.Text = capex.SetErrorMessage()
    End Sub
    Public Shared Function getTaskIDbyAccountID(ByVal AccuntID As Integer) As Integer
        Dim result As Integer = 0
        If AccuntID > 0 Then
            Dim sql As String = "Select RefjobcardID from APEX_TaskAccount where AccountID=" & AccuntID
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("RefjobcardID")) Then
                    result = ds.Tables(0).Rows(0)("RefjobcardID")
                End If
            End If
        End If
        Return result
    End Function

    'Private Sub BindGrid()
    '    Try
    '        Dim sql As String = ""
    '        sql &= "	select EAE.* from [dbo].[Apex_EmployeeAdvanceExpenses] EAE"
    '        sql &= "	inner join Apex_EmployeesAdvence EA on EAE.RefEMPID=EA.ID"
    '        sql &= "	where EA.refBriefID= " & getBriefID() & ""
    '        sql &= ""
    '        sql &= ""
    '        sql &= ""
    '        sql &= ""

    '        'sql = "Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row, * from Apex_PrePnLCost where RefBriefID=" & hdnBriefID.Value & " "
    '        'sql &= "  union "
    '        'sql &= "  Select ROW_NUMBER() OVER(ORDER BY PrePnLCostID DESC) AS Row,* from Apex_tempPrePnLCost where RefBriefID=" & hdnBriefID.Value & "  order by PrePnLCostID Asc"

    '        Dim ds As New DataSet
    '        ds = ExecuteDataSet(sql)
    '        gvClaims.DataSource = ds
    '        gvClaims.DataBind()

    '        If gvClaims.Rows.Count = 0 And Not gvClaims.DataSource Is Nothing Then
    '            Dim dt As Object = Nothing
    '            If gvClaims.DataSource.GetType Is GetType(Data.DataSet) Then
    '                dt = New System.Data.DataSet
    '                dt = CType(gvClaims.DataSource, System.Data.DataSet).Tables(0).Clone()
    '            ElseIf gvClaims.DataSource.GetType Is GetType(Data.DataTable) Then
    '                dt = New System.Data.DataTable
    '                dt = CType(gvClaims.DataSource, System.Data.DataTable).Clone()
    '            ElseIf gvClaims.DataSource.GetType Is GetType(Data.DataView) Then
    '                dt = New System.Data.DataView
    '                dt = CType(gvClaims.DataSource, System.Data.DataView).Table.Clone
    '            End If
    '            dt.Rows.Add(dt.NewRow())
    '            gvClaims.DataSource = dt
    '            gvClaims.DataBind()
    '            gvClaims.Rows(0).Visible = False
    '            gvClaims.Rows(0).Controls.Clear()
    '        End If
    '    Catch ex As Exception
    '        SendExMail(ex.Message, ex.ToString())
    '    End Try
    'End Sub

    Public Shared Sub SendExMail(ByVal exMsg As String, ByVal ex As String)
        Dim toid As String() = {"chander@kestone.in"}
        'Dim ccid As String = "ravi@technocats.in"
        For i As Integer = 0 To 2
            If i = 0 Then
                'sendMail(exMsg.ToString(), ex.ToString(), "", toid(i), ccid)
            Else
                'sendMail(exMsg.ToString(), ex.ToString(), "", toid(i), "")
            End If
        Next
    End Sub

    Private Function getKAMName() As String
        Dim bid As String = getBriefID()
        Dim result As String = ""
        Dim sql As String = "select isnull(FirstName,'') + isnull(LastName,'') as KAMName "
        sql &= " from APEX_UsersDetails as ud "
        sql &= " Inner join APEX_Brief as ab on ud.UserDetailsID = ab.InsertedBy "
        sql &= " where ab.BriefID = " & bid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)("KAMName").ToString()
        End If
        Return result
    End Function

    Private Function getBriefID() As String
        Dim bid As String = ""
        Dim jid As String = ""
        Dim tid As String = ""
        Dim capex As New clsApex

        If hdnTaskID.Value <> "" Then
            tid = getTaskIDbyAccountID(hdnTaskID.Value)
            jid = tid
            'jid = capex.GetJobCardByTaskID(tid)
            bid = capex.GetBriefIDByJobCardID(jid)
        End If

        Return bid
    End Function

    'Protected Sub gvClaims_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvClaims.RowCommand
    '    Try
    '        If e.CommandName = "Add" Then
    '            Dim totalquote As Double = 0
    '            Dim frow As GridViewRow = gvClaims.FooterRow
    '            Dim txtExpencesHead As New TextBox
    '            Dim txtAdvanceAmtReq As New TextBox
    '            txtExpencesHead = CType(frow.FindControl("txtParticular"), TextBox)
    '            txtAdvanceAmtReq = CType(frow.FindControl("txtadvanceamt"), TextBox)

    '            'Dim sqlparams(10) As SqlClient.SqlParameter
    '            'sqlparams(0) = New SqlClient.SqlParameter("@ID", 0)
    '            'sqlparams(1) = New SqlClient.SqlParameter("@RefBriefID", getBriefID())
    '            'sqlparams(2) = New SqlClient.SqlParameter("@AdvanceAmtRequest", txtadvanceAmount.Text)
    '            'sqlparams(3) = New SqlClient.SqlParameter("@PaymentToBeDone", txtpaymenttobedone.Text)
    '            'sqlparams(4) = New SqlClient.SqlParameter("@BudgetAmount", txtbugetamountallocated.Text)
    '            'sqlparams(5) = New SqlClient.SqlParameter("@ExpectedDateOfExpenses", txtexpecteddateofexpences.Text)
    '            'sqlparams(6) = New SqlClient.SqlParameter("@BranchHeadApproval", "")
    '            'sqlparams(7) = New SqlClient.SqlParameter("@BranchHeadApprovalDate", getDate())
    '            'sqlparams(8) = New SqlClient.SqlParameter("@ExpenseHead", txtExpencesHead.Text)
    '            'sqlparams(9) = New SqlClient.SqlParameter("@AdvanceAmtReq", txtAdvanceAmtReq.Text)
    '            'sqlparams(10) = New SqlClient.SqlParameter("@BudgetAmtAllocated", txtbugetamountallocated.Text)

    '            'If ExecuteStoredProcedure("SP_ADDEmployeesAdvance", sqlparams) > 0 Then
    '            '    BindGrid()
    '            'End If

    '            Dim constring As String = ConfigurationManager.ConnectionStrings("localdata2015-16Demo").ConnectionString
    '            Using con As New SqlConnection(constring)

    '                Using cmd As New SqlCommand("SP_ADDEmployeesAdvance", con)

    '                    cmd.CommandType = CommandType.StoredProcedure

    '                    cmd.Parameters.AddWithValue("@RefBriefID", getBriefID())
    '                    cmd.Parameters.AddWithValue("@AdvanceAmtRequest", txtadvanceAmount.Text)
    '                    cmd.Parameters.AddWithValue("@PaymentToBeDone", txtpaymenttobedone.Text)
    '                    cmd.Parameters.AddWithValue("@BudgetAmount", txtbugetamountallocated.Text)
    '                    cmd.Parameters.AddWithValue("@ExpectedDateOfExpenses", txtexpecteddateofexpences.Text)
    '                    cmd.Parameters.AddWithValue("@BranchHeadApproval", "")
    '                    cmd.Parameters.AddWithValue("@BranchHeadApprovalDate", getDate())
    '                    cmd.Parameters.AddWithValue("@ExpenseHead", txtExpencesHead.Text)
    '                    cmd.Parameters.AddWithValue("@AdvanceAmtReq", txtAdvanceAmtReq.Text)
    '                    cmd.Parameters.AddWithValue("@BudgetAmtAllocated", txtbugetamountallocated.Text)
    '                    cmd.Parameters.AddWithValue("@UpdateID", hdnEmpTableID.Value)

    '                    cmd.Parameters.Add("@ID", SqlDbType.Int)
    '                    cmd.Parameters("@ID").Direction = ParameterDirection.Output
    '                    con.Open()
    '                    If cmd.ExecuteNonQuery() Then
    '                        con.Close()
    '                        BindGrid()
    '                    End If

    '                    hdnEmpTableID.Value = cmd.Parameters("@ID").Value.ToString()

    '                End Using
    '            End Using
    '        End If

    '        If e.CommandName = "Delete" Then
    '            Dim hdnPrePnLCostID As New HiddenField
    '            Dim row As GridViewRow = CType(CType(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
    '            hdnPrePnLCostID = CType(row.FindControl("hdnPrePnLCostID"), HiddenField)
    '            Dim sql As String = "Delete from APEX_tempPrePnLCost where PrePnLCostID=" & hdnPrePnLCostID.Value
    '            If ExecuteNonQuery(sql) > 0 Then
    '                BindGrid()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        SendExMail(ex.Message, ex.ToString())
    '    End Try
    'End Sub

    Private Function GetID() As String
        Dim IID As Integer = 0
        Try

            Dim sqla As New StringBuilder
            sqla.Append("")
            sqla.Append("")
            sqla.Append("" & getBriefID() & " ")


        Catch ex As Exception

        End Try

        Return IID

    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim sql As String = ""
        sql &= "  INSERT INTO [dbo].[Apex_EmployeesAdvence]"
        sql &= "          ([RefjobcardID]"
        sql &= "          ,[RefTaskID]"
        sql &= "          ,[AdvanceAmtRequest]"
        sql &= "          ,[PaymentToBeDone]"
        sql &= "          ,[BudgetAmount]"
        sql &= "          ,[ExpectedDateOfExpenses]"
        sql &= "          ,[BranchHeadApproval]"
        sql &= "          ,[BranchHeadApprovalDate])"
        sql &= "     VALUES"
        sql &= "          ('" & getTaskIDbyAccountID(hdnTaskID.Value) & "'"
        sql &= "          ,'" & hdnTaskID.Value & "'"
        sql &= "          ,'" & txtadvanceAmount.Text & "'"
        sql &= "          ,convert(Date,'" & txtpaymenttobedone.Text & "',103)"
        sql &= "          ,'" & txtbugetamountallocated.Text & "'"
        sql &= "          ,convert(Date,'" & txtexpecteddateofexpences.Text & "',103)"
        sql &= "          ,'0'"
        sql &= "          ,NULL)"
        sql &= ""
        If ExecuteNonQuery(sql) Then
            Response.Write("<script>alert('Data Inserted SuccessFully')</script>")
        End If

    End Sub

End Class
