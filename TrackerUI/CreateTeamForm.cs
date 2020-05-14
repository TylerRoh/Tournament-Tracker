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
        private List<PersonModel> availableTeamMembers = new List<PersonModel>(GlobalConfig.Connection.GetPerson_All());
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        public CreateTeamForm()
        {
            InitializeComponent();

            //CreateTestData();

            WireUpLists();
        }


        private void CreateTestData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Tyler", LastName = "Rohweder"});
            availableTeamMembers.Add(new PersonModel { FirstName = "Kelly", LastName = "DiMatteo" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Joe", LastName = "Rogan" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Sterling", LastName = "Archer" });
        }

        private void WireUpLists()
        {

            // TODO - Look into a better way to refresh data binding
            selectTeamMemberDropDown.DataSource = null;

            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
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

                selectedTeamMembers.Add(model);
                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                phoneValue.Text = "";

                WireUpLists();

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

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists();
            }

        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            if(p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();
            }
            
        }
    }
}
