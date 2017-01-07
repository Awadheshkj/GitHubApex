
Imports clsDatabaseHelper
Imports System.Data
Imports clsApex

Partial Class Employees
    Inherits System.Web.UI.Page
    Dim ds As New DataSet
    Dim sql As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex

                FillEmployee()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillEmployee()
        Try
            sql = "  select ROW_NUMBER() OVER(ORDER BY apud.UserDetailsID DESC) AS Row,"
            sql &= " apud.UserDetailsID,apud.FirstName+ ' ' + isnull(apud.LastName,'') as Name,apud.Mobile1 as Contact,"
            sql &= " apd.Designation,apud.EmailID,apud.IsActive,Empcode from APEX_UsersDetails as apud "
            sql &= " inner join APEX_Designation as apd on apd.DesignationID=apud.Designation "
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvEmployee.DataSource = ds
                gvEmployee.DataBind()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Protected Sub gvEmployee_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvEmployee.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnEmployeeID"), HiddenField)

                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"

                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvEmployee_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvEmployee.PageIndexChanging
        Try
            gvEmployee.PageIndex = e.NewPageIndex
            FillEmployee()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class