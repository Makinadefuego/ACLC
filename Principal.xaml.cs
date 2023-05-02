using SCLC.Models;

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

    private async void IrLab1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VisualizacionReservas(new Laboratorio(){ idLaboratorio =1} ) );
    }

    private async void IrLab2(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VisualizacionReservas(new Laboratorio(){ idLaboratorio = 2 }));
    }
}
