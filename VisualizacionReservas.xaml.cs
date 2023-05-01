using SCLC.Pages;

namespace SCLC;

public partial class VisualizacionReservas : ContentPage
{
	public VisualizacionReservas()
	{
		InitializeComponent();
	}

	async void Button_Clicked(object sender, EventArgs e)
	{

        await Navigation.PushAsync(new EleccionReserva());

    }
}