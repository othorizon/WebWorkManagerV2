<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TeacherCenter.aspx.cs" Inherits="TeacherCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>教师个人中心</title>
    <link rel="stylesheet" href="assets/css/admin.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <header class="am-topbar admin-header">
        <div class="am-topbar-brand">
            <strong>Amaze UI</strong> <small>后台管理模板</small>
        </div>

        <button class="am-topbar-btn am-topbar-toggle am-btn am-btn-sm am-btn-success am-show-sm-only" data-am-collapse="{target: '#topbar-collapse'}"><span class="am-sr-only">导航切换</span> <span class="am-icon-bars"></span></button>

        <div class="am-collapse am-topbar-collapse" id="topbar-collapse">

            <ul class="am-nav am-nav-pills am-topbar-nav am-topbar-right admin-header-list">
                <li><a href="javascript:;"><span class="am-icon-envelope-o"></span>收件箱 <span class="am-badge am-badge-warning">5</span></a></li>
                <li class="am-dropdown" data-am-dropdown>
                    <a class="am-dropdown-toggle" data-am-dropdown-toggle href="javascript:;">
                        <span class="am-icon-users"></span>管理员 <span class="am-icon-caret-down"></span>
                    </a>
                    <ul class="am-dropdown-content">
                        <li><a href="#"><span class="am-icon-user"></span>资料</a></li>
                        <li><a href="#"><span class="am-icon-cog"></span>设置</a></li>
                        <li><a href="#"><span class="am-icon-power-off"></span>退出</a></li>
                    </ul>
                </li>
                <li><a href="javascript:;" id="admin-fullscreen"><span class="am-icon-arrows-alt"></span><span class="admin-fullText">开启全屏</span></a></li>
            </ul>
        </div>
    </header>

    <div class="am-cf admin-main">
        <!-- sidebar start -->
        <div class="admin-sidebar">
            <ul class="am-list admin-sidebar-list">
                <li><a href="admin-index.html"><span class="am-icon-home"></span>首页</a></li>
                <li class="admin-parent">
                    <a class="am-cf" data-am-collapse="{target: '#collapse-nav'}"><span class="am-icon-file"></span>页面模块 <span class="am-icon-angle-right am-fr am-margin-right"></span></a>
                    <ul class="am-list am-collapse admin-sidebar-sub am-in" id="collapse-nav">
                        <li><a href="admin-user.html" class="am-cf"><span class="am-icon-check"></span>个人资料<span class="am-icon-star am-fr am-margin-right admin-icon-yellow"></span></a></li>
                        <li><a href="admin-help.html"><span class="am-icon-puzzle-piece"></span>帮助页</a></li>
                        <li><a href="admin-gallery.html"><span class="am-icon-th"></span>相册页面<span class="am-badge am-badge-secondary am-margin-right am-fr">24</span></a></li>
                        <li><a href="admin-log.html"><span class="am-icon-calendar"></span>系统日志</a></li>
                        <li><a href="admin-404.html"><span class="am-icon-bug"></span>404</a></li>
                    </ul>
                </li>
                <li><a href="admin-table.html"><span class="am-icon-table"></span>表格</a></li>
                <li><a href="admin-form.html"><span class="am-icon-pencil-square-o"></span>表单</a></li>
                <li><a href="#"><span class="am-icon-sign-out"></span>注销</a></li>
            </ul>

            <div class="am-panel am-panel-default admin-sidebar-panel">
                <div class="am-panel-bd">
                    <p><span class="am-icon-bookmark"></span>公告</p>
                    <p>时光静好，与君语；细水流年，与君同。—— Amaze UI</p>
                </div>
            </div>

            <div class="am-panel am-panel-default admin-sidebar-panel">
                <div class="am-panel-bd">
                    <p><span class="am-icon-tag"></span>wiki</p>
                    <p>Welcome to the Amaze UI wiki!</p>
                </div>
            </div>
        </div>
        <!-- sidebar end -->

        <!-- content start -->
        <div class="admin-content">

            <div class="am-cf am-padding">
                <div class="am-fl am-cf"><strong class="am-text-primary am-text-lg">表格</strong> / <small>Table</small></div>
            </div>

            <div class="am-g">
                <div class="am-u-md-6 am-cf">
                    <div class="am-fl am-cf">
                        <div class="am-btn-toolbar am-fl">
                            <div class="am-btn-group am-btn-group-xs">
                                <button type="button" class="am-btn am-btn-default" onclick="setiframe()"><span class="am-icon-plus"></span>新增</button>                           
                                <button type="button" class="am-btn am-btn-default"><span class="am-icon-save"></span>保存</button>
                                <button type="button" class="am-btn am-btn-default"><span class="am-icon-archive"></span>审核</button>
                                <button type="button" class="am-btn am-btn-default"><span class="am-icon-trash-o"></span>删除</button>
                            </div>

                            <div class="am-form-group am-margin-left am-fl">
                                <form runat="server">
                                    <asp:DropDownList ID="ClassList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ClassList_SelectedIndexChanged"></asp:DropDownList>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="am-u-md-3 am-cf">
                    <div class="am-fr">
                        <div class="am-input-group am-input-group-sm">
                            <input type="text" class="am-form-field">
                            <span class="am-input-group-btn">
                                <button class="am-btn am-btn-default" type="button">搜索</button>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- 表格-->
            <div class="am-g">
                <div class="am-u-sm-12">
                    <form class="am-form">
                        <table class="am-table am-table-striped am-table-hover table-main">
                            <thead>
                                <tr>
                                    <th class="table-check">
                                        <input type="checkbox" id="Allselect" runat="server" onserverchange="Allselect_ServerChange" /></th>
                                    <th class="table-id">ID</th>
                                    <th class="table-title">标题</th>
                                    <th class="table-title">班级</th>
                                    <th class="table-type">类别</th>
                                    <th class="table-author">进度</th>
                                    <th class="table-date">发布日期</th>
                                    <th class="table-date">截止日期</th>
                                    <th style="display: none" class="table-date">附件路径</th>
                                    <th class="table-date">附件</th>
                                    <th class="table-set">操作</th>
                                </tr>
                            </thead>
                            <tbody id="workbody" runat="server">
                            </tbody>
                        </table>
                        <div class="am-cf">
                            <div id="dvpage" runat="server"></div> 
                            <div class="am-fr">
                                <ul id="pageul" runat="server" class="am-pagination">
                                    <!--
                                    <li class="am-disabled"><a href="#">«</a></li>
                                    <li class="am-active"><a href="#">1</a></li>
                                    <li><a href="#">2</a></li>
                                    <li><a href="#">3</a></li>
                                    <li><a href="#">4</a></li>
                                    <li><a href="#">5</a></li>
                                    <li><a href="#">»</a></li>
                                    -->
                                </ul>
                            </div>
                        </div>
                        <hr />
                        <p>注：.....</p>
                    </form>
                </div>

            </div>
        </div>
        <!-- content end -->
    </div>

    <footer>
        <hr>
        <p class="am-padding-left">© 2014 AllMobilize, Inc. Licensed under MIT license.</p>
    </footer>

    <!--发布作业弹窗-->
    <div class="am-modal am-modal-no-btn" tabindex="-1" id="doc-modal-1">
        <div class="am-modal-dialog">
            <div class="am-modal-hd">
                发布作业
                <a href="javascript: void(0)" class="am-close am-close-spin" data-am-modal-close>&times;</a>
            </div>
            <div class="am-modal-bd">
                <iframe id="add_work" style="height:480px;width:350px"></iframe>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function setiframe() {
            document.getElementById('add_work').src = 'AddWork.aspx';
            $('#doc-modal-1').modal('open');
        }
        function closeaddwork()
        {
            $('#doc-modal-1').modal('close');
            window.location.reload(true);
        }
    </script>
</asp:Content>

