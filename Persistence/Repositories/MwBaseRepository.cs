using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Persistence.Repositories
{

    public class Skladnik
    {
        public int ZestawMatId { get; set; }
        public string SkladnIndeks { get; set; }
        public string ZestawIndeks { get; set; }
        public string ZestawNazwa { get; set; }
        public string SkladnNazwa { get; set; }
        public decimal SkladnIlosc { get; set; }
        public string SkladnJm { get; set; }
    }

    public class MwBaseRepository
    {
        private readonly SqlConnection _dataConnection;
        private List<Skladnik> _listaSkladnikow;

        public MwBaseRepository()
        {
            _dataConnection = new SqlConnection();
            _listaSkladnikow = new List<Skladnik>();
        }
        public List<Skladnik> PobierzSkladniki(string indeks)
        {

            // SqlConnection jest podklasą klasy ADO.NET o nazwie Connection
            // jest przeznaczona do obsługi połączeń z bazami danych SQL Server
            try
            {
                // Zbudowanie ConnectionString'a za pomocą obiektu SqlConnectionStringBuilder
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "192.168.1.186";
                builder.InitialCatalog = "mwbase";
                builder.IntegratedSecurity = false;
                builder.UserID = "sa";
                builder.Password = "#slican27$x";

                // Podstawienie zbudowanego ConnectionString'a do obiektu połączenia z bazą
                _dataConnection.ConnectionString = builder.ConnectionString;
                _dataConnection.Open();

                // utworzenie obiektu zawierającego treść zapytania SQL
                // polecenie tworzy obiekt SqlCommand
                SqlCommand dataCommand = new SqlCommand();

                // nadaje właściwości Connection obiektu SQLCommand wartość połączenia z bazą danych 
                dataCommand.Connection = _dataConnection;
                dataCommand.CommandType = CommandType.Text;

                dataCommand.CommandText =
                dataCommand.CommandText =
                         @"select
                            b.zestaw_matid    
						   ,b.zestaw_indeks
						   ,b.zestaw_nazwa
                           ,b.skladn_indeks
                           ,b.skladn_nazwa
                           ,b.skladnIlosc
                           ,b.skladnJm
                           FROM prdkabat.view_bomy as b
                            WHERE b.skladnIlosc >= @IloscMin
                            AND b.zestaw_indeks = @Indeks
                            AND b.skladn_matid != 0
                            AND b.skladn_matid is not null
                            ORDER BY b.kolejn";


                // parametry przekazywane do zapytania - ochrona przed sql injection
                SqlParameter param1 = new SqlParameter("@IloscMin", SqlDbType.Int, 50);
                param1.Value = 1;
                dataCommand.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter("@Indeks", SqlDbType.VarChar, 50);
                param2.Value = indeks;
                dataCommand.Parameters.Add(param2);

                //

                SqlDataReader dataReader = dataCommand.ExecuteReader();
                // obiekt SqlDataReader zawiera najbardziej aktualny wiersz pozyskiwany z bazy danych
                // metoda Read() pobiera kolejny wiersz zwraca true jeżeli został on pomyślnie odczytany
                // za pomocą GetXXX() wyodrębniamy informację z kolumny z pobranego wiersza
                // zamiast XXX wstawiamy typ danych z C# np. GetInt32(), GetString()
                // 0 oznacza pierwszą kolumnę

                while (dataReader.Read())
                {
                    int pozId = dataReader.GetInt32(0);
                    // obsługa wartości null
                    if (dataReader.IsDBNull(2))
                    {
                       // logowanie
                    }
                    else
                    {

                        string zestawIndeks = dataReader.GetString(dataReader.GetOrdinal("zestaw_indeks")).Trim();
                        string zestawNazwa = dataReader.GetString(dataReader.GetOrdinal("zestaw_nazwa")).Trim();
                        string skladnIndeks = dataReader.GetString(dataReader.GetOrdinal("skladn_indeks")).Trim();
                        string skladnNazwa = dataReader.GetString(dataReader.GetOrdinal("skladn_nazwa")).Trim();
                        string skladnJm = dataReader.GetString(dataReader.GetOrdinal("skladnJm")).Trim();
                        var skladnIlosc = dataReader.GetSqlDecimal(dataReader.GetOrdinal("skladnIlosc"));
                        int zestawMatId = (int)dataReader.GetSqlInt32(dataReader.GetOrdinal("zestaw_matid"));

                        _listaSkladnikow.Add(new Skladnik
                        {
                            ZestawMatId = zestawMatId,
                            ZestawIndeks = zestawIndeks,
                            ZestawNazwa = zestawNazwa,
                            SkladnIndeks = skladnIndeks,
                            SkladnNazwa = skladnNazwa,
                            SkladnIlosc = (decimal)skladnIlosc,
                            SkladnJm = skladnJm
                        });

                    }
                }
                // musimy zawsze zamknąć obiekt SqlDataReader
                dataReader.Close();

                //


            }
            catch (Exception)
            {
                throw;
            } 
            finally
            {
                // zamyka połączenie z bazą danych    
                _dataConnection.Close();
            }

            return _listaSkladnikow;
        }

    }
}
