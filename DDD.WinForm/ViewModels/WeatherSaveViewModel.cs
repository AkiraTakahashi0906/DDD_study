using DDD.Domain.Entities;
using DDD.Domain.Exceptions;
using DDD.Domain.Helpers;
using DDD.Domain.Repositories;
using DDD.Domain.ValueObjects;
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

        private IAreasRepository _areas;

        public WeatherSaveViewModel(IAreasRepository areas)
        {
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

        public BindingList<AreaEntity> Areas { get; set; }= new BindingList<AreaEntity>();

        public BindingList<Condition> Conditions { get; set; } 
       = new BindingList<Condition>(Condition.ToList());

        public void Save()
        {
            Guard.IsNull(SelectedAreaId, "エリアを選択してください");
            var temperature = Guard.IsFloat(TemperatureText, "温度の入力に誤りがあります");
        }
    }
}
