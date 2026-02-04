var gerenciador = new GerenciadorDeVisitantes();
bool executando = true;

while (executando)
{
    Console.WriteLine("\n--- Coworking Check-in ---");
    Console.WriteLine("1. Cadastrar Visitante\n2. Listar Todos\n3. Buscar Nome\n4. Ver Apenas Novatos\n5. Listar Ordenado por ID\n0. Sair");
    
    try 
    {
        string opcao = Console.ReadLine();
        switch (opcao)
        {
            case "1":
                Console.Write("Nome: "); string nome = Console.ReadLine();
                Console.Write("Documento: "); string doc = Console.ReadLine();
                Console.Write("Primeira vez? (s/n): "); bool primeira = Console.ReadLine().ToLower() == "s";
                
                gerenciador.Adicionar(new Visitante(nome, doc, primeira));
                Console.WriteLine("Cadastrado com sucesso!");
                break;

            case "2":
                gerenciador.ListarTodos().ForEach(v => Console.WriteLine(v));
                break;

            case "3":
                Console.Write("Digite o nome: ");
                var v = gerenciador.BuscarPorNome(Console.ReadLine());
                Console.WriteLine(v != null ? v : "Não encontrado.");
                break;

            case "4":
                 Console.WriteLine("\n--- 🆕 Visitantes: Primeira Vez ---");
                var novatos = gerenciador.FiltrarPrimeiraVez();
                if (novatos.Count == 0)
                {
                    Console.WriteLine("Nenhum novo visitante registrado no momento.");
                }
                else
                {
                novatos.ForEach(v => Console.WriteLine(v));
                }
                break;

            case "": 
                var ordenados = gerenciador.ListarOrdenadoPorId();
                ExibirLista(ordenados, "Ordenado por ID");
                break;
            case "0":
                executando = false;
                break;

            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nERRO: {ex.Message}");
    }

    void ExibirLista(List<Visitante> lista, string titulo)
    {
    Console.WriteLine($"\n--- {titulo} ---");
    if (lista.Count == 0) {
        Console.WriteLine("Nenhum registro encontrado.");
        return;
    }
    lista.ForEach(v => Console.WriteLine(v));
}
}