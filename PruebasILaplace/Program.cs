using System.Text.RegularExpressions;

Console.OutputEncoding = System.Text.Encoding.UTF8;
string L = "e^(-st)";
string coeficiente = "-(1/s)";
string iL = coeficiente + L;

Console.WriteLine("ingresa la función");
string funcion = Console.ReadLine() ?? "";
string[] partes = funcion.Split(' ');

string sMultiplicacion = "";

if (partes.Length == 1)
{
    string parte = partes[0];
    sMultiplicacion = $"{L}({parte})";
    Console.WriteLine($"∫[0,b]{sMultiplicacion} dt");

    int constante = 0;
    //evaluamos si la parte es una constante
    if (int.TryParse(parte, out constante))
    {
        if (constante == 1)
        {
            Console.WriteLine($"∫[0,b]{L}");
        }
        else
        {
            Console.WriteLine($"∫[0,b]{parte}{L}");
            Console.WriteLine("Sacamos la constante de la integral");
            Console.WriteLine($"{parte}[∫[0,∞]{L}]");
        }

        //integramos
        coeficiente = $"-({constante}/s)";
        iL = coeficiente + L;
        Console.WriteLine("Integramos");
        Console.WriteLine($"∫[0,b]{L} = {iL}");

        //Transcribimos a integral impropia
        Console.WriteLine("Transcribimos a integral impropia");
        Console.WriteLine($"lim b->∞ [{iL}][0,b]");

        //Evaluamos limites
        Console.WriteLine("Evaluamos limites");
        string evaluacionb = coeficiente + L.Replace("t", "b");
        string evaluacion0 = coeficiente + L.Replace("t", "0");
        Console.WriteLine($"lim b->∞ = [{evaluacionb}] - ({evaluacion0})");

        Console.WriteLine("Si s > 0, e^(-sb) -> 0 cuando b->∞");
        string valorSuperior = "0";
        Console.WriteLine("e^(-s·0) = e^0 = 1");
        string valorInferior = coeficiente;

        string resultado;
        if (valorInferior.StartsWith("-"))
            resultado = valorInferior.Substring(1);
        else
            resultado = "-" + valorInferior;

        Console.WriteLine($"{valorSuperior} - ({valorInferior}) = {resultado}");
        Console.WriteLine($"L{{{funcion}}} = {resultado}, s > 0");
    }
    else
    {
        
        Match m = Regex.Match(parte, @"^(-?\d*)t(\^(\d+))?$");
        if (!m.Success)
        {
            Console.WriteLine("No se reconoce la funcion");
        }
        else
        {
            int c = 1;
            if (m.Groups[1].Value == "-") c = -1;
            else if (m.Groups[1].Value != "") c = int.Parse(m.Groups[1].Value);
            int n = m.Groups[3].Success ? int.Parse(m.Groups[3].Value) : 1;
            string poli = n == 1 ? "t" : $"t^{n}";

            if (c == 1)
            {
                Console.WriteLine($"∫[0,∞]{poli}{L}");
            }
            else
            {
                Console.WriteLine($"∫[0,∞]{c}{poli}{L}");
                Console.WriteLine("Sacamos la constante de la integral");
                Console.WriteLine($"{c}[∫[0,∞]{poli}{L}]");
            }

            //integramos por partes 
            Console.WriteLine($"Integramos por partes : u = {poli}, dv = {L} dt");
            

            List<string> sumandos = new List<string>();
            for (int j = 0; j <= n; j++)
            {
                long coef = Factorial(n) / Factorial(n - j);
                int pot = n - j;
                string tp = pot == 0 ? "" : pot == 1 ? "t" : $"t^{pot}";
                string den = $"s^{j + 1}".Replace("s^1", "s");
                string signoInt = j % 2 == 0 ? "-" : "+";
                string signo = j % 2 == 0 ? "+" : "-";
                string numero = (coef == 1 && tp != "") ? "" : coef.ToString();

                Console.WriteLine($"{numero}{tp}{(numero + tp == "" ? "1" : "")} | {signoInt}{L}/{den} | {signo}");
                // signo alternado * signo de la integral = siempre negativo
                sumandos.Add($"- {numero}{tp}/{den}");
            }
            iL = $"{L}[ {string.Join(" ", sumandos)} ]";
            Console.WriteLine($"∫[0,∞]{poli}{L} = {iL}");

            //Transcribimos a integral impropia
            Console.WriteLine("Transcribimos a integral impropia");
            Console.WriteLine($"lim b->∞ [{iL}][0,b]");

            //Evaluamos limites
            long fact = Factorial(n);
            string denFinal = $"s^{n + 1}";
            Console.WriteLine("Evaluamos limites");
            Console.WriteLine($"Limite superior: si s > 0, {L.Replace("t", "b")} -> 0 cuando b->∞");
            Console.WriteLine($"Limite inferior: {L.Replace("t", "0")} = 1 y todo termino con t vale 0");
            Console.WriteLine($"F(0) = -{fact}/{denFinal}");
            Console.WriteLine($"0 - (-{fact}/{denFinal}) = {fact}/{denFinal}");
            Console.WriteLine($"L{{{funcion}}} = {c * fact}/{denFinal}, s > 0");
        }
    }
}
else
{
    for (int i = 0; i < partes.Length; i++)
    {
        string parte = partes[i];
        if (parte != "+")
        {
            sMultiplicacion += $"{L}({parte})";
        }
        else
        {
            sMultiplicacion += $" {parte} ";
        }
    }
    Console.WriteLine($"∫[0,∞]{sMultiplicacion} dt");
}

Console.ReadKey();

static long Factorial(int n)
{
    long r = 1;
    for (int i = 2; i <= n; i++) r *= i;
    return r;
}