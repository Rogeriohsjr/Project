<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Acesso.aspx.cs" Inherits="SFP.Acesso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sistema Financeiro Pessoal - SFP</title>
    <link href="App_Themes/Green/Geral.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Green/Menu/menu.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btAcessar">
    <div style="margin: 0 auto; max-width: 960px;">
        <table width="100%">
            <tr>
                <td>
                    <div id="menu">
                        <ul class="menu">
                            <li><a href="Home.aspx" class="parent"><span>SISTEMA FINANCEIRO PESSOAL</span></a></li>
                        </ul>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="pnMsgPrincipal" runat="server" Visible="false">
                                    <asp:Label ID="lbMsgPrincipal" runat="server" Text="" ForeColor="Red" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Login:
                                <br />
                                <asp:TextBox ID="txtLogin" runat="server" Width="100px" Style="padding: 4px 8px;
                                    width: 302px; height: 1.466em; border: 1px solid rgba(0, 0, 0, 0.27); background-color: rgba(255, 255, 255, 0.8);
                                    line-height: 142%;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Senha:<br />
                                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="100px" Style="padding: 4px 8px;
                                    width: 302px; height: 1.466em; border: 1px solid rgba(0, 0, 0, 0.27); background-color: rgba(255, 255, 255, 0.8);
                                    line-height: 142%;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btTour" runat="server" Text="Faça um Tour" Width="130px" CssClass="BotaoPesquisar"
                                                OnClick="btTour_OnClick" Style="height: 3em; padding: 3px 12px 5px; font-size: 100%;
                                                margin-right: 20px;" />
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btAcessar" runat="server" Text="Acessar" Width="100px" CssClass="BotaoPesquisar"
                                                OnClick="btAcessar_OnClick" Style="height: 3em; padding: 3px 12px 5px; font-size: 100%;
                                                margin-right: 20px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
