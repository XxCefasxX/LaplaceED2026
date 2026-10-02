// See https://aka.ms/new-console-template for more information
using static System.Net.Mime.MediaTypeNames;
Console.OutputEncoding = System.Text.Encoding.UTF8;

//Titulo
Console.WriteLine("TRANSFORMACIONES DE LAPLACE\n");
Console.WriteLine("Formula a utilizar: ∫[0,∞] e^(-st)f(t)dt\n");

string L = "e^(-st)";//Por eso se multiplica
string iL = "-(1/s)e^-st";//Integral de L
string funcion;//Datos del usuario
do
{
    Console.WriteLine("ingresa la funcion");
    funcion = Console.ReadLine();
}
while (string.IsNullOrWhiteSpace(funcion)); //No permite funcion vacia
string[] partes = funcion.Split(' ');//Separa la función

string sMultiplicacion = "";

//Se escribe la constante ya sea numero o letra
if (partes.Length == 1)
{
    string parte = partes[0];
    sMultiplicacion = $"{L}({parte})";//Se multiplica la funcion por L
    Console.WriteLine($"∫{sMultiplicacion}");

    int constante = 0;
    //numero o una letra
    if (int.TryParse(parte, out constante) || (parte.Length == 1 && char.IsLetter(parte[0])))
    {
        //1 por L es L
        if (parte == "1")
        {
            Console.WriteLine($"∫[0,∞]{L}");
            //integramos
            Console.WriteLine("Integramos");
            Console.WriteLine($"∫[0,∞]{L} = {iL}");
        }
        else //Si hay otra constante
        {
            Console.WriteLine($"∫[0,∞]{parte}{L}");
            Console.WriteLine("Sacamos la constante de la integral");
            Console.WriteLine($"{parte}∫[0,∞]{L}");
            Console.WriteLine("Integramos");
            iL = iL.Replace("1", parte);//Se cambia a la constante
            Console.WriteLine($"{parte}∫[0,∞]{L} = {iL}");
        }
        //Transcribimos a integral impropia
        Console.WriteLine("Transcribimos a integral impropia");
        Console.WriteLine($"lim b->∞ [{iL}][0,b]");
        //Evaluamos limites
        Console.WriteLine("Evaluamos limites");
        string iLb = iL.Replace("-st", "-s*b"); //Ponemos t = b
        string iL0 = iL.Replace("-st", "-s*0"); //Ponemos t = 0
        Console.WriteLine($"lim b->∞ [{iLb}] - [{iL0}]");//Hacemos la resta
        //Si s es mayor a 0, L se hace 0 y e^(-s*0) es 1
        Console.WriteLine("Como s > 0, e^(-s*b) -> 0 cuando b -> ∞ y e^(-s*0) = 1");
        Console.WriteLine($"[0] - [-({parte}/s)(1)]");

        //Resultado final
        string resultado = parte == "0" ? "0" : $"{parte}/s";
        Console.WriteLine("RESULTADO FINAL");
        Console.WriteLine($"L{{{parte}}} = {resultado}, s > 0");
    }
    else //Mnade msj si no es constante
    {
        Console.WriteLine("Por ahora solo se soportan constantes.");
    }
}
else
{
    //Recorre cada parte
    for (int i = 0; i < partes.Length; i++)
    {
        string parte = partes[i];
        if (parte != "+")
        {
            sMultiplicacion += $"{L}({parte})";//Multiplica cada parte por L
        }
        else
        {
            sMultiplicacion += $" {parte} ";//Deja el + en su lugar
        }
    }
    Console.WriteLine($"∫[0,∞]{sMultiplicacion} dt");//Muestra la integral
}