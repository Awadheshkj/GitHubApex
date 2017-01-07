Imports clsDatabaseHelper
Imports System.Data
Imports clsApex
Imports clsMain
Imports System.IO

Partial Class Leads
    Inherits System.Web.UI.Page

    Private Sub Leads_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            getgvdata()
        End If
    End Sub

    Private Sub getgvdata()
        Try
            Dim capex As New clsApex
            Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

            Dim sql As String = ""
            sql &= "Select  LeadID,UD.FirstName KamName,LeadName,Client, ContactName, ProjectType, "
            sql &= "RefActivityTypeID, (Select dbo.fn_parsehtml (isnull(LeadBrief,'N/A')))LeadBrief, Budget, ExecutionMandays, ClosureByDateTime, ClosureProbability, LeadStatus, "
            sql &= " (Select dbo.fn_parsehtml (Remarks))Remarks from Apex_Leads L"
            sql &= ""
            'sql &= " Left join Apex_LeadHistory LH on L.LeadID=LH.RefleadID"
            sql &= " inner join Apex_clients C on L.RefclientID=C.ClientID"
            sql &= " inner join Apex_ClientContacts CC on L.RefClientContactID=CC.ContactID"
            sql &= " inner join APEX_ActivityType AA on AA.ProjectTypeID= L.PrimaryActivityID"
            sql &= " inner join Apex_usersdetails UD on UD.userDetailsID=L.InsertedBy "
            If role <> "A" And role <> "H" Then
                sql &= " where l.IsActive = 'Y' and l.IsDeleted = 'N' and l.insertedBy=" & getLoggedUserID() & " or l.insertedBy in(" & capex.getuserdetailsID(getLoggedUserID()) & ")   order by l.leadid Desc  "
            Else
                sql &= " where l.IsActive = 'Y' and l.IsDeleted = 'N'    order by l.leadid Desc  "
            End If
            sql &= ""
            sql &= ""

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)

            gdvlead.DataSource = ds
            gdvlead.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub gdvlead_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdvlead.RowDataBound
    '    For i As Integer = gdvlead.Rows.Count - 1 To 1 Step -1
    '        Dim row As GridViewRow = gdvlead.Rows(i)
    '        Dim previousRow As GridViewRow = gdvlead.Rows(i - 1)
    '        For j As Integer = 0 To row.Cells.Count - 1
    '            If row.Cells(j).Text = previousRow.Cells(j).Text Then
    '                If previousRow.Cells(j).RowSpan = 0 Then
    '                    If row.Cells(j).RowSpan = 0 Then
    '                        previousRow.Cells(j).RowSpan += 2
    '                    Else
    '                        previousRow.Cells(j).RowSpan = row.Cells(j).RowSpan + 1
    '                    End If
    '                    row.Cells(j).Visible = False
    '                End If
    '            End If
    '        Next
    '    Next
    'End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Response.Clear()
        getgvdata()
        Response.AddHeader("content-disposition", "attachment; filename=Lead" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"
        Dim stringWriter As New StringWriter()
        Dim htmlWriter As New HtmlTextWriter(stringWriter)
        gdvlead.RenderControl(htmlWriter)
        Response.Write(stringWriter.ToString())
        Response.End()
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered

    End Sub

    Private Sub gdvlead_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdvlead.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LeadId As String = gdvlead.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim gdvleadchild As GridView = TryCast(e.Row.FindControl("gdvleadchild"), GridView)

            Dim sql As String = ""
            sql &= "  select NextActionStep,Convert(Varchar(10),ActionDate,105)ActionDate from Apex_LeadHistory where refleadID=" & LeadId
            sql &= ""
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            gdvleadchild.DataSource = ds
            gdvleadchild.DataBind()
        End If
    End Sub
End Class
