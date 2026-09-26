package com.example.POCEmpleado.model;

import java.time.LocalDate;

public record InstrumentoInput(int id, String nombre,
        double precio,
        LocalDate fechaVenta,
        int numeroCuerdas,
        int numeroTrastes) {
}
