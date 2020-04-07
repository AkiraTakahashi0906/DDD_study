using DDD.Domain.Entities;
using DDD.Domain.Exceptions;
using DDD.Domain.Helpers;
using DDD.Domain.Repositories;
using DDD.Domain.ValueObjects;
using DDD.infrastructure.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.WinForm.ViewModels
{
    public class WeatherSaveViewModel : ViewModelBase
    {

        private IWeatherRepository _weather;
        private IAreasRepository _areas;

        //引数なしのコンストラクタ
        public WeatherSaveViewModel() : this(new WeatherSQLite(), new AreasSQLite())
        {
        }
            public WeatherSaveViewModel(
            IWeatherRepository weather ,
            IAreasRepository areas)
        {
            _weather = weather;
            _areas = areas;
            DataDateValue = GetDateTime();
            SelectedCondition = Condition.Sunny.Value;
            TemperatureText = string.Empty;

            foreach (var area in areas.GetData())
            {
                Areas.Add(new AreaEntity(area.AreaId, area.AreaName));
            }
        }
        //基本的にコントロールのバインディングする型と合わせる
        public object  SelectedAreaId { get; set; }　//コンボボックス→オブジェクト
        public DateTime DataDateValue { get; set; }
        public object SelectedCondition { get; set; }　//コンボボックス→オブジェクト
        public string TemperatureText { get; set; }  //テキストボックス→オブジェクト
        public string TemperatureUnitName => Temperature.UnitName;

        public BindingList<AreaEntity> Areas { get; set; }= new BindingList<AreaEntity>();

        public BindingList<Condition> Conditions { get; set; } 
       = new BindingList<Condition>(Condition.ToList());

        public void Save()
        {
            Guard.IsNull(SelectedAreaId, "エリアを選択してください");
            var temperature = Guard.IsFloat(TemperatureText, "温度の入力に誤りがあります");

            var entity = new WeatherEntity(Convert.ToInt32(SelectedAreaId),
                DataDateValue,
                Convert.ToInt32(SelectedCondition),
                temperature);

            _weather.Save(entity);
        }
    }
}
