API para lotes de minerios
---
Esta é uma API desenvolvida em ASP.NET Core para o controle e rastreabilidade de lotes de minério. A aplicação faz um CRUD (Create, Read, Update e Delete), garantindo a integridade dos dados.
---
Regras de Negócio e Validações:

Campos Obrigatórios: CodigoLote, MinaOrigem e LocalizacaoAtual.

Limites de Composição: Teor de Ferro e Umidade devem estar entre 0% e 100%.

Quantidade: Toneladas devem ser maiores que zero.

Status: Aceita apenas valores inteiros 0, 1 ou 2.

Unicidade: Não é permitido duplicar o CodigoLote
---
Tecnologias Utilizadas:

C# / .NET 10

ASP.NET Core Web API

Entity Framework Core (Persistência de dados)

