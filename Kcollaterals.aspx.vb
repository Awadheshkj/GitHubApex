Imports System.Data
Imports clsDatabaseHelper
Imports clsMain
Imports clsApex
Partial Class Kcollaterals
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

                Dim capex As New clsApex
                If Len(Request.QueryString("jid")) > 0 Then
                    If Request.QueryString("jid") <> Nothing Then
                        If Request.QueryString("jid").ToString() <> "" Then
                            hdnBriefID.Value = capex.GetBriefIDByJobCardID(Request.QueryString("jid"))
                        End If
                    Else
                        hdnBriefID.Value = Request.QueryString("bid").ToString()
                    End If
                Else
                    hdnBriefID.Value = Request.QueryString("bid").ToString()
                End If
                'hdnBriefID.Value = Request.QueryString("bid").ToString()
                Dim jobcard As String = capex.GetJobCardIDByBriefID(hdnBriefID.Value)
                lbljc.Text = "#" & capex.GetJobCardNoByJobCardID(jobcard)
                BindColletral()
                BindEstimateColletral()
                BindColletralJC(Request.QueryString("jid").ToString())
                BindclaimColletral(Request.QueryString("jid").ToString())
                BindOtherColletral()
            Else
                'CallDivError()
            End If

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindColletral()
        Try
            Dim sql As String = "select ROW_NUMBER() OVER(ORDER BY CollateralID DESC) AS Row,CollateralID,CollateralName,CollateralPath,convert(varchar,Insertedon,5)Insertedon,(select firstname + ' ' + isnull(lastname,'') from apex_usersdetails where UserdetailsID=APEX_CollateralCenter.Insertedby)name from APEX_CollateralCenter where CollateralType = 2 and CollateralTypeID =" & hdnBriefID.Value
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
                gvFileUploads.DataSource = ds
                gvFileUploads.DataBind()
            End If

            'gvFileDisplay.DataSource = ds
            'gvFileDisplay.DataBind()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindEstimateColletral()
        Try
            Dim sql As String = "select ROW_NUMBER() OVER(ORDER BY JobCardID DESC) AS Row,ISNULL(ApprovalMail,'')ApprovalMail,ISNULL(ApprovedBy,0)ApprovedBy, SUBSTRING(ApprovalMail,20,200)Name"
            sql &= "  ,(Select (isnull(Firstname,'')+' '+ isnull(Lastname,'')) from APEX_UsersDetails where UserDetailsID= b.insertedBy)  as upName,"
            sql &= " convert(varchar,jc.Insertedon,5)Insertedon from APEX_JobCard jc"
            sql &= " join APEX_Brief as b on jc.RefBriefID=b.BriefID"
            sql &= "  where JC.JobCardID =" & Request.QueryString("jid").ToString()
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                '    Dim epass As String = ds.Tables(0).Rows(i)("CollateralPath")
                '    ds.Tables(0).Rows(i).Item("CollateralPath") = Clean(epass)
                'Next
                If ds.Tables(0).Rows(0)("ApprovalMail") <> "" Then
                    Gridestimate.DataSource = ds
                    Gridestimate.DataBind()
                    estemategridata.Visible = False
                Else
                    estemategridata.Visible = True
                End If

            Else
                estemategridata.Visible = False
                Gridestimate.DataSource = ds
                Gridestimate.DataBind()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub BindColletralJC(ByVal JCID As String)
        Try
            Dim sql As String = ""
            'select ROW_NUMBER() OVER(ORDER BY CollateralID DESC) AS Row,CollateralID,CollateralName,CollateralPath from APEX_CollateralCenter where CollateralType = 2 and CollateralTypeID =" & hdnBriefID.Value
            'sql &= " select ROW_NUMBER() OVER(ORDER BY ID DESC) AS Row,ID, RefTaskID, ReftaskaccountID, RefTaskhistoryID, FileName, fileExt, filePath, IsActive, IsDeleted,"
            'sql &= " Inserted_on, Inserted_by, RefJCnumber, Type from [dbo].[Apex_TaskCollaterals] where RefTaskhistoryID=" & taskhistoryID
            sql &= " select ROW_NUMBER() OVER(ORDER BY ID DESC) AS Row,(select Title from APEX_Task where TaskID=TCH.RefTaskID)Task,TA.Particulars,TA.category,"
            sql &= " Name = (select Incharge  from [Apex_TaskHistory] where CheckListID =TCH.RefTaskhistoryID ) ,TCH.* ,convert(varchar(20),TCH.Inserted_on,5)uploadon"
            sql &= " from [dbo].[Apex_TaskCollaterals] TCH"
            sql &= " inner join APEX_TaskAccount TA on TCH.ReftaskaccountID =TA.AccountID "
            sql &= " where  TCH.IsDeleted ='N' and tch.RefJCnumber =" & JCID
            sql &= ""
            sql &= ""
            sql &= ""
            sql &= ""


            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                '    Dim epass As String = ds.Tables(0).Rows(i)("CollateralPath")
                '    ds.Tables(0).Rows(i).Item("CollateralPath") = Clean(epass)
                'Next
                gvprojectcollatrals.DataSource = ds
                gvprojectcollatrals.DataBind()
            Else
                gvprojectcollatrals.DataSource = ds
                gvprojectcollatrals.DataBind()
            End If
            'gvFileDisplay.DataSource = ds
            'gvFileDisplay.DataBind()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub BindclaimColletral(ByVal JCID As String)
        Try
            Dim sql As String = "select ROW_NUMBER() OVER(ORDER BY claimmasterID DESC) AS Row,ClaimFile as CollateralName,ClaimFile as CollateralPath,convert(varchar,Insertedon,5)Insertedon,(select firstname + ' ' + isnull(lastname,'') from apex_usersdetails where UserdetailsID=APEX_ClaimMaster.Insertedby)name from APEX_ClaimMaster where jobcardNo=" & JCID & " and isnull(ClaimFile,'') <>''"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim epass As String = ds.Tables(0).Rows(i)("CollateralPath")
                    ds.Tables(0).Rows(i).Item("CollateralPath") = Clean(epass)
                Next
                gvclaims.DataSource = ds
                gvclaims.DataBind()
            Else
                gvclaims.DataSource = ds
                gvclaims.DataBind()
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
            FUpld_Documents.SaveAs(Server.MapPath("Uploads/Others/" & encname & "." & fext))
            path = "Uploads/Others/" & encname & "." & fext

            Dim sql As String = "INSERT INTO [APEX_CollateralCenter] ([CollateralName]"
            sql &= " ,[CollateralType]"
            sql &= " ,[CollateralTypeID]"
            sql &= " ,[CollateralPath]"
            sql &= " ,[InsertedBy])"
            sql &= " VALUES('" & Clean(txtUploads.Text.ToString().Replace("&", "")) & "'"
            sql &= " ,3"
            sql &= " ," & hdnBriefID.Value
            sql &= " ,'" & path & "'"
            sql &= " ," & getLoggedUserID() & ")"

            If ExecuteNonQuery(sql) > 0 Then
                BindOtherColletral()
                txtUploads.Text = ""
                ' $("#other_Collatrals_status").slideToggle(600);
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub BindOtherColletral()
        Try
            Dim sql As String = "select ROW_NUMBER() OVER(ORDER BY CollateralID DESC) AS Row,CollateralID,CollateralName,CollateralPath,convert(varchar,Insertedon,5)Insertedon,(select firstname + ' ' + isnull(lastname,'') from apex_usersdetails where UserdetailsID=APEX_CollateralCenter.Insertedby)name from APEX_CollateralCenter where CollateralType = 3 and CollateralTypeID =" & hdnBriefID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim epass As String = ds.Tables(0).Rows(i)("CollateralPath")
                    ds.Tables(0).Rows(i).Item("CollateralPath") = Clean(epass)
                Next
                GVothercollatrals.DataSource = ds
                GVothercollatrals.DataBind()
            Else
                GVothercollatrals.DataSource = ds
                GVothercollatrals.DataBind()
            End If

            'gvFileDisplay.DataSource = ds
            'gvFileDisplay.DataBind()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub GVothercollatrals_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GVothercollatrals.RowCommand
        Try
            If e.CommandName = "delete" Then
                Dim hdnColletralID As New HiddenField

                Dim row As GridViewRow = CType(CType(e.CommandSource, Button).NamingContainer, GridViewRow)

                hdnColletralID = CType(row.FindControl("hdnColletralID"), HiddenField)

                Dim sql As String = "Delete from APEX_CollateralCenter where CollateralID=" & hdnColletralID.Value
                If ExecuteNonQuery(sql) > 0 Then
                    BindOtherColletral()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub GVothercollatrals_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GVothercollatrals.RowDeleting

    End Sub
End Class
