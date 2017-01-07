<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false"
    CodeFile="JobDetail.aspx.vb" Inherits="JobDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tabdiv
        {
            width: 150px;
            float: left;
            border: 1px solid red;
            text-align: center;
            margin-left: 20px;
        }

        .tab_innerleft
        {
            width: 50px;
            float: left;
            border-right: 1px solid red;
            padding: 5px 0px 5px 0px;
        }

        .tab_innerRight
        {
            width: 95px;
            float: left;
            padding: 5px 0px 5px 0px;
        }

        #LeadDetail
        {
            width: 100%;
            float: left;
            margin: 15px 0px 5px 20px;
            line-height: 30px;
            border: 1px solid #808080;
            border-radius: 5px;
            width: 58%;
            padding: 10px;
        }

        .category_main
        {
            width: 95%;
            float: left;
            margin: 15px 0px 5px 20px;
            padding: 10px;
            border: 1px solid #808080;
            border-radius: 5px;
        }

        h2
        {
            font-size: 15px;
            color: #bd1919;
            width: 98%;
            margin: 10px;
            cursor: pointer;
        }
    </style>


    <script>
        $(document).ready(function () {
            $("#Task_Status").hide();
            $("#Accountdiv").hide();
            $("#CollateralsDiv").hide();
            $("#mainTask").click(function () {

                $("#Task_Status").slideToggle(600);
                $("#Accountdiv").hide();
                $("#CollateralsDiv").hide();
            });

            $("#mainAccountdiv").click(function () {
                $("#Accountdiv").slideToggle(600);
                $("#Task_Status").hide();
                $("#CollateralsDiv").hide();
            });

            $("#mainCollateralsDiv").click(function () {
                $("#CollateralsDiv").slideToggle(600);
                $("#Accountdiv").hide();
                $("#Task_Status").hide();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <div>
            <div style="font-size: 45px;">
                JC-<asp:Label runat="server" ID="lbljobcode" Text="N/A"></asp:Label>
            </div>
            <div style="font-size: 20px;">
                <asp:Label runat="server" ID="lbljobtitel"></asp:Label>
            </div>
        </div>
    </h1>
    <div class="inner-Content-container">
        <div class="InnerContentData">
            <div>
                <div>
                    <div class="tabdiv">
                        <div class="tab_innerleft">
                            1
                        </div>
                        <div class="tab_innerRight">
                            Event
                        </div>
                    </div>
                    <div class="tabdiv">
                        <div class="tab_innerleft">
                            2
                        </div>
                        <div class="tab_innerRight">
                            C.E.P
                        </div>
                    </div>
                    <div class="tabdiv">
                        <div class="tab_innerleft">
                            3
                        </div>
                        <div class="tab_innerRight">
                            Digital
                        </div>
                    </div>
                </div>
                <div id="LeadDetail">
                    <table>
                        <tr>
                            <td>Client
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label runat="server" ID="lblclient" Text="N/A"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>K.A.M.
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label runat="server" ID="lblcam" Text="N/A"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>P.M.
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label runat="server" ID="lblpm" Text="N/A"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>Start date
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label runat="server" ID="lblStartdate" Text="N/A"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>End date
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label runat="server" ID="lblEnddate" Text="N/A"></asp:Label>
                            </td>
                        </tr>
                        <asp:Label runat="server" ID="lblTotalbudget" Text="N/A" Visible="false" ></asp:Label>
                        <asp:Label runat="server" ID="lblprofit" Text="N/A" Visible="false"> </asp:Label>
                        <%--<tr>
                            <td>Total budget
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label runat="server" ID="lblTotalbudget" Text="N/A"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Profit (%)
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label runat="server" ID="lblprofit" Text="N/A"></asp:Label>
                            </td>
                        </tr>--%>
                    </table>
                </div>
                <div class="category_main" id="mainTask">

                    <h2>Task Status & Check List</h2>
                    <div id="Task_Status">
                        <table>
                            <tr>
                                <td>Is Brief Completed
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="lblBrief" Text="N"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Is Estimate Completed
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="lblEstimated" Text="N"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Is Pre P&L Completed
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="lblPrepnl" Text="N"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>Is Post P&L Completed
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="lblPostPnl" Text="N"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>Is Job Completed
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="lbljobComplete" Text="N"></asp:Label>
                                </td>
                            </tr>


                        </table>



                        <asp:GridView ID="gvtasks" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" Width="90%" AllowPaging="true" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="Row">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                        <asp:HiddenField ID="hdnTaskid" runat="server" Value='<%#Bind("taskID")%>' />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Task Name">
                                    <ItemTemplate>
                                        <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                        <%# DataBinder.Eval(Container.DataItem, "title")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Team Leader">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblteamlead"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Team Member">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTeammembar"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Is Completed">
                                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "taskstatus")%></ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
                <div class="category_main" id="mainAccountdiv">
                    <h2>Account Status</h2>
                    <div id="Accountdiv">
                        <asp:GridView ID="gvAccount" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" Width="90%" AllowPaging="true" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Particulars">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Particulars")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Amount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Total")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="category_main" id="mainCollateralsDiv">
                    <h2>Collaterals</h2>
                    <div id="CollateralsDiv">
                        <b>Brief Collaterals</b>
                        <asp:GridView ID="gvBriefColletral" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" Width="90%" AllowPaging="true" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Collateral Name">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "CollateralName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <a href='<%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>'>Download</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Task Collaterals</b>
                        <asp:GridView ID="gvTaskColletral" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" Width="90%" AllowPaging="true" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Collateral Name">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "CollateralName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <a href='<%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>'>Download</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Sub Task Collaterals</b>
                        <asp:GridView ID="gvSubTaskColletral" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" Width="90%" AllowPaging="true" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Collateral Name">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "CollateralName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <a href='<%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>'>Download</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="category_main" style="text-align:center;border:none;">
                <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="small-button" />
            </div>
            </div>
            
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>
