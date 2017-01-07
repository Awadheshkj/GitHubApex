Imports clsDatabaseHelper
Imports clsMain
Imports System.Data
Imports clsApex

Partial Class AjaxCalls_AX_TravelPlan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim result As String = ""
        If Len(Request.QueryString("call")) > 0 Then
            If Request.QueryString("call").ToString() <> "" Then
                Dim callid As String = Request.QueryString("call").ToString()
                If callid = "1" Then
                    result = BindTravelPlan()
                End If
                If callid = "2" Then
                    result = BindTravelPlancalandar()
                End If
            End If

        End If

        Response.Write(result)
        Response.End()
    End Sub

    Private Function BindTravelPlan() As String
        Dim jsonstring As String = ""
        Dim ds As New DataSet
        Dim sql As String = "Select Month, Client, Region, REPLACE(REPLACE(Event, CHAR(13), ''), CHAR(10), '')Event, KAM, City, Date, Day2, Day3, JCNumber, PM, EM, Venue, ActivityType, Status from [Apex_TravelPlan] "

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            jsonstring = GetJSONString(ds.Tables(0))
            Return jsonstring

        End If

        Return jsonstring

    End Function

    Private Function BindTravelPlancalandar() As String
        Dim jsonstring As String = ""
        Dim ds As New DataSet
        Dim sql As String = "Select tOP 1 Event,  Date, Day2, Day3 from [Apex_TravelPlan] "

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            jsonstring = GetJSONString(ds.Tables(0))
            Return jsonstring

        End If

        Return jsonstring

    End Function
End Class
