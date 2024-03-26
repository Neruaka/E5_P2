using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;

namespace WindowsFormsAppGM23
{
    public partial class FormGM : Form
    {
        public FormGM()
        {
            InitializeComponent();
        }

        private void buttonQuitter_Click(object sender, EventArgs e)
        {
            goodbye();
        }

        private void goodbye()
        {
            DialogResult dr = MessageBox.Show("Voulez-vous vraiment quitter ?",
                "Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            goodbye();
        }

        private void FormGM_Load(object sender, EventArgs e)
        {
            fillMatos();
            fillMateriel();
        }

        private void fillMatos()
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select Nom_modele from Materiel";
            SqlCommand ccom = new SqlCommand(strsql, cn);

            SqlDataReader sqreader = ccom.ExecuteReader();

            comboBoxCreationMatos.Items.Clear();
            while (sqreader.Read() == true)
            {
                string str = sqreader["Nom_modele"].ToString();
                comboBoxCreationMatos.Items.Add(str);
            }

            sqreader.Close();
            cn.Close();
        }


        private int findMatosId(string nommatos)
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select ID_MATOS from Materiel " +
                    "where Nom_modele = '" +
                    comboBoxCreationMatos.SelectedItem.ToString() + "'";
            SqlCommand ccom = new SqlCommand(strsql, cn);

            SqlDataReader sqreader = ccom.ExecuteReader();

            sqreader.Read();


            return Convert.ToInt32(sqreader["ID_MATOS"]);

        }

        private int findMaterielId(string nommatos)
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select ID_MATOS from Materiel " +
                    "where Nom_modele = '" +
                    comboBoxMatos.SelectedItem.ToString() + "'";
            SqlCommand ccom = new SqlCommand(strsql, cn);

            SqlDataReader sqreader = ccom.ExecuteReader();

            sqreader.Read();

            return Convert.ToInt32(sqreader["ID_MATOS"]);

        }


        private void buttonCreateInter_Click(object sender, EventArgs e)
        {
            if (comboBoxCreationMatos.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un matériel !", "Avertissement");
                comboBoxCreationMatos.Focus();
                return;
         
            }

            int idmatos = findMatosId(comboBoxCreationMatos.SelectedItem.ToString());


            string thedate = dateTimePickerCreationInter.Value.Date.ToString();
            string comment = textBoxCommentaire.Text;
            string theid = idmatos.ToString();

            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();


            string strsql = "INSERT INTO Intervention VALUES('" + thedate +
                "','" + comment + "'," + theid + ")";

            SqlCommand ccom = new SqlCommand(strsql, cn);
            ccom.ExecuteNonQuery();

            MessageBox.Show("Intervention enregistrée", "Confirmation");


        }

        private void fillMateriel()
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select Nom_modele from Materiel";
            SqlCommand ccom = new SqlCommand(strsql, cn);

            SqlDataReader sqreader = ccom.ExecuteReader();

            comboBoxMatos.Items.Clear();
            while (sqreader.Read() == true)
            {
                string str = sqreader["Nom_modele"].ToString();
                comboBoxMatos.Items.Add(str);
            }

            sqreader.Close();
            cn.Close();
        }

        private void buttonAfficher_Click(object sender, EventArgs e)
        {
            if (comboBoxMatos.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un matériel !", "Avertissement");
                comboBoxMatos.Focus();
                return;
            }

            int idmatos = findMaterielId(comboBoxMatos.SelectedItem.ToString());

            string thedateDeb = dateTimePickerDebut.Value.Date.ToString();
            string thedateFin = dateTimePickerFin.Value.Date.ToString();
            string theid = idmatos.ToString();

            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();


            string strsql = "SELECT * FROM Intervention where " +
                "Date_inter >= '" + thedateDeb + "' and Date_inter <= '" +
                thedateFin + "' and ID_MATOS = " + theid;

            SqlCommand ccom = new SqlCommand(strsql, cn);

            SqlDataReader sqreader = ccom.ExecuteReader();

            listViewInter.Items.Clear();
            while (sqreader.Read() == true)
            {
                string s = Convert.ToDateTime(sqreader["Date_Inter"])
                    .ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                string[] row = { s, sqreader["ID_MATOS"].ToString(),
                    sqreader["Commentaire"].ToString() };
                var listViewItem = new ListViewItem(row);
                listViewInter.Items.Add(listViewItem);
            }

            sqreader.Close();
            cn.Close();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClient dlg = new FormClient();
            dlg.ShowDialog();
        }

        private void matérielToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMateriel dlg = new FormMateriel();
            dlg.ShowDialog();
        }
    }
}
