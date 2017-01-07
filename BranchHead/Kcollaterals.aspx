<%@ Page Title="" Language="VB" MasterPageFile="~/BranchHead/Apex.master" AutoEventWireup="false" CodeFile="Kcollaterals.aspx.vb" Inherits="Kcollaterals" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        $(document).ready(function () {
            //$("#Brief_Collatrals_status").hide();
            $("#Estimate_Collatrals_status").hide();
            $("#Project_Collatrals_status").hide();
            $("#claim_Collatrals_status").hide();
            $("#other_Collatrals_status1").hide();


            $("#mainBrief_Collatrals").click(function () {
                $("#Brief_Collatrals_status").slideToggle(600);

            });
            $("#mainclaim_Collatrals").click(function () {
                $("#claim_Collatrals_status").slideToggle(600);

            });
            $("#mainEstimate_Collatrals").click(function () {
                $("#Estimate_Collatrals_status").slideToggle(600);
            });
            $("#mainProject_Collatrals").click(function () {
                $("#Project_Collatrals_status").slideToggle(600);
            });
            $("#mainother_Collatrals").click(function () {
                $("#other_Collatrals_status").slideToggle(600);
            });

        });



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
       <%-- <h1>Collateral Center</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Collateral Center</li>
        </ol>--%>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Collateral Center (Job Code :
                            <asp:Label ID="lbljc" runat="server" Text=""></asp:Label>)</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <asp:HiddenField ID="hdnBriefID" runat="server" />
                        <div class="col-lg-12">
                            <h3 style="color: blue;">Client Brief Collaterals </h3>
                            <div id="Brief_Collatrals_status">
                                <asp:GridView ID="gvFileUploads" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered">
                                    <EmptyDataTemplate>

                                        <h5>Data Not Available...</h5>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="50px">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                                <asp:HiddenField ID="hdnColletralID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CollateralID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collateral Name" ItemStyle-Width="450px">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "CollateralName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uploaded By" ItemStyle-Width="150px">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Name")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Upload Date" ItemStyle-Width="150px">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Insertedon")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                            <ItemTemplate>
                                                <a href="<%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>" target="_blank">
                                                    <input type="button" class="btn-sm btn-primary" value="View" />
                                                    <%-- <%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>--%>
                                                </a>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete" Visible="false" CssClass="btn-sm btn-primary" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <h3 style="color: blue;">Client Approval Doc </h3>
                            <div id="Estimate_Collatrals_status">
                                <div id="estemategridata" runat="server" visible="false">
                                    <h5>Data Not Available...</h5>
                                </div>
                                <asp:GridView ID="Gridestimate" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered">
                                    <EmptyDataTemplate>

                                        <h5>Data Not Available...</h5>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="50px">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collateral Name" ItemStyle-Width="450px">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Name")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uploaded By" ItemStyle-Width="150px">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "upName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Upload Date" ItemStyle-Width="150px">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Insertedon")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                            <ItemTemplate>
                                                <a href="<%# DataBinder.Eval(Container.DataItem, "ApprovalMail")%>" target="_blank">
                                                    <input type="button" class="btn-sm btn-primary" value="View" />
                                                    <%-- <%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>--%>
                                                </a>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete" Visible="false" CssClass="btn-sm btn-primary" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <h3 style="color: blue;">Claim Collaterals</h3>
                            <div id="claim_Collatrals_status">
                                <div style="overflow: auto;">
                                    <asp:GridView ID="gvclaims" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered">
                                        <EmptyDataTemplate>
                                            <h5>Data Not Available...</h5>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                                    <%-- <asp:HiddenField ID="hdnColletralID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CollateralID")%>' />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Collateral Name" ItemStyle-Width="450px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "CollateralName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uploaded By" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Date" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Insertedon")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <a href="<%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>" target="_blank">
                                                        <input type="button" class="btn-sm btn-primary" value="View" /></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-12">
                            <h3 style="color: blue;">Project Collaterals </h3>
                            <div id="Project_Collatrals_status">
                                <div style="height: 300px; overflow: auto;">
                                    <asp:GridView ID="gvprojectcollatrals" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered">
                                        <EmptyDataTemplate>

                                            <h5>Data Not Available...</h5>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                                    <asp:HiddenField ID="hdnColletralID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Task">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Task")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Task">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Particulars")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost Center">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "category")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Collateral Name" ItemStyle-Width="450px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "FileName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uploaded By" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Date" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "uploadon")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <a href="<%# DataBinder.Eval(Container.DataItem, "filePath")%>" target="_blank">
                                                        <input type="button" class="btn-sm btn-primary" value="View" />
                                                        <%-- <%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>--%>
                                                    </a>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete" Visible="false" CssClass="btn-sm btn-primary" OnClientClick="return confirm('Are you sure to Delete?');" CausesValidation="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>


                            </div>
                        </div>
                        <div class="col-lg-12">
                            <table style="width: 100%">
                                <tr>
                                    <td width="40%" id="mainother_Collatrals">
                                        <h3 style="color: blue;">Other Collaterals </h3>
                                    </td>
                                    <td><strong>Title:</strong></td>
                                    <td>
                                        <asp:TextBox ID="txtUploads" runat="server" MaxLength="40" placeholder="Title"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtUploads" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator></td>
                                    <td width="24%">
                                        <asp:FileUpload ID="FUpld_Documents" runat="server" onchange="CheckUpl();" /></td>
                                    <td>
                                        <%--<asp:Button ID="btnUpload" runat="server" Text="Upload"   OnClientClick="return checkVal()" />--%>
                                        <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload" />
                                    </td>
                                </tr>
                            </table>
                            <div id="other_Collatrals_status">
                                <div style="height: 300px; overflow: auto;">
                                    <asp:GridView ID="GVothercollatrals" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered">
                                        <EmptyDataTemplate>

                                            <h5>Data Not Available...</h5>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                                    <asp:HiddenField ID="hdnColletralID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CollateralID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Collateral Name" ItemStyle-Width="450px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "CollateralName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uploaded By" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Date" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Insertedon")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <a href="<%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>" target="_blank">
                                                        <input type="button" class="btn-sm btn-primary" value="View" />
                                                        <%-- <%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>--%>
                                                    </a>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete" Visible="true" OnClientClick="return confirm('Are you sure to Delete?');" CssClass="btn-sm btn-primary" CausesValidation="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>


                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label class="col-lg-1 control-label" for="head"></label>
                                <div class="navbards col-lg-10" style="text-align: center;">
                                    <a href='javascript:history.go(-1)' class="btn btn-primary">Back</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>

