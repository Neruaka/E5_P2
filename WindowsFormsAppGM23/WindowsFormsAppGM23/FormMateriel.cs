using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppGM23
{
    public partial class FormMateriel : Form
    {
        public FormMateriel()
        {
            InitializeComponent();
        }

        private void FormMateriel_Load(object sender, EventArgs e)
        {
            ListerMatos();
            ListerClients();
            ListerType();
            buttonAdd.Enabled = false;
        }

        private void ListerMatos()
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select nom_modele from Materiel";
            SqlCommand ccom = new SqlCommand(strsql, cn);
            SqlDataReader sqreader = ccom.ExecuteReader();

            listBoxMatos.Items.Clear();
            while (sqreader.Read() == true)
            {
                string str = sqreader["nom_modele"].ToString();
                listBoxMatos.Items.Add(str);
            }

            sqreader.Close();
            cn.Close();
        }

        private void ListerClients()
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select nom from Client";
            SqlCommand ccom = new SqlCommand(strsql, cn);
            SqlDataReader sqreader = ccom.ExecuteReader();

            comboBoxClient.Items.Clear();
            while (sqreader.Read() == true)
            {
                string str = sqreader["Nom"].ToString();
                comboBoxClient.Items.Add(str);
            }

            sqreader.Close();
            cn.Close();
        }

        private void ListerType()
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select nom from Type_materiel";
            SqlCommand ccom = new SqlCommand(strsql, cn);
            SqlDataReader sqreader = ccom.ExecuteReader();

            comboBoxType.Items.Clear();
            while (sqreader.Read() == true)
            {
                string str = sqreader["Nom"].ToString();
                comboBoxType.Items.Add(str);
            }

            sqreader.Close();
            cn.Close();
        }

        private void listBoxMatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nomMatos = listBoxMatos.SelectedItem.ToString();

            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select * from Materiel where nom_modele = '" + nomMatos + "'";
            SqlCommand ccom = new SqlCommand(strsql, cn);
            SqlDataReader sqreader = ccom.ExecuteReader();

            sqreader.Read();

            textBoxModele.Text = nomMatos;
            textBoxMarque.Text = sqreader["Marque"].ToString();
            textBoxSerie.Text = sqreader["NoSerie"].ToString();
            textBoxPrix.Text = sqreader["Prix"].ToString();
            dateTimePickerInstall.Value = Convert.ToDateTime(sqreader["Date_installation"]);

            int idclient = Convert.ToInt32(sqreader["ID_CLIENT"]);
            comboBoxClient.SelectedItem = findClientById(idclient);

            int idtype = Convert.ToInt32(sqreader["ID_TYPE_MATOS"]);
            comboBoxType.SelectedItem = findTypeById(idtype);
                       

            sqreader.Close();
            cn.Close();
        }

        private string findClientById(int id)
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select nom from client where ID_CLIENT = " + id;
            SqlCommand ccom = new SqlCommand(strsql, cn);

            SqlDataReader sqreader = ccom.ExecuteReader();

            sqreader.Read();

            return sqreader["nom"].ToString();

        }

        private int findClientByName(string nom)
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select ID_CLIENT from client where nom = '" + nom + "'";
            SqlCommand ccom = new SqlCommand(strsql, cn);

            SqlDataReader sqreader = ccom.ExecuteReader();

            sqreader.Read();

            return Convert.ToInt32(sqreader["ID_CLIENT"]);
        }

        private string findTypeById(int id)
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select nom from Type_materiel where ID_TYPE_MATOS = " + id;
            SqlCommand ccom = new SqlCommand(strsql, cn);

            SqlDataReader sqreader = ccom.ExecuteReader();

            sqreader.Read();

            return sqreader["nom"].ToString();

        }

        private int findTypeByName(string nom)
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select ID_TYPE_MATOS from Type_materiel where nom = '" + nom + "'";
            SqlCommand ccom = new SqlCommand(strsql, cn);

            SqlDataReader sqreader = ccom.ExecuteReader();

            sqreader.Read();

            return Convert.ToInt32(sqreader["ID_TYPE_MATOS"]);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string nom_modele = textBoxModele.Text;
            string marque = textBoxMarque.Text;
            string serie = textBoxSerie.Text;
            double prix = Convert.ToDouble(textBoxPrix.Text);

            string ladate = dateTimePickerInstall.Value.ToString("yyyy-MM-dd");

            int idClient = findClientByName(comboBoxClient.SelectedItem.ToString());
            int idTypeMat = findTypeByName(comboBoxType.SelectedItem.ToString());

            string strsql = "INSERT INTO Materiel VALUES ('" + nom_modele + "','" +
                marque + "','" + serie + "'," + prix + ",'" + ladate + "'," +
                idTypeMat + "," + idClient + ")";

            SqlCommand ccom = new SqlCommand(strsql, cn);

            ccom.ExecuteNonQuery();

            cn.Close();

            ListerMatos();

            buttonModifier.Enabled =
             buttonDel.Enabled =
              listBoxMatos.Enabled =
             true;

            buttonAdd.Enabled = false;
            buttonNew.Enabled = true;

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            string nom_modele = textBoxModele.Text;
            string marque = textBoxMarque.Text;
            string serie = textBoxSerie.Text;
            double prix = Convert.ToDouble(textBoxPrix.Text);

            string ladate = dateTimePickerInstall.Value.ToString("yyyy-MM-dd");

            int idClient = findClientByName(comboBoxClient.SelectedItem.ToString());
            int idTypeMat = findTypeByName(comboBoxType.SelectedItem.ToString());

            string strsql = "UPDATE Materiel SET Nom_modele = '"
                + nom_modele + "', Marque = '" + marque + "', NoSerie ='"
                + serie.ToString() + "', Prix = " 
                + prix.ToString() + ", Date_installation = '"
                + ladate + "', ID_TYPE_MATOS = " + idTypeMat.ToString() 
                + ", ID_CLIENT = " + idClient + " where Nom_modele = '" 
                + nom_modele + "'";

            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            SqlCommand ccom = new SqlCommand(strsql, cn);
            ccom.ExecuteNonQuery();

            cn.Close();

            ListerClients();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            textBoxModele.Text = textBoxMarque.Text =
                textBoxSerie.Text = textBoxPrix.Text = "";

            dateTimePickerInstall.Value = DateTime.Now;

            comboBoxClient.SelectedIndex =
                comboBoxType.SelectedIndex = -1;

            textBoxModele.Enabled = true;
            textBoxModele.Focus();

            buttonModifier.Enabled = 
                buttonDel.Enabled =
                listBoxMatos.Enabled = 
                false;            

            buttonAdd.Enabled = true;
            buttonNew.Enabled = false;
        }
    }
}
