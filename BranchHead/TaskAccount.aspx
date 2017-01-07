<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="TaskAccount.aspx.vb" Inherits="TaskAccount" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <%--<h1>Task Accounts
           
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Task Accounts</li>
        </ol>--%>
    </section>

    <!-- Main content -->
    <section class="content">
        <h1>Task Accounts
        </h1>
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">

                        <asp:HiddenField ID="hdnJobCardID" runat="server" />
                        <asp:HiddenField ID="hdnBriefID" runat="server" />
                        <div id="MessageDiv" runat="server" class="alert alert-success">
                            <p>

                                <strong>Message: </strong>
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </p>
                        </div>

                        <div id="divError" runat="server" class="alert alert-danger">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>

                        <div id="divContent" runat="server">

                            <div class="inner-Content-container">
                                <div class="col-lg-12">
                                    <asp:GridView ID="gridprepnlcat"
                                        runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
                                        <EmptyDataTemplate>
                                            NO Data Available...
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="Category" HeaderText="Category" />
                                            <asp:BoundField DataField="PreEventQuote" HeaderText="Pre PnL Budget" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="used" HeaderText="Assign Budget" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="balance" HeaderText="Remaining Budget" ItemStyle-HorizontalAlign="Right" />
                                            <%-- <asp:TemplateField HeaderText="Remaining Balance">
                                <ItemTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblqty" runat="server" Text='<%# Eval("balance")%>' />
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty" runat="server" />
                                    </div>
                                </FooterTemplate>
                                <FooterStyle BackColor="White"  Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-lg-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <a href="uploads/upload_dataTask.xlsx" class="badge">Click here</a> to download Excel Format.
                                        </div>
                                        <div class="panel-body">
                                            <div class="col-lg-12">
                                                <div id="divcopypasteerror" runat="server" class="alert alert-danger">
                                                    <p>
                                                        <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                                        <strong>Warning: </strong>
                                                        <asp:Label ID="lblcopypasteerror" runat="server"></asp:Label>
                                                    </p>
                                                </div>
                                                <asp:GridView ID="GridView1" CssClass="table table-bordered table-striped"
                                                    runat="server" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="id" HeaderText="id" ItemStyle-Width="30" />
                                                        <asp:TemplateField HeaderText="Category" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdncatigory" runat="server" Value='<%# Eval("Category")%>' />
                                                                <asp:DropDownList ID="ddlCategory" class="form-control input-small" TabIndex="1" runat="server">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                    <asp:ListItem>C.E.P</asp:ListItem>
                                                                    <asp:ListItem>Events</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredValidator20" ValidationGroup="impgr" runat="server" ErrorMessage="Please Select Category" Font-Size="11px" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCategory" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Taskname" HeaderText="Task Name" ItemStyle-Width="150" />
                                                        <asp:BoundField DataField="City" HeaderText="City" ItemStyle-Width="130" />
                                                        <asp:BoundField DataField="Particulars" HeaderText="Sub Task" ItemStyle-Width="150" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="UnitPrice" HeaderText="Amount" ItemStyle-Width="30" />
                                                        <%--<asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-Width="30" />--%>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcattotal" runat="server" Text='<%# Eval("Quantity") * Eval("UnitPrice")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:BoundField DataField="TL" HeaderText="Team Leader" ItemStyle-Width="150" />--%>
                                                        <asp:TemplateField HeaderText="TeamLeader" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnteamleader" runat="server" Value='<%# Eval("TL")%>' />
                                                                <asp:DropDownList ID="ddlLead" class="form-control input-small" TabIndex="1" runat="server">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredValidator21" ValidationGroup="impgr" runat="server" ErrorMessage="Please Select PM" Font-Size="11px" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlLead" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Startdate" HeaderText="Start Date" ItemStyle-Width="150" />
                                                        <asp:BoundField DataField="Enddate" HeaderText="End Date" ItemStyle-Width="150" />
                                                        <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="150" />

                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <div class="col-lg-12">
                                                <asp:TextBox ID="txtCopied" runat="server" TextMode="MultiLine" placeholder="Copy from Excel and paste hare without header" AutoPostBack="true"
                                                    OnTextChanged="PasteToGridView" Height="80" CssClass="form-control" />
                                                <asp:Button runat="server" ID="btnimport" Visible="false" Text="Import to Final grid" CssClass="btn btn-primary" ValidationGroup="impgr" TabIndex="6" />
                                                <asp:Button runat="server" ID="btncancelimport" Visible="false" Text="cancel" CssClass="btn btn-primary" CausesValidation="false" TabIndex="6" />
                                                <script type="text/javascript">
                                                    window.onload = function () {
                                                        document.getElementById("<%=txtCopied.ClientID %>").onpaste = function () {
                                                            var txt = this;
                                                            setTimeout(function () {
                                                                __doPostBack(txt.name, '');
                                                            }, 100);
                                                        }
                                                    };
                                                </script>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 20%; float: right;"></div>
                                </div>

                                <div class="InnerContentData">
                                    <div runat="server" class="col-lg-12">
                                        <b>Pre Event Cost : </b>
                                        <asp:Label runat="server" ID="lblPreEventQuote" Text="0"></asp:Label>

                                        <b>Total Cost :</b>
                                        <asp:Label runat="server" ID="lbltotalcost" Text="0"></asp:Label>

                                        <b>Remaining Balance :</b>
                                        <asp:Label runat="server" ID="lblremain" Text="0"></asp:Label>
                                    </div>
                                    <div id="divClaims" runat="server">
                                        Claimable Balance: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblClaim" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-12">
                                        <center>
                        <asp:HiddenField ID="hdnAccountID" runat="server"></asp:HiddenField>
                        <div style="width:100%; overflow:auto;">
                <asp:GridView runat="server" ID="gdvAccount" AutoGenerateColumns="false" Width="1524px" AllowPaging="false" PageSize="15" ShowFooter="true" 
                     OnRowCancelingEdit="gdvAccount_RowCancelingEdit"  
                                 sonrowcommand="gdvAccount_RowCommand" CssClass="table table-bordered table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                <asp:HiddenField ID="hdnTaskAccountID" runat="server" Value='<%# DataBinder.Eval(Container.Dataitem,"AccountID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Category"> 
                            <EditItemTemplate>
                                <%--<asp:TextBox ID="gv_txtParticulars" runat="server" Text='<%#Eval("Particulars") %>' CssClass="form-control input-small" />--%>
                                <asp:DropDownList runat="server" ID="gv_ddlCategory" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" TabIndex="2"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_gv_txtParticulars1" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_ddlCategory"></asp:RequiredFieldValidator>
                            </EditItemTemplate>

                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"Category") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:TextBox runat="server" ID="txtParticulars" TabIndex="1" CssClass="form-control input-small"></asp:TextBox>--%>
                                <asp:DropDownList runat="server" ID="ddlCategory" AutoPostBack="true" CssClass="form-control11" TabIndex="2" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfv1rwqe" ControlToValidate="ddlCategory" ForeColor="Red" ErrorMessage="*" InitialValue="0" MaxLength="40"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TaskName">
                            <EditItemTemplate>
                                <asp:TextBox ID="gv_txtTaskName" runat="server" Text='<%#Eval("TaskName")%>' CssClass="form-control input-small" />
                                <asp:RequiredFieldValidator ID="RFV_gv_txtTaskName" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtTaskName"></asp:RequiredFieldValidator>
                            </EditItemTemplate>

                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "TaskName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtTaskName" TabIndex="1" CssClass="form-control input-small"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfv12" ControlToValidate="txtTaskName" ForeColor="Red" ErrorMessage="*" MaxLength="40"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="City">
                            <EditItemTemplate>
                                <asp:TextBox ID="gv_txtCity" runat="server" Text='<%#Eval("City")%>' CssClass="form-control input-small" />
                                <asp:RequiredFieldValidator ID="RFV_gv_txtcity" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtcity"></asp:RequiredFieldValidator>
                            </EditItemTemplate>

                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "city")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtCity" TabIndex="1" CssClass="form-control input-small"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfv1123" ControlToValidate="txtCity" ForeColor="Red" ErrorMessage="*" MaxLength="40"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sub Task">
                            <EditItemTemplate>
                                <asp:TextBox ID="gv_txtParticulars" runat="server" Text='<%#Eval("Particulars") %>' CssClass="form-control input-small" />
                                <asp:RequiredFieldValidator ID="RFV_gv_txtParticulars" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtParticulars"></asp:RequiredFieldValidator>
                            </EditItemTemplate>

                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"Particulars") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtParticulars" TabIndex="1" CssClass="form-control input-small"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfv1" ControlToValidate="txtParticulars" ForeColor="Red" ErrorMessage="*" MaxLength="40"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Quantity">
                            <EditItemTemplate>
                                <asp:TextBox ID="gv_txtQuantity" onkeyup="javascript:caltotal2(this)" runat="server" Text='<%#Eval("Quantity") %>' CssClass="form-control input-small" />
                                <asp:RequiredFieldValidator ID="RFV_gv_txtQuantity" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtQuantity"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"Quantity") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtQuantity" onkeyup="javascript:caltotal()" CssClass="form-control input-small" MaxLength="12" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfv111" ControlToValidate="txtQuantity" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <EditItemTemplate>
                                <asp:TextBox ID="gv_txtAmount" onkeyup="javascript:caltotal2(this)" runat="server" Text='<%#Eval("Amount") %>' CssClass="form-control input-small" />
                                <asp:RequiredFieldValidator ID="RFV_gv_txtAmount" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtAmount"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Amount")%>
                                <asp:HiddenField ID="hdnAmount" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Total")%>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtAmount" onkeyup="javascript:caltotal()" CssClass="form-control input-small" MaxLength="12" TabIndex="3"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfv11" ControlToValidate="txtAmount" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <EditItemTemplate>
                                <asp:Label  ID="gv_txtTotal" onkeyup="javascript:caltotal2(this)" runat="server" Text='<%#Eval("Total") %>' ></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.Dataitem,"Total") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtTotal" ReadOnly="true" CssClass="form-control input-small" MaxLength="12"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Team Leader" ItemStyle-Width="120px" >
                            <ItemTemplate>
                                  <%--<%# If(Eval("Name").ToString() = "N/A", "", "<input type='button'  value='" & Eval("Name") & " ' class='btn-small btn-info'  />")%>--%>
                                 <%--<input type='button'  value='<%# Eval("Name") %>' style='<%# If(Eval("Name") = "N/A", "Display:None", "Display:Block")%> ' class='btn-small btn-info'  />--%>
                                 <%# DataBinder.Eval(Container.DataItem, "Name")%> 
                              <%-- <asp:DropDownList ID='ddlLead' class='btn-small btn-info' Visible='<%# If(Eval("Name") = "N/A", "True", "False")%>'  runat='server'> <asp:ListItem Value='Select' >Select</asp:ListItem></asp:DropDownList>--%>

                            </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:DropDownList ID='gv_ddlLead' class='btn-small btn-info'  TabIndex="4" runat='server'> <asp:ListItem Value='0' >Select</asp:ListItem></asp:DropDownList>
                                  <asp:RequiredFieldValidator runat="server" ID="gv_lead" ControlToValidate="gv_ddlLead" ForeColor="Red" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </EditItemTemplate> 
                             <FooterTemplate>
                                 <asp:DropDownList ID='ddlLead' class='btn-small btn-info'  TabIndex="4" runat='server'> <asp:ListItem Value='0' >Select</asp:ListItem></asp:DropDownList>
                                  <asp:RequiredFieldValidator runat="server" ID="lead" ControlToValidate="ddlLead" ForeColor="Red" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                             </FooterTemplate> 

<ItemStyle Width="120px"></ItemStyle>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Start Date">
                            <EditItemTemplate>
                                <asp:TextBox ID="gv_txtstartdate"  runat="server" Text='<%#Eval("Startdate", "{0:yyyy-MM-dd HH:mm}")%>' TabIndex="5" onchange="javascript:return checkDates();"  CssClass="form-control input-small"  />
                                
                                <asp:RequiredFieldValidator ID="RFV_gv_txtstartdate" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtstartdate"></asp:RequiredFieldValidator>
                            </EditItemTemplate>

                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "startdate", "{0:dd-MM-yyyy HH:mm}")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox Width="140px" runat="server" ID="txtstartdate" TabIndex="5" CssClass="form-control input-small" onchange="javascript:return checkDates1();"></asp:TextBox>
                               
                                <asp:RequiredFieldValidator runat="server" ID="rfvtxtstartdate" ControlToValidate="txtstartdate" ForeColor="Red" ErrorMessage="*" MaxLength="40"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="End Date">
                            <EditItemTemplate>
                                <asp:TextBox ID="gv_txtEnddate" runat="server" Text='<%#Eval("Enddate", "{0:yyyy-MM-dd HH:mm}")%>' CssClass="form-control input-small" onchange="javascript:return checkDates();"/>
                                <asp:RequiredFieldValidator ID="RFV_gv_txtEnddate" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtEnddate"></asp:RequiredFieldValidator>
                            </EditItemTemplate>

                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Enddate", "{0:dd-MM-yyyy HH:mm}")%>
                            </ItemTemplate>
                            <FooterTemplate >
                                <asp:TextBox  runat="server" Width="140px" ID="txtEnddate" TabIndex="6" CssClass="form-control input-small" onchange="javascript:return checkDates1();"></asp:TextBox>
                                 
                                <asp:RequiredFieldValidator runat="server" ID="rfvEnddate" ControlToValidate="txtEnddate" ForeColor="Red" ErrorMessage="*" MaxLength="40"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Description" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                            <EditItemTemplate>
                                <asp:TextBox ID="gv_txtDescription" runat="server" Text='<%#Eval("Description")%>' TextMode="MultiLine" MaxLength="200" CssClass="form-control input-small" />
                                <%--<asp:RequiredFieldValidator ID="RFV_gv_Description" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtDescription"></asp:RequiredFieldValidator>--%>
                            </EditItemTemplate>

                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtDescription" TabIndex="7" CssClass="form-control input-sm" MaxLength="200" TextMode="MultiLine" ></asp:TextBox>
                                <%--<asp:RequiredFieldValidator runat="server" ID="rfvDescription" ControlToValidate="txtDescription" ForeColor="Red" ErrorMessage="*" MaxLength="40"></asp:RequiredFieldValidator>--%>
                            </FooterTemplate>

<ItemStyle HorizontalAlign="Left" Width="180px"></ItemStyle>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Manage Checklist">
                            <ItemTemplate>
                                 <a href='CheckList.aspx?tid=<%# DataBinder.Eval(Container.Dataitem,"AccountID") %>'><span  class="btn-small btn-info" >manage</span>  </a> 
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="160px">
                            <EditItemTemplate>
                                <asp:Button ValidationGroup="Editvalidation" ID="imgbtnUpdate" CommandName="Update" runat="server" Text="Update" CssClass="btn-small btn-info" />
                                <asp:Button CausesValidation="false" ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-sm btn-danger" />

                            </EditItemTemplate>
                            <ItemTemplate>
                                
                                <asp:Button CausesValidation="false" ID="imgbtnEdit" CommandName="Edit" visible='<%# IIf(CInt(ProcessMyDataItem(Eval("Claimed"))) > 0, "false", "True")%>' runat="server" Text="Edit" CssClass="btn btn-sm btn-info" />
                                <asp:Button CausesValidation="false" ID="imgbtnDelete" OnClientClick="javascript:return ConfirmationBox()" visible='<%# IIf(CInt(ProcessMyDataItem(Eval("Claimed"))) > "0", "false", "True")%>' CommandName="Delete" Text="Delete" runat="server" CssClass="btn-small btn-danger" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button runat="server" ID="btnSave" Text="Add" CssClass="btn-sm btn-info" CommandName="add" TabIndex="8" /></td>
                            </FooterTemplate>

<ItemStyle Width="140px"></ItemStyle>
                        </asp:TemplateField>
                         
                    </Columns>
                </asp:GridView>
                            </div>
                <asp:GridView runat="server" ID="gvDisplay" AutoGenerateColumns="false" Width="90%" AllowPaging="true" PageSize="15" CssClass="table table-bordered table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                <asp:HiddenField ID="hdnTaskAccountID" runat="server" Value='<%# DataBinder.Eval(Container.Dataitem,"AccountID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Particulars">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"Particulars") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"Quantity") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Amount")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.Dataitem,"Total") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Team Leader">
                            <ItemTemplate>
                                  <%# DataBinder.Eval(Container.DataItem, "Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView></center>
                                    </div>
                                    <div class="col-lg-12">
                                        <center>
                    <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary" CausesValidation="false" Visible="false"  TabIndex="5" />
                        <a href='javascript:history.go(-1)' class="btn btn-primary">Back</a>
                    <asp:Button runat="server" ID="btnNext" Text="Next" CssClass="btn btn-primary" CausesValidation="false" TabIndex="6" />
                    <asp:Button runat="server" ID="btnExcel" Text="Export To Excel" CssClass="btn btn-primary" CausesValidation="false" TabIndex="7" />
                                            <a href="#" id="task1" class="btn btn-primary">View Task List</a>
                    <asp:Button ID="btnClaims" runat="server" Text="Claims" CssClass="btn btn-primary" CausesValidation="false" TabIndex="8" />
                        
                                                </center>
                                    </div>

                                    <%--<asp:HiddenField ID="hdnTaskID" runat="server" />--%>
                                    <asp:HiddenField runat="server" ID="hdnNodificationID" />

                                    <asp:HiddenField runat="server" ID="dhncategoryname" />
                                    <asp:HiddenField runat="server" ID="hdnjobid" />
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
    <%--<script src="dist/js/bootstrap-datepicker.js"></script>--%>
    <script src="dist/js/moment.js"></script>
    <script src="dist/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">

        function caltotal() {
            var val1 = false;
            var val2 = false;

            if ($('input[id$=txtQuantity]').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtQuantity]').val())) {
                    val1 = true


                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtQuantity]').val("");
                    val1 = false;
                }
            }
            else {
                //alert('Enter value');
                //$('input[id$=txtQuantity]').val("0");
            }


            if ($('input[id$=txtAmount]').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtAmount]').val())) {
                    val2 = true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtAmount]').val("");
                    val2 = false;
                }
            }
            else {
                //alert('Enter value');
                //$('input[id$=txtAmount]').val("0");
            }

            if (val1 == true && val2 == true) {
                var qty = parseFloat($('input[id$=txtQuantity]').val());
                var amt = parseFloat($('input[id$=txtAmount]').val());

                var total = qty * amt;
                $('input[id$=txtTotal]').val(total);
                //$("[id*=txtPreEventTotal]").html(total);
            }
        }

        function caltotal2(obj) {
            var val1 = false;
            var val2 = false;
            var varid = obj.id
            var stringArray = new Array();
            stringArray = varid.split("_");


            var postfix = stringArray[stringArray.length - 1];
            postfix = "_" + postfix
            if ($('input[id$=txtQuantity' + postfix + ']').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtQuantity' + postfix + ']').val())) {
                    val1 = true


                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtQuantity' + postfix + ']').val("");
                    val1 = false;
                }
            }


            if ($('input[id$=txtAmount' + postfix + ']').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtAmount' + postfix + ']').val())) {
                    val2 = true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtAmount' + postfix + ']').val("");
                    val2 = false;
                }
            }

            if (val1 == true && val2 == true) {
                var qty = parseFloat($('input[id$=txtQuantity' + postfix + ']').val());
                var amt = parseFloat($('input[id$=txtAmount' + postfix + ']').val());
                c
                var total = qty * amt;
                $('span[id$=txtTotal' + postfix + ']').val(total.toFixed(2));
                $('span[id$=txtTotal' + postfix + ']').text(total.toFixed(2));

            }
        }
    </script>

    <script>

        function call(id) {
            location.href = "TeamSelect.aspx?&taID=" + id;
        }

        $(document).ready(function () {
            //alert(1);
            //var jc = readCookie("jcID")
            //$("#tasklistv").attr("href", "ListOfTask.aspx?jid=" + jc);
        });

        $(function DatePicker(obj1) {
            //$('input[id$=ContentPlaceHolder1_gdvAccount_txtstartdate]').datepicker({ dateFormat: 'dd-mm-yyyy' });
            $('input[id$=ContentPlaceHolder1_gdvAccount_txtstartdate]').datetimepicker({
                format: 'DD-MM-YYYY hh:mm:ss'


            });

            $('input[id$=ContentPlaceHolder1_gdvAccount_txtEnddate]').datetimepicker({
                format: 'DD-MM-YYYY hh:mm:ss'

            });

            var GVMaintainReceiptMaster = document.getElementById('<%= gdvAccount.ClientID%>');
            var subtotal = "0";
            var manfee = "0";

            for (var rowId = 1; rowId < GVMaintainReceiptMaster.rows.length; rowId++) {
                //var txtFE = GVMaintainReceiptMaster.rows[rowId].cells[7].children[0];

                $('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtstartdate_' + (rowId - 1) + ']').datetimepicker({
                    format: 'DD-MM-YYYY hh:mm:ss'

                });
                $('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtEnddate_' + (rowId - 1) + ']').datetimepicker({
                    format: 'DD-MM-YYYY hh:mm:ss',
                    useCurrent: false
                });


                $('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtstartdate_' + (rowId - 1) + ']').on("dp.change", function (e) {
                    $('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtEnddate_' + (rowId - 1) + ']').data("DateTimePicker").minDate(e.date);
                });
                $('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtEnddate_' + (rowId - 1) + ']').on("dp.change", function (e) {
                    $('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtstartdate_' + (rowId - 1) + ']').data("DateTimePicker").maxDate(e.date);
                });


            }

        });
        function checkDates() {
            var objDate = new Date();
            var GVMaintainReceiptMaster = document.getElementById('<%= gdvAccount.ClientID%>');
            var subtotal = "0";
            var manfee = "0";
            for (var rowId = 1; rowId < GVMaintainReceiptMaster.rows.length; rowId++) {
                //var txtFE = GVMaintainReceiptMaster.rows[rowId].cells[7].children[0];
                //$('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtstartdate_' + (rowId - 1) + ']').datepicker({ dateFormat: 'dd-mm-yyyy' });
                //$('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtEnddate_' + (rowId - 1) + ']').datepicker({ dateFormat: 'dd-mm-yyyy' });
                if (process($('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtstartdate_' + (rowId - 1) + ']').val()) > process($('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtEnddate_' + (rowId - 1) + ']').val())) {
                    $('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtstartdate_' + (rowId - 1) + ']').val("");
                    $('input[id$=ContentPlaceHolder1_gdvAccount_gv_txtEnddate_' + (rowId - 1) + ']').val("");
                    alert("End Date should be later than Start Date!");
                    return false;
                }
                else {
                    return true;
                }
            }

        }

        function process(date) {
            var parts = date.split("-");
            var date = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
            return date.getTime();
        }

        function checkDates1() {

            var objDate = new Date();
            alert($('input[id$=ContentPlaceHolder1_gdvAccount_txtstartdate]').value);
            if ($('input[id$=ContentPlaceHolder1_gdvAccount_txtstartdate]').value != "" && $('input[id$=ContentPlaceHolder1_gdvAccount_txtEnddate]').value != "") {

                var selectedDate = $('input[id$=ContentPlaceHolder1_gdvAccount_txtstartdate]').val();
                var selectedDate2 = $('input[id$=ContentPlaceHolder1_gdvAccount_txtEnddate]').val();
                //alert(selectedDate);
                //var currentDate = $.datepicker.formatDate('dd/mm/yy', new Date());
                //alert(currentDate);
                if (process(selectedDate) > process(selectedDate2)) {
                    $('input[id$=ContentPlaceHolder1_gdvAccount_txtstartdate]').val("");
                    $('input[id$=ContentPlaceHolder1_gdvAccount_txtEnddate]').val("");
                    alert("End Date should be later than Start Date!");
                    return false;
                }
                else {
                    return true;
                }
            }
        }
    </script>


</asp:Content>

