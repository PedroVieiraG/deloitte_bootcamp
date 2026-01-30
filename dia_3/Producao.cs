class Producao
{
    string ano;
    decimal quantidade;
    DateTime date;
    decimal Volume;

    public int refinarMineiro( Minerio pMinerio, Refinamento refinamento)
    {
        switch (refinamento)
        {
            case Refinamento.Granularidade:
            return 0;
        }

        return this.quantidadeFinalRefinamento(pMinerio);

    }

    private int quantidadeFinalRefinamento(Minerio pMinerio)
    {
        return 1;
    }
}


