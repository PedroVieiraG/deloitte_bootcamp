class Mina
{
    Minerio minerio = new Minerio();
    private string codigo;      
    private string nome;
    private decimal capacidade;
   

    public Minerio getMinerio()
    {
        return this.minerio;
    }

    public void setMinerio()
    {
    }

      public string getpCodigo()
    {
        return this.codigo;
    }

     public void setpCodigo()
    {
    }

    public string getNome()
    {
        return this.nome;
    }

     public void setNome()
    {
    }

    public decimal getCapacidade()
    {
        return this.capacidade;
    }
     public void setCapacidade()
    {
    }

    public Minerio acessarExtrairMinerio(bool IsGestorMina)
    {
        if (IsGestorMina)
        {
            return this.extrairMinerio();    
        }else
        {
            minerio.codigo ="1";
            minerio.tipo = "0";
            return minerio;            
        }
        
    }
    public Minerio extrairMinerio()
    {
        minerio.codigo ="1";
        minerio.tipo = "Ouro";
        return minerio; 
    }
}


