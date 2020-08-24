using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using HondaALL.Models.Dto;
using RestSharp;
using Newtonsoft.Json;

namespace HondaALL.Models
{
    public class ClassCrud
    {
        private static string[] urls = Ut.GetUrls();
        public static List<ModelCar> GetModelCars()
        {
            List<ModelCar> list = new List<ModelCar>();

            for(int i=0; i< urls.Length; i++)
            {
                try
                {
                    string content = RestWork.GetContent($"{urls[i]}/models", Method.GET);

                    List<ModelCar> listTmp = JsonConvert.DeserializeObject<List<ModelCar>>(content);
                    list.AddRange(listTmp);
                }
                catch (Exception ex)
                {
                    string Error = ex.Message;
                    int o = 0;
                }
            }

            list = list.OrderBy(x => x.name).ToList().Distinct().ToList();

            return list;
        }
        public static List<Filters> GetFilters(string model_id,List<string> param)
        {
                List<Filters> list = GetFiltersStart();

                try
                {
                    list = EuWorker.GetFilters(model_id, param, list);
                    list = GmWorker.GetFilters(model_id, param, list);
                    list = JpWorker.GetFilters(model_id, param, list);

                for (int i=0;i<list.Count;i++)
                    {
                        list[i].values = list[i].values.Distinct().ToList().OrderBy(x => x.name).ToList();
                    }
                }
                catch (Exception ex)
                {
                    string Error = ex.Message;
                    int o = 0;
                }

            return list;
        }
        public static List<lang> GetLang()
        {
            List<lang> list = new List<lang>();

            lang en = new lang { code = "EN", name = "English", is_default = true };
            list.Add(en);

            lang ru = new lang { code = "RU", name = "Русский", is_default = false };
            list.Add(ru);

            return list;
        }
        public static List<header> GetHeaders(bool engineEmpty=false, bool countryEmpty = false, bool yearEmpty = false)
        {
            List<header> list = new List<header>();

            header model_name = new header { code = "model_name", title = "Модель" };
            list.Add(model_name);
            header door = new header { code = "door", title = "Двери" };
            list.Add(door);
            header trans = new header { code = "trans", title = "Трансмиссия" };
            list.Add(trans);

            if(!engineEmpty)
            {
                header engine = new header { code = "engine", title = "Двигатель" };
                list.Add(engine);
            }
            if(!countryEmpty)
            {
                header country = new header { code = "country", title = "Страна" };
                list.Add(country);
            }

            if(!yearEmpty)
            {
                header dmodyr = new header { code = "year", title = "Год выпуска" };
                list.Add(dmodyr);
            }

            return list;
        }
        private static List<Filters> GetFiltersStart()
        {
            List<Filters> list = new List<Filters>();

            Filters door = new Filters { filter_id = "door", code = "door", name = "Двери" };
            list.Add(door);

            Filters trans = new Filters { filter_id = "trans", code = "trans", name = "Трансмиссия" };
            list.Add(trans);

            Filters engine = new Filters { filter_id = "engine", code = "engine", name = "Двигатель" };
            list.Add(engine);

            Filters country = new Filters { filter_id = "country", code = "country", name = "Страна" };
            list.Add(country);

            Filters yearIssue = new Filters { filter_id = "year", code = "year", name = "Год выпуска" };
            list.Add(yearIssue);

            return list;
        }
        public static List<CarTypeInfo> GetListCarTypeInfoFilterCars(string model_id, List<string> param)
        {
            List<CarTypeInfo> carTypeInfos = new List<CarTypeInfo>();

            try
            {
                carTypeInfos.AddRange(prefixAdd(EuWorker.GetFilterCars(model_id,  param), Ut.GetPrefixEU()));
                int y = 0;
            }
            catch (Exception ex)
            {
                string Errror = ex.Message;
                int y = 0;
            }

            try
            {
                carTypeInfos.AddRange(prefixAdd(GmWorker.GetFilterCars(model_id, param), Ut.GetPrefixGM()));
                int y = 0;
            }
            catch (Exception ex)
            {
                string Errror = ex.Message;
                int y = 0;
            }

            try
            {
                carTypeInfos.AddRange(prefixAdd(JpWorker.GetFilterCars(model_id, param), Ut.GetPrefixJP()));
                int y = 0;
            }
            catch (Exception ex)
            {
                string Errror = ex.Message;
                int y = 0;
            }
            //    carTypeInfos = carTypeInfos.OrderBy(x=>x.vehicle_id).Distinct().ToList();   //  .Skip((page - 1) * page_size).Take(page_size);
            return carTypeInfos;
        }
        public static List<CarTypeInfo> prefixAdd(List<CarTypeInfo> list, string strPrefix) //  EU_   GM_  JP_
        {
            for(int i=0;i<list.Count; i++)
            {
                if(!String.IsNullOrWhiteSpace(strPrefix))
                {
                    list[i].vehicle_id =  strPrefix + list[i].vehicle_id;
                }
            }
            return list;
        }
        public static List<PartsGroup> GetPartsGroup(string vehicle_id, string lang)
        {
            List<PartsGroup> list = new List<PartsGroup>();

            try
            {
                string url = Ut.GetUrlEU();
                string prefix = vehicle_id.Substring(0, 2);

                vehicle_id = vehicle_id.Substring(3, vehicle_id.Length - 3);

                if (prefix == "EU")
                {
                    url = Ut.GetUrlEU();
                }
                else if (prefix == "GM")
                {
                    url = Ut.GetUrlGM();
                }
                else if (prefix == "JP")
                {
                    url = Ut.GetUrlJP();
                }

                //   /vehicle/{vehicle_id:required}/mgroups
                string content = RestWork.GetContent($"{url}/vehicle/{vehicle_id}/mgroups", Method.GET);

                list = JsonConvert.DeserializeObject<List<PartsGroup>>(content);
            }
            catch(Exception ex)
            {
                string Error = ex.Message;
                int o = 0;
            }
            return list;
        }
        public static VehiclePropArr GetVehiclePropArr(string vehicle_id)
        {
            VehiclePropArr vehicle = new VehiclePropArr();

            try
            {
                string url = Ut.GetUrlEU();
                string prefix = vehicle_id.Substring(0, 2);

                vehicle_id = vehicle_id.Substring(3, vehicle_id.Length - 3);

                if (prefix == "EU")
                {
                    url = Ut.GetUrlEU();
                }
                else if (prefix == "GM")
                {
                    url = Ut.GetUrlGM();
                }
                else if (prefix == "JP")
                {
                    url = Ut.GetUrlJP();
                }

                string content = RestWork.GetContent($"{url}/vehicle/{vehicle_id}", Method.GET);

                vehicle = JsonConvert.DeserializeObject<VehiclePropArr>(content);
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                int o = 0;
            }
            return vehicle;
        }
    }
}

