package com.example.POCEmpleado.controller;

import com.example.POCEmpleado.model.Instrumento;
import com.example.POCEmpleado.model.InstrumentoInput;
import com.example.POCEmpleado.servicios.ServicioInstrumento;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.MutationMapping;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.stereotype.Controller;

import java.util.List;
import java.util.Optional;

@Controller
public class EmpleadoController {

    @QueryMapping
    public List<Instrumento> instrumento(){

        return ServicioInstrumento.listarInstrumentos();
    }

    @QueryMapping
    public Optional<Instrumento> instrumentoPorCodigo(@Argument int codigo){
        return ServicioInstrumento.buscarPorCodigo(codigo);
    }

    @MutationMapping
    public Optional<Instrumento> addInstrumento(@Argument("input") InstrumentoInput input) throws Exception {
        return ServicioInstrumento.addInstrumento(input);
    }

    @MutationMapping
    public Optional<Instrumento> delInstrumento(@Argument("input") int codigo) {
        return ServicioInstrumento.delInstrumento(codigo);
    }

    @MutationMapping
    public Optional<Instrumento> editInstrumento(@Argument("input") InstrumentoInput ins) throws Exception {
        return ServicioInstrumento.editInstrumento(ins);
    }

}
