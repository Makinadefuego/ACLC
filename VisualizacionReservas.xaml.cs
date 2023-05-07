
using SCLC.Models;
using SCLC.Pages;
using System.Globalization;

namespace SCLC;

public partial class VisualizacionReservas : ContentPage
{
    List<DatosBoton> listaBotones = new List<DatosBoton>();
    Laboratorio labseleccionado;
    
	public string CalcularInicioSemana( DateTime fechaActual)
	{
        CultureInfo cultura = new CultureInfo("es-ES");
        int diaDeLaSemana = (int)fechaActual.DayOfWeek;

		int diaInicio = (int)fechaActual.Day;
		int diaFin;

		if (diaDeLaSemana != 0)
		{
			diaInicio -= diaDeLaSemana;
		}
        diaFin = diaInicio + 6;

        return "SEMANA DE " + (diaInicio + 1).ToString() + " - " + (diaFin + 1).ToString() + " de " + cultura.DateTimeFormat.GetMonthName(fechaActual.Month);
    }

    public int InicioSemana(DateTime fechaActual)
    {
        int diaDeLaSemana = (int)fechaActual.DayOfWeek;
        int diaInicio = (int)fechaActual.Day;        
        diaInicio -= diaDeLaSemana;

       Console.WriteLine("Dia de la semana: " + diaDeLaSemana);

        return diaInicio + 1;
    }


    public VisualizacionReservas(Laboratorio laboratorio)
	{
		int desfase = 0;
        labseleccionado = laboratorio;
		DateTime fechaActual = DateTime.Now.AddDays(desfase);

        string bgColor = "#82014A";
		string rosaClaro = "#E9beda";
        string rosaOscuro = "#cf0276";
		string guindaClaro = "#82014A";
		string guidaOscuro = "#661943";


#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        ScrollView scrollView = new ScrollView()
		{
			BackgroundColor = Color.FromHex(bgColor)
		};
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

		VerticalStackLayout verticalPrincipal = new VerticalStackLayout();

		HorizontalStackLayout horizontalPrincipal = new HorizontalStackLayout()
		{
			HorizontalOptions = LayoutOptions.Center
		};

		 
		ImageButton trianguloI = new ImageButton()
		{
			Source = "triangulo.png",
			MaximumHeightRequest = 50,
			MaximumWidthRequest = 50,
			BackgroundColor = Colors.Transparent,
			Rotation = -90
		};

        ImageButton trianguloD = new ImageButton()
        {
            Source = "triangulo.png",
            MaximumHeightRequest = 50,
            MaximumWidthRequest = 50,
            BackgroundColor = Colors.Transparent,
            Rotation = 90
        };




		//Se crea el primer grid 3 columnas y 1 fila
		Grid etiquetas = new Grid()
		{
			BackgroundColor = Colors.White,
			MaximumHeightRequest= 50
		};

        //Se crean las defeniciones del columnas y filas para ese grid
        for (int i = 0; i < 2; i++)
		{
			etiquetas.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) });
		}
		etiquetas.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Absolute) });
		etiquetas.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });   

		//Se crean los elementos que van dentreo del gri etiquetas
		Label etiqueta1 = new Label()
		{
			Text = "Reservaciones",
			VerticalOptions = LayoutOptions.Center,
			HorizontalOptions = LayoutOptions.Center
		};
		Grid.SetColumn(etiqueta1, 0);
		Grid.SetRow(etiqueta1, 0);

        Label etiqueta2 = new Label()
        {
            Text = "LAB " + laboratorio.idLaboratorio,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        Grid.SetColumn(etiqueta2, 1);
        Grid.SetRow(etiqueta2, 0);

        Label etiqueta3 = new Label()
        {
            Text = CalcularInicioSemana(DateTime.Today),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        Grid.SetColumn(etiqueta3, 2);
        Grid.SetRow(etiqueta3, 0);


		etiquetas.Children.Add(etiqueta1);
		etiquetas.Children.Add(etiqueta2);
		etiquetas.Children.Add(etiqueta3);

		//Se hace el grid que contiene los botones
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
		Grid horario = new Grid()
		{
			MinimumWidthRequest = 750,
			Margin = new Thickness(0, 20, 0, 0),
			BackgroundColor = Color.FromHex("#C40093"),
			HorizontalOptions = LayoutOptions.Center,

		};
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

		//Se crea las definiciones de fila y columna de de la grilla
		for(int i = 0; i < 6; i++)
		{
			horario.ColumnDefinitions.Add(new ColumnDefinition { Width =  new GridLength(125, GridUnitType.Absolute) });
		}
        for (int i = 0; i < 9; i++)
        {
            horario.RowDefinitions.Add(new RowDefinition { Height= new GridLength(50, GridUnitType.Absolute) });
        }

        //Primero se crean los elementos que no son dinámicos, los horarios de la parte izquierda y los días de la semana que van en la parte de arriba
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Label labelModulos = new Label()
		{
			Text = "Módulos",
			TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
			BackgroundColor = Color.FromHex(guindaClaro)
        }; 	
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
		Grid.SetRow(labelModulos, 0);
		Grid.SetColumn(labelModulos, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
		Label Lunes = new Label()
		{
			Text = "Lunes",
			TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guidaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(Lunes, 0);
        Grid.SetColumn(Lunes, 1);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Label Martes = new Label()
        {
            Text = "Martes",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guidaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(Martes, 0);
        Grid.SetColumn(Martes, 2);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
		Label Miercoles = new Label()
		{
			Text = "Miercoles",
			TextColor = Colors.White,
			Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guidaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
		Grid.SetRow(Miercoles, 0);
		Grid.SetColumn(Miercoles, 3);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Label Jueves = new Label()
        {
            Text = "Jueves",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guidaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(Jueves, 0);
        Grid.SetColumn(Jueves, 4);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
		Label Viernes = new Label()
		{
			Text ="Viernes",
			TextColor = Colors.White,
			Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guidaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
		Grid.SetRow(Viernes, 0);
		Grid.SetColumn (Viernes, 5);


		//Se crean las etiquetas de los módulos
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
		Label modulo1 = new Label()
		{
			Text = "7:00-8:30",
			TextColor = Colors.Black,
			Padding = new Thickness(30, 10, 0, 0),
			BackgroundColor = Color.FromHex(rosaClaro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo1, 1);
        Grid.SetColumn(modulo1, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Label modulo2 = new Label()
        {
            Text = "8:30-10:00",
            TextColor = Colors.White,
            Padding = new Thickness(25, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo2, 2);
        Grid.SetColumn(modulo2, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
		Label modulo3 = new Label()
		{
			Text = "10:00-11:30",
			TextColor = Colors.Black,
			Padding = new Thickness(20, 10, 0, 0),
			BackgroundColor = Color.FromHex(rosaClaro)
		};
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
		Grid.SetRow(modulo3, 3);
		Grid.SetColumn(modulo3, 0);


#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Label modulo4 = new Label()
        {
            Text = "11:30-13:00",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo4, 4);
        Grid.SetColumn(modulo4, 0);


#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Label modulo5 = new Label()
        {
            Text = "13:00-14:30",
            TextColor = Colors.Black,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaClaro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo5, 5);
        Grid.SetColumn(modulo5, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
		Label modulo6 = new Label()
		{
			Text = "14:30-16:00",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
		Grid.SetRow(modulo6, 6);
		Grid.SetColumn(modulo6, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
		Label modulo7 = new Label()
		{
			Text = "16:00-17:30",
            TextColor = Colors.Black,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaClaro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
		Grid.SetRow(modulo7, 7);
		Grid.SetColumn(modulo7, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
		Label modulo8 = new Label()
		{
			Text = "17:30-19:00",
			TextColor = Colors.White,
			Padding = new Thickness(20, 10, 0, 0),
			BackgroundColor = Color.FromHex(rosaOscuro)
		};
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
		Grid.SetRow(modulo8, 8);
		Grid.SetColumn(modulo8, 0);


        //Se insertan todas las etiquetas estáticas en el grid llamado horario
        horario.Children.Add(modulo1);
		horario.Children.Add(modulo2);
		horario.Children.Add(modulo3);
		horario.Children.Add(modulo4);
		horario.Children.Add(modulo5);
		horario.Children.Add(modulo6);
		horario.Children.Add(modulo7);
		horario.Children.Add(modulo8);
		horario.Children.Add(labelModulos);
		horario.Children.Add(Lunes);
		horario.Children.Add(Martes);
		horario.Children.Add(Miercoles);
		horario.Children.Add(Jueves);
		horario.Children.Add(Viernes);

        //Se crean los botones que llevan a la pagina de EleccionReserva
        //Primero se crea una lista de datosboton

		//

		DateTime fechaFinal = new DateTime(fechaActual.Year, fechaActual.Month, InicioSemana(fechaActual));
        
        for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				
				DatosBoton nuevoBoton = new DatosBoton(j + 1, fechaFinal.AddDays(i)) ;
				Grid.SetRow(nuevoBoton.boton, j+1);
				Grid.SetColumn(nuevoBoton.boton, i+1);

                nuevoBoton.boton.Clicked += Button_Clicked;

                horario.Children.Add(nuevoBoton.boton);
				listaBotones.Add(nuevoBoton);
			}
            
        }

		scrollView.Content = verticalPrincipal;
		horizontalPrincipal.Children.Add(trianguloI);
		horizontalPrincipal.Children.Add(etiquetas);
		horizontalPrincipal.Children.Add(trianguloD);

		verticalPrincipal.Children.Add(horizontalPrincipal);
		verticalPrincipal.Children.Add(horario);











        InitializeComponent();

		Content = scrollView;
	}

	async void Button_Clicked(object sender, EventArgs e)
	{
		int modulo = 0;
		DateTime fechaDelBoton = new DateTime();


		//encontrar el DatosBoton de entre la lista 
		for(int i = 0; i < 6; i++)
		{
			for(int j = 0; j< 8; j++)
			{
				for(int k = 0; k < listaBotones.Count; k++)
				{
					if(sender.Equals(listaBotones[k].boton))
					{
						modulo = listaBotones[k].modulo;
						fechaDelBoton = listaBotones[k].fecha;
						break;
					}
				}
			}
		}
		

        await Navigation.PushAsync( new EleccionReserva(new Usuario(){ boleta = 2022710161, password = "hardcoded" }, labseleccionado, modulo, fechaDelBoton) );

    }

}