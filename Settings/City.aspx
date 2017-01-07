<%@ Page Title="" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="City.aspx.vb" Inherits="Masters_City" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "CityManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Content" runat="Server">

    <div class="subMenuStrip"></div>
    <h1>City Details</h1>
    <div class="span8">
        <div class="text-right">
            <div class="control-group">
                <div class="btn-group">
                <asp:Button ID="btnAdd" runat="server" Text="Add New City" class="btn btn-primary pull-right"  PostBackUrl="CityManager.aspx?mode=add" /></div>
                    
                </div>
            </div>
        </div>

                
            <div class="GridDiv">
                <asp:GridView ID="gvCity" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                    <Columns>
                        <asp:TemplateField HeaderText="Row">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                            </ItemTemplate>
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                <asp:HiddenField ID="hdnCityID" runat="server" Value='<%#Bind("CityID")%>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="State">
                            <ItemTemplate>
                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                <%# DataBinder.Eval(Container.DataItem, "State")%>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="City">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "City")%></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Is Active">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IsActive")%></ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_footer" runat="Server">
   
</asp:Content>

