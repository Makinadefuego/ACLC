using Microsoft.Maui.Controls;

namespace SCLC.Pages;

public partial class EleccionReserva : ContentPage
{
    VerticalStackLayout VerticalLayout = new VerticalStackLayout();

    HorizontalStackLayout HorizontalLayout = new HorizontalStackLayout();

    DatePicker datePicker = new DatePicker();
    public EleccionReserva()
	{
        
        

        datePicker.Date = DateTime.Now;


        HorizontalLayout.Add(datePicker);
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

    private void Boton_Clicked(object sender, EventArgs e)
    {
       //se crea una etiqueta con la fecha

        var fecha = new Entry
        {
            Text = "Fecha: " + datePicker.Date,
            FontSize = 20
            
        };
        VerticalLayout.Children.Add(fecha);

    }
}