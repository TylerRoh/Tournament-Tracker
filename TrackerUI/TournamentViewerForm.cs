using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel tournament;

        private int roundNumber;

        public TournamentViewerForm(TournamentModel model)
        {
            InitializeComponent();

            tournament = model;

            WireUpTournamentInfo();

            roundDropDown.SelectedIndexChanged += roundDropDown_SelectedIndexChanged;
            MatchupListBox.SelectedIndexChanged += MatchupListBox_SelectedIndexChanged;
        }

        private void WireUpTournamentInfo()
        {
            tournamentName.Text = tournament.TournamentName;
            roundDropDown.DataSource = Enumerable.Range(1, tournament.Rounds.Count).ToList();
        }
        private void MatchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                if (round.First().MatchupRound == roundNumber)
                {
                    
                }
            }
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            roundNumber = (int)roundDropDown.SelectedItem;

            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                if (round.First().MatchupRound == roundNumber)
                {
                    MatchupListBox.DataSource = round;
                    MatchupListBox.DisplayMember = "Id";
                    break;
                }

            }
        }

        private void TournamentViewerForm_Load(object sender, EventArgs e)
        {

        }


    }
}
