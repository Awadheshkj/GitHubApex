Imports clsMain
Imports clsApex
Imports clsDatabaseHelper
Imports System.Data

Partial Class JobDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Len(Request.QueryString("jid")) > 0 Then
                Dim jobid As String = Request.QueryString("jid")
                loadmainDetail(jobid)
                bindtask(jobid)
                BindAccountDetail(jobid)
                BindColletral(jobid)
            End If
        End If

    End Sub

    Private Sub loadmainDetail(ByVal jobid As String)
        Dim sql As String = ""
        sql = " Select jc.jobcardName,jc.JobCardNo,c.client,(ud.FirstName+' '+ isnull(ud.LastName,'')) as 'kamName'"
        sql &= " ,(udt.FirstName+ ' ' + isnull(udt.LastName,'')) as 'ProjectManager', convert(varchar(20),jc.ActivityStartDAte,103) as ActivityStartDAte, convert(varchar(20),jc.ActivityEndDAte,103) as ActivityEndDAte "
        sql &= "   ,jc.IsBriefed,jc.isEstimated,jc.isprepnl,jc.IspostPnl,jc.jobCompleted"
        sql &= " ,b.Budget,es.EstimatedGrandtotal from APEX_jobcard      "
        sql &= " as jc join dbo.APEX_Clients as c on jc.refclientid=c.clientid"
        ' sql &= " join dbo.APEX_UsersDetails as ud on jc.KAMID=ud.UserDetailsID"
        sql &= " join dbo.APEX_UsersDetails as udt on jc.ProjectManagerID=udt.UserDetailsID"
        sql &= "  left outer join APEX_Brief as b on jc.RefBriefID=b.BriefID"
        sql &= " join dbo.APEX_UsersDetails as ud on b.Insertedby=ud.UserDetailsID"
        sql &= " left outer join dbo.APEX_Estimate  as es on b.briefID=es.refBriefID"
        sql &= " where  jc.jobcardid=" & jobid

        Dim ds As DataSet = Nothing
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("jobcardName")) Then
                lbljobtitel.Text = ds.Tables(0).Rows(0)("jobcardName").ToString()
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("JobCardNo")) Then
                lbljobcode.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("client")) Then
                lblclient.Text = ds.Tables(0).Rows(0)("client").ToString()
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("kamName")) Then
                lblcam.Text = ds.Tables(0).Rows(0)("kamName").ToString()
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("ProjectManager")) Then
                lblpm.Text = ds.Tables(0).Rows(0)("ProjectManager").ToString()
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("ActivityStartDAte")) Then
                lblStartdate.Text = ds.Tables(0).Rows(0)("ActivityStartDAte").ToString()
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("ActivityEndDAte")) Then
                lblEnddate.Text = ds.Tables(0).Rows(0)("ActivityEndDAte").ToString()
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Budget")) Then
                lblTotalbudget.Text = ds.Tables(0).Rows(0)("Budget").ToString()
                If Not IsDBNull(ds.Tables(0).Rows(0)("EstimatedGrandtotal")) Then

                    Dim var As Double = ds.Tables(0).Rows(0)("Budget") - ds.Tables(0).Rows(0)("EstimatedGrandtotal")

                    Dim per As Double = (var / lblTotalbudget.Text) * 100
                    lblprofit.Text = Math.Round(per, 2)
                Else

                    lblprofit.Text = "N/A"


                End If
            Else
                lblTotalbudget.Text = "N/A"
                lblprofit.Text = "N/A"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("IsBriefed")) Then
                lblBrief.Text = ds.Tables(0).Rows(0)("IsBriefed").ToString()

            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("isEstimated")) Then
                lblEstimated.Text = ds.Tables(0).Rows(0)("isEstimated").ToString()
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("isprepnl")) Then
                lblPrepnl.Text = ds.Tables(0).Rows(0)("isprepnl").ToString()
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("IspostPnl")) Then
                lblPostPnl.Text = ds.Tables(0).Rows(0)("IspostPnl").ToString()
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("jobCompleted")) Then
                lbljobComplete.Text = ds.Tables(0).Rows(0)("jobCompleted").ToString()
            End If



        End If

    End Sub

    Private Sub bindtask(ByVal jid As String)
        Dim sql As String = ""
        sql = "Select taskID,title, isnull(taskCompleted,'N') as taskstatus from dbo.APEX_Task  where refjobCardid = " & jid
        Dim ds As DataSet = Nothing
        ds = ExecuteDataSet(sql)

        If ds.Tables(0).Rows.Count > 0 Then
            gvtasks.DataSource = ds.Tables(0)
            gvtasks.DataBind()

        End If

    End Sub
    Private Sub BindColletral(ByVal jid As String)
        BindBriefColetral(jid)
        BindTaskColetral(jid)
        BindSubTaskColetral(jid)

    End Sub

    Private Sub BindBriefColetral(ByVal jid As String)
        Dim sql As String = ""
        Dim ds As DataSet = Nothing

        sql = " Select jc.jobCardID, b.BriefID ,cc.CollateralName,cc.CollateralPath from dbo.APEX_JobCard as jc"
        sql &= "  join APEX_Brief as b on jc.RefBriefID=b.BriefID"
        sql &= "   join APEX_CollateralCenter as cc on cc.CollateralTypeID=b.BriefID"
        sql &= " where  jc.jobCardID=" & jid & "  order by cc.CollateralType  asc "

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gvBriefColletral.DataSource = ds.Tables(0)
            gvBriefColletral.DataBind()
        Else
            gvBriefColletral.EmptyDataText = "Data Not Found"
            gvBriefColletral.DataBind()
        End If
       
    End Sub
    Private Sub BindTaskColetral(ByVal jid As String)
        Dim sql As String = ""
        Dim ds As DataSet = Nothing
        sql = "Select jc.jobCardID,t.taskID ,cc.CollateralName,cc.CollateralPath from dbo.APEX_JobCard as jc"
        sql &= "    join  dbo.APEX_Task as t on jc.jobCardID=t.RefjobcardID "
        sql &= " join APEX_CollateralCenter as cc on cc.CollateralTypeID=t.taskID "
        sql &= " where  jc.jobCardID=" & jid & "  order by cc.CollateralType  asc "

      
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gvTaskColletral.DataSource = ds.Tables(0)
            gvTaskColletral.DataBind()
        Else
            gvTaskColletral.EmptyDataText = "Data Not Found"
            gvTaskColletral.DataBind()
        End If
        
    End Sub

    Private Sub BindSubTaskColetral(ByVal jid As String)
        Dim sql As String = ""
        Dim ds As DataSet = Nothing

        sql = " Select jc.jobCardID, t.taskID ,cc.CollateralName,cc.CollateralPath from dbo.APEX_JobCard as jc"
        sql &= "    join  dbo.APEX_Task as t on jc.jobCardID=t.RefjobcardID "
        sql &= " join dbo.APEX_SubTask as st on t.taskid=st.reftaskid"
        sql &= " join APEX_CollateralCenter as cc on cc.CollateralTypeID=st.subtaskid      "
        sql &= " where  jc.jobCardID=" & jid & "  order by cc.CollateralType  asc "

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gvSubTaskColletral.DataSource = ds.Tables(0)
            gvSubTaskColletral.DataBind()
        Else
            gvSubTaskColletral.EmptyDataText = "Data Not Found"
            gvSubTaskColletral.DataBind()
        End If

       
    End Sub

    Private Sub BindAccountDetail(ByVal jid As String)
        Dim sql As String = "Select Particulars,Quantity,Amount,Total "
        sql &= " from dbo.APEX_JobCard as jc"
        sql &= " join  dbo.APEX_Task as t on jc.jobCardID=t.RefjobcardID "
        sql &= " join   APEX_TaskAccount  as ta   on ta.ReftaskID=t.taskID"
        sql &= " where  jc.jobCardID=" & jid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gvAccount.DataSource = ds
            gvAccount.DataBind()
        End If

    End Sub



    Protected Sub gvtasks_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvtasks.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hdntaskID As HiddenField = CType(e.Row.FindControl("hdnTaskid"), HiddenField)
            Dim teammembers As Label = CType(e.Row.FindControl("lblTeammembar"), Label)
            Dim lblteamlead As Label = CType(e.Row.FindControl("lblteamlead"), Label)
            Dim sql As String = ""
            teammembers.Text = ""

            sql = "Select RefmemberID,refleadID,"
            sql &= " (Select (isnull(firstname,'')+' '+isnull(lastname,'')) "
            sql &= "  from dbo.APEX_UsersDetails where UserDetailsID=refleadID )  as TeamLeader,"
            sql &= "  (Select (isnull(firstname,'')+' '+isnull(lastname,'')) "
            sql &= " from dbo.APEX_UsersDetails where UserDetailsID=RefmemberID ) as TeamMember "
            sql &= " from APEX_Taskteam where reftaskid=" & hdntaskID.Value

            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("TeamLeader")) Then
                    lblteamlead.Text = ds.Tables(0).Rows(0)("TeamLeader")
                End If
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1


                    If Not IsDBNull(ds.Tables(0).Rows(i)("TeamMember")) Then
                        If teammembers.Text <> "" Then
                            teammembers.Text = teammembers.Text & " , " & ds.Tables(0).Rows(i)("TeamMember")
                        Else
                            teammembers.Text = ds.Tables(0).Rows(i)("TeamMember")
                        End If

                    End If
                Next


            End If

        End If
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("JobCard.aspx?mode=rj")
    End Sub
End Class
