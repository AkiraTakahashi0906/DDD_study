using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DDD.infrastructure.SQLite
{
    public  class WeatherSQLite : IWeatherRepository
    {
        public WeatherEntity GetLatest(int areaId)
        {
            string sql = @"
select DataDate,
    condition,
    temperature
from Weather
where AreaId = @AreaId
order by DataDate desc
LIMIT 1
";
            return SQLiteHelper.QuerySingle<WeatherEntity>(sql,
                new List<SQLiteParameter>
                {
                    new SQLiteParameter("@AreaID",areaId)
                }.ToArray(),reader => 
            {
                return new WeatherEntity(
                                areaId,
                                Convert.ToDateTime(reader["DataDate"]),
                                Convert.ToInt32(reader["Condition"]),
                                Convert.ToSingle(reader["Temperature"]));
            }, null);
        }

        public IReadOnlyList<WeatherEntity> GetData()
        {
            throw new NotImplementedException();
        }
    }
}
