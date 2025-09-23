using System;


internal class Program
{

static int SumFor(int n)
{

return n * (n + 1) / 2;
}



static int SumIte(int n)
{
int acc = 0;
for (int i = 1; i <= n; i++)
{
acc += i; 
}
return acc;
}

static (int n, int sum) FindLastValidAscending(Func<int, int> sumFn)
{
int lastN = 0;
int lastSum = 0;



for (int n = 1; n > 0; n++) 
{
int s = sumFn(n);
if (s > 0)
{
lastN = n;
lastSum = s;
}
else
{
break; 
}


if (n == int.MaxValue)
break; 
}
return (lastN, lastSum);
}



static (int n, int sum) FindFirstValidDescending(Func<int, int> sumFn)
{
for (int n = int.MaxValue; n >= 1; n--)
{
int s = sumFn(n);
if (s > 0)
{
return (n, s); 
}
}
return (0, 0); 
}


private static void Main(string[] args)
{
// SumFor
var ascFor = FindLastValidAscending(SumFor);
var descFor = FindFirstValidDescending(SumFor);


// SumIte
var ascIte = FindLastValidAscending(SumIte);
var descIte = FindFirstValidDescending(SumIte);


Console.WriteLine("\n• SumFor:");
Console.WriteLine($"\t◦ From 1 to Max → n: {ascFor.n} → sum: {ascFor.sum}");
Console.WriteLine($"\t◦ From Max to 1 → n: {descFor.n} → sum: {descFor.sum}");


Console.WriteLine("\n• SumIte:");
Console.WriteLine($"\t◦ From 1 to Max → n: {ascIte.n} → sum: {ascIte.sum}");
Console.WriteLine($"\t◦ From Max to 1 → n: {descIte.n} → sum: {descIte.sum}");
}
}