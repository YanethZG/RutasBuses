using RutasBuses.Models;

namespace RutasBuses.Repositories
{
    public interface IBusesRepositories
    {
        IEnumerable<BusesModel> GetAll();

        BusesModel GetBusesById(int id);

        void Add(BusesModel buses);

        void Edit(BusesModel buses);

        void Delete(int id);
        IEnumerable<BusesModel> GetAllBuses();
    }
}
