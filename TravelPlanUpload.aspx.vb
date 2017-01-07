Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports clsDatabaseHelper
Imports System.Data.Common

Partial Class TravelPlanUpload

    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsPostBack Then
            'GETDATA()
        End If
    End Sub

    'Protected Sub btnImport_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim connString As String = ""
    '    Dim strFileType As String = Path.GetExtension(fileuploadExcel.FileName).ToLower()
    '    Dim path__1 As String = fileuploadExcel.PostedFile.FileName
    '    'Connection String to Excel Workbook
    '    If strFileType.Trim() = ".xls" Then
    '        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path__1 & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=2"""
    '    ElseIf strFileType.Trim() = ".xlsx" Then
    '        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path__1 & ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2"""
    '    End If
    '    Dim query As String = "SELECT EmployeeID FROM [Sheet1$]"
    '    Dim conn As New OleDbConnection(connString)
    '    If conn.State = ConnectionState.Closed Then
    '        conn.Open()
    '    End If
    '    Dim cmd As New OleDbCommand(query, conn)
    '    Dim da As New OleDbDataAdapter(cmd)
    '    Dim ds As New DataSet()
    '    da.Fill(ds)
    '    grvExcelData.DataSource = ds.Tables(0)
    '    grvExcelData.DataBind()
    '    da.Dispose()
    '    conn.Close()
    '    conn.Dispose()
    'End Sub

    Private Sub GetExcelSheets(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)
        Dim conStr As String = ""
        Select Case Extension
            Case ".xls"
                'Excel 97-03 
                conStr = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                Exit Select
            Case ".xlsx"
                'Excel 07 
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString").ConnectionString
                Exit Select
        End Select

        'Get the Sheets in Excel WorkBoo 
        conStr = String.Format(conStr, FilePath, isHDR)
        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        cmdExcel.Connection = connExcel
        connExcel.Open()

        'Bind the Sheets to DropDownList 
        ddlSheets.Items.Clear()
        ddlSheets.Items.Add(New ListItem("--Select Sheet--", ""))
        ddlSheets.DataSource = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        ddlSheets.DataTextField = "TABLE_NAME"
        ddlSheets.DataValueField = "TABLE_NAME"
        ddlSheets.DataBind()
        connExcel.Close()

    End Sub

    'Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
    '    Dim FileName As String = lblFileName.Text
    '    Dim Extension As String = Path.GetExtension(FileName)
    '    Dim FolderPath As String = Server.MapPath( _
    '       ConfigurationManager.AppSettings("FolderPath"))
    '    Dim CommandText As String = ""
    '    Select Case Extension
    '        Case ".xls"
    '            'Excel 97-03 
    '            CommandText = "spx_ImportFromExcel03"
    '            Exit Select
    '        Case ".xlsx"
    '            'Excel 07 
    '            CommandText = "spx_ImportFromExcel07"
    '            Exit Select
    '    End Select
    '    'Read Excel Sheet using Stored Procedure 
    '    'And import the data into Database Table 
    '    Dim strConnString As String = ConfigurationManager _
    '      .ConnectionStrings("conString").ConnectionString
    '    Dim con As New SqlConnection(strConnString)
    '    Dim cmd As New SqlCommand()
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.CommandText = CommandText
    '    cmd.Parameters.Add("@SheetName", SqlDbType.VarChar).Value = ddlSheets.SelectedItem.Text
    '    cmd.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = FolderPath + FileName
    '    'cmd.Parameters.Add("@HDR", SqlDbType.VarChar).Value = rbHDR.SelectedItem.Text
    '    cmd.Parameters.Add("@HDR", SqlDbType.VarChar).Value = "Yes"
    '    cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = "Promoters_TrainingDetails"
    '    cmd.Connection = con
    '    Try
    '        con.Open()
    '        Dim count As Object = cmd.ExecuteNonQuery()
    '        lblMessage.ForeColor = System.Drawing.Color.White
    '        lblMessage.Text = count.ToString() & " Records inserted."
    '    Catch ex As Exception
    '        'lblMessage.ForeColor = System.Drawing.Color.Red
    '        lblMessage.Text = ex.Message
    '    Finally
    '        con.Close()
    '        con.Dispose()
    '        Panel1.Visible = True
    '        Panel2.Visible = False
    '    End Try
    'End Sub

    'Protected Sub btnview_Click(sender As Object, e As EventArgs) Handles btnview.Click
    '    Dim FileName As String = lblFileName.Text
    '    Dim Extension As String = Path.GetExtension(FileName)
    '    Dim FolderPath As String = Server.MapPath( _
    '       ConfigurationManager.AppSettings("FolderPath"))
    '    Dim CommandText As String = ""
    '    Select Case Extension
    '        Case ".xls"
    '            'Excel 97-03 
    '            CommandText = "SELECT TrainingDate	,TrainingHubTown	,EmployeeID	,EmployeeName	,ContactNo	,FFASMName	,TrainerName 	,TrainerNo	,QuizScores	,RoleplayScores	,Attendance	,Remarks FROM OPENDATASOURCE('Microsoft.Jet.OLEDB.4.0','Data Source=" & FolderPath + FileName & ";Extended Properties=''Excel 8.0;HDR=Yes''')...[" & ddlSheets.SelectedItem.Text & "] "
    '            Exit Select
    '        Case ".xlsx"
    '            'Excel 07 
    '            CommandText = "SELECT TrainingDate	,TrainingHubTown	,EmployeeID	,EmployeeName	,ContactNo	,FFASMName	,TrainerName 	,TrainerNo	,QuizScores	,RoleplayScores	,Attendance	,Remarks FROM OPENDATASOURCE('Microsoft.Jet.OLEDB.12.0','Data Source=" & FolderPath + FileName & ";Extended Properties=''Excel 12.0;HDR=Yes''')...[" & ddlSheets.SelectedItem.Text & "] "
    '            Exit Select
    '    End Select
    '    Try
    '        Dim ds As New DataSet
    '        ds = ExecuteDataSet(CommandText)
    '        grvExcelData.DataSource = Nothing
    '        grvExcelData.DataBind()
    '        grvExcelData.DataSource = ds
    '        grvExcelData.DataBind()
    '    Catch ex As Exception
    '        lblMessage.ForeColor = System.Drawing.Color.Red
    '        lblMessage.Text = ex.Message
    '    Finally
    '        'Panel1.Visible = True
    '        'Panel2.Visible = False
    '    End Try
    'End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Panel1.Visible = True
        'Panel2.Visible = False
        grvExcelData.DataSource = Nothing
        grvExcelData.DataBind()
    End Sub

    'Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
    '    If Me.FileUpload1.HasFile Then
    '        If Me.FileUpload1.PostedFile.ContentType = "application/vnd.ms-excel" OrElse Me.FileUpload1.PostedFile.ContentType = "text/excel" OrElse Me.FileUpload1.PostedFile.ContentType = "aplication/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Then
    '            Try
    '                Try
    '                    'string str = string.Concat(base.Server.MapPath("~/TempFiles/"), this.FileUpload1.FileName);
    '                    Dim str As String = String.Concat(MyBase.Server.MapPath("~/Files/"), "ProductTemplate.xls")
    '                    Me.FileUpload1.SaveAs(str)
    '                    Dim str1 As String = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", str)
    '                    Using oleDbConnection As New OleDbConnection(str1)
    '                        Dim oleDbCommand As New OleDbCommand("Select * FROM [Sample sheet$]", oleDbConnection)
    '                        oleDbConnection.Open()
    '                        Using dbDataReaders As DbDataReader = oleDbCommand.ExecuteReader()

    '                            grvExcelData.DataSource = dbDataReaders
    '                            grvExcelData.DataBind()
    '                            '    Dim str2 As String = ""
    '                            '    Dim str3 As String = ""
    '                            '    Dim str4 As String = ""
    '                            '    Dim str5 As String = ""
    '                            '    Dim str6 As String = ""
    '                            '    Dim str7 As String = ""
    '                            '    Dim str8 As String = ""
    '                            '    Dim str9 As String = ""
    '                            '    Dim num As Integer = 0
    '                            '    While dbDataReaders.Read()
    '                            '        str2 = dbDataReaders!saSSD
    '                            '        str3 = Me.valid(dbDataReaders, 1)
    '                            '        str4 = Me.valid(dbDataReaders, 2)
    '                            '        str5 = Me.valid(dbDataReaders, 3)
    '                            '        str6 = Me.valid(dbDataReaders, 4)
    '                            '        str7 = Me.valid(dbDataReaders, 5)
    '                            '        str8 = Me.valid(dbDataReaders, 6)
    '                            '        str9 = Me.valid(dbDataReaders, 7)
    '                            '        'Me.insertdataintosql(str2, str3, str4, str5, str6, str7, _
    '                            '        '    str8, str9)
    '                            '        num += 1
    '                            '    End While



    '                            oleDbConnection.Close()
    '                            System.Web.UI.ScriptManager.RegisterStartupScript(Me, MyBase.[GetType](), "Message", "alert('Data Uploaded successfully.');", True)
    '                        End Using
    '                    End Using
    '                Catch dataException As DataException
    '                End Try
    '            Finally

    '            End Try
    '        Else
    '            System.Web.UI.ScriptManager.RegisterStartupScript(Me, MyBase.[GetType](), "Message", "alert('Please import excel sheet(xls/xlsx) of ProductTemplate format')", True)
    '        End If
    '    End If
    'End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If fileuploadExcel.HasFile Then
            Dim FileName As String = Path.GetFileName(fileuploadExcel.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(fileuploadExcel.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")

            Dim FilePath As String = Server.MapPath(FolderPath + FileName)
            lblFileName.Text = FileName
            fileuploadExcel.SaveAs(FilePath)
            GetExcelSheets(FilePath, Extension, "Yes")
            ddlSheets.Visible = True
            btnImport.Visible = True
            fileuploadExcel.Visible = False
            btnUpload.Visible = False
        End If
    End Sub
    Protected Sub Import_Click(sender As Object, e As EventArgs)
        'If Me.fileuploadExcel.HasFile Then
        'If Me.fileuploadExcel.PostedFile.ContentType = "application/vnd.ms-excel" Or Me.fileuploadExcel.PostedFile.ContentType = "text/excel" Or Me.fileuploadExcel.PostedFile.ContentType = "aplication/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Then
        Try
            Try
                'string str = string.Concat(base.Server.MapPath("~/TempFiles/"), this.FileUpload1.FileName);
                Dim FileName As String = lblFileName.Text
                Dim FolderPath As String = Server.MapPath( _
       ConfigurationManager.AppSettings("FolderPath"))

                'Dim str As String = String.Concat(MyBase.Server.MapPath("~/Document/"), "Employee.xlsx")
                'Me.fileuploadExcel.SaveAs(str)

                Dim str1 As String = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", FolderPath + FileName)
                Using oleDbConnection As New OleDbConnection(str1)
                    Dim oleDbCommand As New OleDbCommand("Select * FROM [" & ddlSheets.SelectedItem.Text & "]", oleDbConnection)
                    oleDbConnection.Open()
                    Using dbDataReaders As OleDbDataReader = oleDbCommand.ExecuteReader()

                        Dim str2 As String = ""
                        Dim str3 As String = ""
                        Dim str4 As String = ""
                        Dim str5 As String = ""
                        Dim str6 As String = ""
                        Dim str7 As String = ""
                        Dim str8 As String = ""
                        Dim str9 As String = ""
                        Dim str10 As String = ""
                        Dim str11 As String = ""
                        Dim str12 As String = ""
                        Dim str13 As String = ""
                        Dim str14 As String = ""
                        Dim str15 As String = ""
                        Dim str16 As String = ""


                        Dim num As Integer = 0

                        Dim sql As String = " Delete from Apex_TravelPlan"
                        ExecuteNonQuery(sql)
                        Dim dt As New DataTable
                        While dbDataReaders.Read()
                            'Dim R As DataRow = dt.NewRow
                            str2 = Me.valid(dbDataReaders, 0)
                            str3 = Me.valid(dbDataReaders, 1)
                            str4 = Me.valid(dbDataReaders, 2)
                            str5 = Me.valid(dbDataReaders, 3)
                            str6 = Me.valid(dbDataReaders, 4)
                            str7 = Me.valid(dbDataReaders, 5)
                            str8 = Me.valid(dbDataReaders, 6)
                            str9 = Me.valid(dbDataReaders, 7)
                            str10 = Me.valid(dbDataReaders, 8)
                            str11 = Me.valid(dbDataReaders, 9)
                            str12 = Me.valid(dbDataReaders, 10)
                            str13 = Me.valid(dbDataReaders, 11)
                            str14 = Me.valid(dbDataReaders, 12)
                            str15 = Me.valid(dbDataReaders, 13)
                            str16 = Me.valid(dbDataReaders, 14)

                            Me.insertdataintosql(str2, str3, str4, str5, str6, str7, str8, str9, str10, str11, str12, str13, str14, str15, str16)
                            'Me.insertdataintosql(str2, str3, str4, str5, str6)
                            num += 1
                        End While
                        'grvExcelData.DataSource = dt
                        'grvExcelData.DataBind()
                        oleDbConnection.Close()
                        System.Web.UI.ScriptManager.RegisterStartupScript(Me, MyBase.[GetType](), "Message", "alert('Data Uploaded successfully.');", True)
                        GETDATA()

                    End Using
                End Using
            Catch dataException As DataException
            End Try

        Finally

        End Try
        'Else
        '    System.Web.UI.ScriptManager.RegisterStartupScript(Me, MyBase.[GetType](), "Message", "alert('Please import excel sheet(xls/xlsx) of ProductTemplate format')", True)
        'End If
        ' End If
    End Sub

    Protected Function valid(myreader As OleDbDataReader, stval As Integer) As String
        Dim item As Object = myreader(stval)
        'If item <> DBNull.Value Then
        Return item.ToString()
        'End If
        Return Convert.ToString(0)
    End Function

    Public Sub insertdataintosql(Month As String, Client As String, Region As String, Eventname As String, KAM As String, City As String, Date1 As String, Day2 As String, Day3 As String, JCNumber As String, PM As String, EM As String, Venue As String, ActivityType As String, Status As String)


        Try
            Dim sql As New StringBuilder
            sql.Append("INSERT INTO [dbo].[Apex_TravelPlan]([Month],[Client],[Region],[Event],[KAM],[City],[Date],[Day2],[Day3],[JCNumber]")
            sql.Append("       ,[PM],[EM],[Venue],[ActivityType],[Status])")
            sql.Append(" VALUES")
            sql.Append("       ('" & Month & "','" & Client & "', '" & Region & "', '" & Eventname & "','" & KAM & "','" & City & "','" & Date1 & "','" & Day2 & "','" & Day3 & "','" & JCNumber & "','" & PM & "','" & EM & "','" & Venue & "','" & ActivityType & "','" & Status & "')")
            sql.Append("")

            'sql.Append("update [Reg_Registration] set Remarks='" & Remarks & "',Reason='" & Reason & "',lastupdatedate=GETDATE(),UpdateOn='" & UpdateOn & "',WIPDate='" & WIPDate & "' where TicketNo='" & TicketNo & "'")

            If ExecuteNonQuery(sql.ToString()) Then
                ' Response.Write("<script language='javascript'>alert('Data Uploade')</script>")
            End If

        Catch exception As Exception
        End Try


    End Sub

    Private Sub GETDATA()
        Try
            'Dim sql As New StringBuilder
            'sql.Append("SELECT TrainingDate	,TrainingHubTown	,EmployeeID	,EmployeeName	,ContactNo	,FFASMName	,TrainerName 	,TrainerNo	,QuizScores	,RoleplayScores	,Attendance	,Remarks FROM Promoters_TrainingDetails order by ID desc")
            'sql.Append("")
            'Dim ds As New DataSet
            'ds = ExecuteDataSet(sql.ToString())
            'grvExcelData.DataSource = ds
            'grvExcelData.DataBind()

        Catch ex As Exception

        End Try

    End Sub
End Class
