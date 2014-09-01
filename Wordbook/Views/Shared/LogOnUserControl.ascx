<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        ようこそ <b><%= Html.Encode(Page.User.Identity.Name) %></b> さん[ <%= Html.ActionLink("ログオフ", "LogOff", "Account") %> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("ログオン", "LogOn", "Account") %> ]
<%
    }
%>
