package com.viacep.service;

import com.viacep.dto.EnderecoResponseDto;
import com.viacep.dto.ViaCepResponseDto;
import com.viacep.exception.CepNotFoundException;
import com.viacep.exception.CepValidationException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.stereotype.Service;

import java.util.regex.Pattern;

@Service
public class ViaCepService {

    private static final Logger log = LoggerFactory.getLogger(ViaCepService.class);
    private static final Pattern CEP_REGEX = Pattern.compile("^\\d{8}$");

    private final ViaCepClient viaCepClient;

    @Value("${viacep.cache-minutes}")
    private int cacheMinutes;

    public ViaCepService(ViaCepClient viaCepClient) {
        this.viaCepClient = viaCepClient;
    }

    @Cacheable(value = "ceps", key = "#cep.replace('-', '').trim()")
    public EnderecoResponseDto consultarCep(String cep) {
        String cepLimpo = cep.replace("-", "").trim();

        if (!CEP_REGEX.matcher(cepLimpo).matches()) {
            log.warn("[java] CEP inválido recebido: {}", cep);
            throw new CepValidationException("CEP inválido. Informe somente 8 dígitos numéricos.");
        }

        ViaCepResponseDto resposta = viaCepClient.buscarCep(cepLimpo);

        if (resposta == null || Boolean.TRUE.equals(resposta.getErro())) {
            log.warn("[java] CEP não encontrado: {}", cepLimpo);
            throw new CepNotFoundException("CEP '" + cepLimpo + "' não encontrado.");
        }

        EnderecoResponseDto resultado = mapToDto(resposta);
        log.info("[java] CEP {} armazenado em cache por {} minutos.", cepLimpo, cacheMinutes);
        return resultado;
    }

    private EnderecoResponseDto mapToDto(ViaCepResponseDto dto) {
        EnderecoResponseDto result = new EnderecoResponseDto();
        result.setCep(dto.getCep() != null ? dto.getCep() : "");
        result.setLogradouro(dto.getLogradouro() != null ? dto.getLogradouro() : "");
        result.setComplemento(dto.getComplemento() != null ? dto.getComplemento() : "");
        result.setBairro(dto.getBairro() != null ? dto.getBairro() : "");
        result.setCidade(dto.getLocalidade() != null ? dto.getLocalidade() : "");
        result.setEstado(dto.getUf() != null ? dto.getUf() : "");
        result.setIbge(dto.getIbge() != null ? dto.getIbge() : "");
        result.setGia(dto.getGia() != null ? dto.getGia() : "");
        result.setDdd(dto.getDdd() != null ? dto.getDdd() : "");
        result.setSiafi(dto.getSiafi() != null ? dto.getSiafi() : "");
        return result;
    }
}
