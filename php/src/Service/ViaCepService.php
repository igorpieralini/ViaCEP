<?php

declare(strict_types=1);

namespace App\Service;

use Symfony\Component\Cache\Adapter\ArrayAdapter;
use Symfony\Contracts\Cache\ItemInterface;

class ViaCepService
{
    private ViaCepClient $client;
    private ArrayAdapter $cache;
    private int $cacheMinutes;

    public function __construct()
    {
        $this->client = new ViaCepClient();
        $this->cacheMinutes = (int) ($_ENV['CACHE_MINUTES'] ?? 60);
        $this->cache = new ArrayAdapter($this->cacheMinutes * 60);
    }

    public function consultarCep(string $cep): array
    {
        $cepLimpo = str_replace('-', '', trim($cep));

        if (!preg_match('/^\d{8}$/', $cepLimpo)) {
            throw new \InvalidArgumentException('CEP inválido. Informe somente 8 dígitos numéricos.');
        }

        $cacheKey = 'cep_' . $cepLimpo;

        return $this->cache->get($cacheKey, function (ItemInterface $item) use ($cepLimpo): array {
            $item->expiresAfter($this->cacheMinutes * 60);

            $data = $this->client->buscarCep($cepLimpo);

            if ($data === null || !empty($data['erro'])) {
                throw new \RuntimeException("CEP '{$cepLimpo}' não encontrado.");
            }

            return [
                'cep' => $data['cep'] ?? '',
                'logradouro' => $data['logradouro'] ?? '',
                'complemento' => $data['complemento'] ?? '',
                'bairro' => $data['bairro'] ?? '',
                'cidade' => $data['localidade'] ?? '',
                'estado' => $data['uf'] ?? '',
                'ibge' => $data['ibge'] ?? '',
                'gia' => $data['gia'] ?? '',
                'ddd' => $data['ddd'] ?? '',
                'siafi' => $data['siafi'] ?? '',
            ];
        });
    }
}
