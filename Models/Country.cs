using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WorldData;

namespace WorldData.Models
{
  public class Country
  {
    private string _country;
    private string _continent;
    private string _code;
    private string _pop;
    private List<City> _cities;

    public Country(string code, string country, string continent, string pop, List<City> cities)
    {
      _country = country;
      _continent = continent;
      _code = code.ToUpper();
      _pop = pop;
      _cities = cities;
    }

    public string GetName()
    {
      return _country;
    }

    public string GetContinent()
    {
      return _continent;
    }

    public string GetCode()
    {
      return _code;
    }

    public string GetPop()
    {
      return _pop;
    }

    public static Dictionary<string, Country> Find(string searchId)
    {
      Dictionary<string, Country> someCountries = new Dictionary<string, Country>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM country WHERE Name = '" + searchId +"' OR Code = '" + searchId +"' OR Continent = '" + searchId + "';";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      City.GetCities();
      while (rdr.Read())
      {
        string code = rdr.GetString(0);
        string country = rdr.GetString(1);
        string continent = rdr.GetString(2);
        string population = rdr.GetInt32(6).ToString();
        List<City> cities = City.GetCities()[code];
        Country newCountry = new Country (code, country, continent, population, cities);
        someCountries.Add(code, newCountry);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return someCountries;
    }

    public static Dictionary<string, Country> GetAll()
    {
      Dictionary<string, Country> allCountries = new Dictionary<string, Country>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM country;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      //City.GetCities();
      while (rdr.Read())
      {
        string code = rdr.GetString(0);
        string country = rdr.GetString(1);
        string continent = rdr.GetString(2);
        string population = rdr.GetInt32(6).ToString();
        List<City> cities = new List<City>();
        try
        {
          cities = City.GetCities()[code];
        }
        catch
        {
          Console.WriteLine(code);
        }
        Country newCountry = new Country (code, country, continent, population, cities);
        allCountries.Add(code, newCountry);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCountries;
    }
  }
}
