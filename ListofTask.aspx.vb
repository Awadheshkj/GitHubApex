Imports clsDatabaseHelper
Imports clsMain
Imports System.Data
Imports clsApex
Imports System.IO
Imports System.Drawing
Imports ExcelLibrary
Imports ExcelLibrary.SpreadSheet

Partial Class ListofTask
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        divError.Visible = False
        If Not Page.IsPostBack Then
            Dim apx As New clsApex
            Dim role As String = apx.GetRoleNameByUserID(getLoggedUserID)
            If role = "K" Or role = "A" Or role = "K" Then
                btnAdd.Visible = False
            End If
            If Len(Request.QueryString("jid")) > 0 Then
                hdnjobid.Value = Request.QueryString("jid")
                bindtasks(hdnjobid.Value)
            End If
            If Len(Request.QueryString("nid")) > 0 Then
                If Request.QueryString("nid") <> Nothing Then
                    Dim capex As New clsApex
                    hdnNodificationID.Value = Request.QueryString("nid")
                    capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
                End If
            End If
        End If

    End Sub

    Private Sub bindtasks(ByVal jid As Integer)
        Dim capex As New clsApex
        'Dim sql As String = "Select  Quantity,TaskID,(case when t.insertedby='" & getLoggedUserID() & "'   then '<a href=''TaskAccount.aspx?tid=' + convert(varchar(50),TaskID) +'''>' + title +  '</a>' else title end)Title,convert(varchar(10),ta.StartDate,4) as  startdate,RefJobcardID,"
        'sql &= " convert(varchar(10),ta.enddate,4) as enddate,categoryTask,ta.Description, "
        'sql &= " TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end "
        'sql &= " ,  Name = case when (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) <>'' then"
        'sql &= "  (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) else '<a href=''TaskAccount.aspx?tid='+convert(varchar(10),TaskID) +'''> N/A</a>' end"
        'sql &= " ,(select isnull(sum(PreEventTotal),0) as PreEventQuote from APEX_PrePnLcost where category=categoryTask and refbriefID=(select refbriefID from apex_jobcard where jobcardid=" & jid & "))Budge"
        ''sql &= " ,(select Total from APEX_TaskAccount where reftaskid=TaskID and TL=ta.TL)Expence"
        'sql &= "  ,Total as Expence,(select (isnull(firstName,'')+' '+isnull(LastName,'')) from APEX_UsersDetails where UserDetailsID=t.insertedby)AssignFrom,particulars,AccountID,status,ta.Remarks,t.insertedby ,workstatus  "
        'sql &= "  from APEX_Task as t "
        'sql &= "  Inner join APEX_TaskAccount as ta on t.taskid=ta.refTaskID"
        'sql &= " left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        'sql &= "  where RefJobcardID=" & jid & " and (t.insertedby=" & getLoggedUserID() & " or ta.TL=" & getLoggedUserID() & ") order by CategoryTask "
        Dim sql As String = ""
        sql &= "	select AccountID ,RefjobcardID,Particulars ,StartDate,EndDate,Description,Quantity,Total,category,TaskName,"
        sql &= "	(isnull(ud.firstName,'')+' '+isnull(ud.LastName,''))Name       "
        sql &= "	,Status,Remarks,TaskCompleted,Workstatus,ta.insertedby,ta.TL,(case when Status<>'Completed' and Enddate < Getdate() then 'expired' else '' end)sts,ta.TL   from APEX_TaskAccount ta"
        sql &= "	left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        sql &= "	 where RefjobcardID=" & jid & " and (ta.insertedby =" & getLoggedUserID() & " or ta.insertedby in(" & capex.getuserdetailsID(getLoggedUserID()) & ") or ta.TL=" & getLoggedUserID() & " or  ta.TL in(" & capex.getuserdetailsID(getLoggedUserID()) & ")) "
        ' sql &= " and (ta.InsertedBy=" & getLoggedUserID() & " or ta.TL=" & getLoggedUserID() & ") "
        sql &= " order by Category"

        Dim ds As DataSet = Nothing
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gvTasks.DataSource = ds.Tables(0)
            gvTasks.DataBind()
        End If
        gvTasks.DataSource = ds.Tables(0)
        gvTasks.DataBind()
        'GridView1.DataSource = ds.Tables(0)
        'GridView1.DataBind()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("dashboard.aspx")
    End Sub

    Protected Sub gvTasks_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTasks.PageIndexChanging
        gvTasks.PageIndex = e.NewPageIndex
        If hdnjobid.Value <> "" Then
            bindtasks(hdnjobid.Value)
        End If

    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Response.Redirect("Task.aspx?jid=" & hdnjobid.Value & "&mode=add")
    End Sub

    Protected Sub gvTasks_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTasks.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hdnTaskID As HiddenField = CType(e.Row.FindControl("hdnTaskID"), HiddenField)
            Dim lblstatus As Label = CType(e.Row.FindControl("lbltaskstatus"), Label)
            Dim hdnstatus As HiddenField = CType(e.Row.FindControl("hdnstatus"), HiddenField)
            Dim ddlstatus As DropDownList = CType(e.Row.FindControl("ddlStatus"), DropDownList)
            Dim perc As DropDownList = CType(e.Row.FindControl("ddlcompletestatus"), DropDownList)
            Dim hdninsertedby As HiddenField = CType(e.Row.FindControl("hdninsertedby"), HiddenField)
            Dim btnaddRemarks As LinkButton = CType(e.Row.FindControl("btnaddRemarks"), LinkButton)
            Dim hdnworkperc As HiddenField = CType(e.Row.FindControl("hdnworkperc"), HiddenField)
            Dim hdnsts As HiddenField = CType(e.Row.FindControl("hdnsts"), HiddenField)

            'If hdninsertedby.Value = getLoggedUserID() Then
            '    btnaddRemarks.Visible = False
            'Else
            '    btnaddRemarks.Visible = True
            'End If

            ddlstatus.SelectedValue = hdnstatus.Value
            perc.SelectedValue = hdnworkperc.Value
            'If IsTaskCompleted(hdnTaskID.Value) = True Then
            '    lblstatus.Text = "Completed"
            'Else
            '    'lblstatus.Text = "Pending"
            '    e.Row.Attributes("onclick") = "javascript:call(" & hdnjobid.Value & "," & hdnTaskID.Value & ");"

            'End If

            If hdnstatus.Value = "" Then
                If hdnsts.Value = "expired" Then
                    e.Row.Attributes.Add("style", "background-color: #f2dede;")
                Else
                    e.Row.Attributes.Add("style", "background-color: #d9edf7;")
                End If

            ElseIf hdnstatus.Value = "Running" Then
                If hdnsts.Value = "expired" Then
                    e.Row.Attributes.Add("style", "background-color: #f2dede;")
                End If

            ElseIf hdnstatus.Value = "Completed" Then
                e.Row.Attributes.Add("style", "background-color: #dff0d8; color:#468847;")

            End If

            e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
            e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")
        End If
    End Sub

    Private Function IsTaskCompleted(ByVal tid As String) As Boolean
        Dim sql As String = ""
        Dim result As Boolean
        Dim j As Integer = -1
        sql = "Select AccountID from APEX_TaskAccount where RefTAskID=" & tid
        Dim ds As DataSet = Nothing
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If Not IsDBNull(ds.Tables(0).Rows(i)("AccountID")) Then
                    If Get_CheckListAvailable(ds.Tables(0).Rows(i)("AccountID")) = True Then
                        If Status_CheckList(ds.Tables(0).Rows(i)("AccountID")) = True Then
                            j = j * 1
                        Else
                            j = j * 0
                        End If
                    Else
                        j = j * 0

                    End If
                End If
            Next
        Else
            j = 0
        End If
        If j = 0 Then
            result = False
        Else
            result = True
        End If
        Return result
    End Function

    Protected Sub btnaddRemarks_Click(sender As Object, e As EventArgs)
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim CommandName As String = btn.CommandName
        Dim CommandArgument As String = btn.CommandArgument
        Dim gvrow As GridViewRow = CType(sender, LinkButton).NamingContainer
        Dim rowindex As Integer = CType(gvrow, GridViewRow).RowIndex
        'hdnCID = CType(gdvChecklist.Rows(rowindex).FindControl("hdnCID"), HiddenField)

        Dim Remarks As String = TryCast(gvTasks.Rows(rowindex).FindControl("txtRemarks"), TextBox).Text
        Dim Status As String = TryCast(gvTasks.Rows(rowindex).FindControl("ddlStatus"), DropDownList).SelectedValue
        Dim perc As String = TryCast(gvTasks.Rows(rowindex).FindControl("ddlcompletestatus"), DropDownList).SelectedValue
        Dim FileUpload1 As FileUpload = TryCast(gvTasks.Rows(rowindex).FindControl("file1"), FileUpload)
        Dim hdnRefJobcardID As HiddenField = TryCast(gvTasks.Rows(rowindex).FindControl("hdnRefJobcardID"), HiddenField)
        Dim hdnTaskIDval As HiddenField = TryCast(gvTasks.Rows(rowindex).FindControl("hdnTaskID"), HiddenField)

        'Dim lbltaskassign As String = TryCast(gvTasks.Rows(rowindex).FindControl("lbltaskassign"), Label).Text
        Dim lbltaskassign As String = getLoggedUserName()
        If Status = "Completed" Then
            perc = "100"
        End If

        If perc = "100" Then
            Status = "Completed"
        End If

        'lbltaskassign
        'Response.Write("<script>alert('" & Remarks & "');</script>")
        'ETD = "Convert(datetime,'" & txtETD.Text & "',105)"

        Dim sqlString As String = ""
        sqlString &= "  update APEX_TaskAccount set status='" & Status & "', Remarks='" & Remarks & "', workstatus= '" & perc & "' where AccountID='" & CommandArgument & "'         "
        sqlString &= " INSERT INTO [Apex_TaskHistory]( [RefTaskID],[Incharge],Status"
        If Remarks <> "" Then
            sqlString &= " ,Remarks"
        End If
        sqlString &= " ,workstatus)"
        sqlString &= "     VALUES (" & CommandArgument
        sqlString &= "  ,'" & lbltaskassign & "'"
        sqlString &= " ,'" & Clean(Status.Trim()) & "'"
        If Remarks <> "" Then
            sqlString &= " ,'" & Clean(Remarks.Trim()) & "'"
        End If
        sqlString &= "  ,'" & perc & "')"
        sqlString &= " "

        'lblSuccess.Text = String.Format("{0} files have been uploaded successfully.", hfc.Count - 1)

        If ExecuteNonQuery(sqlString) > 0 Then


            If FileUpload1.HasFile Then
                Dim hfc As HttpFileCollection = Request.Files
                For i As Integer = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        Dim encname As String = ""
                        Dim filename As String = hpf.FileName
                        Dim ext As String = System.IO.Path.GetExtension(hpf.FileName)
                        Dim fext As String = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - (filename.LastIndexOf(".") + 1))
                        Dim filepath As String = ""
                        encname = Clean(filename.Substring(0, filename.LastIndexOf(".")).ToString().Replace("&", "")) & DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date.Day & DateTime.Now.Date.Hour & DateTime.Now.Date.Minute & DateTime.Now.Date.Second

                        filepath = "uploads/TaskCollatrals/" & encname & "." & fext

                        'hpf.SaveAs(Server.MapPath("uploads/TaskCollatrals") & "\" & Path.GetFileName(hpf.FileName))
                        Dim sql As String = ""
                        sql &= "INSERT INTO [dbo].[Apex_TaskCollaterals]"
                        sql &= "           ([RefTaskID]"
                        sql &= "           ,[ReftaskaccountID]"
                        sql &= "           ,[RefTaskhistoryID]"
                        sql &= "           ,[FileName]"
                        sql &= "           ,[fileExt]"
                        sql &= "           ,[filePath]"
                        sql &= "           ,[Inserted_by]"
                        sql &= "           ,[RefJCnumber]"
                        sql &= "           ,[Type])"
                        sql &= "     VALUES"
                        sql &= "           (" & hdnTaskIDval.Value & ""
                        sql &= "           ," & CommandArgument & ""
                        sql &= "           ,(select max(checklistID) from [dbo].[Apex_TaskHistory] where RefTaskID=" & CommandArgument & ")"
                        sql &= "           ,'" & encname & "'"
                        sql &= "           ,'" & fext & "'"
                        sql &= "           ,'" & filepath & "'"
                        sql &= "           ,'" & getLoggedUserID() & "'"
                        sql &= "           ,'" & hdnRefJobcardID.Value & "'"
                        sql &= "           ,'Task')"
                        sql &= ""
                        sql &= ""
                        If ExecuteNonQuery(sql) Then
                            hpf.SaveAs(Server.MapPath("uploads/TaskCollatrals/" & encname & "." & fext))
                        End If
                    End If
                Next i
            End If

            ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", "alert('Data Updated Successfully')")
            lblError.Text = ""
            divError.Visible = False
            bindtasks(hdnjobid.Value)
        End If
    End Sub

    'Protected Sub UploadMultipleFiles(sender As Object, e As EventArgs)
    '    For Each postedFile As HttpPostedFile In FileUpload1.PostedFiles
    '        Dim fileName As String = Path.GetFileName(postedFile.FileName)
    '        postedFile.SaveAs(Server.MapPath("~/Uploads/") & fileName)
    '    Next
    '    lblSuccess.Text = String.Format("{0} files have been uploaded successfully.", FileUpload1.PostedFiles.Count)
    'End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            GridView1.AllowPaging = False
            bindtasks1(hdnjobid.Value)
            GridView1.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In GridView1.HeaderRow.Cells
                cell.BackColor = GridView1.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In GridView1.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = GridView1.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            GridView1.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using

    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered

    End Sub
    'Private Sub bindtasks1(ByVal jid As Integer)
    '    Dim sql As String = "Select  Quantity,TaskID,(case when t.insertedby='" & getLoggedUserID() & "'   then  title end)Title,convert(varchar(10),ta.StartDate,4) as  startdate,RefJobcardID,"
    '    sql &= " convert(varchar(10),ta.enddate,4) as enddate,categoryTask,ta.Description, "
    '    sql &= " TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end "
    '    sql &= " ,  Name = case when (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) <>'' then"
    '    sql &= "  (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) else '<a href=''TaskAccount.aspx?tid='+convert(varchar(10),TaskID) +'''> N/A</a>' end"
    '    sql &= " ,(select isnull(sum(PreEventTotal),0) as PreEventQuote from APEX_PrePnLcost where category=categoryTask and refbriefID=(select refbriefID from apex_jobcard where jobcardid=" & jid & "))Budge"
    '    'sql &= " ,(select Total from APEX_TaskAccount where reftaskid=TaskID and TL=ta.TL)Expence"
    '    sql &= "  ,Total as Expence,(select (isnull(firstName,'')+' '+isnull(LastName,'')) from APEX_UsersDetails where UserDetailsID=t.insertedby)AssignFrom,particulars,AccountID,status,ta.Remarks,t.insertedby ,( case when workstatus='' then '0' else workstatus end)workstatus  "
    '    sql &= "  from APEX_Task as t "
    '    sql &= "  Inner join APEX_TaskAccount as ta on t.taskid=ta.refTaskID"
    '    sql &= " left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
    '    sql &= "  where RefJobcardID=" & jid & " and (t.insertedby=" & getLoggedUserID() & " or ta.TL=" & getLoggedUserID() & ") order by CategoryTask "
    '    Dim ds As DataSet = Nothing
    '    ds = ExecuteDataSet(sql)
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        'gvTasks.DataSource = ds.Tables(0)
    '        'gvTasks.DataBind()
    '    End If
    '    GridView1.DataSource = ds.Tables(0)
    '    GridView1.DataBind()

    'End Sub
    Private Sub bindtasks1(ByVal jid As Integer)

        Dim sql As String = ""
        sql &= "	select AccountID,RefjobcardID,Particulars ,StartDate,EndDate,Description,Quantity,Total,category,"
        sql &= "	(isnull(ud.firstName,'')+' '+isnull(ud.LastName,''))Name       "
        sql &= "	,Status,Remarks,TaskCompleted,Workstatus,ta.insertedby   from APEX_TaskAccount ta"
        sql &= "	left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        sql &= "	 where RefjobcardID=" & jid & " and (ta.InsertedBy=" & getLoggedUserID() & " or ta.TL=" & getLoggedUserID() & ") order by Category"
        sql &= ""

        Dim ds As DataSet = Nothing
        ds = ExecuteDataSet(sql)
        GridView1.DataSource = ds.Tables(0)
        GridView1.DataBind()
    End Sub
End Class
