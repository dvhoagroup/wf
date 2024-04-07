﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BEE.BanDo._Default" %>

<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/styles/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function checkBooking(paras) {
            callBooking.PerformCallback(paras);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="project-tower">
        <div class="project">
            <%= ProjectName %>Ⅰ　PRICE & AREA
        </div>
        <div class="date">
            <%= string.Format("{0:dd/MM/yyyy}", DateTime.Now) %>
        </div>
        <div class="towerB">
            TOWER B
        </div>
        <div class="tower">
            TOWER A
        </div>
    </div>
    <div id="divContent" runat="server" style="width: 3000px">
        <div class="floor">2F</div>
        <div class="itemBox">
            <div class="caption">
                <div class="no">B401</div>
                <div class="type">B-a</div>
            </div>
            <div class="area-br">
                <div class="area">60m2</div>
                <div class="br">2BR</div>
            </div>
            <div class="price">1.200.000.000</div>
            <div class="discount-year">
                <div class="discount">ORIGINAL</div>
                <div class="year">2yrs</div>
            </div>
            <div class="discountprice-spa">1.200.000.000</div>
            <div class="status-spa">for Sale</div>
            <div class="customer-spa">Pham Minh Thuan</div>
            <div class="cate-customer-spa">Individual</div>

            <div class="booking">
                <a href="javascript:void();" onclick="checkBooking('100:3')">Đặt cọc</a> | 
                <a href="javascript:void();" onclick="checkBooking('100:6')">Hợp đồng</a>
            </div>
        </div>
        <div class="itemBox">
            <div class="caption">
                <div class="no">B402</div>
                <div class="type">B-b</div>
            </div>
            <div class="area-br">
                <div class="area">60m2</div>
                <div class="br">2BR</div>
            </div>
            <div class="price">1.200.000.000</div>
            <div class="discount-year">
                <div class="discount">ORIGINAL</div>
                <div class="year">2yrs</div>
            </div>
            <div class="discountprice">1.200.000.000</div>
            <div class="status">for Sale</div>
            <div class="customer">Pham Minh Thuan</div>
            <div class="cate-customer">Individual</div>
        </div>      
        <div class="itemBox15">
            <div class="caption">
                <div class="no">B401</div>
                <div class="type">B-a</div>
            </div>
            <div class="area-br">
                <div class="area">60m2</div>
                <div class="br">2BR</div>
            </div>
            <div class="price">1.200.000.000</div>
            <div class="discount-year">
                <div class="discount">ORIGINAL</div>
                <div class="year">2yrs</div>
            </div>
            <div class="discountprice-da">1.200.000.000</div>
            <div class="status-da">for Sale</div>
            <div class="customer-da">Pham Minh Thuan</div>
            <div class="cate-customer-da">Individual</div>
        </div>
        <div class="itemBox2">
            <div class="caption">
                <div class="no">B401</div>
                <div class="type">B-a</div>
            </div>
            <div class="area-br">
                <div class="area">60m2</div>
                <div class="br">2BR</div>
            </div>
            <div class="price">1.200.000.000</div>
            <div class="discount-year">
                <div class="discount">ORIGINAL</div>
                <div class="year">2yrs</div>
            </div>
            <div class="discountprice-sale">1.200.000.000</div>
            <div class="status-sale">for Sale</div>
            <div class="customer-sale">Pham Minh Thuan</div>
            <div class="cate-customer-sale">Individual</div>
        </div>

        <div class="itemBox2">
            <div class="not-use">GYM and LOBBY</div>
        </div>
        <div class="itemBox">
            <div class="caption">
                <div class="no">B402</div>
                <div class="type">B-b</div>
            </div>
            <div class="area-br">
                <div class="area">60m2</div>
                <div class="br">2BR</div>
            </div>
            <div class="price">1.200.000.000</div>
            <div class="discount-year">
                <div class="discount">ORIGINAL</div>
                <div class="year">2yrs</div>
            </div>
            <div class="discountprice">1.200.000.000</div>
            <div class="status">for Sale</div>
            <div class="customer">Pham Minh Thuan</div>
            <div class="cate-customer">Individual</div>
        </div>
        <div class="itemBox">
            <div class="not-use">GYM and LOBBY</div>
        </div>
    </div>
    <dx:ASPxCallback ID="callBooking" runat="server" 
        ClientInstanceName="callBooking" oncallback="callBooking_Callback">
    </dx:ASPxCallback>
    </form>
</body>
</html>