Imports System.Data
Imports clsDatabaseHelper
Imports clsMain
Imports clsApex

Partial Class AjaxCalls_PMDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim result As String = "False"
        If Len(Request.QueryString("call")) > 0 Then
            If Request.QueryString("call").ToString() <> "" Then
                Dim callid As String = Request.QueryString("call").ToString()
                If callid = "1" Then
                    result = Fillcalendar()
                End If
                Response.Write(result)
                Response.End()
            End If

        End If
    End Sub


    Private Function Fillcalendar() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim sql As String = ""
        sql &= " select  * from ("
        sql &= " select Convert(varchar(10),Convert(Date,Date))Day1, Convert(varchar(10),Convert(Date,Day2))Day2, "
        sql &= " (Case when Convert(Date,Date)='1900-01-01' and  Convert(Date,Day2)='1900-01-01' then '0'  when Convert(Date,Day2)='1900-01-01'"
        sql &= "  then '1' else '2' end)EventDays,"
        ' sql &= " Event,City,Client,ActivityType,Status"
        sql &= " month(Date)M1,Year(Date)Y1,Day(Date)D1,month(Day2)M2,Year(Day2)Y2,Day(Day2)D2,Title= REPLACE(REPLACE(Event + ', ' + City + ', ' + Client + ', ' + ActivityType, CHAR(13), ''), CHAR(10), '') "
        sql &=" from [dbo].[Apex_TravelPlan]"
        sql &= " ) A where A.eventdays <>0"

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

End Class
