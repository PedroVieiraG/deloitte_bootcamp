public class ContaBancaria
{
    public string NomeCliente {get; set;}
    public string NumConta {get; set;}
    public decimal Saldo {get; set;}

    public ContaBancaria(string nomeCliente, string numConta, decimal saldoInicial)
    {
        this.NomeCliente = nomeCliente;
        this.NumConta = numConta;
        this.Saldo = saldoInicial;
    }
    
    public virtual void Sacar(decimal valor)
        {
            if(valor > Saldo){throw new ArgumentException("Saldo insuficiente para saque.");}
            if(valor <= 0){throw new ArgumentException("O valor do saque deve ser positivo.");}
            else
            {
                Saldo -= valor;
                Console.WriteLine($"Saque de {valor:C} realizado com sucesso.");
            }
        }
    public void Depositar(decimal valor)
    {
     if(valor <= 0){throw new ArgumentException("O valor do depósito deve ser positivo.");}
     else
     {
        Saldo += valor;
     }
    }

    public override string ToString()
        {
            return $"Cliente: {NomeCliente} | Conta: {NumConta} | Saldo: {Saldo:C}";
        }
}

public class ContaPoupanca : ContaBancaria
{
     public int DiaRendimento {get; set;}

     public ContaPoupanca(string nomeCliente, string numConta, decimal saldoInicial, int diaRendimento) 
        : base(nomeCliente, numConta, saldoInicial)
     {
        this.DiaRendimento = diaRendimento;
     }

     public void CalcularNovoSaldo(decimal taxaRendimento)
    {
        Saldo += Saldo * taxaRendimento;
    }
}

public class ContaEspecial : ContaBancaria
{
    public decimal Limite {get; set;}
    public ContaEspecial(string nomeCliente, string numConta, decimal saldoInicial, decimal limite)
        :base(nomeCliente, numConta, saldoInicial)
    {
        this.Limite = limite;
    }
public override void Sacar(decimal valor)
    {
        if (valor <= (Saldo + Limite))
        {
            Saldo -= valor;
            Console.WriteLine($"Saque de {valor:C} realizado (usando limite se necessário).");
        }
        else
        {
            Console.WriteLine("Limite excedido!");
        }
    }
}