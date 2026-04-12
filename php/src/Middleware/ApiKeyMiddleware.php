<?php

declare(strict_types=1);

namespace App\Middleware;

use Psr\Http\Message\ResponseInterface;
use Psr\Http\Message\ServerRequestInterface;
use Psr\Http\Server\MiddlewareInterface;
use Psr\Http\Server\RequestHandlerInterface;
use Slim\Psr7\Response;

class ApiKeyMiddleware implements MiddlewareInterface
{
    private const HEADER_NAME = 'X-Api-Key';

    public function process(ServerRequestInterface $request, RequestHandlerInterface $handler): ResponseInterface
    {
        $expected = $_ENV['API_KEY'];
        $received = $request->getHeaderLine(self::HEADER_NAME);

        if (!hash_equals($expected, $received)) {
            $response = new Response();
            $response->getBody()->write(json_encode([
                'mensagem' => 'Token de acesso inválido ou ausente.',
                'statusCode' => 401,
            ]));
            return $response
                ->withStatus(401)
                ->withHeader('Content-Type', 'application/json');
        }

        return $handler->handle($request);
    }
}
