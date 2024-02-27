using RutasBuses.Models;
using static RutasBuses.Repositories.RutasRepositories;
using System.Data;
using RutasBuses.Data;
using Microsoft.Data.SqlClient;

namespace RutasBuses.Repositories
{
    public class BusesRepositories : IBusesRepositories
    {
        private readonly SqlDataAccess _dbConnection;

        public BusesRepositories(SqlDataAccess dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<BusesModel> GetAll()
        {
            List<BusesModel> busesList = new List<BusesModel>();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT Rutas.id_Ruta,Buses.Capacidad,
                                            Buses.id_Bus, Buses.Placa, Buses.Fecha_Fabricacion, Rutas.Nombre_Ruta, Rutas.Valor_Pasaje
                                            FROM Buses
                                            INNER JOIN Rutas
                                            ON Rutas.id_Ruta = Buses.id_Ruta;";

                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BusesModel buses = new BusesModel();
                            buses.id_Buses = Convert.ToInt32(reader["id_Bus"]);
                            buses.Capacidad = reader["Capacidad"].ToString();
                            buses.Placa = Convert.ToString(reader["Placa"]);
                            buses.Fecha_Fabricacion = Convert.ToDateTime(reader["Fecha_Fabricacion"]);

                            busesList.Add(buses);
                        }
                    }
                }
            }

            return busesList;
        }

        public void Add(BusesModel buses)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Buses
                                           VALUES(@Capacidad, @Placa, @Fecha_Fabricacion)";

                    command.Parameters.AddWithValue("@Capacidad", buses.Capacidad);
                    command.Parameters.AddWithValue("@Placa", buses.Placa);
                    command.Parameters.AddWithValue("@Fecha_Fabricacion", buses.Fecha_Fabricacion);

                    command.CommandType = CommandType.Text;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(BusesModel buses)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE 
                                           SET Capacidad = @Capacidad, Placa = @Placa
                                           WHERE id_Bus = @id_Bus";

                    command.Parameters.AddWithValue("@Capacidad", buses.Capacidad);
                    command.Parameters.AddWithValue("@Placa", buses.Placa);
                    command.Parameters.AddWithValue("@id_Bus", buses.id_Buses);

                    command.CommandType = CommandType.Text;

                    command.ExecuteNonQuery();
                }
            }
        }



        public BusesModel? GetBusesById(int id)
        {
            BusesModel buses = new BusesModel();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT id_Bus, Capacidad, Placa, Fecha_Fabricacion
                                          FROM Buses
                                          WHERE id_Bus = @id_Bus";

                    command.Parameters.AddWithValue("@id_Bus", id);

                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            buses.id_Buses = Convert.ToInt32(reader["id_Bus"]);
                            buses.Capacidad = reader["Capacidad"].ToString();
                            buses.Placa = Convert.ToString(reader["Placa"]);
                            buses.Fecha_Fabricacion= Convert.ToDateTime(reader["Fecha_Fabricacion"]);
                        }
                    }
                }
            }

            return buses;
        }

        public IEnumerable<BusesModel> GetAllBuses()
        {
            throw new NotImplementedException();
        }
    }
}
