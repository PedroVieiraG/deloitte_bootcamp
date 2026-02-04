using System;

ContaPoupanca poupanca = new ContaPoupanca("João Silva", "a1001", 1000.00m, 15);
ContaEspecial especial = new ContaEspecial("Maria Oliveira", "2002", 500.00m, 1000.00m);


Console.WriteLine("--- Testando Saques ---");
poupanca.Sacar(1200.00m); 
especial.Sacar(1200.00m); 


poupanca.Depositar(500.00m);
especial.Depositar(200.00m);


Console.WriteLine("\n--- Aplicando Rendimento na Poupança ---");
poupanca.CalcularNovoSaldo(0.10m); // 10% de rendimento


Console.WriteLine("\n--- Dados Atualizados ---");
Console.WriteLine(poupanca.ToString() + $" | Dia Rendimento: {poupanca.DiaRendimento}");
Console.WriteLine(especial.ToString() + $" | Limite: {especial.Limite:C}");