// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Runtime.CompilerServices;
using LagrangeApp;


Console.OutputEncoding = System.Text.Encoding.UTF8;
ConsoleHelper.WriteLine("Приклад розрахунку значення функції з використанням формули Ньютона");
//задаємо кількість замірів
var samples = 10;
//визначимо масиви для збереження даних X та Y
double[] arrX = new double[samples];
double[] arrY = new double[samples];

//задамо початкове значення Х
double x =-10;
//задамо крок заміру
int step = 2;
//для прикладу замовнимо наші дані значеннями тестової функції
//в реальних ситуаціях фіксуються значення з реальних замірів, 
//їх можна задавати наприклад з консолі 
for(var i =0;i<samples;i++){
    //значення У з замірів
    var  y = f(x);
    arrX[i] = x;
    arrY[i] = y;
    //наступне значення Х
    x +=step;
}
//роздрукуємо наші заміри
PrintData(arrX,arrY);
//розрахуємо значення функції  в новій точці з допомогою інтерполяції
var newX = -5;
Console.WriteLine("Розрахуємо значення функції для Х="+newX);

//основний алгоритм розрахунку


//визначими двовимірний динамічний масив для збереження скінченної різниці у виавдку рівномірних замірів або  розділеної різниці у випадку не рівномірних замірів
//збереження проміжних розрахунків сильно пришвидшує алгоритм розрахунку
double[][] differences = new double[samples][];
double[][] divDifferences = new double[samples][];
differences[0] = arrY;
divDifferences[0] = arrY;
//розрахуємо нове прогнозоване значення 
var result = CalcValue(newX);
//справжнє значення  для порівняння
var expectedValue = f(newX);
//виводимо результат обчислень на екран консолі
Console.WriteLine($"Очікуване значення У = {expectedValue}; Розраховане значення У = {result}");

//функція що розраховує многочлен ньютона для нерівномірних замірів
double CalcValue(double x){
    //https://www.mathros.net.ua/persha-interpoljacijna-formula-njutona-dlja-rivnoviddalenyh-vuzliv-interpoljacii.html
    double result = 0;
    //для всіх одиниць заміру формуємо елементи многочлена
    for(var rank = 0;rank<arrX.Length; rank++){
        //перший член завжди перший замір
        if(rank ==0) {
            result = arrY[0];
            continue;
            }
        //отримуємо розділені різниці н-го порядку
        var divDiff = GetDividedDifferences(rank);
        //коефіцієнт це завжди перша розділена різниця н-го порядку
        var c = divDiff[0];
        //добуток на 0 завжди рівний 0, тому пропускаємо цей крок
        if (c == 0) continue;
        //до обраховуєм н-й член многочлена і додаємо його до результату
        result +=  c*Xn(rank,x);
    }
    //прогнозоване значення функції
    return result;
}
//добуток комбінації різниць прогнозованого Х і всіх Х наших замірів
double Xn(int rank, double x){
    double result = 1;
    for(var i = rank; i>0;i--){
        result = result *(x- arrX[i-1]);
    }
    return result;
}
//розраховує розділені різниці вказаного порядку
double[] GetDividedDifferences(int rank){
    //пробуєм дістати розділені різниці розраховані раніше
    var rankDivDiff =  divDifferences[rank];
    if (rankDivDiff  == null){
        //визначаємо масив розділених різниць
        rankDivDiff = new double[differences[0].Length-rank];
        //отримуємо розділені різниці попереднього порядку
        var prevDiff = GetDividedDifferences(rank-1);
        for( var i =0; i<rankDivDiff.Length; i++){
            //розраховуємо розділену різницю
            rankDivDiff[i] =(prevDiff[i+1]- prevDiff[i])/(arrX[i+rank]-arrX[i]);
        }
        divDifferences[rank] = rankDivDiff;
    }
    return rankDivDiff;
}
//розраховує скінчені різниці вказаного порядку
double[] GetDifferences(int rank){
    //пробуєм дістати скінченні різниці розраховані раніше
    double[] rankDiff = differences[rank];

    //якщо розрахованих значень немає то розраховуємо
    if ( rankDiff==null){
        //визначаємо масив скінчених різниць
        rankDiff = new double[differences[0].Length-rank];
        //дістаємо скінченні різничі попереднього порядку
        var prevDifferences = GetDifferences(rank-1);
        
        for( var i =0; i<rankDiff.Length; i++){
            //розраховуємо скінченну різницю
            rankDiff[i] = prevDifferences[i+1] - prevDifferences[i];
        }
        //зберігаємо для наступного використання
        differences[rank] = rankDiff;
    }
    return rankDiff;
}

//роздруковує дані замірів
void PrintData(double[] arrX, double[] arrY){
    for(var i =0;i<arrX.Length;i++){
        Console.WriteLine($"X{ConsoleHelper.AsSubscript(i)} ={arrX[i]}; Y{ConsoleHelper.AsSubscript(i)} ={arrY[i]};");
    }
}
// метод з функцією для генерації тестових даних 
double f(double x){
    var y =-3*x*x*x+8*x*x-4;
    return y;
}
