<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true"
    CodeBehind="Graficos.aspx.cs" Inherits="SFP.Graficos" %>

<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Assembly="Trirand.Web" TagPrefix="trirand" Namespace="Trirand.Web.UI.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pnTituloPizza" runat="server">
        <table width="100%">
            <tr>
                <td class="TituloPesquisa">
                    <div style="width: 49%; text-align: left; float: left;">
                        Acompanhamento por Mês - Grafico Pizza</div>
                    <div style="width: 49%; text-align: right; float: right;">
                        <asp:Image ID="imgCollapse1" runat="server" ImageUrl="App_Themes/Green/Image/cpDownDoubleReverse.png" /></div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnCaixaPizza" runat="server" DefaultButton="btPesquisar">
        <table width="500px">
            <tr>
                <td style="width: 130px;">
                    Data de Vencimento:
                </td>
                <td>
                    <asp:TextBox ID="txtDataDe" runat="server" Width="100px"></asp:TextBox>
                    <ajax:CalendarExtender ID="txCalendarDe" runat="server" TargetControlID="txtDataDe"
                        Format="dd/MM/yyyy">
                    </ajax:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtDataAte" runat="server" Width="100px"></asp:TextBox>
                    <ajax:CalendarExtender ID="txCalendarAte" runat="server" TargetControlID="txtDataAte"
                        Format="dd/MM/yyyy">
                    </ajax:CalendarExtender>
                </td>
                <td>
                    <asp:Button ID="btPesquisar" runat="server" Text="Pesquisar" CssClass="BotaoPesquisar"
                        OnClick="btPesquisar_OnClick" />
                </td>
            </tr>
        </table>
        <div style="border: 1px solid black;">
            <trirand:JQChart runat="server" ID="PieChart" Width="900" Height="400">
                <Title Text="Acompanhamento Mensal" /><Legend Layout="Vertical" Align="Right" VerticalAlign="Top"
                    X="-10" Y="100" BorderWidth="0" />
                <ToolTips Formatter="formatToolTip" />
                <PlotOptions>
                    <Pie AllowPointSelect="true" Cursor="pointer">
                        <DataLabels Enabled="true" Formatter="formatLabels" />
                    </Pie>
                </PlotOptions>
            </trirand:JQChart>
        </div>
    </asp:Panel>
    <ajax:CollapsiblePanelExtender ID="CollapsePanelSearch" runat="Server" TargetControlID="pnCaixaPizza"
        ExpandControlID="pnTituloPizza" CollapseControlID="pnTituloPizza" Collapsed="true"
        TextLabelID="Label1" ExpandedText="" CollapsedText="" ImageControlID="imgCollapse1"
        ExpandedImage="App_Themes/Green/Image/cpUpDoubleReverse.png" CollapsedImage="App_Themes/Green/Image/cpDownDoubleReverse.png"
        SuppressPostBack="true" />
    <asp:Panel ID="pnTituloColuna" runat="server">
        <table width="100%">
            <tr>
                <td class="TituloPesquisa">
                    <div style="width: 49%; text-align: left; float: left;">
                        Acompanhamento Anual - Grafico Coluna</div>
                    <div style="width: 49%; text-align: right; float: right;">
                        <asp:Image ID="imgCollapse2" runat="server" ImageUrl="App_Themes/Green/Image/cpDownDoubleReverse.png" /></div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnCaixaColuna" runat="server" DefaultButton="btPesquisar">
        <table width="250px">
            <tr>
                <td>
                    Tipo:
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" Width="100px">
                        <asp:ListItem Text="Categoria" Value="Categoria"></asp:ListItem>
                        <asp:ListItem Text="Total" Value="Total" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    De:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlYear" runat="server" Width="100px" />
                </td>
                <td>
                    <asp:Button ID="btPesquisarColuna" runat="server" Text="Pesquisar" CssClass="BotaoPesquisar"
                        OnClick="btPesquisarColuna_OnClick" />
                </td>
            </tr>
        </table>
        <div style="border: 1px solid black;">
            <trirand:JQChart runat="server" ID="ColumnChart" Width="900" Height="400" Type="Column">
                <Title Text="Acompanhamento Anual" /><Legend Layout="Vertical" Align="Right" VerticalAlign="Top"
                    X="-10" Y="100" BorderWidth="0" />
                <ToolTips Formatter="formatToolTip" />
                <XAxis>
                    <trirand:ChartXAxisSettings>
                        <Categories>
                            <trirand:AxisCategory Text="Jan" />
                            <trirand:AxisCategory Text="Fev" />
                            <trirand:AxisCategory Text="Mar" />
                            <trirand:AxisCategory Text="Abr" />
                            <trirand:AxisCategory Text="Mai" />
                            <trirand:AxisCategory Text="Jun" />
                            <trirand:AxisCategory Text="Jul" />
                            <trirand:AxisCategory Text="Ago" />
                            <trirand:AxisCategory Text="Set" />
                            <trirand:AxisCategory Text="Out" />
                            <trirand:AxisCategory Text="Nov" />
                            <trirand:AxisCategory Text="Dez" />
                        </Categories>
                    </trirand:ChartXAxisSettings>
                </XAxis>
                <YAxis>
                    <trirand:ChartYAxisSettings>
                        <Title Text="Categoria(s)" />
                    </trirand:ChartYAxisSettings>
                </YAxis>
                <Legend Layout="Vertical" Align="Left" VerticalAlign="Top" X="100" Y="70" Floating="true"
                    BorderWidth="1" BackgroundColor="#FFFFFF" Shadow="true" />
                <ToolTips Formatter="formatToolTipAnual" />
            </trirand:JQChart>
        </div>
    </asp:Panel>
    <ajax:CollapsiblePanelExtender ID="CollapsePanelColuna" runat="Server" TargetControlID="pnCaixaColuna"
        ExpandControlID="pnTituloColuna" CollapseControlID="pnTituloColuna" Collapsed="true"
        TextLabelID="Label1" ExpandedText="" CollapsedText="" ImageControlID="imgCollapse2"
        ExpandedImage="App_Themes/Green/Image/cpUpDoubleReverse.png" CollapsedImage="App_Themes/Green/Image/cpDownDoubleReverse.png"
        SuppressPostBack="true" />
    <script type="text/javascript">
        function formatToolTip() {
            return '<b>' + this.point.x + '</b>: ' + this.y + ' %';
        }

        function formatLabels() {
            return '<b>' + this.point.x + '</b>: ' + this.y + ' %';
        }

        function formatToolTipAnual() {
            return '<b>' + this.series.name + '</b>: R$ ' + this.y + ' ';
        }
        
    </script>
</asp:Content>
