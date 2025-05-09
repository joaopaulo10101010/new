using System.Configuration;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using TrabalhoCidade.Models;

namespace TrabalhoCidade.Repositorio
{
    public class LoginRepositorio(IConfiguration configuration)
    {

        private readonly string _connectionString = configuration.GetConnectionString("MySQLConnection");

        

        public void AdicionarUsuario(Usuario usuario)
        {
            using (var db = new Conexao(_connectionString))
            {
                if (ValidarExistenciaUsuario(usuario) == false)
                {
                    var cmd = db.MySqlCommand();

                    cmd.CommandText = "INSERT INTO Usuario (Nome, Email, Senha) VALUES (@Nome,@Email,@Senha)";
                    cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                    cmd.ExecuteNonQuery();
                }
            }    
        }

        public Usuario ObterUsuario(Usuario usuario)
        {
            if(ValidarExistenciaUsuario(usuario) == true)
            {
                using (var db = new Conexao(_connectionString))
                {
                    var cmd = db.MySqlCommand();
                    cmd.CommandText = "SELECT * FROM Usuario WHERE Email = @Email";
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.ExecuteNonQuery();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Nome"),
                                Email = reader.GetString("Email"),
                                Senha = reader.GetString("Senha"),
                            };
                        }
                    }
                    return new Usuario();
                }
            }
            else
            {
                return null;
            }
        }
        
        public bool ValidarExistenciaUsuario(Usuario usuario)
        {
            using (var db = new Conexao(_connectionString))
            {
                var cmd = db.MySqlCommand();
                cmd.CommandText = "SELECT * FROM Tb_usuario WHERE Email = @Email";
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.ExecuteNonQuery();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var usuariobanco = new Usuario
                        {
                            Email = reader.GetString("Email"),
                        };
                        if (usuario.Email == usuariobanco.Email)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

        }
        



    }
}
