using AdresDefteri.DataLayer;
using AdresDefteri.Model;
using System;
using System.Web.UI;

namespace AdresDefteri
{
    public partial class Detay : System.Web.UI.Page
    {
        private AddressDataHelper _helper = new AddressDataHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id;

                if (int.TryParse(Request.QueryString["id"], out id))
                {
                    FormDataBind(id);
                }

            }
        }

        private void FormDataBind(int id)
        {
            var address = _helper.GetAddress(id);

            txtName.Text = address.Name;
            txtSurname.Text = address.SurName;
            txtNick.Text = address.Nick;
            calBirth.SelectedDate = address.BirthDate;
            ddlGender.SelectedValue = address.Gender ? "Bay" : "Bayan";
            txtMobile.Text = address.MobileNumber;
            txtHome.Text = address.HomeNumber;
            txtEmail.Text = address.Email;
            txtFax.Text = address.Fax;
            txtAddress.Text = address.UserAddress;
            txtNote.Text = address.Note;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int id;

            if (!int.TryParse(Request.QueryString["id"], out id))
            {
                return;
            }

            var address = new Address();

            address.Id = id;
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

            var res = _helper.UpdateAddress(address);

            if (res > 0)
            {
                lblInfo.Visible = true;
            }

        }

        protected void btnDeleteAddress_OnClick(object sender, EventArgs e)
        {
            //Data katmanında sil 
            var id = 0;
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                if (_helper.DeleteAddress(id) > 0)
                {
                    //Anasayfaya yönlendir
                    ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>window.parent.location.href='/AnaSayfa.aspx'</script>");
                }
            }
        }
    }
}