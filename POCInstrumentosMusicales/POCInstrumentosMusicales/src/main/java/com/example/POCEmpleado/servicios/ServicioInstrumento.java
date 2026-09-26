package com.example.POCEmpleado.servicios;

import com.example.POCEmpleado.model.Instrumento;
import com.example.POCEmpleado.model.InstrumentoInput;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public class ServicioInstrumento {
    private static List<Instrumento>  instrumentos = new ArrayList<>();

    public static List<Instrumento> listarInstrumentos(){
        return List.copyOf(instrumentos);
    }

    public static List<Instrumento> listarInstrumentos(String nombre, Double precioMaximo){
        return instrumentos.stream()
                .filter(instrumento -> nombre == null || nombre.isBlank()
                        || instrumento.getNombre().toLowerCase().contains(nombre.toLowerCase()))
                .filter(instrumento -> precioMaximo == null || instrumento.getPrecio() <= precioMaximo)
                .toList();
    }

    public static Optional<Instrumento> buscarPorCodigo(int codigo){
        return instrumentos.stream()
                .filter( e -> e.getId() == codigo)
                .findFirst();
    }

    public static Optional<Instrumento> addInstrumento(InstrumentoInput input) throws Exception {
        for (Instrumento instrumento : listarInstrumentos()){
            if (input.id() < 0 || input.id() == instrumento.getId()){
                throw  new Exception("id invalido");
            }
        }
        Instrumento ins = new Instrumento(input.id(), input.nombre(), input.precio(), input.fechaVenta(), input.numeroCuerdas(), input.numeroTrastes());
        instrumentos.add(ins);

        return Optional.of(ins);
    }

    public static Optional<Instrumento> delInstrumento(int codigo) {
        for (Instrumento instrumento : listarInstrumentos()){
            if (instrumento.getId() == codigo){
                instrumentos.remove(instrumento);
                return Optional.of(instrumento);
            }
        }


        return null;
    }

    public static Optional<Instrumento> editInstrumento(InstrumentoInput ins) throws Exception {
        for (Instrumento instrumento : listarInstrumentos()){

            Instrumento inst = new Instrumento(ins.id(), ins.nombre(), ins.precio(), ins.fechaVenta(), ins.numeroCuerdas(), ins.numeroTrastes());

            if (instrumento.getId() == ins.id() && inst != null) {
                instrumentos.add(inst);
                instrumentos.remove(instrumento);
                return Optional.of(inst);
            }
        }
        return null;


    }

}
