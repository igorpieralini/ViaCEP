package com.viacep.dto;

public class EnderecoResponseDto {
    private String cep = "";
    private String logradouro = "";
    private String complemento = "";
    private String bairro = "";
    private String cidade = "";
    private String estado = "";
    private String ibge = "";
    private String gia = "";
    private String ddd = "";
    private String siafi = "";

    public EnderecoResponseDto() {}

    public String getCep() { return cep; }
    public void setCep(String cep) { this.cep = cep; }
    public String getLogradouro() { return logradouro; }
    public void setLogradouro(String logradouro) { this.logradouro = logradouro; }
    public String getComplemento() { return complemento; }
    public void setComplemento(String complemento) { this.complemento = complemento; }
    public String getBairro() { return bairro; }
    public void setBairro(String bairro) { this.bairro = bairro; }
    public String getCidade() { return cidade; }
    public void setCidade(String cidade) { this.cidade = cidade; }
    public String getEstado() { return estado; }
    public void setEstado(String estado) { this.estado = estado; }
    public String getIbge() { return ibge; }
    public void setIbge(String ibge) { this.ibge = ibge; }
    public String getGia() { return gia; }
    public void setGia(String gia) { this.gia = gia; }
    public String getDdd() { return ddd; }
    public void setDdd(String ddd) { this.ddd = ddd; }
    public String getSiafi() { return siafi; }
    public void setSiafi(String siafi) { this.siafi = siafi; }
}
