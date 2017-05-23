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
            if (result.error is ArgumentException)
            {
                this.labelOutput.Text = "入力値が不正です";
                return;
            }

            var ex = result.error as WebException;
            if (ex != null)
            {
                this.labelOutput.Text = $"Error: {ex.Status}";
                return;
            }

            labelOutput.Text = result.text;
        }
	}

    public class Loader
    {
        public async Task<(string text, Exception error)> LoadAsync(string input)
        {
            await Task.Delay(3000);

            int i;
            if (!int.TryParse(input, out i))
            {
                return (null, new ArgumentException());
            }

            if (i < 0)
            {
                return (null, new WebException("error", WebExceptionStatus.ConnectFailure));
            }

            return (DateTime.Now.ToString(), null);
        }
    }

}
