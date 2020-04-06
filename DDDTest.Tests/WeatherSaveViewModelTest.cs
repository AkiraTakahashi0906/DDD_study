﻿using System;
using System.Collections.Generic;
using DDD.Domain.Entities;
using DDD.Domain.Exceptions;
using DDD.Domain.Repositories;
using DDD.WinForm.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DDDTest.Tests
{
    [TestClass]
    public class WeatherSaveViewModelTest
    {
        [TestMethod]
        public void 天気シナリオ()
        {

            var areasMock = new Mock<IAreasRepository>();
            var areas = new List<AreaEntity>();
            areas.Add(new AreaEntity(1, "東京"));
            areas.Add(new AreaEntity(2, "神戸"));

            areasMock.Setup(x => x.GetData()).Returns(areas);
            //viewModelMockを作るときに、引数にareasMock.Objectを投げる
            var viewModelMock = new Mock<WeatherSaveViewModel>(areasMock.Object);
            //viewModelではGetDateTime=>DateTime.now
            //テストではGetDateTimeが切り替わる
            viewModelMock.Setup(x => x.GetDateTime()).Returns(
                Convert.ToDateTime("2018/01/01 12:34:56"));

            //初期値のテスト
            var viewModel = viewModelMock.Object;
            viewModel.SelectedAreaId.IsNull();
            viewModel.DataDateValue.Is(Convert.ToDateTime("2018/01/01 12:34:56"));
            viewModel.SelectedCondition.Is(1);
            viewModel.TemperatureText.Is("");

            viewModel.Areas.Count.Is(2);
            viewModel.Conditions.Count.Is(4);

            //セーブボタンが押されたときのテスト
            //viewModel.Save();

            //Chaining Assertion 参照
            var ex = AssertEx.Throws<InputException>(() => viewModel.Save());
            ex.Message.Is("エリアを選択してください");

            viewModel.SelectedAreaId = 2;

            ex = AssertEx.Throws<InputException>(() => viewModel.Save());
            ex.Message.Is("温度の入力に誤りがあります");

        }
    }
}
