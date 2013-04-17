<%@ Page Title="SFP - Gestão de Conta" Language="C#" MasterPageFile="~/Layout.Master"
    AutoEventWireup="true" CodeBehind="EditarContasAPagar.aspx.cs" Inherits="SFP.MODEL.EditarContasAPagar" %>

<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td class="Titulo">
                Contas a Pagar
            </td>
        </tr>
        <tr>
            <td class="TituloPesquisa">
                <asp:Label ID="lbTitulo" runat="server" Text="Novo" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                Categoria<br />
                <asp:DropDownList ID="ddlCategoria" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td>
                Descrição<br />
                <asp:TextBox ID="txtDescricao" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Data Vencimento<br />
                <asp:TextBox ID="txtDataVencimento" runat="server" MaxLength="10" Width="150px" />
                <ajax:CalendarExtender ID="txCalendarVencimento" runat="server" TargetControlID="txtDataVencimento"
                    Format="dd/MM/yyyy">
                </ajax:CalendarExtender>
            </td>
            <td>
                Valor Total<br />
                <asp:TextBox ID="txtValorTotal" runat="server" Width="150px" />
            </td>
        </tr>
        <tr>
            <td>
                Data Pagamento<br />
                <asp:TextBox ID="txtDataPagamento" runat="server" MaxLength="10" Width="150px" />
                <ajax:CalendarExtender ID="txCalendarPagamento" runat="server" TargetControlID="txtDataPagamento"
                    Format="dd/MM/yyyy">
                </ajax:CalendarExtender>
            </td>
            <td>
                Valor Pago<br />
                <asp:TextBox ID="TextBox2" runat="server" Width="150px" />
            </td>
        </tr>
        <tr>
            <td>
                Documento<br />
                <asp:TextBox ID="txtDocumento" runat="server" Width="150px" />
            </td>
            <td>
                Histórico<br />
                <asp:TextBox ID="txtHistorico" runat="server" Width="150px" />
            </td>
        </tr>
        <tr>
            <td class="TituloPesquisa" colspan="2" style="text-align: center;">
                <asp:Button ID="btGravar" runat="server" Text="Salvar" Width="100px" />
                &nbsp;
                <asp:Button ID="btVoltar" runat="server" Text="Voltar" Width="100px" OnClick="btVoltar_Onclick" />
            </td>
        </tr>
    </table>
</asp:Content>
