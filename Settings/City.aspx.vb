Imports clsDatabaseHelper
Imports System.Data
Imports clsApex

Partial Class Masters_City
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex

                FillCity()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillCity()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            sql = "select ROW_NUMBER() OVER(ORDER BY ct.CityID DESC) AS Row,"
            sql &= " st.State,ct.CityID, ct.City,ct.IsActive from APEX_City as "
            sql &= " ct  inner join APEX_State as st on st.StateID=ct.RefStateID"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvCity.DataSource = ds
                gvCity.DataBind()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvCity_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCity.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnCityID"), HiddenField)

                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"

                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub


    Protected Sub gvCity_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvCity.PageIndexChanging
        Try
            gvCity.PageIndex = e.NewPageIndex
            FillCity()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class