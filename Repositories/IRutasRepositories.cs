using RutasBuses.Models;
using System.Data;  

namespace RutasBuses.Repositories
{
    public interface IRutasRepository
    {
        IEnumerable<RutasModel> GetAll();

        RutasModel GetRutasById(int id);

        void Add(RutasModel rutas);

        void Edit(RutasModel rutas);

        void Delete(int id);
        IEnumerable<BusesModel> GetAllBuses();
    }

}
