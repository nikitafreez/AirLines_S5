using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirLines_S5
{
    public partial class AmenitiesForm : Form
    {
        public String conString = @"Data Source=LAPTOP-GTKGDTGS\NIKITASERVER;Initial Catalog=Session5_10;Integrated Security=True";

        private SqlConnection connection;
        private DataSet dataSet;
        private SqlDataAdapter dataAdapter;
        public AmenitiesForm()
        {
            InitializeComponent();
            FontsInProject();
            ApplyFonts();
        }
        PrivateFontCollection font;
        private void FontsInProject()
        {
            this.font = new PrivateFontCollection();
            this.font.AddFontFile("Fonts/TeXGyreAdventor-Regular.ttf");
        }
        private void ApplyFonts()
        {
            label1.Font = new Font(font.Families[0], 8);
            BookingBox.Font = new Font(font.Families[0], 8);
            label2.Font = new Font(font.Families[0], 8, FontStyle.Bold);
            label3.Font = new Font(font.Families[0], 8);
            label4.Font = new Font(font.Families[0], 8, FontStyle.Bold);
            label5.Font = new Font(font.Families[0], 8);
            label6.Font = new Font(font.Families[0], 8, FontStyle.Bold);
            label7.Font = new Font(font.Families[0], 8);
            label8.Font = new Font(font.Families[0], 8);
            label9.Font = new Font(font.Families[0], 8);
            label11.Font = new Font(font.Families[0], 8);
            label12.Font = new Font(font.Families[0], 8);
            PrePriceLabel.Font = new Font(font.Families[0], 8, FontStyle.Bold);
            TaxesLabel.Font = new Font(font.Families[0], 8, FontStyle.Bold);
            PricePlusTaxesLabel.Font = new Font(font.Families[0], 8, FontStyle.Bold);
            Wifi50Box.Font = new Font(font.Families[0], 8);
            DrinksBox.Font = new Font(font.Families[0], 8);
            BlanketBox.Font = new Font(font.Families[0], 8);
            TableRentalBox.Font = new Font(font.Families[0], 8);
            LoungeBox.Font = new Font(font.Families[0], 8);
            LaptopBox.Font = new Font(font.Families[0], 8);
            HeadphonesBox.Font = new Font(font.Families[0], 8);
            ExtraBagBox.Font = new Font(font.Families[0], 8);
            Wifi250Box.Font = new Font(font.Families[0], 8);
            SaveButton.Font = new Font(font.Families[0], 8);
            ExitButton.Font = new Font(font.Families[0], 8);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(conString);
            try
            {
                DrinksBox.Visible = false;
                Wifi50Box.Visible = false;
                BlanketBox.Visible = false;
                TableRentalBox.Visible = false;
                LoungeBox.Visible = false;
                LaptopBox.Visible = false;
                HeadphonesBox.Visible = false;
                ExtraBagBox.Visible = false;
                Wifi250Box.Visible = false;
                BlanketBox.Checked = false;
                TableRentalBox.Checked = false;
                LoungeBox.Checked = false;
                LaptopBox.Checked = false;
                HeadphonesBox.Checked = false;
                ExtraBagBox.Checked = false;
                Wifi250Box.Checked = false;
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM dbo.Tickets WHERE BookingReference LIKE '%{BookingBox.Text}%'", connection);
                int k = Convert.ToInt32(command.ExecuteScalar());
                if (k == 0)
                {
                    MessageBox.Show("Bad Reference code. Try Again", "Warning");
                }
                else if (k > 0)
                {
                    PrePriceLabel.Text = "0$";
                    TaxesLabel.Text = "0$";
                    PricePlusTaxesLabel.Text = "0$";
                    SqlCommand command1 = new SqlCommand($"SELECT Firstname FROM dbo.Tickets WHERE BookingReference LIKE '%{BookingBox.Text}%'", connection);
                    SqlCommand command2 = new SqlCommand($"SELECT Lastname FROM dbo.Tickets WHERE BookingReference LIKE '%{BookingBox.Text}%'", connection);
                    label2.Text = command1.ExecuteScalar().ToString() + " " + command2.ExecuteScalar().ToString();
                    SqlCommand command3 = new SqlCommand($"SELECT CabinTypeID FROM dbo.Tickets WHERE BookingReference LIKE '%{BookingBox.Text}%'", connection);
                    if (Convert.ToInt32(command3.ExecuteScalar()) == 1)
                    {
                        DrinksBox.Visible = true;
                        Wifi50Box.Visible = true;
                        label4.Text = "Economy";
                    }
                    if (Convert.ToInt32(command3.ExecuteScalar()) == 2)
                    {
                        DrinksBox.Visible = true;
                        Wifi50Box.Visible = true;
                        BlanketBox.Visible = true;
                        TableRentalBox.Visible = true;
                        LoungeBox.Visible = true;
                        label4.Text = "Business";
                    }
                    if (Convert.ToInt32(command3.ExecuteScalar()) == 3)
                    {
                        DrinksBox.Visible = true;
                        Wifi50Box.Visible = true;
                        BlanketBox.Visible = true;
                        TableRentalBox.Visible = true;
                        LoungeBox.Visible = true;
                        LaptopBox.Visible = true;
                        HeadphonesBox.Visible = true;
                        ExtraBagBox.Visible = true;
                        Wifi250Box.Visible = true;
                        label4.Text = "First Class";
                    }
                    SqlCommand command4 = new SqlCommand($"SELECT PassportNumber FROM dbo.Tickets WHERE BookingReference LIKE '%{BookingBox.Text}%'", connection);
                    label6.Text = command4.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        public static int PrePrice = 0;
        public static double Taxes = 0;
        public static double EndPrice = 0;
        private void BlanketBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BlanketBox.Checked == true)
            {
                PrePrice += 10;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            else
            {
                PrePrice -= 10;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;

            }
            PrePriceLabel.Text = PrePrice.ToString() + "$";
            TaxesLabel.Text = Taxes.ToString() + "$";
            PricePlusTaxesLabel.Text = EndPrice.ToString() + "$";
        }

        private void TableRentalBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TableRentalBox.Checked == true)
            {
                PrePrice += 12;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            else
            {
                PrePrice -= 12;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            PrePriceLabel.Text = PrePrice.ToString() + "$";
            TaxesLabel.Text = Taxes.ToString() + "$";
            PricePlusTaxesLabel.Text = EndPrice.ToString() + "$";
        }

        private void LoungeBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LoungeBox.Checked == true)
            {
                PrePrice += 25;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            else
            {
                PrePrice -= 25;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            PrePriceLabel.Text = PrePrice.ToString() + "$";
            TaxesLabel.Text = Taxes.ToString() + "$";
            PricePlusTaxesLabel.Text = EndPrice.ToString() + "$";
        }

        private void LaptopBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LaptopBox.Checked == true)
            {
                PrePrice += 15;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            else
            {
                PrePrice -= 15;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            PrePriceLabel.Text = PrePrice.ToString() + "$";
            TaxesLabel.Text = Taxes.ToString() + "$";
            PricePlusTaxesLabel.Text = EndPrice.ToString() + "$";
        }

        private void HeadphonesBox_CheckedChanged(object sender, EventArgs e)
        {
            if (HeadphonesBox.Checked == true)
            {
                PrePrice += 5;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            else
            {
                PrePrice -= 5;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            PrePriceLabel.Text = PrePrice.ToString() + "$";
            TaxesLabel.Text = Taxes.ToString() + "$";
            PricePlusTaxesLabel.Text = EndPrice.ToString() + "$";
        }

        private void ExtraBagBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ExtraBagBox.Checked == true)
            {
                PrePrice += 15;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            else
            {
                PrePrice -= 15;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            PrePriceLabel.Text = PrePrice.ToString() + "$";
            TaxesLabel.Text = Taxes.ToString() + "$";
            PricePlusTaxesLabel.Text = EndPrice.ToString() + "$";
        }

        private void Wifi250Box_CheckedChanged(object sender, EventArgs e)
        {
            if (Wifi250Box.Checked == true)
            {
                PrePrice += 25;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            else
            {
                PrePrice -= 25;
                Taxes = PrePrice * 0.05;
                EndPrice = PrePrice + Taxes;
            }
            PrePriceLabel.Text = PrePrice.ToString() + "$";
            TaxesLabel.Text = Taxes.ToString() + "$";
            PricePlusTaxesLabel.Text = EndPrice.ToString() + "$";
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (PricePlusTaxesLabel.Text != "....")
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Not yet. Non finished", "Warning");
            }
        }
    }
}
