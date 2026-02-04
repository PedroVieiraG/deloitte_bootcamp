public class Lampada
{
    private bool isLigada;

    public Lampada()
    {
        isLigada = false; 
    }

    public void Ligar()
    {
        isLigada = true;
        Console.WriteLine("Ligou!");
    }
    public void Desligar()
    {
        isLigada = false;
        Console.WriteLine("Desligou.");
    }
    public bool EstaLigada()
    {
        return isLigada;
    }
}