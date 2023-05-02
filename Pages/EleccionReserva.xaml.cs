using Microsoft.Maui.Controls;
using SCLC.Models;
using SCLC.Services;

namespace SCLC.Pages;

public partial class EleccionReserva : ContentPage
{
    VerticalStackLayout VerticalLayout = new VerticalStackLayout();

    HorizontalStackLayout HorizontalLayout = new HorizontalStackLayout();

    DatePicker datePicker = new DatePicker();

    Usuario _usuario;
    Laboratorio _laboratorio;
    int _modulo;
    DateTime _fecha;
    
    public EleccionReserva(Usuario usuario, Laboratorio laboratorio, int modulo, DateTime fecha)
	{
        _usuario = usuario;
        _laboratorio = laboratorio;
        _modulo = modulo;
        _fecha = fecha;





        Label mod = new Label
        {
            Text = "Modulo: " + modulo,
            FontSize = 20
        };

        Button ReservarTodo = new Button
        { 
            Text = "Reservar todo",
            FontSize = 10,
            BackgroundColor = Color.FromHex("#82014A"),
            CornerRadius = 10,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };

        ReservarTodo.Clicked += Boton_Clicked;

        datePicker.Date = fecha;


        HorizontalLayout.Add(datePicker);
        HorizontalLayout.Add(mod);
        HorizontalLayout.Add(ReservarTodo);
        VerticalLayout.Add(HorizontalLayout);
        VerticalLayout.HorizontalOptions = LayoutOptions.CenterAndExpand;

       

        //Se crean elementos de la grilla de 9*4
        Grid computadoras = new Grid();
        
        for (int i = 0; i < 9; i++)
        {
            computadoras.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) });
        }
        for (int i = 0; i < 4; i++)
        {
            computadoras.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
        }
        //Se crean los image button de cada elemento de la grilla

        for (int i = 0; i < 4; i++)
		{
            for (int j = 0; j < 9; j++)
			{
                if(!(j == 4 || (j == 2 && i ==0) || (j == 3 && i == 0)))
                {
                    ImageButton boton = new ImageButton
                    {
                        ClassId = "lab" + (i + 1) + (j + 1),
                        Source = "lab1.png",
                        Aspect = Aspect.AspectFit,
                    };
                    boton.Clicked += Boton_Clicked;
                    Grid.SetRow(boton, i);
                    Grid.SetColumn(boton, j);
                    computadoras.Children.Add(boton);
                }
                
            }
        }
        VerticalLayout.Add(computadoras);
        //Se actualiza el contenido de la pagina
        
        InitializeComponent();
        Content = VerticalLayout;
    }

    public async void Boton_Clicked(object sender, EventArgs e)
    {

        IReservacionModulo reservarModulo = new ModuloReservacionServicio();

        bool reservacionExitosa = await reservarModulo.ReservarModulo( _usuario.boleta, _modulo, _fecha, _laboratorio.idLaboratorio );

        Label label = new Label()
        {
            Text = "Mal"
        };
        if ( reservacionExitosa )
        {
            label.Text = "Bien";
            
        }
        VerticalLayout.Children.Add(label);

        Content = VerticalLayout;

    }
}