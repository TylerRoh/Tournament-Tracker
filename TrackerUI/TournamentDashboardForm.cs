using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentDashboardForm : Form, ITournamentRequester
    {
        private List<TournamentModel> tournaments = new List<TournamentModel>(GlobalConfig.Connection.GetTournament_All());
        public TournamentDashboardForm()
        {
            InitializeComponent();

            WireUpList();
        }

        private void WireUpList()
        {
            loadExistingTournamentDropDown.DataSource = null;

            loadExistingTournamentDropDown.DataSource = tournaments;
            loadExistingTournamentDropDown.DisplayMember = "TournamentName";
        }


        public void TournamentComplete(TournamentModel model)
        {
            throw new NotImplementedException();
        }

        private void headerLable_Click(object sender, EventArgs e)
        {

        }

        private void TournamentDashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm frm = new CreateTournamentForm(this);
            frm.Show();
        }
    }
}
