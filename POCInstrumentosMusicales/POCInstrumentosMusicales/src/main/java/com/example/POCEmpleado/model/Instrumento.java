package com.example.POCEmpleado.model;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;

@Data
@NoArgsConstructor
public class Instrumento {
    private int id;
    private String nombre;
    private double precio;
    private LocalDate fechaVenta;
    private int numeroCuerdas;
    private int numeroTrastes;


    public Instrumento(int id, String nombre) throws Exception {
        setId(id);
        this.nombre = nombre;
    }



    public Instrumento(int id, String nombre, double precio, LocalDate fechaVenta, int numeroCuerdas, int numeroTrastes) throws Exception {
        this.id = id;
        this.nombre = nombre;
        setId(id);
        this.fechaVenta = fechaVenta;
        setNombre(nombre);
        setPrecio(precio);
        setNumeroCuerdas(numeroCuerdas);
        setNumeroTrastes(numeroTrastes);

    }

    public void setPrecio(double precio) throws Exception {
        if (precio > 0) {
            this.precio = precio;
        } else {
            throw new Exception("Precio menor o igual a 0");
        }
    }


    public void setId(int id) throws Exception {

        if (id > 0) {
            this.id = id;
        } else {
            throw new Exception("Id menor a 0");
        }

    }


    public void setNombre(String nombre) throws Exception {

        if(nombre.isEmpty())
        {
            throw new Exception ("No se puede crear un instrumento sin un nombre");
        }
        else
        {
            this.nombre = nombre;
        }
    }


    public void setNumeroCuerdas(int numeroCuerdas) throws Exception
    {
        if(numeroCuerdas > 0)
        {
            this.numeroCuerdas = numeroCuerdas;
        }
        else
        {
            throw new Exception("Numero de cuerdas menor o igual a 0");
        }
    }


    public void setNumeroTrastes(int numeroTrastes) throws Exception
    {
        if (numeroTrastes >= 0) {
            this.numeroTrastes =  numeroTrastes;
        } else {
            throw new Exception("Número de trastes menor a 0");
        }
    }
}
