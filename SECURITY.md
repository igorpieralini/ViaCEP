# Política de Segurança

## Versões suportadas

| Linguagem  | Versão mínima   | Suportada |
|------------|-----------------|-----------|
| C#         | .NET 6          | ✅        |
| Node.js    | 20 LTS          | ✅        |
| TypeScript | 20 LTS (Node)   | ✅        |
| Python     | 3.12            | ✅        |
| Java       | 17 LTS          | ✅        |
| PHP        | 8.1             | ✅        |

## Reportando vulnerabilidades

Se você encontrar uma vulnerabilidade de segurança neste projeto, **não abra uma issue pública**.

Em vez disso, envie um e-mail para **security@example.com** com:

1. Descrição da vulnerabilidade
2. Passos para reprodução
3. Impacto potencial
4. Sugestão de correção (se houver)

Como canal alternativo, você também pode usar **GitHub Security Advisories** do repositório: `https://github.com/OWNER/REPO/security/advisories/new`.
Você receberá uma confirmação em até **48 horas** e uma resposta detalhada em até **7 dias**.

## Práticas de segurança adotadas

### Autenticação

- Todas as rotas (exceto `/health`) exigem header `X-Api-Key`
- Comparação de token usa **timing-safe equality** para evitar timing attacks
- API Keys são carregadas via variáveis de ambiente ou arquivos de configuração externos, nunca hardcoded no código-fonte

### Comunicação

- Chamadas ao ViaCEP são feitas via **HTTPS**
- CORS habilitado sem restrição de origem (`*`) — adequado para APIs públicas de consulta

### Dependências

- CI/CD executa build em todas as linguagens a cada push/PR
- Dependabot configurado para monitorar atualizações de dependências

### Configuração

- Arquivos `.env` estão no `.gitignore` — apenas `.env.example` é versionado
- Arquivos de configuração versionados devem conter apenas valores de exemplo/placeholder; secrets reais devem ficar em variáveis de ambiente ou arquivos externos não versionados
- Caso alguma credencial seja exposta no repositório, ela deve ser removida dos arquivos versionados e rotacionada imediatamente

## Disclosure

Este projeto é de código aberto sob licença. Use por sua conta e risco em ambientes de produção.
