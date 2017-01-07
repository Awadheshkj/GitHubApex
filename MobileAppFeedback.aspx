<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MobileAppFeedback.aspx.vb" Inherits="MobileAppFeedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Feedback</title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <div class="alert alert-danger" id="divError" runat="server">
                <strong>Warning: </strong>
                <asp:Label ID="lblError" runat="server"></asp:Label>


            </div>

            <div class="alert alert-success" id="MessageDiv" runat="server">
                <strong>Message: </strong>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

            </div>
        <div class="container" id="content" runat="server" >
           
            <div class="col-md-12">
                <div class="row">
                    1.	Pre-event /planning experience with Kestone's Team
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rbtQ1" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ1">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    2.	Experience with the PM, was he/she able to understand the requirement and advice you effectively
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rbtQ2" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ2">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    3.	Was the PM able to add value to the project, with some new suggestions 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rbtQ3" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ3">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    4.	Was the team able to deliver everything as per timelines 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rbtQ4" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ4">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    5.	Overall delivery experience onsite
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rbtQ5" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ5">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    6.	Overall concept or approach of team
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rbtQ6" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ6">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    7.	To what an extent the designs that came out were satisfactory
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rbtQ7" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ7">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    8.	How was your experience with the CEP team
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rbtQ8" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ8">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    9.	How much would you rate us for on time reporting
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="rbtQ9" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ9">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    10.	How much would you rate us on bringing quality attendees
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="rbtQ10" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ10">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    11.	Any other feedback that you would like to give to us regarding this project or otherwise
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="rbtQ11" SetFocusOnError="true" ForeColor="red" Display="Dynamic" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:RadioButtonList runat="server" ID="rbtQ11">
                            <asp:ListItem>Excellent</asp:ListItem>
                            <asp:ListItem>Very Good</asp:ListItem>
                            <asp:ListItem>Good</asp:ListItem>
                            <asp:ListItem>Average</asp:ListItem>
                            <asp:ListItem>Poor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
            </div>

            <div class="col-md-12">
                <%--<div class="row">
                    
                </div>--%>
                <div class="row">
                    <div class="col-md-8 pull-right">
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-sm btn-primary" />
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    &nbsp;<br />
                    <br />
                    <asp:HiddenField ID="hdnloginID" runat="server" />
                    <asp:HiddenField ID="hdnjobcardID" runat="server" />
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
