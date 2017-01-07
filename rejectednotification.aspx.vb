Imports clsDatabaseHelper
Imports clsMain
Imports System.Data
Imports clsApex

Partial Class rejectednotification
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Len(Request.QueryString("bid")) > 0 Then
                Dim sql As String = ""
                Dim capex As New clsApex
                If Len(Request.QueryString("nid")) > 0 Then
                    hdnnotificationID.Value = Request.QueryString("nid")
                    capex.UpdateRecieptentSeen(hdnnotificationID.Value, getLoggedUserID())

                End If

                hdnBriefID.Value = Request.QueryString("bid")
                If hdnBriefID.Value <> "" Then
                    BindFinalEstimateGrid()
                End If

                If CheckNotification() = True Then
                    Dim capex1 As New clsApex
                    capex1.UpdateRecieptentSeen(hdnnotificationID.Value, getLoggedUserID())

                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub BindFinalEstimateGrid()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            sql = "Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,* from  "
            sql &= " APEX_TempEstimate where RefBriefID= " & hdnBriefID.Value & " "
            sql &= "  union  "
            sql &= " Select ROW_NUMBER() OVER(ORDER BY refEstimateID DESC) AS Row,*"
            sql &= " from APEX_EstimateCost where RefBriefID= " & hdnBriefID.Value & " "
            sql &= " order by refEstimateID Asc"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvDisplay.DataSource = ds
                gvDisplay.DataBind()
            End If

            FillOtherDetails()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub FillOtherDetails()
        Try
            Dim sql As String = "Select SUM(Estimate) as SubTotal,EstimatedSubTotal,EstimatedManagentFees,EstimatedTotal,EstimatedGrandTotal,EstimatedServiceTax "
            sql &= " from APEX_TempEstimate as te"
            sql &= "  Join APEX_Estimate as et on et.EstimateID = te.RefEstimateID"
            sql &= " where et.RefBriefID = " & hdnBriefID.Value
            sql &= " Group By EstimatedSubTotal,EstimatedManagentFees,EstimatedTotal,EstimatedGrandTotal,EstimatedServiceTax"



            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)

            Dim mfee As Double = 0
            Dim stax As Double = 0

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("SubTotal").ToString().Trim() <> "" Then
                    lblSubTotal.Text = ds.Tables(0).Rows(0)("SubTotal").ToString().Trim()
                Else
                    lblSubTotal.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString()
                End If

                If ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString() <> "" And ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString() <> "" Then
                    txtMFeePer.Text = ((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString()) / Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString())) * 100).ToString()
                    mfee = Convert.ToDouble(txtMFeePer.Text)
                    lblMFeePer.Text = Math.Round(mfee, 2)
                End If

                If ds.Tables(0).Rows(0)("EstimatedGrandTotal").ToString() <> "" And ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString() <> "" Then
                    txtServiceTax.Text = ((Convert.ToDouble(ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString()) / ds.Tables(0).Rows(0)("EstimatedTotal").ToString()) * 100).ToString()
                    stax = Convert.ToDouble(txtServiceTax.Text)
                    lblServiceTax.Text = Math.Round(stax, 2)
                    txtServiceTax.Text = lblServiceTax.Text
                End If

                If lblSubTotal.Text <> "" Then
                    txtMangnmtFees.Text = Math.Round((Convert.ToDouble(lblSubTotal.Text) * mfee) / 100, 2)
                End If

                If lblSubTotal.Text <> "" And txtMangnmtFees.Text <> "" Then
                    lblTotal.Text = Math.Round(Convert.ToDouble(lblSubTotal.Text) + Convert.ToDouble(txtMangnmtFees.Text), 2)
                End If

                If lblTotal.Text <> "" Then
                    lblServiceTax.Text = Math.Round((Convert.ToDouble(lblTotal.Text) * stax) / 100, 2)
                End If

                If lblTotal.Text <> "" And lblServiceTax.Text <> "" Then
                    lblGrandTotal.Text = Math.Round(Convert.ToDouble(lblTotal.Text) + Convert.ToDouble(lblServiceTax.Text), 2)
                End If

                lblMangnmtFees.Text = txtMangnmtFees.Text
                lblServiceTaxPer.Text = txtServiceTax.Text
                lblMFeePer.Text = txtMFeePer.Text
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancelNotification_Click(sender As Object, e As EventArgs) Handles btnCancelNotification.Click
        Try
            Dim APX As New clsApex
            If hdnnotificationID.Value <> "" Then
                APX.UpdateRecieptentExecuted(hdnnotificationID.Value, getLoggedUserID())
                Response.Redirect("home.aspx")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        Try
            Response.Redirect("home.aspx")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnEditEstimation_Click(sender As Object, e As EventArgs) Handles btnEditEstimation.Click
        Try
            Dim capex As New clsApex
            capex.UpdateRecieptentExecuted(hdnnotificationID.Value, getLoggedUserID())
            Dim sql As String = "update APEX_Estimate set "
            sql &= " IsOperationHeadApproved = 'N'"
            sql &= ",OperationHeadApprovedOn = getdate()"
            sql &= ",IsBranchHeadApproved = 'N'"
            sql &= ",BranchHeadApprovedOn = getdate()"
            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("Estimate.aspx?bid=" & hdnBriefID.Value & "&mode=edit")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Public Function CheckNotification() As Boolean
        Dim result As Boolean = False
        Dim sql As String = "select * from APEX_Notifications "
        sql &= " inner join APEX_Recieptents on RefNotificationID=NotificationID"
        sql &= " where type=10 and AssociateID=" & hdnBriefID.Value & " and IsExecuted='N' and UserID=" & getLoggedUserID()
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnnotificationID.Value = ds.Tables(0).Rows(0)("NotificationID").ToString()
            result = True
        End If
        Return result
    End Function
End Class
