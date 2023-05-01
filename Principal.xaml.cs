namespace SCLC;

public partial class Principal : ContentPage
{
	public Principal()
	{
		InitializeComponent();
	}

    private async void IrInicioSesion(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new MainPage());
    }

    private async void IrVisualizacion(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VisualizacionReservas());
    }
}