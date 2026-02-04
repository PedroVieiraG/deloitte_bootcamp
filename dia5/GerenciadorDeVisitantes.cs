using System;
using System.Collections.Generic;
using System.Linq;

public class GerenciadorDeVisitantes
{
    private readonly List<Visitante> _visitantes = new();

    public void Adicionar(Visitante visitante) => _visitantes.Add(visitante);

    public List<Visitante> ListarTodos() => _visitantes.OrderBy(v => v.Nome).ToList();

    public List<Visitante> FiltrarPrimeiraVez() => _visitantes.Where(v => v.IsPrimeiraVez).ToList();

    public Visitante? BuscarPorNome(string nome) => 
        _visitantes.FirstOrDefault(v => v.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

    public List<Visitante> ListarOrdenadoPorId() 
    {
        return _visitantes.OrderBy(v => v.Id).ToList();
    }
}
