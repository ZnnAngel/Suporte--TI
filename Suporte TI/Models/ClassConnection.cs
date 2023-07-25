using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ModelSolution
{
    public class ClassConnection : IDisposable
    {
        public SqlConnection con;
        private SqlTransaction tran;

        /// <summary>
        /// Cria a classe de conexão de acordo com o banco de dados do Cliente
        /// </summary>
        /// <param name="dbName"></param>
        public ClassConnection(string dbName)
        {
            string ConnectionString = "Server=TIC-06;";
            ConnectionString += "Database=" + dbName;
            ConnectionString += ";Uid=System";
            ConnectionString += ";Pwd=123456;";

            con = new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Abre conexao
        /// </summary>
        /// <param name="dbName"></param>
        public void abreConexao()
        {
            try
            {
                con.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Fecha Conexão
        /// </summary>
        public void fechaConexao()
        {
            try
            {
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executa comando sql e retorna um dataReader
        /// </summary>
        /// <param name="comand"></param>
        /// <returns></returns>
        public SqlDataReader executeComand(string comand)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(comand, con);
                cmd.CommandTimeout = 999;
                return cmd.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executa comando sql e retorna um dataReader
        /// </summary>
        /// <param name="comand"></param>
        /// <returns></returns>
        public int executeComandInsert(string comand)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(comand, con);
                cmd.CommandTimeout = 600;

                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region controle de transação
        /// <summary>
        /// Inicia controle de transação
        /// </summary>
        public void iniciarTransacao()
        {
            try
            {
                tran = con.BeginTransaction();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Fecha a transação e caso ocorra problemas durante o commit, executa o rollback
        /// </summary>
        public void fechaTransacao()
        {
            try
            {
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }
        #endregion

        public void Dispose()
        {
            // Adicione aqui a lógica para liberar os recursos, como fechar a conexão com o banco de dados
            if (con != null)
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}
