using Shared.Requests;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IListaService
    {
        Task<ListaResponse> GetByIdAsync(string id);

        Task<ListaResponse> CreateAsync(ListaRequest listaRequest);

        Task<ListaResponse> UpdateAsync(string id, ListaRequest listaRequest);

        Task DeleteAsync(string id);
    }
}
