package com.viacep.controller;

import com.viacep.dto.EnderecoResponseDto;
import com.viacep.dto.ErrorResponseDto;
import com.viacep.exception.CepNotFoundException;
import com.viacep.exception.CepValidationException;
import com.viacep.exception.ExternalServiceException;
import com.viacep.service.ViaCepService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/cep")
public class CepController {

    private static final Logger log = LoggerFactory.getLogger(CepController.class);
    private final ViaCepService viaCepService;

    public CepController(ViaCepService viaCepService) {
        this.viaCepService = viaCepService;
    }

    @Operation(summary = "Consulta informações de endereço para o CEP informado.")
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Endereço encontrado com sucesso."),
            @ApiResponse(responseCode = "400", description = "CEP informado possui formato inválido."),
            @ApiResponse(responseCode = "404", description = "CEP não encontrado na base da ViaCEP."),
            @ApiResponse(responseCode = "500", description = "Erro interno ou falha na comunicação com o serviço ViaCEP.")
    })
    @GetMapping("/{cep}")
    public ResponseEntity<?> getCep(@PathVariable String cep) {
        log.info("[java] Requisição recebida para o CEP: {}", cep);

        try {
            EnderecoResponseDto endereco = viaCepService.consultarCep(cep);
            return ResponseEntity.ok(endereco);
        } catch (CepValidationException ex) {
            log.warn("[java] Formato inválido de CEP: {}. Detalhe: {}", cep, ex.getMessage());
            return ResponseEntity.badRequest().body(new ErrorResponseDto(ex.getMessage(), 400));
        } catch (CepNotFoundException ex) {
            log.warn("[java] CEP não encontrado: {}. Detalhe: {}", cep, ex.getMessage());
            return ResponseEntity.status(404).body(new ErrorResponseDto(ex.getMessage(), 404));
        } catch (ExternalServiceException ex) {
            log.error("[java] Falha na integração com ViaCEP para o CEP: {}", cep, ex);
            return ResponseEntity.status(500)
                    .body(new ErrorResponseDto("Erro ao consultar o serviço externo ViaCEP.", 500));
        } catch (Exception ex) {
            log.error("[java] Erro inesperado ao processar CEP: {}", cep, ex);
            return ResponseEntity.status(500)
                    .body(new ErrorResponseDto("Ocorreu um erro interno inesperado.", 500));
        }
    }
}
