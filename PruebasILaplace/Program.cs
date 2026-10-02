//este codigo es MEJOR¿¿¿ en veses
using System.Text;

Console.OutputEncoding = Encoding.UTF8; //Es para que el simbolo de infinito y integral se vean bien.

TC("TRANSFORMACIONES DE LAPLACE\n", ConsoleColor.Red);
TC("Formula a utilizar: ∫[0,∞] e^(-st)f(t)dt\n", ConsoleColor.Blue); //Introducción. uso \n para salto de linea.

string fnc; //"funcion"
string L = "e^(-st)";
string intL = "-1/s(e^(-st))";
string mult; //Multiplicacion
string evl1; //Evaluacion de infinito
string evl2; //Evaluacion de cero
string nr; //Numero Remplazado
int caso3 = 0; //Reglas de convergencia s < 0 = 0
string spf; //Simplificación
int eC = 1; //Evaluacion de e^(-s*0)
string resultado; //Resultado final
//Definimos todas las funciones que vamos a utilizar. Por el momento estas.
do
{
    Console.WriteLine("Ingrese funcion f(t): ");
    fnc = Console.ReadLine(); //Lee la funcion que el usuario ingresa y la guarda en la variable fnc.
}
while (string.IsNullOrWhiteSpace(fnc)); //No permitir que el usuario ingrese una funcion vacia. Pude especificar mas pero no quise.

if (int.TryParse(fnc, out int numero))  //Si el usuario ingresa un numero, lo guarda en la variable numero y entra al if. 
{
    TC($"Funcion ingresada: f({numero})", ConsoleColor.Yellow);
    TC("Sustitucion...\n", ConsoleColor.DarkYellow);
    if (numero == 1) //Caso especial , si el usuario ingresa 1, no se multiplica por 1, ya que es innecesario.
    {
        mult = L; //En este caso la multiplicacion es solo L, ya que 1*L = L.

        TC($"∫[0,∞] ({L})({numero})dt\n", ConsoleColor.Gray); // "$" es para que se pueda escribir la variable dentro del string.
        TC("Multiplicacion...\n", ConsoleColor.DarkYellow);
        TC($"∫[0,∞] {mult}\n", ConsoleColor.DarkGreen);
        TC("Integrando...\n", ConsoleColor.DarkYellow);
        TC($"{intL}\n", ConsoleColor.DarkGreen); //Ya habiamos definido la integral de L, por lo que solo la mostramos.
        TC("Reescribir la integral impropia con un limite...\n", ConsoleColor.DarkYellow);
        TC($"lim b->∞ : [{intL}][0,b]\n", ConsoleColor.DarkGreen);
        TC("Evaluacion...\n", ConsoleColor.DarkYellow);

        evl1 = intL.Replace("t", "*b"); //Reemplaza la variable t por b para evaluar en el limite.
        evl2 = intL.Replace("t", "*0"); //Reemplaza la variable t por 0 para evaluar en el limite.

        TC($"lim b->∞ : [{evl1}] - [{evl2}]\n", ConsoleColor.DarkGreen); //Escribimos ambos resultados para que el usuario vea lo que se esta haciendo.
        TC("Realizando...\n", ConsoleColor.DarkYellow);
        if (evl1.Contains("e^(-s*b)"))
        {
            TC($"lim b->∞ : [{caso3}] - [{evl2}]\n", ConsoleColor.DarkGreen); //if para que si el resultado de la evaluacion en el limite es 0,
                                                                              //se muestre 0 en lugar de e^(-s*b) que es lo que aparece al evaluar.
        }
        TC("Realizando...\n", ConsoleColor.DarkYellow);

        evl2 = evl2.Replace("e^(-s*0)", $"{eC}"); //sabemos que e^(-s*0) = 1, por lo que reemplazamos el resultado de la evaluacion en 0 por 1.
        spf = evl2.Remove(0, 1); //Eliminamos el primer caracter de la variable que es negativo. al hacerse el procedimiento es (-)(-) que es (+)

        TC($" - [{evl2}] = {spf}\n", ConsoleColor.DarkGreen);
        TC("Realizando...\n", ConsoleColor.DarkYellow);
        resultado = spf.Replace("(1)", ""); //Sabemos que se mutiplicara por 1, asi que lo quitamos
        TC($"{resultado}\n", ConsoleColor.DarkGreen);
        TC("RESULTADO FINAL\n", ConsoleColor.Green);
        TC($"L({numero}) = {resultado}\n", ConsoleColor.Red); //Imprimimos el resultado en color llamativo

    }
    else //Si el usuario ingresa un numero diferente a 1, se multiplica por L y se realiza el procedimiento normal.
    {
        mult = $"{numero}{L}"; //Multiplicacion

        TC($"∫[0,∞] ({L})({numero})dt\n", ConsoleColor.Gray);
        TC("Multiplicacion...\n", ConsoleColor.DarkYellow);
        TC($"∫[0,∞] {mult}", ConsoleColor.DarkGreen); //Hacemos los mismos pasos que en el caso de 1. pero aqui tenemos una constante que sacar
        TC("Sacando la constante de la integral...\n", ConsoleColor.DarkYellow);
        TC($"{numero}∫[0,∞]{L}dt\n", ConsoleColor.DarkGreen); //Ponemos la constante fuera de la integral de esa manera
        TC("Integrando...\n", ConsoleColor.DarkYellow);
        TC($"({numero})({intL})\n", ConsoleColor.DarkGreen); //De nuevo queda la integral de L, pero ahora multiplicada por la constante que sacamos.

        nr = intL.Replace("1", $"{numero}"); //Reemplazamos el 1 por la constante que sacamos de la integral. Es lo mismo

        TC("Simplificando...\n", ConsoleColor.DarkYellow);
        TC($"{nr}\n", ConsoleColor.DarkGreen); //Se escribe el numero remplazado
        TC("Reescribir la integral impropia con un limite...\n", ConsoleColor.DarkYellow);
        TC($"lim b->∞ : [{nr}][0,b]\n", ConsoleColor.DarkGreen);
        TC("Evaluacion...\n", ConsoleColor.DarkYellow);

        evl1 = nr.Replace("t", "*b");
        evl2 = nr.Replace("t", "*0");

        TC($"lim b->∞ : [{evl1}] - [{evl2}]\n", ConsoleColor.DarkGreen);
        TC("Realizando...\n", ConsoleColor.DarkYellow);
        if (evl1.Contains("e^(-s*b)")) ;
        {
            TC($"lim b->∞ : [{caso3}] - [{evl2}]\n", ConsoleColor.DarkGreen);
        }
        TC("Realizando...\n", ConsoleColor.DarkYellow);

        evl2 = evl2.Replace("e^(-s*0)", $"{eC}");
        spf = evl2.Remove(0, 1);

        TC($" - [{evl2}] = {spf}\n", ConsoleColor.DarkGreen);
        TC("Realizando...\n", ConsoleColor.DarkYellow);
        resultado = spf.Replace("(1)", "");
        TC($"{resultado}\n", ConsoleColor.DarkGreen);
        TC("RESULTADO FINAL\n", ConsoleColor.Green);
        TC($"L({numero}) = {resultado}\n", ConsoleColor.Red); //El mismo procedimiento de arriba, solo que el lugar de numero nr en algunas partes


    }
}
else //Si el usuario ingresa una constante letra
{
    if (fnc == "t") //Caso especial donde el usuario ingresa t, ya que la integral de t es diferente a la integral de una constante.
    {
        for (int i = 0; i < 100; i++)
        {
            TC("CONTINUARA... NO ESTAS PREPARADO", ConsoleColor.DarkRed); //Aun no eh hecho eso xd
        }
        Environment.Exit(0); //Termina el programa y no ejecuta nada mas despues del if. Esto es para que no se ejecute el resto del codigo que es para constantes.
    }
    TC($"Funcion ingresada: f({fnc})\n", ConsoleColor.Yellow);
    TC("Sustitucion...\n", ConsoleColor.DarkYellow);

    mult = $"{fnc}{L}";

    TC($"∫[0,∞] ({L})({fnc})dt\n", ConsoleColor.Gray);
    TC($"Multiplicacion...\n", ConsoleColor.DarkYellow);
    TC($"∫[0,∞] {mult}\n", ConsoleColor.DarkGreen);
    TC("Sacando la constante de la integral...\n", ConsoleColor.DarkYellow);
    TC($"{fnc}∫[0,∞]{L}dt\n", ConsoleColor.DarkGreen);
    TC("Integrando...\n", ConsoleColor.DarkYellow);
    TC($"({fnc})({intL})\n", ConsoleColor.DarkGreen);

    nr = intL.Replace("1", $"{fnc}");

    TC("Simplificando...\n", ConsoleColor.DarkYellow);
    TC($"{nr}\n", ConsoleColor.DarkGreen);
    TC("Reescribir la integral impropia con un limite...\n", ConsoleColor.DarkYellow);
    TC($"lim b->∞ : [{nr}][0,b]\n", ConsoleColor.DarkGreen);
    TC("Evaluacion...\n", ConsoleColor.DarkYellow);

    evl1 = nr.Replace("t", "*b");
    evl2 = nr.Replace("t", "*0");

    TC($"lim b->∞ : [{evl1}] - [{evl2}]\n", ConsoleColor.DarkGreen);
    TC("Realizando...\n", ConsoleColor.DarkYellow);
    if (evl1.Contains("e^(-s*b)"))
    {
        TC($"lim b->∞ : [{caso3}] - [{evl2}]\n", ConsoleColor.DarkGreen);
    }
    TC("Realizando...\n", ConsoleColor.DarkYellow);

    evl2 = evl2.Replace("e^(-s*0)", $"{eC}");
    spf = evl2.Remove(0, 1);

    TC($" - [{evl2}] = {spf}\n", ConsoleColor.DarkGreen);
    TC("Realizando...\n", ConsoleColor.DarkYellow);
    resultado = spf.Replace("(1)", "");
    TC($"{resultado}\n", ConsoleColor.DarkGreen);
    TC("RESULTADO FINAL\n", ConsoleColor.Green);
    TC($"L({fnc}) = {resultado}\n", ConsoleColor.Red); //El mismo procedimiento de arriba, la variable ahora es fnc en lugar de numero o nr,
                                                       //ya que el usuario ingreso una constante letra y no un numero.



}


static void TC(string texto, ConsoleColor color) //Cree una funcion para no estar escribiendo Console.WriteLine
                                                 //y Console.ForegroundColor cada vez que quiero cambiar el color del texto.
{
    Console.ForegroundColor = color;
    Console.WriteLine(texto);
    Console.ResetColor();
}