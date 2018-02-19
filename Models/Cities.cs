using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WorldData;

namespace WorldData.Models
{
  public class City
  {
    private string _name;
    private string _code;
    private string _district;
    private int _pop;

    public City(string name, string code, string district, int pop)
    {
      _name = name;
      _code = code;
      _district = district;
      _pop = pop;
    }

    public string GetName(){
      return _name;
    }

    public string GetCode(){
      return _code;
    }

    public string GetDistrict(){
      return _district;
    }

    public int GetPop(){
      return _pop;
    }

    public static Dictionary<string, List<City>> GetCities()
    {
      Dictionary<string, List<City>> allCities = new Dictionary<string, List<City>>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM city;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        string name = rdr.GetString(1);
        string code = rdr.GetString(2);
        string district = rdr.GetString(3);
        int population = rdr.GetInt32(4);
        City newCity = new City(name, code, district, population);
        if (!allCities.ContainsKey(code))
        {
          allCities.Add(code, new List<City>{newCity});
        }
        else
        {
          allCities[code].Add(newCity);
        }
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCities;
    }
  }
}
