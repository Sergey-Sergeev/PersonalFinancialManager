

using PersonalFinancialManager.source;

namespace PersonalFinancialManager
{
    public partial class MainForm : Form
    {
        FTS fts;
        public MainForm()
        {            



            InitializeComponent();
            fts = FTS.FTSFabric("0", "0!");          // TO DO: Get login and password from user
        }

        private void loadQRCodesButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg";
            ofd.FilterIndex = 0;
            ofd.Multiselect = true;


            if (ofd.ShowDialog() != DialogResult.Cancel && ofd.FileNames.Length != 0)
            {
                fts.GetReceiptsFromQRCodes(ofd.FileNames);
            }

        }
    }
}
