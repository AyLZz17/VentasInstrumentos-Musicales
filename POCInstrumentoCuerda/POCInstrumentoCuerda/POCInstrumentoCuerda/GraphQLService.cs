using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using POCInstrumentoCuerda.model;

namespace POCInstrumentoCuerda
{
    internal static class GraphQLService
    {
        private const string Endpoint = "http://localhost:8081/graphql";

        private static GraphQLHttpClient CreateClient()
        {
            return new GraphQLHttpClient(Endpoint, new SystemTextJsonSerializer());
        }

        public static async Task<List<Instrumento>> ListarAsync(string nombre, double? precioMaximo)
        {
            using (var client = CreateClient())
            {
                var response = await client.SendQueryAsync<InstrumentoQueryResponse>(new GraphQLRequest
                {
                    Query = "query ($nombre: String, $precioMaximo: Float) { instrumento(nombre: $nombre, precioMaximo: $precioMaximo) { id nombre precio fechaVenta numeroCuerdas numeroTrastes } }",
                    Variables = new { nombre, precioMaximo }
                });
                Validate(response.Errors);
                return response.Data.instrumento ?? new List<Instrumento>();
            }
        }

        public static async Task<Instrumento> BuscarAsync(int id)
        {
            using (var client = CreateClient())
            {
                var response = await client.SendQueryAsync<InstrumentoQueryResponse>(new GraphQLRequest
                {
                    Query = "query ($codigo: Int!) { instrumentoPorCodigo(codigo: $codigo) { id nombre precio fechaVenta numeroCuerdas numeroTrastes } }",
                    Variables = new { codigo = id }
                });
                Validate(response.Errors);
                return response.Data.instrumentoPorCodigo;
            }
        }

        public static async Task<Instrumento> AdicionarAsync(InstrumentoInput input)
        {
            using (var client = CreateClient())
            {
                var response = await client.SendMutationAsync<InstrumentoMutationResponse>(new GraphQLRequest
                {
                    Query = "mutation ($input: InstrumentoINput!) { addInstrumento(input: $input) { id nombre precio fechaVenta numeroCuerdas numeroTrastes } }",
                    Variables = new { input }
                });
                Validate(response.Errors);
                return response.Data.addInstrumento;
            }
        }

        public static async Task<Instrumento> EliminarAsync(int id)
        {
            using (var client = CreateClient())
            {
                var response = await client.SendMutationAsync<InstrumentoMutationResponse>(new GraphQLRequest
                {
                    Query = "mutation ($id: Int!) { delInstrumento(input: $id) { id nombre precio fechaVenta numeroCuerdas numeroTrastes } }",
                    Variables = new { id }
                });
                Validate(response.Errors);
                return response.Data.delInstrumento;
            }
        }

        public static async Task<Instrumento> ActualizarAsync(InstrumentoInput input)
        {
            using (var client = CreateClient())
            {
                var response = await client.SendMutationAsync<InstrumentoMutationResponse>(new GraphQLRequest
                {
                    Query = "mutation ($input: InstrumentoINput!) { editInstrumento(input: $input) { id nombre precio fechaVenta numeroCuerdas numeroTrastes } }",
                    Variables = new { input }
                });
                Validate(response.Errors);
                return response.Data.editInstrumento;
            }
        }

        private static void Validate(GraphQLError[] errors)
        {
            if (errors != null && errors.Length > 0)
                throw new InvalidOperationException(string.Join(Environment.NewLine, errors.Select(error => error.Message)));
        }
    }
}
