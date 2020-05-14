using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        public CreateTeamForm()
        {
            InitializeComponent();
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel model = new PersonModel(
                    firstNameValue.Text,
                    lastNameValue.Text,
                    emailValue.Text,
                    phoneValue.Text);

                GlobalConfig.Connection.CreatePerson(model);

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                phoneValue.Text = "";

            }
            else
            {
                MessageBox.Show("This form has invalid information. Please check and try again.");
            }
        }

        private bool ValidateForm()
        {
            bool output = true;

            if (String.IsNullOrWhiteSpace(firstNameValue.Text))
            {
                output = false;
            }
            
            if (String.IsNullOrWhiteSpace(lastNameValue.Text))
            {
                output = false;
            }

            if (String.IsNullOrWhiteSpace(emailValue.Text))
            {
                output = false;
            }
            else
            {
                try
                {
                    MailAddress m = new MailAddress(emailValue.Text);
                }
                catch (FormatException)
                {
                    output = false;
                }
            }

            return output;
        }
    }
}
