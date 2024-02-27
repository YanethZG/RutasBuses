using RutasBuses.Models;
using static RutasBuses.Repositories.RutasRepositories;
using System.Data;
using RutasBuses.Data;
using Microsoft.Data.SqlClient;

namespace RutasBuses.Repositories
{
    public class RutasRepositories : IRutasRepository
    {
        private readonly SqlDataAccess _dbConnection;

        public RutasRepositories(SqlDataAccess dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<RutasModel> GetAll()
        {
            List<RutasModel> rutasList = new List<RutasModel>();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT Rutas.id_Ruta,Buses.Capacidad,
                                            Buses.id_Bus, Buses.Placa, Buses.Fecha_Fabricacion, Rutas.Nombre_Ruta, Rutas.Valor_Pasaje
                                            FROM Rutas
                                            INNER JOIN Buses
                                            ON Rutas.id_Ruta = Buses.id_Ruta;";

                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RutasModel rutas = new RutasModel();
                            rutas.id_Rutas = Convert.ToInt32(reader["id_Ruta"]);
                            rutas.Nombre_Ruta = reader["Nombre_Ruta"].ToString();
                            rutas.Valor_Pasaje = Convert.ToDecimal(reader["Valor_Pasaje"]);

                            rutasList.Add(rutas);
                        }
                    }
                }
            }

            return rutasList;
        }

        public void Add(RutasModel rutas)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Rutas 
                                           VALUES(@Nombre_Ruta, @id_Ruta)";

                    command.Parameters.AddWithValue("@Nombre_Ruta", rutas.Nombre_Ruta);
                    command.Parameters.AddWithValue("@id_Ruta", rutas.id_Rutas);

                    command.CommandType = CommandType.Text;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(RutasModel rutas)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE 
                                           SET Nombre_Ruta = @Nombre_Ruta,
                                           WHERE id_Ruta = @id_Ruta";

                    command.Parameters.AddWithValue("@Nombre_Ruta", rutas.Nombre_Ruta);
                    command.Parameters.AddWithValue("@id_Ruta", rutas.id_Rutas);

                    command.CommandType = CommandType.Text;

                    command.ExecuteNonQuery();
                }
            }
        }



        public RutasModel? GetRutasById(int id)
        {
            RutasModel rutas = new RutasModel();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT id_Ruta, Nombre_Ruta, Valor_Pasaje FROM Rutas
                                            WHERE id_Ruta = @id_Ruta";

                    command.Parameters.AddWithValue("@id_Ruta", id);

                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rutas.id_Rutas = Convert.ToInt32(reader["id_Ruta"]);
                            rutas.Nombre_Ruta = reader["Nombre_Ruta"].ToString();
                            rutas.Valor_Pasaje = Convert.ToDecimal(reader["Valor_Pasaje"]); 
                        }
                    }
                }
            }

            return rutas;
        }

        public IEnumerable<BusesModel> GetAllBuses()
        {
            throw new NotImplementedException();
        }
    }
}


