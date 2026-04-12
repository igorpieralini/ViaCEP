package com.viacep.exception;

public class CepValidationException extends RuntimeException {
    public CepValidationException(String message) {
        super(message);
    }
}
