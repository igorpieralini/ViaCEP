package com.viacep.service;

import com.viacep.dto.ViaCepResponseDto;
import com.viacep.exception.ExternalServiceException;
import io.github.resilience4j.retry.annotation.Retry;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClientException;
import org.springframework.web.client.RestTemplate;

@Service
public class ViaCepClient {

    private static final Logger log = LoggerFactory.getLogger(ViaCepClient.class);

    private final RestTemplate restTemplate;
    private final String baseUrl;

    public ViaCepClient(RestTemplate restTemplate, @Value("${viacep.base-url}") String baseUrl) {
        this.restTemplate = restTemplate;
        this.baseUrl = baseUrl;
    }

    @Retry(name = "viaCep")
    public ViaCepResponseDto buscarCep(String cep) {
        String url = baseUrl + "/" + cep + "/json/";
        log.info("[java] Consultando ViaCEP para o CEP: {}", cep);

        try {
            ViaCepResponseDto response = restTemplate.getForObject(url, ViaCepResponseDto.class);
            log.info("[java] Resposta recebida da ViaCEP para o CEP: {}", cep);
            return response;
        } catch (RestClientException ex) {
            log.error("[java] Erro ao consultar ViaCEP para o CEP: {}", cep, ex);
            throw new ExternalServiceException("Falha ao comunicar com o serviço ViaCEP.", ex);
        }
    }
}
