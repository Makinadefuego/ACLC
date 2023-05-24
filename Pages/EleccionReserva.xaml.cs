using Microsoft.Maui.Controls;
using SCLC.Models;
using SCLC.Services;
using System.Linq;
using Newtonsoft.Json;

namespace SCLC.Pages;

public partial class EleccionReserva : ContentPage
{
    VerticalStackLayout VerticalLayout = new VerticalStackLayout();

    HorizontalStackLayout HorizontalLayout = new HorizontalStackLayout();

    //Se crean elementos de la grilla de 9*4
    Grid computadoras = new Grid()
    {
        //MinimumWidthRequest = 500,
        VerticalOptions = LayoutOptions.Center,
        HorizontalOptions = LayoutOptions.Center,
        Margin = new Thickness(50, 0, 0, 0),
        BackgroundColor = Color.FromHex("#E9beda")
    };

    DatePicker datePicker = new DatePicker();

    List<int> _reservadas = new List<int>();



    Usuario _usuario;
    Laboratorio _laboratorio;
    int _modulo;
    DateTime _fecha;

    //Se crea la lista que contiene el id de las computadoras que el usuario selecciono
    List<int> _computadorasSeleccionadas = new List<int>();

    public EleccionReserva(Usuario usuario, Laboratorio laboratorio, int modulo, DateTime fecha)
    {
        _usuario = usuario;
        _laboratorio = laboratorio;
        _modulo = modulo;
        _fecha = fecha;
        _reservadas = new List<int>();

        string cadenaFecha = fecha.ToString("dd/MM/yyyy");


        Console.WriteLine(cadenaFecha);




        Label mod = new Label
        {
            Text = "Modulo: " + modulo,
            FontSize = 20
        };



        datePicker.Date = fecha;


        //HorizontalLayout.Add(datePicker);
        //HorizontalLayout.Add(mod);
        //HorizontalLayout.Add(ReservarTodo);
        VerticalLayout.Add(HorizontalLayout);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        VerticalLayout.HorizontalOptions = LayoutOptions.CenterAndExpand;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos





        for (int i = 0; i < 9; i++)
        {
            computadoras.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) });
        }
        for (int i = 0; i < 4; i++)
        {
            computadoras.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
        }
        CargarComputadoras();

        //VerticalLayout.Add(computadoras);
        //Se actualiza el contenido de la pagina	

        InitializeComponent();



        Grid.SetColumn(computadoras, 1);
        Grid.SetRow(computadoras, 1);



        contenedor.Children.Add(computadoras);
        labelLab.Text = "Laboratorio: " + laboratorio.idLaboratorio;
        labelDate.Text = "Fecha: " + cadenaFecha;
        labelModulo.Text = "Modulo: " + modulo;

        labelTime.Text = "Horario: " + CalcularHorarioModulo(modulo);

        picker.Date = _fecha;

    }

    public string CalcularHorarioModulo(int modulo)
    {

        if (modulo == 1)
        {
            return "7:00-7:30";
        }
        if (modulo == 2)
        {
            return "8:30-10:00";
        }
        if (modulo == 3)
        {
            return "10:00-11:30";
        }
        if (modulo == 4)
        {
            return "11:30-13:00";
        }
        if (modulo == 5)
        {
            return "13:00-14:30";
        }
        if (modulo == 6)
        {
            return "14:30-16:00";
        }
        if (modulo == 7)
        {
            return "16:00-17:30";
        }
        if (modulo == 8)
        {
            return "17:30-19:00";
        }

        return null;

    }
    public void CrearComputadoras()
    {



        bool disponible = false;
        string sourceG = "pc_green.png";
        string sourceR = "pc_red.png";
#pragma warning disable CS0219 // La variable está asignada pero nunca se usa su valor
        string sourceY = "pc_yellow.png";
#pragma warning restore CS0219 // La variable está asignada pero nunca se usa su valor

        string origen = null;

        //Se pone en pantalla las resservasdas
        Label label = new Label()
        {
            Text = "Reservadas: "
        };



        for (int i = 0; i < _reservadas.Count; i++)
        {
            label.Text += _reservadas[i] + " ";
        }
        VerticalLayout.Children.Add(label);


        //Se crean los image button de cada elemento de la grilla
        int auxID = 1;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 9; j++)
            {

                if (!(j == 4 || (j == 2 && i == 0) || (j == 3 && i == 0)))
                {
                    if (_reservadas.Contains(auxID))
                    {
                        origen = sourceR;
                        disponible = false;
                    }
                    else
                    {
                        origen = sourceG;
                        disponible = true;
                    }
                    ImageButton boton = new ImageButton
                    {
                        ClassId = auxID.ToString(),
                        Source = origen,
                        Aspect = Aspect.AspectFit,
                        IsEnabled = disponible
                    };
                    boton.Clicked += OnClick;
                    Grid.SetRow(boton, i);
                    Grid.SetColumn(boton, j);
                    computadoras.Children.Add(boton);
                    auxID++;
                }

            }
        }

        //Content = VerticalLayout;


    }

    public async void ReservarModulo(object sender, EventArgs e)
    {

        IReservacionModulo reservarModulo = new ModuloReservacionServicio();

        bool reservacionExitosa = await reservarModulo.ReservarModulo(_usuario.boleta, _modulo, _fecha, _laboratorio.idLaboratorio);

        Label label = new Label()
        {
            Text = ""
        };
        if (reservacionExitosa)
        {
            string informacion = "Reservacion hecha con exito: \n" +
                "Laboratorio: " + _laboratorio.idLaboratorio + "\n" +
                "Modulo: " + _modulo + "\n" +
                "Fecha: " + _fecha + "\n" +
                "Hora: " + CalcularHorarioModulo(_modulo) + "\n" +
                "Usuario: " + _usuario.boleta + "\n";
            label.Text = "";
            label.Text += informacion;
            label.Margin = new Thickness(20);
        }
        VerticalLayout.Children.Add(label);

        Content = VerticalLayout;

    }

    public async void CargarComputadoras()
    {
        IObtenerModulo obtenerModulo = new ObtenerModuloServicio();

        List<Computadora> computadoras = await obtenerModulo.ObtenerModulo(_modulo, _laboratorio.idLaboratorio, _fecha);

        if (computadoras != null)
        {
            //Se crea una lista de IDs "ya reservados"

            for (int i = 0; i < computadoras.Count; i++)
            {
                int x = computadoras[i].idComputadora;
                _reservadas.Add(x);


            }
            labelModulo.Text += "    Hay " + _reservadas.Count + " computadoras reservadas";


        }


        if (_reservadas.Count == 0)
        {
            ReservarTodo.Clicked += ReservarModulo;
        }
        else
        {
            ReservarTodo.Clicked += ReservarTodo_Clicked;
        }

        if (_reservadas.Count <= 30)
        {
            btnReservarSeleccionadas.Clicked += ReservarSeleccionadas;
        }



        CrearComputadoras();
        //Content = VerticalLayout;
    }


    public void OnClick(object sender, EventArgs e)
    {
        ImageButton boton = (ImageButton)sender;

        string origen = boton.Source.ToString();

        if (origen.Contains("pc_green.png"))
        {
            boton.Source = "pc_yellow.png";
            _computadorasSeleccionadas.Add(int.Parse(boton.ClassId));
        }
        else
        {
            boton.Source = "pc_green.png";
            _computadorasSeleccionadas.Remove(int.Parse(boton.ClassId));
        }

    }

    public async void ReservarSeleccionadas(object sender, EventArgs e)
    {

        if (_computadorasSeleccionadas.Count == 0)
        {
            await DisplayAlert("Error en la reservar", "Por favor asegurate de haber seleccionado al menos una computadora", "Entendido!");
            return;
        }

        IReservarComputadoras reservarComputadora = new ComputadorasReservacionServicio();
        bool reservacionExitosa = await reservarComputadora.ReservarComputadora(_computadorasSeleccionadas, _fecha, _usuario, _modulo, _laboratorio.idLaboratorio);


        Label label = new Label()
        {
            Text = ""
        };
        if (reservacionExitosa)
        {
            string informacion = "Reservacion hecha con exito: \n" +
                "Laboratorio: " + _laboratorio.idLaboratorio + "\n" +
                "Modulo: " + _modulo + "\n" +
                "Fecha: " + _fecha + "\n" +
                "Hora: " + CalcularHorarioModulo(_modulo) + "\n" +
                "Usuario: " + _usuario.boleta + "\n";
            label.Text = "";
            label.Text += informacion;
            label.Margin = new Thickness(20);
        }
        VerticalLayout.Children.Add(label);

        Content = VerticalLayout;

    }

    private async void ReservarTodo_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Reservación fallida", "El laboratorio no está completamente disponible por lo que no es posible reservarlo todo", "Entendido!");
    }

    private async void btnReservarSeleccionadas_ClickedAsync(object sender, EventArgs e)
    {
        await DisplayAlert("Reservación fallida", "No hay computadoras disponibles para reservar en este horario", "Entendido!");
    }
}