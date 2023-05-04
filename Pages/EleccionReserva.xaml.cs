using Microsoft.Maui.Controls;
using SCLC.Models;
using SCLC.Services;
using System.Linq;

namespace SCLC.Pages;

public partial class EleccionReserva : ContentPage
{
    VerticalStackLayout VerticalLayout = new VerticalStackLayout();

    HorizontalStackLayout HorizontalLayout = new HorizontalStackLayout();

    //Se crean elementos de la grilla de 9*4
    Grid computadoras = new Grid();

    DatePicker datePicker = new DatePicker();

    public List<int> _reservadas ;

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
        _reservadas = new List<int>();

        string cadenaFecha = fecha.ToString("dd/MM/yyyy");


        Console.WriteLine(cadenaFecha);




        Label mod = new Label
        {
            Text = "Modulo: " + modulo,
            FontSize = 20
        };

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Button ReservarTodo = new Button
        { 
            Text = "Reservar todo",
            FontSize = 10,
            BackgroundColor = Color.FromHex("#82014A"),
            CornerRadius = 10,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

        ReservarTodo.Clicked += ReservarModulo;

        datePicker.Date = fecha;


        HorizontalLayout.Add(datePicker);
        HorizontalLayout.Add(mod);
        HorizontalLayout.Add(ReservarTodo);
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
        crearComputadoras();
        
        //VerticalLayout.Add(computadoras);
        //Se actualiza el contenido de la pagina
        
       
        Content = VerticalLayout;


      
        

        InitializeComponent();

        Grid.SetColumn(computadoras, 1);
        Grid.SetRow(computadoras, 1);
        contenedor.Children.Add(computadoras);
    }
    public void crearComputadoras()
    {
 
        


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
                    if ( estaEnReservadas(auxID))
                    {
                        origen = sourceR;
                    }
                    else
                    {
                        origen = sourceG;
                    }
                    ImageButton boton = new ImageButton
                    {
                        ClassId = auxID.ToString(),
                        Source = origen,
                        Aspect = Aspect.AspectFit,
                    };
                    //boton.Clicked += Boton_Clicked;
                    Grid.SetRow(boton, i);
                    Grid.SetColumn(boton, j);
                    computadoras.Children.Add(boton);
                    auxID++;
                }

            }
        }

        Content = VerticalLayout;

        
    }

    public async void ReservarModulo(object sender, EventArgs e)
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

    public async void CargarComputadoras()
    {
        IObtenerModulo obtenerModulo = new ObtenerModuloServicio();

        List<Computadora> computadoras = await obtenerModulo.ObtenerModulo(_modulo, _laboratorio.idLaboratorio, _fecha);
        Label label = new Label()
        {
            Text = "No cargaron"
        };

        if (computadoras != null)
        {
            //Se crea una lista de IDs "ya reservados"
        
            for(int i = 0; i < computadoras.Count; i++)
            {
                _reservadas.Add(computadoras[i].idComputadora);
          
            }

            label.Text = "Cargaron " + _reservadas.Count;

        }

        VerticalLayout.Children.Add(label);
        Content = VerticalLayout;
    }

    public bool estaEnReservadas(int i)
    {
        int j = this._reservadas.Count;
        for(int h = 0; h < j; h++)
        {
            if (_reservadas[h] == i)
            {
                return true;
            }
        }
        return false;
    }
    /*
    public async void ReservarComputadora(object sender, EventArgs e)
    {
        ImageButton boton = (ImageButton)sender;
        string id = boton.ClassId;
        int idComp = int.Parse(id.Substring(4));
        IReservacionComputadora reservarComputadora = new ComputadoraReservacionServicio();
        bool reservacionExitosa = await reservarComputadora.ReservarComputadora(_usuario.boleta, idComp, _fecha, _laboratorio.idLaboratorio);
        Label label = new Label()
        {
            Text = "Mal"
        };
        if (reservacionExitosa)
        {
            label.Text = "Bien";
        }
        VerticalLayout.Children.Add(label);
        Content = VerticalLayout;
    }
    */
}