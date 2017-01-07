Imports System.Data
Imports clsDatabaseHelper
Imports clsMain
Imports System.IO
Imports System.Drawing

Partial Class RptEstimateVsPnL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            getdata()
            'txtfromdate.Attributes.Add("readonly", "true")
            'txttodate.Attributes.Add("readonly", "true")
        End If
    End Sub

    Private Sub getdata()
        Try
            Dim sql As String = ""
            sql &= ""
            sql &= " select * from ("
            sql &= "  select  JobCardNo,Client,isnull(FirstName,'') + ' ' + isnull(LastName,'') as KAM,JobCardName   , "
            sql &= "  ProjectType,TE.category,convert(varchar(10),ActivityStartDate,105) as    StartDate, "
            sql &= "  convert(varchar(10),ActivityEndDate,105) as EndDate,      "
            sql &= "  JobStatus = case when isnull(IsEstimateVsActuals,'N') = 'Y' then 'Closed' Else 'Open' End, "
            sql &= "  Cast(sum(isnull(((case when isnull(IsEstimateVsActuals,'N') = 'N' then (isnull(TE.Estimate,0))  "
            sql &= "  else (isnull((TE.Actual),0)) end))*((TE.AgencyFee)/100),0) + ((case when isnull(IsEstimateVsActuals,'N') = 'N'  "
            sql &= "  then (isnull(TE.Estimate,0))  "
            sql &= "  else (isnull((TE.Actual),0)) end))) As numeric(18,2)) NetRevenueActual "
            sql &= "  ,(select sum(posteventcost) from [dbo].[apex_temppostpnlcost] pr where pr.refbriefID=b.BriefID and category=TE.category  )posteventTotal,j.primaryActivityID "
            sql &= "  from APEX_JobCard as j  	 		   "
            sql &= "  inner join APEX_Clients as c on j.RefClientID =c.ClientID  "
            sql &= "  inner join APEX_Brief as b on j.RefBriefID =  b.BriefID       		    "
            sql &= "  inner join APEX_UsersDetails as u on b.InsertedBy = u.UserDetailsID   	 		    "
            sql &= "  Inner Join APEX_ActivityType as a on a.ProjectTypeID = j.PrimaryActivityID  		    "
            sql &= "  inner join APEX_Estimate as e on e.RefBriefID = b.BriefID "
            sql &= "  inner join APEX_TempEstimate as TE on  b.BriefID=TE.RefBriefID 	        "
            sql &= "   where  e.EstimatedGrandTotal is not NUll and j.JobCardNo is not NUll        "
            sql &= "  and isnull(IsEstimateVsActuals,'N') = 'Y' "
            'If txtfromdate.Text.Trim() <> "" And txttodate.Text.Trim() <> "" Then
            '    sql &= "  and convert(datetime,ActivityStartDate,105)  "
            '    sql &= " between convert(datetime,'" & txtfromdate.Text & "',105) and convert(datetime,'" & txttodate.Text & "',105) "
            'End If
            'sql &= "  and convert(datetime,ActivityStartDate,105)  "
            'sql &= "  between convert(datetime,'01-05-2015 00:00:00.000',105) and convert(datetime,'31-05-2015 00:00:00.000',105) "
            sql &= "  group by  JobCardNo,Client,isnull(FirstName,'') + ' ' + isnull(LastName,''),JobCardName, "
            sql &= "  ProjectType,TE.category,convert(varchar(10),ActivityStartDate,105), "
            sql &= "  convert(varchar(10),ActivityEndDate,105),IsEstimateVsActuals,b.briefID,j.primaryActivityID "
            sql &= "  "
            sql &= "  union all"
            sql &= ""
            sql &= "  select  JobCardNo,Client,isnull(FirstName,'') + ' ' + isnull(LastName,'') as KAM,JobCardName   ,"
            sql &= "  ProjectType,TE.category,convert(varchar(10),ActivityStartDate,105) as    StartDate,"
            sql &= "   	convert(varchar(10),ActivityEndDate,105) as EndDate,     "
            sql &= "  JobStatus = case when isnull(IsEstimateVsActuals,'N') = 'Y' then 'Closed' Else 'Open' End,"
            sql &= "  Cast( sum(isnull(((case when isnull(IsEstimateVsActuals,'N') = 'N' then (isnull(TE.Estimate,0))  "
            sql &= "  else (isnull((TE.Actual),0)) end))*((TE.AgencyFee)/100),0) + ((case when isnull(IsEstimateVsActuals,'N') = 'N'  "
            sql &= "  then (isnull(TE.Estimate,0))  "
            sql &= "  else (isnull((TE.Actual),0)) end))) As numeric(18,2)) NetRevenue "
            sql &= "  ,(select sum(preeventcost) from [dbo].[APEX_PrePnLcost] pr where pr.refbriefID=b.BriefID and category=TE.category  )posteventTotal,j.primaryActivityID "
            sql &= "    from APEX_JobCard as j  	 		   "
            sql &= "  inner join APEX_Clients as c on j.RefClientID =c.ClientID  "
            sql &= "  inner join APEX_Brief as b on j.RefBriefID =  b.BriefID       		    "
            sql &= "  inner join APEX_UsersDetails as u on b.InsertedBy = u.UserDetailsID   	 		    "
            sql &= "  Inner Join APEX_ActivityType as a on a.ProjectTypeID = j.PrimaryActivityID  		    "
            sql &= "  inner join APEX_Estimate as e on e.RefBriefID = b.BriefID "
            sql &= "  inner join APEX_TempEstimate as TE on  b.BriefID=TE.RefBriefID 	        "
            sql &= "  "
            sql &= "  where  e.EstimatedGrandTotal is not NUll and j.JobCardNo is not NUll        "
            sql &= "  and isnull(IsEstimateVsActuals,'N') = 'N' "
            sql &= " group by  JobCardNo,Client,isnull(FirstName,'') + ' ' + isnull(LastName,''),JobCardName, "
            sql &= " ProjectType,TE.category,convert(varchar(10),ActivityStartDate,105), "
            sql &= " convert(varchar(10),ActivityEndDate,105),IsEstimateVsActuals,b.briefID,j.primaryActivityID) A Where 1=1"

            If ddlsearchcat.SelectedValue <> "0" Then
                sql &= " and  jobstatus='" & ddlsearchcat.SelectedValue & "'"
            End If

            If txtfromdate.Text.Trim() <> "" And txttodate.Text.Trim() <> "" Then
                sql &= "  and convert(datetime,StartDate,105)  "
                sql &= " between convert(datetime,'" & txtfromdate.Text & "',105) and convert(datetime,'" & txttodate.Text & "',105) "
            End If

            Dim capex As New clsApex
            Dim role As String = capex.GetBHRoleNameByUserID(getLoggedUserID())

            sql &= " and primaryActivityID='" & role & "' "
            sql &= " order by jobcardno,category "
            sql &= " "
            sql &= "  "
            sql &= " "
            sql &= ""
            sql &= ""
            sql &= ""
            sql &= ""
            sql &= ""
            sql &= ""
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            grdDeal.DataSource = ds
            grdDeal.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered

    End Sub
    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            grdDeal.AllowPaging = False
            Me.getdata()

            grdDeal.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In grdDeal.HeaderRow.Cells
                cell.BackColor = grdDeal.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In grdDeal.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = grdDeal.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = grdDeal.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            grdDeal.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub

    Protected Sub grdDeal_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdDeal.PageIndexChanging
        Try
            grdDeal.PageIndex = e.NewPageIndex
            getdata()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        getdata()
    End Sub

End Class
