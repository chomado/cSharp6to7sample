using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CSharp6to7sample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var loader = new Loader();

            var result = await loader.LoadAsync(this.entryInput.Text);
            if (result.Error is ArgumentException)
            {
                this.labelOutput.Text = "入力値が不正です";
                return;
            }

            var ex = result.Error as WebException;
            if (ex != null)
            {
                this.labelOutput.Text = $"Error: {ex.Status}";
                return;
            }

            labelOutput.Text = result.Text;
        }
	}

    public class Loader
    {
        public async Task<ResultData> LoadAsync(string input)
        {
            await Task.Delay(3000);

            int i;
            if (!int.TryParse(input, out i))
            {
                return new ResultData { Error = new ArgumentException() };
            }

            if (i < 0)
            {
                return new ResultData { Error = new WebException("error", WebExceptionStatus.ConnectFailure) };
            }

            return new ResultData { Text = DateTime.Now.ToString() };
        }
    }

    public class ResultData
    {
        public string Text { get; set; }
        public Exception Error { get; set; }
    }
}
