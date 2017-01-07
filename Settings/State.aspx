<%@ Page Title="" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="State.aspx.vb" Inherits="Masters_State" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "StateManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">


    <h1>State Details</h1>

    <div class="span8">
        <div class="text-right">
            <div class="control-group">
                <div class="btn-group">
                    <asp:Button ID="btnAdd" runat="server" Text="Add New State" class="btn btn-primary pull-right" PostBackUrl="StateManager.aspx?mode=add" />
                </div>
               
            </div>

        </div>
         <div>
                    <asp:GridView ID="gvState" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                        <Columns>
                            <asp:TemplateField HeaderText="Row">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                    <asp:HiddenField ID="hdnStateID" runat="server" Value='<%#Bind("StateID")%>' />

                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "State")%></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Is Active">
                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IsActive")%></ItemTemplate>
                            </asp:TemplateField>



                        </Columns>
                    </asp:GridView>
                </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_footer" runat="Server">
</asp:Content>

