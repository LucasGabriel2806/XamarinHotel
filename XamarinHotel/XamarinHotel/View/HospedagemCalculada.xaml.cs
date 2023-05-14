using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinHotel.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HospedagemCalculada : ContentPage
    {
        public HospedagemCalculada()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}