# ViaCEP API

API para consulta de endereços brasileiros a partir do CEP, implementada em múltiplas linguagens.

## Estrutura

| Pasta | Linguagem | Framework | Status |
|-------|-----------|-----------|--------|
| [csharp/](csharp/) | C# | ASP.NET 6 | ✅ Implementado |
| [java/](java/) | Java | Spring Boot | 🔜 Em breve |
| [python/](python/) | Python | FastAPI | 🔜 Em breve |
| [nodejs/](nodejs/) | Node.js | Express | 🔜 Em breve |
| [php/](php/) | PHP | Laravel | 🔜 Em breve |
| [typescript/](typescript/) | TypeScript | NestJS | 🔜 Em breve |

## Exemplo de resposta

```
GET /api/cep/01310100
```

```json
{
  "cep": "01310-100",
  "logradouro": "Avenida Paulista",
  "complemento": "de 610 a 610 - lado par",
  "bairro": "Bela Vista",
  "cidade": "São Paulo",
  "estado": "SP",
  "ibge": "3550308",
  "ddd": "11"
}
```

Cada implementação segue a mesma interface de API. Consulte o README de cada pasta para instruções específicas.

## Licença

[MIT](LICENSE)
