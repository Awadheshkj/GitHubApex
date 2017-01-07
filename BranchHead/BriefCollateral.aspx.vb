Imports System.Data
Imports clsDatabaseHelper
Imports clsMain
Imports clsApex
Imports System.IO

Partial Class BriefCollateral
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                divMsgAlert.Visible = False
                If Len(Request.QueryString("bid")) > 0 Then
                    If Request.QueryString("bid") <> Nothing Then
                        If Request.QueryString("bid").ToString() <> "" Then
                            Dim capex As New clsApex
                            If Len(Request.QueryString("jid")) > 0 Then
                                If Request.QueryString("jid") <> Nothing Then
                                    If Request.QueryString("jid").ToString() <> "" Then
                                        hdnBriefID.Value = capex.GetBriefIDByJobCardID(Request.QueryString("jid"))
                                        btnBrief.Visible = False

                                    End If
                                Else
                                    hdnBriefID.Value = Request.QueryString("bid").ToString()
                                End If
                            Else
                                hdnBriefID.Value = Request.QueryString("bid").ToString()
                            End If

                            If capex.GetPLIDByBriefID(hdnBriefID.Value) <> "" Then
                                btnBrief.Visible = False
                            End If

                            'hdnBriefID.Value = Request.QueryString("bid").ToString()

                            Dim jobcard As String = capex.GetJobCardIDByBriefID(hdnBriefID.Value)
                            BindColletral()
                            'If checkBriefed() = False Then
                            '    CloseAllControl()
                            '    btnBrief.Visible = False
                            '    btnUpload.Enabled = False

                            'Else

                            'trUpload.Visible = True
                            'trUpload1.Visible = True
                            'trUpload2.Visible = True
                            'trDisplay.Visible = False


                            '    btnBrief.Visible = True
                            '    btnBrief.Visible = True
                            '    btnUpload.Enabled = True
                            'End If
                        Else
                            'CallDivError()
                        End If
                    Else
                        'CallDivError()
                    End If
                Else
                    'CallDivError()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindColletral()
        Try
            Dim sql As String = "select ROW_NUMBER() OVER(ORDER BY CollateralID DESC) AS Row,CollateralID,CollateralName,CollateralPath from APEX_CollateralCenter where CollateralType = 2 and CollateralTypeID =" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim epass As String = ds.Tables(0).Rows(i)("CollateralPath")
                    ds.Tables(0).Rows(i).Item("CollateralPath") = Clean(epass)
                Next
                gvFileUploads.DataSource = ds
                gvFileUploads.DataBind()
            Else

            End If

            'gvFileDisplay.DataSource = ds
            'gvFileDisplay.DataBind()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            Dim filename As String = FUpld_Documents.FileName
            Dim fname As String = filename.Substring(0, filename.LastIndexOf("."))
            Dim fext As String = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - (filename.LastIndexOf(".") + 1))
            Dim encname As String = ""
            'txtUploads.Text = fname
            Dim path As String = ""
            encname = Clean(txtUploads.Text.ToString().Replace("&", "")) & DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date.Day & DateTime.Now.Date.Hour & DateTime.Now.Date.Minute & DateTime.Now.Date.Second
            FUpld_Documents.SaveAs(Server.MapPath("Uploads/Brief/" & encname & "." & fext))
            path = "Uploads/Brief/" & encname & "." & fext

            Dim sql As String = "INSERT INTO [APEX_CollateralCenter] ([CollateralName]"
            sql &= " ,[CollateralType]"
            sql &= " ,[CollateralTypeID]"
            sql &= " ,[CollateralPath]"
            sql &= " ,[InsertedBy])"
            sql &= " VALUES('" & Clean(txtUploads.Text.ToString().Replace("&", "")) & "'"
            sql &= " ,2"
            sql &= " ," & hdnBriefID.Value
            sql &= " ,'" & path & "'"
            sql &= " ," & getLoggedUserID() & ")"

            If ExecuteNonQuery(sql) > 0 Then
                BindColletral()
                txtUploads.Text = ""
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnBrief_Click(sender As Object, e As EventArgs) Handles btnBrief.Click
        Try
            ' Dim sql As String = "Update APEX_JobCard set IsBriefed='Y' where RefBriefID=" & hdnBriefID.Value
            'If ExecuteNonQuery(sql) > 0 Then
            ' FillEstimate()

            Dim capex As New clsApex
            hdnJobCardID.Value = capex.GetJobCardID(hdnBriefID.Value)
            If hdnJobCardID.Value <> "" Then
                capex.InsertStageLevel(hdnJobCardID.Value)
                Response.Redirect("RequestForprePnl.aspx?bid=" & hdnBriefID.Value)
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvFileUploads_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFileUploads.RowCommand
        Try
            If e.CommandName = "delete" Then
                Dim hdnColletralID As New HiddenField

                Dim row As GridViewRow = CType(CType(e.CommandSource, Button).NamingContainer, GridViewRow)

                hdnColletralID = CType(row.FindControl("hdnColletralID"), HiddenField)

                Dim sql As String = "Delete from APEX_CollateralCenter where CollateralID=" & hdnColletralID.Value
                If ExecuteNonQuery(sql) > 0 Then
                    BindColletral()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("BriefManager.aspx?mode=edit&bid=" & hdnBriefID.Value)
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

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

    Private Sub CloseAllControl()
        Try
            gvFileUploads.Enabled = False
            txtUploads.Enabled = False
            FUpld_Documents.Enabled = False
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvFileUploads_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvFileUploads.PageIndexChanging
        Try
            gvFileUploads.PageIndex = e.NewPageIndex
            BindColletral()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    'Protected Sub gvFileDisplay_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvFileDisplay.PageIndexChanging
    '    Try
    '        gvFileDisplay.PageIndex = e.NewPageIndex
    '        BindColletral()
    '    Catch ex As Exception
    '        SendExMail(ex.Message, ex.ToString())
    '    End Try
    'End Sub

    'Private Sub CallDivError()
    '    Try
    '        divContent.Visible = False
    '        divError.Visible = True
    '        Dim capex As New clsApex
    '        lblError.Text = capex.SetErrorMessage()
    '    Catch ex As Exception
    '        SendExMail(ex.Message, ex.ToString())
    '    End Try
    'End Sub

    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = CType(sender, LinkButton).CommandArgument
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
        Response.WriteFile(filePath)
        Response.End()
    End Sub

End Class