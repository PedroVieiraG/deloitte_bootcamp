public class Visitante
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string Documento { get; set; }
    public DateTime HorarioChegada { get; set; }
    public bool IsPrimeiraVez { get; set; }

    public Visitante(string nome, string documento, bool isPrimeiraVez)
    {
        Nome = nome;
        Documento = documento;
        IsPrimeiraVez = isPrimeiraVez;
        HorarioChegada = DateTime.Now;
    }

    public override string ToString()
    {
        return $"[ID: {Id.ToString().Substring(0, 5)}] {Nome} - Chegada: {HorarioChegada:HH:mm} | {(IsPrimeiraVez ? "Novato" : "Recorrente")}";
    }
}