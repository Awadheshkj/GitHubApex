<%@ page title="" language="VB" masterpagefile="settingmaster.master" autoeventwireup="false" codefile="Activity.aspx.vb" inherits="Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "ActivityManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">

    <h1>Activity Manager</h1>
    <div class="span8">
        <div class="text-right">
            <div class="control-group">
                <div class="btn-group">

                    <asp:Button ID="btnAdd" runat="server" Text="Add New Activity" class="btn btn-primary pull-right" PostBackUrl="ActivityManager.aspx?mode=add" />
                </div>
            </div>
        </div>


        <div class="">
            <asp:GridView ID="gvActivity" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="5">
                <Columns>
                    <asp:TemplateField HeaderText="Row">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Row")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ActivityId" Visible="false">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnActivityID" runat="server" Value='<%#Bind("ProjectTypeID")%>' />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText=" Activity">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ProjectType")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Allow profit(%)">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Allowprofit")%></ItemTemplate>
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

