using Microsoft.AspNetCore.Mvc;
using ModelSolution;
using System.Data.SqlClient;


namespace Suporte_TI.Controllers
{
    public class LoginController : Controller
    {
        private ClassConnection _dbConnection; // Adicione essa variável para representar a conexão com o banco de dados

        public LoginController()
        {
            // Inicialize a conexão com o banco de dados no construtor do controlador
            _dbConnection = new ClassConnection("Ti"); // Substitua "SeuNomeDoBancoDeDados" pelo nome do banco de dados desejado
        }
        public IActionResult Index()
        {
            ViewBag.IsLoginPage = true;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string nome_usuario, string senha)
        {
            // Use a conexão com o banco de dados (_dbConnection) para verificar as credenciais
            using (_dbConnection.con)
            {
                _dbConnection.con.Open();

                // Crie o comando SQL para verificar o usuário no banco de dados
                string query = "SELECT * FROM Usuario WHERE nome_usuario = @nome_usuario AND senha = @senha";
                SqlCommand cmd = new SqlCommand(query, _dbConnection.con);
                cmd.Parameters.AddWithValue("@nome_usuario", nome_usuario);
                cmd.Parameters.AddWithValue("@senha", senha);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Usuário autenticado com sucesso, você pode definir uma sessão aqui ou usar o ASP.NET Identity
                   return RedirectToAction("Index", "Home"); // Substitua "PaginaInicial" e "Home" pelas suas páginas desejadas
                }
                else
                {
                    // Caso o usuário não seja encontrado ou a senha esteja incorreta, exiba uma mensagem de erro
                    ViewBag.MensagemErro = "Usuário ou senha inválidos";
                    return View();
                }
            }
        }
    }

}
