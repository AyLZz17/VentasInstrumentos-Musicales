using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace POCInstrumentoCuerda.model
{
    public class Instrumento
    {
        [JsonPropertyName("id")] public string id { get; set; }
        [JsonPropertyName("nombre")] public string nombre { get; set; }
        [JsonPropertyName("precio")] public double precio { get; set; }
        [JsonPropertyName("fechaVenta")] public string fechaVenta { get; set; }
        [JsonPropertyName("numeroCuerdas")] public int numeroCuerdas { get; set; }
        [JsonPropertyName("numeroTrastes")] public int numeroTrastes { get; set; }
    }

    public class InstrumentoInput
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public string fechaVenta { get; set; }
        public int numeroCuerdas { get; set; }
        public int numeroTrastes { get; set; }
    }

    public class InstrumentoQueryResponse
    {
        [JsonPropertyName("instrumento")] public List<Instrumento> instrumento { get; set; }
        [JsonPropertyName("instrumentoPorCodigo")] public Instrumento instrumentoPorCodigo { get; set; }
    }

    public class InstrumentoMutationResponse
    {
        [JsonPropertyName("addInstrumento")] public Instrumento addInstrumento { get; set; }
        [JsonPropertyName("delInstrumento")] public Instrumento delInstrumento { get; set; }
        [JsonPropertyName("editInstrumento")] public Instrumento editInstrumento { get; set; }
    }
}
