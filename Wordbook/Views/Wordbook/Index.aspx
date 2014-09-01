<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Kyotsu Wordbook Demo</title>
    <link href="<%=ResolveClientUrl("~/Content/bootstrap.min.css")%>" rel="stylesheet">
    <link href="<%=ResolveClientUrl("~/Content/jquery.dataTables.css")%>" rel="stylesheet">
    <link href="<%=ResolveClientUrl("~/Content/ladda-themeless.min.css")%>" rel="stylesheet">
    <style type="text/css" class="init">
            td.details-control {
	            background: url('<%=ResolveClientUrl("~/Content/details_open.png")%>') no-repeat center center;
	            cursor: pointer;
            }
            tr.shown td.details-control {
	            background: url('<%=ResolveClientUrl("~/Content/details_close.png")%>') no-repeat center center;
            }
            tr.group,
            tr.group:hover {
                    background-color: #ddd !important;
            }
            .top-buffer { margin:20px; }
	</style>
</head>

<body>
<div class="page-header">
   <h1 class="text-center">単語帳</h1>  
   
    </div>
<div class="container">
    <div class="row">
          <div class="col-md-2"></div>
          <div class="col-md-6">
            
            <!--<button class="btn btn-success ladda-button" data-style="expand-right" id="Expand"><span class="ladda-label">expand-right</span></button>-->
          </div>
          <div class="col-md-4">
            <button type="button" class="btn btn-success ladda-button " data-style="zoom-in" data-size="l" id="Logout"><span class="ladda-label">ログアウト</span></button>
          </div>
    </div> 
    <div class="row">
        
        <ul class="nav nav-tabs" role="tablist">
            <li class="active"><a href="#word_list" role="tab" data-toggle="tab">単語リスト</a></li>
            <li ><a href="#word_add" role="#tab" data-toggle="tab">単語追加</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="word_list">
                <div class="row  top-buffer">
                <!--<button type="button" class="btn btn-success ladda-button  popover-dismiss" data-style="expand-right" data-size="l" id="AddWord"  data-toggle="popover"><span class="ladda-label">単語追加</span> </button>-->
            <!--<button type="button" class="btn btn-success ladda-button " data-style="zoom-in" data-size="l" id="EditWord"><span class="ladda-label">単語編集</span></button>-->
            <button type="button" class="btn btn-success ladda-button " data-style="zoom-in" data-size="l" id="DeleteWord" ><span class="ladda-label">単語削除</span></button>
            <button type="button" class="btn btn-success ladda-button " data-style="zoom-in" data-size="l" id="LoadData"><span class="ladda-label">リフレッシュ</span></button>
                </div>
                <table id="table_id" class="display">
                    <thead>
                        <tr>
                            <th>index</th>
                            <th>単語</th>
                            <th>意味</th>
                            <th>日付</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div class="tab-pane top-buffer" id="word_add">
                    <div class="form-group">
                        <label for="inputEmail">単語</label>
                        <input  class="form-control" id="inputWord" placeholder="単語">
                    </div>
                    <div class="form-group">
                        <label for="inputPassword">翻訳</label>
                        <br/>
                        <textarea class="form-control" id="inputTranslation" rows="6" placeholder="翻訳" style="width:100%"></textarea>
                    </div>
                    
                    <button type="submit" class="btn btn-primary" id="AddWord">登録</button>
              </div>
        </div>
    </div>
    <!--<div class="row">
        <form role="form">
            <div class="form-group">
                <label for="exampleInputEmail1">Word</label>
                <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">
            </div>
            <div class="form-group">
                <label for="exampleInputPassword1">Translation</label>
                <input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password">
            </div>
         
            <button type="submit" class="btn btn-default">Submit</button>
        </form>
    </div>-->
</div>

<!-- Core Scripts - Include with every page -->
<script src="<%=ResolveClientUrl("~/Scripts/jquery-1.11.1.min.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Datatable/jquery.dataTables.min.js")%>" ></script>
<script src="<%=ResolveClientUrl("~/Scripts/bootstrap.min.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Thrift/thrift.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Thrift/wordbook_types.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Thrift/WordbookThriftService.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/spin.min.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/ladda.min.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Global.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Wordbook.js")%>"></script>
</body>
</html>
