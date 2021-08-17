using System.Windows.Forms;


namespace CancellationTokenExample
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// In fact, when you cancel code, there are three possibilities: 
        /// 1) It may respond tothe cancellation request (throwing OperationCanceledException).
        /// 2) It may finish successfully.
        /// 3) It may finish with an error unrelated to the cancellation (throwing a different exception).
        /// 
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
            CancelButton.Enabled = false;
            _cancellationTokenSource = new();
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = false;
            CancelButton.Enabled = true;

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(5), _cancellationTokenSource.Token);
                MessageBox.Show("Delay completed successfully.");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Delay was canceled.");
                //throw;
            }
            catch (Exception)
            {
                MessageBox.Show("Delay completed with error.");
                throw;
            }
            finally
            {
                StartButton.Enabled = true;
                CancelButton.Enabled = false;
                _cancellationTokenSource = new(); // comment this line at the first. after that if you want interactive UI uncomment this line.
            }


        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
            CancelButton.Enabled = false;
        }
    }
}
