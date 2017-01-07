<%@ Page Title="" Language="VB" MasterPageFile="~/apex.master" AutoEventWireup="false" CodeFile="ListofTask.aspx.vb" Inherits="ListofTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        textarea {
            resize: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <%--<h1>List Of Tasks/Check List
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List Of Tasks/Check List</li>
        </ol>--%>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">List Of Tasks/Check List</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">



                        <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>

                        <div class="modal fade" id="dialog-box-task">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">Check list History</h4>
                                    </div>
                                    <div class="modal-body">
                                        <%--    <h4>Check list History</h4>--%>
                                        <div id="statuslist">Loading...</div>

                                    </div>
                                    <div class="modal-footer"></div>
                                </div>
                            </div>
                        </div>
                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <div class="InnerContentData table-responsive">
                                    <div style="text-align: right; width: 90%; display: none;">
                                        <asp:Button ID="btnAdd" runat="server" Text="Create Task" CssClass="btn btn-primary" CausesValidation="false" TabIndex="2" />
                                    </div>
                                    <span class="label" style="background-color: #dff0d8; color:#1e1b1b;">Info</span>
                                    <span class="label" style="background-color: #d9edf7; color:#1e1b1b;">Warning</span>
                                    <span class="label" style="background-color: #f2dede; color:#1e1b1b;">Over Due</span>

                                    <asp:GridView runat="server" ID="gvTasks" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="15" CssClass="table table-bordered table-striped">
                                        <EmptyDataTemplate>
                                            <p>Data Not Available...</p>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnTaskID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "AccountID")%>' />
                                                    <asp:HiddenField ID="hdnRefJobcardID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "RefjobcardID")%>' />
                                                    <asp:HiddenField ID="hdnsts" runat="server" Value='<%# Eval("sts") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Category")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Task">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "TaskName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Task">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Particulars")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Start date">
                                                <ItemTemplate>
                                                    <a href='TaskAccount.aspx?tid=<%# Eval("refjobcardID")%>'></a><%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:dd-MM-yyyy HH:mm}")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End date">
                                                <ItemTemplate>
                                                    <a href='TaskAccount.aspx?tid=<%# Eval("refjobcardID")%>'></a><%# DataBinder.Eval(Container.DataItem, "EndDate", "{0:dd-MM-yyyy HH:mm}")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="200px" ItemStyle-Wrap="true">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Assign To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltaskassign" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                                            <asp:TemplateField HeaderText="Assign Budget">
                                                <ItemTemplate>
                                                    <%# Eval("Total")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status">

                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnstatus" Value='<%# Eval("Status")%>' runat="server" />
                                                    <asp:DropDownList runat="server" ID="ddlStatus" Enabled='<%# IIf(Eval("Total").ToString() = "", "false", "True")%>' Width="80px">
                                                        <asp:ListItem Value="Running">Running</asp:ListItem>
                                                        <asp:ListItem Value="Completed">Completed</asp:ListItem>

                                                    </asp:DropDownList>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtRemarks" runat="server" Enabled='<%# IIf(Eval("Total").ToString() = "", "false", "True")%>' TextMode="MultiLine" MaxLength="200" Text='<%# Eval("Remarks") %>' Width="120" Height="40"></asp:TextBox>


                                                    <asp:HiddenField runat="server" ID="hdninsertedby" Value='<%# Eval("insertedby")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Task complete(%)" HeaderStyle-Width="80px">
                                                <ItemTemplate>
                                                    <%--<asp:TextBox ID="txttaskcomplete" runat="server" Enabled='<%# IIf(Eval("Expence").ToString() = "", "false", "True")%>'  MaxLength="3" Text='<%# Eval("Remarks") %>' Width="50"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlcompletestatus" runat="server">
                                                        <asp:ListItem>0</asp:ListItem>
                                                        <asp:ListItem>5</asp:ListItem>

                                                        <asp:ListItem>10</asp:ListItem>

                                                        <asp:ListItem>15</asp:ListItem>

                                                        <asp:ListItem>20</asp:ListItem>

                                                        <asp:ListItem>25</asp:ListItem>

                                                        <asp:ListItem>30</asp:ListItem>

                                                        <asp:ListItem>35</asp:ListItem>

                                                        <asp:ListItem>40</asp:ListItem>

                                                        <asp:ListItem>45</asp:ListItem>

                                                        <asp:ListItem>50</asp:ListItem>

                                                        <asp:ListItem>55</asp:ListItem>

                                                        <asp:ListItem>60</asp:ListItem>

                                                        <asp:ListItem>65</asp:ListItem>

                                                        <asp:ListItem>70</asp:ListItem>

                                                        <asp:ListItem>75</asp:ListItem>

                                                        <asp:ListItem>80</asp:ListItem>

                                                        <asp:ListItem>85</asp:ListItem>

                                                        <asp:ListItem>90</asp:ListItem>

                                                        <asp:ListItem>95</asp:ListItem>

                                                        <asp:ListItem>100</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdnworkperc" Value='<%# Eval("workstatus")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="150px">
                                                <ItemTemplate>

                                                    <div class="btn btn-sm btn-primary" data-toggle="tooltip" title="Upload" onclick="uploaddata(<%# Container.DataItemIndex + 0%>)" style='display: <%# IIf(Eval("TL").ToString() = clsMain.getLoggedUserID().ToString(), "", "none")%>'><i class="fa fa-upload"></i></div>


                                                    <asp:FileUpload ID="file1" runat="server" Style="display: none" AllowMultiple="true" />

                                                    <%--<asp:LinkButton ID="LinkButton1" CssClass="btn btn-sm btn-primary" runat="server" visible='<%# IIf(Eval("TL").ToString() = clsMain.getLoggedUserID().ToString(), "True", "false")%>' Text='<%# clsMain.getLoggedUserID().ToString() & Eval("TL").ToString() %>' OnClick="btnaddRemarks_Click" CommandArgument='<%# Eval("AccountID") %>'></asp:LinkButton>--%>

                                                    <asp:LinkButton ID="btnaddRemarks" CssClass="btn btn-sm btn-primary" data-toggle="tooltip" ToolTip="Save" runat="server" Visible='<%# IIf(Eval("TL").ToString() = clsMain.getLoggedUserID().ToString(), "True", "false")%>' Text="Save" OnClick="btnaddRemarks_Click" CommandArgument='<%# Eval("AccountID") %>'><i class="fa fa-save" ></i></asp:LinkButton>

                                                    <div class="btn btn-sm btn-primary" data-toggle="tooltip" data-target="#dialog-box-task" title="View History" onclick='javascript:loadHistory(<%# DataBinder.Eval(Container.DataItem, "AccountID")%>)'>

                                                        <i class="fa fa-eye"></i>

                                                    </div>

                                                    <%--<asp:LinkButton ID="LinkButton1" CssClass="btn btn-sm btn-primary" runat="server" visible='<%# IIf(Eval("TL").ToString() = clsMain.getLoggedUserID().ToString(), "True", "false")%>'  PostBackUrl='TaskAccount.aspx?jc=<%# Eval("refjobcardID")%> &tid=<%# Eval("refjobcardID")%>' ToolTip="Edit"><i class="fa fa-edit" ></i></asp:LinkButton>--%>

                                                    <a class="btn btn-sm btn-primary" style="display: <%# IIf(Eval("insertedby").ToString() = clsMain.getLoggedUserID().ToString(), "", "none")%>;" href="TaskAccount.aspx?jc=<%# Eval("refjobcardID")%>&tid=<%# Eval("refjobcardID")%>" data-toggle="tooltip" title="Edit">

                                                        <i class="fa fa-edit"></i>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <div style="display: block;">
                                        <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-hover">
                                            <EmptyDataTemplate>
                                                <p>Data Not Available...</p>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Task">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Category") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="particulars" HeaderText="Task" />
                                                <asp:TemplateField HeaderText="Start date">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "StartDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End date">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "EndDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description" ItemStyle-Width="200px" ItemStyle-Wrap="true">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assign To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltaskassign" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                                                <asp:TemplateField HeaderText="Assign Budget">
                                                    <ItemTemplate>
                                                        <%# Eval("Total")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status">

                                                    <ItemTemplate>

                                                        <%# Eval("Status")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <%# Eval("Remarks") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Task complete(%)" HeaderStyle-Width="80px">
                                                    <ItemTemplate>

                                                        <%# Eval("workstatus")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <asp:HiddenField ID="hdnTaskID" runat="server" />
                                    <asp:HiddenField runat="server" ID="hdnNodificationID" />


                                    <asp:HiddenField runat="server" ID="hdnjobid" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12" style="text-align: center">


                            <asp:Button runat="server" ID="btnExcel" Text="Export To Excel" CssClass="btn btn-primary" CausesValidation="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary" OnClientClick="javascript:history.go(-1)" CausesValidation="false" Visible="false" TabIndex="1" />
                            <a href='javascript:history.go(-1)' class="btn btn-primary">Back</a>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">

    <script>

        function call(jid, tid) {

            location.href = "Task.aspx?jid=" + jid + "&mode=edit&tid=" + tid;
        }


        $(document).ready(function () {


        });

        function loadHistory(lid) {
            var listid = lid;
            $("#dialog-box-task").modal();
            $.ajax({
                url: "AjaxCall/Taskhistory.aspx?lid=" + listid,
                context: document.body,
                success: function (Result) {

                    document.getElementById('statuslist').innerHTML = Result;

                }

            });
        }

        function uploaddata(cid) {
            //            alert(cid);
            eval($("#ContentPlaceHolder1_gvTasks_file1_" + cid).trigger('click'));

        }

    </script>
</asp:Content>
