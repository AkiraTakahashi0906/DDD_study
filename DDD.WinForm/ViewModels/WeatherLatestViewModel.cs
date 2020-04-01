using DDD.Domain.Entities;
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
    public class WeatherLatestViewModel : ViewModelBase
    {
        private IWeatherRepository _weather;
        IAreasRepository _areas;

        //引数無しで呼ばれたら、下の引数ありのコンストラクタに　new WeatherSqlite()をなげる　本番コードDBを指定する
        public WeatherLatestViewModel():this(new WeatherSQLite(),new AreasSQLite())
        {
        }

        public WeatherLatestViewModel(IWeatherRepository weather,
            IAreasRepository areas)　//テストコード用　モックを指定する
        {
            _weather = weather;
            _areas = areas;

            foreach(var area in areas.GetData())
            {
                Areas.Add(new AreaEntity(area.AreaId, area.AreaName));
            }
        }

        private object _selectedAreaId;
        public object SelectedAreaId
        {
            get { return _selectedAreaId; }
            set
            {
                SetProperty(ref _selectedAreaId, value);
            }
        }

        private string _DataDateText = string.Empty;
        public string DataDateText
        {
            get { return _DataDateText; }
            set
            {
                SetProperty(ref _DataDateText, value);
            }
        }

        private string _ConditionText = string.Empty;
        public string ConditionText
        {
            get { return _ConditionText; }
            set
            {
                SetProperty(ref _ConditionText, value);
            }
        }

        private string _TemperatureText = string.Empty;
        public string TemperatureText
        {
            get { return _TemperatureText; }
            set
            {
                SetProperty(ref _TemperatureText, value);
            }
        }

        public BindingList<AreaEntity> Areas { get; set; }
        = new BindingList<AreaEntity>();

        public void Search()
        {
            var entity =_weather.GetLatest(Convert.ToInt32(_selectedAreaId));
            if (entity == null)
            {
                DataDateText = string.Empty;
                ConditionText = string.Empty;
                TemperatureText = string.Empty;
            }
            else
            {
                DataDateText = entity.DataDate.ToString();
                ConditionText = entity.Condition.DisplayValue;
                TemperatureText = entity.Temperature.DisplayValueWithUnitSpace;
            }
        }

    }
}
