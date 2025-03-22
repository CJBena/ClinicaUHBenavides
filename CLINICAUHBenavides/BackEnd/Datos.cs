using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CLINICAUHBenavides.BackEnd;

namespace CLINICAUHBenavides.BackEnd
{
    public class Datos
    {
        private readonly string _connectionString = "Server=CARLOSPC\\SQLEXPRES;Database=CLINICAUHBenavides;Integrated Security=True;";

        public List<Paciente> ObtenerPacientes()
        {
            List<Paciente> pacientes = new List<Paciente>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Cedula, Nombre, Apellidos, Telefono, FechaNacimiento, Edad FROM Pacientes";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Paciente paciente = new Paciente
                            {
                                Cedula = reader.GetString(0),
                                Nombre = reader.GetString(1),
                                Apellidos = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Telefono = reader.IsDBNull(3) ? null : reader.GetString(3),
                                FechaNacimiento = reader.GetDateTime(4),
                                Edad = reader.GetInt32(5)
                            };
                            pacientes.Add(paciente);
                        }
                    }
                }
            }
            return pacientes;
        }

        public void AgregarPaciente(Paciente paciente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Pacientes (Cedula, Nombre, Apellidos, Telefono, FechaNacimiento, Edad) VALUES (@Cedula, @Nombre, @Apellidos, @Telefono, @FechaNacimiento, @Edad)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cedula", paciente.Cedula);
                    command.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", paciente.Apellidos ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Telefono", paciente.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FechaNacimiento", paciente.FechaNacimiento);
                    command.Parameters.AddWithValue("@Edad", paciente.Edad);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ModificarPaciente(Paciente paciente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Pacientes SET Nombre = @Nombre, Apellidos = @Apellidos, Telefono = @Telefono, FechaNacimiento = @FechaNacimiento, Edad = @Edad WHERE Cedula = @Cedula";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cedula", paciente.Cedula);
                    command.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", paciente.Apellidos ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Telefono", paciente.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FechaNacimiento", paciente.FechaNacimiento);
                    command.Parameters.AddWithValue("@Edad", paciente.Edad);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarPaciente(string cedula)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Pacientes WHERE Cedula = @Cedula";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cedula", cedula);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ProcesarAccionPaciente(string accion, IFormCollection formData)
        {
            Paciente paciente = new Paciente
            {
                Cedula = formData["cedula"],
                Nombre = formData["nombre"],
                Apellidos = formData["apellidos"],
                Telefono = formData["telefono"],
                FechaNacimiento = DateTime.Parse(formData["fechaNacimiento"]),
                Edad = int.Parse(formData["edad"])
            };

            if (accion == "agregar")
            {
                AgregarPaciente(paciente);
            }
            else if (accion == "modificar")
            {
                ModificarPaciente(paciente);
            }
            else if (accion == "eliminar")
            {
                EliminarPaciente(paciente.Cedula);
            }
        }
    }

}

