package com.viacep.dto;

public class ErrorResponseDto {
    private String mensagem;
    private int statusCode;

    public ErrorResponseDto(String mensagem, int statusCode) {
        this.mensagem = mensagem;
        this.statusCode = statusCode;
    }

    public String getMensagem() { return mensagem; }
    public void setMensagem(String mensagem) { this.mensagem = mensagem; }
    public int getStatusCode() { return statusCode; }
    public void setStatusCode(int statusCode) { this.statusCode = statusCode; }
}
