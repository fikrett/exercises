﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using AdresDefteri.DataLayer;
using AdresDefteri.Model;

namespace AdresDefteri
{
    public partial class Anasayfa : System.Web.UI.Page
    {
        AddressDataHelper _helper = new AddressDataHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var data = _helper.GetAddresses();
                GridViewDataBind(data);
            }

            var activePage = ActivePage;
            //Javascript ile aktif sayfa değiştilecek
            ClientScript.RegisterStartupScript(GetType(), "Page", "<script type='text/javascript'>setActivePage('" + activePage + "')</script>");
        }

        private void GridViewDataBind(List<Address> data)
        {
            gvAddresses.DataSource = data;
            gvAddresses.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var address = new Address();

            address.Name = txtName.Text;
            address.SurName = txtSurname.Text;
            address.Nick = txtNick.Text;
            address.BirthDate = calBirth.SelectedDate;

            if (ddlGender.SelectedValue != "Seçiniz")
            {
                address.Gender = ddlGender.SelectedValue == "Bay";
            }

            address.MobileNumber = txtMobile.Text;
            address.HomeNumber = txtHome.Text;
            address.Fax = txtFax.Text;
            address.Email = txtEmail.Text;
            address.UserAddress = txtAddress.Text;
            address.Note = txtNote.Text;

            var rowCount = _helper.AddAddress(address);

            if (rowCount > 0)
            {
                ClearAddressForm();
            }

        }

        private void ClearAddressForm()
        {
            txtName.Text = string.Empty;
            txtSurname.Text = string.Empty;
            txtNick.Text = string.Empty;
            calBirth.SelectedDate = DateTime.Now;
            ddlGender.SelectedIndex = 0;
            txtMobile.Text = string.Empty;
            txtHome.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtNote.Text = string.Empty;
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchKeyword.Text))
            {
                var keyword = txtSearchKeyword.Text;

                List<Address> data = _helper.GetAddress(keyword);

                gvAddresses.DataSource = data;
                gvAddresses.DataBind();

                ActivePage = "addresses";

            }
        }

        public string ActivePage
        {
            get
            {
                var sessionActivePage = Session["activePage"];
                return sessionActivePage == null ? string.Empty : sessionActivePage.ToString();
            }
            set { Session["activePage"] = value; }
        }

    }
}